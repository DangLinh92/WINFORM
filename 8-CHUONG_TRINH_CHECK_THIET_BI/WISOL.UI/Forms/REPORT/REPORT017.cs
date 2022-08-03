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
using System.Drawing.Imaging;
using DevExpress.Utils;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT017 : PageType
    {
        public REPORT017()
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
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            dtpFromMonth.EditValue = new DateTime(DateTime.Now.Year, 1, 1);
        }

        public override void SearchPage()
        {
            base.SearchPage();
            C1();
        }


        private void C1()
        {
            int monthDiff = ((dtpToMonth.DateTime.Year - dtpFromMonth.DateTime.Year) * 12) + dtpToMonth.DateTime.Month - dtpFromMonth.DateTime.Month;

            if (monthDiff > 11)
            {
                MsgBox.Show("Max range is 12 month.", MsgType.Warning);
                return;
            }

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();

            chartControl2.DataSource = null;
            chartControl2.Series.Clear();
            chartControl2.Titles.Clear();

            DataTable tableFlux = new DataTable("Table1");
            DataTable tableSolder = new DataTable("Table2");
            string from = dtpFromMonth.DateTime.ToString("yyyy-MM-01");
            string to = dtpToMonth.DateTime.AddMonths(1).ToString("yyyy-MM-01");

            DataTable table = new DataTable("Table1");

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT017.GET_LIST"
                    , new string[] { "A_PLANT", "A_FROM", "A_TO"
                    }
                    , new string[] { Consts.PLANT, dtpFromMonth.DateTime.ToString("yyyy-MM-01"), dtpToMonth.DateTime.AddMonths(1).ToString("yyyy-MM-01")
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    tableFlux = base.m_ResultDB.ReturnDataSet.Tables[0];
                    tableSolder = base.m_ResultDB.ReturnDataSet.Tables[1];
                    table.Merge(tableFlux);
                    table.Merge(tableSolder);

                    base.m_BindData.BindGridView(gcList,
                    table  
                    );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            gvList.Columns["RATE"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["RATE"].DisplayFormat.FormatString = "n3";
            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[2].DisplayFormat.FormatString = "n0";
            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "n0";
            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[4].DisplayFormat.FormatString = "n0";
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "n0";
            gvList.OptionsView.ShowFooter = false;

            gvList.BeginSort();
            try
            {
                gvList.ClearSorting();
                gvList.Columns["MONTH"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gvList.Columns["TYPE"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            finally
            {
                gvList.EndSort();
            }

            Series lineFlux = new Series("Flux Chi Phí theo EA(VND)", ViewType.Line);
            for(int i = 0; i < tableFlux.Rows.Count; i++)
            {
                lineFlux.Points.Add(new SeriesPoint(tableFlux.Rows[i][0].ToString(), Double.Parse(tableFlux.Rows[i][6].ToString()).ToString("0.###")));
            }

            Series lineSolder = new Series("Solder Chi Phí theo EA(VND)", ViewType.Line);
            for (int i = 0; i < tableSolder.Rows.Count; i++)
            {
                lineSolder.Points.Add(new SeriesPoint(tableSolder.Rows[i][0].ToString(), Double.Parse(tableSolder.Rows[i][6].ToString()).ToString("0.###")));
            }

            chartControl1.Series.AddRange(new Series[] { lineFlux, lineSolder});

            chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chartControl1.Legend.Direction = LegendDirection.LeftToRight;
            ((XYDiagram)chartControl1.Diagram).Rotated = false;

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            // sap xep line theo thu tu abc
            diagram.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();
            lineFlux.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            lineSolder.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "FLUX 개당 사용금액(WON)";
            //chartControl1.Titles.Add(chartTitle1);

            chartControl1.Dock = DockStyle.Fill;



            //Series lineSolder = new Series("", ViewType.Line);
            //for (int i = 0; i < tableSolder.Rows.Count; i++)
            //{
            //    lineSolder.Points.Add(new SeriesPoint(tableSolder.Rows[i][0].ToString(), tableSolder.Rows[i][2].ToString()));
            //}
            //chartControl2.Series.AddRange(new Series[] { lineSolder });

            //XYDiagram diagram2 = (XYDiagram)chartControl2.Diagram;

            //// sap xep line theo thu tu abc
            //diagram2.AxisX.QualitativeScaleComparer = new CaseInsensitiveComparer();
            //lineSolder.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //ChartTitle chartTitle2 = new ChartTitle();
            //chartTitle2.Text = "SOLDER 개당 사용금액(WON)";
            //chartControl2.Titles.Add(chartTitle2);

            //chartControl2.Dock = DockStyle.Fill;
        }


        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //using(var saveDialog = new SaveFileDialog())
            //{
            //    saveDialog.Filter = "Excel Files(*.xlsx)|*.xlsx";
            //    saveDialog.FileName = "Pick Up Rate_By Day and Line_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            //    if (saveDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        chartControl1.ExportToImage(Application.StartupPath + @"\images1.png", ImageFormat.Png);
            //        chartControl2.ExportToImage(Application.StartupPath + @"\images2.png", ImageFormat.Png);

            //        IWorkbook workbook;
            //        Worksheet worksheet;

            //        workbook = spreadsheetControl1.Document;
            //        worksheet = workbook.Worksheets[0];

            //        workbook.BeginUpdate();
            //        try
            //        {
            //            worksheet.Pictures.AddPicture(Application.StartupPath + @"\images1.png", worksheet.Cells["A13"]);
            //            worksheet.Pictures.AddPicture(Application.StartupPath + @"\images2.png", worksheet.Cells["A30"]);
            //        }
            //        finally
            //        {
            //            workbook.EndUpdate();
            //        }

            //        spreadsheetControl1.SaveDocument(saveDialog.FileName);
            //        worksheet.Pictures.Clear();
            //    }
            //}
        }

    }
}
