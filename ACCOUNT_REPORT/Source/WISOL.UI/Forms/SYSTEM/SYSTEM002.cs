using PROJ_B_DLL.Objects;
using System;
using System.IO;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM002 : PageType
    {

        private string menualName = string.Empty;


        public SYSTEM002()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            Classes.Common.SetFormIdToButton(this, "SYSTEM002");
        }

        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM002.INT_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT" }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT }
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
            base.SearchPage();
            //try
            //{
            //    base.m_BindData.BindGridView(gcList,
            //        "PKG_SYSTEM002.GET_LIST",
            //        new string[] { "A_PLANT" },
            //        new string[] { Consts.PLANT },
            //        false,
            //        "REMARKS"
            //        );
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM002.GET_LIST"
                    , new string[] { "A_PLANT",
                        "A_LANG",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        Consts.USER_INFO.Language,
                        Consts.DEPARTMENT
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



        private void Init_Control()
        {
            try
            {
                txtFormCode.EditValue = string.Empty;
                txtFormName.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtFormCode.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_102".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtFormName.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_103".Translation(), MsgType.Warning);
                    return;
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM002.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_FORM",
                        "A_FORMNAME",
                        "A_USEFLAG",
                        "A_REMARKS",
                        "A_TRAN_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        txtFormCode.EditValue.NullString(),
                        txtFormName.EditValue.NullString(),
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
                        this.InitializePage();
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
                    txtFormCode.EditValue = gvList.GetDataRow(e.RowHandle)["FORM"].NullString();
                    txtFormName.EditValue = gvList.GetDataRow(e.RowHandle)["FORMNAME"].NullString();
                    txtRemarks.EditValue = gvList.GetDataRow(e.RowHandle)["REMARKS"].NullString();
                    rdgUseFlag.EditValue = gvList.GetDataRow(e.RowHandle)["USEFLAG"].NullString();
                    menualName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gvList.FocusedColumn.FieldName == "MANUAL_FILE")
                {
                    if (gvList.GetDataRow(gvList.FocusedRowHandle)["MANUAL_FILE"].NullString() == string.Empty)
                    {
                        return;
                    }
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.FileName = gvList.GetDataRow(gvList.FocusedRowHandle)["MANUAL_FILE"].NullString();
                        sfd.Title = "Save Excel File";
                        DialogResult result = sfd.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            Wisol.DataAcess.FileAccess fileAccess = new Wisol.DataAcess.FileAccess(Consts.SERVICE_INFO.ServiceIp);
                            FileObject fileObject = fileAccess.GetFile("MANUAL/" + gvList.GetDataRow(gvList.FocusedRowHandle)["MANUAL_FILE"].NullString());
                            File.WriteAllBytes(sfd.FileName, fileObject.FileContent);
                        }
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
