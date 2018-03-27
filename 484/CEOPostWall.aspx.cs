using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class CEOLogin : System.Web.UI.Page
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
        else
        {
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
                    rewardpool();
                    //updatecombox();
                    if (!IsPostBack)
                    {
                        LatestUpdates();
                    }
                    break;
            }
            

        }
            
    }

    PagedDataSource pds = new PagedDataSource();
    private void LatestUpdates()
    {

        //if (Session["Name"] != null)
        //{
        //    lblName.Text = Session["Name"].ToString();
        //}

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

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;

        insert.CommandText = "SELECT [TotalAmount] FROM [MoneyTransaction] where MoneyTransactionID=(select max(MoneyTransactionID) from MoneyTransaction)";
        SqlDataReader reader = insert.ExecuteReader();

        if (Convert.ToInt32(txtFrontLoad.Text) <= 0)
        {
            Response.Write("<script>alert('Please Enter Positive Amount! ')</script>");
            txtFrontLoad.Text = String.Empty;
        }
        else
        {
            if (reader.HasRows)
            {
                reader.Read();
                int totalPoints = Convert.ToInt32(reader["TotalAmount"]);
                int transactionAmount = Convert.ToInt32(txtFrontLoad.Text);
                reader.Close();


                MoneyTransaction newTransaction = new MoneyTransaction(totalPoints, DateTime.Today.ToShortDateString(), transactionAmount, DateTime.Today.ToShortDateString(), Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]), "Fund");
                insert.CommandText = "INSERT INTO [dbo].[MoneyTransaction] ([TotalAmount],[TransactionAmount],[LastUpdated],[LastUpdatedBy],[PersonID],[TransactionType])" +
                "VALUES (@TotalAmount,@TransactionAmount,@LastUpdated,@LastUpdatedBy,@PersonID,@Type)";
                insert.Parameters.AddWithValue("@TotalAmount", totalPoints + transactionAmount);
                //insert.Parameters.AddWithValue("@Date", newTransaction.getDate());
                insert.Parameters.AddWithValue("@TransactionAmount", transactionAmount);
                insert.Parameters.AddWithValue("@LastUpdated", newTransaction.getLUD());
                insert.Parameters.AddWithValue("@LastUpdatedBy", newTransaction.getLUDB());
                insert.Parameters.AddWithValue("@PersonID", newTransaction.getPersonID());
                insert.Parameters.AddWithValue("@Type", newTransaction.getTransactionType());
                insert.ExecuteNonQuery();

                sc.Close();
                rewardpool();
            }
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
        lblPoints.Text = "Pool Balance: "+Session["PointsBalance"].ToString();
    }
}