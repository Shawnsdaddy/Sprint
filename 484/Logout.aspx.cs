using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loginScreen : System.Web.UI.Page
{
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");

    }

    protected void backToLoginButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}