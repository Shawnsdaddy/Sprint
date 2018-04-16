using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

public partial class UserDashboard : System.Web.UI.Page
{
    // private int giverID;

    private string userName;
    private int id;
    private int businessid;
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;

        sc.ConnectionString = connStr;
        sc.Open();


        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            userName = Session["loggedIn"].ToString();
            displayRedeemGrid();
            displayActivitiesGrid();
            //display current balance
            try
            {
                int rewardBalance = 0;
                String selectBalance = "Select PointsBalance from Person where PersonID = @PersonID";
                SqlCommand balanceSelect = new SqlCommand(selectBalance, sc);
                balanceSelect.Parameters.AddWithValue("@PersonID", Session["ID"]);
                rewardBalance = Convert.ToInt32(balanceSelect.ExecuteScalar());
                balance.Text = "$" + rewardBalance.ToString();

                int activityquantity = 0;
                String selectCount = "Select COUNT(PointsTransactionID) FROM PeerTransaction WHERE ReceiverID =" + Session["ID"];
                SqlCommand countSelect = new SqlCommand(selectCount, sc);
                activityquantity = Convert.ToInt32(countSelect.ExecuteScalar());

                if (activityquantity > 0)
                {
                    actquantity.Text = activityquantity.ToString();
                }
            }
            catch (Exception)
            {
            }
        }     
    }

    private void displayRedeemGrid()
    {
        if (Session["ID"] != null)
        {
            id = Convert.ToInt32(Session["ID"]);
            businessid = Convert.ToInt32(Session["BusinessEntityID"]);
        }
        if (Session["loggedIn"] != null)
        {
            userName = Session["loggedIn"].ToString();
        }
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        //sc.ConnectionString = connStr;
        sc.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT CONVERT(VARCHAR(10), cast(RedeemTransaction.RedeemDate as date), 101) as Date,  format(ProviderRewards.GiftCardAmount, 'C', 'en-us') as Amount, RedeemTransaction.RedeemQuantity as Quantity, RewardProvider.ProviderName as Company FROM RedeemTransaction INNER JOIN ProviderRewards ON RedeemTransaction.GiftCardID= ProviderRewards.GiftCardID INNER JOIN RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID WHERE PersonID =  @PersonID AND BusinessEntityID = @BusinessEntityID order by RedeemTransaction.RedeemDate desc";
        //SqlCommand rewardsGrid = new SqlCommand(MyRedeem, sc);
        cmd.Parameters.AddWithValue("@PersonID", id);
        cmd.Parameters.AddWithValue("@BusinessEntityID", businessid);
        //rewardsGrid.Connection = sc;
        //SqlDataReader reader2 = rewardsGrid.ExecuteReader();
        cmd.Connection = sc;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        RedeemedGrid.DataSource = ds;
        RedeemedGrid.DataBind();

    }

    private void displayActivitiesGrid()
    {
        if (Session["ID"] != null)
        {
            id = Convert.ToInt32(Session["ID"]);
        }
        if (Session["loggedIn"] != null)
        {
            userName = Session["loggedIn"].ToString();
        }
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        //sc.ConnectionString = connStr;
        sc.Open();
        

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT CONVERT(VARCHAR(10), cast(PeerTransaction.RewardDate as date), 101) as Date,  Value.ValueName as Value, Category.CategoryName as Category, cast(PeerTransaction.PointsAmount as int) as Points, PeerTransaction.EventDescription as Details FROM Value INNER JOIN PeerTransaction ON Value.ValueID = PeerTransaction.ValueID INNER JOIN Category ON PeerTransaction.CategoryID = Category.CategoryID where ReceiverID =@PersonID order by PeerTransaction.RewardDate desc";
        //SqlCommand rewardsGrid = new SqlCommand(MyRedeem, sc);
        cmd.Parameters.AddWithValue("@PersonID", id);
        //rewardsGrid.Connection = sc;
        //SqlDataReader reader2 = rewardsGrid.ExecuteReader();
        cmd.Connection = sc;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        ActivitiesGrid.DataSource = ds;
        ActivitiesGrid.DataBind();

    }


    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RedeemedGrid.PageIndex = e.NewPageIndex;
        this.displayRedeemGrid();
    }

    protected void ActivitiesGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ActivitiesGrid.PageIndex = e.NewPageIndex;
        this.displayActivitiesGrid();
    }

}