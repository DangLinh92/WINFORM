using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.LOGDAT
{
    public partial class LOGDAT002 : PageType
    {

        string fromDate = string.Empty;
        string toDate = string.Empty;
        public LOGDAT002()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_LOGDAT002.INT_LIST"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    base.m_BindData.BindGridLookEdit(gleUserId,
                        base.m_ResultDB.ReturnDataSet.Tables[1],
                        "USER_ID",
                        "USER_NAME"
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
            string userID = gleUserId.EditValue.NullString();
            try
            {
                base.m_BindData.BindGridView(gcList
                    , "PKG_LOGDAT002.GET_LIST"
                    , new string[] { "A_PLANT",
                        "A_FROM_DATE",
                        "A_TO_DATE",
                        "A_USER_ID",
                        "A_LANG"
                    }
                    , new string[] { Consts.PLANT,
                        dtpFromDate.DateTime.ToString("yyyyMMdd"),
                        dtpToDate.DateTime.ToString("yyyyMMdd"),
                        userID,
                        Consts.USER_INFO.Language
                        }
                    );
                fromDate = dtpFromDate.DateTime.ToString("yyyyMMdd");
                toDate = dtpToDate.DateTime.ToString("yyyyMMdd");

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
                gleUserId.EditValue = string.Empty;
                dtpFromDate.DateTime = DateTime.Now;
                dtpToDate.DateTime = DateTime.Now;
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
                if (gvList.FocusedRowHandle < 0)
                {
                    return;
                }
                POP.POP_LOGDAT002 popup = new POP.POP_LOGDAT002(fromDate, toDate, gvList.GetDataRow(gvList.FocusedRowHandle)["FORM_CODE"].NullString(), gvList.GetDataRow(gvList.FocusedRowHandle)["USER_ID"].NullString());
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


    }
}
