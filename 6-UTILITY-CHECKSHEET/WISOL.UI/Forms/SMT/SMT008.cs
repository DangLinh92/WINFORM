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
    public partial class SMT008 : PageType
    {
        private DataTable dt = new DataTable();
        private DataTable dt2 = new DataTable();
        public SMT008()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            txtStatus.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            Font font = new System.Drawing.Font(txtStatus.Properties.Appearance.Font.FontFamily, 9, FontStyle.Bold);
            txtStatus.Properties.Appearance.Font = font;
        }



        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT008.INT_LIST"
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

            base.InitializePage();
        }

        public override void SearchPage()
        {
            if (String.IsNullOrWhiteSpace(txtLot.Text.Trim()))
            {
                return;
            }
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT008.GET_LIST",
                    new string[] { "A_PLANT", "A_LOT" },
                    new string[] { Consts.PLANT, txtLot.Text.Trim() }
                 ); 
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    dt2 = base.m_ResultDB.ReturnDataSet.Tables[1];
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            if(gvList.DataRowCount < 1)
            {
                MsgBox.Show("Lot không tồn tại.", MsgType.Warning);
                return;
            }
            txtStatus.Text = "OK";
            txtStatus.BackColor = Color.Lime;
            for (int i = 0; i < gvList.DataRowCount; i++)
            {
                if(gvList.GetDataRow(i)["Judgment"].ToString().ToUpper().Trim() != "OK")
                {
                    if (String.IsNullOrWhiteSpace(gvList.GetDataRow(i)["NG_Reason"].ToString().Trim()))
                    {
                        txtStatus.Text = "NG";
                        txtStatus.BackColor = Color.FromArgb(255, 199, 206);
                        break;
                    }
                }
            }

            if(dt2.Rows.Count > 1)
            {
                txtStatus.Text = "NG";
                txtStatus.BackColor = Color.FromArgb(255, 199, 206);
            }
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;
            int indexCol = gvList.FocusedColumn.AbsoluteIndex;
            string column = gvList.FocusedColumn.FieldName;
            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string cellValue = gvList.GetDataRow(indexRow)["Judgment"].ToString();
                if(cellValue.Trim().ToUpper() != "OK")
                {
                    string lot = gvList.GetDataRow(indexRow)["Lot_No"].ToString();
                    string model = gvList.GetDataRow(indexRow)["Model_No"].ToString();
                    string cereal = gvList.GetDataRow(indexRow)["Cereal_Counter"].ToString();
                    string barcode = gvList.GetDataRow(indexRow)["Barcode"].ToString();
                    string reason = gvList.GetDataRow(indexRow)["NG_Reason"].ToString().Trim();
                    POP.POP_SMT008 popup = new POP.POP_SMT008(lot, model, cereal, barcode, reason);
                    popup.ShowDialog();
                    gvList.SetRowCellValue(indexRow, gvList.Columns["NG_Reason"], popup.comment);
                    dt.Rows[indexRow]["NG_Reason"] = popup.comment;
                }
            }

            txtStatus.Text = "OK";
            txtStatus.BackColor = Color.Lime;
            for (int i = 0; i < gvList.DataRowCount; i++)
            {
                if (gvList.GetDataRow(i)["Judgment"].ToString().ToUpper().Trim() != "OK")
                {
                    if (String.IsNullOrWhiteSpace(gvList.GetDataRow(i)["NG_Reason"].ToString().Trim()))
                    {
                        txtStatus.Text = "NG";
                        txtStatus.BackColor = Color.FromArgb(255, 199, 206);
                        break;
                    }
                }
            }
            gvList.BestFitColumns();
        }


        private void txtLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (String.IsNullOrWhiteSpace(txtLot.Text.Trim()))
                {
                    return;
                }
                else
                {
                    this.SearchPage();
                }
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Judgment")
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (cellValue.Trim().ToUpper() != "OK")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                }
            }

            if(dt2.Rows.Count > 1)
            {
                if(e.Column.FieldName == "Barcode")
                {
                    string cellValueBarcode = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                    if (!string.IsNullOrWhiteSpace(cellValueBarcode))
                    {
                        string sub = cellValueBarcode.Substring(0, 6);
                        if(sub.ToUpper() == dt2.Rows[0][0].ToString())
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                        }
                    }
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(gvList.DataRowCount < 1)
            {
                return;
            }
            if(txtUserAction.Text.Trim() == String.Empty)
            {
                MsgBox.Show("MSG_ERR_042".Translation(), MsgType.Warning);
                return;
            }

            if(dt2.Rows.Count > 1)
            {
                MsgBox.Show("MSG_ERR_041".Translation(), MsgType.Warning);
                return;
            }

            try
            {
                //string XML = Converter.GetDataTableToXml(gcList.DataSource as DataTable);
                string XML = Converter.GetDataTableToXml(dt);
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT008.PUT_ITEM",
                    new string[]
                    {
                        "A_XML",
                        "A_STATUS",
                        "A_USER_ACTION"
                    },
                    new string[]
                    {
                        XML,
                        txtStatus.Text.ToString(),
                        txtUserAction.Text.Trim().ToUpper()
                    }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    gcList.DataSource = null;
                    txtLot.Text = string.Empty;
                    txtStatus.Text = string.Empty;
                    txtUserAction.Text = string.Empty;
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
