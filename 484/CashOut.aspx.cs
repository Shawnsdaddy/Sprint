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
    private string type;

    protected void Page_Load(object sender, EventArgs e)
    {

        Quantitytxt.Visible = false;
        Quantitylbl.Visible = false;
        Quantitylbl.Text = "Enter Quantity";
        RedeemedGiftLabel.Visible = false;
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            
            if (Session["Approve"] != null)
            {
                //    String insertredeemquery = "INSERT INTO [dbo].[RedeemTransaction] (RedeemDate,RedeemQuantity, GiftCardID, PersonID,  LastUpdated, LastUpdatedBy, TotalAmount)" +
                //    "VALUES (@RedeemDate, @RedeemQuantity, @GiftcardID, @PersonID,  @LastUpdated, @LastUpdatedBy, @TotalAmount)";
                //    SqlCommand insertIntoRedeemReward = new SqlCommand(insertredeemquery, sc);
                //    insertIntoRedeemReward.Connection = sc;
                //    insertIntoRedeemReward.Parameters.AddWithValue("@RedeemDate", Session["RedeemDate"]);
                //    insertIntoRedeemReward.Parameters.AddWithValue("@RedeemQuantity", Session["RedeemQuantity"]);
                //    insertIntoRedeemReward.Parameters.AddWithValue("@GiftcardID", Session["giftCardID"]);
                //    insertIntoRedeemReward.Parameters.AddWithValue("@PersonID", Session["ID"]);
                //    insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdatedBy", Session["LastUpdatedBy"]);
                //    insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdated", Session["LastUpdated"]);
                //    insertIntoRedeemReward.Parameters.AddWithValue("@TotalAmount", Convert.ToDouble(Session["RedeemAmount"]) * Convert.ToInt32(Session["RedeemQuantity"]));
                //    insertIntoRedeemReward.ExecuteNonQuery();
                Response.Write("<script>alert('Payment Successful!')</script>");
                //    Send_Mail(Session["E-mail"].ToString(), ((0 - Convert.ToDouble(Session["RedeemAmount"])) * Convert.ToDouble(Session["RedeemQuantity"])).ToString());
                Session["Approve"] = null;

            }
            clearSession();
            displaygvImages();
            if (!IsPostBack)
            {
                Session["giftCardID"] = null;
            }
        }

    }
   
    protected void OnRowDataBound2(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (!Convert.IsDBNull(dr["GiftCardPicture"]))
            {
                string imageUrl = "data:image/png;base64," + Convert.ToBase64String(resize((byte[])dr["GiftCardPicture"]));
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
                Session["giftCardID"] = gvImages.SelectedValue;
                gvImages.Visible = true;

                redemptionSelection = "You have chosen " + provierName + " $" + giftCardAmount + " giftcard";
                RedeemedGiftLabel.Text = redemptionSelection;
                RedeemedGiftLabel.Visible = true;

                SqlConnection sc = new SqlConnection();
                string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.ConnectionString = connStr;
                sc.Open();
                String selectProviderID = "SELECT [ProviderID],[ProviderEmail] FROM [RewardProvider] where [ProviderName] = @ProviderName";
                SqlCommand ProviderSelect = new SqlCommand(selectProviderID, sc);
                ProviderSelect.Parameters.AddWithValue("@ProviderName", provierName);
                //providerID = (int)ProviderSelect.ExecuteScalar();

                SqlDataReader reader = ProviderSelect.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Session["ProviderID"] = reader["ProviderID"].ToString();
                    Session["ReceiverEmail"] = reader["ProviderEmail"].ToString();
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

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    type = "person";
    //    if (Session["giftCardID"] != null && Quantitytxt.Text.Trim() != "" && Session["ReceiverEmail"] != null)
    //    {
    //        Session["quantity"] = Quantitytxt.Text.Trim();
    //        if ((rewardBalance <= 0) || (rewardBalance < ((Convert.ToDouble(Session["giftCardAmount"])) * Convert.ToDouble(Session["quantity"]))))
    //        {
    //            Response.Write("<script>alert('Insufficient Points. Do Some activities to get rewarded!')</script>");
    //        }
    //        else
    //        {
    //            RedeemReward redeemObject = new RedeemReward(DateTime.Now, Convert.ToDouble(Session["giftCardAmount"]), Convert.ToDouble(Session["quantity"]), Convert.ToDouble(Session["giftCardAmount"]), DateTime.Now, Session["loggedIn"].ToString());
    //            Session["RedeemDate"] = redeemObject.getDate();
    //            Session["RedeemAmount"] = 0 - redeemObject.getRedeemAmount();
    //            Session["RedeemQuantity"] = redeemObject.getRedeemQuantity();
    //            Session["LastUpdatedBy"] = redeemObject.getlastUpdatedBy();
    //            Session["LastUpdated"] = redeemObject.getlastUpdated();
    //            Session["totalAmount"] = redeemObject.getTransactionAmount();
    //            RunSample();
    //            clearSession();
    //            Session["giftCardID"] = null;
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("<script>alert('Please select a provider and enter quantity')</script>");
    //    }
    //}
    
    protected void RewardsGrid_ChangingPages(object sender, GridViewPageEventArgs e)
    {
        gvImages.PageIndex = e.NewPageIndex;
        this.displaygvImages();
    }
   
    //protected void withdrawCommit_Click(object sender, EventArgs e)
    //{
    //    type = "Company";
    //    Session["totalAmount"] = txtWithDraw.Text.Trim();
    //    if ((rewardBalance <= 0) || (rewardBalance < (Convert.ToDouble(Session["totalAmount"]))))
    //    {
    //        Response.Write("<script>alert('Insufficient Points. Do Some activities to get rewarded!')</script>");
    //    }
    //    else
    //    {
    //        Session["ReceiverEmail"] = Session["E-mail"];
    //        SqlConnection sc = new SqlConnection();
    //        string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
    //        sc.ConnectionString = connStr;
    //        sc.Open();
    //        String insertredeemquery = "INSERT INTO [dbo].[RedeemTransaction] (RedeemDate,  RedeemQuantity, GiftCardID, PersonID,  LastUpdated, LastUpdatedBy, TotalAmount)" +
    //        "VALUES (@RedeemDate, @RedeemQuantity, @GiftcardID, @PersonID,  @LastUpdated, @LastUpdatedBy, @TotalAmount)";
    //        SqlCommand insertIntoRedeemReward = new SqlCommand(insertredeemquery, sc);
    //        insertIntoRedeemReward.Connection = sc;
    //        insertIntoRedeemReward.Parameters.AddWithValue("@RedeemDate", DateTime.Now);
    //        insertIntoRedeemReward.Parameters.AddWithValue("@RedeemQuantity", 1);
    //        insertIntoRedeemReward.Parameters.AddWithValue("@GiftcardID", 0);
    //        insertIntoRedeemReward.Parameters.AddWithValue("@PersonID", Session["ID"]);
    //        insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdatedBy", Session["loggedIn"]);
    //        insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
    //        insertIntoRedeemReward.Parameters.AddWithValue("@TotalAmount", 0 - Convert.ToDouble(Session["totalAmount"]));
    //        insertIntoRedeemReward.ExecuteNonQuery();           
    //        RunSample();
    //        clearSession();
    //        Session["giftCardID"] = null;
    //    }
    //}

    protected void btnCompany_Click(object sender, EventArgs e)
    {
        type = "Company";
        if (Session["giftCardID"] != null && Quantitytxt.Text.Trim() != "" && Session["ReceiverEmail"] != null)
        {

            Session["quantity"] = Quantitytxt.Text.Trim();

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
                Session["totalAmount"] = redeemObject.getTransactionAmount();
                SqlConnection sc = new SqlConnection();
                string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.ConnectionString = connStr;
                sc.Open();
                String insertredeemquery = "INSERT INTO [dbo].[RedeemTransaction] (RedeemDate,RedeemQuantity, GiftCardID, PersonID,  LastUpdated, LastUpdatedBy, TotalAmount)" +
                "VALUES (@RedeemDate, @RedeemQuantity, @GiftcardID, @PersonID,  @LastUpdated, @LastUpdatedBy, @TotalAmount)";
                SqlCommand insertIntoRedeemReward = new SqlCommand(insertredeemquery, sc);
                insertIntoRedeemReward.Connection = sc;
                insertIntoRedeemReward.Parameters.AddWithValue("@RedeemDate", Session["RedeemDate"]);
                insertIntoRedeemReward.Parameters.AddWithValue("@RedeemQuantity", Session["RedeemQuantity"]);
                insertIntoRedeemReward.Parameters.AddWithValue("@GiftcardID", Session["giftCardID"]);
                insertIntoRedeemReward.Parameters.AddWithValue("@PersonID", Session["ID"]);
                insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdatedBy", Session["LastUpdatedBy"]);
                insertIntoRedeemReward.Parameters.AddWithValue("@LastUpdated", Session["LastUpdated"]);
                insertIntoRedeemReward.Parameters.AddWithValue("@TotalAmount", Convert.ToDouble(Session["RedeemAmount"]) * Convert.ToInt32(Session["RedeemQuantity"]));
                insertIntoRedeemReward.ExecuteNonQuery();
                try
                {
                    RunSample();
                    Response.Write("<script>alert('Redeem Successful!')</script>");
                }
                catch
                {
                    Response.Write("<script>alert('Redeem failed, Please Contact Company Officer')</script>");
                }             
                clearSession();
                Session["giftCardID"] = null;
            }
        }
        else
        {
            Response.Write("<script>alert('Please select a provider and enter quantity')</script>");
        }
    }

    protected override void RunSample()
    {

        if (type == "person")
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // a request id if you do not pass one explicitly. 
            // (that ensures idempotency). The SDK generates
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
                    subtotal = ((0 - Convert.ToDouble(Session["RedeemAmount"])) * Convert.ToDouble(Session["RedeemQuantity"])).ToString()
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
                    email = Session["ReceiverEmail"].ToString()
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
        else
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
                                value =  Session["totalAmount"].ToString(),
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

    }

    public static Byte[] resize(byte[] myBytes)
    {
        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(myBytes);
        System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
        System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(40, 40, null, IntPtr.Zero);
        System.IO.MemoryStream myResult = new System.IO.MemoryStream();
        newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);
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
    protected void clearSession()
    {
        SqlConnection sc = new SqlConnection();
        string connStr = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.ConnectionString = connStr;
        sc.Open();
        String selectBalance = "Select PointsBalance from Person where PersonID = @PersonID";
        SqlCommand balanceSelect = new SqlCommand(selectBalance, sc);
        balanceSelect.Parameters.AddWithValue("@PersonID", Session["ID"]);
        rewardBalance = Convert.ToInt32(balanceSelect.ExecuteScalar());
        Session["PointsBalance"] = rewardBalance;
        redeemlabel.Text = Session["FirstName"].ToString() + ", You have " + rewardBalance + " redeemable points!";
        sc.Close();
        Session["RedeemDate"] = null;
        Session["RedeemAmount"] = null;
        Session["RedeemQuantity"] = null;
        Session["LastUpdatedBy"] = null;
        Session["LastUpdated"] = null;
        Session["ProviderID"] = null;
        Session["ProviderEmail"] = null;
        gvImages.SelectedIndex = -1;
        Session["ProviderName"] = null;
    }
    private void displaygvImages()
    {
        //ShowEmpImage(Session["loggedIn"].ToString());
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        conn.Open();
        SqlCommand fill = new SqlCommand();
        fill.Connection = conn;
        fill.CommandText = "SELECT RewardProvider.ProviderName, ProviderRewards.GiftCardPicture, format (ProviderRewards.GiftCardAmount, 'C', 'en-us') AS GiftCardAmount, GiftCardID FROM ProviderRewards INNER JOIN RewardProvider ON ProviderRewards.ProviderID = RewardProvider.ProviderID WHERE ProviderRewards.BusinessEntityID = @BusinessEntityID and  ProviderRewards.Status = 'Approved'";
        fill.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"].ToString());
        SqlDataAdapter sda = new SqlDataAdapter(fill);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        gvImages.DataSource = dt;
        gvImages.DataBind();

        conn.Close();
        gvImages.SelectedIndex = -1;
    }



}

