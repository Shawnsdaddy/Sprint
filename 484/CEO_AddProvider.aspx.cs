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

        // Existing
        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        select.CommandText = "SELECT ProviderRewards.GiftCardID, RewardProvider.ProviderName, RewardProvider.TypeOfBusiness, format(ProviderRewards.GiftCardAmount,'N2','en-us') as GiftCardAmount FROM ProviderRewards INNER JOIN RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID WHERE (ProviderRewards.Status = 'Pending') AND (ProviderRewards.BusinessEntityID = @businessentityID)";
        select.Parameters.AddWithValue("@businessentityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        SqlDataAdapter da = new SqlDataAdapter(select);
        DataTable exist = new DataTable();
        da.Fill(exist);
        gdvPending.DataSource = exist;
        gdvPending.DataBind();

        //New
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "SELECT ProviderRewards.GiftCardID, RewardProvider.ProviderName, RewardProvider.TypeOfBusiness, format(ProviderRewards.GiftCardAmount,'N2','en-us') as GiftCardAmount FROM ProviderRewards INNER JOIN RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID WHERE (ProviderRewards.Status = 'Approved') AND (ProviderRewards.BusinessEntityID = @ID)";
        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        SqlDataAdapter reader = new SqlDataAdapter(cmd);
        DataTable approve = new DataTable();
        reader.Fill(approve);
        gdvApproved.DataSource = approve;
        gdvApproved.DataBind();

        
        gdvPending.SelectedIndex = -1;
        //gdvApproved.SelectedIndex = -1;


        //Current Provider
        SqlCommand cmd2 = new SqlCommand();
        cmd2.Connection = sc;
        cmd2.CommandText = "SELECT        RewardProvider.ProviderName, RewardProvider.ProviderEmail FROM ProviderRewards INNER JOIN RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID WHERE(ProviderRewards.BusinessEntityID = @BusinessEntityID) AND(ProviderRewards.Status = 'initial') AND (RewardProvider.Status = 1)";
        cmd2.Parameters.AddWithValue("@businessentityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        SqlDataAdapter cp = new SqlDataAdapter(cmd2);
        DataTable provider = new DataTable();
        cp.Fill(provider);
        CurrentProvider.DataSource = provider;
        CurrentProvider.DataBind();

        sc.Close();
    }

    protected void gdvPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvPending.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void gdvPending_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string ID = ((System.Web.UI.WebControls.Label)gdvPending.Rows[e.RowIndex].FindControl("lblGiftCardID")).Text;
        string name = ((System.Web.UI.WebControls.Label)gdvPending.Rows[e.RowIndex].FindControl("lblProviderName")).Text;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "Select ProviderEmail from RewardProvider Where ProviderName = @name";
        cmd.Parameters.AddWithValue("@name", name);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Session["SelectedEmail"] = reader["ProviderEmail"].ToString();
        }
        reader.Close();
        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Update [dbo].[ProviderRewards] Set Status = 'Declined', LastUpdated=@lasted, LastUpdatedBy=@updatedby Where GiftCardID = @ID";
        delete.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
        delete.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
        delete.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        delete.ExecuteNonQuery();
        Response.Write("<script>alert('Declined Card successfully!')</script>");
        sc.Close();
        GridBind();
        Send_Mail(Session["SelectedEmail"].ToString(),"Declined");
    }
    public void Send_Mail(String email, string status)
    {
        String message = "Dear Reward Provider: \n";
        message += "Gift Card has been " + status + "!!\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Gift Card Status Updated ", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }
    public void Send_Provider(String email, string name)
    {
        String message = "Dear Reward Provider: \n";
        message += "You are invited by " + name + "!!\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Status Updated ", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }

    protected void gdvApproved_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        string ID = ((System.Web.UI.WebControls.Label)gdvApproved.Rows[e.RowIndex].FindControl("lblGiftCardID")).Text;
        string name = ((System.Web.UI.WebControls.Label)gdvApproved.Rows[e.RowIndex].FindControl("lblProviderName")).Text;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "Select ProviderEmail from RewardProvider Where ProviderName = @name";
        cmd.Parameters.AddWithValue("@name", name);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Session["ApprovedEmail"] = reader["ProviderEmail"].ToString();
        }
        reader.Close();
        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Update [dbo].[ProviderRewards] Set Status = 'Declined', LastUpdated=@lasted, LastUpdatedBy=@updatedby Where GiftCardID = @ID";
        delete.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
        delete.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
        delete.Parameters.AddWithValue("@ID", Convert.ToInt32(ID));
        delete.ExecuteNonQuery();
        Response.Write("<script>alert('Declined Card successfully!')</script>");
        sc.Close();
        GridBind();
        Send_Mail(Session["ApprovedEmail"].ToString(), "Declined");
    }

    protected void gdvApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvApproved.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        string name = txtProviderName.Text.Trim();
        string email = txtProviderEmail.Text.Trim();
        string type = TypeOfBusiness.SelectedItem.Text;
        string password = System.Web.Security.Membership.GeneratePassword(8, 6);
        string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
        cmd.CommandText = "SELECT ProviderRewards.ProviderID, RewardProvider.ProviderEmail FROM RewardProvider INNER JOIN ProviderRewards ON RewardProvider.ProviderID = ProviderRewards.ProviderID where ProviderEmail=@email GROUP BY ProviderRewards.ProviderID, RewardProvider.ProviderEmail, ProviderRewards.BusinessEntityID HAVING (ProviderRewards.BusinessEntityID = @BusinessEntityID)";
        cmd.Parameters.AddWithValue("@BusinessEntityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
        cmd.Parameters.AddWithValue("@email", email);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            Response.Write("<script>alert('You already have had this Reward Provider!')</script>");
            popAdd.Show();
        }
        else
        {
            reader.Close();
            cmd.CommandText = "select providerID from rewardprovider where ProviderEmail=@email";
            SqlDataReader exist = cmd.ExecuteReader();

            if (exist.HasRows)
            {
                exist.Read();
                int id = Convert.ToInt32(exist["ProviderID"].ToString());
                exist.Close();
                insert.CommandText = "Insert into [ProviderRewards] (ProviderID, BusinessEntityID, GiftCardAmount, Status, LastUpdated, LastUpdatedBy) " +
                "Values(@id,@EntityID,'10','Initial',@LasteUpdated,@LastUpdatedBy)";
                insert.Parameters.AddWithValue("@EntityID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
                insert.Parameters.AddWithValue("@id", id);
                insert.Parameters.AddWithValue("@LasteUpdated", DateTime.Now.ToShortDateString());
                insert.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"]);
                insert.ExecuteNonQuery();
                Response.Write("<script>alert('Created new Reward Provider successfully!')</script>");
                Send_Provider(email, Session["loggedIn"].ToString());
                txtProviderEmail.Text = String.Empty;
                txtProviderName.Text = String.Empty;
                TypeOfBusiness.SelectedIndex = -1;

            }
            else
            {
                exist.Close();
                cmd.CommandText = "Insert into [RewardProvider] (ProviderName, ProviderEmail, TypeOfBusiness, Password, Status, LastUpdated, LastUpdatedBy) " +
                    "Values(@name,@email,@type,@password,1,@lasted,@updatedby)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
                cmd.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
                cmd.Parameters.AddWithValue("@password", passwordHashNew);
                cmd.ExecuteNonQuery();

                insert.CommandText = "Insert into [ProviderRewards] (ProviderID, BusinessEntityID, GiftCardAmount, Status, LastUpdated, LastUpdatedBy) " +
                            "Values((select Max(ProviderID) from [RewardProvider]),@businessID,'10','Initial',@last,@by)";
                insert.Parameters.AddWithValue("@businessID", Convert.ToInt32(Session["BusinessEntityID"].ToString()));
                insert.Parameters.AddWithValue("@last", DateTime.Now.ToShortDateString());
                insert.Parameters.AddWithValue("@by", Session["loggedIn"]);
                insert.ExecuteNonQuery();

                Response.Write("<script>alert('Created new Reward Provider successfully!')</script>");
                Send_MailProvider(email, Session["loggedIn"].ToString(), email, password);
                txtProviderEmail.Text = String.Empty;
                txtProviderName.Text = String.Empty;
                TypeOfBusiness.SelectedIndex = -1;
                CurrentProvider.Visible = true;
                Response.Redirect("CEO_AddProvider.aspx");
            }           
        }

        sc.Close();
        gdvApproved.Visible = true;
        gdvPending.Visible = true;
        
    }
    public void Send_MailProvider(String email, string CEOName, string Name, string Password)
    {
        String message = "Dear Reward Provider: \n";
        message += "You have been invited into the company by " + CEOName + "!!\n";
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (gdvPending.SelectedValue == null)
        {
            Response.Write("<script>alert('Please select a gift card')</script>");
        }
        else
        {
            int selectID = Convert.ToInt32(gdvPending.SelectedValue.ToString());
            string name = gdvPending.SelectedRow.Cells[1].Text;

            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sc;
            cmd.CommandText = "Select ProviderEmail from RewardProvider Where ProviderName = @name";
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                Session["SelectedPendingEmail"] = reader["ProviderEmail"].ToString();
            }
            reader.Close();

            SqlCommand select = new SqlCommand();
            select.Connection = sc;
            select.CommandText = "Update [dbo].[ProviderRewards] Set Status = @status,LastUpdated=@lasted, LastUpdatedBy=@updatedby Where GiftCardID = @ID";
            select.Parameters.AddWithValue("@ID", selectID);
            select.Parameters.AddWithValue("@lasted", DateTime.Now.ToShortDateString());
            select.Parameters.AddWithValue("@updatedby", Session["loggedIn"]);
            select.Parameters.AddWithValue("@status", "Approved");
            select.ExecuteNonQuery();
            GridBind();
            Response.Write("<script>alert('Approved Card successfully!')</script>");
            //Send_Mail(Session["SelectedPendingEmail"].ToString(), "Approved");
            sc.Close();
            
        }
        gdvApproved.Visible = true;
        gdvPending.Visible = true;

    }


    protected void btnCurrent_Click(object sender, EventArgs e)
    {
        CurrentProvider.Visible = true;
    }
}


