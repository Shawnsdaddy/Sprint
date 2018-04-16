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

public partial class SystemAdminHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
                lblPoints.Text = "Hi! " + Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
        
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
            //command.CommandText = "Insert into BusinessEntity ([BusinessEntityName],[PhoneNumber],[BusinessEntityEmail],[LastUpdated],[LastUpdatedBy],[LimitPeriod],[GiveLimitation],[AlertBalance],[Status]) " +
            //"Values(@businessname, @phonenumber, @email, @lastupdated, @lastupdatedby,@period,@give,@alert,1)";
            //command.Parameters.AddWithValue("@businessname", business.getBusinessName());
            //command.Parameters.AddWithValue("@phonenumber", business.getPhoneNumber());
            //command.Parameters.AddWithValue("@lastupdated", DateTime.Now);
            //command.Parameters.AddWithValue("@lastupdatedby", Session["loggedIn"].ToString());
            ////if ((txtReward.Text.Trim() == "") || (txtAlert.Text.Trim() == ""))
            ////{
            //command.Parameters.AddWithValue("@period", "1");
            //command.Parameters.AddWithValue("@give", "1");
            //command.Parameters.AddWithValue("@alert", "1000");
            ////}
            ////else
            ////{
            ////    command.Parameters.AddWithValue("@give", txtReward.Text.Trim());
            ////    command.Parameters.AddWithValue("@alert", txtAlert.Text.Trim());
            ////}
            //command.ExecuteNonQuery();
            Person employee = new Person(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtBusinessEmail.Text.Trim(), "CEO", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
            //command.CommandText = "Insert into Person([FirstName],[LastName],[MI],[PersonEmail],[JobTitle],[Privilege],[Password],[PointsBalance],[Status],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount]) " +
            //"Values (@first, @last,@MI, @email, @title, @position,@password, 0, 1, @LU, @LUB,(select Max(BusinessEntityID) from [BusinessEntity]), 0)";
            //command.Parameters.AddWithValue("@first", employee.getFirstName());
            //command.Parameters.AddWithValue("@last", employee.getLastName());
            //command.Parameters.AddWithValue("@title", employee.getEmployeeTitle());
            //command.Parameters.AddWithValue("@position", "Administrative");
            //command.Parameters.AddWithValue("@LU", DateTime.Now.ToShortDateString());
            //command.Parameters.AddWithValue("@LUB", Session["loggedIn"].ToString());


            string password = System.Web.Security.Membership.GeneratePassword(8, 6);
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
            command.Parameters.AddWithValue("@password", passwordHashNew);
            command.ExecuteNonQuery();
            Send_Mail(employee.getEmail(), employee.getEmail(), password, "1", "1000");
            Response.Write("<script>alert('BusinessEntity and CEO created successfully')</script>");
            txtBusinessEmail.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtMI.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPhoneNumber.Text = String.Empty;

            SqlCommand moneydefault = new SqlCommand("default_money",con);
            moneydefault.CommandType = CommandType.StoredProcedure;
            moneydefault.Parameters.AddWithValue("@EName", business.getBusinessName());
            moneydefault.Parameters.AddWithValue("@Phone", business.getPhoneNumber());
            moneydefault.Parameters.AddWithValue("@Email", business.getBusinessEmail());
            moneydefault.Parameters.AddWithValue("@lastUpdated", DateTime.Now.ToShortDateString());
            moneydefault.Parameters.AddWithValue("@lastupdatedby", Session["loggedIn"].ToString());
            moneydefault.Parameters.AddWithValue("@fristname", employee.getFirstName());
            moneydefault.Parameters.AddWithValue("@last", employee.getLastName());
            moneydefault.Parameters.AddWithValue("@password", passwordHashNew);
            if (txtMI.Text.Trim() == "")
            {
                moneydefault.Parameters.AddWithValue("@mi", DBNull.Value);
            }
            else
            {
                moneydefault.Parameters.AddWithValue("@mi", txtMI.Text.Trim());
            }
            moneydefault.ExecuteNonQuery();
            con.Close();
        }


        con.Close();
    }
    public void Send_Mail(String email, String Name, String Password, String Reward, String Alert)
    {
        String message = "Dear CEO " + txtName.Text + ": \n";
        message += "Your account has been created!!\n";
        message += "Please login with UserName and Password provides below:\n";
        message += "UserName:  " + Name + "\n PassWord: " + Password + "\n";
        message += "Reward Limitation(per person per calendar day):  " + Reward + "\n Alert Point Balance: " + Alert + "\n";
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