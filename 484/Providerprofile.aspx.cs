using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Data;

public partial class Providerprofile : System.Web.UI.Page
{
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]

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
                string name = Session["JobTitle"].ToString();
                lblFullName.Text = name;
                lblName.Text = name + "'s Profile";
                if (!Page.IsPostBack)
                {
                    ShowEmpImage(Session["E-mail"].ToString());
                    SqlConnection sc = new SqlConnection();
                    sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                    sc.Open();
                    SqlCommand insert = new SqlCommand();

                    insert.Connection = sc;
                    insert.CommandText = "select FirstName, LastName, MI, JobTitle from person where PersonEmail = @PersonEmail";
                    insert.Parameters.AddWithValue("@PersonEmail", Session["E-mail"].ToString());
                    SqlDataReader reader = insert.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        lblContact.Text = Session["E-mail"].ToString();

                    }

                    showdata();
                    sc.Close();
                }
                break;
        }

        
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        //string username = Session["loggedIn"].ToString();
        String oldpass = Session["Password"].ToString();
        if (oldpass == txtOldPass.Text.Trim())
        {
            string password = txtNew1.Text;
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "UPDATE [dbo].[Person] SET [Password] = @Password  where PersonID = @ID";
            insert.Parameters.AddWithValue("@Password", passwordHashNew);
            insert.Parameters.AddWithValue("@ID", Session["ID"].ToString());
            insert.ExecuteNonQuery();
            Response.Write("<script>alert('Password changed successfully')</script>");
            Send_Mail(Session["E-Mail"].ToString(), "Your login password is here: " + password);
            sc.Close();
        }
        else
        {
            Response.Write("<script>alert('OldPassword incorrect')</script>");
            popPic.Show();
        }
    }

    protected void Upload_Click(object sender, EventArgs e)
    {
        try
        {
            HttpPostedFile postedFile = PictureUpload.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string extension = Path.GetExtension(filename);
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                SqlConnection sc = new SqlConnection();

                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sc;
                cmd.CommandText = "UPDATE [dbo].[Person] SET [ProfilePicture] = @ProfilePicture WHERE PersonID=@id";

                cmd.Parameters.AddWithValue("@ProfilePicture", bytes);
                cmd.Parameters.AddWithValue("@id", Session["ID"]);
                cmd.ExecuteNonQuery();
                sc.Close();
                ShowEmpImage(Session["E-Mail"].ToString());
                Response.Write("<script>alert('File Updated Successful')</script>");

            }
            else
            {
                Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");
                popPic.Show();
            }
        }
        catch
        {
            Response.Write("<script>alert('Picture Size is exceeding the database can take')</script>");
        }
    }


    public void ShowEmpImage(string empno)
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
                    ProfilePicture.ImageUrl = "data:image/png;base64," + img;
                    picture.ImageUrl = "data:image/png;base64," + img;
                    Master.UpdatePicture = "data:image/png;base64," + img;
                }
                else
                {
                    ProfilePicture.ImageUrl = "~/image/empty.png";
                    picture.ImageUrl = "~/image/empty.png";
                    Master.UpdatePicture = "~/image/empty.png";
                }
            }


        }
        sc.Close();
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

    protected void gdvShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvShow.EditIndex = -1;
        showdata();
    }

    protected void gdvShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        gdvShow.EditIndex = e.NewEditIndex;
        showdata();
        sc.Close();
    }

    protected void gdvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;

        int userid = Convert.ToInt32(gdvShow.DataKeys[e.RowIndex].Value.ToString());
        GridViewRow row = (GridViewRow)gdvShow.Rows[e.RowIndex];
        string AmountProvided = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtAmount")).Text;
        gdvShow.EditIndex = -1;
        sc.Open();

        SqlCommand cmd = new SqlCommand("UPDATE ProviderAmount SET Amount=@amount, LastUpdate=@lastupdate, LastUpdateBy=@lastby WHERE ProviderID=@ID", sc);
        cmd.Parameters.AddWithValue("@ID", userid);
        cmd.Parameters.AddWithValue("@amount",Convert.ToDecimal(AmountProvided));
        cmd.Parameters.AddWithValue("@lastupdate", DateTime.Now);
        cmd.Parameters.AddWithValue("@lastby", Session["JobTitle"].ToString());
        cmd.ExecuteNonQuery();
      
        showdata();

        sc.Close();
    }
    protected void showdata()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand show = new SqlCommand("SELECT ProviderID, ProviderAmount.TypeOfBusiness, ProviderAmount.Amount FROM ProviderAmount " +
                "INNER JOIN Person ON ProviderAmount.ProviderID = Person.PersonID WHERE (Person.JobTitle = @title) and (Person.BusinessEntityID=@ID)", sc);
        show.Parameters.AddWithValue("@title", Session["JobTitle"].ToString());
        show.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        SqlDataAdapter adapter = new SqlDataAdapter(show);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        string sql = " update [dbo].[Person] set [DefaultPage] = @page where PersonID=@personID";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@personID", Session["ID"]);
        cmd.Parameters.AddWithValue("@page", dropPages.SelectedValue.ToString());
        cmd.ExecuteScalar();
        sc.Close();
    }

    protected void gdvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvShow.PageIndex = e.NewPageIndex;
        showdata();
    }
}