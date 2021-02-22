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
    public partial class AdminHome : System.Web.UI.Page
    {
        protected static string firstName, lastName;
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            setNames();
            setLabel();
            if (!Page.IsPostBack)
            {
                FillGvStats();
                FillGvToDoList();
                FillGvCompleted();
                FillGvLog();
            }

        }
        private void setNames()
        {
            firstName = Request.Cookies["Info"]["FirstName"];
            lastName = Request.Cookies["Info"]["LastName"];
        }
        private void setLabel()
        {
            String welcomeText = String.Format("Welcome, {0} {1}! (Administrator)", firstName, lastName);

            lblWelcomeAdmin.Text = welcomeText;
        }

        protected void btnNewTask_Click(object sender, EventArgs e)
        {
            string name = firstName + " " + lastName;
            string priority = ddlPriority.SelectedValue;
            string task = txtNewTask.Text.Trim();
            DateTime dt = DateTime.Now;

            string sql = String.Format("INSERT into ToDoList (ItemText, IsCompleted, AddedBy, DateAdded, Priority) Values('{0}', 0, '{1}', '{2}', '{3}')", task, name, dt, priority);
            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            AddEventLog("Task Added to To Do List", name);
            FillGvToDoList();
        }

        protected void FillGvStats()
        {
            string sql = "SELECT 'Total Guests' as Title, Count(*) as Stat, 1 as Sort, 1 as Bold from GuestTable Union SELECT 'Guest RSVP Yes' as Title, Count(*) as Stat, 2 as Sort, 0 as Bold from GuestTable where GuestTable.RSVP = 1 Union SELECT 'Guest RSVP No' as Title, Count(*) as Stat, 3 as Sort, 0 as Bold from GuestTable where GuestTable.RSVP = 0 Union SELECT 'Guest No Response' as Title, Count(*) as Stat, 4 as Sort, 0 as Bold from GuestTable where GuestTable.RSVP is null Union SELECT 'Total Plus Ones' as Title, Count(*) as Stat, 5 as Sort, 1 as Bold from GuestTable where GuestTable.HasPlusOne = 1 Union SELECT '+1 RSVP Yes' as Title, Count(*) as Stat, 6 as Sort, 0 as Bold from GuestTable where GuestTable.HasPlusOne = 1 and GuestTable.PlusOneRSVP = 1 Union SELECT '+1 RSVP No' as Title, Count(*) as Stat, 7 as Sort, 0 as Bold from GuestTable where GuestTable.HasPlusOne = 1 and GuestTable.PlusOneRSVP = 1 Union SELECT '+1 No Response' as Title, Count(*) as Stat, 9 as Sort, 0 as Bold from GuestTable where GuestTable.HasPlusOne = 1 and  GuestTable.PlusOneRSVP is null order by Sort";

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvStatistics.DataSource = ds;
            gvStatistics.DataBind();
        }

        protected void cbMarkDone_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            GridViewRow gvr = cb.Parent.Parent as GridViewRow;
            int index = gvr.RowIndex;

            string id = gvToDoList.DataKeys[index]["ItemID"].ToString();

            DateTime dt = DateTime.Now;

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            string query = String.Format("UPDATE ToDoList set IsCompleted = 1, DateCompleted = '{0}' where ItemID = '{1}'", dt, id);

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            FillGvToDoList();
            FillGvCompleted();
        }

        protected void FillGvToDoList()
        {
            string sort = ddlSortTasks.SelectedValue;
            string sql = "SELECT ItemID, ItemText, DateAdded, Priority from ToDoList where IsCompleted = 0 order by " + sort;

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvToDoList.DataSource = ds;
            gvToDoList.DataBind();
        }
        protected void FillGvCompleted()
        {
            string sql = "SELECT ItemID, ItemText, Priority, DateCompleted from ToDoList where IsCompleted = 1 order by Priority desc";

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvCompleted.DataSource = ds;
            gvCompleted.DataBind();
        }
        protected void FillGvLog()
        {
            string sql = "SELECT TOP (5) DateTime as Date, EventInformation as [Event Information], Name from EventLogs order by EventID desc";

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvLog.DataSource = ds;
            gvLog.DataBind();
        }

        protected void ddlSortTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGvToDoList();
        }

        protected void lbViewLogs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewLogs.aspx");
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