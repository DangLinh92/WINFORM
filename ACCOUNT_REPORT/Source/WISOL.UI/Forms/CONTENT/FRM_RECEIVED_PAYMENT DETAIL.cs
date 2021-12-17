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
    public partial class FRM_RECEIVED_PAYMENT_DETAIL : PageType
    {
        public FRM_RECEIVED_PAYMENT_DETAIL()
        {
            InitializeComponent();
            this.Load += FRM_RECEIVED_PAYMENT_DETAIL_Load;
        }

        private void FRM_RECEIVED_PAYMENT_DETAIL_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_RECEIVED_PAYMENT_DETAIL");
            dateFrom.EditValue = DateTime.Now;
            InitData();
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_RECEIVE_PAYMENT_MONTHLY_DETAIL.INIT",
                    new string[] { "A_DATE" },
                    new string[] { dateFrom.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection dataCollection = base.m_ResultDB.ReturnDataSet.Tables;

                    m_BindData.BindGridView(gcList, dataCollection[0]);
                   
                    gvList.OptionsView.ColumnAutoWidth = true;

                    gvList.Columns["DEBIT_AMT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["DEBIT_AMT"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["DEBIT_AMT_LOCAL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["DEBIT_AMT_LOCAL"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["CREDIT_AMT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["CREDIT_AMT_LOCAL"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["EXCHANGE_PAYMENT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["EXCHANGE_PAYMENT"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["EXCHANGE_RECEIVE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["EXCHANGE_RECEIVE"].DisplayFormat.FormatString = "n2";
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if(dateFrom.EditValue.ToString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                POP.IMPORT_EXCEL pop = new POP.IMPORT_EXCEL();
                pop.ImportType = Consts.IMPORT_TYPE_RECEIVE_PAYMENT;
                pop.Date = DateTime.Parse(dateFrom.EditValue.ToString());
                pop.ExPay = "0";
                pop.ExRev = "0";
                pop.IsPlant = chePlant.Checked == true ? "Y" : "N";
                pop.ShowDialog();
                InitData();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "GL_DATA.xlsx";

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

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            InitData();
        }
    }
}
