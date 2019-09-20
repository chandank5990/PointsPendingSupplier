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


namespace Points_Pending_Supplier
{
    public partial class Page1 : System.Web.UI.Page
    {


        public OleDbConnection database;
        DataTable data = new DataTable();
        bool chkstatus = false;
        OleDbConnection con = new OleDbConnection();
        //OleDbCommand cmd;


        //private OleDbConnection con;
        OleDbCommand oleDbCmd = new OleDbCommand();

        //String connParam = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\IT\Documents\School.accdb;Persist Security Info=False";
        //String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\CSK\Tablas.mdb";
        String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=W:\test\Access\Tablas.mdb";
        //String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=W:\\test\\Access\\tablas.mdb";
        OleDbDataAdapter da;
        //DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Button1.Visible = false;
            //Button2.Visible = false;
            //Label2.Visible = false;
            //Label3.Visible = false;
            //Panel1.Visible = false;
            //txtto.Text = System.DateTime.Now.ToShortDateString();
            Iframe1.Visible = false;
            if (!IsPostBack)
            {
                //Calendar1.Visible = false;
                //Calendar2.Visible = false;
            }
            //BindDropDownList();
            con = new OleDbConnection(connParam);
            //ddsupplier.Items.Insert(0, new ListItem("--Select Supplier--", "0"));
            //Iframe1
            //document.getElementById("Iframe1").style.display = "none";
            //document.getElementById("yourIFrameid").style.display = "block";
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //It solves the error "Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //Button1.Visible = true;
            //Button2.Visible = true;
            //Label2.Visible = true;
           // Label3.Visible = true;
            Label1.Text = ddsupplier.SelectedItem.Text;
            if (txtfrom.Text == "")
            {
                string script = "alert('Please Enter From Date!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnsubmit, this.GetType(), "Test", script, true);
                /*int GridView1HasRows = GridView1.Rows.Count;
                if (GridView1HasRows > 0)
                {
                    GridView1.Columns.Clear();
                    GridView1.DataBind();
                }*/
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
                //System.Data.OleDb.OleDbDataAdapter da;
                DataSet ds = new DataSet();
                OleDbCommand oleDbCmd = con.CreateCommand();
                //OleDbCommand Cmd; //= new OleDbCommand();
                con.Open();

                //String StrQuery = ("SELECT [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli, [Ordenes de fabricación].PreOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].NumOrd, [Pedidos a proveedor (líneas)].CodPie, [Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie, [Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (líneas)].AuxCtd, [Pedidos a proveedor (líneas)].Urgente FROM (([Ordenes de fabricación] INNER JOIN [Pedidos a proveedor (líneas)] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) WHERE [Ordenes de fabricación].EntOrd Between format(#" + txtfrom.Text + "#, \"mm/dd/yyyy\") And format(#" + txtto.Text + "#, \"mm/dd/yyyy\")");
                //String StrQuery = "SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd  " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                // " FROM   " +
                // " ([Pedidos de clientes] INNER JOIN ([Artículos de clientes] INNER JOIN [Ordenes de fabricación] ON [Artículos de clientes].CodArt = [Ordenes de fabricación].ArtOrd) ON [Pedidos de clientes].NumPed = [Ordenes de fabricación].PinOrd) " +
                // " " +
                //" WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"mm/dd/yyyy\") And format(#" + txtto.Text + "#, \"mm/dd/yyyy\")))) ) GROUP BY [Pedidos de clientes].CliPed, [Ordenes de fabricación].NumOrd ";

                //String StrQuery = "SELECT DISTINCTROW [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli,[Ordenes de fabricación].PreOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (líneas)].AuxCtd, [Pedidos a proveedor (líneas)].Urgente, [Pedidos a proveedor (cabeceras)].NumPed, [Pedidos a proveedor (cabeceras)].FecPed, [Pedidos a proveedor (cabeceras)].NumOfe,[Pedidos a proveedor (cabeceras)].ProPed " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                // " FROM   " +
                // " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Pedidos a proveedor (cabeceras)].NumPed = [Pedidos a proveedor (líneas)].NumPed) ON [Pedidos a proveedor (líneas)].NumOrd = [Ordenes de fabricación].NumOrd)" +
                // " " +
                // " WHERE [Ordenes de fabricación].EntOrd Between format(#" + txtfrom.Text + "#, \"mm/dd/yyyy\") And format(#" + txtto.Text + "#, \"mm/dd/yyyy\")"; //GROUP BY [Pedidos de clientes].CliPed, [Ordenes de fabricación].NumOrd ";

                //String StrQuery = "SELECT DISTINCTROW [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli,[Ordenes de fabricación].PreOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (líneas)].AuxCtd, [Pedidos a proveedor (líneas)].Urgente " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                // " FROM   " +
                //" ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Pedidos a proveedor (líneas)].NumOrd = [Ordenes de fabricación].NumOrd)) INNER " +
                // " " +
                //" WHERE ([Ordenes de fabricación].EntOrd Between format(#" + txtfrom.Text + "#, \"mm/dd/yyyy\") And format(#" + txtto.Text + "#, \"mm/dd/yyyy\")) AND ()" ;
               // oleDbCmd = new OleDbCommand("SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PieOrd, [Ordenes de fabricación].EntCli,[Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (cabeceras)].ProPed, [Pedidos a proveedor (cabeceras)].FecPed " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
               //                          " FROM   " +
              //                           " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) ON [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) " +
              //                           " " +
              //                           " WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"mm/dd/yyyy\") And format(#" + txtto.Text + "#, \"mm/dd/yyyy\"))))AND ProPed = " + ddsupplier.SelectedValue + ")", con);







                //oleDbCmd = new OleDbCommand("SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (cabeceras)].FecPed " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                //                         " FROM   " +
                //                         " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) ON [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) " +
                //                         " " +
                //                         " WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"dd/mm/yyyy\") And format(#" + txtto.Text + "#, \"dd/mm/yyyy\"))))AND ProPed = " + ddsupplier.SelectedValue + ")", con); //GROUP BY [Pedidos de clientes].CliPed, [Ordenes de fabricación].NumOrd ";
                


                oleDbCmd = new OleDbCommand("SELECT DISTINCTROW  [Ordenes de fabricación].NumOrd, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].EntOrd, [Ordenes de fabricación].PreOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].Datos, [Ordenes de fabricación].DtoOrd, [Pedidos a proveedor (líneas)].NumPed, [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas, [Pedidos a proveedor (líneas)].PiePed, [Pedidos a proveedor (líneas)].PrePie, [Pedidos a proveedor (líneas)].PlaPie,[Pedidos a proveedor (líneas)].PieRec, [Pedidos a proveedor (cabeceras)].FecPed " +   /* Clientes.NomCli, Clientes.Divisa, Divisas.Cambio"  +*/
                                         " FROM   " +
                                         " ([Pedidos a proveedor (cabeceras)] INNER JOIN ([Pedidos a proveedor (líneas)] INNER JOIN [Ordenes de fabricación] ON [Ordenes de fabricación].NumOrd = [Pedidos a proveedor (líneas)].NumOrd) ON [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) " +
                                        " " +
                                         " WHERE (((([Ordenes de fabricación].FinOrd) Is Null))  AND (((([Ordenes de fabricación].EntOrd) Between format(#" + txtfrom.Text + "#, \"dd/mm/yyyy\") And format(#" + txtto.Text + "#, \"dd/mm/yyyy\"))))AND ProPed = " + ddsupplier.SelectedValue + ") ORDER BY [Ordenes de fabricación].EntOrd ASC ", con); //GROUP BY [Pedidos de clientes].CliPed, [Ordenes de fabricación].NumOrd ";






                //OleDbDataReader reader;
                //con.Open();
                //reader = oleDbCmd.ExecuteReader();
                /*using (con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\CSK\Tablas.mdb"))
                {
                    using (cmd = new OleDbCommand(StrQuery, con))
                    {

                        OleDbDataAdapter Da = new OleDbDataAdapter(cmd);
                        Da.Fill(ds);
                    }*/
                OleDbDataAdapter Da = new OleDbDataAdapter(oleDbCmd);
                Da.SelectCommand = oleDbCmd;
                Da.Fill(ds);
                //}
                GridView1.DataSource = ds;
                GridView1.DataBind();
                //con.Close();
                //Iframe1.Attributes.Add("src", "Drawing.aspx?UID=" + TextBoxUID.Text + "&Article=" + TextBoxArticle.Text + "&testdrawing= kkk");
                Iframe1.Visible = false;
            }
        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
             Iframe1.Visible = true;
             if (IsPostBack)
             {
                 GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                 //Iframe1.Visible = true;
                 //string rowNumber = grdrow.Cells[0].Text;
                 //string dealId = grdrow.Cells[1].Text;
                 // string dealTitle = grdrow.Cells[2].Text;
                 //Iframe1.Attributes.Add("src", "Drawing.aspx?UID=" + grdrow.Cells[0].Text + "&Article=" + grdrow.Cells[1].Text + "&testdrawing= kkk");
                 Iframe1.Attributes.Add("src", "Drawing2.aspx?UID=" + grdrow.Cells[1].Text + "&Article=" + grdrow.Cells[2].Text + "&testdrawing= kkk");
                 //Iframe1.Attributes.Add("src", "Drawing2.aspx?UID=" + TextBox1.Text + "&Article=" + TextBox2.Text);
                 //Iframe1.Attributes.Add("src", "Drawing2.aspx?UID=" + TextBox1.Text + "&Article=" + TextBox2.Text + "&testdrawing= kkk");
                //Iframe1.Attributes.Add("src", "Drawing2.aspx?UID=" + grdrow.Cells[0].Text + "&Article=" + grdrow.Cells[1].Text) + "&testdrawing= kkk");
             //TextBox1.Text = grdrow.Cells[0].Text;
             //TextBox2.Text = grdrow.Cells[1].Text;
             //GridViewRow row = GridView1.SelectedRow;
             //TextBox1.Text = GridView1.SelectedRow.Cells[0].Text;
             //TextBox2.Text = GridView1.SelectedRow.Cells[1].Text;
             }
             //GridViewRow row = GridView1.SelectedRow;
           
            //TextBox1.Text = row.Cells[0].Text;
             //TextBox2.Text = row.Cells[1].Text;
        }

        protected void ddsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*System.Data.OleDb.OleDbDataAdapter da; 

            OleDbCommand oleDbCmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT NomPro From Proveedores";
            cmd.Parameters.AddWithValue("NomPro", ddsupplier.SelectedValue);
            OleDbDataReader reader;
            cmd.Connection = con;
            reader = oleDbCmd.ExecuteReader();
            //reader.Close();
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //ddlCountry.DataSource = ds;
            //ddlCountry.DataTextField = "UserName";
            //ddlCountry.DataValueField = "UserId";
            //ddlCountry.DataBind();
            //ddsupplier.Items.Insert(0, new ListItem("--Select--", "0"));
            ddsupplier.DataBind();
            con.Close();*/
           // BindDropDownList();
        }

        protected void txtfrom_TextChanged(object sender, EventArgs e)
        {
            //txtfrom.Text = "10/10/2016";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
            /*Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Deatails.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();*/


            /*if (GridView1.DataSource=="")
            {
                string script = "alert('There is Nothing to Download!!!')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Button1, this.GetType(), "Test", script, true);

            }
            else
            {*/
                
           // }
       // }
        /*private void BindDropDownList()
        {
            OleDbConnection con = new OleDbConnection();
            string id = string.Empty;
            string name = string.Empty;
            string newName = string.Empty;

            DataTable dt = new DataTable();
            con.Open();
            String StrQuery = "SELECT CodePro,NomPro FROM Proveedores";

            using (con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\CSK\Tablas.mdb"))
            {
                using (cmd = new OleDbCommand(StrQuery, con))
                {

                    OleDbDataAdapter Da = new OleDbDataAdapter(cmd);
                    Da.Fill(dt);
                }
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id = dt.Rows[i]["CodPro"].ToString();
                    name = dt.Rows[i]["NomPro"].ToString();
                    newName = id + " ---- " + name;
                    ddsupplier.Items.Add(new ListItem(newName, id));
                }
            }*/

        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            //string FileName = "Details"+DateTime.Now+ ".xls";
            //string FileName = ddsupplier.SelectedItem.Text + "(" + DateTime.Now.ToString("dd-MM-yyyy") + ")" + ".xls";
            string FileName = ddsupplier.SelectedItem.Text+ ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //Response.AddHeader("content-disposition", "attachment;  filename=products.xls");
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        //public override void VerifyRenderingInServerForm(Control control)
       // {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        //}

        /*protected void Button2_Click(object sender, EventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtfrom.Text = Calendar1.SelectedDate.ToString("d");
            Calendar1.Visible = false;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            txtto.Text = Calendar2.SelectedDate.ToString("d");
            Calendar2.Visible = false;
        }*/

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
            string filename = @"W:\test\PPS\MergedDrawings2\" + ddsupplier.SelectedItem.Text + ".pdf";
            //string filename = ddsupplier.SelectedItem.Text +".pdf";
            outputDocument.Save(filename);
            //MessageBox.Show("Files are merged successfully");
            string script = "alert('Drawings Merged & Downloaded Successfully!!!')";
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnsubmit, this.GetType(), "Test", script, true);
            // ...and start a viewer.
            //Process.Start(filename);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {


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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Decimal total = 0;
            /*float total = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqy = (Label)e.Row.FindControl("lblqty");
                int PreOrd = Int32.Parse(lblqy.Text);
                //int PiePed = Int32.Parse(lblqy.Text);
                //total = PreOrd + PiePed;
                total = total + PreOrd;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
                lblTotalqty.Text = total.ToString();
            }*/

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string val3 = e.Row.Cells[10].Text;
                string val4 = e.Row.Cells[5].Text;
                string val5 = e.Row.Cells[11].Text;
                Label lblTotal = (Label)e.Row.Cells[13].FindControl("Label1");

                Decimal multiply = Math.Round(Convert.ToDecimal(val3) * Convert.ToDecimal(val4) * Convert.ToDecimal(1 - Convert.ToDecimal(val5) / 100), 2);
                lblTotal.Text += multiply.ToString();
                total += multiply;
                //TextBoxPoints.Text = Convert.ToString(points);
                string val16 = e.Row.Cells[5].Text;
                string val17 = e.Row.Cells[6].Text;
                Label lblSub = (Label)e.Row.Cells[7].FindControl("Label2");
                Decimal sub = Math.Round(Convert.ToDecimal(val16) - Convert.ToDecimal(val17));
                lblSub.Text += sub.ToString();
                //TextBoxPoints.Text = Convert.ToString(points);


                /* string val1 = e.Row.Cells[6].Text; //Gets the value in Column 1
                 string val2 = e.Row.Cells[7].Text; //Gets the value in Column 2
                 Label lblTotal = (Label)e.Row.Cells[9].FindControl("Label1");

                 if (val1 == string.Empty || val1 == "")
                 { val1 = "0"; }
                 if (val2 == string.Empty || val2 == "")
                 { val2 = "0"; }

                 int multiply = int.Parse(val1) * int.Parse(val2);
                 lblTotal.Text += multiply.ToString();
                 total += multiply;*/
                //total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "lblTotalpoint"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label Totalpoint = (Label)e.Row.FindControl("lblTotalpoint");
                Totalpoint.Text = total.ToString();
                //e.Row.Cells[9].Text = total.ToString(); 
                Label3.Text = Totalpoint.Text;
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


    }
}