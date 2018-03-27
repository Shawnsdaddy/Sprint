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


public partial class CEO_AddProvider : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    GridBind();

                }
                break;
        }      
    }
    protected void btnAddProvider_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.ConnectionString = connStr;
        sc.Open();
        RewardsProvider providerObject = new RewardsProvider(txtProviderName.Text, txtProviderEmail.Text, TypeOfBusiness.SelectedItem.Text, DateTime.Now, Session["loggedIn"].ToString(), Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        SqlCommand insertProvider = new SqlCommand();
        insertProvider.Connection = sc;
        insertProvider.CommandText = "select [PersonEmail] from Person where jobtitle =@ProviderName and status=1 and Privilege='RewardProvider' and BusinessEntityID = @BusinessEntityID";
        insertProvider.Parameters.AddWithValue("@ProviderName", providerObject.getcompanyName());
        //insertProvider.Parameters.AddWithValue("@Amount", providerObject.getamountProvided());
        insertProvider.Parameters.AddWithValue("@TypeOfBusiness", providerObject.gettypeOfBusiness());
        insertProvider.Parameters.AddWithValue("@BusinessEntityID", providerObject.getBusinessEntityID());
        SqlDataReader reader = insertProvider.ExecuteReader();

        if (reader.HasRows)
        {
            Response.Write("<script>alert('Provider with the same gift card amount already exists!')</script>");
            reader.Close();
        }
        else
        {
            reader.Close();

            string password = System.Web.Security.Membership.GeneratePassword(8, 6);
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

            try
            {
                HttpPostedFile postedFile = Picture.PostedFile;
                string filename = Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(filename);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                    insertProvider.Parameters.AddWithValue("@Password", passwordHashNew);
                    //create sql query
                    insertProvider.CommandText = "INSERT INTO [dbo].[Person] (JobTitle, PersonEmail, LastUpdated, LastUpdatedBy, Status,[Privilege],[Password],[PointsBalance],[BusinessEntityID], [loginCount],[ProfilePicture]) VALUES (@ProviderName, @ProviderEmail, @LastUpdated, @LastUpdateBy,@status,'RewardProvider',@Password,0,@BusinessEntityID,0,@ProfilePicture)";
                    insertProvider.Parameters.AddWithValue("@ProviderEmail", providerObject.getemail());
                    insertProvider.Parameters.AddWithValue("@LastUpdateBy", providerObject.getLastUpdatedBy());
                    insertProvider.Parameters.AddWithValue("@LastUpdated", providerObject.getLastUpdated());
                    insertProvider.Parameters.AddWithValue("@status", '1');
                    insertProvider.Parameters.AddWithValue("@ProfilePicture", bytes);
                    insertProvider.ExecuteNonQuery();
                    insertProvider.CommandText = "INSERT INTO [dbo].[ProviderAmount] ([ProviderID],[TypeOfBusiness],[LastUpdate],[LastUpdateBy],[Amount]) VALUES ((select max(personID) from Person),@TypeOfBusiness,@LastUpdated,@LastUpdateBy,20)";
                    insertProvider.ExecuteNonQuery();    
                    Send_Mail(providerObject.getemail(), providerObject.getemail(), password);

                    sc.Close();
                    txtProviderName.Text = string.Empty;
                    txtProviderEmail.Text = string.Empty;
                    TypeOfBusiness.SelectedIndex = -1;
                    Picture.Attributes.Clear();
                    GridBind();
                    Response.Write("<script>alert('New provider added successfully')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");
                }
        }
            catch
        {
            Response.Write("<script>alert('Picture Size is exceeding the databases capability')</script>");
        }

    }
    }

    public void Send_Mail(String email, String Name, String Password)
    {
        String message = "Dear Reward Provider: \n";
        message += "Your account has been created!!\n";
        message += "Please login with UserName and Password provides below:\n";
        message += "UserName:  " + Name + "\n PassWord: " + Password + "\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Account Has been Created(DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
    protected void btnUpdateProvider_Click(object sender, EventArgs e)
    {
        if (gdvShow.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select an employee')</script>");
        }
        else
        {
            int selectID = Convert.ToInt32(gdvShow.SelectedRow.Cells[0].Text);

            RewardsProvider providerObject = new RewardsProvider(txtprovider.Text, txtPEmail.Text, Bussiness.SelectedItem.Text, DateTime.Now, Session["loggedIn"].ToString(), Convert.ToInt32(Session["BusinessEntityID"].ToString()));
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [PersonEmail] from [Person] where [PersonID] = @ProviderID";
            insert.Parameters.AddWithValue("@ProviderID", selectID);
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.Read())
            {
                Session["ProviderEmail"] = reader["PersonEmail"].ToString();
            }

            reader.Close();
            insert.CommandText = "UPDATE [dbo].[Person] SET JobTitle = @JobTitle, PersonEmail = @Email, LastUpdated = @LastUpdated, LastUpdatedBy=@LastUpdatedBy where PersonID=@ProviderID";        
            insert.Parameters.AddWithValue("@JobTitle", providerObject.getcompanyName());
            insert.Parameters.AddWithValue("@LastUpdatedBy", providerObject.getLastUpdatedBy());
            insert.Parameters.AddWithValue("@LastUpdated", providerObject.getLastUpdated());
            insert.Parameters.AddWithValue("@Email", providerObject.getemail());
            insert.ExecuteNonQuery();

            insert.CommandText = "UPDATE [dbo].[ProviderAmount] SET [TypeOfBusiness]=@TypeOfBusiness, [LastUpdate]=@LastUpdated, [LastUpdateBy]=@LastUpdatedBy WHERE [ProviderID] = @ProviderID";
            insert.Parameters.AddWithValue("@TypeOfBusiness", providerObject.gettypeOfBusiness());
            insert.ExecuteNonQuery();
            if (txtPEmail.Text.Trim() == Session["ProviderEmail"].ToString())
            {
                HttpPostedFile postedFile = Photo.PostedFile;
                if (postedFile != null)
                {
                    try
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string extension = Path.GetExtension(filename);
                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
                        {
                            Stream stream = postedFile.InputStream;
                            BinaryReader binaryReader = new BinaryReader(stream);
                            byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = sc;
                            cmd.CommandText = "UPDATE [dbo].[Person] SET [ProfilePicture] = @ProfilePicture WHERE PersonID=@ProviderID";
                            cmd.Parameters.AddWithValue("@ProfilePicture", bytes);
                            cmd.Parameters.AddWithValue("@ProviderID", selectID);
                            cmd.ExecuteNonQuery();
                            sc.Close();
                        }
                        Response.Write("<script>alert('Information has been updated')</script>");
                }
                    catch
                {
                    Response.Write("<script>alert('Picture Size is exceeding the database can take')</script>");
                }
            }
            }
            else
            {
                string password = System.Web.Security.Membership.GeneratePassword(9, 1);
                string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
                insert.CommandText = "UPDATE [dbo].[Person] SET [Password]=@Password where PersonID=@EmployeeID";
                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.Parameters.AddWithValue("@EmployeeID", selectID);
                insert.ExecuteNonQuery();
                Send_Mail(providerObject.getemail(), providerObject.getemail(), password);
                Response.Write("<script>alert('An E-mail has been send to new Address')</script>");
            }
            sc.Close();
            }
            GridBind();
        }


    


    public void ShowEmpImage(string id)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string sql = "SELECT [ProfilePicture] FROM Person WHERE PersonID = @ID";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@ID", id);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                if (!Convert.IsDBNull(dr["ProfilePicture"]))
                {
                    Byte[] imagedata = (byte[])dr["ProfilePicture"];
                    string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
                    ProfilePicture.ImageUrl = "data:image/png;base64," + img;
                }
                else
                {
                    ProfilePicture.ImageUrl = "~/image/empty.png";
                }
            }


        }
        sc.Close();
    }
    public void GridBind()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand fill =new SqlCommand();
        fill.Connection = sc;
        fill.CommandText = "SELECT ProviderAmount.ProviderID, Person.JobTitle AS Name,Person.PersonEmail AS Email, ProviderAmount.TypeOfBusiness AS Type FROM Person " +
            "INNER JOIN ProviderAmount ON Person.PersonID = ProviderAmount.ProviderID WHERE (Person.Status = 1) AND (Person.BusinessEntityID = @businessentity)";
        fill.Parameters.AddWithValue("@businessentity", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        SqlDataAdapter adapter = new SqlDataAdapter(fill);

        DataSet ds = new DataSet();
        adapter.Fill(ds);

        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand fill = new SqlCommand();
        fill.Connection = sc;

        string companyname = txtCompany.Text;
        string companyemail = txtemail.Text;

        fill.CommandText = "SELECT ProviderAmount.ProviderID, Person.JobTitle AS Name,Person.PersonEmail AS Email, ProviderAmount.TypeOfBusiness AS Type, ProviderAmount.Amount FROM Person " +
            "INNER JOIN ProviderAmount ON Person.PersonID = ProviderAmount.ProviderID WHERE (Person.Status = 1)  and (Person.BusinessEntityID = @businessentity) and ((Person.JobTitle like '%'+@name+'%') and (Person.PersonEmail like+'%'+@email+'%'))";
        fill.Parameters.AddWithValue("@name", companyname);
        fill.Parameters.AddWithValue("@email", companyemail);
        fill.Parameters.AddWithValue("@businessentity", Convert.ToInt32(Session["BusinessEntityID"].ToString()));

        SqlDataAdapter adapter = new SqlDataAdapter(fill);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (gdvShow.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select an employee')</script>");
        }
        else
        {
            int ID = Convert.ToInt32(gdvShow.SelectedRow.Cells[0].Text);

            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "UPDATE [dbo].[Person] SET [status]=0 where PersonID=@ID";
            delete.Parameters.AddWithValue("@ID", ID);
            SqlDataAdapter adapter = new SqlDataAdapter(delete);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            gdvShow.DataSource = ds;
            //gdvShow.DataBind();
            sc.Close();
            GridBind();
        }
    }

    protected void gdvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvShow.PageIndex = e.NewPageIndex;
        GridBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }

    protected void gdvShow_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if(gdvShow.SelectedIndex == -1)
        {
            txtprovider.Text = string.Empty;
            txtPEmail.Text = string.Empty;
            Bussiness.SelectedIndex = 0;
            Photo.Attributes.Clear();
            ProfilePicture.Attributes.Clear();
        }
        else
        {
         String ID = gdvShow.SelectedRow.Cells[0].Text;

        txtprovider.Text = gdvShow.SelectedRow.Cells[1].Text;


        txtPEmail.Text = gdvShow.SelectedRow.Cells[2].Text;

        if (gdvShow.SelectedRow.Cells[3].Text.Equals("Restaurant"))
        {
            Bussiness.SelectedIndex = 1;
        }
        else if (gdvShow.SelectedRow.Cells[3].Text.Equals("Lodging"))
        {
            Bussiness.SelectedIndex = 2;
        }

        if (gdvShow.SelectedRow.Cells[3].Text.Equals("Clothing"))
        {
            Bussiness.SelectedIndex = 3;
        }
        else if (gdvShow.SelectedRow.Cells[3].Text.Equals("Shopping"))
        {
            Bussiness.SelectedIndex = 4;
        }
        else if (gdvShow.SelectedRow.Cells[3].Text.Equals("Other"))
        {
            Bussiness.SelectedIndex = 5;
        }

        ShowEmpImage(ID);
        }

    }
}


