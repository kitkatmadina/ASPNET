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
    public partial class GuestHome1 : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected string firstName, lastName, hasPassword, RSVP, HasParty, PartyID, HasPlusOne, PlusOneRSVP, Email, Phone, Address, City, State, ZipCode;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validGuest())
                {
                    setNames();
                    setLabel();

                    getGuestInfo();

                    setGuestInfoMessage();
                }
                else
                {
                    Response.Redirect("../ErrorPage.aspx?Code=EGL&Ret=L");
                }
                
            }
        }

        private bool validGuest()
        {
            if(Request.Cookies["Info"] == null)
            {
                return false;
            }
            else if (Request.Cookies["Info"]["FirstName"] == null && Request.Cookies["Info"]["LastName"] == null)
            {
                return false;
            }
            else
            {
                return true;
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
        private void getGuestInfo()
        {
            DataTable dt = new DataTable();
            try
            {
                
                using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
                {
                    string query = String.Format("SELECT GuestTable.HasPassword, GuestTable.RSVP, GuestTable.HasParty, GuestTable.PartyID, GuestTable.HasPlusOne, GuestTable.PlusOneRSVP, GuestTable.Email, GuestTable.Phone, GuestTable.StreetAddress, GuestTable.City, GuestTable.State, GuestTable.ZipCode FROM GuestTable where GuestTable.FirstName = '{0}' and GuestTable.LastName = '{1}'", firstName, lastName);

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
                Response.Redirect("../ErrorPage.aspx?Code=GE&Ret=L&Mess=" + ex.Message.Replace("\n", " "));
            }

            hasPassword = dt.Rows[0]["HasPassword"].ToString();
            RSVP = dt.Rows[0]["RSVP"].ToString();
            HasParty = dt.Rows[0]["HasParty"].ToString();
            PartyID = dt.Rows[0]["PartyID"].ToString();
            HasPlusOne = dt.Rows[0]["HasPlusOne"].ToString();
            PlusOneRSVP = dt.Rows[0]["PlusOneRSVP"].ToString();
            Email = dt.Rows[0]["Email"].ToString();
            Phone = dt.Rows[0]["Phone"].ToString();
            Address = dt.Rows[0]["StreetAddress"].ToString();
            City = dt.Rows[0]["City"].ToString();
            State = dt.Rows[0]["State"].ToString();
            ZipCode = dt.Rows[0]["ZipCode"].ToString();
        }
        
        private void setGuestInfoMessage()
        {
            //Password
            if (hasPassword == "False")
            {
                newPassword.Visible = true;
            }


            //Contact Information
            if(Email == null || Phone == null || Address == null || City == null || State == null || ZipCode == null)
            {
                lblContactMessage.Text = "Looks like some of your information may be missing... ";
            }
            else
            {
                lblContactMessage.Text = "Please double check that this is all correct. ";
            }

            //PartyInformation
            if(HasParty == "True")
            {
                FillGvParty();
            }
        }

        protected void btnNewPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("GuestNewPassword.aspx");
        }

        protected void FillGvParty()
        {
            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            String query = String.Format("SELECT GuestTable.FirstName, GuestTable.LastName, GuestTable.RSVP, GuestTable.HasPlusOne, GuestTable.PlusOneRSVP from PartyTable, GuestTable where PartyTable.PartyID = GuestTable.PartyID and GuestTable.FirstName != '{0}'", firstName);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvParty.DataSource = ds;
            gvParty.DataBind();

        }

    }
}