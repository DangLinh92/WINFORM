using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT016 : PageType
    {
        string target = string.Empty;
        string reality = string.Empty;
        public SMT016()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT016.INT_LIST"
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
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            ComboBoxItemCollection coll = cbShiftWork.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("Day");
                coll.Add("Night");
            }
            finally
            {
                coll.EndUpdate();
            }

            dtpDate.EditValue = DateTime.Today;

            txtMinutes.Properties.MinValue = 5;
            txtMinutes.Properties.MaxValue = 99;
            txtMinutes.Properties.Mask.EditMask = "\\d+";
            txtMinutes.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtMinutes.EditValue = 5;

            this.timerSMT016.Enabled = false;
            this.timerSMT016.Interval = 5000;
            this.timerSMT016.Tick += new System.EventHandler(this.timerSMT016_Tick);
            base.InitializePage();
        }

        private void timerSMT016_Tick(object sender, EventArgs e)
        {
            int hour = DateTime.Now.Hour;
            if (hour >= 8)
            {
                dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            if (hour >= 8 && hour < 20)
            {
                cbShiftWork.SelectedIndex = 0;
            }
            if (hour >= 20 && hour < 24)
            {
                cbShiftWork.SelectedIndex = 1;
            }
            if (hour < 8)
            {
                cbShiftWork.SelectedIndex = 1;
            }
            this.SearchPage();
        }

        public override void SearchPage()
        {
            if(cbShiftWork.SelectedIndex < 0 || cbShiftWork.SelectedIndex > 1)
            {
                MsgBox.Show("MSG_ERR_043".Translation(), MsgType.Warning);
                return;
            }

            base.SearchPage();
            string fromDate = string.Empty;
            string toDate = string.Empty;
            string shift = string.Empty;

            if(cbShiftWork.SelectedIndex == 0)
            {
                fromDate = dtpDate.DateTime.ToString("yyyy-MM-dd") + " 08:00:00.000";
                toDate = dtpDate.DateTime.ToString("yyyy-MM-dd") + " 20:00:00.000";
                shift = "DAY";
            }
            if (cbShiftWork.SelectedIndex == 1)
            {
                fromDate = dtpDate.DateTime.ToString("yyyy-MM-dd") + " 20:00:00.000";
                toDate = dtpDate.DateTime.AddDays(1).ToString("yyyy-MM-dd") + " 08:00:00.000";
                shift = "NIGHT";
            }
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT016.GET_LIST"
                , new string[] { "A_DATE_FROM", "A_DATE_TO", "A_SHIFT" },
                  new string[] { fromDate, toDate, shift }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                      base.m_ResultDB.ReturnDataSet.Tables[0]
                      );
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

            gvList.OptionsView.ShowFooter = false;
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "n0";
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "n0";
            gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[7].DisplayFormat.FormatString = "n0";
            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "n0";
            gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[9].DisplayFormat.FormatString = "n0";
            gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[10].DisplayFormat.FormatString = "n0";
            gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[11].DisplayFormat.FormatString = "n0";
            gvList.Columns["MODEL1"].Visible = false;
            gvList.Columns["LINE1"].Visible = false;

            gvList.Columns["MODEL"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            gvList.Columns["LINE"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            gvList.Columns["NOTE"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            //gvList.Columns["DATE"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            //gvList.Columns["SHIFT"].OptionsColumn.AllowMerge = DefaultBoolean.True;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                return;
            }
            string model = txtModel.Text.Trim();
            string line = txtLine.Text.Trim();
            string date = txtDate.Text.Trim();
            date = date.Replace("-", "");
            string shift = txtShift.Text.Trim();
            string reality = txtReality.EditValue.ToString().Trim();
            reality = reality.Replace("-", "");

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT016.PUT_ITEM"
                    , new string[] {
                        "A_MODEL",
                        "A_LINE",
                        "A_DATE",
                        "A_SHIFT",
                        "A_REALITY",
                        "A_NOTE"
                    }
                    , new string[] {
                        model, line, date, shift, reality, txtNote.EditValue.NullString()
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtModel.Text = string.Empty;
                    txtLine.Text = string.Empty;
                    txtDate.Text = string.Empty;
                    txtShift.Text = string.Empty;
                    txtReality.Text = "0";
                    txtNote.Text = string.Empty;
                    SearchPage();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.AbsoluteIndex >= 6 && e.Column.AbsoluteIndex <= 11)
            {
                string per = gvList.GetRowCellDisplayText(e.RowHandle, gvList.Columns[4]);
                if(per == "PERFORMANCE(%)")
                {
                    string value = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                    if (!string.IsNullOrEmpty(value))
                    {
                        //if (value.Length == 1)
                        //{
                        //    value = "0" + value;
                        //}
                        //if (string.Compare(value, "60") < 0)
                        //{
                        //    e.Appearance.BackColor = Color.FromArgb(255, 255, 179);
                        //}
                        int v = Int32.Parse(value);
                        if(60 - v > 0)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 179);
                        }
                    }
                }
            }

            if (e.Column.AbsoluteIndex == 5)
            {               
                string value = gvList.GetRowCellDisplayText(e.RowHandle, gvList.Columns[4]);
                if (value == "TARGET")
                {
                    target = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                }
                if(value == "REALITY")
                {
                    reality = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);

                    if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(reality))
                    {
                        int tar = Convert.ToInt32(target);
                        int rea = Convert.ToInt32(reality);

                        if (rea - tar > 10)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                        }
                    }
                }
            }

            if (e.Column.AbsoluteIndex >= 2 && e.Column.AbsoluteIndex <= 11)
            {
                string per = gvList.GetRowCellDisplayText(e.RowHandle, gvList.Columns[4]);
                if (per == "TARGET")
                { 
                    e.Appearance.BackColor = Color.FromArgb(239, 239, 245);  
                }
            }
        }

        private void gvList_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if ((e.Column.FieldName == "MODEL") && (e.Column.FieldName == "LINE") && (e.Column.FieldName == "NOTE"))
                {
                    int value1 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle1, e.Column));
                    int value2 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle2, e.Column));

                    e.Merge = (value1 == value2);
                    e.Handled = true;
                    return;
                }
                //if ((e.Column.FieldName == "LINE"))
                //{
                //    int value1 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle1, e.Column));
                //    int value2 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle2, e.Column));

                //    e.Merge = (value1 == value2);
                //    e.Handled = true;
                //    return;
                //}
            }
            catch 
            {
            }
        }

        private void chkRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                int hour = DateTime.Now.Hour;
                if (hour >= 8)
                {
                    dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (hour >= 8 && hour < 20)
                {
                    cbShiftWork.SelectedIndex = 0;
                }
                if (hour >= 20 && hour < 24)
                {
                    cbShiftWork.SelectedIndex = 1;
                }
                if (hour < 8)
                {
                    cbShiftWork.SelectedIndex = 1;
                }
                int minute = Int32.Parse(txtMinutes.EditValue.ToString());
                timerSMT016.Interval = minute * 60 * 1000;
                timerSMT016.Enabled = true;
                this.SearchPage();
            }
            else
            {
                int hour = DateTime.Now.Hour;
                if (hour >= 8)
                {
                    dtpDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpDate.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (hour >= 8 && hour < 20)
                {
                    cbShiftWork.SelectedIndex = 0;
                }
                if (hour >= 20 && hour < 24)
                {
                    cbShiftWork.SelectedIndex = 1;
                }
                if (hour < 8)
                {
                    cbShiftWork.SelectedIndex = 1;
                }
                timerSMT016.Enabled = false;
                this.SearchPage();
            }
        }

        private void txtMinutes_EditValueChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                timerSMT016.Enabled = false;
                int minute = Int32.Parse(txtMinutes.EditValue.ToString());
                timerSMT016.Interval = 1 * 60 * 1000;
                timerSMT016.Enabled = true;
            }
        }

        private void gvList_Click(object sender, EventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;
            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle && gvList.RowCount > 0)
            {
                if (gvList.GetRowCellValue(indexRow, gvList.Columns["INFO"]).ToString() == "REALITY")
                {
                    //string model = gvList.GetRowCellValue(indexRow, gvList.Columns["MODEL"]).ToString();
                    string model = gvList.GetDataRow(indexRow)["MODEL"].ToString().Trim();
                    string line = gvList.GetDataRow(indexRow)["LINE"].ToString().Trim();
                    string date = gvList.GetDataRow(indexRow)["DATE"].ToString().Trim();
                    string shift = gvList.GetDataRow(indexRow)["SHIFT"].ToString().Trim();
                    string note = gvList.GetDataRow(indexRow)["NOTE"].ToString().Trim();
                    var cycle = gvList.GetDataRow(indexRow)[5];
                    if(cycle != DBNull.Value)
                    {
                        if (!string.IsNullOrWhiteSpace(cycle.ToString()))
                        {
                            txtReality.Text = cycle.ToString();
                        }
                    }

                    txtModel.Text = model;
                    txtLine.Text = line;
                    txtDate.Text = date;
                    txtShift.Text = shift;
                    txtNote.Text = note;
                }
            }
        }
    }
}
