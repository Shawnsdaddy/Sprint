using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
public partial class firsttime : System.Web.UI.Page
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
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        String userName = txtUsername.Text;
        insert.Connection = sc;
        insert.CommandText = "select user from person where person.nickname =@newUserName and PersonID!=@id";
        insert.Parameters.AddWithValue("@newUserName", userName);
        insert.Parameters.AddWithValue("@id", Convert.ToInt32(Session["ID"]));
        SqlDataReader reader = insert.ExecuteReader();  
            if (!reader.HasRows)
            {
                string passwordHashNew = SimpleHash.ComputeHash(txtNew1.Text, "MD5", null);
                //SqlConnection sc = new SqlConnection();
                //sc.ConnectionString = "Data Source=groupproject.clltaluyh8dp.us-east-1.rds.amazonaws.com;Initial Catalog=RewardSystemLab4;Persist Security Info=True;User ID=javauser;Password=javapass";
                //sc.Open();
                //SqlCommand insert = new SqlCommand();
                //insert.Connection = sc;
                //insert.CommandText = "UPDATE [dbo].[Person] SET [Password] = @Password WHERE username= @username";

                //insert.Parameters.AddWithValue("@username", username);
                //insert.ExecuteNonQuery();
                //sc.Close();
                reader.Close();
                insert.CommandText = "update person set nickname = @newUserName,[Password] = @Password  where PersonID = @id";      
                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.ExecuteNonQuery();

                SqlCommand login = new SqlCommand("login_count", sc);
                login.CommandType = CommandType.StoredProcedure;
            login.Parameters.AddWithValue("@LoginEmail", Session["E-mail"].ToString());
            login.ExecuteNonQuery();

            sc.Close();
            //bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
            if(Session["Privilege"].ToString()=="Employee")
            {
                Response.Redirect("EmployeeReward.aspx");
            }
            else if (Session["Privilege"].ToString() == "Administrative")
            {
                Response.Redirect("CEOPostWall.aspx");
            }
            else if (Session["Privilege"].ToString() == "RewardProvider")
            {
                Response.Redirect("RewardProvider.aspx");
            }
        }
            else
            {
            Response.Write("<script>alert('Sorry, NickName has been taken')</script>");
            sc.Close();
        }
            
        
    }
}