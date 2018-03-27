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

public partial class SystemAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");

        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        switch (Session["Privilege"].ToString())
        {
            case "Administrative":
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Response.Redirect("CEOPostWall.aspx");
                        break;
                    case "ProviderInfor":
                        Response.Redirect("CEO_AddProvider.aspx");
                        break;
                    case "EmployeeInfor":
                        Response.Redirect("CreateEmployee.aspx");
                        break;
                    case "ViewReport":
                        Response.Redirect("Report.aspx");
                        break;
                    case "Setting":
                        Response.Redirect("CEOprofile.aspx");
                        break;
                    default:
                        Response.Redirect("CEOPostWall.aspx");
                        break;

                }
                break;
            case "SystemAdmin":
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Response.Redirect("SystemAdmin.aspx");
                        break;
                    case "Setting":
                        Response.Redirect("SytemAdminprofile.aspx");
                        break;
                    default:
                        Response.Redirect("SystemAdmin.aspx");
                        break;

                }
                break;
            case "Employee":
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Response.Redirect("EmployeeReward.aspx");
                        break;
                    case "GetReward":
                        Response.Redirect("CashOut.aspx");
                        break;
                    case "DashBoard":
                        Response.Redirect("UserDashboard.aspx");
                        break;
                    case "Setting":
                        Response.Redirect("EmployeeProfile.aspx");
                        break;
                    default:
                        Response.Redirect("EmployeeReward.aspx");
                        break;
                }
                break;
            case "RewardProvider":
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Response.Redirect("RewardProvider.aspx");
                        break;
                    case "Setting":
                        Response.Redirect("Providerprofile.aspx");
                        break;
                    default:
                        Response.Redirect("RewardProvider.aspx");
                        break;
                }
                break;
            default:
                lblPoints.Text = "Welcome, " + Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString() + "!";

                break;
        }
    }
                

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        con.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        BusinessEntity business = new BusinessEntity(txtName.Text.Trim(), txtPhoneNumber.Text.Trim(), txtBusinessEmail.Text.Trim());
        command.CommandText = "select [BusinessEntity].BusinessEntityEmail, Person.PersonEmail from BusinessEntity, person where [BusinessEntityEmail] =@email or Person.personemail=@email";
        command.Parameters.AddWithValue("@email", txtBusinessEmail.Text.Trim());
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            Response.Write("<script>alert('BusinessEmail and CEOEmail have already existed in Database')</script>");
        }
        else
        {
            reader.Close();
            command.CommandText = "Insert into BusinessEntity ([BusinessEntityName],[PhoneNumber],[BusinessEntityEmail],[LastUpdated],[LastUpdatedBy]) " +
            "Values(@businessname, @phonenumber, @email, @lastupdated, @lastupdatedby)";
            command.Parameters.AddWithValue("@businessname", business.getBusinessName());
            command.Parameters.AddWithValue("@phonenumber", business.getPhoneNumber());
            command.Parameters.AddWithValue("@lastupdated", DateTime.Now);
            command.Parameters.AddWithValue("@lastupdatedby", Session["loggedIn"].ToString());
            command.ExecuteNonQuery();
            Person employee = new Person(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtBusinessEmail.Text.Trim(), "CEO", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
            command.CommandText = "Insert into Person([FirstName],[LastName],[MI],[PersonEmail],[JobTitle],[Privilege],[Password],[PointsBalance],[Status],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount]) " +
            "Values (@first, @last,@MI, @email, @title, @position,@password, 0, 1, @LU, @LUB,(select Max(BusinessEntityID) from [BusinessEntity]), 0)";
            command.Parameters.AddWithValue("@first", employee.getFirstName());
            command.Parameters.AddWithValue("@last", employee.getLastName());
            command.Parameters.AddWithValue("@title", employee.getEmployeeTitle());
            command.Parameters.AddWithValue("@position", "Administrative");
            command.Parameters.AddWithValue("@LU", DateTime.Now);
            command.Parameters.AddWithValue("@LUB", Session["loggedIn"].ToString());

            if (txtMI.Text.Trim() == "")
            {
                command.Parameters.AddWithValue("@MI", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@MI", txtMI.Text.Trim());
            }
            string password = System.Web.Security.Membership.GeneratePassword(8, 6);
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
            command.Parameters.AddWithValue("@password", passwordHashNew);
            command.ExecuteNonQuery();
            Send_Mail(employee.getEmail(), employee.getEmail(), password);
            Response.Write("<script>alert('BusinessEntity and CEO created successfully')</script>");
            txtBusinessEmail.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtMI.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPhoneNumber.Text = String.Empty;
        }


        con.Close();
    }
    public void Send_Mail(String email, String Name, String Password)
    {
        String message = "Dear CEO" + txtName.Text + ": \n";
        message += "Your account has been created!!\n";
        message += "Please login with UserName and Password provides below:\n";
        message += "UserName:  " + Name + "\n PassWord: " + Password + "\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Account Has been Created(DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
}