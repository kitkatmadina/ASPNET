using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WeddingWebsite
{
    public partial class Landing : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Info"] != null)
            {
                if (Request.Cookies["Info"]["LoggedIn"] != null && Request.Cookies["Info"]["FirstName"] != null && Request.Cookies["Info"]["LastName"] != null)
                { 
                    if (Request.Cookies["Info"]["Role"] == "g66541")
                    {
                        Response.Redirect("guest/GuestHome.aspx");
                    }
                    else if (Request.Cookies["Info"]["Role"] == "a9599")
                    {
                        Response.Redirect("admin/AdminHome.aspx");
                    }
                }
            }
            else
            {
                Response.Cookies["Info"].Expires = DateTime.Today.AddDays(60);


            }
        }

        protected void lbEnterAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin/AdminLogin.aspx");
        }

        protected void btnEnterGuest_Click(object sender, EventArgs e)
        {
            //set cookies
            String first = txtFirstName.Text.Trim();
            String last = txtLastName.Text.Trim();
            if(!String.IsNullOrEmpty(first) && !String.IsNullOrEmpty(last))
            {
                if(first != "FirstName" && last != "LastName")
                {
                    if(validateGuestInfo(first, last))
                    {
                        if (HasPassword(first, last))
                        {
                            Response.Cookies["Info"]["FirstName"] = txtFirstName.Text;
                            Response.Cookies["Info"]["LastName"] = txtLastName.Text;
                            Response.Cookies["Info"].Expires = DateTime.Now.AddDays(60);
                            Response.Redirect("GuestLogin.aspx");
                        }
                        else
                        {
                            Response.Cookies["Info"]["FirstName"] = txtFirstName.Text;
                            Response.Cookies["Info"]["LastName"] = txtLastName.Text;
                            Response.Cookies["Info"]["LoggedIn"] = "true";
                            Response.Cookies["Info"]["Role"] = "g66541"; //guest
                            Response.Cookies["Info"].Expires = DateTime.Now.AddDays(60);
                            Response.Redirect("guest/GuestHome.aspx");

                        }
                    }
                    else
                    {

                        Response.Redirect("ErrorPage.aspx?Code=IGI&Ret=L");
                    }
                }
            }

            

        }

        private bool HasPassword(String FirstName, String LastName)
        {

            /// Function takes in two strings, String FirstName and String LastName
            /// Checks it against the Database Guest Registry to check if they have a password
            try
            {
                
                using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
                {
                    string query = String.Format("SELECT HasPassword from GuestTable where FirstName = '{0}' and LastName = '{1}'", FirstName, LastName);

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    var ret = cmd.ExecuteScalar();

                    conn.Close();


                    if ((Boolean)ret == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx?Code=GE&Ret=L&Mess=" + ex.Message.Replace("\n", " "));
            }

            return false;
            
        }

        private bool validateGuestInfo(String FirstName, String LastName)
        {
            /// Function takes in two strings, String FirstName and String LastName
            /// Checks it against the Database Guest Registry to make sure they are on the list
            /// Returns true if they are on the list
            /// else, returns false
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
                {
                    string query = String.Format("SELECT GuestID from GuestTable where FirstName = '{0}' and LastName = '{1}'", FirstName, LastName);

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    var ret = cmd.ExecuteScalar();

                    conn.Close();


                    if(ret != null)
                    {
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("ErrorPage.aspx?Code=GE&Ret=L&Mess=" + ex.Message.Replace("\n", " "));
            }

            return false;
        }
    }
}