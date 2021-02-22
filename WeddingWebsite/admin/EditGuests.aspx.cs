using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using OfficeOpenXml;


namespace WeddingWebsite.admin
{
    public partial class EditGuests : System.Web.UI.Page
    {
        protected string name;
        protected static bool PartyEditMode,GuestEditMode;
        private string CONNECTIONINFO = ConfigurationManager.ConnectionStrings["SQLSERVERCONN1"].ConnectionString;
        protected static Stack<int> editedRowIndices = new Stack<int>(); //stack of rows which the user has edited in edit;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            name = Request.Cookies["Info"]["FirstName"] + " " + Request.Cookies["Info"]["LastName"];
            if (!IsPostBack)
            {
                IsPartyEditMode = false;
                FillGVParties();
                BindDDLParties();
                FillGVGuests();
            }
            
        }

        /*Control Event Methods*/
        protected void btnAddParty_Click(object sender, EventArgs e)
        {
            string PartyName = txtPartyName.Text.Trim();
            if (String.IsNullOrEmpty(PartyName))
            {
                //validation

                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Party Name')", true);

            }
            else
            {
                string sql = "Insert into PartyTable (PartyName) Values('" + PartyName + "')";
                SqlConnection conn = new SqlConnection(CONNECTIONINFO);
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string log = PartyName + " was added to the Party List";
                AddEventLog(log, name);
                FillGVParties();
            }
        }
        protected void btnAddGuest_Click(object sender, EventArgs e)
        {
            string first = txtFirstName.Text.Trim();
            string last = txtLastName.Text.Trim();
            int partyid = Convert.ToInt32(ddlParty.SelectedValue);
            int plus1 = cbPlusOne.Checked ? 1 : 0;
            string address = txtAddress.Text.Trim();
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string zip = txtZipCode.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string notes = txtNotes.Text.Trim();

            if (String.IsNullOrEmpty(first) || String.IsNullOrEmpty(last))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('One or more Required Fields have been left empty')", true);
                return;
            }
            else if (!String.IsNullOrEmpty(email) && !validateEmail(email))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter Valid Email')", true);
                return;
            }
            else if (!String.IsNullOrEmpty(phone) && !validatePhoneNumber(phone))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter Valid Phone Number (numbers only)')", true);
                return;
            }
            else if (!String.IsNullOrEmpty(state)  && state.Length != 2)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Use the 2 Letter State Abbreviation')", true);
                return;
            }
            else
            {
                string party;
                int hasparty;
                if (partyid == -1)
                {
                    party = "NULL";
                    hasparty = 0;
                }
                else
                {
                    party = "'" + partyid.ToString() + "'";
                    hasparty = 1;
                }

                string sql = String.Format("Insert into GuestTable (FirstName, LastName, HasPassword, HasParty, PartyID, HasPlusOne, Email, Phone, StreetAddress, City, State, Zipcode, Notes) Values ({0}, {1}, 0, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})", addQuotes(first), addQuotes(last), hasparty, party, plus1, addQuotes(email), addQuotes(phone), addQuotes(address), addQuotes(city), addQuotes(state), addQuotes(zip), addQuotes(notes));
                SqlConnection conn = new SqlConnection(CONNECTIONINFO);

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


                string log = "A Guest was added to the Guest List";
                AddEventLog(log, name);

                FillGVGuests();
            }


        }
        protected void btnBulkAdd_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                if (Path.GetExtension(FileUpload1.FileName).Equals(".xlsx"))
                {
                    var excel = new ExcelPackage(FileUpload1.FileContent);
                    var dt = ToDataTable(excel);
                    var table = "GuestTable";
                    using (var conn = new SqlConnection(CONNECTIONINFO))
                    {
                        var bulkCopy = new SqlBulkCopy(conn);
                        bulkCopy.DestinationTableName = table;
                        conn.Open();
                        var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt);
                    }

                    string log = FileUpload1.FileName + ".xlsx was imported into the Guest List.";
                    AddEventLog(log, name);
                    FillGVGuests();
                }
            }
        }        
        protected void ddlPartySortyBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGVParties();
        }
        protected void ddlSortGuests_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGVGuests();
        }
        protected void txtFilterName_TextChanged(object sender, EventArgs e)
        {
            FillGVGuests();
        }        

        //Party Functions
        protected void FillGVParties()
        {
            string sort = ddlPartySortyBy.SelectedValue;
            string sql = "SELECT * from PartyTable order by " + sort;

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvParties.DataSource = ds;
            gvParties.DataBind();
        }
        protected bool IsPartyEditMode
        {
            get { return PartyEditMode;  }
            set { PartyEditMode = value;  }
        }
        protected void btnEnterPartyEditMode_Click(object sender, EventArgs e)
        {
            IsPartyEditMode = true;
            setPartyEditModeView();
            //fill gv
            FillGVParties();
            //set gv to multirow edit mode
            gv_MultiEditMode("gvParties");

            

        }
        protected void btnExitPartyEditMode_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).ID;
            
            if(id == "btnUpdatePartyEditMode")
            {
                if(editedRowIndices.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('No Changes were made');", true);
                }
                else
                {
                    while(editedRowIndices.Count != 0)
                    {
                        int index = editedRowIndices.Pop();
                        UpdateRow("party", index);
                        
                    }
                }
            }

            IsPartyEditMode = false;

            resetPartyDefaultModeView();

            FillGVParties();

        }
        private void setPartyEditModeView()
        {
            gvParties.Columns[1].Visible = true; //view checkboxes
            gvParties.Columns[2].Visible = false; //cannot view delete linkbutton

            //set button visibility
            btnEnterPartyEditMode.Visible = false;
            btnUpdatePartyEditMode.Visible = true;
            btnCancelPartyEditMode.Visible = true;
            btnDeleteSelected.Visible = true;

     
        }
        private void resetPartyDefaultModeView()
        {
            gvParties.Columns[1].Visible = false; //view checkboxes
            gvParties.Columns[2].Visible = true; //cannot view delete linkbutton

            //set button visibility
            btnEnterPartyEditMode.Visible = true;
            btnUpdatePartyEditMode.Visible = false;
            btnCancelPartyEditMode.Visible = false;
            btnDeleteSelected.Visible = false;

        }
        protected void gvPartyMultiRow_RowChanged(object sender, EventArgs e)
        {
            var tempObject = sender as Control;
            while (tempObject.GetType().Name != "GridViewRow")
            {
                tempObject = tempObject.Parent;
            }

            if (tempObject.GetType().Name == "GridViewRow")
            {
                GridViewRow row = tempObject as GridViewRow;
                if (!editedRowIndices.Contains(row.RowIndex))
                {
                    editedRowIndices.Push(row.RowIndex);
                }
            }
        }
        protected void gvParties_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;
            DeleteRow("party", index);
            FillGVParties();
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvParties.Rows)
            {
                CheckBox cbDelete = (CheckBox)gvParties.Rows[gvr.RowIndex].FindControl("cbDelete");
                if (cbDelete.Checked)
                {
                    DeleteRow("party", gvr.RowIndex);
                }
            }
            IsPartyEditMode = false;
            resetPartyDefaultModeView();
            FillGVParties();

        }
        protected void cbPartyCheckAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow gvr in gvParties.Rows)
            {
                CheckBox cbDelete = (CheckBox)gvParties.Rows[gvr.RowIndex].FindControl("cbDelete");
                cbDelete.Checked = ((CheckBox)sender).Checked; ;
            }

        }

        //MultiRow Guest Functions
        protected void FillGVGuests()
        {
            string sort = ddlSortGuests.SelectedValue;
            string search = txtFilterName.Text.Trim();


            string sql = String.Format("SELECT [GuestID],GuestTable.[PartyID], [FirstName],[LastName],[RSVP],PartyName,[HasPlusOne],[PlusOneRSVP],[Email],[Phone],[StreetAddress],[City],[State],[ZipCode],[Notes] FROM[GuestTable] left join PartyTable on GuestTable.PartyID = PartyTable.PartyID where FirstName like '%{0}%' or LastName like '%{0}%' or Email like '%{0}%' or StreetAddress like '%{0}%' or City like '%{0}%' or State like '%{0}%' or ZipCode like '%{0}%' or Notes like '%{0}%' or PartyName like '%{0}%' order by {1}", search, sort);

            SqlConnection conn = new SqlConnection(CONNECTIONINFO);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvGuests.DataSource = ds;
            gvGuests.DataBind();
        }
        protected bool IsGuestEditMode
        {
            get { return GuestEditMode; }
            set { GuestEditMode = value; }
        }
        protected void btnGuestEditMode_Click(object sender, EventArgs e)
        {
            IsGuestEditMode = true;
            setGuestEditModeView();
            FillGVGuests();
            //set gv to multirow edit mode
            gv_MultiEditMode("gvGuests");

        }
        protected void btnGuestUpdate_Click(object sender, EventArgs e)
        {

            string id = ((Button)sender).ID;

            if (id == "btnGuestUpdate")
            {
                if (editedRowIndices.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('No Changes were made');", true);
                }
                else
                {
                    while (editedRowIndices.Count != 0)
                    {
                        int index = editedRowIndices.Pop();
                        UpdateRow("gust", index);

                    }
                }
            }

            IsGuestEditMode = false;
            resetGuestDefaultView();
            FillGVGuests();
        }
        private void setGuestEditModeView()
        {
            gvGuests.Columns[13].Visible = true;

            btnGuestEditMode.Visible = false;
            btnGuestUpdate.Visible = true;
            btnGuestEditCancel.Visible = true;
            btnDeleteGuests.Visible = true;
        }
        private void resetGuestDefaultView()
        {
            gvGuests.Columns[13].Visible = false;

            btnGuestEditMode.Visible = true;
            btnGuestUpdate.Visible = false;
            btnGuestEditCancel.Visible = false;
            btnDeleteGuests.Visible = false;
        }
        protected void btnDeleteGuests_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gvGuests.Rows)
            {
                CheckBox cbDelete = (CheckBox)gvGuests.Rows[gvr.RowIndex].FindControl("cbDeleteGuests");
                if (cbDelete.Checked)
                {
                    DeleteRow("guest", gvr.RowIndex);
                }
            }
            IsGuestEditMode = false;
            resetGuestDefaultView();
            FillGVGuests();
        }
        protected void cbGuestCheckAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow gvr in gvGuests.Rows)
            {
                CheckBox cbDelete = (CheckBox)gvGuests.Rows[gvr.RowIndex].FindControl("cbDelete");
                cbDelete.Checked = ((CheckBox)sender).Checked;
            }



        }
       
        /* General Gridview Methods */
        protected void UpdateRow(string type, int index)
        {
            string query = "";
            if(type == "party")
            {
                int partyid = Convert.ToInt32(((HiddenField)gvParties.Rows[index].FindControl("hidPartyID")).Value);
                string oldpartyname = ((HiddenField)gvParties.Rows[index].FindControl("hidPartyName")).Value.Trim();
                string newpartyname = ((TextBox)gvParties.Rows[index].FindControl("txtPartyName")).Text.Trim();
                query = String.Format("Update PartyTable set PartyName = '{0}' where PartyID = '{1}'", newpartyname, partyid);
                SqlConnection conn = new SqlConnection(CONNECTIONINFO);
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string log = oldpartyname + " was changed to " + newpartyname + ".";
                AddEventLog(log, name);
            }
            else if(type == "guest")
            {

            }
            
            
        }
        protected void DeleteRow(string type, int index)
        {
            string query = "";
            if (type == "party")
            {
                int partyid = Convert.ToInt32(((HiddenField)gvParties.Rows[index].FindControl("hidPartyID")).Value);
                string oldpartyname = ((HiddenField)gvParties.Rows[index].FindControl("hidPartyName")).Value.Trim();
                query = String.Format("Delete From PartyTable where PartyID = '{0}'", partyid);
                SqlConnection conn = new SqlConnection(CONNECTIONINFO);
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string log = oldpartyname + " was deleted from the Party List";
                AddEventLog(log, name);
                

            }
            else if (type == "guest")
            {
                int guestID = Convert.ToInt32(((HiddenField)gvGuests.Rows[index].FindControl("hidGuestID")).Value);
                string firstname = ((HiddenField)gvGuests.Rows[index].FindControl("hidFirstName")).Value;
                string lastname = ((HiddenField)gvGuests.Rows[index].FindControl("hidLastName")).Value;
                query = String.Format("Delete From GuestTable where GuestID = '{0}'", guestID);
                SqlConnection conn = new SqlConnection(CONNECTIONINFO);
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string log = firstname + " " + lastname + " was deleted from the guest list.";
                AddEventLog(log, name);
            }
        }
        protected void gv_MultiEditMode(string gvID)
        {
            if (gvID == "gvParties")
            {
                if (this.IsPartyEditMode)
                {
                    //drop downs and textboxes
                }
            }
            else if (gvID == "gvGuests")
            {
                if (this.IsGuestEditMode)
                {
                    foreach(GridViewRow gvr in gvGuests.Rows)
                    {
                        DropDownList ddlParties = (DropDownList)gvGuests.Rows[gvr.RowIndex].FindControl("ddlPartyName");
                        SqlConnection con = new SqlConnection(CONNECTIONINFO);
                        string com = "Select 'No Party' as PartyName, '-1' as PartyID Union Select Distinct PartyName, PartyID from PartyTable";
                        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                        DataTable dt = new DataTable();
                        adpt.Fill(dt);
                        ddlParties.DataSource = dt;
                        ddlParties.DataBind();
                        ddlParties.DataTextField = "PartyName";
                        ddlParties.DataValueField = "PartyID";
                        ddlParties.DataBind();

                    }
                }
            }

        }
        

        /* Helper Methods*/
        public string addQuotes(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                text = "NULL";
            }
            else
            {
                text = "'" + text + "'";
            }
            return text;
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
        protected void BindDDLParties()
        {
            SqlConnection con = new SqlConnection(CONNECTIONINFO);
            string com = "Select 'No Party' as PartyName, '-1' as PartyID Union Select Distinct PartyName, PartyID from PartyTable";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlParty.DataSource = dt;
            ddlParty.DataBind();
            ddlParty.DataTextField = "PartyName";
            ddlParty.DataValueField = "PartyID";
            ddlParty.DataBind();
        }
        public static DataTable ToDataTable(ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        /*Validation Methods*/
        private bool validatePhoneNumber(string phone)
        {
            if (phone.Length != 10)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Valid Phone Number')", true);
                return false;
            }

            foreach (char c in phone)
            {
                if (!Char.IsDigit(c))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Valid Phone Number')", true);
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
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "notification", "alert('Please Enter a Valid Email Address')", true);
                return false;
            }
        }

        

        

        
    }
}