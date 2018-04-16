using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class SystemAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
                if (!Page.IsPostBack)
                {
                    GridBind();
                }
        
}


    public void GridBind()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand fill = new SqlCommand();
        fill.Connection = sc;
        fill.CommandText = "select BusinessEntityID, BusinessEntityName,BusinessEntityEmail from BusinessEntity " +
            "WHERE     BusinessEntityID != 1 and Status = 1 ";
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

        if ((companyname != null) && (companyemail == null))
        {
            fill.CommandText = "select BusinessEntityID, BusinessEntityName,BusinessEntityEmail from BusinessEntity " +
            "where        (BusinessEntity.BusinessEntityName LIKE + '%'+@name+'%') and BusinessEntityID != 1 and status = 1";
            fill.Parameters.AddWithValue("@name", companyname);
        }
        else if ((companyname == null) && (companyemail != null))
        {
            fill.CommandText = "select BusinessEntityID, BusinessEntityName,BusinessEntityEmail from BusinessEntity " +
            "Where      (BusinessEntity.BusinessEntityEmail LIKE + '%'+@email+'%') and BusinessEntityID != 1 and status = 1";
            fill.Parameters.AddWithValue("@email", companyemail);
        }
        else if ((companyname != null) && (companyemail != null))
        {
            fill.CommandText = "select BusinessEntityID, BusinessEntityName,BusinessEntityEmail from BusinessEntity " +
            "where        (BusinessEntity.BusinessEntityName LIKE + '%'+@name+'%') AND (BusinessEntity.BusinessEntityEmail LIKE + '%'+@email+'%') and BusinessEntityID != 1 and status = 1";
            fill.Parameters.AddWithValue("@name", companyname);
            fill.Parameters.AddWithValue("@email", companyemail);
        }            

        SqlDataAdapter adapter = new SqlDataAdapter(fill);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
    }
    protected void gdvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvShow.PageIndex = e.NewPageIndex;
        GridBind();
    }
    
    public void Send_Mail(String email)
    {
        String message = "Dear CEO: \n";
        message += "Your account has been terminated!!\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Account Has been Terminated(DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
    protected void gdvShow_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        string ID = ((Label)gdvShow.Rows[e.RowIndex].FindControl("lblBusinessEntityID")).Text;
        string email = ((Label)gdvShow.Rows[e.RowIndex].FindControl("lblBusinessEntityEmail")).Text;
        delete.CommandText = "UPDATE [dbo].[BusinessEntity] SET [status]=0, [LastUpdated]=@lasted,[LastUpdatedBy]=@updatedby where BusinessEntityID=@ID";
        delete.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
        delete.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
        delete.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        SqlDataAdapter adapter = new SqlDataAdapter(delete);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;

        //delete.CommandText = "UPDATE [dbo].[Person] SET [status]=0 where BusinessEntityID=@ID";
        //delete.ExecuteNonQuery();
        //SqlCommand statusChange = new SqlCommand("BusinessEntityStatusChange", sc);
        //statusChange.CommandType = CommandType.StoredProcedure;
        //statusChange.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(ID));
        //statusChange.ExecuteNonQuery();
        Response.Write("<script>alert('Terminate entity successfully!')</script>");
        txtCompany.Text = String.Empty;
        txtemail.Text = String.Empty;
        sc.Close();
        GridBind();
        Send_Mail(email);
    }
 
    protected void Download(object sender, EventArgs e)
    {
        string Money = "SELECT        BusinessEntity.BusinessEntityID, BusinessEntity.BusinessEntityName AS[Company Name],( Person.LastName + isnull(Person.MI, '') + Person.FirstName) AS CEO, BusinessEntity.PhoneNumber as[Contact Number], BusinessEntity.BusinessEntityEmail As Email "+
   "FROM            BusinessEntity INNER JOIN "+
    "                        Person ON BusinessEntity.BusinessEntityID = Person.BusinessEntityID "+
"WHERE(BusinessEntity.Status = 1) AND(Person.JobTitle = 'CEO')";

        ConvertToExcel(CreateDataTable(Money), "Users", "Users Information");
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
}