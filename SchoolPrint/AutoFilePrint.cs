using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ppt = Microsoft.Office.Interop.PowerPoint;
using O2S.Components.PDFRender4NET;
using System.Drawing.Imaging;
using Microsoft.Office.Core;

namespace SchoolPrint
{
    public partial class AutoFilePrint : Form
    {
        private bool isDuplex;
        private bool isBlackWhite;
        private string paperSize;
        private int copies;
        private int numberPerPage;
        private string openFilePath;
        private string fileType;

        private ListboxItem fatherForm;

        public AutoFilePrint(AutoPrintParameter temp, ListboxItem fatherForm)
        {
            InitializeComponent();

            this.isDuplex = temp.isDuplex;
            this.isBlackWhite = temp.isBlackWhite;
            this.paperSize = temp.paperSize;
            this.copies = temp.copies;
            this.numberPerPage = temp.numberPerPage;
            this.openFilePath = temp.openFilePath;
            this.fileType = temp.fileType;
            this.fatherForm = fatherForm;

            Random ran = new Random();
            int RandKey = ran.Next(0,100000);
            TempFileName = System.Windows.Forms.Application.StartupPath + "\\temp\\" + RandKey;
            Directory.CreateDirectory(TempFileName);
        }

        public void PrintFile()
        {
            switch (fileType)
            {
                case "ppt": 
                    PrintPPT();
                    break;
                case "pptx":
                    PrintPPT();
                    break;
                case "pdf":
                    PrintPPT();
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        private void PrintPPT()
        {
            //MessageBox.Show("fuck");
            //PrintPpt(openFilePath);
            label1.Text = "正在打印中......";
            for (int i = 0; i < copies; i++)
            {
                SetEvent(numberPerPage);
                OnFilePrint();
            }
            this.Close();
        }

        #region printppt 
        PrintDocument pdDocument;
        PageSetupDialog dlgPageSetup = new PageSetupDialog();
        private int linesPrinted;
        PrintDialog dlgPrint = new PrintDialog();
        PrintPreviewDialog dlgPrintPreview = new PrintPreviewDialog();
        string TempFileName;

        bool is_landscape;

        int PageWidth = 0;
        int PageHight = 0;

        public void preHandlePpt(string FilePath)
        {
            label1.Text = "正在预处理......";
            Ppt.ApplicationClass app = new Ppt.ApplicationClass();
            Ppt.Presentation pp = null;
            try
            {
                pp = app.Presentations.Open(FilePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Microsoft.Office.Core.MsoTriState.msoFalse);
                //pp.PrintOptions.FrameSlides = Microsoft.Office.Core.MsoTriState.msoCTrue;
                //pp.PrintOptions.HandoutOrder = Ppt.PpPrintHandoutOrder.ppPrintHandoutHorizontalFirst;
                //pp.PrintOptions.OutputType = Ppt.PpPrintOutputType.ppPrintOutputNineSlideHandouts;
                //pp.PrintOptions.PrintInBackground = 0;
                //pp.PrintOptions.ActivePrinter = PrinterMachine;
                //pp.PrintOptions.PrintColorType = Ppt.PpPrintColorType.ppPrintBlackAndWhite;
                //pp.PrintOptions.FitToPage = Microsoft.Office.Core.MsoTriState.msoCTrue;
                //pp.PrintOptions.RangeType = Ppt.PpPrintRangeType.ppPrintAll;

                foreach (Ppt.Slide slide in pp.Slides)
                {
                    slide.FollowMasterBackground = Microsoft.Office.Core.MsoTriState.msoFalse;
                    slide.Background.Fill.Background();
                    slide.Background.Fill.ForeColor.RGB = ColorTranslator.ToOle(Color.White);
                    slide.Background.Fill.Solid();
                    slide.DisplayMasterShapes = Microsoft.Office.Core.MsoTriState.msoFalse;
                    foreach (Ppt.Hyperlink hyperlink in slide.Hyperlinks)
                    {
                        hyperlink.Delete();
                    }

                    foreach (Ppt.Shape shape in slide.Shapes)
                    {
                        //shape.BlackWhiteMode = Microsoft.Office.Core.MsoBlackWhiteMode.msoBlackWhiteBlack;
                        if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
                        {
                            if (shape.TextFrame.HasText == Microsoft.Office.Core.MsoTriState.msoTrue)
                            {
                                shape.TextFrame.TextRange.Font.Color.RGB = ColorTranslator.ToOle(Color.Black);
                                //shape.TextFrame.TextRange.Font
                            }
                        }
                    }

                }

                //PrintDialog printDialog = new PrintDialog();
                //printDialog.UseEXDialog = true;
                //printDialog.Document = pd;
                //printDialog.ShowDialog();
                PictureCount = pp.Slides.Count;
                if (!Directory.Exists(TempFileName))
                    Directory.CreateDirectory(TempFileName);
                //pp.SaveAs(TempFileName, Ppt.PpSaveAsFileType.ppSaveAsJPG, Microsoft.Office.Core.MsoTriState.msoCTrue);  
                pp.SaveAs(TempFileName, Ppt.PpSaveAsFileType.ppSaveAsJPG);
                //pp.PrintOut(1, pp.Slides.Count, null, 1, Microsoft.Office.Core.MsoTriState.msoCTrue);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (pp != null)
                    try
                    {
                        pp.Close();
                    }
                    catch (System.Exception ex)
                    {
                        ;
                    }
                if (app != null)
                    try
                    {
                        app.Quit();
                    }
                    catch (System.Exception ex)
                    {
                        ;
                    }
                GC.Collect();
            }
        }

        #region pdf2image
        public void preHandlePdf(string filePath)
        {
            label1.Text = "正在预处理......";
            ConvertPDF2Image(filePath, TempFileName+"\\", "幻灯片", 1, 1000, ImageFormat.Jpeg, Definition.One);
        }

        private enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }

        /// <summary>
        /// 将PDF文档转换为图片的方法
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径</param>
        /// <param name="imageName">生成图片的名字</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰</param>
        private void ConvertPDF2Image(string pdfInputPath, string imageOutputPath,
            string imageName, int startPageNum, int endPageNum, ImageFormat imageFormat, Definition definition)
        {
            PDFFile pdfFile = PDFFile.Open(pdfInputPath);

            if (!Directory.Exists(imageOutputPath))
            {
                Directory.CreateDirectory(imageOutputPath);
            }

            // validate pageNum
            if (startPageNum <= 0)
            {
                startPageNum = 1;
            }

            if (endPageNum > pdfFile.PageCount)
            {
                endPageNum = pdfFile.PageCount;
            }

            if (startPageNum > endPageNum)
            {
                int tempPageNum = startPageNum;
                startPageNum = endPageNum;
                endPageNum = startPageNum;
            }

            // start to convert each page
            int i ;
            for (i = startPageNum; i <= endPageNum; i++)
            {
                Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)definition);
                pageImage.Save(imageOutputPath + imageName + i.ToString() + ".JPG", imageFormat);
                pageImage.Dispose();
            }
            //MessageBox.Show(i.ToString());
            PictureCount = i - 1;
            pdfFile.Dispose();
        }
        #endregion

        private void SetEvent(int picture_per_page)
        {
            pdDocument = new PrintDocument();

            dlgPageSetup.Document = pdDocument;
            //dlgPageSetup.PrinterSettings.PrinterName = PrinterMachine;

            dlgPrint.Document = pdDocument;

            dlgPrintPreview.Document = pdDocument;

            switch (picture_per_page)
            {
                case 1: pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage_1);
                    pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
                    pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
                    PageHight = dlgPageSetup.PageSettings.PaperSize.Width;
                    PageWidth = dlgPageSetup.PageSettings.PaperSize.Height;
                    dlgPageSetup.PageSettings.Landscape = true;
                    break;
                case 2: pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage_2);
                    pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
                    pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
                    PageWidth = dlgPageSetup.PageSettings.PaperSize.Width;
                    PageHight = dlgPageSetup.PageSettings.PaperSize.Height;
                    dlgPageSetup.PageSettings.Landscape = false;
                    break;
                case 4: pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage_4);
                    pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
                    pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
                    PageHight = dlgPageSetup.PageSettings.PaperSize.Width;
                    PageWidth = dlgPageSetup.PageSettings.PaperSize.Height;
                    dlgPageSetup.PageSettings.Landscape = true;
                    break;
                case 6: pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage_6);
                    pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
                    pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
                    PageWidth = dlgPageSetup.PageSettings.PaperSize.Width;
                    PageHight = dlgPageSetup.PageSettings.PaperSize.Height;
                    dlgPageSetup.PageSettings.Landscape = false;
                    break;
                case 8: pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage_8);
                    pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
                    pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
                    PageWidth = dlgPageSetup.PageSettings.PaperSize.Width;
                    PageHight = dlgPageSetup.PageSettings.PaperSize.Height;
                    dlgPageSetup.PageSettings.Landscape = false;
                    break;
                case 9: pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage_9);
                    pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
                    pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
                    PageHight = dlgPageSetup.PageSettings.PaperSize.Width;
                    PageWidth = dlgPageSetup.PageSettings.PaperSize.Height;
                    dlgPageSetup.PageSettings.Landscape = true;
                    break;
            }



            //dlgPrint.ShowDialog();
            //dlgPrint.UseEXDialog = true;

            //dlgPrintPreview.ShowDialog();

        }

        void pdDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            //char[] param = { '\n' };

            //int i = 0;
            //char[] trimParam = { '\r' };
            //foreach (string s in lines)
            //{
            //    lines[i++] = s.TrimEnd(trimParam);
            //}
        }

        int PictureCount = 0;
        int PageCount = 0;
        int NowPage = 0;
        int NowPictureCount = 0;
        bool HasLoad = false;
        Image[] image;

        private void OnPrintPage_9(object sender, PrintPageEventArgs e)
        {
            /*
             *得到TextBox中每行的字符串数组
             * \n换行
             * \r回车
             */
            PageCount = PictureCount / 9;
            if (!HasLoad)
            {
                image = new Image[PictureCount];
                for (int k = 0; k < PictureCount; k++)
                {
                    image[k] = Image.FromFile(TempFileName + @"\幻灯片" + (k + 1) + ".JPG");
                }
                HasLoad = true;
            }

            //FileStream fs = new FileStream(@"f:\haha\幻灯片2.JPG", FileMode.Open, FileAccess.Read);
            //Image image = Image.FromStream(fs);
            while (NowPage <= PageCount)
            {
                NowPage++;
                //绘制要打印的页面
                //e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, x, y);
                int PictureHight = (PageHight - 20 * 4) / 3;
                int PictureWidth = (PageWidth - 20 * 4) / 3;
                for (int k = 0; (k < 9) && (NowPictureCount < PictureCount); NowPictureCount++, k++)
                {
                    int LocationX = k % 3;
                    int LocationY = k / 3;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(20 * (LocationX + 1) + LocationX * PictureWidth, 20 * (LocationY + 1) + LocationY * PictureHight, PictureWidth, PictureHight);
                    e.Graphics.DrawImage(image[NowPictureCount], destRect);
                }

                int lastBored = 20;
                Pen penLine = new Pen(Color.Black, 0.5f);
                penLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(penLine, new Point(lastBored, 20 * 1 + 10 + PictureHight), new Point(PageWidth - lastBored, 20 * 1 + 10 + PictureHight));
                e.Graphics.DrawLine(penLine, new Point(lastBored, 20 * 2 + 10 + 2 * PictureHight), new Point(PageWidth - lastBored, 20 * 2 + 10 + 2 * PictureHight));
                e.Graphics.DrawLine(penLine, new Point(20 * 1 + 10 + PictureWidth, lastBored), new Point(20 * 1 + 10 + PictureWidth, PageHight - lastBored));
                e.Graphics.DrawLine(penLine, new Point(20 * 2 + 10 + 2 * PictureWidth, lastBored), new Point(20 * 2 + 10 + 2 * PictureWidth, PageHight - lastBored));

                if (NowPictureCount < PictureCount)
                {
                    e.HasMorePages = true;
                    return;
                }
                //OnFilePrint();
            }
        }

        private void OnPrintPage_6(object sender, PrintPageEventArgs e)
        {
            /*
             *得到TextBox中每行的字符串数组
             * \n换行
             * \r回车
             */
            PageCount = PictureCount / 6;
            if (!HasLoad)
            {
                image = new Image[PictureCount];
                for (int k = 0; k < PictureCount; k++)
                {
                    image[k] = Image.FromFile(TempFileName + @"\幻灯片" + (k + 1) + ".JPG");
                }
                HasLoad = true;
            }

            //FileStream fs = new FileStream(@"f:\haha\幻灯片2.JPG", FileMode.Open, FileAccess.Read);
            //Image image = Image.FromStream(fs);
            while (NowPage <= PageCount)
            {
                NowPage++;
                //绘制要打印的页面
                //e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, x, y);
                //int PictureWidth = (PageWidth - 20 * 4) / 2;
                //int PictureHight = (PageHight - 50 * 6) / 3;
                int PictureWidth = (int)(image[1].Width / 2.5);
                int PictureHight = (int)(image[1].Height / 2.5);
                int WhiteWidth = (PageWidth - PictureWidth * 2) / 4;
                int WhiteHight = (PageHight - PictureHight * 3) / 6;
                for (int k = 0; (k < 6) && (NowPictureCount < PictureCount); NowPictureCount++, k++)
                {
                    int LocationX = k % 2;
                    int LocationY = k / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(WhiteWidth * (LocationX * 2 + 1) + LocationX * PictureWidth, WhiteHight * (LocationY * 2 + 1) + LocationY * PictureHight, PictureWidth, PictureHight);
                    e.Graphics.DrawImage(image[NowPictureCount], destRect);
                }
                int lastBored = 20;
                Pen penLine = new Pen(Color.Black, 0.5f);
                penLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(penLine, new Point(lastBored, WhiteHight * 2 + WhiteHight / 2 + PictureHight), new Point(PageWidth - lastBored, WhiteHight * 2 + WhiteHight / 2 + PictureHight));
                e.Graphics.DrawLine(penLine, new Point(lastBored, WhiteHight * 4 + WhiteHight / 2 + 2 * PictureHight), new Point(PageWidth - lastBored, WhiteHight * 4 + WhiteHight / 2 + 2 * PictureHight));
                e.Graphics.DrawLine(penLine, new Point(20 * 1 + 10 + PictureWidth, lastBored), new Point(20 * 1 + 10 + PictureWidth, PageHight - lastBored));
                //e.Graphics.DrawLine(penLine, new Point(20 * 2 + 10 + 2 * PictureWidth, lastBored), new Point(20 * 2 + 10 + 2 * PictureWidth, PageHight - lastBored));

                if (NowPictureCount < PictureCount)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
        }

        private void OnPrintPage_8(object sender, PrintPageEventArgs e)
        {
            /*
             *得到TextBox中每行的字符串数组
             * \n换行
             * \r回车
             */
            PageCount = PictureCount / 8;
            if (!HasLoad)
            {
                image = new Image[PictureCount];
                for (int k = 0; k < PictureCount; k++)
                {
                    image[k] = Image.FromFile(TempFileName + @"\幻灯片" + (k + 1) + ".JPG");
                }
                HasLoad = true;
            }

            //FileStream fs = new FileStream(@"f:\haha\幻灯片2.JPG", FileMode.Open, FileAccess.Read);
            //Image image = Image.FromStream(fs);
            while (NowPage <= PageCount)
            {
                NowPage++;
                //绘制要打印的页面
                //e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, x, y);
                //int PictureWidth = (PageWidth - 20 * 4) / 2;
                //int PictureHight = (PageHight - 50 * 6) / 3;
                int PictureWidth = (int)(image[1].Width / 2.6);
                int PictureHight = (int)(image[1].Height / 2.6);
                int WhiteWidth = (PageWidth - PictureWidth * 2) / 4;
                int WhiteHight = (PageHight - PictureHight * 4) / 8;
                for (int k = 0; (k < 8) && (NowPictureCount < PictureCount); NowPictureCount++, k++)
                {
                    int LocationX = k % 2;
                    int LocationY = k / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(WhiteWidth * (LocationX + 1) + LocationX * PictureWidth, WhiteHight * (LocationY + 1) + LocationY * PictureHight, PictureWidth, PictureHight);
                    e.Graphics.DrawImage(image[NowPictureCount], destRect);
                }

                int lastBored = 20;
                Pen penLine = new Pen(Color.Black, 0.5f);
                penLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(penLine, new Point(lastBored, WhiteHight * 1 + WhiteHight / 2 + PictureHight), new Point(PageWidth - lastBored, WhiteHight * 1 + WhiteHight / 2 + PictureHight));
                e.Graphics.DrawLine(penLine, new Point(lastBored, WhiteHight * 2 + WhiteHight / 2 + 2 * PictureHight), new Point(PageWidth - lastBored, WhiteHight * 2 + WhiteHight / 2 + 2 * PictureHight));
                e.Graphics.DrawLine(penLine, new Point(lastBored, WhiteHight * 3 + WhiteHight / 2 + 3 * PictureHight), new Point(PageWidth - lastBored, WhiteHight * 3 + WhiteHight / 2 + 3 * PictureHight));
                e.Graphics.DrawLine(penLine, new Point(20 * 1 + 10 + PictureWidth, lastBored), new Point(20 * 1 + 10 + PictureWidth, PageHight - lastBored));
                //e.Graphics.DrawLine(penLine, new Point(20 * 2 + 10 + 2 * PictureWidth, lastBored), new Point(20 * 2 + 10 + 2 * PictureWidth, PageHight - lastBored));

                if (NowPictureCount < PictureCount)
                {
                    e.HasMorePages = true;
                    return;
                }
            }
        }

        private void OnPrintPage_4(object sender, PrintPageEventArgs e)
        {
            /*
             *得到TextBox中每行的字符串数组
             * \n换行
             * \r回车
             */
            PageCount = PictureCount / 4;
            if (!HasLoad)
            {
                image = new Image[PictureCount];
                for (int k = 0; k < PictureCount; k++)
                {
                    image[k] = Image.FromFile(TempFileName + @"\幻灯片" + (k + 1) + ".JPG");
                }
                HasLoad = true;
            }

            //FileStream fs = new FileStream(@"f:\haha\幻灯片2.JPG", FileMode.Open, FileAccess.Read);
            //Image image = Image.FromStream(fs);
            while (NowPage <= PageCount)
            {
                NowPage++;
                //绘制要打印的页面
                //e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, x, y);
                //int PictureWidth = (PageWidth - 20 * 4) / 2;
                //int PictureHight = (PageHight - 50 * 6) / 3;
                int PictureWidth = (int)(image[1].Width / 2);
                int PictureHight = (int)(image[1].Height / 2);
                int WhiteWidth = (PageWidth - PictureWidth * 2) / 4;
                int WhiteHight = (PageHight - PictureHight * 2) / 4;
                for (int k = 0; (k < 4) && (NowPictureCount < PictureCount); NowPictureCount++, k++)
                {
                    int LocationX = k % 2;
                    int LocationY = k / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(WhiteWidth * (LocationX * 2 + 1) + LocationX * PictureWidth, WhiteHight * (LocationY * 2 + 1) + LocationY * PictureHight, PictureWidth, PictureHight);
                    e.Graphics.DrawImage(image[NowPictureCount], destRect);
                }

                Pen penLine = new Pen(Color.Black, 0.5f);
                penLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(penLine, new Point(WhiteWidth, WhiteHight * 2 + PictureHight), new Point(PictureWidth * 2 + WhiteWidth * 3, WhiteHight * 2 + PictureHight));
                //e.Graphics.DrawLine(penLine, new Point(WhiteWidth, WhiteHight * 4 + 2 * PictureHight), new Point(PageWidth * 2 + WhiteWidth * 3, WhiteHight * 4 + 2 * PictureHight));
                //e.Graphics.DrawLine(penLine, new Point(WhiteWidth, WhiteHight * 6 + 3 * PictureHight), new Point(PageWidth * 2 + WhiteWidth * 3, WhiteHight * 6 + 3 * PictureHight));
                e.Graphics.DrawLine(penLine, new Point(WhiteWidth * 2 + PictureWidth, WhiteHight), new Point(WhiteWidth * 2 + PictureWidth, WhiteHight * 3 + PictureHight * 2));
                //e.Graphics.DrawLine(penLine, new Point(20 * 2 + 10 + 2 * PictureWidth, lastBored), new Point(20 * 2 + 10 + 2 * PictureWidth, PageHight - lastBored));

                if (NowPictureCount < PictureCount)
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }

        private void OnPrintPage_2(object sender, PrintPageEventArgs e)
        {
            /*
             *得到TextBox中每行的字符串数组
             * \n换行
             * \r回车
             */
            PageCount = PictureCount / 2;
            if (!HasLoad)
            {
                image = new Image[PictureCount];
                for (int k = 0; k < PictureCount; k++)
                {
                    image[k] = Image.FromFile(TempFileName + @"\幻灯片" + (k + 1) + ".JPG");
                }
                HasLoad = true;
            }

            //FileStream fs = new FileStream(@"f:\haha\幻灯片2.JPG", FileMode.Open, FileAccess.Read);
            //Image image = Image.FromStream(fs);
            while (NowPage <= PageCount)
            {
                NowPage++;
                //绘制要打印的页面
                //e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, x, y);
                //int PictureWidth = (PageWidth - 20 * 4) / 2;
                //int PictureHight = (PageHight - 50 * 6) / 3;
                int PictureWidth = (int)(image[1].Width / 1.35);
                int PictureHight = (int)(image[1].Height / 1.35);
                int WhiteWidth = (PageWidth - PictureWidth) / 2;
                int WhiteHight = (PageHight - PictureHight * 2) / 4;
                for (int k = 0; (k < 2) && (NowPictureCount < PictureCount); NowPictureCount++, k++)
                {
                    int LocationX = k % 2;
                    int LocationY = k % 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(WhiteWidth, WhiteHight * (LocationY * 2 + 1) + LocationY * PictureHight, PictureWidth, PictureHight);
                    e.Graphics.DrawImage(image[NowPictureCount], destRect);
                }

                Pen penLine = new Pen(Color.Black, 0.5f);
                penLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(penLine, new Point(WhiteWidth, WhiteHight * 2 + PictureHight), new Point(PictureWidth + WhiteWidth, WhiteHight * 2 + PictureHight));
                //e.Graphics.DrawLine(penLine, new Point(WhiteWidth, WhiteHight * 4 + 2 * PictureHight), new Point(PageWidth * 2 + WhiteWidth * 3, WhiteHight * 4 + 2 * PictureHight));
                //e.Graphics.DrawLine(penLine, new Point(WhiteWidth, WhiteHight * 6 + 3 * PictureHight), new Point(PageWidth * 2 + WhiteWidth * 3, WhiteHight * 6 + 3 * PictureHight));
                //e.Graphics.DrawLine(penLine, new Point(WhiteWidth * 2 + PictureWidth, WhiteHight), new Point(WhiteWidth * 2 + PictureWidth, WhiteHight * 3 + PictureHight * 2));
                //e.Graphics.DrawLine(penLine, new Point(20 * 2 + 10 + 2 * PictureWidth, lastBored), new Point(20 * 2 + 10 + 2 * PictureWidth, PageHight - lastBored));

                if (NowPictureCount < PictureCount)
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }

        private void OnPrintPage_1(object sender, PrintPageEventArgs e)
        {
            /*
             *得到TextBox中每行的字符串数组
             * \n换行
             * \r回车
             */
            PageCount = PictureCount;
            if (!HasLoad)
            {
                image = new Image[PictureCount];
                for (int k = 0; k < PictureCount; k++)
                {
                    image[k] = Image.FromFile(TempFileName + @"\幻灯片" + (k + 1) + ".JPG");
                }
                HasLoad = true;
            }

            //FileStream fs = new FileStream(@"f:\haha\幻灯片2.JPG", FileMode.Open, FileAccess.Read);
            //Image image = Image.FromStream(fs);
            while (NowPage <= PageCount)
            {
                NowPage++;
                //绘制要打印的页面
                //e.Graphics.DrawString(lines[linesPrinted++], new Font("Arial", 10), Brushes.Black, x, y);
                //int PictureWidth = (PageWidth - 20 * 4) / 2;
                //int PictureHight = (PageHight - 50 * 6) / 3;
                int PictureWidth = (int)(image[1].Width / 1);
                int PictureHight = (int)(image[1].Height / 1);
                int WhiteWidth = (PageWidth - PictureWidth) / 2;
                int WhiteHight = (PageHight - PictureHight) / 2;
                for (int k = 0; (k < 1) && (NowPictureCount < PictureCount); NowPictureCount++, k++)
                {
                    int LocationX = k % 2;
                    int LocationY = k % 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(WhiteWidth, WhiteHight * (LocationY * 2 + 1) + LocationY * PictureHight, PictureWidth, PictureHight);
                    e.Graphics.DrawImage(image[NowPictureCount], destRect);
                }

                if (NowPictureCount < PictureCount)
                {
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }

        void pdDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //变量Lines占用和引用的字符串数组，现在释放
            //Directory.Delete(@"f:\haha", true);
            //Directory.Delete(TempFileName, true);
            HasLoad = false;
            //MessageBox.Show(PageCount.ToString());
            double money;
            if (PictureCount % numberPerPage == 0)
                PageCount = PageCount;
            else
                PageCount = PageCount + 1;
            if (!isBlackWhite)
                money = PageCount * 0.6;
            else if (isDuplex)
            {
                if (PageCount % 2 != 0)
                {
                    PageCount = PageCount + 1;
                }
                money = PageCount * 0.04;
            }
            else
                money = PageCount * 0.07;
            money = money * 100;
            if ((((int)money) % 10) != 0)
            {
                money = ((((int)money) / 10) + 1) * 10.0;
            }
            money = money / 100.0;
            fatherForm.setMoney(money.ToString(),TempFileName);
        }

        private void OnFilePrint()
        {
            try
            {
                //MessageBox.Show(pdDocument.PrinterSettings.PrinterName);
                if (isDuplex)
                {
                    if (numberPerPage == 8 || numberPerPage == 6)
                        pdDocument.PrinterSettings.Duplex = Duplex.Vertical;
                    else
                        pdDocument.PrinterSettings.Duplex = Duplex.Horizontal;
                }
                else
                    pdDocument.PrinterSettings.Duplex = Duplex.Simplex;
                if (isBlackWhite)
                    pdDocument.PrinterSettings.DefaultPageSettings.Color = false;
                else
                    pdDocument.PrinterSettings.DefaultPageSettings.Color = true;
                //调用打印
                pdDocument.Print();

                /*
                 * PrintDocument对象的Print()方法在PrintController类的帮助下，执行PrintPage事件。
                 */
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        #endregion

        private void AutoFilePrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    //if (Directory.Exists(TempFileName))
            //        //Directory.Delete(TempFileName, true);
            //}
            //catch
            //{
            //    ;
            //}
        }
    }
}
