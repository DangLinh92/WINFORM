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
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class NHAN_VIEN : PageType
    {
        public NHAN_VIEN()
        {
            InitializeComponent();
            this.Load += NHAN_VIEN_Load;
        }

        private void NHAN_VIEN_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "NHAN_VIEN");
            Init();
        }

        private void Init()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN.INIT", new string[] {}, new string[] {});
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    // base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.OptionsView.ColumnAutoWidth = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            POP.IMPORT_EXCEL import = new POP.IMPORT_EXCEL();
            import.ImportType = "0";
            import.ShowDialog();
            Init();
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                fileName = "DANH_SACH_NHAN_VIEN.xlsx";

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
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
