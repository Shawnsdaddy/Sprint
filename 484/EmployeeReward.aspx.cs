using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using PayPal.Api;
using PayPal.Sample.Utilities;
using PayPal.Sample;

public partial class EmployeeReward : BaseSamplePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");

        BindCalendar();
        
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
            gvEvents.DataSource = null;
            gvEvents.DataBind();
        }
        else
        {
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
                    rewardpool();
                    AutoCompleteExtender1.ContextKey = Session["ID"].ToString();
                    //updatecombox();
                    if (!IsPostBack)
                    {
                        LatestUpdates();
                        reset();
                    }
                    break;
            }        
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
        //Connect to Database
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        //Send Command
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sc;
        cmd.CommandText = "SELECT [PersonID],([FirstName] + ' ' + isnull([MI],'')+ ' ' + [LastName]+', '+[NickName]) as RewardName FROM [dbo].[Person] where (NickName like '%' + @SearchText + '%' or FirstName like '%' + @SearchText + '%' or LastName like '%' + @SearchText + '%') and status = 1 and [Privilege] = 'Employee' and PersonID != @loginID and loginCount != 0";
        //cmd.CommandText = "SELECT [FirstName] FROM[RewardSystemLab4].[dbo].[Person]  where FirstName like '%' + @SearchText + '%'";
        cmd.Parameters.AddWithValue("@SearchText", prefixText);
        cmd.Parameters.AddWithValue("@loginID", contextKey);
        
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
                    RunSample();
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


        string getpost = "SELECT        isnull(Person.ReceivePrivacy, Person.NickName) AS ReceiverName, isnull(Person_1.GivePrivacy,Person_1.NickName) AS RewarderName, Category.CategoryName, Value.ValueName, PeerTransaction.RewardDate, PeerTransaction.LastUpdated, PeerTransaction.EventDescription, PeerTransaction.PointsAmount" +
                         " FROM            Person INNER JOIN PeerTransaction ON Person.PersonID = PeerTransaction.ReceiverID INNER JOIN Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN Category ON PeerTransaction.CategoryID = Category.CategoryID INNER JOIN Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID ORDER BY PeerTransaction.PointsTransactionID DESC";

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

    protected override void RunSample()
    {
        // ### Api Context
        // Pass in a `APIContext` object to authenticate 
        // the call and to send a unique request id 
        // (that ensures idempotency). The SDK generates
        // a request id if you do not pass one explicitly. 
        // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
        var apiContext = PayPal.Sample.Configuration.GetAPIContext();

        // ### Initialize `Payout` Object
        // Initialize a new `Payout` object with details of the batch payout to be created.
        var payout = new Payout
        {
            // #### sender_batch_header
            // Describes how the payments defined in the `items` array are to be handled.
            sender_batch_header = new PayoutSenderBatchHeader
            {
                sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                email_subject = "You have a payment"
            },
            // #### items
            // The `items` array contains the list of payout items to be included in this payout.
            // If `syncMode` is set to `true` when calling `Payout.Create()`, then the `items` array must only
            // contain **one** item.  If `syncMode` is set to `false` when calling `Payout.Create()`, then the `items`
            // array can contain more than one item.
            items = new List<PayoutItem>
                    {
                        new PayoutItem
                        {
                            recipient_type = PayoutRecipientType.EMAIL,
                            amount = new Currency
                            {
                                value =  Session["RewardAmount"].ToString(),
                                currency = "USD"
                            },
                            receiver = Session["ReceiverEmail"].ToString() ,
                            note = "Thank you.",
                            sender_item_id = "item_1"
                        }
                      
                    }
        };

        // ^ Ignore workflow code segment
        #region Track Workflow
        //this.flow.AddNewRequest("Create payout", payout);
        #endregion

        // ### Payout.Create()
        // Creates the batch payout resource.
        // `syncMode = false` indicates that this call will be performed **asynchronously**,
        // and will return a `payout_batch_id` that can be used to check the status of the payouts in the batch.
        // `syncMode = true` indicates that this call will be performed **synchronously** and will return once the payout has been processed.
        // > **NOTE**: The `items` array can only have **one** item if `syncMode` is set to `true`.
        var createdPayout = payout.Create(apiContext, false);

        // ^ Ignore workflow code segment
        #region Track Workflow
        //this.flow.RecordResponse(createdPayout);
        #endregion
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
        var date = Calendar1.SelectedDate.ToString("M/d/yyyy");
        SqlCommand location2 = new SqlCommand("SELECT Date, Eventname as Event, Location FROM Calendar WHERE CONVERT(VARCHAR(10), cast(Date as date), 101) = @Date", sc);
        location2.Parameters.AddWithValue("@Date", Convert.ToDateTime(date).Date);
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

}