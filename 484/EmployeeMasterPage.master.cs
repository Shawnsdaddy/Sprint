using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration; 

public partial class employeeMasterPage : System.Web.UI.MasterPage
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
            default:
                ShowEmpImage();
                break;
        }

            //System.Threading.Thread.Sleep(100);
            //string currenttime = DateTime.Now.ToLongTimeString();
            //lblcurrenttime.Text = currenttime;
        
    }
    public void Unnamed_Click(object sender, EventArgs e)
    {

        Session.Clear();
        Response.Redirect("Logout.aspx");
    }

    public void ShowEmpImage()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        //string sql = "SELECT profilepicture FROM person WHERE PersonEmail = @PersonEmail";
        string sql = "SELECT mi,lastname, profilepicture FROM person WHERE PersonEmail = @PersonEmail";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@PersonEmail", Session["E-mail"]);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Session["middle"] = dr["MI"].ToString();
                Session["Last"] = dr["LastName"].ToString();
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
        //txtName.Text = Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
        txtName.Text = Session["FirstName"].ToString() + " " + Session["middle"].ToString() + " " + Session["Last"].ToString();
        sc.Close();

        //SqlConnection sc = new SqlConnection();
        //sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        //sc.Open();
        //string sql = "SELECT profilepicture FROM person WHERE username = @username";
        //SqlCommand cmd = new SqlCommand(sql, sc);
        //cmd.Parameters.AddWithValue("@username", empno);
        //SqlDataReader dr = cmd.ExecuteReader();
        //if (dr.HasRows)
        //{
        //    while (dr.Read())
        //    {
        //        if (!Convert.IsDBNull(dr["profilepicture"]))
        //        {
        //            Byte[] imagedata = (byte[])dr["profilepicture"];
        //            string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
        //            ProfilePicture.ImageUrl = "data:image/png;base64," + img;
        //        }
        //        else
        //        {
        //            ProfilePicture.ImageUrl = "~/image/empty.png";
        //        }
        //    }
        //}
        //sc.Close();
    }

    public string Updatelable
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
        Response.Redirect("employeeProfile.aspx");
    }
}

