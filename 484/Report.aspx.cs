using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.ClientServices;
using System.Data.SqlClient;
using System.Configuration;

//using Microsoft.Office.Interop.Excel;
using System.Web.UI.DataVisualization.Charting;

public partial class Test : System.Web.UI.Page
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
                break;
        }
    }
    protected void EmployeeSheet(object sender, EventArgs e)
    {
        string employee = "SELECT  BusinessEntity.BusinessEntityName, Person.PersonID, Person.FirstName, Person.LastName," +
            " isnull(Person.MI,'') as MI, Person.NickName, Person.PointsBalance, Person.JobTitle, " +
            "COUNT(PeerTransaction.PointsTransactionID) AS [Rewarded Times], Value.ValueName FROM BusinessEntity " +
            "INNER JOIN Person ON BusinessEntity.BusinessEntityID = Person.BusinessEntityID INNER JOIN PeerTransaction " +
            "ON Person.PersonID = PeerTransaction.ReceiverID INNER JOIN Value ON PeerTransaction.ValueID = Value.ValueID " +
            "where Person.Privilege ='Employee' and Person.BusinessEntityID = " + Session["BusinessEntityID"].ToString() +" GROUP BY BusinessEntity.BusinessEntityName, Person.PersonID, Person.FirstName, " +
            "Person.LastName, Person.MI, Person.NickName, Person.PointsBalance, Person.JobTitle, Value.ValueName";



         ConvertToExcel(CreateDataTable(employee), "Employee Sheet", ValueGraph);


    }
    protected void RewardProviderSheet(object sender, EventArgs e)
    {
        string RewardProvider = "SELECT Person.PersonID AS [Reward Provider ID], Person.JobTitle AS [Provider Name], " +
            "Person.PersonEmail AS [Provider Email], ProviderAmount.TypeOfBusiness AS [Business Type], " +
            " ProviderAmount.Amount AS [Gift Card Amount], SUM(RedeemTransaction.RedeemAmount * RedeemTransaction.RedeemQuantity) AS TotalCashOut," +
            " ProviderAmount.LastUpdate, ProviderAmount.LastUpdateBy FROM Person INNER JOIN ProviderAmount ON Person.PersonID =" +
            " ProviderAmount.ProviderID INNER JOIN RedeemTransaction ON Person.PersonID = RedeemTransaction.ProviderID WHERE " +
            "(Person.Status = 1) AND (Person.BusinessEntityID = " + Session["BusinessEntityID"].ToString() + ") GROUP BY Person.PersonID, Person.JobTitle, Person.PersonEmail," +
            " ProviderAmount.TypeOfBusiness, ProviderAmount.Amount, ProviderAmount.LastUpdate, ProviderAmount.LastUpdateBy";


        ConvertToExcel(CreateDataTable(RewardProvider), "Reward Provider Sheet", ProviderPerformance);

    }
    protected void PeerRewardHistorySheet(object sender, EventArgs e)
    {
       

        string RewardHistpry = "SELECT ISNULL(Person.FirstName, '') + ' ' + ISNULL(Person.MI, '') + ' ' + ISNULL(Person.LastName, '') AS [Receiver Name]," +
            " ISNULL(Person_1.FirstName, '') + ' ' + ISNULL(Person_1.MI, '') + ' ' + ISNULL(Person_1.LastName, '') AS [Rewarder Name]," +
            "PeerTransaction.PointsAmount, Category.CategoryName, Value.ValueName,  PeerTransaction.EventDescription, " +
            "PeerTransaction.RewardDate FROM  Person INNER JOIN PeerTransaction ON Person.PersonID = PeerTransaction.ReceiverID INNER JOIN " +
            "Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN Category ON PeerTransaction.CategoryID = Category.CategoryID INNER JOIN" +
            " Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID where Person.BusinessEntityID = " + Session["BusinessEntityID"].ToString() + "Order By PeerTransaction.RewardDate";

        ConvertToExcel(CreateDataTable(RewardHistpry), "Peer Reward History Sheet", EmployeePerformance);





    }

    protected void MoneyTransactionSheet(object sender, EventArgs e)
    {
        string Money = "SELECT MoneyTransaction.MoneyTransactionID,isnull( Person.FirstName,'')+' '+isnull( Person.MI,'')+' '+isnull( Person.LastName,'') As Name," +
            " Person.NickName, MoneyTransaction.TransactionAmount, MoneyTransaction.TotalAmount as [Amount in the Pool], " +
            " MoneyTransaction.LastUpdated as Date, MoneyTransaction.TransactionType FROM Person INNER JOIN MoneyTransaction ON Person.PersonID = MoneyTransaction.PersonID " +
            "where Person.BusinessEntityID=" + Session["BusinessEntityID"].ToString();

        ConvertToExcel(CreateDataTable(Money), "Money Transaction Sheet", MoneyTrendChart);

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


    private void ConvertToExcel(System.Data.DataTable dt, string Sheetname, Chart chartID)
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

        HttpContext.Current.Response.Write("<TR> <font style='font-size:12.0pt; font-family:Times New Roman;'> <TR>");
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
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
           
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</B>");
            //HttpContext.Current.Response.Write("</Td>");
           // HttpContext.Current.Response.Write("</th>");
        }
      
        HttpContext.Current.Response.Write("<Td>");
        HttpContext.Current.Response.Write("<Td>");
        //string headerTable = @"<img src='" + "http://localhost:49766/ChartImg.axd?i=chart_831bcb4e95154836baf7f6819c30497c_0.png&g=27969d6efd014939a108f29dcda1ae5a" + @"' \>";
        string headerTable = @"<img src='" + saveChart(chartID) + @"' \>";
        HttpContext.Current.Response.Write(headerTable);
        HttpContext.Current.Response.Write("<Td>");


        HttpContext.Current.Response.Write("<Td>");
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


    private String saveChart(Chart chartID)
    {
        // string tmpChartName = chartID.ID.ToString() + ".jpg";


        // string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
        // chartID.SaveImage(imgPath);

        string tmpChartName = chartID.ImageLocation;
        string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

        return imgPath2;


    }

    //    string tmpChartName = "ChartImage.jpg";

    //    string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;

    //    chart.SaveImage(imgPath);

    //string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

    //    Response.Clear();

    //Response.ContentType = "application/vnd.ms-excel";

    //Response.AddHeader("Content-Disposition", "attachment; filename=Chart.xls;");

    //StringWriter stringWrite = new StringWriter();

    //    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

    //    string headerTable = @"

    //";

    //    Response.Write(headerTable);

    //Response.Write(stringWrite.ToString());

    //Response.End();
}



