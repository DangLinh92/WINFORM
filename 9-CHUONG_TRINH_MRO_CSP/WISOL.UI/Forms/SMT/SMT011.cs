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
    public partial class SMT011 : PageType
    {
        public bool choose = true;
        public SMT011()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT011.INT_LIST"
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
            dtpFromDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();

            ComboBoxItemCollection coll = cbLine.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("C");
                coll.Add("D");
                coll.Add("E");
                coll.Add("F");
                coll.Add("G");
                coll.Add("H");
                coll.Add("I");
            }
            finally
            {
                coll.EndUpdate();
            }

            cbLine.SelectedIndex = -1;

            //txtExpTime.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtExpTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

            this.timer1.Enabled = true;
            this.timer1.Interval = 5 * 60 * 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SMT011.GET_LIST",
                    new string[] { "A_FROM_DATE", "A_TO_DATE" },
                    new string[] {  dtpFromDate.DateTime.ToString("yyyyMMdd") + "000000", dtpToDate.DateTime.ToString("yyyyMMdd") + "235959"}
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "n0";
            gvList.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.SearchPage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string value = cbLine.Text.Trim().ToUpper();
            if (!chkFlux.Checked)
            {
                if (cbLine.EditValue.NullString() == string.Empty)
                {
                    MsgBox.Show("MSG_ERR_023".Translation(), MsgType.Warning);
                    return;
                }
                if (value != "C" && value != "D" && value != "E" && value != "F" && value != "G" && value != "H" && value != "I")
                {
                    MsgBox.Show("MSG_ERR_027".Translation(), MsgType.Warning);
                    return;
                }
                if (txtLotMes.Text.Trim() == string.Empty)
                {
                    MsgBox.Show("MSG_ERR_028".Translation(), MsgType.Warning);
                    return;
                }
            }

            if (txtLot.Text.Trim() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_022".Translation(), MsgType.Warning);
                return;
            }
            //if (txtExpTime.EditValue.NullString() == string.Empty)
            //{
            //    MsgBox.Show("MSG_ERR_119".Translation(), MsgType.Warning);
            //    return;
            //}

            string isFlux = chkFlux.Checked.ToString();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT011.PUT_ITEM"
                    , new string[] {
                        "A_CHECK_FLUX",
                        "A_LINE",
                        "A_LOT",
                        "A_LOT_MES",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] {
                        isFlux,
                        cbLine.EditValue.NullString(),
                        txtLot.Text.Trim().ToUpper(),
                        txtLotMes.Text.Trim().ToUpper(),
                        Consts.USER_INFO.Id
                    }
                    ); 

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    cbLine.SelectedIndex = -1;
                    txtLot.Text = string.Empty;
                    txtLotMes.Text = string.Empty;
                    txtExpTime.EditValue = null;
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


        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            //int indexRow = gvList.FocusedRowHandle;
            //if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle && gvList.RowCount > 0)
            //{
            //    string line = gvList.GetDataRow(indexRow)["LINE"].ToString().Trim();
            //    string lot = gvList.GetDataRow(indexRow)["LOT_NO"].ToString().Trim();
            //    string lot_mes = gvList.GetDataRow(indexRow)["LOT_MES"].ToString().Trim();
            //    string exp_time = gvList.GetDataRow(indexRow)["HAN_SU_DUNG"].ToString().Trim();

            //    cbLine.SelectedText = line;
            //    txtLot.Text = lot;
            //    txtLotMes.Text = lot_mes;
            //    txtExpTime.Text = exp_time;
            //}
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MsgBox.Show("Bạn chắc chắn muốn xóa?", MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    //base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT011.DELETE_ITEM"
                    //    , new string[] { "A_PLANT", "A_LOT"
                    //    }
                    //    , new string[] { Consts.PLANT, Lot
                    //    }
                    //    );
                    //if (base.m_ResultDB.ReturnInt == 0)
                    //{
                    //    MsgBox.Show("Xóa thành công.", MsgType.Information);
                    //}
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
                SearchPage();
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName == "NGAY_HET_HAN" )
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        DateTime exp_date = DateTime.Parse(cellValue);
            //        DateTime current_date = DateTime.Now.Date;
            //        double count = (current_date - exp_date).TotalDays;
            //        if(count <= 7)
            //        {
            //            e.Appearance.BackColor = Color.Yellow;
            //        }
            //    }
            //}

            if (e.Column.AbsoluteIndex == 7)
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if(String.Compare(cellValue, "02H:00M:00S") <= 0)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void cbLine_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chkFlux_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFlux.Checked)
            {
                cbLine.ReadOnly = true;
                txtLotMes.ReadOnly = true;
                cbLine.SelectedIndex = -1;
                txtLotMes.Text = string.Empty;
            }
            else
            {
                cbLine.ReadOnly = false;
                txtLotMes.ReadOnly = false;
            }
        }
    }
}
