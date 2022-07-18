using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraCharts;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Spreadsheet;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using Wisol.Common;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT019 : PageType
    {
        public SMT019()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT019.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            this.timerSMT019.Enabled = true;
            this.timerSMT019.Interval = 5 * 60 * 1000;
            this.timerSMT019.Tick += new System.EventHandler(this.timerSMT019_Tick);

            base.InitializePage();
        }

        private void timerSMT019_Tick(object sender, EventArgs e)
        {
            this.SearchPage();
        }

        public override void SearchPage()
        {
            
            base.SearchPage();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT019.GET_LIST"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "STATUS")
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (cellValue == "Hãy kiểm tra lại thông tin")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                }
            }
        }

        private void gcList_DoubleClick(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtLotNo.Text.Trim() == string.Empty)
            {
                return;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT019.PUT_ITEM"
                    , new string[] { "A_LOT_NO", "A_NOTE", "A_TRAN_USER" }
                    , new string[] { txtLotNo.Text.Trim(), txtNote.Text.Trim(), Consts.USER_INFO.Id }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    this.SearchPage();
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
    }

}
