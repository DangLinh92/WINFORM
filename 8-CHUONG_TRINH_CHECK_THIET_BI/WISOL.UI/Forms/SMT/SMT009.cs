using System;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT009 : PageType
    {
        public SMT009()
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
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT009.INT_LIST"
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

                    Init_Control(true);
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
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT009.GET_LIST"
                    , new string[] { "A_PLANT" , "A_MONTH",
                    }
                    , new string[] { Consts.PLANT, dtpYearMonth.DateTime.ToString("yyyyMM")
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtType.EditValue = string.Empty;
                txtCode.EditValue = string.Empty;
                txtPrice.EditValue = string.Empty;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtType.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_113".Translation(), MsgType.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtCode.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_114".Translation(), MsgType.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtPrice.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_115".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT009.PUT_ITEM"
                    , new string[] { 
                        "A_MONTH",
                        "A_TYPE",
                        "A_CODE",
                        "A_PRICE",
                        "A_EXPIRY_HOUR",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { 
                        dtpYearMonth.DateTime.ToString("yyyyMM"),
                        txtType.Text.Trim().ToUpper(),
                        txtCode.Text.Trim().ToUpper(),
                        txtPrice.Text.Trim(),
                        txtExpiryHour.Text.Trim(),
                        Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    txtType.EditValue = gvList.GetDataRow(e.RowHandle)["TYPE"].NullString();
                    txtCode.EditValue = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                    float price = float.Parse(gvList.GetDataRow(e.RowHandle)["PRICE"].NullString());
                    txtPrice.EditValue = price.ToString("#,##0.00");
                    float expiry = float.Parse(gvList.GetDataRow(e.RowHandle)["EXPIRY_HOUR"].NullString());
                    txtExpiryHour.EditValue = expiry.ToString("#,##0.00");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
