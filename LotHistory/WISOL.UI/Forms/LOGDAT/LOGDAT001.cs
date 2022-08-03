using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.LOGDAT
{
    public partial class LOGDAT001 : PageType
    {
        public LOGDAT001()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_LOGDAT001.INT_LIST"
                    , new string[] { "A_DEPARTMENT", "A_ROLE"}
                    , new string[] { Consts.DEPARTMENT, Consts.USER_INFO.UserRole }
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
            try
            {
                base.m_BindData.BindGridView(gcList
                    , "PKG_LOGDAT001.GET_LIST"
                    , new string[] {
                        "A_FROM_DATE",
                        "A_TO_DATE",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] {
                        dtpFromDate.DateTime.ToString("yyyyMMdd"),
                        dtpToDate.DateTime.ToString("yyyyMMdd"),
                        gleUserId.EditValue.NullString(),
                        Consts.DEPARTMENT
                        }
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
                gleUserId.EditValue = string.Empty;
                dtpFromDate.DateTime = DateTime.Now;
                dtpToDate.DateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPage();
        }
    }
}
