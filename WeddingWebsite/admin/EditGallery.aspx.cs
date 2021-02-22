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
    public partial class EditGallery : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        private string name;
        protected void Page_Load(object sender, EventArgs e)
        {
            name = Request.Cookies["Info"]["FirstName"] + " " + Request.Cookies["Info"]["LastName"];
            FillgvGallery();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            if (!FileUpload.HasFile)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Upload a File.')", true);
            }
            else if (String.IsNullOrEmpty(txtTitle.Text.Trim())) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Title.')", true);
            }
            else if (String.IsNullOrEmpty(txtDescription.Text.Trim())) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Description.')", true);
            }
            else
            {
                string query = String.Format("Select Count(*) + 1 from GalleryTable");
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                var ret = cmd.ExecuteScalar();
                conn.Close();

                SqlConnection conn1 = new SqlConnection(CONNECTIONINFO);
                string strname = FileUpload.FileName.ToString();
                FileUpload.PostedFile.SaveAs(Server.MapPath("~/Images/Uploads/") + strname);
                conn1.Open();
                query = String.Format("INSERT into GalleryTable (Title, Description, Image, ImgOrder) VALUES ('{0}', '{1}', '/Images/Uploads/{2}', '{3}')", txtTitle.Text.Trim(), txtDescription.Text.Trim(), strname, ret);
                SqlCommand cmd2 = new SqlCommand(query, conn1);
                cmd2.ExecuteNonQuery();
                conn1.Close();

                string log = strname + " was added to the gallery.";
                AddEventLog(log, name);

                FillgvGallery();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('File Successfully Uploaded')", true);
            }
        }

        protected void FillgvGallery()
        {
            string sql = "SELECT ImageID, Title, Description, Image, ImgOrder from GalleryTable Order by ImgOrder";

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvGalleryEdit.DataSource = ds;
            gvGalleryEdit.DataBind();
        }
        protected void gvGalleryEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvGalleryEdit.DataKeys[e.RowIndex].Values["ImageID"];
            string imgpath = gvGalleryEdit.DataKeys[e.RowIndex].Values["Image"].ToString();

            string sql = String.Format("DELETE FROM GalleryTable where ImageID='{0}'", id);

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            FillgvGallery();
            DeleteFileFromFolder(imgpath);

            string log = imgpath.Replace("/Images/Uploads/", "") + " was remove from the gallery.";
            AddEventLog(log, name);


            Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Image Has Been Removed from Gallery')", true);
        }

        protected void lbMoveUp_Command(object sender, CommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            int id = (int)this.gvGalleryEdit.DataKeys[rowIndex]["ImageID"];
            using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
            {
                conn.Open();
                SqlCommand sql_cmnd = new SqlCommand("MoveImageUp", conn);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@ImgID", SqlDbType.Int).Value = id;
                sql_cmnd.ExecuteNonQuery();
                conn.Close();
            }
            FillgvGallery();

            string log = "Gallery was reordered.";
            AddEventLog(log, name);

        }
        protected void lbMoveDown_Command(object sender, CommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            int id = (int)this.gvGalleryEdit.DataKeys[rowIndex]["ImageID"];
            using (SqlConnection conn = new SqlConnection(CONNECTIONINFO))
            {
                conn.Open();
                SqlCommand sql_cmnd = new SqlCommand("MoveImageDown", conn);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@ImgID", SqlDbType.Int).Value = id;
                sql_cmnd.ExecuteNonQuery();
                conn.Close();
            }
            FillgvGallery();

            string log = "Gallery was reordered.";
            AddEventLog(log, name);
        }


        /* HELPER METHODS*/
        private void DeleteFileFromFolder(string StrFilename)
        {

            string strPhysicalFolder = Server.MapPath("..\\");

            string strFileFullPath = strPhysicalFolder + StrFilename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
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