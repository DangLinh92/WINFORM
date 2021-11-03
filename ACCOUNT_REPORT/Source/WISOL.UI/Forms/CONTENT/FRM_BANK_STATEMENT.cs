using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class FRM_BANK_STATEMENT : PageType
    {
        public FRM_BANK_STATEMENT()
        {
            InitializeComponent();
            this.Load += FRM_BANK_STATEMENT_Load;
        }

        private void FRM_BANK_STATEMENT_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_BANK_STATEMENT");
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "BANK_STATEMENT.xlsx";

                string url = Application.StartupPath + @"\\" + fileName;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (WebClient web1 = new WebClient())
                        web1.DownloadFile(url, saveFileDialog.FileName);

                    MsgBox.Show("MSG_COM_001".Translation(), MsgType.Information);

                    //open file 
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            POP.IMPORT_EXCEL pop = new POP.IMPORT_EXCEL();
            pop.ImportType = Consts.IMPORT_TYPE_BANK_STATEMENT;
            pop.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK_STATEMENT.GET_BY_TIME", 
                    new string[] { "A_DATE_FROM", "A_DATE_TO" }, 
                    new string[] { dateFrom.EditValue.NullString(), dateTo.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                     //base.m_BindData.BindGridView(gcList, data);
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.OptionsView.ColumnAutoWidth = true;
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }
        }
    }
}
