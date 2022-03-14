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
using DevExpress.XtraGrid.Columns;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using log4net;

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
            txtSubject.Text = "WHC - THÔNG TIN LƯƠNG THÁNG ";
            WriteLogFile.WriteLog("Form1_Load :" + DateTime.Now.ToString());
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string url = System.Windows.Forms.Application.StartupPath + @"\\DataMail.xlsx";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = "DataMail.xlsx";

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
                WriteLogFile.WriteLog("btnGetFileTemp_Click :" + ex.Message);
                MessageBox.Show("btnGetFileTemp_Click :" + ex.Message);
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
                        if (drSheet["TABLE_NAME"].NullString().Contains("$"))
                        {
                            sheetName = drSheet["TABLE_NAME"].NullString();
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
                            Data.Columns[i].ColumnName = Data.Rows[0][i].NullString();
                        }
                    }

                    // Data.Columns.Add("PATH_FILE");
                    Data.Columns.Add("SEND_OK");

                    conexcel.Close();

                    Data.Rows.RemoveAt(0);
                    gcList.DataSource = Data;

                    foreach (GridColumn col in gvList.Columns)
                    {
                        if (col.FieldName == "Email" || col.FieldName == "Code" || col.FieldName == "Ten" || col.FieldName == "BP" || col.FieldName == "SEND_OK")
                        {
                            gvList.Columns[col.FieldName].Visible = true;
                        }
                        else
                        {
                            gvList.Columns[col.FieldName].Visible = false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    conexcel.Close();
                    WriteLogFile.WriteLog("btnLoadTo_Click :" + ex.Message);
                    MessageBox.Show("btnLoadTo_Click :" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Load folder contain file attach
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnLoadFile_Click(object sender, EventArgs e)
        //{
        //    if (Data == null || gvList.RowCount == 0)
        //    {
        //        MessageBox.Show("Hãy chọn danh sách gửi mail!");
        //        return;
        //    }

        //    FolderBrowserDialog folderDlg = new FolderBrowserDialog();
        //    folderDlg.ShowNewFolderButton = true;

        //    // Show the FolderBrowserDialog.  
        //    DialogResult result = folderDlg.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        txtAttachUrl.Text = folderDlg.SelectedPath;

        //        string[] fileEntries = Directory.GetFiles(folderDlg.SelectedPath);
        //        foreach (string fileName in fileEntries)
        //        {
        //            for (int i = 0; i < gvList.RowCount; i++)
        //            {
        //                if (gvList.GetRowCellValue(i, "MaNV") != null && fileName.Contains(gvList.GetRowCellValue(i, "MaNV").ToString())) // Contain MaNV
        //                {
        //                    gvList.SetRowCellValue(i, "PATH_FILE", fileName);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

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

                if (datePaySlip.EditValue == null || datePaySlip.EditValue + "" == "")
                {
                    MessageBox.Show("Hãy chọn tháng năm xác nhận lương tháng !");
                    datePaySlip.Focus();
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Xác nhận bạn có muốn gửi mail!", "CONFIRM", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
                Root.Enabled = false;

                progressFinishMail.EditValue = 0;
                progressFinishMail.Properties.Step = 1;
                progressFinishMail.Properties.PercentView = true;
                progressFinishMail.Properties.Maximum = gvList.RowCount;
                progressFinishMail.Properties.Minimum = 0;
                progressFinishMail.BackColor = Color.Green;
                countSuccess = 0;
                lblCount.Text = "COUNT: ";

                EmailSender emailSender;
                bool result;
                for (int i = 0; i < gvList.RowCount; i++)
                {
                    emailSender = new EmailSender();
                    emailSender.FROM_ADDRESS = txtFrom.Text.NullString();
                    emailSender.AddCcEmailAddress(txtCc.Text.Trim());
                    emailSender.Subject = txtSubject.Text;

                    if (gvList.GetRowCellValue(i, "Email") != null)
                    {
                        emailSender.AddToEmailAddress(gvList.GetRowCellValue(i, "Email").NullString());
                    }

                    // emailSender.AddAttachmentFilePath(gvList.GetRowCellValue(i, "PATH_FILE").ToString());

                    emailSender.Body = GetBodyMail(i);


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
                    Thread.Sleep(100);
                }

                lblCount.Text += countSuccess + "/" + gvList.RowCount;
                MessageBox.Show("Hoàn thành gửi mail");

            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("btnSend_Click :" + ex.Message);
                MessageBox.Show("SbtnSend_Clickend error :" + ex.Message);
            }
            finally
            {
                Root.Enabled = true;
            }
        }

        private string GetBodyMail(int rowHandle)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Template.html";
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.Load(path);


                document.GetElementbyId("title_bangluong").InnerHtml += DateTime.Parse(datePaySlip.EditValue.ToString()).ToString("MM/yyyy");
                document.GetElementbyId("title_payslip").InnerHtml += DateTime.Parse(datePaySlip.EditValue.ToString()).ToString("Y");

                // NAME AND DEPT
                document.GetElementbyId("Name_NV").InnerHtml = gvList.GetRowCellValue(rowHandle, "Ten").NullString();
                document.GetElementbyId("Group_NV").InnerHtml = gvList.GetRowCellValue(rowHandle, "BP").NullString();
                document.GetElementbyId("Code").InnerHtml = gvList.GetRowCellValue(rowHandle, "Code").NullString();
                document.GetElementbyId("Date_join").InnerHtml = gvList.GetRowCellValue(rowHandle, "Ngayvao").NullString();

                // LUONG CO BAN
                document.GetElementbyId("LCB_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "LCB").NullString();
                document.GetElementbyId("PCDS_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "PCDS").NullString();
                document.GetElementbyId("PCTN_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "PCTN").NullString();
                document.GetElementbyId("Luong_Day_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Luong D").NullString();
                document.GetElementbyId("phep_nam").InnerHtml = gvList.GetRowCellValue(rowHandle, "Phép năm tồn").NullString();
                document.GetElementbyId("PCTNhiem_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "PCCV").NullString();
                document.GetElementbyId("PCDH_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "PCDH").NullString();
                document.GetElementbyId("Luong_H_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Luong H").NullString();

                // THOI GIAN LAM VIEC
                document.GetElementbyId("Thu_viec_ngay_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "TV ngay").NullString();
                document.GetElementbyId("thu_viec_dem130_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "TV dem").NullString();

                document.GetElementbyId("Chinh_thuc_ngay_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "CT ngay").NullString();
                document.GetElementbyId("chinh_thuc_dem130_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "CT dem").NullString();
                document.GetElementbyId("nghi_co_luong_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Nghi co luong").NullString();

                document.GetElementbyId("Tong_cong_BN_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong ngay").NullString();
                document.GetElementbyId("ngay_cong_dem_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong dem").NullString();
                document.GetElementbyId("tt_nghi_luong_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "T.Tien nghi co luong").NullString();
                document.GetElementbyId("nghi_khong_luong_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "nghi KL").NullString();
                document.GetElementbyId("tong_lam_viec_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "TT lviec").NullString();
                document.GetElementbyId("luong_theo_ngay_cong_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Luong theo ngay cong").NullString();

                // TANG CA - HO TRO LAM VIEC
                document.GetElementbyId("OT_time_150_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 150").NullString();
                document.GetElementbyId("OT_time_200_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 200").NullString();
                document.GetElementbyId("OT_time_210_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 210").NullString();
                document.GetElementbyId("OT_time_270_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 270").NullString();
                document.GetElementbyId("OT_time_300_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 300").NullString();
                document.GetElementbyId("OT_time_390_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 390").NullString();
                document.GetElementbyId("OT_time_260").InnerHtml = gvList.GetRowCellValue(rowHandle, "OT time 260").NullString();

                document.GetElementbyId("Cong15_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong1.5_OT").NullString();
                document.GetElementbyId("Cong20_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong2.0_OT").NullString();
                document.GetElementbyId("Cong21_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong2.1_OT").NullString();
                document.GetElementbyId("Cong27_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong2.7_OT").NullString();
                document.GetElementbyId("Cong30_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong3.0_OT").NullString();
                document.GetElementbyId("Cong39_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "COng3.9_OT").NullString();
                document.GetElementbyId("Cong26_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "COng2.6").NullString();


                document.GetElementbyId("HT_15_Total_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT 1.5 Total").NullString();
                document.GetElementbyId("HT_200_Total_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT 200 Total").NullString();
                document.GetElementbyId("HT_270_Total_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT 270 Total").NullString();
                document.GetElementbyId("HT_300_Total_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT 300 Total").NullString();
                document.GetElementbyId("HT_390_Total_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT 390 Total").NullString();

                document.GetElementbyId("Cong15_HT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong1.5").NullString();
                document.GetElementbyId("Cong20_HT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong2.0").NullString();
                document.GetElementbyId("Cong27_HT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong2.7").NullString();
                document.GetElementbyId("Cong30_HT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong3.0").NullString();
                document.GetElementbyId("Cong39_HT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "COng3.9").NullString();

                document.GetElementbyId("Tong_OT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Tong OT").NullString();
                document.GetElementbyId("Tong_HTLV_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Tổng HTLV").NullString();

                // HO TRO THANH LAP CONG TY
                document.GetElementbyId("Ca_ngay_TV_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Ca ngày TV").NullString();
                document.GetElementbyId("Ca_ngay_CT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Ca ngày CT").NullString();
                document.GetElementbyId("Ca_dem_TV_truoc_le_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "ca đêm TV kỷ niệm trước lễ").NullString();
                document.GetElementbyId("Ca_dem_CT_truoc_le_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "ca đêm kỷ niệm CT trước lễ").NullString();
                document.GetElementbyId("Thanh_tien_truoc_le_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Thành tiền").NullString();

                // NGHI BU
                document.GetElementbyId("Nghi_Bu_CT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Nghi Bu CT").NullString();
                document.GetElementbyId("Ho_tro_PC_NB_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Hỗ trợ PC NB").NullString();
                document.GetElementbyId("Ho_tro_luong30_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Hỗ trợ lương NB").NullString();
                document.GetElementbyId("Tong_ho_tro_NB_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Tổng hỗ trợ NB").NullString();

                // LUU TRU CONG TY
                document.GetElementbyId("so_ngay_nghi_70_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "số ngày nghỉ 70%").NullString();
                document.GetElementbyId("So_ngay_luu_tru_cty_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Số ngày lưu trú Cty").NullString();
                document.GetElementbyId("Thanh_tien_nghi_70_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Thành tiền nghỉ 70%").NullString();
                document.GetElementbyId("Thanh_tien_luu_tru_cty_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Thành tiền lưu trú cty").NullString();
                document.GetElementbyId("Ho_tro_dthoai_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Hỗ trợ điện thoại").NullString();

                // KHOAN CONG KHAC
                document.GetElementbyId("Cong_them_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong them").NullString();
                document.GetElementbyId("Chuyen_can_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Chuyencan").NullString();
                document.GetElementbyId("Incentive_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Incentive").NullString();
                document.GetElementbyId("HT_gui_tre_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT gui tre").NullString();
                document.GetElementbyId("HT_PCCC_co_so_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT PCCC co so").NullString();
                document.GetElementbyId("HT_ATNVSV_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT ATNVSV").NullString();
                document.GetElementbyId("HT_CD_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "HT CĐ").NullString();
                document.GetElementbyId("TN_Khac_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "TN khac").NullString();
                document.GetElementbyId("TT_TV_dem_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "T.tien_TV").NullString();
                document.GetElementbyId("TT_CT_dem_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "T.tien_CT").NullString();
                document.GetElementbyId("Dem_TV_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Dem TV").NullString();
                document.GetElementbyId("Dem_CT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Dem CT").NullString();

                // CAC KHOAN KHAU TRU
                document.GetElementbyId("Cong_tru_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong tru").NullString();
                document.GetElementbyId("BH_XH_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "BHXH").NullString();
                document.GetElementbyId("TRUY_THU_BHYT_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Truy thu BHYT").NullString();
                document.GetElementbyId("Cong_doan_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Cong doan").NullString();
                document.GetElementbyId("Thue_TNCN_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "thue TNCN").NullString();
                document.GetElementbyId("hmuon_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "hmuon").NullString();
                document.GetElementbyId("Di_muon_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Di muon").NullString();
                document.GetElementbyId("tru_khac_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "tru khac").NullString();
                document.GetElementbyId("Truy_Thu_PN_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Truy thu PN").NullString();
                document.GetElementbyId("TTT_LamTet_Id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Thanh Toan Tien Lam Tet").NullString(); 
                 

                // THUC NHAN
                document.GetElementbyId("Thuc_nhan_id").InnerHtml = gvList.GetRowCellValue(rowHandle, "Thuc nhan").NullString();
                

                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    stream.Position = 0;
                    return new StreamReader(stream).ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("GetBodyMail :" + ex.Message);
                MessageBox.Show("GetBodyMail :" + ex.Message);
            }

            return "";
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

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

        #region DELETE
        //private void SaveDocument()
        //{
        //    try
        //    {
        //        Object oMissing = System.Reflection.Missing.Value;

        //        Object oTemplatePath = System.Windows.Forms.Application.StartupPath + "\\MAU_BANG_LUONG.dotx";

        //        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

        //        string folderPath = System.Windows.Forms.Application.StartupPath + "\\PAYSLIP";

        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }
        //        else
        //        {
        //            Directory.Delete(folderPath, true);
        //        }

        //        foreach (DataRow row in Data.Rows)
        //        {
        //            Document wordDoc = new Document();
        //            wordDoc = wordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

        //            foreach (Field myMergeField in wordDoc.Fields)
        //            {
        //                Range rngFieldCode = myMergeField.Code;

        //                String fieldText = rngFieldCode.Text;

        //                // ONLY GETTING THE MAILMERGE FIELDS
        //                if (fieldText.StartsWith(" MERGEFIELD"))
        //                {
        //                    // THE TEXT COMES IN THE FORMAT OF

        //                    // MERGEFIELD  MyFieldName  \\* MERGEFORMAT

        //                    // THIS HAS TO BE EDITED TO GET ONLY THE FIELDNAME "MyFieldName"

        //                    Int32 endMerge = fieldText.IndexOf("\\");

        //                    Int32 fieldNameLength = fieldText.Length - endMerge;

        //                    String fieldName = fieldText.Substring(11, endMerge - 11);

        //                    // GIVES THE FIELDNAMES AS THE USER HAD ENTERED IN .dot FILE

        //                    fieldName = fieldName.Trim();

        //                    // **** FIELD REPLACEMENT IMPLEMENTATION GOES HERE ****//

        //                    // THE PROGRAMMER CAN HAVE HIS OWN IMPLEMENTATIONS HERE

        //                    if (fieldName == "Thang_nam")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }

        //                    if (fieldName == "Thang_nam_text")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }
        //                    if (fieldName == "Ten")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }

        //                    if (fieldName == "BP")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }

        //                    if (fieldName == "Code")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }

        //                    if (fieldName == "Ngayvao")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }

        //                    // THONG TIN CO BAN
        //                    if (fieldName == "LCB")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("Vivek");
        //                    }

        //                    if (fieldName == "PCDS")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("500000");
        //                    }

        //                    if (fieldName == "PCCV")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Luong_D")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "PHEP_NAM_TON")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "PCTN")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "PCDH")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "LUONG_H")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // thoi gian lam viec
        //                    if (fieldName == "TV_ngay")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TV_dem")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "CT_ngay")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "CT_dem")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Nghi_co_luong")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong_ngay")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong_dem")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TTien_nghi_co_luong")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "nghi_KL")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TT_Lviec")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Luong_theo_ngay_cong")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // TANG CA
        //                    if (fieldName == "OT_time_150")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "OT_time_200")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "OT_time_210")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "OT_time_270")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "OT_time_300")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "OT_time_390")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "OT_time_260")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    //
        //                    if (fieldName == "Cong15_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong20_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong21_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong27_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong30_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong39_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong26_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Tong_OT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // Ho tro thoi gian lam viec
        //                    if (fieldName == "HT_15_Total")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_200_Total")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_270_Total")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_300_Total")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_390_Total")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    //. so tien
        //                    if (fieldName == "Cong15_HT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong20_HT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong27_HT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong30_HT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong39_HT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Tong_HTLV")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // HO TRO THANH LAP CTY
        //                    if (fieldName == "Ca_ngay_TV")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Ca_ngay_CT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Ca_dem_TV_truoc_le")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Ca_dem_CT_truoc_le")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Thanh_tien_truoc_le")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // NGHI BU
        //                    if (fieldName == "Nghi_Bu_CT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Ho_tro_PC_NB")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Ho_tro_luong30")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Tong_ho_tro_NB")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // LUU TRU CONG TY
        //                    if (fieldName == "so_ngay_nghi_70")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "So_ngay_luu_tru_cty")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Thanh_tien_nghi_70")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Thanh_tien_luu_tru_cty")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Ho_tro_dthoai")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // Cac khoan khac
        //                    if (fieldName == "Cong_them")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Chuyen_can")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Incentive")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_gui_tre")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_PCCC_co_so")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_ATNVSV")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "HT_CD")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TN_Khac")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TT_TV_dem")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Dem_TV")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Dem_CT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TT_CT_dem")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    // KHOAN KHAU TRU
        //                    if (fieldName == "Cong_tru")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "BH_XH")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "TRUY_THU_BHYT")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Cong_doan")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Thue_TNCN")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Di_muon")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "tru_khac")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "hmuon")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }

        //                    if (fieldName == "Thuc_nhan")
        //                    {
        //                        myMergeField.Select();
        //                        wordApp.Selection.TypeText("120000000");
        //                    }
        //                }
        //            }

        //            try
        //            {
        //                string filePath = folderPath + "\\" + row["Code"] + "_PAYSLIP.docx";
        //                wordDoc.SaveAs(filePath);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Send error :" + ex.Message);
        //            }

        //        }

        //        //wordApp.Documents.Open(filePath);
        //        wordApp.Documents.Close();
        //        wordApp.Application.Quit();


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Send error :" + ex.Message);
        //    }
        //}
        #endregion
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datePaySlip_EditValueChanged(object sender, EventArgs e)
        {
            if (datePaySlip.EditValue != null && datePaySlip.EditValue.NullString() != "" && DateTime.TryParse(datePaySlip.EditValue.ToString(), out _))
            {
                txtSubject.Text += (DateTime.Parse(datePaySlip.EditValue.ToString()).Month.ToString() + "/" + DateTime.Parse(datePaySlip.EditValue.ToString()).Year.ToString());
            }
        }
    }
}
