using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class RewardProvider : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        laterthantoday.ValueToCompare = DateTime.Now.ToShortDateString();
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
       
                lblPoints.Text = "Welcome, " + Session["ProviderName"] + "!";
        Calendar1.VisibleDate = DateTime.Today;
        displayCompanyGrid();
        Session["BusinessEntityID"] = null;

    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        
    }
    //This changes the selected date and displays in a label
    protected void Calendar1_SelectionChange(object sender, EventArgs e)
    {

        displayGV();
    }
    private void displayGV()
    {
        SqlConnection sc = new SqlConnection();
        string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;

        sc.ConnectionString = connStr;
        sc.Open();
        var date = Calendar1.SelectedDate.ToString("M/d/yyyy");
        SqlCommand location2 = new SqlCommand("SELECT Date, Eventname as Event, Location FROM Calendar WHERE CONVERT(VARCHAR(10), cast(Date as date), 101) = @Date", sc);
        location2.Parameters.AddWithValue("@Date", Convert.ToDateTime(date).Date);
        SqlDataAdapter sda = new SqlDataAdapter(location2);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        gvEvents.DataSource = dt;
        gvEvents.DataBind();

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {

        try
        {
            if (Session["BusinessEntityID"] != null)
            {
                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                SqlCommand insertIntoCalendar = new SqlCommand("INSERT INTO [dbo].[Calendar] ([ProviderID] ,[EventName] ,[Date] ,[Location] ,[LastUpdated] ,[LastUpdatedBy] ,[BusinessEntityID]) " +
                    "VALUES (@PersonID, @EventName, @Date, @Location, @LastUpdated, @LastUpdatedBy, @BusinessEntityID)");
                insertIntoCalendar.Connection = sc;
                insertIntoCalendar.Parameters.AddWithValue("@PersonID", Session["ID"].ToString());
                insertIntoCalendar.Parameters.AddWithValue("@EventName", txtEventName.Text);
                var time = Convert.ToDateTime(txtTime.Text);
                string Time = time.ToString("hh:mm:ss");
                DateTime datetime = DateTime.Parse(txtDate.Text + " " + Time);
                insertIntoCalendar.Parameters.AddWithValue("@Date", datetime);
                insertIntoCalendar.Parameters.AddWithValue("@Location", txtStreet.Text + " "+ txtCity.Text + ", " + drpState.SelectedValue + " " + txtZip.Text);
                insertIntoCalendar.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                insertIntoCalendar.Parameters.AddWithValue("@LastUpdatedBy", Session["ProviderName"]);
                insertIntoCalendar.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"]);
                insertIntoCalendar.ExecuteNonQuery();

                Response.Write("<script>alert('New Event added successfully! ')</script>");
                
            }
            else
            {
                Response.Write("<script>alert('Please Choose a Business Enity ')</script>");
                popSelectCompany.Show();
            }
        }
        catch
        {
            Response.Write("<script>alert('Error Adding Event!')</script>");
        }
    }
    protected void gvEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEvents.PageIndex = e.NewPageIndex;
        this.displayGV();
    }
    protected void btnautofill_Click(object sender, EventArgs e)
    {
        txtEventName.Text = "Sprint Planning";
        txtDate.Text = "03/27/2018";
        txtTime.Text = "15:30";
        txtStreet.Text = "Zane Showker Hall 421 Bluestone Dr";
        txtCity.Text = "Harrisonburg";
        txtZip.Text = "22807";
        drpState.SelectedValue = "VA";
        popUpdate.Show();
    }

    protected void displayCompanyGrid()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        SqlCommand selectCompany = new SqlCommand("Select BusinessEntityID, BusinessEntityName, BusinessEntityEmail From BusinessEntity where BusinessEntityID in (select BusinessEntityID from [ProviderRewards] where ProviderID = @ProviderID)");
        selectCompany.Parameters.AddWithValue("@ProviderID", Session["ID"]);
        selectCompany.Connection = sc;
        //selectCompany.ExecuteNonQuery();
        SqlDataAdapter adptr = new SqlDataAdapter(selectCompany);
        DataTable dtb = new DataTable();
        adptr.Fill(dtb);
        CompanyGrid.DataSource = dtb;
        CompanyGrid.DataBind();
    }

    protected void BtnContinuetoAdd_Click(object sender, EventArgs e)
    {
        if (Session["BusinessEntityID"] != null){
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insertIntoCalendar = new SqlCommand("INSERT INTO [dbo].[Calendar] ([ProviderID] ,[EventName] ,[Date] ,[Location] ,[LastUpdated] ,[LastUpdatedBy] ,[BusinessEntityID]) " +
                "VALUES (@PersonID, @EventName, @Date, @Location, @LastUpdated, @LastUpdatedBy, @BusinessEntityID)");
            insertIntoCalendar.Connection = sc;
            insertIntoCalendar.Parameters.AddWithValue("@PersonID", Session["ID"].ToString());
            insertIntoCalendar.Parameters.AddWithValue("@EventName", txtEventName.Text);
            var time = Convert.ToDateTime(txtTime.Text);
            string Time = time.ToString("hh:mm:ss");
            DateTime datetime = DateTime.Parse(txtDate.Text + " " + Time);
            insertIntoCalendar.Parameters.AddWithValue("@Date", datetime);
            insertIntoCalendar.Parameters.AddWithValue("@Location", txtStreet.Text + txtCity.Text + ", " + drpState.SelectedValue + " " + txtZip.Text);
            insertIntoCalendar.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            insertIntoCalendar.Parameters.AddWithValue("@LastUpdatedBy", Session["ProviderName"]);
            insertIntoCalendar.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"]);
            insertIntoCalendar.ExecuteNonQuery();
        }
        else
        {
            Response.Write("<script>alert('Please Choose a Business Enity ')</script>");
            popSelectCompany.Show();
        }

    }

    protected void CompanyGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["BusinessEntityID"] = CompanyGrid.SelectedRow.Cells[0].Text;
        popSelectCompany.Show();
    }

    protected void btnEvent_Click(object sender, EventArgs e)
    {
        displayCompanyGrid();
    }


    protected void CompanyGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEvents.PageIndex = e.NewPageIndex;
        this.displayCompanyGrid();
    }
}