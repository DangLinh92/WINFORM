﻿using DevExpress.Utils;
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
    public partial class WLP1004 : PageType
    {
        public WLP1004()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1004.INT_LIST"
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
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            dtpFromDate.EditValue = new DateTime(DateTime.Now.Year, 6, 20);
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();
        }


        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1004.GET_LIST"
                    , new string[] { "A_FROM_DATE", "A_TO_DATE" },
                    new string[] { dtpFromDate.DateTime.ToString("yyyyMMdd"), dtpToDate.DateTime.ToString("yyyyMMdd") }
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

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtLotNo.EditValue.NullString() == string.Empty)
            {
                return;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1004.PUT_ITEM"
                    , new string[] {
                        "A_LOT_NO",
                        "A_TRAN_USER"
                    }
                    , new string[] {
                        txtLotNo.Text.Trim(),
                        Consts.USER_INFO.Id
                    }
                    ); 

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtLotNo.Text = string.Empty;
                    this.SearchPage();
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
