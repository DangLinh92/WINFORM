using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT005 : PageType
    {
        private string max_date = string.Empty;
        private string Ctext = string.Empty;
        private bool CanEditFA = false;
        public SMT005()
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
            dtpFrom.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.EditValue = DateTime.Now.ToString();
            if(dtpFrom.DateTime.ToString("yyyyMMdd") == dtpTo.DateTime.ToString("yyyyMMdd"))
            {
                dtpFrom.EditValue = dtpFrom.DateTime.AddMonths(-1);
            }
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT005.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    max_date = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT005.GET_LIST",
                    new string[] { "A_PLANT", "A_FROM", "A_TO", "A_USER_ID" },
                    new string[] { Consts.PLANT, dtpFrom.DateTime.ToString("yyyy-MM-dd 00:00:00.000"), 
                        dtpTo.DateTime.ToString("yyyy-MM-dd 00:00:00.000"), Consts.USER_INFO.Id }
                 );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    max_date = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                    if(base.m_ResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString() == "1")
                    {
                        CanEditFA = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            gvList.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[4].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[5].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[6].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[7].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[8].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[7].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[9].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[10].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[11].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[12].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[13].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[14].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[15].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[16].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[17].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[18].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[19].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[19].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[20].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[20].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[21].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[21].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[22].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[22].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[23].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[23].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.BestFitColumns();

            //gvList.Columns["MODEL"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = string.Empty;
                string Year = dtpFrom.DateTime.Year.ToString();
                string Month = dtpFrom.DateTime.Month.ToString();
                if(Month.Length == 1)
                {
                    Month = "0" + Month;
                }
                if (!GetExcelFileName(ref fileName)) return;
                var pop = new POP_SMT005(Year, fileName, Convert.ToDateTime(max_date));
                if (pop.ShowDialog() == DialogResult.OK)
                    SearchPage();
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;
            int indexCol = gvList.FocusedColumn.AbsoluteIndex;
            string column = gvList.FocusedColumn.FieldName;
            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                if (indexCol > 8 && column != "COMMENT")
                {
                    DXMouseEventArgs ea = e as DXMouseEventArgs;
                    GridView view = sender as GridView;
                    GridHitInfo info = view.CalcHitInfo(ea.Location);
                    if (info.InRow || info.InRowCell)
                    {
                        string date_event = DateTime.Parse(gvList.GetDataRow(indexRow)[1].NullString()).ToString("yyyy-MM-dd");
                        string model = gvList.GetDataRow(indexRow)[2].NullString();
                        string lot_no = gvList.GetDataRow(indexRow)[3].NullString();
                        //string column = gvList.FocusedColumn.FieldName;
                        //MsgBox.Show(indexRow.ToString(), MsgType.Information);
                        //MsgBox.Show(column, MsgType.Information);
                        string value = gvList.GetDataRow(indexRow)[column].NullString();
                        //string nghi_ngo_sw = gvList.GetDataRow(indexRow)[17].NullString();
                        //string nghi_ngo_lna = gvList.GetDataRow(indexRow)[20].NullString();
                        //string nghi_ngo_sw_lna = gvList.GetDataRow(indexRow)[21].NullString();

                        //POP.POP_SMT005_2 popup = new POP.POP_SMT005_2(date_event, model, lot_no, nghi_ngo_sw, nghi_ngo_lna, nghi_ngo_sw_lna);
                        if (CanEditFA)
                        {
                            POP.POP_SMT005_2 popup = new POP.POP_SMT005_2(date_event, model, lot_no, column, value);
                            if (popup.ShowDialog() == DialogResult.OK)
                            {
                                //if (string.IsNullOrWhiteSpace(Ctext))
                                //{
                                //    SearchPage();
                                //}
                                hyperlinkLabelControl1.Text = "";
                            }
                        }
                        //else
                        //{
                        //    MsgBox.Show("You cannot modify this data!", MsgType.Warning);
                        //}
                    }
                }
            }
        }


        private void hyperlinkLabelControl1_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            hyperlinkLabelControl1.LinkVisited = true;
            try
            {
                System.Diagnostics.Process.Start(hyperlinkLabelControl1.Text);
            }
            catch { MsgBox.Show("Cannot open Hyperlink", MsgType.Warning); }
        }

        private void gvList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                string value = e.CellValue.ToString();
                //MsgBox.Show(value, MsgType.Information);

                string date_event = DateTime.Parse(gvList.GetDataRow(e.RowHandle)[1].NullString()).ToString("yyyy-MM-dd");
                string model = gvList.GetDataRow(e.RowHandle)[2].NullString();
                string lot_no = gvList.GetDataRow(e.RowHandle)[3].NullString();
                string column = gvList.FocusedColumn.FieldName;
                
                if (column == "PLANT")
                    return;
                if (e.Column.AbsoluteIndex > 8)
                {
                    try
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT005.POP_GET_LIST",
                            new string[]
                            {
                        "A_PLANT", "A_DATE_EVENT", "A_MODEL",
                        "A_LOT_NO", "A_COLUMN"
                            },
                            new string[]
                            {
                        Consts.PLANT, date_event, model, lot_no, column
                            }
                        );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                            {
                                this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                hyperlinkLabelControl1.Appearance.LinkColor = System.Drawing.Color.Blue;
                                hyperlinkLabelControl1.Appearance.VisitedColor = System.Drawing.Color.Navy;
                                hyperlinkLabelControl1.Text = @"" + base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                hyperlinkLabelControl1.Text = "";
                            }
                        }
                        else
                        {
                            MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                        }
                    }
                    catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Ctext = string.Empty;
            if (e.Column.AbsoluteIndex > 8 && e.Column.FieldName != "COMMENT")
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (Regex.IsMatch(cellValue, @"\d"))
                    {
                        if (Convert.ToInt32(cellValue) > 0)
                        {
                            e.Appearance.ForeColor = Color.FromArgb(156, 0, 6);
                            e.Appearance.BackColor = Color.FromArgb(255, 204, 204);
                        }
                    }
                }
            }
        }

        private void gvList_CellMerge(object sender, CellMergeEventArgs e)
        {
            //GridView view = sender as GridView;
            //try
            //{
            //    if ((e.Column.FieldName == "MODEL"))
            //    {
            //        int value1 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle1, e.Column));
            //        int value2 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle2, e.Column));

            //        e.Merge = (value1 == value2);
            //        e.Handled = true;
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
}
