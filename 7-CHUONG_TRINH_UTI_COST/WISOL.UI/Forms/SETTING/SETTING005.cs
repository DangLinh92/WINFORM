using System;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING005 : PageType
    {
        public SETTING005()
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
            //try
            //{
            //    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.INT_LIST"
            //        , new string[] { "A_PLANT", "A_DEPARTMENT"
            //        }
            //        , new string[] { Consts.PLANT, Consts.DEPARTMENT
            //        }
            //        );
            //    if (base.m_ResultDB.ReturnInt == 0)
            //    {
            //        base.m_BindData.BindGridView(gcList,
            //            base.m_ResultDB.ReturnDataSet.Tables[0]
            //            );

            //        Init_Control(true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
            this.SearchPage();

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT
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
                txtCodeGroup.EditValue = string.Empty;
                txtMakerName.EditValue = string.Empty;
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
                //if (string.IsNullOrEmpty(txtCodeGroup.EditValue.NullString()) == true)
                //{
                //    MsgBox.Show("MSG_ERR_022".Translation(), MsgType.Warning);
                //    return;
                //}

                //if (string.IsNullOrEmpty(txtMakerName.EditValue.NullString()) == true)
                //{
                //    MsgBox.Show("MSG_ERR_023".Translation(), MsgType.Warning);
                //    return;
                //}
                if (string.IsNullOrEmpty(txtCodeGroup.EditValue.NullString()) || string.IsNullOrEmpty(txtMakerName.EditValue.NullString())
                   )
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                if (gvList.DataRowCount > 0)
                {
                    if (txtCodeGroup.ReadOnly == false)
                    {
                        for (int i = 0; i < gvList.DataRowCount; i++)
                        {
                            if (gvList.GetRowCellValue(i, "CODE").ToString().ToUpper() == txtCodeGroup.EditValue.ToString().ToUpper())
                            {
                                MsgBox.Show("MSG_ERR_056".Translation(), MsgType.Warning);
                                return;
                            }
                        }
                    }
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.PUT_ITEM"
                    , new string[] { "A_CODE",
                        "A_MAKER_NAME",
                        "A_DEPARTMENT",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] {
                        txtCodeGroup.EditValue.NullString().ToUpper(),
                        txtMakerName.EditValue.NullString(),
                        Consts.DEPARTMENT,
                        Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    SearchPage();
                    txtCodeGroup.ReadOnly = true;
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
                    txtCodeGroup.EditValue = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                    txtMakerName.EditValue = gvList.GetDataRow(e.RowHandle)["MAKER_NAME"].NullString();
                    txtCodeGroup.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            this.Init_Control(true);
            txtCodeGroup.ReadOnly = false;
            this.ActiveControl = txtCodeGroup;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCodeGroup.Text.Trim() == string.Empty)
            {
                return;
            }

            string code = txtCodeGroup.Text.Trim();
            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING005.DELETE_ITEM"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                        }
                        , new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Id, Consts.USER_INFO.Language, code
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
    }
}
