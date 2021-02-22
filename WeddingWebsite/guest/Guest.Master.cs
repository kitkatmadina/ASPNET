using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeddingWebsite
{

    public partial class GuestHome : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Cookies["Info"]["Role"] == null || (Request.Cookies["Info"]["Role"] != "g66541" && Request.Cookies["Info"]["Role"] != "a9599") || !validGuest())
            {
                Response.Cookies["Info"]["FirstName"] = null;
                Response.Cookies["Info"]["LastName"] = null;
                Response.Cookies["Info"]["LoggedIn"] = null;
                Response.Cookies["Info"]["Role"] = null;
                Response.Cookies["Info"].Value = null;
                Response.Redirect("../Landing.aspx");
            }
            
        }

        protected void lbNav_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            string lbName = lb.ID;
            string destination = "GuestHome.aspx";
            switch (lbName)
            {
                case ("lbHome"):
                    break;
                case ("lbProfile"):
                    destination = "Profile.aspx";
                    break;
                case ("lbGallery"):
                    destination = "Gallery.aspx";
                    break;
                case ("lbRegistry"):
                    destination = "Registry.aspx";
                    break;
                case ("lbMessage"):
                    destination = "Message.aspx";
                    break;
            }

            Response.Redirect(destination);
        }

        private bool validGuest()
        {
            if (Request.Cookies["Info"]["FirstName"] != null && Request.Cookies["Info"]["LastName"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        protected void lbEnterAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("../admin/AdminLogin.aspx");
        }

        protected void lbSignOut_Click(object sender, EventArgs e)
        {
            Response.Cookies["Info"]["FirstName"] = null;
            Response.Cookies["Info"]["LastName"] = null;
            Response.Cookies["Info"]["LoggedIn"] = null;
            Response.Cookies["Info"]["Role"] = null;
            Response.Cookies["Info"].Value = null;
            Response.Redirect("../Landing.aspx");
        }
    }
}