using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
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

namespace Points_Pending_Supplier
{
    public partial class Drawing_Page : System.Web.UI.Page
    {
        OleDbConnection con = new OleDbConnection();
        DataTable data = new DataTable();
        OleDbCommand cmd = new OleDbCommand();
        //String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\CSK\Tablas.mdb";
        String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=W:\test\Access\Tablas.mdb";
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection(connParam);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            cmd = con.CreateCommand();
            //con.Open();
            if (TextOrder.Text == "")
            {
                string script = "alert('Please Enter Order No.!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Button3, this.GetType(), "Test", script, true);
            }
            else
            {
                cmd = new OleDbCommand("SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli,[Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].DtoOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (cabeceras)].ProPed, [Pedidos a proveedor (cabeceras)].FecPed " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                                             " FROM   " +
                                             " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) ON [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) " +
                                             " " +
                                             " WHERE [Pedidos a proveedor (líneas)].NumPed = " + TextOrder.Text + " ORDER BY [Pedidos a proveedor (líneas)].NumPed", con);//(((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Pedidos a proveedor (cabeceras)].FecPed) Between format(#" + txtfrom.Text + "#, \"mm/dd/yyyy\") And format(#" + txtto.Text + "#, \"mm/dd/yyyy\"))))AND ProPed = " + ddsupplier.SelectedValue + ") ORDER BY [Ordenes de fabricación].EntOrd ASC ", con);

                OleDbDataAdapter Da = new OleDbDataAdapter(cmd);
                Da.SelectCommand = cmd;
                Da.Fill(data);
                GridView1.DataSource = data;
                GridView1.DataBind();
                GridView1.Visible = false;

                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = data.NewRow();
                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        dr["column" + j.ToString()] = row.Cells[j].Text;
                    }
                }


                // MakePrintList(data);

                //toPrint.Text = GridView1.Rows[e.NewEditIndex].Cells[1].Text.ToString();
                //GridView1.DataSource = data;

                string temp = "";
                string folder = @"W:\test\access\planos\";
                foreach (DataRow dr in data.Rows)
                {
                    temp = dr[1].ToString();
                    temp = folder + temp.Substring(0, 6) + "\\" + temp + ".PC.pdf";
                    dr[1] = temp;
                }
                MakeCombinedPDF(data);
                ////con.Close();
                }
            
        }
        void MakeCombinedPDF(DataTable data)
        {
            //List<byte[]> filesByte = new List<byte[]>();
            PdfDocument outputDocument = new PdfDocument();
            DataTable NotFound = new DataTable();
            NotFound.Columns.Add("NOT FOUND");
            try
            {
                foreach (DataRow dr in data.Rows)
                {
                    if (!File.Exists(dr[1].ToString()))
                    {
                        NotFound.Rows.Add(dr[1].ToString());
                        continue;
                    }

                    PdfDocument inputDocument = PdfReader.Open(dr[1].ToString(), PdfDocumentOpenMode.Import);
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
           /* string filename = @"W:\test\PPS\MergedDrawings2\" +TextOrder.Text + ".pdf";
            outputDocument.Save(filename);*/
            outputDocument.Save(Server.MapPath("~/MergeDrawings/" + TextOrder.Text + ".pdf"));
            outputDocument.Close();
            //MessageBox.Show("Files are merged successfully");
            //string script = "alert('Drawings Downloaded Successfully!!!')";
           // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Button3, this.GetType(), "Test", script, true);
            // ...and start a viewer.
           // Process.Start(filename);

           /* //string strURL = "~/MergedDrawings2/" + ddsupplier.SelectedItem.Text + ".pdf";
            string strURL = @"W:\test\PPS\MergedDrawings2\" + TextOrder.Text + ".pdf";
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            //response.AddHeader(" Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + UID + ".pdf");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + TextOrder.Text + ".pdf");
            //byte[] data1 = req.DownloadData(Server.MapPath(strURL));
            byte[] data1 = req.DownloadData(strURL);
            response.BinaryWrite(data1);
            response.End();*/

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + TextOrder.Text + ".pdf");
            //Response.TransmitFile(Server.MapPath("~/Files/MyFile.pdf"));
            //Response.TransmitFile(@"W:\test\PPS\MergedDrawings2\" + TextOrder.Text + ".pdf");
            Response.TransmitFile(Server.MapPath("~/MergeDrawings/" + TextOrder.Text + ".pdf"));
            //Response.TransmitFile(Server.MapPath("/CSk/MergeDrawings/" + TextOrder.Text + ".pdf"));
            Response.End();

        }

    }

}