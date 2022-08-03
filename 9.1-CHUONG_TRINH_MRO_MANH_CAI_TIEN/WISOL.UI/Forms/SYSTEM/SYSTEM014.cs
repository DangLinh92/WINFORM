using System;
using System.Data;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraEditors.Repository;

namespace Wisol.MES.Forms.SYSTEM
{
    public partial class SYSTEM014 :PageType
    {
        public SYSTEM014()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM014.INT_LIST"
                    , new string[] { "A_PLANT",
                        "A_LANG"
                    }
                    , new string[] { Consts.PLANT,
                        Consts.USER_INFO.Language
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
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
                    "PKG_SYSTEM014.GET_LIST",
                    new string[] { "A_PLANT",
                        "A_LANG",
                        "A_FROM_MONTHS",
                        "A_TO_MONTHS"
                    },
                    new string[]{Consts.PLANT,
                        Consts.USER_INFO.Language,
                        dtpFromMonth.DateTime.ToString("yyyyMM"),
                        dtpToMonth.DateTime.ToString("yyyyMM"),
                    }
                    );
                RepositoryItemCheckEdit repPostFlag = new RepositoryItemCheckEdit();
                repPostFlag.ValueChecked = "Y";
                repPostFlag.ValueGrayed = "N";
                repPostFlag.ValueUnchecked = "N";
                gvList.OptionsBehavior.Editable = true;
                gvList.Columns["POST_FLAG"].ColumnEdit = repPostFlag;
                gvList.Columns["POST_FLAG"].OptionsColumn.AllowEdit = true;
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
                dtpFromMonth.DateTime = DateTime.Now.AddMonths(-6);
                dtpToMonth.DateTime = DateTime.Now.AddMonths(6);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string strXml = string.Empty;

            try
            {
                strXml = Converter.GetDataTableToXml((gcList.DataSource as DataTable));
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SYSTEM014.PUT_LIST",
                    new string[]{"A_PLANT",
                        "A_XML",
                        "A_TRAN_USER_ID"
                    },
                    new string[]{Consts.PLANT,
                        strXml,
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
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

    }
}
