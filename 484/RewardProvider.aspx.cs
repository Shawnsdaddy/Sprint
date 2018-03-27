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
            Calendar1.VisibleDate = DateTime.Today;
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
            default:
                lblPoints.Text = "Welcome, " + Session["JobTitle"] + "!";
                break;
        }

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
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();


            SqlCommand insertIntoCalendar = new SqlCommand("INSERT INTO [dbo].[Calendar] ([PersonID],[EventName],[Date],[Location],[LastUpdated],[LastUpdatedBy]) VALUES " +
               "(@PersonID, @EventName, @Date, @Location, @LastUpdated, @LastUpdatedBy)");
            insertIntoCalendar.Connection = sc;
            insertIntoCalendar.Parameters.AddWithValue("@PersonID", Session["ID"].ToString());
            insertIntoCalendar.Parameters.AddWithValue("@EventName", txtEventName.Text);
            var time = Convert.ToDateTime(txtTime.Text);
            string Time = time.ToString("hh:mm:ss");
            DateTime datetime = DateTime.Parse(txtDate.Text + " " + Time);
            insertIntoCalendar.Parameters.AddWithValue("@Date", datetime);
            insertIntoCalendar.Parameters.AddWithValue("Location", txtStreet.Text + txtCity.Text + ", " + drpState.SelectedValue + " "+txtZip.Text);
            insertIntoCalendar.Parameters.AddWithValue("LastUpdated", DateTime.Now);
            insertIntoCalendar.Parameters.AddWithValue("LastUpdatedBy", Session["JobTitle"]);
            insertIntoCalendar.ExecuteNonQuery();

            Response.Write("<script>alert('New Event added successfully! ')</script>");
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
    }
}