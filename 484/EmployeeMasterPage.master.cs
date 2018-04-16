using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Configuration;


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
                ShowEmpImage();

                //Modify("AWVlkg6zW8bPcK88nxFc8FfRM1gXbcxxC-Z-R8stlrtdZv819Okk6TgD0AwFX44gOZcsmp9i5gI5rmLP", "EDtJ1b4EYsq1Xs8aM9cGl8uI5G9O-YdiU8Hyyrd0cbCDOf0qPHOQLftu7dJcAjugWSTmxN4uJz8eh-LA");


                //Dictionary<string, string> config = new Dictionary<string, string>();
                //config.Add("mode", "sandbox");
                //config.Add("clientId", "AWVlkg6zW8bPcK88nxFc8FfRM1gXbcxxC-Z-R8stlrtdZv819Okk6TgD0AwFX44gOZcsmp9i5gI5rmLP");
                //config.Add("clientSecret", "EDtJ1b4EYsq1Xs8aM9cGl8uI5G9O-YdiU8Hyyrd0cbCDOf0qPHOQLftu7dJcAjugWSTmxN4uJz8eh-LA");
                //config.Add("account1.apiUsername", "354013233_api1.qq.com");
                //config.Add("account1.apiPassword", "Q9PPD6TEDSRFLWBV");
                //config.Add("account1.apiSignature", "AIrjZNoKWfSz9UvFcW7U5u0Bm0sdAGYbepkmCq0ytPQ3ziZvwY9qsnyA");

                //PayPalAPIInterfaceService s = new PayPalAPIInterfaceService(config);
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
        string sql = "SELECT profilepicture FROM person WHERE PersonEmail = @PersonEmail";
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
        txtName.Text = Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
        sc.Close();

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

    //public void Modify(string Id, string Secret)

    //{

    //    Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");

    //    PayPal.SDKConfigHandler appSettingsSection = (PayPal.SDKConfigHandler)configuration.GetSection("paypal");

    //    if (appSettingsSection != null)
    //    {

    //        appSettingsSection.Settings["clientId"].Value = Id;
    //        appSettingsSection.Settings["clientSecret"].Value = Secret;

    //        configuration.Save();

    //    }
    //    else
    //    {
           
    //    }
       

    //}
}

