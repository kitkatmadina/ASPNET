using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WeddingWebsite.admin
{
    public partial class ViewLogs : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            FillGvLog();
        }
        protected void FillGvLog()
        {
            string sql = "SELECT DateTime as Date, EventInformation as [Event Information], Name from EventLogs order by EventID desc";

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvLog.DataSource = ds;
            gvLog.DataBind();
        }
    }
}