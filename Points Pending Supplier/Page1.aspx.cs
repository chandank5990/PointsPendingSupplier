using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Microsoft.VisualBasic;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing; // <- for merging pdfs
using System.Diagnostics;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Net;
using System.Drawing.Imaging;


namespace Points_Pending_Supplier
{
    public partial class Page1 : System.Web.UI.Page
    {
        private string _sortDirection;
        private DataSet ds;
        public OleDbConnection database;
        DataTable data = new DataTable();
        bool chkstatus = false;
        OleDbConnection con = new OleDbConnection();
        OleDbCommand oleDbCmd = new OleDbCommand();
        //String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\CSK\Tablas.mdb";
        String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=W:\test\Access\Tablas.mdb";
        OleDbDataAdapter da;
        DataTable dtAWS2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Iframe1.Visible = false;
            con = new OleDbConnection(connParam);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //It solves the error "Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            Label1.Text = ddsupplier.SelectedItem.Text;
            if (txtfrom.Text == "")
            {
                string script = "alert('Please Enter From Date!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnsubmit, this.GetType(), "Test", script, true);
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else if (txtto.Text == "")
            {
                string script = "alert('Please Enter To Date!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnsubmit, this.GetType(), "Test", script, true);
                GridView1.DataSource = null;
                GridView1.DataBind();
                
            }
            else
            {
                DataSet ds = new DataSet();
                OleDbCommand oleDbCmd = con.CreateCommand();
                con.Open();
                oleDbCmd = new OleDbCommand("SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, ([Pedidos a proveedor (líneas)].PiePed-[Pedidos a proveedor (líneas)].PieRec) as R, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (cabeceras)].FecPed, [Ordenes de fabricación].Location " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                                         " FROM   " +
                                         " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) ON [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) " +
                                        " " +
                                         " WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"dd/mm/yyyy\") And format(#" + txtto.Text + "#, \"dd/mm/yyyy\"))))AND ProPed = " + ddsupplier.SelectedValue + ") ORDER BY [Ordenes de fabricación].EntOrd ", con); //GROUP BY [Pedidos de clientes].CliPed, [Ordenes de fabricación].NumOrd ";

                OleDbDataAdapter Da = new OleDbDataAdapter(oleDbCmd);
                Da.SelectCommand = oleDbCmd;
                Da.Fill(data);
                foreach (DataRow dr in data.Rows)
                {
                    if (Convert.ToString(dr[16]) == "2")
                    {
                        dr.Delete();

                    }

                    //if (Convert.ToString(dr[9]) == "2")
                    //{
                    //    dr.Delete();
                    //}
                }
                data.AcceptChanges();
                GridView1.DataSource = data;
                GridView1.DataBind();
                Iframe1.Visible = false;
                if (ddsupplier.SelectedItem.Text == "AWS2")
                {
                    AWS2();
                }
                Label3.Text = Convert.ToString(total+total2);
            }
        }

        void AWS2()
        {
            //DataTable dtAWS2 = new DataTable();
            OleDbCommand oleDbCmd2 = con.CreateCommand();
            //con.Open();
            //oleDbCmd2 = new OleDbCommand("SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, ([Pedidos a proveedor (líneas)].PiePed-[Pedidos a proveedor (líneas)].PieRec) as R, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (cabeceras)].FecPed " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
            //                         " FROM   " +
            //                         " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) ON [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) " +
            //                        " " +
            //                         " WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"dd/mm/yyyy\") And format(#" + txtto.Text + "#, \"dd/mm/yyyy\")))) AND [Ordenes de fabricación].Location = " + 2 + ") ORDER BY [Ordenes de fabricación].EntOrd ", con); //GROUP BY [Pedidos de clientes].CliPed, [Ordenes de fabricación].NumOrd ";

            oleDbCmd2 = new OleDbCommand("SELECT [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].PinOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].LanOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].MarPie, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].PlaOrd, [Ordenes de fabricación].Observaciones, ([Ordenes de fabricación].PieOrd - [Ordenes de fabricación].EntCli) as R, [Pedidos de clientes].FecPed, [Pedidos de clientes].PedPed, [Artículos de clientes].NomArt FROM (([Ordenes de fabricación] INNER JOIN [Pedidos de clientes] ON [Ordenes de fabricación].PinOrd = [Pedidos de clientes].NumPed) INNER JOIN [Artículos de clientes] ON [Ordenes de fabricación].ArtOrd = [Artículos de clientes].CodArt)  WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"dd/mm/yyyy\") And format(#" + txtto.Text + "#, \"dd/mm/yyyy\")))) AND [Ordenes de fabricación].Location = " + 2 + ") ORDER BY [Ordenes de fabricación].EntOrd", con);

            //oleDbCmd2 = new OleDbCommand("Select * from [Ordenes de fabricación] where Location = "+2+"",con);
            OleDbDataAdapter Da = new OleDbDataAdapter(oleDbCmd2);
            Da.SelectCommand = oleDbCmd2;
            Da.Fill(dtAWS2);

            OleDbDataAdapter adcli = new OleDbDataAdapter("SELECT CodCli,Divisa FROM Clientes  ORDER BY CodCli ASC", con);
            DataTable dtcli = new DataTable();
            adcli.Fill(dtcli);
            string s1 = "$";
            string s2 = "Rs";
            string s3 = "EU";

            for (int j = 0; j < dtcli.Rows.Count; j++)
            {
                for (int i = 0; i < dtAWS2.Rows.Count; i++)
                {
                    string customer_code = dtAWS2.Rows[i][7].ToString().Substring(0, 6);
                    string customer_dtcli = dtcli.Rows[j][0].ToString();
                    string currency = dtcli.Rows[j][1].ToString();

                    if (customer_code.Equals(customer_dtcli) && currency.Equals(s1))
                    {
                        dtAWS2.Rows[i][10] = Convert.ToDecimal(dtAWS2.Rows[i][10].ToString()) * Convert.ToDecimal(0.74);//**for $ **//

                        //dataTable3.Rows[i][11] = Convert.ToDecimal(dataTable3.Rows[i][11].ToString()) / Convert.ToDecimal(1.1);//**for $ **//
                    }
                    if (customer_code.Equals(customer_dtcli) && currency.Equals(s2))
                    {
                        string rupees = dtAWS2.Rows[i][10].ToString();
                        if (dtAWS2.Rows[i][10] != null)
                        {
                            dtAWS2.Rows[i][10] = Convert.ToDecimal(dtAWS2.Rows[i][10].ToString()) * Convert.ToDecimal(0.02);//**for Rs.**//

                            //dataTable3.Rows[i][11] = Convert.ToDecimal(dataTable3.Rows[i][11].ToString()) / Convert.ToDecimal(60);//**for Rs.**//
                        }
                        string rupees1 = dtAWS2.Rows[i][10].ToString();
                    }
                    if (customer_code.Equals(customer_dtcli) && currency.Equals(s3))
                    {
                        if (dtAWS2.Rows[i][10] != null)
                            dtAWS2.Rows[i][10] = Convert.ToDecimal(dtAWS2.Rows[i][10].ToString()) * Convert.ToDecimal(1);//**for Euro**//

                    }
                }
            }
            dtAWS2.AcceptChanges();

            GridView2.DataSource = dtAWS2;
            GridView2.DataBind();

        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
         
                 //Pop up...........................
                 GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                 int intId = 100;

                 string strPopup = "<script language='javascript' ID='script1'>"

                 // Passing intId to popup window.
                 + "window.open('Drawing2.aspx?UID=" + grdrow.Cells[1].Text + "&Article=" + grdrow.Cells[2].Text + "&testdrawing= kkk" + "data=" + HttpUtility.UrlEncode("UID=" + grdrow.Cells[1].Text + "&Article=" + grdrow.Cells[2].Text + "&testdrawing= kkk")

                 + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                 + "</script>";

                 ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

        }

        protected void ddsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtfrom_TextChanged(object sender, EventArgs e)
        {
            //txtfrom.Text = "10/10/2016";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
            //ExportGrid2ToExcel();
        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            //string FileName = ddsupplier.SelectedItem.Text + "(" + DateTime.Now.ToString("dd-MM-yyyy") + ")" + ".xls";
            string FileName = ddsupplier.SelectedItem.Text+ ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }

        private void ExportGrid2ToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            //string FileName = ddsupplier.SelectedItem.Text + "(" + DateTime.Now.ToString("dd-MM-yyyy") + ")" + ".xls";
            string FileName = ddsupplier.SelectedItem.Text +"2"+ ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView2.GridLines = GridLines.Both;
            GridView2.HeaderStyle.Font.Bold = true;
            GridView2.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }

        void MakeCombinedPDF(DataTable data)
        {
            //List<byte[]> filesByte = new List<byte[]>();
            DataTable dtOut = null;
            data.DefaultView.Sort = "column16" + " " + "ASC";
            dtOut = data.DefaultView.ToTable();
            PdfDocument outputDocument = new PdfDocument();
            DataTable NotFound = new DataTable();
            NotFound.Columns.Add("NOT FOUND");
            try
            {
                foreach (DataRow dr in dtOut.Rows)
                {
                    if (!File.Exists(dr[2].ToString()))
                    {
                        NotFound.Rows.Add(dr[2].ToString());
                        continue;
                    }

                    PdfDocument inputDocument = PdfReader.Open(dr[2].ToString(), PdfDocumentOpenMode.Import);
                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Get the page from the external document...
                        PdfPage page = inputDocument.Pages[idx];
                        // ...and add it to the output document.
                        outputDocument.AddPage(page);
                        // logger.log("Completed Processing " + file.Name);
                    }
                }
            }
            catch (FileNotFoundException fe)
            {
                NotFound.Rows.Add(fe.ToString());
                //throw new Exception("OMG");
            }

            //GridView1.DataSource = NotFound;
            //GridView1.AutoResizeColumns();
            // Save the document...
            //string filename = @"W:\test\access\pointspending.pdf";
            //string filename = @"W:\test\PPS\MergedDrawings2\" + ddsupplier.SelectedItem.Text + ".pdf";
            //string filename = ddsupplier.SelectedItem.Text +".pdf";
            //outputDocument.Save(filename);
            outputDocument.Save(Server.MapPath("~/MergeDrawings/" + ddsupplier.SelectedItem.Text + ".pdf"));
            outputDocument.Close();
            //string script = "alert('Drawings Merged & Downloaded Successfully!!!')";
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnsubmit, this.GetType(), "Test", script, true);
            
            // ...and start a viewer.
            //Process.Start(filename);

            /*//string strURL = "~/MergedDrawings2/" + ddsupplier.SelectedItem.Text + ".pdf";
            string strURL = @"W:\test\PPS\MergedDrawings2\" + ddsupplier.SelectedItem.Text + ".pdf";
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            //response.AddHeader(" Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + UID + ".pdf");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ddsupplier.SelectedItem.Text + ".pdf");
            //byte[] data1 = req.DownloadData(Server.MapPath(strURL));
            byte[] data1 = req.DownloadData(strURL);
            response.BinaryWrite(data1);
            response.End();*/


            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ddsupplier.SelectedItem.Text + ".pdf");
            //Response.TransmitFile(Server.MapPath("~/Files/MyFile.pdf"));
            //Response.TransmitFile(@"W:\test\PPS\MergedDrawings2\" + ddsupplier.SelectedItem.Text + ".pdf");
            Response.TransmitFile(Server.MapPath("~/MergeDrawings/" + ddsupplier.SelectedItem.Text + ".pdf"));
            //Response.TransmitFile(Server.MapPath("/CSk/MergeDrawings/" + ddsupplier.SelectedItem.Text + ".pdf"));
            Response.End();

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {


            
                DataTable dataTable = Session["data"] as DataTable;

                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    dataView.Sort = e.SortExpression + " " + ConvertSortDirection(e.SortDirection);

                    GridView1.DataSource = dataView;
                    GridView1.DataBind();
                }
            
        }

        private string ConvertSortDirection(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }




        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                data.Columns.Add("column" + i.ToString());
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                DataRow dr = data.NewRow();
                for (int j = 0; j < GridView1.Columns.Count; j++)
                {
                    dr["column" + j.ToString()] = row.Cells[j].Text;
                }

                data.Rows.Add(dr);
            }


            // MakePrintList(data);

            //toPrint.Text = GridView1.Rows[e.NewEditIndex].Cells[1].Text.ToString();
            GridView1.DataSource = data;
            
            string temp = "";
            string folder = @"W:\test\access\planos\";
            foreach (DataRow dr in data.Rows)
            {
                temp = dr[2].ToString();
                temp = folder + temp.Substring(0, 6) + "\\" + temp + ".PC.pdf";
                dr[2] = temp;
            }
            MakeCombinedPDF(data);
            
        }


        Decimal total = 0;
        Decimal AWS2total = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Calculating AWS Total Qty..................
                //GridView1.Columns[4].Visible = false;
                string val3 = e.Row.Cells[3].Text;
                string val4 = e.Row.Cells[10].Text;
                string val5 = e.Row.Cells[18].Text;
                Label lblTotal = (Label)e.Row.Cells[11].FindControl("Label1");

                Decimal multiply = Math.Round(Convert.ToDecimal(val3) * Convert.ToDecimal(val4) * Convert.ToDecimal(1 - Convert.ToDecimal(val5) / 100), 2);
                lblTotal.Text += multiply.ToString();
                total += multiply;
                //TextBoxPoints.Text = Convert.ToString(points);

                // Calculating Pending Qty.....................
               
                //string val16 = e.Row.Cells[18].Text;
                //string val17 = e.Row.Cells[19].Text;
                //Label lblSub = (Label)e.Row.Cells[6].FindControl("Label2");
                //Decimal sub = Math.Round(Convert.ToDecimal(val16) - Convert.ToDecimal(val17));
                //lblSub.Text += sub.ToString();

                // Calculating AWS Delivered Point.....................
                string val16 = e.Row.Cells[19].Text;
                string val17 = e.Row.Cells[10].Text;
                Label lblSub = (Label)e.Row.Cells[12].FindControl("Label2");
                Decimal sub = Math.Round(Convert.ToDecimal(val16) * Convert.ToDecimal(val17));
                lblSub.Text += sub.ToString();
                AWS2total += sub;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label Totalpoint = (Label)e.Row.FindControl("lblTotalpoint");
                Totalpoint.Text = total.ToString();
                //e.Row.Cells[9].Text = total.ToString(); 

                //Label3.Text = Totalpoint.Text;

                //Label lblSub = (Label)e.Row.Cells[12].FindControl("AWSTotalpoint");
                Label lblSub = (Label)e.Row.FindControl("AWSTotalpoint");
                lblSub.Text = AWS2total.ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(e.Row.Cells[19].Text) < Convert.ToInt32(e.Row.Cells[6].Text) && Convert.ToInt32(e.Row.Cells[19].Text) > 0 || Convert.ToInt32(e.Row.Cells[6].Text) - Convert.ToInt32(e.Row.Cells[6].Text) < 0) //Here is the condition!
                {
            
                    //Change the cell color.
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].Text = "R";

                    //Change the back color.
                    //e.Row.Cells[3].BackColor = Color.Red;
                    //Label1.Visible = true;
                    // Label1.Text = "Pending On AWS2";

                }
                else
                    e.Row.Cells[3].Text = "";


            }

            /*if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal rowTotal = Convert.ToDecimal
                            (DataBinder.Eval(e.Row.DataItem, "AWS Total Point"));
                total = total + rowTotal;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotalpoint");
                lbl.Text = total.ToString();
            }*/



       }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string url = "Drawing_Page.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=400,height=200,left=400,top=100,resizable=no');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        protected void btnPanel_Click(object sender, EventArgs e)
        {
           
                Panel2.Visible = false;
            
        }

        Decimal total2;
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Calculating AWS Total Qty..................
                //GridView1.Columns[4].Visible = false;

                string val3 = e.Row.Cells[7].Text;
                string val4 = e.Row.Cells[9].Text;
                string val5 = e.Row.Cells[14].Text;
                Label lblTotal = (Label)e.Row.Cells[10].FindControl("Label101");

                Decimal multiply = Math.Round(Convert.ToDecimal(val3) * Convert.ToDecimal(val4) * Convert.ToDecimal(1 - Convert.ToDecimal(val5) / 100), 2);
                lblTotal.Text += multiply.ToString();
                total2 += multiply;

                //TextBoxPoints.Text = Convert.ToString(points);

                // Calculating Pending Qty.....................

                //string val16 = e.Row.Cells[18].Text;
                //string val17 = e.Row.Cells[19].Text;
                //Label lblSub = (Label)e.Row.Cells[6].FindControl("Label2");
                //Decimal sub = Math.Round(Convert.ToDecimal(val16) - Convert.ToDecimal(val17));
                //lblSub.Text += sub.ToString();

                // Calculating AWS Delivered Point.....................

               /* string val16 = e.Row.Cells[19].Text;
                string val17 = e.Row.Cells[10].Text;
                Label lblSub = (Label)e.Row.Cells[12].FindControl("Label2");
                Decimal sub = Math.Round(Convert.ToDecimal(val16) * Convert.ToDecimal(val17));
                lblSub.Text += sub.ToString();
                AWS2total += sub;*/

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label TotalpointAWS2 = (Label)e.Row.FindControl("lblTotalpointAWS2");
                TotalpointAWS2.Text = total2.ToString();
                //e.Row.Cells[9].Text = total.ToString(); 

                //Label3.Text = TotalpointAWS2.Text;

                //Label lblSub = (Label)e.Row.Cells[12].FindControl("AWSTotalpoint");

               /* Label lblSub = (Label)e.Row.FindControl("AWSTotalpoint");
                lblSub.Text = AWS2total.ToString();*/
            }
        }

        protected void btnDD2_Click(object sender, EventArgs e)
        {
            ExportGrid2ToExcel();
        }

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            AWS2();
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataView sortedView = new DataView(dtAWS2);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            GridView2.DataSource = sortedView;
            GridView2.DataBind();
        }
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }  


    }
}