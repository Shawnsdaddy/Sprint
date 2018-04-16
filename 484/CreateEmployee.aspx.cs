using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

using System.Configuration;
using System.Data.OleDb;
using System.IO;

using OfficeOpenXml;


public partial class CreateEmployee : System.Web.UI.Page

{
    private string privilegetype;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
                {
                    showEmployee();
                }
        //gdvShow.DataBind();
    }
    public void showEmployee()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand fill = new SqlCommand();
        fill.Connection = sc;
        if (Session["JobTitle"].ToString() == "CEO")
        {
            fill.CommandText = "SELECT [PersonID], [FirstName], [MI], [LastName], [PersonEmail], [JobTitle], [Privilege] FROM [Person] where Status = 1 and Privilege != 'RewardProvider' and BusinessEntityID = @BusinessEntityID and [JobTitle] != 'CEO'";
        }
        else
        {
            fill.CommandText = "SELECT [PersonID], [FirstName], [MI], [LastName], [PersonEmail], [JobTitle], [Privilege] FROM [Person] where Status = 1 and Privilege != 'RewardProvider' and Privilege != @LoginPrivilege and BusinessEntityID = @BusinessEntityID";
        }
        fill.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        fill.Parameters.AddWithValue("@loginPrivilege", Session["Privilege"].ToString());

        SqlDataAdapter adapter = new SqlDataAdapter(fill);

        DataSet ds = new DataSet();
        adapter.Fill(ds);

        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
    }

    public void Send_Mail(String email, String Name, String Password)
    {
        String message = "Dear Employee: \n";
        message += "Your account has been created!!\n";
        message += "Please login with UserName and Password provided below:\n";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand search = new SqlCommand();
        search.Connection = sc;
        if (Session["JobTitle"].ToString() == "CEO")
        {
            search.CommandText = "SELECT [PersonID], [FirstName], ISNULL([MI],'') as MI, [LastName], [PersonEmail], [JobTitle], [Privilege] FROM [Person] where (Status = 1 and Privilege != 'Administrative' and BusinessEntityID = @BusinessEntityID and [JobTitle] != 'CEO') " +
            "and ((FirstName like '%'+ @first +'%') or (LastName like '%'+ @last+ '%')) and (PersonEmail like'%' + @email+ '%')";
        }
        else
        {
            search.CommandText = "SELECT [PersonID], [FirstName], isNull([MI],'') as MI, [LastName], [PersonEmail], [JobTitle], [Privilege] FROM [Person] where (Status = 1 and Privilege != 'Administrative' and BusinessEntityID = @BusinessEntityID and  [JobTitle] != @LoginJobTitle " +
           "and ((FirstName like '%'+ @first +'%') or (LastName like '%'+ @last+ '%')) and (PersonEmail like'%' + @email+ '%')";
        }
        search.Parameters.AddWithValue("@first", txtName.Text);
        search.Parameters.AddWithValue("@last", txtName.Text);
        search.Parameters.AddWithValue("@email", txtemail.Text);
        search.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        search.Parameters.AddWithValue("@loginPrivilege", Session["JobTitle"]);

        SqlDataAdapter adapter = new SqlDataAdapter(search);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        gdvShow.DataSource = ds;
        gdvShow.DataBind();
        sc.Close();
        gdvShow.SelectedIndex = -1;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Person employee = new Person(txtFirstName.Text, txtLastName.Text, txtEmployeeEmail.Text, ddlJob.SelectedItem.Text, Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        employee.setLastUpdatedBy((string)(Session["loggedIn"]));
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select [PersonEmail] from [Person] where [PersonEmail] = @Email and BusinessEntityID = @BusinessEntityID";
        insert.Parameters.AddWithValue("@Email", employee.getEmail());
        insert.Parameters.AddWithValue("@BusinessEntityID", employee.getBusinessEntityID());
        SqlDataReader reader = insert.ExecuteReader();

        privilegetype = type.SelectedValue;

        if (reader.HasRows)
        {
            Response.Write("<script>alert('Email has already existed in Database')</script>");
            reader.Close();
            sc.Close();
        }
        else
        {
            reader.Close();
            insert.CommandText = "INSERT INTO [dbo].[Person] ([FirstName],[LastName],[MI],[PersonEmail],[JobTitle],[Privilege],[Password],[PointsBalance],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount],[Status]) VALUES" +
           "(@FirstName,@LastName,@MI,@Email,@JobTitle, @Privilege, @Password,@PointsBalance,@LastUpdated,@LastUpdatedBy,@BusinessEntityID,0,1)";
            insert.Parameters.AddWithValue("@FirstName", employee.getFirstName());
            insert.Parameters.AddWithValue("@LastName", employee.getLastName());
            insert.Parameters.AddWithValue("@PointsBalance", employee.getPointsBalance());
            insert.Parameters.AddWithValue("@Privilege", privilegetype);

            insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
            insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            insert.Parameters.AddWithValue("@JobTitle", employee.getEmployeeTitle());
            //insert.Parameters.AddWithValue("@Email", employee.getEmail());
            if (txtMI.Text.Trim() == "")
            {
                insert.Parameters.AddWithValue("@MI", DBNull.Value);
            }
            else
            {
                insert.Parameters.AddWithValue("@MI", txtMI.Text.Trim());
            }
            string password = System.Web.Security.Membership.GeneratePassword(8, 6);
            string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

            insert.Parameters.AddWithValue("@Password", passwordHashNew);

            insert.ExecuteNonQuery();
            sc.Close();
            Send_Mail(employee.getEmail(), employee.getEmail(), password);

            Response.Write("<script>alert('Employee Account: " + employee.getFirstName() + " " + employee.getMI() + " " + employee.getLastName() + " is created')</script>");
            txtFirstName.Text = string.Empty;
            txtMI.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmployeeEmail.Text = string.Empty;
            ddlJob.ClearSelection();
            type.SelectedIndex = -1;
        }
        showEmployee();
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
            showEmployee();
            Response.Write("<script>alert('The Employee has been Terminated')</script>");
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (gdvShow.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select an employee')</script>");
        }

    }
    protected void gdvShow_SelectedIndexChanged(object sender, EventArgs e)
    {

        int ID = Convert.ToInt32(gdvShow.SelectedRow.Cells[0].Text);

        txtFirst.Text = gdvShow.SelectedRow.Cells[1].Text;
        if (gdvShow.SelectedRow.Cells[2].Text != "&nbsp;") { txtMiddle.Text = gdvShow.SelectedRow.Cells[2].Text.Trim(); } else { txtMiddle.Text = string.Empty; }

        txtLast.Text = gdvShow.SelectedRow.Cells[3].Text;
        txtEmployee.Text = gdvShow.SelectedRow.Cells[4].Text;

        if (gdvShow.SelectedRow.Cells[5].Text.Equals("Trucking"))
        {
            ddlTitle.SelectedIndex = 1;
        }
        else if (gdvShow.SelectedRow.Cells[5].Text.Equals("Logistics"))
        {
            ddlTitle.SelectedIndex = 2;
        }

        if (gdvShow.SelectedRow.Cells[6].Text.Equals("Administrative"))
        {
            rdtnType.SelectedIndex = 0;
        }
        else if (gdvShow.SelectedRow.Cells[6].Text.Equals("Employee"))
        {
            rdtnType.SelectedIndex = 1;
        }
    }

    protected void btnUpdateEmployee_Click(object sender, EventArgs e)
    {
        if (gdvShow.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select an employee')</script>");
        }
        else
        {
            int selectID = Convert.ToInt32(gdvShow.SelectedRow.Cells[0].Text);

            Person employee = new Person(txtFirst.Text, txtLast.Text, txtEmployee.Text, ddlTitle.SelectedItem.Text, Convert.ToInt32(Session["BusinessEntityID"].ToString()));
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [PersonEmail] from [Person] where [PersonID] = @EmployeeID";
            insert.Parameters.AddWithValue("@EmployeeID", selectID);
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.Read())
            {
                Session["EmployeeEmail"] = reader["PersonEmail"].ToString();
            }

            reader.Close();
            privilegetype = rdtnType.SelectedValue;
            insert.CommandText = "UPDATE [dbo].[Person] SET [FirstName]=@FirstName,[LastName]=@LastName,[MI]=@MI,[PersonEmail]=@Email,[JobTitle]=@JobTitle,[Privilege]=@Privilege,[LastUpdated]=@LastUpdated,[LastUpdatedBy]=@LastUpdatedBy where PersonID=@EmployeeID";
            insert.Parameters.AddWithValue("@FirstName", employee.getFirstName());
            insert.Parameters.AddWithValue("@LastName", employee.getLastName());
            insert.Parameters.AddWithValue("@Privilege", privilegetype);
            insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"].ToString());
            insert.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            insert.Parameters.AddWithValue("@JobTitle", employee.getEmployeeTitle());
            insert.Parameters.AddWithValue("@Email", employee.getEmail());
            if (txtMiddle.Text.Trim() == "")
            {
                insert.Parameters.AddWithValue("@MI", DBNull.Value);
            }
            else
            {
                insert.Parameters.AddWithValue("@MI", txtMiddle.Text.Trim());
            }

            insert.ExecuteNonQuery();

            if (txtEmployee.Text.Trim() == Session["EmployeeEmail"].ToString())
            {
                Response.Write("<script>alert('Information has been updated')</script>");
            }
            else
            {
                string password = System.Web.Security.Membership.GeneratePassword(9, 1);
                string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
                insert.CommandText = "UPDATE [dbo].[Person] SET [Password]=@Password where PersonID=@EmployeeID";
                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.ExecuteNonQuery();
                Send_Mail(employee.getEmail(), employee.getEmail(), password);
                Response.Write("<script>alert('An E-mail has been send to new Address')</script>");
            }

            sc.Close();
            showEmployee();
        }
    }

    protected void gdvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvShow.PageIndex = e.NewPageIndex;
        showEmployee();
    }

    //protected void btnImport_Click(object sender, EventArgs e)
    //{

    //}
    
    //public void insertdataintosql(string fname, string lname,
    //    string mi, string personalemail, string jobtitle, string privilege)
    //{//inserting data into the Sql Server

    //    string password = System.Web.Security.Membership.GeneratePassword(8, 6);
    //    string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
    //    SqlConnection conn = new SqlConnection();
    //    conn.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = conn;
    //    cmd.CommandText = "INSERT INTO REWARDSYSTEM.[dbo].[Person] ([FirstName],[LastName],[MI],[PersonEmail],[JobTitle],[Privilege],[Password],[PointsBalance],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount],[Status]) VALUES" +
    //       "(@FirstName,@LastName,@MI,@Email,@JobTitle, @Privilege, @Password,@PointsBalance,@LastUpdated,@LastUpdatedBy,@BusinessEntityID,0,1)";
    //    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fname;
    //    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lname;
    //    cmd.Parameters.Add("@MI", SqlDbType.Char).Value = mi;
    //    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = personalemail;
    //    cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar).Value = jobtitle;
    //    cmd.Parameters.Add("@Privilege", SqlDbType.VarChar).Value = privilege;
    //    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = passwordHashNew;
    //    cmd.Parameters.Add("@PointsBalance", SqlDbType.Int).Value = 0;
    //    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = Session["loggedin"];
    //    cmd.Parameters.Add("@LastUpdated", SqlDbType.SmallDateTime).Value = DateTime.Now;
    //    cmd.Parameters.Add("@BusinessEntityID", SqlDbType.Int).Value = Session["BusinessEntityID"];
    //    cmd.CommandType = CommandType.Text;
    //    conn.Open();
    //    cmd.ExecuteNonQuery();
    //    conn.Close();
    //}

    protected void btnFinalImport_Click(object sender, EventArgs e)
    {int duplicate = 0;
        try
        {
            
            foreach (GridViewRow row in ImportExcel.Rows)
            {
               CheckBox box = (CheckBox)row.FindControl("ItemCheckBox");
                //string isSelected =(row.Cells[0].UniqueID);
                if (box.Checked)
                {

                   
                    string password = System.Web.Security.Membership.GeneratePassword(8, 6);
                    string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                    conn.Open();
                    SqlCommand valid = new SqlCommand();
                    valid.Connection = conn;
                    valid.CommandText = "select [PersonEmail] from [Person] where [PersonEmail] = @Email and BusinessEntityID = @BusinessEntityID";
                    valid.Parameters.AddWithValue("@Email", row.Cells[4].Text.ToString());
                    valid.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"]);
                    SqlDataReader reader = valid.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                       duplicate++;
                       reader.Close();

                    } 

                    else
                    {
                        reader.Close();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO REWARDSYSTEM.[dbo].[Person] ([FirstName],[LastName],[MI],[PersonEmail],[JobTitle],[Privilege],[Password],[PointsBalance],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount],[Status]) VALUES" +
                           "(@FirstName,@LastName,@MI,@Email,@JobTitle, @Privilege, @Password,@PointsBalance,@LastUpdated,@LastUpdatedBy,@BusinessEntityID,0,1)";

                        if (row.Cells[3].Text.Trim() != "")
                        {
                            cmd.Parameters.AddWithValue("@MI", row.Cells[3].Text.ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MI", DBNull.Value);
                        }


                            cmd.Parameters.AddWithValue("@FirstName", row.Cells[1].Text.ToString());
                        cmd.Parameters.AddWithValue("@LastName", row.Cells[2].Text.ToString());
                            

                       
                        cmd.Parameters.AddWithValue("@Email",  row.Cells[4].Text.ToString());
                        cmd.Parameters.AddWithValue("@JobTitle", row.Cells[5].Text.ToString());
                        cmd.Parameters.AddWithValue("@Privilege", row.Cells[6].Text.ToString());
                        cmd.Parameters.AddWithValue("@Password", passwordHashNew);
                        cmd.Parameters.AddWithValue("@PointsBalance", "0");
                        cmd.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedin"].ToString());
                        cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
                        //cmd.CommandType = CommandType.Text;
                        
                        cmd.ExecuteNonQuery();
                       
                    }
                    conn.Close();
                }
            }
        }
        catch(DataException)
        {
            Response.Write("<script>alert('Import Failed')</script>");
        }
        finally
            {
               Response.Write("<script>alert('Employee Imported with "+duplicate+" duplications Skipped!')</script>");
            ImportExcel.Visible = false;
            showEmployee();

        }


        }
        protected void btnUpload_Click(object sender, EventArgs e)
    {
        popImport.Show();
        ImportExcel.Visible = true;
        btnFinalImport.Visible = true;
        if (FileUpload1.HasFile)
        {
            if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
            {
                ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                ImportExcel.DataSource = ToDataTable(package);
                ImportExcel.DataBind();
            }
        }
       
    }
    protected static DataTable ToDataTable(ExcelPackage package)
    {
        
        ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
        DataTable table = new DataTable();
        foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
        {
            table.Columns.Add(firstRowCell.Text);
        }
   



        for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
        {
            var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
            var newRow = table.NewRow();
            foreach (var cell in row)
            {
               
                    newRow[cell.Start.Column - 1] = cell.Text;
                
            }
            table.Rows.Add(newRow);
        }
        return table;
    }


    private void ConvertToExcel( )
    {

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename= Template.xls");


        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        
     
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<TR> <Table  border='1' style='font - size:12.0pt; font - family:Times New Roman;'> <TR>");



        string[] header = { "FirstName", "LastName", "MI", "PersonEmail", "JobTitle", "Privilege" };

        //am getting my grid's column headers
        int columnscount = 6;

        for (int j = 0; j < columnscount; j++)
        {      
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");

            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write(header[j].ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            // HttpContext.Current.Response.Write("</th>");
        }

        HttpContext.Current.Response.Write("</TR>");
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();




    }





    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ConvertToExcel();
    }

 



}




