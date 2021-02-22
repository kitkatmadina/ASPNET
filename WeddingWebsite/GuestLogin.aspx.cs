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
    public partial class GuestLogin : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected static string firstName, lastName;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setNames();
                setLabel();
            }
        }


        private void setNames()
        {
            firstName = Request.Cookies["Info"]["FirstName"];
            lastName = Request.Cookies["Info"]["LastName"];
        }
        private void setLabel()
        {
            String welcomeText = String.Format("Welcome, {0} {1}!", firstName, lastName);

            lblWelcomeGuest.Text = welcomeText;
        }

        protected void lbEnterAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin/AdminLogin.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            


            if (validPassword())
            {
                Response.Cookies["Info"]["FirstName"] = firstName;
                Response.Cookies["Info"]["LastName"] = lastName;
                Response.Cookies["Info"]["LoggedIn"] = "true";
                Response.Cookies["Info"]["Role"] = "g66541"; //guest
                Response.Cookies["Info"].Expires = DateTime.Now.AddDays(60);
                Response.Redirect("guest/GuestHome.aspx");
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Incorrect password. Contact Kirsten if you need to reset your password.');", true);
            }
        }

        protected bool validPassword()
        {
            string password = "";
            setNames();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
                {
                    string query = String.Format("SELECT Password from GuestTable where FirstName = '{0}' and LastName = '{1}'", firstName, lastName);

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    var ret = cmd.ExecuteScalar();

                    conn.Close();

                    password = ret.ToString();
                }

                if (String.Equals(txtPassword.Text, password))
                {
                    return true;
                }
                

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return false;
            
        }


    }
}