using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WeddingWebsite.guest
{
    public partial class GuestNewPassword : System.Web.UI.Page
    {
        protected string firstname, lastname;
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            setName();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validatePassword())
            {
                string password = txtPassword1.Text.Trim();
                setPassword(password);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Passwords do not match')", true);
            }
        }

        protected void setName()
        {
            firstname = Request.Cookies["Info"]["FirstName"];
            lastname = Request.Cookies["Info"]["LastName"];
        }

        protected bool validatePassword()
        {
            if(txtPassword1.Text.Trim() == txtPassword2.Text.Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void setPassword(string password)
        {
            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            string query = String.Format("UPDATE GuestTable set Password = '{0}', HasPassword = 'True' where FirstName = '{1}' and LastName = '{2}'", password, firstname, lastname);
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }
    }
}