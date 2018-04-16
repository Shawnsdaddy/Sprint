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
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        String userName = txtUsername.Text;
        insert.Connection = sc;
        insert.CommandText = "select NickName from person where person.nickname =@newUserName and PersonID!=@id and BusinessEntityID = @EntityID";
        insert.Parameters.AddWithValue("@newUserName", userName);
        insert.Parameters.AddWithValue("@id", Convert.ToInt32(Session["ID"]));
        insert.Parameters.AddWithValue("@EntityID", Convert.ToInt32(Session["BusinessEntityID"]));
        SqlDataReader reader = insert.ExecuteReader();  
            if (!reader.HasRows)
            {
                string passwordHashNew = SimpleHash.ComputeHash(txtNew1.Text, "MD5", null);
                reader.Close();
                insert.CommandText = "update person set nickname = @newUserName,[Password] = @Password, LastUpdated=@last, LastUpdatedBy=@By where PersonID = @id and BusinessEntityID = @EntityID";      
                insert.Parameters.AddWithValue("@Password", passwordHashNew);
            insert.Parameters.AddWithValue("@last", DateTime.Now.ToShortDateString());
            insert.Parameters.AddWithValue("@By", Session["loggedIn"]);
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
            else if (Session["Privilege"].ToString() == "SystemAdmin")
            {
                Response.Redirect("SystemAdminHome.aspx");
            }
        }
            else
            {
            Response.Write("<script>alert('Sorry, NickName has been taken')</script>");
            sc.Close();
        }
            
        
    }
}