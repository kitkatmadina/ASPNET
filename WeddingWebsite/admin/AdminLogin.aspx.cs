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
    public partial class AdminLogin : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Cookies["Info"]["LoggedIn"] != null && Request.Cookies["Info"]["Role"] != null)
            {
                redirectIfLoggedIn();
            }
        }

        private void redirectIfLoggedIn()
        {
            if (Request.Cookies["Info"]["LoggedIn"] == "true" && Request.Cookies["Info"]["Role"] == "a9599")
            {
                if (Request.Cookies["Info"]["FirstName"] != null && Request.Cookies["Info"]["LastName"] != null)
                {
                    Response.Redirect("AdminHome.aspx");
                }
            }
        }
        protected void lbEnterGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("../guest/GuestHome.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtUserName.Text;
            String lastname = txtLastName.Text;
            String pwd = txtPassword.Text;

            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(lastname) && !String.IsNullOrEmpty(pwd))
            {
                    string query = String.Format("SELECT AdminTable.AdminID, GuestTable.LastName, GuestTable.HasPassword, GuestTable.Password, GuestTable.FirstName FROM AdminTable, GuestTable WHERE AdminTable.GuestID = GuestTable.GuestID and AdminTable.AdminID = '{0}'", username);

                    DataTable dt = new DataTable();

                    using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
                    {

                        SqlCommand cmd = new SqlCommand(query, conn);

                        conn.Open();

                        //create data adapter
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //query database annd return result to datatable
                        da.Fill(dt);
                        conn.Close();
                        da.Dispose();
                    }

                    //check to make sure there are no duplicate accounts
                    if(dt.Rows.Count > 1)
                    {
                        Response.Redirect("ErrorPage.aspx?Code=EAL&Ret=AL&Mess=There+Exists+More+Than+One+User+with+That+ID");
                    }

                    //validate login
                    String dt_username = dt.Rows[0][0].ToString(); //AdminID
                    String dt_lastname = dt.Rows[0][1].ToString(); //LastName
                    String haspassword = dt.Rows[0][2].ToString(); //HasPassword 
                    String dt_password = dt.Rows[0][3].ToString(); //Password
                    String dt_firstname = dt.Rows[0][4].ToString(); //FirstName

                    if(haspassword == "True")
                    {
                        if (username == dt_username && lastname == dt_lastname &&  pwd == dt_password)
                        {
                        //Valid Login
                            
                            Response.Cookies["Info"]["FirstName"] = dt_firstname;
                            Response.Cookies["Info"]["LastName"] = dt_lastname;
                            Response.Cookies["Info"]["LoggedIn"] = "true";
                            Response.Cookies["Info"]["Role"] = "a9599"; //admin
                            Response.Cookies["Info"].Expires = DateTime.Today.AddDays(60);
                            Response.Redirect("AdminHome.aspx");

                        }
                    }
                    else
                    {
                        Response.Redirect("ErrorPage.aspx?Code=EAL&Ret=AL&Mess=There+Is+No+Password+Associated+With+This+Account");
                    }
            }
        }
    }
}