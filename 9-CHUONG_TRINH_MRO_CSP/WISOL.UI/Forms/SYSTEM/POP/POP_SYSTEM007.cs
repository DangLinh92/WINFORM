using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SYSTEM.POP
{
    public partial class POP_SYSTEM007 : FormType
    {

        public POP_SYSTEM007()
        {
            InitializeComponent();

            Init_Control();
        }





        private void Init_Control()
        {
            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SYSTEM007.GET_GROUP"
                    , new string[] { "A_PLANT",
                        "A_USER_ID"
                    }
                    , new string[] { Consts.PLANT,
                        Consts.USER_INFO.Id
                    }
                    );
                if (mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridView(gcList,
                        base.mResultDB.ReturnDataSet.Tables[0]
                        , false
                        , "REMARKS"
                        );
                }
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
                if (string.IsNullOrEmpty(txtGroupCode.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_018".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtGroupName.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_019".Translation(), MsgType.Warning);
                    return;
                }
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SYSTEM007.PUT_GROUP"
                    , new string[] { "A_PLANT" ,
                        "A_GROUP_CODE",
                        "A_GROUP_NAME",
                        "A_GROUP_NAME_KR",
                        "A_GROUP_NAME_VT",
                        "A_VIEW_GUBUN",
                        "A_USE_FLAG",
                        "A_REMARKS",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { Consts.PLANT ,
                        txtGroupCode.EditValue.NullString().ToUpper(),
                        txtGroupName.EditValue.NullString().ToUpper(),
                        txtGroupNameKR.EditValue.NullString().ToUpper(),
                        txtGroupNameVT.EditValue.NullString().ToUpper(),
                        rdgViewGubun.EditValue.NullString(),
                        rdgUseFlag.EditValue.NullString(),
                        txtRemarks.EditValue.NullString(),
                        Consts.USER_INFO.Id
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Information);
                    this.Init_Control();
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
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
                    txtGroupCode.EditValue = gvList.GetDataRow(e.RowHandle)["COMMGRP"].NullString();
                    txtGroupName.EditValue = gvList.GetDataRow(e.RowHandle)["COMMGRPNAME"].NullString();
                    txtGroupNameKR.EditValue = gvList.GetDataRow(e.RowHandle)["COMMGRPNAME_KR"].NullString();
                    txtGroupNameVT.EditValue = gvList.GetDataRow(e.RowHandle)["COMMGRPNAME_VT"].NullString();
                    rdgViewGubun.EditValue = gvList.GetDataRow(e.RowHandle)["VIEW_GUBUN"].NullString();
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
