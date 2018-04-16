using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class EmployeeReward : System.Web.UI.Page
{
    private int businessid;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
            gvEvents.DataSource = null;
            gvEvents.DataBind();
        }
        BindCalendar();
        rewardpool();
        AutoCompleteExtender1.ContextKey = Session["ID"].ToString() +' '+ Session["BusinessEntityID"].ToString();                
                    //updatecombox();
        if (!IsPostBack)
         {
         LatestUpdates();
         reset();
         }

       
  }

    private void BindCalendar()
    {
        Calendar1.SelectedDate = DateTime.Now;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> SearchName(string prefixText, int count, string contextKey)
    {

        string[] IDs = contextKey.Split(' ');
        //Connect to Database

        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        //Send Command
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "SELECT [PersonID],([FirstName] + ' ' + isnull([MI],'')+ ' ' + [LastName]+', '+ isnull([NickName],'')) as RewardName FROM [dbo].[Person] where (NickName like '%' + @SearchText + '%' or FirstName like '%' + @SearchText + '%' or LastName like '%' + @SearchText + '%') and status = 1 and [Privilege] = 'Employee' and PersonID != @loginID and loginCount != 0 and BusinessEntityID = @BusinessEntityID";
        //cmd.CommandText = "SELECT [FirstName] FROM[RewardSystemLab4].[dbo].[Person]  where FirstName like '%' + @SearchText + '%'";
        cmd.Parameters.AddWithValue("@SearchText", prefixText);
        
        cmd.Parameters.AddWithValue("@loginID", IDs[0]);
        cmd.Parameters.AddWithValue("@BusinessEntityID", IDs[1]);
        
        List<string> NameLists = new List<string>();
        SqlDataReader sdr = cmd.ExecuteReader();

        while (sdr.Read())
        {
            NameLists.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["RewardName"].ToString(), sdr["PersonID"].ToString()));
        }

        sc.Close();
        return NameLists;
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ReceiverID"] == null)
            {
                Response.Write("<script>alert('Please select a valid Receiver Name')</script>");
                txtName.Text = string.Empty;
                popReward.Show();
            }
            else if (Session["RewardAmount"] == null)
            {
                Response.Write("<script>alert('Please select a valid Reward Amount')</script>");
                popReward.Show();
            }
            else if (Convert.ToInt32(Session["ReceiverID"]) == Convert.ToInt32(Session["ID"]))
            {
                Response.Write("<script>alert('Can not rewawrd yourself')</script>");
                txtName.Text = string.Empty;
                popReward.Show();
            }
            else if (Session["ValueID"].ToString() == "-1" || Session["CategoryID"].ToString() == "-1" || Session["ValueID"] == null || Session["CategoryID"] == null)
            {
                Response.Write("<script>alert('Please select Value and Category')</script>");
                popReward.Show();
            }
            else if (CheckRewardLimit() >=Convert.ToInt32(Session["GiveLimit"].ToString()))
            {
                Response.Write("<script>alert('You can Only Make "+ Session["GiveLimit"].ToString()+" Reward per day')</script>");
            }
            else
            {
               
                double pointsAmount = Convert.ToDouble(rblRewardPoints.SelectedValue);
                string EventDate = DateTime.Now.ToShortDateString();
                string EventDescription = txtRDescription.Text;
                string LastUpdated = DateTime.Now.ToShortDateString();
                string LastUpdatedBy = Session["loggedIn"].ToString();
                int ReceiverID = Convert.ToInt32(Session["ReceiverID"]);
                int RewarderID = Convert.ToInt32(Session["ID"]);
                int CategoryID = Convert.ToInt32(ddlRCategory.SelectedValue);
                int ValueID = Convert.ToInt32(ddlRValue.SelectedValue);
                int Privacy = 1;
            try
            {
                //Connect to Database
                SqlConnection sc = new SqlConnection();
                    sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                    sc.Open();
                    PeerTranscation emp = new PeerTranscation(pointsAmount, EventDate, EventDescription, LastUpdated, LastUpdatedBy, ReceiverID, RewarderID, CategoryID, ValueID, Privacy);

                    string sqlString = "INSERT INTO [dbo].[PeerTransaction]([PointsAmount],[RewardDate],[EventDescription],[LastUpdated],[LastUpdatedBy],[ReceiverID],[RewarderID],[CategoryID],[ValueID],[Privacy]) VALUES (@PointsAmount,@RewardDate,@EventDescription,@LastUpdated,@LastUpdatedBy,@ReceiverID,@RewarderID,@CategoryID,@ValueID,@Privacy)";

                    SqlCommand insert = new SqlCommand(sqlString);
                    insert.Connection = sc;

                    insert.Parameters.AddWithValue("@PointsAmount", emp.getPoints());
                    insert.Parameters.AddWithValue("@RewardDate", emp.getDate());
                    insert.Parameters.AddWithValue("@EventDescription", emp.getDescription());
                    insert.Parameters.AddWithValue("@LastUpdated", emp.getLUD());
                    insert.Parameters.AddWithValue("@LastUpdatedBy", emp.getLUDB());
                    insert.Parameters.AddWithValue("@ReceiverID", emp.getReceiverID());
                    insert.Parameters.AddWithValue("@RewarderID", emp.getRewarderID());
                    insert.Parameters.AddWithValue("@CategoryID", emp.getCategoryID());
                    insert.Parameters.AddWithValue("@ValueID", emp.getValueID());
                    insert.Parameters.AddWithValue("@Privacy", emp.getPrivacy());

                    insert.ExecuteNonQuery();

                insert.CommandText = "select PersonEmail from person where personID=@id";
                insert.Parameters.AddWithValue("@id", Session["ReceiverID"].ToString());
                SqlDataReader reader = insert.ExecuteReader();
                reader.Read();
                Session["ReceiverEmail"] = reader["PersonEmail"].ToString();
                sc.Close();
                    Response.Write("<script>alert('Peer reward Successful')</script>");
                    LatestUpdates();
                    reset();
        }
                catch
        {
            Response.Write("<script>alert('Fail add to database')</script>");
            popReward.Show();
        }
            }
        }
        catch
        {
            Response.Write("<script>alert('Please select a valid name')</script>");
            txtName.Text = string.Empty;
        }


    }

    PagedDataSource pds = new PagedDataSource();
    private void LatestUpdates()
    {
        if (Session["ID"] != null)
        {

            businessid = Convert.ToInt32(Session["BusinessEntityID"]);
        }

        string getpost = "SELECT        isnull(Person.ReceivePrivacy, isnull(Person.NickName,Person.FirstName)) AS ReceiverName, isnull(Person_1.GivePrivacy,isnull(Person_1.NickName,Person_1.FirstName)) AS RewarderName, Category.CategoryName, Value.ValueName, PeerTransaction.RewardDate, PeerTransaction.LastUpdated, PeerTransaction.EventDescription, PeerTransaction.PointsAmount" +
                         " FROM            Person INNER JOIN PeerTransaction ON Person.PersonID = PeerTransaction.ReceiverID INNER JOIN Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN Category ON PeerTransaction.CategoryID = Category.CategoryID INNER JOIN Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID WHERE Person.BusinessEntityID = " + businessid + "ORDER BY PeerTransaction.PointsTransactionID DESC";

        SqlDataAdapter da = new SqlDataAdapter(getpost, ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString);
        DataTable dt = new DataTable();
        da.Fill(dt);

        pds.DataSource = dt.DefaultView;
        pds.AllowPaging = true;
        //pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        pds.PageSize = 10;
        pds.CurrentPageIndex = CurrentPage;
        //pds.CurrentPageIndex = 3;
        lnkbtnNext.Enabled = !pds.IsLastPage;
        lnkbtnPrevious.Enabled = !pds.IsFirstPage;

        dlPosts.DataSource = pds;
        dlPosts.DataBind();

        doPaging();


    }


    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("lnkbtnPaging"))
        {
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            LatestUpdates();
        }
    }

    protected void lnkbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        LatestUpdates();
    }

    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        LatestUpdates();
    }


    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Font.Bold = true;
        }
    }
 
    private void rewardpool()
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select pointsbalance from person where personID=@id";
        insert.Parameters.AddWithValue("@id", Session["ID"]);
        SqlDataReader reader = insert.ExecuteReader();
        reader.Read();
        Session["PointsBalance"] = Convert.ToInt32(reader["pointsbalance"]);
        lblPoints.Text = "Current Balance: " + Session["PointsBalance"].ToString();
    }

 

    protected void getCustomerID(object sender, EventArgs e)
    {
        Session["ReceiverID"] = Request.Form[hfEmployeeId.UniqueID];
    
    }

    protected void ddlRValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ValueID"] = ddlRValue.SelectedValue.ToString();
    }

    protected void ddlRCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CategoryID"] = ddlRCategory.SelectedValue.ToString();
    }

    protected void rblRewardPoints_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["RewardAmount"] = rblRewardPoints.SelectedValue.ToString();
        

    }

    protected void reset()
    {
        Session["ReceiverID"] = null;
        Session["ValueID"] = null;
        Session["CategoryID"] = null;
        Session["RewardAmount"] = null;
        txtName.Text = string.Empty;
        ddlRValue.ClearSelection();
        ddlRCategory.ClearSelection();
        txtRDescription.Text = string.Empty;
        rblRewardPoints.ClearSelection();
    }

    public int CurrentPage
    {
        get
        {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt32(this.ViewState["CurrentPage"].ToString());
        }
        set
        {
            this.ViewState["CurrentPage"] = value;
        }

    }

    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");
        for (int i = 0; i < pds.PageCount; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        dlPaging.DataSource = dt;
        dlPaging.DataBind();
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {

    }

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
        string date = Calendar1.SelectedDate.ToString("MM/dd/yyyy");
        SqlCommand location2 = new SqlCommand("SELECT [Date] As Time, Eventname as Event, Location FROM Calendar WHERE RIGHT('00' + CAST(DATEPART(month, [Date]) AS varchar(2)), 2) + '/' + RIGHT('00' + CAST(DATEPART(Day, [Date]) AS varchar(2)), 2) + '/' + RIGHT('0000' + CAST(DATEPART(year, [Date]) AS varchar(4)), 4) = @Date and BusinessEntityID= @BusinessEntityID", sc);
        location2.Parameters.AddWithValue("@Date", date);
        location2.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"]);
        SqlDataAdapter sda = new SqlDataAdapter(location2);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        gvEvents.DataSource = dt;
        gvEvents.DataBind();
        ModalPopupExtender1.Show();

    }


    protected void gvEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEvents.PageIndex = e.NewPageIndex;
        this.displayGV();
    }
    private int CheckRewardLimit()
    {
        int rewardCount = 0;
        SqlConnection sc = new SqlConnection();
        string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;

        sc.ConnectionString = connStr;
        sc.Open();


        SqlCommand command = new SqlCommand();
        command.Connection = sc;

        command.CommandText = "SELECT RewardDate, RewarderID, COUNT(PointsTransactionID) AS RewardTime " +
            "FROM PeerTransaction GROUP BY RewardDate, RewarderID " +
            "HAVING  (RewardDate =(SELECT CAST(CONVERT(varchar(10), GETDATE(), 110) AS datetime) AS Expr1)) " +
            "AND (RewarderID = @RewarderID)";
      
        command.Parameters.AddWithValue("@RewarderID", Convert.ToInt32(Session["ID"]));
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            rewardCount =Convert.ToInt32(reader["RewardTime"].ToString());
        }
        else
        {
            rewardCount = 0;
        }
            return rewardCount;
    }


    protected void AllGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AllGrid.PageIndex = e.NewPageIndex;
        this.displayAllEvents();
    }
    protected void displayAllEvents()
    {
        SqlConnection sc = new SqlConnection();
        string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;

        sc.ConnectionString = connStr;
        sc.Open();
        
        SqlCommand location2 = new SqlCommand("select EventName, Date, Location from Calendar where BusinessEntityID = @BusinessEntityID", sc);
        location2.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"]);
        SqlDataAdapter sda = new SqlDataAdapter(location2);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        AllGrid.DataSource = dt;
        AllGrid.DataBind();
        //popViewAll.Show();
    }

    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        displayAllEvents();
    }
}