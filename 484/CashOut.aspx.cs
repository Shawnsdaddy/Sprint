using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using System.Net.Mail;
using System.Drawing;
using PayPal.Api;
using PayPal.Sample.Utilities;
using PayPal.Sample;
public partial class CashOut : BaseSamplePage
{
    int empid;
    int giverID;
    int rewardBalance;
    string userName;
    string provierName;
    double giftCardAmount;
    int providerID;
    string emailInput;
    string redemptionSelection;
    int giftcardAmount;

    protected void Page_Load(object sender, EventArgs e)
    {
        Quantitytxt.Visible = false;
        Quantitylbl.Visible = false;
        Quantitylbl.Text = "Enter Quantity";
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");

        displaygvImages();
        RedeemedGiftLabel.Visible = false;
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
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
                SqlConnection sc = new SqlConnection();
                string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.ConnectionString = connStr;
                sc.Open();
                if (Session["Approve"] != null)
                {
                    String insertredeemquery = "INSERT INTO [dbo].[RedeemTransaction] (RedeemDate, RedeemAmount, RedeemQuantity, ProviderID, PersonID,  LastUpdated, LastUpdatedBy)" +
                    "VALUES (@RedeemDate, @RedeemAmount, @RedeemQuantity, @ProviderID, @PersonID,  @LastUpdated, @LastUpdatedBy)";
                    SqlCommand insertIntoRedeemReward = new SqlCommand(insertredeemquery, sc);
                    insertIntoRedeemReward.Connection = sc;
                    insertIntoRedeemReward.Parameters.AddWithValue("@RedeemDate", Session["RedeemDate"]);
                    insertIntoRedeemReward.Parameters.AddWithValue("@RedeemAmount", Session["RedeemAmount"]);
                    insertIntoRedeemReward.Parameters.AddWithValue("@RedeemQuantity", Session["RedeemQuantity"]);
                    insertIntoRedeemReward.Parameters.AddWithValue("@ProviderID", Session["ProviderID"]);
                    insertIntoRedeemReward.Parameters.AddWithValue("@PersonID", Session["ID"]);
                    insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdatedBy", Session["LastUpdatedBy"]);
                    insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdated", Session["LastUpdated"]);
                    insertIntoRedeemReward.ExecuteNonQuery();
                    Response.Write("<script>alert('Payment Successful!')</script>");
                    Session["Approve"] = null;
                    Session["RedeemDate"] = null;
                    Session["RedeemAmount"] = null;
                    Session["RedeemQuantity"] = null;
                    Session["LastUpdatedBy"] = null;
                    Session["LastUpdated"] = null;
                    Session["ProviderID"] = null;
                    Session["ProviderEmail"] = null;
                }
                String selectBalance = "Select PointsBalance from Person where PersonID = @PersonID";
                SqlCommand balanceSelect = new SqlCommand(selectBalance, sc);
                balanceSelect.Parameters.AddWithValue("@PersonID", Session["ID"]);
                rewardBalance = Convert.ToInt32(balanceSelect.ExecuteScalar());
                Session["PointsBalance"] = rewardBalance;
                redeemlabel.Text = Session["FirstName"].ToString() + ", You have " + rewardBalance + " redeemable points!";
                sc.Close();
                break;
        }

        

    }

    protected void OnRowDataBound2(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (!Convert.IsDBNull(dr["ProfilePicture"]))
            {
                string imageUrl = "data:image/png;base64," + Convert.ToBase64String(resize((byte[])dr["ProfilePicture"]));
                (e.Row.FindControl("Image1") as System.Web.UI.WebControls.Image).ImageUrl = imageUrl;
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvImages, "Select$"
                    + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this Reward!";
            }
        }
    }

    protected void gvImages_onSelectIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvImages.Rows)
        {
            if (row.RowIndex == gvImages.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;
                String tempAmount = row.Cells[2].Text;
                tempAmount = tempAmount.Substring(1);
                giftCardAmount = Convert.ToDouble(tempAmount);
                provierName = Convert.ToString(row.Cells[1].Text);
                
                Session["ProviderName"] = provierName;
                Session["giftCardAmount"] = tempAmount;

                gvImages.Visible = true;

                redemptionSelection = "You have chosen " + provierName + " $" + giftCardAmount + " giftcard";
                RedeemedGiftLabel.Text = redemptionSelection;
                RedeemedGiftLabel.Visible = true;

                SqlConnection sc = new SqlConnection();
                string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.ConnectionString = connStr;
                sc.Open();
                String selectProviderID = "SELECT ProviderAmount.ProviderID,Person.personemail FROM ProviderAmount INNER JOIN Person ON ProviderAmount.ProviderID = Person.PersonID where Person.JobTitle = @ProviderName";
                SqlCommand ProviderSelect = new SqlCommand(selectProviderID, sc);
                ProviderSelect.Parameters.AddWithValue("@ProviderName", provierName);
                //providerID = (int)ProviderSelect.ExecuteScalar();

                SqlDataReader reader = ProviderSelect.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Session["ProviderID"] = reader["ProviderID"].ToString();
                    Session["ProviderEmail"] = reader["personemail"].ToString();
                }
                    
                sc.Close();
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";
            }
        }

        Quantitylbl.Visible = true;
        Quantitytxt.Visible = true;

    }
    public static Byte[] resize(byte[] myBytes)
    {
        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(myBytes);
        System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
        System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(40, 40, null, IntPtr.Zero);
        System.IO.MemoryStream myResult = new System.IO.MemoryStream();
        newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);  //Or whatever format you want.
        return myResult.ToArray();
    }

    public void Send_Mail(String email, String Amount)
    {
        String message = "Dear Employee: \n";
        message += "Your Tranaction of Cashing out" + Amount + " has been submited!!\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "You have redeemed a giftcard (DO NOT REPLY)", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["ProviderID"]!=null && Quantitytxt.Text.Trim()!="")
        {
            Session["quantity"] = Quantitytxt.Text.Trim();
            //int checking = Convert.ToInt32(Session["giftCardAmount"]);
            if ((rewardBalance <= 0) || (rewardBalance < ((Convert.ToDouble(Session["giftCardAmount"])) * Convert.ToDouble(Session["quantity"]))))
            {
                Response.Write("<script>alert('Insufficient Points. Do Some activities to get rewarded!')</script>");
            }
            else
            {

                RedeemReward redeemObject = new RedeemReward(DateTime.Now, Convert.ToDouble(Session["giftCardAmount"]), Convert.ToDouble(Session["quantity"]), Convert.ToDouble(Session["giftCardAmount"]), DateTime.Now, Session["loggedIn"].ToString());
                Session["RedeemDate"] = redeemObject.getDate();
                Session["RedeemAmount"] = 0 - redeemObject.getRedeemAmount();
                Session["RedeemQuantity"] = redeemObject.getRedeemQuantity();
                Session["LastUpdatedBy"] = redeemObject.getlastUpdatedBy();
                Session["LastUpdated"] = redeemObject.getlastUpdated();
                RunSample();
            }
        }
        else
        {
            Response.Write("<script>alert('Please select a provider and enter quantity')</script>");

        }


    }
    private void displaygvImages()
    {
        if (!Page.IsPostBack)
        {
            //ShowEmpImage(Session["loggedIn"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            conn.Open();
            String constr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            SqlConnection conn2 = new SqlConnection(constr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Person.JobTitle, Person.profilepicture, format (ProviderAmount.Amount, 'C', 'en-us') AS Amount FROM  Person INNER JOIN ProviderAmount ON Person.PersonID = ProviderAmount.ProviderID", conn2);
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvImages.DataSource = dt;
                gvImages.DataBind();
            }
            conn.Close();
        }
    }
    protected void RewardsGrid_ChangingPages(object sender, GridViewPageEventArgs e)
    {
        gvImages.PageIndex = e.NewPageIndex;
        this.displaygvImages();
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

        string payerId = Request.Params["PayerID"];
        if (string.IsNullOrEmpty(payerId))
        {
            // ###Items
            // Items within a transaction.
            var itemList = new ItemList()
            {
                items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Item Name",
                            currency = "USD",
                            price = (0-Convert.ToDouble(Session["RedeemAmount"])).ToString(),
                            quantity = Convert.ToDouble(Session["RedeemQuantity"]).ToString(),
                            sku = "sku"
                        }
                    }
            };

            // ###Payer
            // A resource representing a Payer that funds a payment
            // Payment Method
            // as `paypal`
            var payer = new Payer() { payment_method = "paypal" };

            // ###Redirect URLS
            // These URLs will determine how the user is redirected from PayPal once they have either approved or canceled the payment.
            var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayProvider.aspx?";
            var guid = Convert.ToString((new Random()).Next(100000));
            var redirectUrl = baseURI + "guid=" + guid;
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&cancel=true",
                return_url = redirectUrl
            };

            // ###Details
            // Let's you specify details of a payment amount.
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = ((0-Convert.ToDouble(Session["RedeemAmount"])) * Convert.ToDouble(Session["RedeemQuantity"])).ToString()
        };

            // ###Amount
            // Let's you specify a payment amount.
            var amount = new Amount()
            {
                currency = "USD",
                total = ((0 - Convert.ToDouble(Session["RedeemAmount"])) * Convert.ToDouble(Session["RedeemQuantity"])).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            var transactionList = new List<Transaction>();

            // ### Payee
            // Specify a payee with that user's email or merchant id
            // Merchant Id can be found at https://www.paypal.com/businessprofile/settings/
            Payee payee = new Payee()
            {
                email = Session["ProviderEmail"].ToString()
            };

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = Common.GetRandomInvoiceNumber(),
                amount = amount,
                item_list = itemList,
                payee = payee
            });

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            //this.flow.AddNewRequest("Create PayPal payment", payment);
            #endregion

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            // ^ Ignore workflow code segment
            #region Track Workflow
            //this.flow.RecordResponse(createdPayment);
            #endregion

            // Using the `links` provided by the `createdPayment` object, we can give the user the option to redirect to PayPal to approve the payment.
            var links = createdPayment.links.GetEnumerator();
            Session.Add(guid, createdPayment.id);  
            Session.Add("flow-" + guid, this.flow);
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    Response.Redirect(link.href);
                }
            }
        }
    }

}

