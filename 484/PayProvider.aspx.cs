// #Create Payment Using PayPal Sample
// This sample code demonstrates how you can process a 
// PayPal Account based Payment.
// API used: /v1/payments/payment
using System;
using PayPal.Api;
using System.Collections.Generic;
using PayPal.Sample.Utilities;

namespace PayPal.Sample
{
    public partial class PayProvider : BaseSamplePage
    {
        protected override void RunSample()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

            string payerId = Request.Params["PayerID"];
            if (string.IsNullOrEmpty(payerId))
            {
            
                Session["Approve"] = null;
                Session["RedeemDate"] = null;
                Session["RedeemAmount"] = null;
                Session["RedeemQuantity"] = null;
                Session["LastUpdatedBy"] = null;
                Session["LastUpdated"] = null;
                Session["ProviderID"] = null;
                Session["ProviderEmail"] = null;
            }
            else
            {
                var guid = Request.Params["guid"];

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow = Session["flow-" + guid] as RequestFlow;
                this.RegisterSampleRequestFlow();
                this.flow.RecordApproval("PayPal payment approved successfully.");
                #endregion

                // Using the information from the redirect, setup the payment to execute.
                var paymentId = Session[guid] as string;
                var paymentExecution = new PaymentExecution() { payer_id = payerId };
                var payment = new Payment() { id = paymentId };

                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.AddNewRequest("Execute PayPal payment", payment);
                #endregion

                // Execute the payment.
                var executedPayment = payment.Execute(apiContext, paymentExecution);
                // ^ Ignore workflow code segment
                #region Track Workflow
                this.flow.RecordResponse(executedPayment);
                Session["Approve"] = "yes";
                
                #endregion
                // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RunSample();
            Response.Redirect("CashOut.aspx");
        }
    }
}
