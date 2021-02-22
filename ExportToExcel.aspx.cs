#include System.Web.UI
#include System.Web.IO 
#include System.Web.UI.Webcontrols 

/*This method take in/reads data from an asp gridview object, formats it, and then exports the data into an excel spreadsheet */

void ExportToExcel(){
        //gridView is the name of the GridView object you wish to export
		Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PartsExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gridView.AllowPaging = false;
            
            this.fillGridViewSparePartDetails();


            gridView.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gridView.HeaderRow.Cells)
            {
                cell.BackColor = gridView.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gridView.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gridView.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gridView.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            //   gridViewSpare.RenderControl
            gridView.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
}