using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace WeddingWebsite
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!validateAdmin())
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
            string destination = "AdminHome.aspx";
            switch (lbName)
            {
                case ("lbHome"):
                    break;
                case ("lbGuests"):
                    destination = "EditGuests.aspx";
                    break;
                case ("lbParties"):
                    destination = "EditParties.aspx";
                    break;
                case ("lbGallery"):
                    destination = "EditGallery.aspx";
                    break;
            }

            Response.Redirect(destination);
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

        protected void lbEnterGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("../guest/GuestHome.aspx");
        }

        protected bool validateAdmin()
        {
            if(Request.Cookies["Info"]["FirstName"] == null || Request.Cookies["Info"]["LastName"] == null || Request.Cookies["Info"]["Role"] == null)
            {
                return false;
            }
            else 
            {
                string first = Request.Cookies["Info"]["FirstName"];
                string last = Request.Cookies["Info"]["LastName"];
                string sql = String.Format("SELECT AdminID from AdminTable, GuestTable where AdminTable.GuestID = GuestTable.GuestID and GuestTable.FirstName = '{0}' and GuestTable.LastName = '{1}'", first, last);

                SqlConnection conn = new SqlConnection(CONNECTIONINFO);
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                var id = cmd.ExecuteScalar();
                conn.Close();
                if (String.IsNullOrEmpty((string)id))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        
    }
}