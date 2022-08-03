using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraCharts;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.DataAccess.Excel;
using System.Collections.Generic;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT011 : PageType
    {
        public REPORT011()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
        }


        public override void InitializePage()
        {
            //IWorkbook workbook;
            //Worksheet worksheet;


            //spreadsheetControl1.WorksheetDisplayArea.SetSize(0, 44, 11);
            //spreadsheetControl1.WorksheetDisplayArea.SetSize(0, 100, 100);
            spreadsheetControl1.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;
            spreadsheetControl1.Options.Behavior.ShowPopupMenu = DevExpress.XtraSpreadsheet.DocumentCapability.Hidden;

            //workbook = spreadsheetControl1.Document;
            //worksheet = workbook.Worksheets[0];
            //worksheet.ActiveView.ShowGridlines = false;
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public override void SearchPage()
        {
            base.SearchPage();

            using (FileStream stream = new FileStream(@"\\10.70.21.236\AppUpdate\Luan_PI\run\Caitien.xlsx", FileMode.Open))
            {
                IWorkbook workbook = spreadsheetControl1.Document;
                
                workbook.LoadDocument(stream, DocumentFormat.Xlsx);
                //Worksheet worksheet = workbook.Worksheets[1];

                //List<Chart> chart = (List<Chart>) worksheet.Charts.GetCharts("차트 1");

                //chart[0].PrimaryAxes[1].MajorUnit = 0.5;

                //chart[0].SecondaryAxes[1].NumberFormat.FormatCode = "0.00%";
                ////chart[0].SecondaryAxes[1].NumberFormat.IsSourceLinked = false;
                ////workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[1];
                //chart[0].SecondaryAxes[1].Scaling.AutoMax = false;
                //chart[0].SecondaryAxes[1].Scaling.Max = 0.003;
                //chart[0].SecondaryAxes[1].Scaling.AutoMin = false;
                //chart[0].SecondaryAxes[1].Scaling.Min = 0.00;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = string.Empty;

                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel Files(*.xls;*.xlsx;*xlsm)|*.xls;*.xlsx;*.xlsm";
                    dialog.RestoreDirectory = true;
                    dialog.Title = "Open Excel File";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        fileName = dialog.FileName;
                        IWorkbook workbook = spreadsheetControl2.Document;
                        string path = Application.StartupPath + @"\Caitien.xlsx";
                        File.Copy(fileName, path, true);

                        using (FileStream stream = new FileStream(path , FileMode.Open, FileAccess.ReadWrite))
                        {
                            workbook.LoadDocument(stream, DocumentFormat.Xlsx);
                            int count = workbook.Worksheets.Count;
                            for(int i = 0; i < count; i++)
                            {
                                if (i != 1)
                                {
                                    workbook.Worksheets[i].Visible = false;
                                }
                            }

                            using(var stream1 = File.Open(@"\\10.70.21.236\AppUpdate\Luan_PI\run\Caitien.xlsx", FileMode.Create, FileAccess.ReadWrite))
                            {
                                workbook.SaveDocument(stream1, DocumentFormat.Xlsx);
                            }

                            //workbook.SaveDocument(@"\\10.70.21.236\AppUpdate\Luan\run\Caitien.xlsx", DocumentFormat.Xlsx);
                        }


                        //File.Copy(fileName, @"\\10.70.21.236\AppUpdate\Luan\run\Caitien.xlsx", true);
                    }
                }

            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
