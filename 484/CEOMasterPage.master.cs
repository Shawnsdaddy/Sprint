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
            default:
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                //command.CommandText = "select mi,lastname from person where person.[PersonEmail] = @Email";
                //command.Parameters.AddWithValue("@Email", Session["E-mail"].ToString());
                //SqlDataReader reader = command.ExecuteReader();
                //if (reader.HasRows)
                //{
                //    reader.Read();
                //    Session["Middle"] = reader["MI"].ToString();
                //    Session["last"] = reader["LastName"].ToString();
                //}
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
}