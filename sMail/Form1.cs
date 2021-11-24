using DevExpress.Export;
using DevExpress.XtraEditors.Mask;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sMail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFrom.Text = "thidao335@wisol.co.kr";
            txtCc.Text = "thidao335@wisol.co.kr";
            Data = null;
            lblCount.Text = "COUNT: ";
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string url = Application.StartupPath + @"\\MailToList.xlsx";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "MailToList.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;

                    using (WebClient web1 = new WebClient())
                        web1.DownloadFile(url, path);
                    MessageBox.Show("LOAD FILE SUCCESS!");
                    Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.Message);
            }
        }
        DataTable Data;
        private void btnLoadTo_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn file danh sách gửi mail.";
            fileDialog.Filter = "All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                txtTo.EditValue = filePath;
                string excelcon;

                if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                {
                    excelcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
                }
                else
                {
                    excelcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
                }
                OleDbConnection conexcel = new OleDbConnection(excelcon);

                try
                {
                    conexcel.Open();
                    DataTable dtExcel = conexcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string sheetName = "DATA$";

                    foreach (DataRow drSheet in dtExcel.Rows)
                    {
                        if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                        {
                            sheetName = drSheet["TABLE_NAME"].ToString();
                            break;
                        }
                    }

                    OleDbCommand cmdexcel1 = new OleDbCommand();
                    cmdexcel1.Connection = conexcel;
                    cmdexcel1.CommandText = "select * from[" + sheetName + "]";

                    Data = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = cmdexcel1;
                    da.Fill(Data);

                    if (Data.Rows.Count > 0)
                    {
                        for (int i = 0; i < Data.Columns.Count; i++)
                        {
                            Data.Columns[i].ColumnName = Data.Rows[0][i].ToString();
                        }
                    }

                    Data.Columns.Add("MAP_FILE");
                    Data.Columns.Add("PATH_FILE");
                    Data.Columns.Add("SEND_OK");

                    conexcel.Close();

                    Data.Rows.RemoveAt(0);
                    gcList.DataSource = Data;
                }
                catch (Exception ex)
                {
                    conexcel.Close();
                    MessageBox.Show("Send error :" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Load folder contain file attach
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (Data == null || gvList.RowCount == 0)
            {
                MessageBox.Show("Hãy chọn danh sách gửi mail!");
                return;
            }

            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtAttachUrl.Text = folderDlg.SelectedPath;

                string[] fileEntries = Directory.GetFiles(folderDlg.SelectedPath);
                foreach (string fileName in fileEntries)
                {
                    for (int i = 0; i < gvList.RowCount; i++)
                    {
                        if (gvList.GetRowCellValue(i, "MaNV") != null && fileName.Contains(gvList.GetRowCellValue(i, "MaNV").ToString())) // Contain MaNV
                        {
                            gvList.SetRowCellValue(i, "MAP_FILE", "OK");
                            gvList.SetRowCellValue(i, "PATH_FILE", fileName);
                            break;
                        }
                    }
                }
            }
        }

        int countSuccess = 0;
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data == null || gvList.RowCount == 0)
                {
                    MessageBox.Show("Hãy chọn danh sách gửi mail!");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Xác nhận bạn có muốn gửi mail!", "CONFIRM", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
                Root.Enabled = false;
                progressFinishMail.Properties.Step = 1;
                progressFinishMail.Properties.PercentView = true;
                progressFinishMail.Properties.Maximum = gvList.RowCount - 1;
                progressFinishMail.Properties.Minimum = 0;
                countSuccess = 0;

                EmailSender emailSender;
                bool result;
                for (int i = 0; i < gvList.RowCount; i++)
                {
                    emailSender = new EmailSender();
                    emailSender.FROM_ADDRESS = txtFrom.Text.ToString().Trim();
                    emailSender.AddCcEmailAddress(txtCc.Text.Trim());
                    emailSender.Subject = txtSubject.Text.Trim();

                    if (gvList.GetRowCellValue(i, "Email") != null)
                    {
                        emailSender.AddToEmailAddress(gvList.GetRowCellValue(i, "Email").ToString());
                    }

                    if (gvList.GetRowCellValue(i, "PATH_FILE") != null && gvList.GetRowCellValue(i, "MAP_FILE") + "" == "OK")
                    {
                        emailSender.AddAttachmentFilePath(gvList.GetRowCellValue(i, "PATH_FILE").ToString());
                    }

                    emailSender.Body = richTextMailContent.HtmlText;

                    result = emailSender.Send();

                    if (result)
                    {
                        gvList.SetRowCellValue(i, "SEND_OK", "OK");
                        countSuccess++;
                    }
                    else
                    {
                        gvList.SetRowCellValue(i, "SEND_OK", "NG");
                    }


                    progressFinishMail.PerformStep();
                    progressFinishMail.Update();
                    Thread.Sleep(1000);
                }

                lblCount.Text += countSuccess + "/" + gvList.RowCount;
                MessageBox.Show("Hoàn thành gửi mail");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Send error :" + ex.Message);
            }
            finally
            {
                Root.Enabled = true;
            }
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (e.Column.FieldName == "MAP_FILE")
            {
                if (e.CellValue + "" == "OK")
                {
                    e.Appearance.BackColor = Color.FromArgb(125, 206, 160);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(253, 242, 233);
                }
            }

            if (e.Column.FieldName == "SEND_OK")
            {
                if (e.CellValue + "" == "OK")
                {
                    e.Appearance.BackColor = Color.FromArgb(125, 206, 160);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(236, 112, 99);
                }
            }
        }
    }
}
