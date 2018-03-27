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

public partial class employeeProfile : System.Web.UI.Page
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
                string name = Session["FirstName"].ToString() + " " + Session["middle"].ToString() + " " + Session["Last"].ToString();
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
                    insert.CommandText = "select FirstName, LastName, MI, PersonEmail,Privilege,PointsBalance,NickName from person where PersonEmail = @PersonEmail";
                    insert.Parameters.AddWithValue("@PersonEmail", Session["E-mail"].ToString());

                    SqlDataReader reader = insert.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtMI.Text = reader["MI"].ToString();
                        lblContact.Text = Session["E-mail"].ToString();
                        lblPosition.Text = reader["Privilege"].ToString();
                        lblPoints.Text = reader["PointsBalance"].ToString();
                        lblNick.Text = reader["NickName"].ToString();
                        txtNickName.Text = reader["NickName"].ToString();
                        Session["NickName"] = reader["NickName"].ToString();
                        //bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);
                    }
                    sc.Close();
                }
                break;
        }

        
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {


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

    protected void btnChangeProfile_Click(object sender, EventArgs e)

    {
        //try
        //{
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            //insert.CommandText = "select [PersonEmail] from [Person] where [PersonEmail] = @Email";
            //insert.Parameters.AddWithValue("@Email", txtEmail.Text);
          
            //SqlDataReader reader = insert.ExecuteReader();

            //if (reader.HasRows)
            //{
            //    Response.Write("<script>alert('Email record has already existed in Database')</script>");
            //    reader.Close();
            //    sc.Close();
            //    popProfile.Show();
            //}
            //else
            //{
            //    reader.Close();
                insert.CommandText = "UPDATE [dbo].[Person] SET [FirstName] = @FirstName,[LastName] = @LastName,[MI] = @MI, [LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy WHERE PersonID=" +
                    "@ID";
                insert.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                insert.Parameters.AddWithValue("@LastName", txtLastName.Text);
                insert.Parameters.AddWithValue("@nickname", txtNickName.Text);
                insert.Parameters.AddWithValue("@ID", Session["ID"]);
                insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
                insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToShortDateString());

                if (txtMI.Text.Trim() == "")
                {
                    insert.Parameters.AddWithValue("@MI", DBNull.Value);
                    Session["middle"] = "";
                }
                else
                {
                    insert.Parameters.AddWithValue("@MI", txtMI.Text.Trim());
                    Session["middle"] = txtMI.Text.Trim();
                }

                //if (txtManagerID.Text.Trim() == "")
                //{
                //    insert.Parameters.AddWithValue("@ManagerID", DBNull.Value);
                //}
                //else
                //{
                //    insert.Parameters.AddWithValue("@ManagerID", txtManagerID.Text.Trim());
                //}
                Session["FirstName"] = txtFirstName.Text.Trim();
                Session["Last"] = txtLastName.Text.Trim();
                insert.ExecuteNonQuery();
                string name = Session["FirstName"].ToString() + " " + Session["middle"].ToString() + " " + Session["Last"].ToString();
                Master.Updatelable = name;
                lblFullName.Text = name;
                lblName.Text = name + "'s Profile";
            Session["NickName"]= txtNickName.Text.Trim();
            lblNick.Text = Session["NickName"].ToString();
                insert.ExecuteNonQuery();
            Response.Write("<script>alert(' Employee Information has been updated!')</script>");
            sc.Close();               
            }
        //}
        //catch
        //{
            
        //    Response.Write("<script>alert('ManagerID not found in the database')</script>");
        //    popProfile.Show();
        //}
        
        
    

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
            cmd.Parameters.AddWithValue("@id", Session["ID"].ToString());
            cmd.ExecuteNonQuery();
            sc.Close();
            ShowEmpImage(Session["E-Mail"].ToString());
                Response.Write("<script>alert('Your profile picture has been updated')</script>");
            }
        else
        {
            Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");
            popPic.Show();
        }
        }
        catch
        {
            Response.Write("<script>alert('Picture Size is exceeding the databases capability')</script>");
        }
    }


    public void ShowEmpImage(string empno)
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

        string sql = " update [dbo].[Person] set [DefaultPage] = @page where PersonID=@personID";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@personID", Session["ID"]);
        cmd.Parameters.AddWithValue("@page", dropPages.SelectedValue.ToString());
        cmd.ExecuteScalar();
        sc.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        string yesPrivacy = "Anonymous";
        
        
        if (GivePrivacy.Checked)
        {
            string giving = " update [dbo].[Person] set [GivePrivacy] = @Check where PersonID=@personID";
            SqlCommand cmd = new SqlCommand(giving, sc);
            cmd.Parameters.AddWithValue("@personID", Session["ID"]);
            cmd.Parameters.AddWithValue("@Check", yesPrivacy);
            cmd.ExecuteNonQuery();
        }
        if (ReceivePrivacy.Checked)
        {
            string receving = " update [dbo].[Person] set [ReceivePrivacy] = @Check where PersonID=@personID";
            SqlCommand cmd = new SqlCommand(receving, sc);
            cmd.Parameters.AddWithValue("@personID", Session["ID"]);
            cmd.Parameters.AddWithValue("@Check", yesPrivacy);
            cmd.ExecuteNonQuery();
        }
        if(!ReceivePrivacy.Checked)
        {
            string updatereceving = " update [dbo].[Person] set [ReceivePrivacy] = @Check where PersonID=@personID";
            SqlCommand cmd = new SqlCommand(updatereceving, sc);
            cmd.Parameters.AddWithValue("@personID", Session["ID"]);
            cmd.Parameters.AddWithValue("@Check", DBNull.Value);
            cmd.ExecuteNonQuery();
        }
        if(!GivePrivacy.Checked)
        {
            string updategiving = " update [dbo].[Person] set [GivePrivacy] = @Check where PersonID=@personID";
            SqlCommand cmd = new SqlCommand(updategiving, sc);
            cmd.Parameters.AddWithValue("@personID", Session["ID"]);
            cmd.Parameters.AddWithValue("@Check", DBNull.Value);
            cmd.ExecuteNonQuery();
        }


        sc.Close();
    }
}