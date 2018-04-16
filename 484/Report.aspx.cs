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
//using OfficeOpenXml;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Response.Redirect("test.aspx");

        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        switch (Session["BusinessEntityID"].ToString())
        {
            case "2":
                Tableau.Attributes.Add("src", "https://us-east-1.online.tableau.com/t/jingyi/views/Business2CEOReport/Story1?iframeSizedToWindow=true&:embed=y&:showAppBanner=false&:display_count=no&:showVizHome=no");
                break;
            case "4":
                Tableau.Attributes.Add("src", "https://us-east-1.online.tableau.com/t/jingyi/views/Business4CEOReport/Story1?iframeSizedToWindow=true&:embed=y&:showAppBanner=false&:display_count=no&:showVizHome=no");
                break;
        }     
    }
    protected void EmployeeSheet(object sender, EventArgs e)
    {
        string employee = "SELECT Person.NickName, Person.FirstName, Person.LastName, Person.MI, Person.PersonEmail, " +
            "Person.JobTitle, COUNT(PeerTransaction.PointsTransactionID) AS [Total Rewarded Times], MAX(Value.ValueName) AS [Top Value Rewarded] " +
            "FROM Person INNER JOIN PeerTransaction ON Person.PersonID = PeerTransaction.ReceiverID INNER JOIN Value ON " +
            "PeerTransaction.ValueID = Value.ValueID where Person.BusinessEntityID = " + Session["BusinessEntityID"].ToString() + " GROUP BY Person.NickName, Person.FirstName, Person.LastName, Person.MI, " +
            "Person.PersonEmail, Person.JobTitle, Person.Privilege HAVING (Person.Privilege = 'Employee')";



        ConvertToExcel(CreateDataTable(employee), "Employee Sheet","Employee Information");


    }
    protected void RewardProviderSheet(object sender, EventArgs e)
    {
        string RewardProvider = "SELECT        RewardProvider.ProviderName, RewardProvider.TypeOfBusiness, ProviderRewards.GiftCardAmount, COUNT(RedeemTransaction.RedeemTransactionID) AS [Rewarded Time], "+
                         "ABS(SUM(RedeemTransaction.TotalAmount)) AS[Total Redeemption Amount] "+
"FROM RedeemTransaction INNER JOIN "+
 "                        ProviderRewards ON RedeemTransaction.GiftCardID = ProviderRewards.GiftCardID INNER JOIN "+
   "                      RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID "+
"GROUP BY RewardProvider.ProviderName, RewardProvider.TypeOfBusiness, RewardProvider.Status, ProviderRewards.GiftCardAmount, ProviderRewards.BusinessEntityID "+
"HAVING(RewardProvider.Status = 1) AND(ProviderRewards.BusinessEntityID = "+ Session["BusinessEntityID"].ToString()+" )";


        ConvertToExcel(CreateDataTable(RewardProvider), "Reward Provider Sheet","Reward Provider Performance");

    }
    protected void PeerRewardHistorySheet(object sender, EventArgs e)
    {


        string RewardHistpry = "SELECT        Person_1.LastName + ISNULL(Person_1.MI, '') + Person_1.FirstName AS [Employee (Rewarder)], Person.LastName + ISNULL(Person.MI, '') + Person.FirstName AS [Employee (Receiver)], "+
                         "PeerTransaction.PointsAmount AS Points, Value.ValueName, Category.CategoryName, PeerTransaction.EventDescription, PeerTransaction.LastUpdated "+
"FROM            PeerTransaction INNER JOIN "+
 "                        Person ON PeerTransaction.ReceiverID = Person.PersonID INNER JOIN "+
  "                       Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN "+
   "                      Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID INNER JOIN "+
    "                     Category ON PeerTransaction.CategoryID = Category.CategoryID "+
"WHERE(Person.BusinessEntityID = "+ Session["BusinessEntityID"].ToString()+")";

        ConvertToExcel(CreateDataTable(RewardHistpry), "Peer Reward History Sheet","Peer Reward History");





    }

    protected void MoneyTransactionSheet(object sender, EventArgs e)
    {
        string Money = "SELECT        MoneyTransaction.MoneyTransactionID, Person.LastName + ISNULL(Person.MI, '') + Person.FirstName AS Employee, Person.JobTitle, MoneyTransaction.TransactionAmount, MoneyTransaction.TransactionType, "+
                         "MoneyTransaction.TotalAmount AS[Total Amount in the Pool] "+
"FROM MoneyTransaction INNER JOIN "+
                         "Person ON MoneyTransaction.PersonID = Person.PersonID "+
"WHERE(Person.BusinessEntityID = "+ Session["BusinessEntityID"].ToString()+")";

        ConvertToExcel(CreateDataTable(Money), "Money Transaction Sheet","Money Pool");

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


    private void ConvertToExcel(System.Data.DataTable dt, string Sheetname,String Title)
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
        HttpContext.Current.Response.Write("<Td colspan='5' style='background-color:Maroon;border:solid 1 #fff;color:#fff;'><B> "+Title+"</B>");
        HttpContext.Current.Response.Write("<TR> <font style='font-size:12.0pt;font-family:Times New Roman;background-color: #D20B0C;color:#ffffff''> <TR>");
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
            //HttpContext.Current.Response.Write("<TC> <font style='font-size:12.0pt; font-family:Times New Roman;'> <TR>");
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
           
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
           // HttpContext.Current.Response.Write("</th>");
        }
  
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





    //test


    //public void toexcel(string Filename)
    //{
    //    MemoryStream ms = DataTableToExcelXlsx();
    //    ms.WriteTo(HttpContext.Current.Response.OutputStream);
    //    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Filename);
    //    HttpContext.Current.Response.StatusCode = 200;
    //    HttpContext.Current.Response.End();
    //}


    //public static MemoryStream DataTableToExcelXlsx()
    //{
    //    MemoryStream Result = new MemoryStream();
    //    ExcelPackage pack = new ExcelPackage();
    //    ExcelWorksheet ws = pack.Workbook.Worksheets.Add("testSheet1");
    //    ExcelWorksheet ws1 = pack.Workbook.Worksheets.Add("testSheet2");
    //    ExcelWorksheet ws2 = pack.Workbook.Worksheets.Add("testSheet3");
    //    pack.SaveAs(Result);
    //    return Result;
    //}
}



