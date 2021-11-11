using System;
using System.Data;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING009 : PageType
    {
        public SETTING009()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            Classes.Common.SetFormIdToButton(this, "SETTING009");
        }
        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.INIT_LIST",
                    new string[] { },
                    new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridLookEdit(stlParent, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE", "NAME_LOCATION");
                    Init_Control(true);

                    tlList.KeyFieldName = "CODE";
                    tlList.ParentFieldName = "GROUP_LOCATION";
                    base.m_BindData.BindTreeList(tlList, base.m_ResultDB.ReturnDataSet.Tables[0], false, "CODE, GROUP_LOCATION");

                    tlList.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            base.InitializePage();
        }

        private void Init_Control(bool condFlag)
        {
            try
            {
                txtCode.EditValue = string.Empty;
                txtName.EditValue = string.Empty;
                txtDesc.EditValue = string.Empty;
                txtFloor.EditValue = string.Empty;
                stlParent.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtName.EditValue.NullString()) 
                    || string.IsNullOrEmpty(txtCode.EditValue.NullString()) 
                    || string.IsNullOrEmpty(txtFloor.EditValue.NullString()) 
                    || string.IsNullOrEmpty(stlParent.EditValue.NullString())
                   )
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.PUT_ITEM"
                    , new string[] { "A_CODE",
                        "A_NAME",
                        "A_FLOOR",
                        "A_GROUP",
                        "A_DESC",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] {
                        txtCode.EditValue.NullString(),
                        txtName.EditValue.NullString(),
                        txtFloor.EditValue.NullString(),
                        stlParent.EditValue.NullString().ToUpper(),
                        txtDesc.EditValue.NullString(),
                        Consts.USER_INFO.Id,
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
        public override void SearchPage()
        {
            base.SearchPage();
            try
            {

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.INIT_LIST",
                    new string[] { },
                    new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    //base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    base.m_BindData.BindGridLookEdit(stlParent, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE", "NAME_LOCATION");
                    Init_Control(true);
                    tlList.KeyFieldName = "CODE";
                    tlList.ParentFieldName = "GROUP_LOCATION";
                    base.m_BindData.BindTreeList(tlList, base.m_ResultDB.ReturnDataSet.Tables[0], false, "CODE, GROUP_LOCATION");
                    tlList.ExpandAll();
                }

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Init_Control(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                return;
            }

            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.DELETE_ITEM"
                        , new string[] { "A_CODE", "A_TRAN_USER"
                        }
                        , new string[] { txtCode.Text.Trim(), Consts.USER_INFO.Id  
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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
                SearchPage();
            }
        }

        private void tlList_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.GET_DETAIL"
                    , new string[] { "A_CODE"
                    }
                    , new string[] {(string)e.Node.GetValue("CODE")
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    txtCode.EditValue = dt.Rows[0]["CODE"].NullString();
                    txtName.EditValue = dt.Rows[0]["NAME"].NullString();
                    txtFloor.EditValue = dt.Rows[0]["FLOOR"].NullString();
                    txtDesc.EditValue = dt.Rows[0]["DESC"].NullString();
                    stlParent.EditValue = dt.Rows[0]["PARENT"].NullString();

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
