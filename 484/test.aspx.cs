using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

using System.IO;

using System.Data;

using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;

using System.Runtime.InteropServices;


public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
            {
                ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                GridView1.DataSource = ToDataTable(package);
                GridView1.DataBind();
            }
        }
    }

    protected static DataTable ToDataTable(ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }


    protected void Button2_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GridView1.Rows)
        {
            System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("ItemCheckBox");
            //string isSelected =(row.Cells[0].UniqueID);
            if (box.Checked)
            {
                string password = System.Web.Security.Membership.GeneratePassword(8, 6);
                string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO REWARDSYSTEM.[dbo].[Person] ([FirstName],[LastName],[MI],[PersonEmail],[JobTitle],[Privilege],[Password],[PointsBalance],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount],[Status]) VALUES" +
                   "(@FirstName,@LastName,@MI,@Email,@JobTitle, @Privilege, @Password,@PointsBalance,@LastUpdated,@LastUpdatedBy,@BusinessEntityID,0,1)";
                cmd.Parameters.AddWithValue("@FirstName", row.Cells[1].Text.ToString());
                cmd.Parameters.AddWithValue("@LastName", row.Cells[2].Text.ToString());
                cmd.Parameters.AddWithValue("@MI", row.Cells[3].Text.ToString());
                cmd.Parameters.AddWithValue("@Email", row.Cells[4].Text.ToString());
                cmd.Parameters.AddWithValue("@JobTitle", row.Cells[5].Text.ToString());
                cmd.Parameters.AddWithValue("@Privilege", row.Cells[6].Text.ToString());
                cmd.Parameters.AddWithValue("@Password", passwordHashNew);
                cmd.Parameters.AddWithValue("@PointsBalance", "0");
                cmd.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedin"].ToString());
                cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                cmd.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
                //cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
 
}