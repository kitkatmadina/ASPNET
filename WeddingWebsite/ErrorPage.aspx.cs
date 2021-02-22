using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeddingWebsite
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String code = Request.QueryString["Code"];
            switch (code)
            {
                case "IGI":
                    IncorrectGuestInfo.Visible = true;
                    break;
                case "EGL":
                    ErrorGuestLogin.Visible = true;
                    break;
                case "EAL":
                    ErrorAdminLogin.Visible = true;
                    lblEALMessage.Text = Request.QueryString["Mess"].Replace("+", " ");
                    break;
                case "GE":
                    GeneralError.Visible = true;
                    lblGEMessage.Text = Request.QueryString["Mess"].Replace("+", " ");
                    break;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            String retPage = Request.QueryString["Ret"];
            String url = "Landing.aspx"; //default
            switch (retPage)
            {
                case "L":
                    break;
                case "AL":
                    url = "AdminLogin.aspx";
                    break;
            }

            Response.Redirect(url);
        }
    }
}