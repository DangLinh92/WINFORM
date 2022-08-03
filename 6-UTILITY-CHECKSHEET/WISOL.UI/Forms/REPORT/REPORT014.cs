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
using System.Collections.Generic;
using System.Linq;
using Wisol.Common;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT014 : PageType
    {
        int yellow = 0;
        int red = 0;
        DataTable table1 = new DataTable("Table1");
        DataTable table2 = new DataTable("Table2");
        public REPORT014()
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
            txtNumberOfDays.Properties.MinValue = 5;
            txtNumberOfDays.Properties.MaxValue = 30;
            txtNumberOfDays.Properties.Mask.EditMask = "\\d+";
            txtNumberOfDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            //txtNumberOfDays.EditValue = 14;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT014.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    txtNumberOfDays.EditValue = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                    yellow = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][1].NullString());
                    red = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][1].NullString());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            txtTarget.Text = "0.12";
            txtTarget.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTarget.Properties.Mask.EditMask = "n4";

        }

        public override void SearchPage()
        {
            
            base.SearchPage();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT014.TREND_FED"
                    , new string[] { "A_DAYS" }
                    , new string[] { txtNumberOfDays.EditValue.ToString() }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    yellow = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][1].NullString());
                    red = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[0].Rows[1][1].NullString());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            TrendGet();

            
        }

        private void TrendGet()
        {
            gcList.DataSource = null;
            double target = Double.Parse(txtTarget.EditValue.ToString());
            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT014.TREND_GET"
                    , new string[] { "A_DAYS" }
                    , new string[] { txtNumberOfDays.EditValue.ToString() }
                    );
            if (base.m_ResultDB.ReturnInt == 0)
            {
                //DataTable table1 = new DataTable("Table1");
                //DataTable table2 = new DataTable("Table2");

                table1 = base.m_ResultDB.ReturnDataSet.Tables[0];
                table2 = base.m_ResultDB.ReturnDataSet.Tables[1];

                table1 = SetColumnsOrder(table1, table2);

                table1.Columns.Add("Ave", typeof(System.Double));
                table1.Columns.Add("Next Day", typeof(System.Double));
                table1.Columns.Add("Day over target", typeof(System.Double));
                table1.Columns["Comment"].SetOrdinal(table1.Columns.Count - 1);

                foreach (DataRow row in table1.Rows)
                {
                    int c = 0;
                    float v = 0;
                    int g = 0;
                    List<PointF> list = new List<PointF>();
                    for (int i = 7; i < row.ItemArray.Length - 4; i++)
                    {
                        if (row[i] != DBNull.Value)
                        {
                            c += 1;
                            v += (float)Convert.ToDouble(row[i].ToString());
                            PointF f = new PointF(c, (float)Convert.ToDouble(row[i].ToString()));
                            list.Add(f);
                            if (Convert.ToDouble(row[i].ToString()) > target)
                            {
                                g += 1;
                            }
                        }
                    }
                    float slope = SlopeOfPoints(list);
                    float yIntercept = YInterceptOfPoints(list, slope);
                    row["Ave"] = Math.Round(v / c, 4).ToString();
                    row["Next Day"] = Math.Round((slope * (c + 1)) + yIntercept, 4).ToString();
                    if (c == 1)
                    {
                        row["Next Day"] = Math.Round(v / c, 4).ToString();
                    }
                    //row["Trend"] = (Math.Round((slope * (c + 1)) + yIntercept, 4) - Math.Round(v / c, 4)).ToString();
                    row["Day over target"] = g.ToString();
                }

                for (int i = table1.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = table1.Rows[i];
                    if (Convert.ToDouble(dr["Next Day"].ToString()) < 0)// || dr["Next Day"].ToString() == "NaN")
                        dr["Next Day"] = "0.0001";
                }
                table1.AcceptChanges();

                base.m_BindData.BindGridView(gcList,
                    table1
                    );
            }


            gvList.OptionsView.ShowFooter = false;
            gvList.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvList.Columns[0].AppearanceHeader.Options.UseTextOptions = true;
            gvList.Columns[0].Width = 140;
            //gvList.Columns["Day over target"].Width = 120;
            gvList.Columns["Day over target"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Day over target"].DisplayFormat.FormatString = "n0";
            gvList.Columns["Cycle Count"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Cycle Count"].DisplayFormat.FormatString = "n0";
            gvList.Columns["Head"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Head"].DisplayFormat.FormatString = "n0";
            gvList.Columns["Track"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Track"].DisplayFormat.FormatString = "n0";
            int count = gvList.Columns.Count;
            for (int i = 7; i < count - 2; i++)
            {
                gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[i].DisplayFormat.FormatString = "n4";
            }
            gvList.Columns["Comment"].Width = 200;

            gvList.BeginSort();
            try
            {
                gvList.ClearSorting();
                gvList.Columns["Comment"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                gvList.Columns["Day over target"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                gvList.Columns["Ave"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
            finally
            {
                gvList.EndSort();
            }

            gvList.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[4].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[5].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[6].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            table1.Clear();
            table2.Clear();
        }
        private static DataTable SetColumnsOrder(DataTable table, DataTable columnNames)
        {
            table.Columns["Cycle Count"].SetOrdinal(1);
            table.Columns["Mainteneance Date"].SetOrdinal(2);
            table.Columns["Line"].SetOrdinal(3);
            table.Columns["Machine"].SetOrdinal(4);
            table.Columns["Head"].SetOrdinal(5);
            table.Columns["Track"].SetOrdinal(6);

            int columnIndex = 7;
            for (int i = 0; i < columnNames.Rows.Count; i++)
            {
                table.Columns[columnNames.Rows[i][0].ToString()].SetOrdinal(columnIndex);
                columnIndex++;
            }
            return table;
        }

        private static float SlopeOfPoints(List<PointF> points)
        {
            float xBar = points.Average(p => p.X);
            float yBar = points.Average(p => p.Y);

            float dividend = points.Sum(p => (p.X - xBar) * (p.Y - yBar));
            float divisor = (float)points.Sum(p => Math.Pow(p.X - xBar, 2));

            return dividend / divisor;
        }

        private static float YInterceptOfPoints(List<PointF> points, float slope)
        {
            float xBar = points.Average(p => p.X);
            float yBar = points.Average(p => p.Y);

            return yBar - (slope * xBar);
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if(e.Column.FieldName != "Feeder ID" && e.Column.FieldName != "Day over target" && e.Column.FieldName != "Line"
            //   && e.Column.FieldName != "Cycle Count" && e.Column.FieldName != "Mainteneance Date"
            //   && e.Column.FieldName != "Machine" && e.Column.FieldName != "Head" && e.Column.FieldName != "Track")
            //{
            //    double target = Double.Parse(txtTarget.EditValue.ToString());
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        if (Convert.ToDouble(cellValue) > target)
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 187, 153);
            //        }
            //    }
            //}
            string value = gvList.GetRowCellValue(e.RowHandle, "Comment").NullString();
            if (!string.IsNullOrWhiteSpace(value))
            {
                e.Appearance.BackColor = Color.Yellow;
            }

            if (e.Column.AbsoluteIndex > 6 && e.Column.FieldName != "Day over target" && e.Column.FieldName != "Comment")
            {
                double target = Double.Parse(txtTarget.EditValue.ToString());
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (Convert.ToDouble(cellValue) > target)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 187, 153);
                    }
                }
            }

            if (e.Column.FieldName == "Cycle Count")
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    int intCellValue = Convert.ToInt32(cellValue.Replace(",",""));
                    if (intCellValue >= yellow && intCellValue < red)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 128);
                    }
                    if (intCellValue >= red)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                }
            }

            
            //if (e.Column.FieldName == "Trend")
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        if(Convert.ToDouble(cellValue) > 0)
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
            //            e.Appearance.ForeColor = Color.Red;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 0x9F, 0xFB, 0x69);
            //            e.Appearance.ForeColor = Color.BlueViolet;
            //        }
            //    }
            //}
        }

        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            //int indexRow = gvList.FocusedRowHandle;

            //if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //{
            //    string feeder = gvList.GetDataRow(indexRow)[0].NullString();

            //    POP.POP_REPORT001 popup = new POP.POP_REPORT001(feeder);
            //    popup.ShowDialog();
            //}
        }

        private void gcList_Click(object sender, EventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;

            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string feeder = gvList.GetDataRow(indexRow)[0].NullString();
                string comment = gvList.GetDataRow(indexRow)["Comment"].NullString();
                txtFeeder.Text = feeder;
                txtComment.Text = comment;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFeeder.EditValue.ToString()))
            {
                return;
            }

            try
            {

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT014.PUT_COMMENT"
                    , new string[] { "A_FEEDER",
                        "A_COMMENT",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { txtFeeder.EditValue.ToString(),
                        txtComment.EditValue.ToString().Trim(),
                        Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    //table = base.m_ResultDB.ReturnDataSet.Tables[0];
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    TrendGet();
                    //gvList.RowCellStyle += GvList_RowCellStyle;
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }

}
