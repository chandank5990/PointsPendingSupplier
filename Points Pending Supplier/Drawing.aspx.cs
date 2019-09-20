/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Points_Pending_Supplier
{
    public partial class Drawing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using PQScan.PDFToImage;
using System.Drawing;
namespace Points_Pending_Supplier
{
    public partial class Drawing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1_Click(null, null);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Article = Request.QueryString["Article"];
            if (Article.Length > 6)
            {

                string pdfpath = Server.MapPath("~/Access/Planos/" + Article.Substring(0, 6) + "/" + Article + ".PC" + ".pdf");
                if (File.Exists(pdfpath))
                {
                    PdftoIMG(pdfpath);
                    
                    //Image1.ImageUrl = "~/Images/UIDimage/New.jpg";
                    //Iframe.Attributes.Add("src", "Access/Planos/" + TextBoxArticle.Text.Substring(0, 6) + "/" + TextBoxArticle.Text + ".PC.pdf");
                }

                else
                {
                    //Image1.ImageUrl = "~/Images/draft.jpg";

                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string Article = Request.QueryString["Article"];
            if (Article.Length > 6)
            {

                string pdfpath = Server.MapPath("~/Access/Planos/" + Article.Substring(0, 6) + "/" + Article + ".PT" + ".pdf");
                if (File.Exists(pdfpath))
                {
                    //WebClient client = new WebClient();
                    //Byte[] buffer = client.DownloadData(pdfPath);
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-length", buffer.Length.ToString());
                    //Response.BinaryWrite(buffer);
                    // Iframe.Attributes.Add("src", "Planos/" + TextBoxArticle.Text.Substring(0, 6) + "/" + TextBoxArticle.Text + ".PT.pdf");
                    PdftoIMG(pdfpath);
                    //Image1.ImageUrl = "~/Images/UIDimage/New.jpg";
                }

                else
                {
                    //Iframe.Attributes.Add("src", "Planos/draft.pdf");
                    //Image1.ImageUrl = "~/Images/draft.jpg";

                }
            }
        }


        void PdftoIMG(string pdfpath)
        {
            // string pdfFileName = Server.MapPath(pdfpath);
            string pdfFileName = pdfpath;

            // Create an instance of PQScan.PDFToImage.PDFDocument object.
            PDFDocument pdfDoc = new PDFDocument();

            // Load PDF document.
            pdfDoc.LoadPDF(pdfFileName);

            // Prepare response.
            //Response.Clear();
            //Response.ContentType = "image/jpeg";

            // Render first page of the document to the output image.
            System.Drawing.Image jpgImage = pdfDoc.ToImage(0);

            using (MemoryStream ms = new MemoryStream())
            {
                // Save image to jpg format.
                jpgImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                // Show jpg image to the aspx web page.
                //Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);

                Bitmap bmp = (Bitmap)System.Drawing.Image.FromStream(ms);
                bmp = new Bitmap(bmp); // make a copy of it

                ms.Close();


                // ...



                if (File.Exists(Server.MapPath("~/Images/UIDimage/New.jpg")))
                {
                    // File.Delete(HttpContext.Current.Server.MapPath("~/Images/UIDimage/New.jpg"));

                    bmp.Save(Server.MapPath("~/Images/UIDimage/New.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    bmp.Save(Server.MapPath("~/Images/UIDimage/New.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                }




            }




        }

    }
}