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
using Microsoft.Office.Interop.Word;

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
            richTextMailContent.LoadDocument("H1611001_PAYSLIP.docx");
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string url = System.Windows.Forms.Application.StartupPath + @"\\MailToList.xlsx";

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
        System.Data.DataTable Data;
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
                    System.Data.DataTable dtExcel = conexcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

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

                    Data = new System.Data.DataTable();
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

                    //SaveDocument();
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
                    //emailSender.AddCcEmailAddress(txtCc.Text.Trim());
                    emailSender.Subject = txtSubject.Text.Trim();

                    if (gvList.GetRowCellValue(i, "Email") != null)
                    {
                        emailSender.AddToEmailAddress(gvList.GetRowCellValue(i, "Email").ToString());
                    }

                    if (gvList.GetRowCellValue(i, "PATH_FILE") != null && gvList.GetRowCellValue(i, "MAP_FILE") + "" == "OK")
                    {
                        emailSender.AddAttachmentFilePath(gvList.GetRowCellValue(i, "PATH_FILE").ToString());
                    }

                    emailSender.Body = File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\Template.html");//richTextMailContent.HtmlText;

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

        private void SaveDocument()
        {
            try
            {
                Object oMissing = System.Reflection.Missing.Value;

                Object oTemplatePath = System.Windows.Forms.Application.StartupPath + "\\MAU_BANG_LUONG.dotx";

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

                string folderPath = System.Windows.Forms.Application.StartupPath + "\\PAYSLIP";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                else
                {
                    Directory.Delete(folderPath, true);
                }

                foreach (DataRow row in Data.Rows)
                {
                    Document wordDoc = new Document();
                    wordDoc = wordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                    foreach (Field myMergeField in wordDoc.Fields)
                    {
                        Range rngFieldCode = myMergeField.Code;

                        String fieldText = rngFieldCode.Text;

                        // ONLY GETTING THE MAILMERGE FIELDS
                        if (fieldText.StartsWith(" MERGEFIELD"))
                        {
                            // THE TEXT COMES IN THE FORMAT OF

                            // MERGEFIELD  MyFieldName  \\* MERGEFORMAT

                            // THIS HAS TO BE EDITED TO GET ONLY THE FIELDNAME "MyFieldName"

                            Int32 endMerge = fieldText.IndexOf("\\");

                            Int32 fieldNameLength = fieldText.Length - endMerge;

                            String fieldName = fieldText.Substring(11, endMerge - 11);

                            // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE

                            fieldName = fieldName.Trim();

                            // **** FIELD REPLACEMENT IMPLEMENTATION GOES HERE ****//

                            // THE PROGRAMMER CAN HAVE HIS OWN IMPLEMENTATIONS HERE

                            if (fieldName == "Thang_nam")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }

                            if (fieldName == "Thang_nam_text")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }
                            if (fieldName == "Ten")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }

                            if (fieldName == "BP")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }

                            if (fieldName == "Code")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }

                            if (fieldName == "Ngayvao")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }

                            // THONG TIN CO BAN
                            if (fieldName == "LCB")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("Vivek");
                            }

                            if (fieldName == "PCDS")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("500000");
                            }

                            if (fieldName == "PCCV")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Luong_D")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "PHEP_NAM_TON")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "PCTN")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "PCDH")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "LUONG_H")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // thoi gian lam viec
                            if (fieldName == "TV_ngay")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TV_dem")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "CT_ngay")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "CT_dem")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Nghi_co_luong")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong_ngay")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong_dem")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TTien_nghi_co_luong")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "nghi_KL")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TT_Lviec")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Luong_theo_ngay_cong")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // TANG CA
                            if (fieldName == "OT_time_150")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "OT_time_200")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "OT_time_210")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "OT_time_270")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "OT_time_300")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "OT_time_390")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "OT_time_260")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            //
                            if (fieldName == "Cong15_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong20_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong21_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong27_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong30_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong39_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong26_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Tong_OT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // Ho tro thoi gian lam viec
                            if (fieldName == "HT_15_Total")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_200_Total")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_270_Total")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_300_Total")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_390_Total")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            //. so tien
                            if (fieldName == "Cong15_HT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong20_HT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong27_HT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong30_HT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong39_HT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Tong_HTLV")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // HO TRO THANH LAP CTY
                            if (fieldName == "Ca_ngay_TV")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Ca_ngay_CT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Ca_dem_TV_truoc_le")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Ca_dem_CT_truoc_le")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Thanh_tien_truoc_le")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // NGHI BU
                            if (fieldName == "Nghi_Bu_CT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Ho_tro_PC_NB")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Ho_tro_luong30")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Tong_ho_tro_NB")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // LUU TRU CONG TY
                            if (fieldName == "so_ngay_nghi_70")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "So_ngay_luu_tru_cty")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Thanh_tien_nghi_70")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Thanh_tien_luu_tru_cty")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Ho_tro_dthoai")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // Cac khoan khac
                            if (fieldName == "Cong_them")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Chuyen_can")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Incentive")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_gui_tre")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_PCCC_co_so")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_ATNVSV")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "HT_CD")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TN_Khac")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TT_TV_dem")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Dem_TV")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Dem_CT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TT_CT_dem")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            // KHOAN KHAU TRU
                            if (fieldName == "Cong_tru")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "BH_XH")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "TRUY_THU_BHYT")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Cong_doan")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Thue_TNCN")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Di_muon")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "tru_khac")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "hmuon")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }

                            if (fieldName == "Thuc_nhan")
                            {
                                myMergeField.Select();
                                wordApp.Selection.TypeText("120000000");
                            }
                        }
                    }

                    try
                    {
                        string filePath = folderPath + "\\" + row["Code"] + "_PAYSLIP.docx";
                        wordDoc.SaveAs(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Send error :" + ex.Message);
                    }

                }

                //wordApp.Documents.Open(filePath);
                wordApp.Documents.Close();
                wordApp.Application.Quit();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Send error :" + ex.Message);
            }
        }
    }
}
