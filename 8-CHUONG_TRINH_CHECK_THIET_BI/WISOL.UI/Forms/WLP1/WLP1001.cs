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
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1001 : PageType
    {
        public WLP1001()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkPrintLabel.Checked = true;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1001.INT_LIST"
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
                    base.m_BindData.BindGridLookEdit(gleCategory, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE",  "CHEMICAL_NAME");
                    //base.m_BindData.BindGridLookEdit(gleMaker, base.m_ResultDB.ReturnDataSet.Tables[0], "MAKER", "MAKER_NAME");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            dtpFromDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();
        }


        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_WLP1001.GET_LIST",
                    new string[] { "A_FROM_DATE", "A_TO_DATE" },
                    new string[] {  dtpFromDate.DateTime.ToString("yyyyMMdd") , dtpToDate.DateTime.ToString("yyyyMMdd")}
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            //gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[3].DisplayFormat.FormatString = "n0";
            //for (int i = 9; i < gvList.Columns.Count - 2; i++)
            //{
            //    gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    gvList.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //}
            //gvList.OptionsView.ShowFooter = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(gleCategory.EditValue.NullString() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_116".Translation(), MsgType.Warning);
                return;
            }
            if (txtQuantity.EditValue.NullString() == "0" || txtQuantity.EditValue.NullString().Contains("."))
            {
                MsgBox.Show("MSG_ERR_048".Translation(), MsgType.Warning);
                return;
            }

            //if (txtMakeDate.EditValue.NullString() == string.Empty)
            //{
            //    MsgBox.Show("MSG_ERR_117".Translation(), MsgType.Warning);
            //    return;
            //}
            //if (txtExpDate.EditValue.NullString() == string.Empty)
            //{
            //    MsgBox.Show("MSG_ERR_118".Translation(), MsgType.Warning);
            //    return;
            //}
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1001.PUT_ITEM"
                    , new string[] {
                        "A_CODE",
                        "A_QUANTITY",
                        "A_MAKE_DATE",
                        "A_EXP_DATE",
                        "A_TRAN_USER"
                    }
                    , new string[] {
                        gleCategory.EditValue.NullString(),
                        txtQuantity.EditValue.NullString(),
                        txtMakeDate.DateTime.ToString("yyyyMMdd"),
                        txtExpDate.DateTime.ToString("yyyyMMdd"),
                        Consts.USER_INFO.Id
                    }
                    ); 

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    gleCategory.EditValue = null;
                    txtQuantity.Text = "0";
                    txtMakeDate.EditValue = null;
                    txtExpDate.EditValue = null;
                    SearchPage();
                    if (chkPrintLabel.Checked)
                    {
                        DataTable dtPrint = new DataTable();
                        dtPrint = base.m_ResultDB.ReturnDataSet.Tables[0];
                        UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
                        print.PrintTest(0, 0, dtPrint);
                    }
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
            //    string code = gvList.GetDataRow(indexRow)["CODE"].ToString().Trim();
            //    string lot = gvList.GetDataRow(indexRow)["LOT_NO"].ToString().Trim();
            //    string quantity = gvList.GetDataRow(indexRow)["SO_LUONG"].ToString().Trim();
            //    string make_date = gvList.GetDataRow(indexRow)["NGAY_SAN_XUAT"].ToString().Trim();
            //    string exp_date = gvList.GetDataRow(indexRow)["NGAY_HET_HAN"].ToString().Trim();
            //    gleCategory.EditValue = code;
            //    txtLot.Text = lot;
            //    txtQuanity.EditValue = quantity;
            //    txtMakeDate.EditValue = make_date;
            //    txtExpDate.EditValue = exp_date;
            //}
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

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
            //        double count = (exp_date - current_date).TotalDays;
            //        if(count <= 3 && count > 0)
            //        {
            //            e.Appearance.BackColor = Color.Yellow;
            //        }
            //        if(count <= 0)
            //        {
            //            e.Appearance.BackColor = Color.Red;
            //        }
            //    }
            //}
        }
    }
}
