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
                string name = Session["ProviderName"].ToString();
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
                    insert.CommandText = "select ProviderEmail from RewardProvider where ProviderEmail = @PersonEmail";
                    insert.Parameters.AddWithValue("@PersonEmail", Session["E-mail"].ToString());
                    SqlDataReader reader = insert.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        lblContact.Text = Session["E-mail"].ToString();

                    }

                    //showdata();
                    sc.Close();
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
            insert.CommandText = "UPDATE [dbo].[RewardProvider] SET [Password] = @Password, LastUpdated=@lasted, LastUpdatedBy=@updatedby where ProviderID = @ID";
            insert.Parameters.AddWithValue("@Password", passwordHashNew);
            insert.Parameters.AddWithValue("@lasted", DateTime.Today.ToShortDateString());
            insert.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
            insert.Parameters.AddWithValue("@ID", Session["ID"].ToString());
            insert.ExecuteNonQuery();
            Response.Write("<script>alert('Password changed successfully')</script>");
            Send_Mail(Session["E-Mail"].ToString(), "Your login password is here: " + password);
            sc.Close();
        }
        else
        {
            Response.Write("<script>alert('OldPassword incorrect')</script>");
            popPass.Show();
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
                cmd.CommandText = "UPDATE [dbo].[RewardProvider] SET [ProfilePicture] = @ProfilePicture,LastUpdated=@lasted, LastUpdatedBy=@updatedby WHERE ProviderID=@id";

                cmd.Parameters.AddWithValue("@ProfilePicture", bytes);
                cmd.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
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
        string sql = "SELECT [ProfilePicture] FROM [RewardProvider] WHERE ProviderEmail = @PersonEmail";
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
    protected void btnSet_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        string sql = " update [dbo].[RewardProvider] set [DefaultPage] = @page,[LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy where ProviderID=@personID";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@personID", Session["ID"]);
        cmd.Parameters.AddWithValue("@page", dropPages.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
        cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToShortDateString());
        cmd.ExecuteScalar();
        sc.Close();
    }
}