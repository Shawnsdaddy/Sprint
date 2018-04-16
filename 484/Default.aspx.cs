using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using Newtonsoft.Json;

public partial class loginScreen : System.Web.UI.Page
{
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]

    protected void Page_Load(object sender, EventArgs e)
    {
        
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

        string errorMessage = string.Empty;

        bool isValidCaptcha = ValidateReCaptcha(ref errorMessage);

        if (isValidCaptcha)
        {


            Login1.FailureText = "Your login attempt was not successful. Please try again. ";
        string userName = Login1.UserName.Trim();
        string password = Login1.Password.Trim();

        e.Authenticated = false;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;

        command.CommandText = "SELECT Person.PersonEmail, Person.FirstName, Person.MI, Person.LastName,nickname, Person.loginCount, Person.PointsBalance, Person.PersonID, Person.Password, Person.JobTitle, Person.Privilege, Person.Status, "+
                         "Person.BusinessEntityID, Person.DefaultPage, BusinessEntity.GiveLimitation "+
                         "FROM Person INNER JOIN "+
                         " BusinessEntity ON Person.BusinessEntityID = BusinessEntity.BusinessEntityID where person.[PersonEmail] = @Email and BusinessEntity.Status = 1";
        command.Parameters.AddWithValue("@Email", userName);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            Session["GiveLimit"] =Convert.ToInt32( reader["GiveLimitation"].ToString());
            Session["E-mail"] = reader["PersonEmail"].ToString();
            Session["FirstName"] = reader["firstname"].ToString();
            Session["Middle"] = reader["MI"].ToString();
            Session["last"] = reader["LastName"].ToString();
            Session["PointsBalance"] = Convert.ToInt32(reader["PointsBalance"]);
            Session["loginCount"] = reader["loginCount"].ToString();
            Session["ID"] = Convert.ToInt32(reader["PersonID"]);
            Session["Status"]= Convert.ToInt32(reader["status"]);
            String pwHash = reader["Password"].ToString();  // retrieve the password hash
            Session["Privilege"] = reader["Privilege"].ToString();
            Session["BusinessEntityID"] = reader["BusinessEntityID"].ToString();
            Session["JobTitle"] = reader["JobTitle"].ToString();
            Session["DefaultPage"] = reader["DefaultPage"].ToString();
            Session["NickName"] = reader["nickname"].ToString();
            bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
            e.Authenticated = verify;

            
            switch (Session["Privilege"].ToString())
            {
                case "Administrative":
                    
                    switch(Session["DefaultPage"].ToString())
                    {
                        case "Homepage":
                            Login1.DestinationPageUrl = "CEOPostWall.aspx";
                            break;
                        case "ProviderInfor":
                            Login1.DestinationPageUrl = "CEO_AddProvider.aspx";
                            break;
                        case "EmployeeInfor":
                            Login1.DestinationPageUrl = "CreateEmployee.aspx";
                            break;
                        case "ViewReport":
                            Login1.DestinationPageUrl = "Report.aspx";
                            break;
                        case "Setting":
                            Login1.DestinationPageUrl = "CEOprofile.aspx";
                            break;
                        default:
                            Login1.DestinationPageUrl = "CEOPostWall.aspx";
                            break;

                    }
                    break;
                case "Employee":
                    switch (Session["DefaultPage"].ToString())
                    {
                        case "Homepage":
                            Login1.DestinationPageUrl = "EmployeeReward.aspx";
                            break;
                        case "GetReward":
                            Login1.DestinationPageUrl = "CashOut.aspx";
                            break;
                        case "DashBoard":
                            Login1.DestinationPageUrl = "UserDashboard.aspx";
                            break;
                        case "Setting":
                            Login1.DestinationPageUrl = "EmployeeProfile.aspx";
                            break;
                        default:
                            Login1.DestinationPageUrl = "EmployeeReward.aspx";
                            break;
                    }
                    break;
                case "SystemAdmin":
                    switch (Session["DefaultPage"].ToString())
                    {
                        case "Homepage":
                            Login1.DestinationPageUrl = "SystemAdminHome.aspx";
                            break;
                        case "UserInfo":
                            Login1.DestinationPageUrl = "SystemAdmin.aspx";
                            break;
                        case "Settings":
                            Login1.DestinationPageUrl = "SytemAdminprofile.aspx";
                            break;
                        default:
                            Login1.DestinationPageUrl = "SystemAdminHome.aspx";
                            break;

                    }
                   
                    break;
                case "RewardProvider":
                    switch (Session["DefaultPage"].ToString())
                    {
                        case "Homepage":
                            Login1.DestinationPageUrl = "RewardProvider.aspx";
                            break;
                        case "GiftCardInfo":
                            Login1.DestinationPageUrl = "GiftCardInfo.aspx";
                            break;
                        case "Setting":
                            Login1.DestinationPageUrl = "Providerprofile.aspx";
                            break;
                        default:
                            Login1.DestinationPageUrl = "RewardProvider.aspx";
                            break;
                    }
                   
                    break;
            }
            if (Session["Status"].ToString() == "0")
            {
                Login1.FailureText = "Your Account has been terminated!";
                con.Close();
            }
            else
            {
                if (e.Authenticated)
                {
                        Session["loggedIn"] = reader["lastname"].ToString() + " " + reader["firstname"];
                        Session["Password"] = password;
                        reader.Close();
                        if (Session["loginCount"].ToString() == "0")
                        {
                            Login1.DestinationPageUrl = "firsttime.aspx";
                        con.Close();
                        }
                        else
                        {
                            SqlCommand login = new SqlCommand("login_count", con);
                            login.CommandType = CommandType.StoredProcedure;
                            login.Parameters.AddWithValue("@LoginEmail", Session["E-mail"].ToString());
                            login.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                
            }
            reader.Close();
        }
        else
        {
            reader.Close();
        }

        if (!e.Authenticated)
        {
            
            command.CommandText = "select [ProviderName],[TypeOfBusiness],[ProviderEmail],ProviderID,Password,status,[DefaultPage] from RewardProvider where [ProviderEmail] = @Email";
            SqlDataReader rdr = command.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                Session["E-mail"] = rdr["ProviderEmail"].ToString();
                Session["ProviderName"] = rdr["ProviderName"].ToString();
                Session["ID"] = Convert.ToInt32(rdr["ProviderID"]);
                Session["Status"] = Convert.ToInt32(rdr["status"]);
                String pwHash = rdr["Password"].ToString();  // retrieve the password hash
                Session["Privilege"] = "RewardProvider";
                Session["DefaultPage"] = rdr["DefaultPage"].ToString();
                Session["TypeOfBusiness"] = rdr["TypeOfBusiness"].ToString();
                bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
                e.Authenticated = verify;
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Login1.DestinationPageUrl = "RewardProvider.aspx";
                        break;
                    case "GiftCardInfo":
                        Login1.DestinationPageUrl = "GiftCardInfo.aspx";
                        break;
                    case "Setting":
                        Login1.DestinationPageUrl = "Providerprofile.aspx";
                        break;
                    default:
                        Login1.DestinationPageUrl = "RewardProvider.aspx";
                        break;
                }
                if (Session["Status"].ToString() == "0")
                {
                    Login1.FailureText = "Your Account has been terminated!";
                    con.Close();
                }
                else
                {
                    if (e.Authenticated)
                    {
                        Session["loggedIn"] = Session["ProviderName"].ToString();
                        Session["Password"] = password;
                        rdr.Close();
                        con.Close();
                    }
                }
                rdr.Close();
            }
            else
            {
                rdr.Close();
            }
        }
        con.Close();

        }
        else
        {
            Response.Write("<script>alert('Please Verify You Are Not A Robot')</script>");
        }

    }
    protected void btnResetPass_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;

        command.CommandText = "select [PersonEmail] from person where [PersonEmail] = @email ";
        command.Parameters.AddWithValue("@email", txtResetEmail.Text);
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            string email = reader["PersonEmail"].ToString();
            reader.Close();
            string password = System.Web.Security.Membership.GeneratePassword(8, 6);
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

            command.CommandText = "UPDATE [dbo].[Person] SET [Password] = @password WHERE PersonEmail=@email";

            command.Parameters.AddWithValue("@password", passwordHashNew);
            command.ExecuteNonQuery();
            Send_Mail(email, "Your login password is here: " + password);
            Response.Write("<script>alert('Your temporary password has been send to your email')</script>");
        }
        else
        {
            Response.Write("<script>alert('Your entered e-mail is not regristered in our Database')</script>");
        }
        con.Close();
    }

    public void Send_Mail(String email, String Message)
    {
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Important Information(DO NOT REPLY)", Message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }


    //im not a robot
    public bool ValidateReCaptcha(ref string errorMessage)
    {
        var gresponse = Request["g-recaptcha-response"];
        string secret = "6Lc7q1IUAAAAAE7GqkDuvsJKDiMuMi_88B1rOvps";

        string ipAddress = GetIpAddress();

        var client = new WebClient();

        string url = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", secret, gresponse, ipAddress);


        var response = client.DownloadString(url);

        var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(response);

        if (captchaResponse.Success)
        {
            return true;
        }
        else
        {
            var error = captchaResponse.ErrorCodes[0].ToLower();
            switch (error)
            {
                case ("missing-input-secret"):
                    errorMessage = "The secret key parameter is missing.";
                    break;
                case ("invalid-input-secret"):
                    errorMessage = "The given secret key parameter is invalid.";
                    break;
                case ("missing-input-response"):
                    errorMessage = "The g-recaptcha-response parameter is missing.";
                    break;
                case ("invalid-input-response"):
                    errorMessage = "The given g-recaptcha-response parameter is invalid.";
                    break;
                default:
                    errorMessage = "reCAPTCHA Error. Please try again!";
                    break;
            }

            return false;
        }
    }

    string GetIpAddress()
    {
        var ipAddress = string.Empty;

        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        else if (!string.IsNullOrEmpty(Request.UserHostAddress))
        {
            ipAddress = Request.UserHostAddress;
        }

        return ipAddress;
    }

    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }




}

