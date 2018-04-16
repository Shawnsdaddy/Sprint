using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
//using System.Windows.Forms;
using System.Configuration;
using System.IO;


public partial class GiftCardInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            gdvCard.Visible = false;
            gdvShow.Visible = true;
            Session["BusinessEntityID"] = null;
            Session["GiftCardID"] = null;
            showEntity();
        }
    }


    public void showEntity()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand search = new SqlCommand();
        search.Connection = sc;
        search.CommandText = "select [BusinessEntity].BusinessEntityID,[BusinessEntityName],[PhoneNumber],[BusinessEntityEmail] from BusinessEntity, ProviderRewards where[BusinessEntity].BusinessEntityID=ProviderRewards.BusinessEntityID and ProviderID=@ID and ProviderRewards.Status='Initial'";
        search.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["ID"]));
        SqlDataAdapter adapter = new SqlDataAdapter(search);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
      
        //btnAddCard.Enabled = false;
        //btnUpdateCard.Enabled = false;
        //gdvShow.Visible = true;
        //gdvCard.Visible = false;
    }

    public void showGiftCard()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand search = new SqlCommand();
        search.Connection = sc;
        //if (Session["JobTitle"].ToString() == "CEO")
        //{
        search.CommandText = "select [GiftCardID],[GiftCardPicture],format ([GiftCardAmount], 'N2', 'en-us') AS GiftCardAmount, [Status] from ProviderRewards where ProviderID=@ID and BusinessEntityID=@BID and status !='Initial';";
        search.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["ID"]));
        search.Parameters.AddWithValue("@BID", Convert.ToInt32(Session["BusinessEntityID"]));
        SqlDataAdapter adapter = new SqlDataAdapter(search);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvCard.DataSource = ds;
        gdvCard.DataBind();
        sc.Close();
        gdvCard.SelectedIndex = -1;

        btnAddCard.Enabled = true;
        btnUpdateCard.Enabled = true;
        //gdvShow.Visible = false;
        //gdvCard.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand search = new SqlCommand();
        search.Connection = sc;
            search.CommandText = "select [BusinessEntity].BusinessEntityID,[BusinessEntityName],[PhoneNumber],[BusinessEntityEmail] from BusinessEntity, ProviderRewards where[BusinessEntity].BusinessEntityID=ProviderRewards.BusinessEntityID and ProviderID=@ID and ProviderRewards.Status='Initial' "+
            "and ([BusinessEntityName] like '%'+ @name +'%') and ([BusinessEntityEmail] like'%' + @email+ '%')";
        search.Parameters.AddWithValue("@name", txtName.Text);
        search.Parameters.AddWithValue("@email", txtemail.Text);
        search.Parameters.AddWithValue("@ID", Session["ID"]);
        SqlDataAdapter adapter = new SqlDataAdapter(search);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
        gdvCard.SelectedIndex = -1;
        gdvShow.Visible = true;
        gdvCard.Visible = false;
        Session["BusinessEntityID"] = null;
        Session["GiftCardID"] = null;
    }

    protected void gdvShow_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["BusinessEntityID"] = gdvShow.SelectedRow.Cells[0].Text;
        Session["BusinessEntityID"] = gdvShow.SelectedValue.ToString();
        Session["CEOEmail"] = gdvShow.SelectedRow.Cells[3].Text;
        gdvShow.Visible = false;
        gdvCard.Visible = true;
        showGiftCard();
    }

    protected void btnUpdateGift_Click(object sender, EventArgs e)
    {
        if (gdvCard.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select a gift card')</script>");
            gdvCard.Visible = true;
            gdvShow.Visible = false;
        }
        else
        {
            int selectID = Convert.ToInt32(gdvCard.SelectedValue);          
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            ShowEmpImage(selectID);
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

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sc;
                    cmd.CommandText = "UPDATE [dbo].[ProviderRewards] SET [GiftCardPicture] = @ProfilePicture, [LastUpdated] = @Now, [LastUpdatedBy] = @Xinge WHERE GiftCardID=@id";
                    cmd.Parameters.AddWithValue("@ProfilePicture", bytes);
                    cmd.Parameters.AddWithValue("@id", selectID);
                    cmd.Parameters.AddWithValue("@Now", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@Xinge", Session["loggedIn"]);
                    cmd.ExecuteNonQuery();
                    //sc.Close();
                    ShowEmpImage(selectID);
                    Response.Write("<script>alert('Gift Card picture has been updated')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");
                    popUpdate.Show();
                }
            }
            catch
            {
                Response.Write("<script>alert('Picture Size is exceeding the databases capability')</script>");
                popUpdate.Show();
            }

            sc.Close();
            showGiftCard();
        }
    }
    public void ShowEmpImage(int id)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select [GiftCardPicture] from [ProviderRewards] where [GiftCardID] = @GiftID";
        insert.Parameters.AddWithValue("@GiftID", id);
        SqlDataReader reader = insert.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (!Convert.IsDBNull(reader["GiftCardPicture"]))
                {
                    Byte[] imagedata = (byte[])reader["GiftCardPicture"];
                    string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
                    updatePicture.ImageUrl = "data:image/png;base64," + img;

                }
                else
                {
                    updatePicture.ImageUrl = "~/image/empty.png";
                }
            }
        }
    }


    protected void gdvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvShow.PageIndex = e.NewPageIndex;
        showEntity();
    }

    protected void gdvCard_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvCard.PageIndex = e.NewPageIndex;
        showGiftCard();
    }

    protected void gdvCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["GiftCardID"] = gdvCard.SelectedRow.Cells[0].Text;
        gdvCard.Visible = true;
        ShowEmpImage(Convert.ToInt32(gdvCard.SelectedValue));
    }

    public void Send_Mail(String email, String Name)
    {
        String message = "Dear CEO: \n";
        message += Name + " has already updated the gift card!!\n";
        message += "You can login to check!\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Gift Card Have Been Updated(DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
    protected void OnRowDataBound2(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (!Convert.IsDBNull(dr["GiftCardPicture"]))
            {
                string imageUrl = "data:image/png;base64," + Convert.ToBase64String(resize((byte[])dr["GiftCardPicture"]));
                (e.Row.FindControl("Image1") as Image).ImageUrl = imageUrl;
            }
        }
    }
    public static Byte[] resize(byte[] myBytes)
    {
        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(myBytes);
        System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
        System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(40, 40, null, IntPtr.Zero);
        System.IO.MemoryStream myResult = new System.IO.MemoryStream();
        newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);  //Or whatever format you want.
        return myResult.ToArray();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (gdvShow.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select an entity')</script>");
        }
        else
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [GiftCardAmount] from [ProviderRewards] where BusinessEntityID = @BusinessEntityID and GiftCardAmount =@amount and providerID = @providerID and Status != 'initial'";
            insert.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"]));
            insert.Parameters.AddWithValue("@amount", txtAmount.Text);
            insert.Parameters.AddWithValue("@ProviderID", Convert.ToInt32(Session["ID"]));
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("<script>alert('Gift Card has already existed')</script>");
                reader.Close();
            }
            else
            {
                reader.Close();
                insert.CommandText = "INSERT INTO [dbo].[ProviderRewards] (GiftCardAmount,GiftCardPicture,[LastUpdated],[LastUpdatedBy],[Status],[BusinessEntityID],ProviderID) VALUES" +
               "(@amount,@picture,@lasted,@updatedby,'Pending',@BusinessEntityID,@ProviderID)";
                insert.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
                insert.Parameters.AddWithValue("@updatedby", Session["loggedIn"].ToString());              
                
                try
                {
                    HttpPostedFile postedFile = PictureAdd.PostedFile;
                string filename = Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(filename);
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
                    {

                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                        insert.Parameters.AddWithValue("@picture", bytes);
                        insert.ExecuteNonQuery();
                        Response.Write("<script>alert('New Gift Card has been created')</script>");
                        Send_Mail(Session["CEOEmail"].ToString(), Session["ProviderName"].ToString());
                    }
                    else
                    {
                        Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");
                        popAdd.Show();
                    }
            }
                catch
            {
                Response.Write("<script>alert('Picture Size is exceeding the databases capability')</script>");
                popAdd.Show();
            }
        }
            showGiftCard();
            sc.Close();
        }
        
    }


    protected void gdvCard_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvCard.EditIndex = e.NewEditIndex;
        showGiftCard();

    }

    protected void gdvCard_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvCard.EditIndex = -1;
        showGiftCard();
    }

    protected void gdvCard_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

            SqlCommand Update = new SqlCommand();
            string GID = ((Label)gdvCard.Rows[e.RowIndex].FindControl("lblGID")).Text;

            String Amount = ((TextBox)gdvCard.Rows[e.RowIndex].FindControl("txtCAmount")).Text;
            Update.Connection = sc;

        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select [GiftCardAmount] from [ProviderRewards] where BusinessEntityID = @BusinessEntityID and GiftCardAmount =@amount and ProviderID = @ProviderID and Status != 'initial'";
        insert.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"]));
        insert.Parameters.AddWithValue("@amount", Amount);
        insert.Parameters.AddWithValue("@ProviderID", Session["ID"]);
        SqlDataReader reader = insert.ExecuteReader();
        

        if (reader.HasRows)
        {
            Response.Write("<script>alert('Gift Card has already existed')</script>");
            reader.Close();
        }
        else
        {
            //SqlConnection sc = new SqlConnection();
            //sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            //sc.Open();


            reader.Close();
            Update.CommandText = "UPDATE [dbo].[ProviderRewards] SET[GiftCardAmount] = @Amount, [Status]='Pending', [LastUpdated] = @Now, [LastUpdatedBy] = @Xinge WHERE GiftCardID = @PID";
            Update.Parameters.AddWithValue("@PID", Convert.ToInt32(GID));
            Update.Parameters.AddWithValue("@Amount", Amount);
            Update.Parameters.AddWithValue("@Xinge", Session["loggedIn"]);
            Update.Parameters.AddWithValue("@Now", DateTime.Now.ToShortDateString());
            Update.ExecuteNonQuery();
            Response.Write("<script>alert('Gift Card Amount updated successfully')</script>");

            Send_Mail(Session["CEOEmail"].ToString(), Session["ProviderName"].ToString());
            gdvCard.EditIndex = -1;
            gdvCard.DataBind();
            sc.Close();
            showGiftCard();
        }

    }

    protected void gdvCard_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        //select command
        SqlCommand Delete = new SqlCommand();
        string GID = ((Label)gdvCard.Rows[e.RowIndex].FindControl("lblGID")).Text;
        Delete.Connection = sc;
        Delete.CommandText = "Update [dbo].[ProviderRewards] Set [Status] = 'Declined', [LastUpdated]=@lasted,[LastUpdatedBy]=@updatedby where GiftCardID = @PID";
        Delete.Parameters.AddWithValue("@PID", Convert.ToInt32(GID));
        Delete.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
        Delete.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
        Delete.ExecuteNonQuery();
        gdvCard.DataBind();
        Response.Write("<script>alert('Gift Card deleted successfully')</script>");
        showGiftCard();
        Send_Mail(Session["CEOEmail"].ToString(), Session["ProviderName"].ToString());
        sc.Close();
    }

    protected void BusinessEntities(object sender, EventArgs e)
    {
        string Money = "SELECT        BusinessEntity.BusinessEntityName [Company Name], Person.LastName + ISNULL(Person.MI, '') + Person.FirstName AS CEO, BusinessEntity.PhoneNumber AS [Contact Number], BusinessEntity.BusinessEntityEmail AS Email, "+
                        " ProviderRewards.GiftCardAmount AS[Gift Card Amount], COUNT(RedeemTransaction.RedeemTransactionID) AS[Redeemption Times] "+
"FROM BusinessEntity INNER JOIN "+
                         "ProviderRewards ON BusinessEntity.BusinessEntityID = ProviderRewards.BusinessEntityID INNER JOIN "+
                         "RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID INNER JOIN "+
                         "RedeemTransaction ON ProviderRewards.GiftCardID = RedeemTransaction.GiftCardID INNER JOIN "+
                         "Person ON BusinessEntity.BusinessEntityID = Person.BusinessEntityID "+
"GROUP BY BusinessEntity.BusinessEntityName, BusinessEntity.BusinessEntityEmail, ProviderRewards.GiftCardAmount, RewardProvider.ProviderID, Person.LastName, Person.MI, Person.FirstName, Person.JobTitle, "+
 "                        BusinessEntity.PhoneNumber "+
"HAVING(RewardProvider.ProviderID = "+ Session["ID"]+") AND(Person.JobTitle = 'CEO')";

        ConvertToExcel(CreateDataTable(Money), "Companies/Users", "Users Gift Card Redeemption Information");
    }


    private System.Data.DataTable CreateDataTable(string SQL)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        SqlCommand cmd = new SqlCommand(SQL, sc);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd);
        System.Data.DataTable dt = new System.Data.DataTable();
        da3.Fill(dt);
        return dt;
    }
    private void ConvertToExcel(System.Data.DataTable dt, string Sheetname, String Title)
    {

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename= " + Sheetname + ".xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<Td colspan='5' style='background-color:Maroon;border:solid 1 #fff;color:#fff;'><B> " + Title + "</B>");
        HttpContext.Current.Response.Write("<TR> <font style='font-size:12.0pt;font-family:Times New Roman;background-color: #D20B0C;color:#ffffff''> <TR>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        // sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<TR> <Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='5' cellPadding='0' " +
              "style='font-size:12.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = dt.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column 
               // HttpContext.Current.Response.Write("<TR> <font style = ' font-size:14.opt; background-color: #D20B0C;color:#ffffff'> <TR>");
               //HttpContext.Current.Response.Write("<TC> <font style='font-size:12.0pt; font-family:Times New Roman;'> <TR>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");

            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            // HttpContext.Current.Response.Write("</th>");
        }

        HttpContext.Current.Response.Write("</TR>");

        foreach (DataRow row in dt.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();




    }

    protected void btnUpdateCard_Click(object sender, EventArgs e)
    {
        if (gdvCard.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select a gift card')</script>");

        }
        else
        {
            ShowEmpImage(Convert.ToInt32(gdvCard.SelectedValue));
        }
    }
}
