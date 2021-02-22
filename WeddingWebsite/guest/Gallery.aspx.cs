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
    public partial class Gallery : System.Web.UI.Page
    {
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                createGallery();
            }
        }
        protected void createGallery()
        {
            DataTable gallery = getGalleryImages();
            buildGallery(gallery);
        }

        protected DataTable getGalleryImages()
        {
            DataTable dt = new DataTable();

            string query = "Select GalleryTable.Title, GalleryTable.Description, GalleryTable.Image from GalleryTable order by ImgOrder asc";

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dt);
            conn.Close();
            da.Dispose();

            return dt;

        }

        protected void buildGallery(DataTable dt)
        {
            int row_count = dt.Rows.Count;
            string img_lit_text = "";
            string nav_lit_text = "";


            for (int i = 0; i < row_count; i++)
            {
                int j = i + 1;
                string temp = "<div class=\"mySlides fade\">"; //slide start

                temp += "<div class=\"numbertext\">" + j + " / " + row_count + "</div>"; //index / row_count


                temp += "<img src = \".." + dt.Rows[i].Field<String>("Image") + "\" style = \"width:100%\" />"; //image


                temp += "<div class=\"text\">" + dt.Rows[i].Field<String>("Description") + "</div>";//caption

                temp += "</div>"; //slide end

                img_lit_text += temp;
                nav_lit_text += "<span class=\"dot\" onclick=\"currentSlide(" + j + ")\"></span>";
            }

            litGallery.Text = img_lit_text;
            litNavigateGallery.Text = nav_lit_text;


        }
    }
}