using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Net.Mail;
//using Tulpep.NotificationWindow;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        switch (Session["Privilege"].ToString())
        {
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
            case "SystemAdmin":
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Response.Redirect("SystemAdminHome.aspx");
                        break;
                    case "UserInfo":
                        Response.Redirect("SystemAdmin.aspx");
                        break;
                    case "Settings":
                        Response.Redirect("SytemAdminprofile.aspx");
                        break;
                    default:
                        Response.Redirect("SystemAdminHome.aspx");
                        break;

                }

                break;
            case "RewardProvider":
                switch (Session["DefaultPage"].ToString())
                {
                    case "Homepage":
                        Response.Redirect("RewardProvider.aspx");
                        break;
                    case "GiftCardInfo":
                        Response.Redirect("GiftCardInfo.aspx");
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
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                con.Open();
                SqlCommand alert = new SqlCommand();
                alert.Connection = con;


                alert.CommandText = "SELECT BusinessEntity.AlertBalance, Person.PointsBalance FROM BusinessEntity INNER JOIN Person ON" +
                    " BusinessEntity.BusinessEntityID = Person.BusinessEntityID where [dbo].[Person].PersonID= @id and Person.BusinessEntityID=@EntityID";
                alert.Parameters.AddWithValue("@id", Session["ID"]);
                alert.Parameters.AddWithValue("@EntityID", Session["BusinessEntityID"]);



                SqlDataReader dr = alert.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    int leftbalance = Convert.ToInt32(dr["PointsBalance"].ToString());
                    int alertamount = Convert.ToInt32(dr["AlertBalance"].ToString());
                    if (!Page.IsPostBack)
                    {
                        if (leftbalance <= alertamount)
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script language='javascript'>");
                            sb.Append(@"Sendalert();");
                            sb.Append(@"</script>");
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "JCall1", sb.ToString(), false);
                            Send_Mail(Session["E-Mail"].ToString(), "Dear " + Session["FirstName"] + ",<br />&nbsp;<br />&nbsp;The Money Pool is too low, Please Frontload on time!<br />&nbsp;<br />&nbsp;<br />" +
                               "Please go to this link: http://reward.us-east-1.elasticbeanstalk.com/CEOPostWall.aspx ");

                        }
                    }
                }
                txtName.Text = Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
                profilePicture();
                con.Close();
                break;
        }
        
    }

    public void Unnamed_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Logout.aspx");
    }

    public void profilePicture()
    {
       
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string sql = "SELECT [ProfilePicture] FROM person WHERE PersonEmail = @PersonEmail";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@PersonEmail", Session["E-mail"]);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                if (!Convert.IsDBNull(dr["profilepicture"]))
                {
                    Byte[] imagedata = (byte[])dr["profilepicture"];
                    string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
                    profileImage.ImageUrl = "data:image/png;base64," + img;

                }
                else
                {
                    profileImage.ImageUrl = "~/image/empty.png";
            
                }
            }


        }
        sc.Close();

    }
    public string MasterPageLabel
    {
        get { return txtName.Text; }
        set { txtName.Text = value; }
    }

    public string UpdatePicture
    {
        get { return profileImage.ImageUrl; }
        set { profileImage.ImageUrl = value; }
    }
    protected void profileImage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CEOprofile.aspx");
    }

    public void Send_Mail(String email, String Message)
    {
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Important Information(DO NOT REPLY)", Message);
        SmtpClient client = new SmtpClient();
        mail.IsBodyHtml = true;
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }

}