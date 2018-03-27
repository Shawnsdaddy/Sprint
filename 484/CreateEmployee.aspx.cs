using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;

public partial class CreateEmployee : System.Web.UI.Page

{
    private string privilegetype;

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
                if (!IsPostBack)
                {
                    showEmployee();
                }
                break;
        }

        
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand search = new SqlCommand();
        search.Connection = sc;
        if (Session["JobTitle"].ToString() == "CEO")
        {
            search.CommandText = "SELECT [PersonID], [FirstName], ISNULL([MI],'') as MI, [LastName], [PersonEmail], [JobTitle], [Privilege] FROM [Person] where (Status = 1 and Privilege != 'RewardProvider' and BusinessEntityID = @BusinessEntityID and [JobTitle] != 'CEO') " +
            "and ((FirstName like '%'+ @first +'%') or (LastName like '%'+ @last+ '%')) and (PersonEmail like'%' + @email+ '%')";
        }
        else
        {
            search.CommandText = "SELECT [PersonID], [FirstName], isNull([MI],'') as MI, [LastName], [PersonEmail], [JobTitle], [Privilege] FROM [Person] where (Status = 1 and Privilege != 'RewardProvider' and BusinessEntityID = @BusinessEntityID and  Privilege != @LoginPrivilege " +
           "and ((FirstName like '%'+ @first +'%') or (LastName like '%'+ @last+ '%')) and (PersonEmail like'%' + @email+ '%')";
        }
        search.Parameters.AddWithValue("@first", txtName.Text);
        search.Parameters.AddWithValue("@last", txtName.Text);
        search.Parameters.AddWithValue("@email", txtemail.Text);
        search.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        search.Parameters.AddWithValue("@loginPrivilege", Session["Privilege"].ToString());

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
        if(gdvShow.SelectedValue == null)
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
        if(gdvShow.SelectedRow.Cells[2].Text != "&nbsp;") { txtMiddle.Text = gdvShow.SelectedRow.Cells[2].Text.Trim(); } else { txtMiddle.Text = string.Empty; }
           
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

}
