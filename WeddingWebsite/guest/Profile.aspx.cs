using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WeddingWebsite.guest
{
    public partial class Profile : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected static string firstName, lastName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setNames();
                getContactInfo();
            }
        }

        /* CONTROL EVENTS*/
        protected void btnSaveContact_Click(object sender, EventArgs e)
        {
            if (validateContactInfo())
            {
                AddEventLog("Contact Info Was Changed", firstName + lastName);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Your Information Has Been Saved')", true);
            }

        }


        /* SETUP METHODS */
        private void getContactInfo()
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
                {
                    string query = String.Format("SELECT GuestTable.Email, GuestTable.Phone, GuestTable.StreetAddress, GuestTable.City, GuestTable.State, GuestTable.ZipCode FROM GuestTable where GuestTable.FirstName = '{0}' and GuestTable.LastName = '{1}'", firstName, lastName);

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(dt);

                    conn.Close();

                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("ErrorPage.aspx?Code=GE&Ret=L&Mess=" + ex.Message.Replace("\n", " "));
            }
            string Email, Phone, Address, City, State, ZipCode;
            Email = dt.Rows[0]["Email"].ToString();
            Phone = dt.Rows[0]["Phone"].ToString();
            Address = dt.Rows[0]["StreetAddress"].ToString();
            City = dt.Rows[0]["City"].ToString();
            State = dt.Rows[0]["State"].ToString();
            ZipCode = dt.Rows[0]["ZipCode"].ToString();

            txtEmail.Text = Email;
            txtPhone1.Text = Phone.Substring(0,3);
            txtPhone2.Text = Phone.Substring(3,3);
            txtPhone3.Text = Phone.Substring(6,4);
            txtStreetAddress.Text = Address;
            txtCity.Text = City;
            txtState.Text = State;
            txtZipCode.Text = ZipCode;

        }
        private void setNames()
        {
            firstName = Request.Cookies["Info"]["FirstName"];
            lastName = Request.Cookies["Info"]["LastName"];
        }

        /* VALIDATION METHODS */
        private bool validateContactInfo()
        {
            string phone1 = txtPhone1.Text.Trim();
            string phone2 = txtPhone2.Text.Trim();
            string phone3 = txtPhone3.Text.Trim();
            string email = txtEmail.Text.Trim();

            return validatePhoneNumber(phone1, phone2, phone3) && validateEmail(email);

        }
        private bool validatePhoneNumber(string phone1, string phone2, string phone3)
        {
            if(phone1.Length != 3 || phone2.Length != 3 || phone3.Length != 4)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Valid Phone Number1')", true);
                return false;
            }
            
            foreach(char c in phone1+phone2+phone3)
            {
                if (!Char.IsDigit(c))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Valid Phone Number2')", true);
                    return false;
                }
            }

            return true;
        }
        private bool validateEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*(([.][a-z|0-9]+([_][a-z|0-9]+)*)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            Match match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
                return true;
            else {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Valid Email Address')", true);
                return false;
            }
        }

        private void AddEventLog(String info, String name)
        {
            DateTime dt = DateTime.Now;

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            string query = String.Format("INSERT INTO EventLogs (EventInformation, Name, DateTime) Values ('{0}', '{1}', '{2}')", info, name, dt);

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}