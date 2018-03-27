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

public partial class RewardProviderMasterPage : System.Web.UI.MasterPage
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
            default:
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                txtName.Text = Session["JobTitle"].ToString();
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
        Response.Redirect("Providerprofile.aspx");
    }
}
    