using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;

public partial class aspx_MultiRow_Manip : System.Web.UI.Page 
{
    protected bool EditMode;
    protected static Stack<int> editedRowIndices = new Stack<int>();

    protected void Page_Load(object sender, EventArgs eventArgs)
    {
        if(!IsPostBack)
        {
            FillGridView();
        }
    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        setEditModeView();

        FillGridView();

        gvMultiEditMode();
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        resetDefaultView();
        FillGridView();
    }

    protected void btnUpdate_OnClick(object sender, EventArgs e){
        while (editedRowIndices.Count > 0){
            int index = editedRowIndiced.Pop();
            UpdateRow(2,index);
        }

        resetDefaultView();
        FillGridView();
    }

    protected void FillGridView() {
        string sel = "SELECT ID from Table";

        OleDbDataAdapter da = new OleDbDataAdapter(sel, CONNECTIONINFO);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
}