using System;
using System.Data;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING008 : PageType
    {
        private string ctgId = string.Empty;

        public SETTING008()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();

            Classes.Common.SetFormIdToButton(this, "SETTING008");
        }

        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.INIT_LIST", new string[] { }, new string[] { });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    Init_Control(true);
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
                if (string.IsNullOrEmpty(txtCode.EditValue.NullString()) || string.IsNullOrEmpty(txtName.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_004".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.PUT_ITEM",
                    new string[] { "A_CODE", "A_NAME", "A_DESC", "A_USER_ID" },
                    new string[]
                    {
                        txtCode.EditValue.NullString(),
                        txtName.EditValue.NullString(),
                        txtDesc.EditValue.NullString(),
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

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_LIST", new string[] { }, new string[] { });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
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
                    Console.WriteLine(ctgId);
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.DELETE_ITEM",
                        new string[] { "A_CODE", "A_TRAN_USER" },
                        new string[] { ctgId, Consts.USER_INFO.Id });
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

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }
                else
                {
                    try
                    {
                        ctgId = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING008.GET_DETAIL",
                            new string[] { "A_CODE" },
                            new string[] { ctgId });
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                            txtCode.EditValue = dt.Rows[0]["CODE"].NullString();
                            txtName.EditValue = dt.Rows[0]["NAME_CATEGORY"].NullString();
                            txtDesc.EditValue = dt.Rows[0]["DESC"].NullString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
