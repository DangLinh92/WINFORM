using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM006 : PageType
    {
        public SYSTEM006()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            Classes.Common.SetFormIdToButton(this, "SYSTEM006");
        }

        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM006.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_USER_ID" }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT,Consts.USER_INFO.Id }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0],
                        false,
                        "REMARKS"
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
            InitializePage();
        }



        private void Init_Control()
        {
            try
            {
                txtUserRole.EditValue = string.Empty;
                txtUserRoleName.EditValue = string.Empty;
                rdgUseFlag.EditValue = "Y";
                txtRemarks.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtUserRole.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_104".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtUserRoleName.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_105".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM006.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_USERROLE",
                        "A_USERROLE_NAME",
                        "A_USEFLAG",
                        "A_REMARKS",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        txtUserRole.EditValue.NullString(),
                        txtUserRoleName.EditValue.NullString(),
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
                    txtUserRole.EditValue = gvList.GetDataRow(e.RowHandle)["USERROLE"].NullString();
                    txtUserRoleName.EditValue = gvList.GetDataRow(e.RowHandle)["USERROLE_NAME"].NullString();
                    txtRemarks.EditValue = gvList.GetDataRow(e.RowHandle)["REMARKS"].NullString();
                    rdgUseFlag.EditValue = gvList.GetDataRow(e.RowHandle)["USEFLAG"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

    }
}
