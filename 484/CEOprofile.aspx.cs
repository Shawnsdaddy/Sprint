using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Data;


public partial class CEOprofile : System.Web.UI.Page
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
                string name = Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
                lblFullName.Text = name;
                lblName.Text = name + "'s Profile";
                if (!IsPostBack)
                {
                    
                        txtFirstName.Text = Session["FirstName"].ToString();
                        txtLastName.Text = Session["last"].ToString();
                        txtMI.Text = Session["Middle"].ToString();
                        txtEmail.Text = Session["E-mail"].ToString();
                        txtNickName.Text = Session["NickName"].ToString();
                        lblContact.Text = Session["E-mail"].ToString();
                        lblPosition.Text = Session["JobTitle"].ToString();
                        lblNick.Text = Session["NickName"].ToString();
    
                    displayCategories();
                    displayValues();
                    dataFill();
                ShowEmpImage(Session["ID"].ToString());
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
            insert.CommandText = "UPDATE [dbo].[Person] SET [Password] = @Password, [LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy where PersonID = @ID and BusinessEntityID= @BusinessEntityID";
            insert.Parameters.AddWithValue("@Password", passwordHashNew);
            insert.Parameters.AddWithValue("@ID", Session["ID"].ToString());
            insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToShortDateString());
            insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
            insert.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
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
        string username = Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select [PersonEmail] from [Person] where [PersonEmail] = @Email and PersonID !=@ID and BusinessEntityID= @BusinessEntityID";
        insert.Parameters.AddWithValue("@Email", txtEmail.Text);
        insert.Parameters.AddWithValue("@ID", Session["ID"].ToString());
        insert.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
        SqlDataReader reader = insert.ExecuteReader();

        if (reader.HasRows)
        {
            Response.Write("<script>alert('Email record has already existed in Database')</script>");
            popProfile.Show();
            reader.Close();
            sc.Close();
        }
        else
        {
            reader.Close();
            insert.CommandText = "UPDATE [dbo].[Person] SET [FirstName] = @FirstName,[LastName] = @LastName,[MI] = @MI,[NickName]=@nickname,[PersonEmail] = @Email, [LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy WHERE PersonID=" +
            "@ID and BusinessEntityID= @BusinessEntityID";
            insert.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            insert.Parameters.AddWithValue("@LastName", txtLastName.Text);
            insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
            insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToShortDateString());
            //insert.Parameters.AddWithValue("@id", Session["ID"].ToString());
            //insert.Parameters.AddWithValue("@Email", txtEmail.Text);

            if (txtMI.Text.Trim() == "")
            {
                insert.Parameters.AddWithValue("@MI", DBNull.Value);
                Session["Middle"] = "";
            }
            else
            {
                insert.Parameters.AddWithValue("@MI", txtMI.Text.Trim());
                Session["Middle"] = txtMI.Text.Trim();
            }

            if (txtNickName.Text.Trim() == "")
            {
                insert.Parameters.AddWithValue("@nickname", DBNull.Value);
                Session["NickName"] = "";
            }
            else
            {
                insert.Parameters.AddWithValue("@nickname", txtNickName.Text.Trim());
                Session["NickName"] = txtNickName.Text.Trim();
            }
            Session["FirstName"] = txtFirstName.Text.Trim();
            Session["last"] = txtLastName.Text.Trim();
            Session["E-Mail"] = txtEmail.Text.Trim();
            insert.ExecuteNonQuery();
            insert.CommandText = "UPDATE [dbo].[BusinessEntity] SET [BusinessEntityEmail]=@businessEmail, [LastUpdated] = @LastUpdated,[LastUpdatedBy] = @LastUpdatedBy WHERE BusinessEntityID= @BusinessEntityID";
            insert.Parameters.AddWithValue("@businessEmail", txtEmail.Text);
            insert.ExecuteNonQuery();
            string name = Session["FirstName"].ToString() + " " + Session["Middle"].ToString() + " " + Session["last"].ToString();
            Master.MasterPageLabel = name;
            lblFullName.Text = name;
            lblName.Text = name + "'s Profile";
            lblContact.Text = Session["E-Mail"].ToString();
            lblNick.Text = Session["NickName"].ToString();
            Response.Write("<script>alert('Your Information has been updated')</script>");
            sc.Close();
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
                cmd.CommandText = "UPDATE [dbo].[Person] SET [ProfilePicture] = @ProfilePicture WHERE PersonID=@id and BusinessEntityID= @BusinessEntityID";

                cmd.Parameters.AddWithValue("@ProfilePicture", bytes);
                cmd.Parameters.AddWithValue("@id", Session["ID"]);
                cmd.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
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
        string sql = "SELECT [ProfilePicture] FROM person WHERE PersonEmail = @PersonEmail and BusinessEntityID= @BusinessEntityID";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@PersonEmail", Session["E-mail"]);
        cmd.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
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

        string sql = " update [dbo].[Person] set [DefaultPage] = @page where PersonID=@personID and BusinessEntityID= @BusinessEntityID";
        SqlCommand cmd = new SqlCommand(sql, sc);
        cmd.Parameters.AddWithValue("@personID", Session["ID"]);
        cmd.Parameters.AddWithValue("@page", dropPages.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
        cmd.ExecuteScalar();
        sc.Close();
    }
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        popCategory.Show();
        CategoryGrid.PageIndex = e.NewPageIndex;
        this.displayCategories();
    }

    protected void btnAddNewValue_Click( object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand valuesSelect = new SqlCommand("select [ValueName] from [Value] where BusinessEntityID=@businessID");
        valuesSelect.Parameters.AddWithValue("@businessID", Convert.ToInt32(Session["BusinessEntityID"]));
        valuesSelect.Connection = sc;
        SqlDataReader reader = valuesSelect.ExecuteReader();
        if(reader.HasRows)
        {
            reader.Read();
            Session["ValueName"] = reader["ValueName"].ToString();
        }
        reader.Close();
        if(txtValueName.Text== Session["ValueName"].ToString())
        {
            Response.Write("<script>alert('Value has already existed!')</script>");
        }
        else
        {
            SqlCommand valuesInsert = new SqlCommand("INSERT INTO[dbo].[Value] ([ValueName],[ValueDescription],[LastUpdated],[LastUpdatedBy],[BusinessEntityID]) VALUES (@ValueName, @ValueDescription, @LastUpdated, @LastUpdatedBy, @BusinessEntityID)");
            valuesInsert.Connection = sc;
            valuesInsert.Parameters.AddWithValue("@ValueName", txtValueName.Text);
            valuesInsert.Parameters.AddWithValue("@ValueDescription", txtValueDesc.Text);
            valuesInsert.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToShortDateString());
            valuesInsert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
            valuesInsert.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"]));
            valuesInsert.ExecuteNonQuery();
            Response.Write("<script>alert('New Value Added!')</script>");
            txtValueDesc.Text = String.Empty;
            txtValueName.Text = String.Empty;
        }
        sc.Close();
    }
    protected void displayValues()
    {

        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        SqlCommand valuesSelect = new SqlCommand( "SELECT ValueID, ValueName, ValueDescription FROM [dbo].[Value] WHERE BusinessEntityID = @BusinessEntityID"); // need to add business entity thing
        valuesSelect.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"]));
        valuesSelect.Connection = sc;

        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(valuesSelect);
        da.Fill(ds);
        ValueGrid.DataSource = ds;
        ValueGrid.DataBind();
    }
    protected void displayCategories()
    {
        //if (Session["BusinessEntityID"] != null)
        //{
        //    id = Convert.ToInt32(Session["ID"]);
        //}
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        SqlCommand categoriesSelect = new SqlCommand("SELECT CategoryID, CategoryName FROM [dbo].[Category] WHERE BusinessEntityID = @BusinessEntityID"); // need to add business entity thing
        categoriesSelect.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"]));
        categoriesSelect.Connection = sc;

        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(categoriesSelect);
        da.Fill(ds);
        CategoryGrid.DataSource = ds;
        CategoryGrid.DataBind();
    }

    protected void btnAddNewCategory_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand valuesSelect = new SqlCommand("select [CategoryName] from [Category] where BusinessEntityID=@businessID");
        valuesSelect.Parameters.AddWithValue("@businessID", Convert.ToInt32(Session["BusinessEntityID"]));
        valuesSelect.Connection = sc;
        SqlDataReader reader = valuesSelect.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Session["CategoryName"] = reader["CategoryName"].ToString();
        }
        reader.Close();
        if (txtCatName.Text == Session["CategoryName"].ToString())
        {
            Response.Write("<script>alert('Category has already existed!')</script>");
        }
        else
        {
            SqlCommand categoriesInsert = new SqlCommand("INSERT INTO[dbo].[Category] ([CategoryName],[LastUpdated],[LastUpdatedBy],[BusinessEntityID]) VALUES (@CategoryName, @LastUpdated, @LastUpdatedBy, @BusinessEntityID)");
            categoriesInsert.Connection = sc;
            categoriesInsert.Parameters.AddWithValue("@CategoryName", txtCatName.Text);
            categoriesInsert.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            categoriesInsert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
            categoriesInsert.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"]));
            categoriesInsert.ExecuteNonQuery();

            Response.Write("<script>alert('New Category Added!')</script>");
            txtCatName.Text = String.Empty;
        }
        sc.Close();
    }

    protected void ValueGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ValueGrid.PageIndex = e.NewPageIndex;
        this.displayValues();
        popValue.Show();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        popLimit.Show();
        GridLimit.EditIndex = e.NewEditIndex;
      
        dataFill();
       
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        popLimit.Show();
        GridLimit.EditIndex = -1;
        dataFill();
    }

    protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {

        SqlConnection sc = new SqlConnection();

        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand Update = new SqlCommand();
        String Period = ((Label)GridLimit.Rows[e.RowIndex].FindControl("lblLimitPeriod")).Text;
        String Limit = ((TextBox)GridLimit.Rows[e.RowIndex].FindControl("txtlimit")).Text;
        String Alert = ((TextBox)GridLimit.Rows[e.RowIndex].FindControl("txtAlert")).Text;
        Update.Connection = sc;
        
        Update.CommandText = "update [dbo].[BusinessEntity] set [GiveLimitation]=@limit,[LimitPeriod]=@period,[AlertBalance]=@alert where[BusinessEntityID] = @ID";
        Update.Parameters.AddWithValue("@limit",Limit);
        Update.Parameters.AddWithValue("@period", Period);
        Update.Parameters.AddWithValue("@alert", Alert);
        Update.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["BusinessEntityID"]));

        Update.ExecuteNonQuery();
        sc.Close();
  

        GridLimit.EditIndex = -1;
        Response.Write("<script>alert('Limitations Updated successfully!')</script>");
        
        dataFill();
      
    }
    public void dataFill()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand search = new SqlCommand();
        search.Connection = sc;
      
       
        search.CommandText = "SELECT [LimitPeriod], [GiveLimitation], [AlertBalance] FROM[BusinessEntity] where[BusinessEntityID] = @ID";

        search.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["BusinessEntityID"]));
        
        
        SqlDataAdapter adapter = new SqlDataAdapter(search);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        GridLimit.DataSource = ds;
        GridLimit.DataBind();
        sc.Close();
     
    }

    protected void ValueGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        popValue.Show();
        ValueGrid.EditIndex = -1;
        displayValues();
    }

    protected void ValueGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        popValue.Show();
        ValueGrid.EditIndex = e.NewEditIndex;
        displayValues();
    }

    protected void ValueGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection sc = new SqlConnection();

        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand Update = new SqlCommand();
        string ID = ((Label)ValueGrid.Rows[e.RowIndex].FindControl("lblValueID")).Text;
        string ValueName = ((TextBox)ValueGrid.Rows[e.RowIndex].FindControl("textValueName")).Text;
        String ValueDescription = ((TextBox)ValueGrid.Rows[e.RowIndex].FindControl("textValueDescription")).Text;
        Update.Connection = sc;

        Update.CommandText = "update [dbo].[Value] set [ValueName]=@limit,[ValueDescription]=@period, LastUpdated=@last, LastUpdatedBy=@By where [ValueID] = @ID";
        Update.Parameters.AddWithValue("@limit", ValueName);
        Update.Parameters.AddWithValue("@period", ValueDescription);
        Update.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        Update.Parameters.AddWithValue("@last", DateTime.Now.ToShortDateString());
        Update.Parameters.AddWithValue("@By", Session["loggedIn"].ToString());
        Update.ExecuteNonQuery();
        sc.Close();

        ValueGrid.EditIndex = -1;
        Response.Write("<script>alert('Value Updated successfully!')</script>");

        displayValues();
    }

    protected void ValueGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string ID = ((Label)ValueGrid.Rows[e.RowIndex].FindControl("lblValueID")).Text;
        //string ValueName = ((System.Web.UI.WebControls.Label)ValueGrid.Rows[e.RowIndex].FindControl("lblValueName")).Text;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "delete from [dbo].[Value] where ValueID =@ID";
        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        cmd.ExecuteNonQuery();
        displayValues();
        ValueGrid.EditIndex = -1;
        Response.Write("<script>alert('Deleted Value successfully!')</script>");
        sc.Close();
        displayValues();
    }

    protected void CategoryGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        popCategory.Show();
        CategoryGrid.EditIndex = -1;
        displayCategories();
    }

    protected void CategoryGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        popCategory.Show();
        CategoryGrid.EditIndex = e.NewEditIndex;
        displayCategories();
    }

    protected void CategoryGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection sc = new SqlConnection();

        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand Update = new SqlCommand();
        string ID = ((Label)CategoryGrid.Rows[e.RowIndex].FindControl("lblCategoryID")).Text;
        string CategoryName = ((TextBox)CategoryGrid.Rows[e.RowIndex].FindControl("textCategoryName")).Text;
        Update.Connection = sc;

        Update.CommandText = "update [dbo].[Category] set [CategoryName]=@limit, LastUpdated=@last, LastUpdatedBy=@By where [CategoryID] = @ID";
        Update.Parameters.AddWithValue("@limit", CategoryName);
        Update.Parameters.AddWithValue("@last", DateTime.Now.ToShortDateString());
        Update.Parameters.AddWithValue("@By", Session["loggedIn"].ToString());
        Update.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        Update.ExecuteNonQuery();
        sc.Close();

        CategoryGrid.EditIndex = -1;
        Response.Write("<script>alert('Category Updated successfully!')</script>");

        displayCategories();
    }

    protected void CategoryGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string ID = ((Label)CategoryGrid.Rows[e.RowIndex].FindControl("lblCategoryID")).Text;
        
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "delete from [dbo].[Category] where [CategoryID] = @ID";
        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        cmd.ExecuteNonQuery();
        CategoryGrid.EditIndex = -1;
        Response.Write("<script>alert('Deleted Category successfully!')</script>");
        sc.Close();
        displayCategories();
    }
}