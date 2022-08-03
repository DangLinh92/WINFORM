using DevExpress.XtraReports.UI;
using System;
using System.IO;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM005 : PageType
    {
        public SYSTEM005()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM005.INT_LIST"
                    , new string[] { "A_PLANT",
                        "A_LANG",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        Consts.USER_INFO.Language,
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0],
                        false,
                        "PASSWORD, REMARKS, USER_ROLE"
                        );

                    Init_Control();
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
                base.m_BindData.BindGridView(gcList,
                    "PKG_SYSTEM005.GET_LIST",
                    new string[] { "A_PLANT",
                        "A_LANG",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    },
                    new string[] { Consts.PLANT,
                        Consts.USER_INFO.Language,
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    }
                    ,
                    false,
                    "PASSWORD, REMARKS, USER_ROLE"
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void Init_Control()
        {
            try
            {

                txtUserId.EditValue = string.Empty;
                txtUserName.EditValue = string.Empty;
                txtPassword.EditValue = string.Empty;
                rdgUseFlag.EditValue = "Y";
                txtRemarks.EditValue = string.Empty;
                txtPhoneNum.EditValue = string.Empty;
                txtEmail.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtUserId.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_106".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_107".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_108".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM005.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_USER_ID",
                        "A_PASSWORD",
                        "A_USER_NAME",
                        "A_PHONE_NUM",
                        "A_EMAIL",
                        "A_USEFLAG",
                        "A_REMARKS",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        txtUserId.EditValue.NullString(),
                        txtPassword.EditValue.NullString(),
                        txtUserName.EditValue.NullString(),
                        txtPhoneNum.EditValue.NullString(),
                        txtEmail.EditValue.NullString(),
                        rdgUseFlag.EditValue.NullString(),
                        txtRemarks.EditValue.NullString(),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    SearchPage();
                    if (chkInitData.Checked)
                    {
                        this.Init_Control();
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



        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    txtUserId.EditValue = gvList.GetDataRow(e.RowHandle)["USER_ID"].NullString();
                    txtPassword.EditValue = gvList.GetDataRow(e.RowHandle)["PASSWORD"].NullString();
                    txtUserName.EditValue = gvList.GetDataRow(e.RowHandle)["USER_NAME"].NullString();
                    txtRemarks.EditValue = gvList.GetDataRow(e.RowHandle)["REMARKS"].NullString();
                    rdgUseFlag.EditValue = gvList.GetDataRow(e.RowHandle)["USEFLAG"].NullString();
                    txtPhoneNum.EditValue = gvList.GetDataRow(e.RowHandle)["PHONE_NUM"].NullString();
                    txtEmail.EditValue = gvList.GetDataRow(e.RowHandle)["EMAIL"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
