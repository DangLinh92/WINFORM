using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Classes;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class COVID_CHECK : PageType
    {
        public COVID_CHECK()
        {
            InitializeComponent();
            this.Load += COVID_CHECK_Load;
        }

        private void COVID_CHECK_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "COVID_CHECK");
            Classes.Common.SelectPrinter(cboPrinter);
            dateSearch.EditValue = DateTime.Now;
            dateCheck.EditValue = DateTime.Now;
            GetLabelTemplate();

            Init();
        }

        private void Init()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.INIT", new string[] { "A_DATE" }, new string[] { DateTime.Parse(dateSearch.EditValue.NullString()).ToString("yyyy-MM-dd") });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection data = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridLookEdit(stlDeptCode, data[1], "CODE", "DEPARTMENT");
                    gcList.DataSource = data[2];
                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["PCR"].Width = 40;
                    gvList.Columns["QUICK_TEST"].Width = 60;
                    gvList.Columns["PCR_OK"].Width = 50;
                    gvList.Columns["QUICK_OK"].Width = 50;
                    gvList.Columns["NGAY_TEST"].Width = 70;
                    gvList.Columns["DEPT_CODE"].Width = 50;
                    gvList.Columns["CA_LAM_VIEC"].Width = 50;
                    gvList.Columns["THOI_GIAN_TEST"].Width = 40;
                    gvList.Columns["DIA_DIEM_TEST"].Width = 40;
                    gvList.Columns["NOTE"].Width = 150;
                    //gvList.OptionsView.ColumnAutoWidth = true;
                    gvList.OptionsSelection.MultiSelect = true;
                    gvList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                    //gvList.ClearColumnsFilter();
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

        private void txtQRCODE_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                InputLanguage originalInputLang = InputLanguage.CurrentInputLanguage;
                var lang = InputLanguage.InstalledInputLanguages.OfType<InputLanguage>().Where(l => l.Culture.Name.StartsWith("en")).FirstOrDefault();
                if (lang != null)
                {
                    InputLanguage.CurrentInputLanguage = lang;
                }

                if (e.KeyCode == Keys.Enter)
                {
                    gvList.ClearColumnsFilter();

                    lblQRCODE.Text = txtQRCODE.EditValue.NullString();

                    bool isChecked = false;
                    int rowHandle = -1;
                    string qrCode = txtQRCODE.EditValue.NullString().ToUpper().ToString(new CultureInfo("en-US"));

                    if (qrCode == "" || qrCode == null)
                    {
                        qrCode = txtQRCODE.EditValue + "";
                    }

                    if (cheIsPrintLabel.Checked)
                    {
                        Print(qrCode);
                    }

                    for (int i = 0; i < gvList.RowCount; i++)
                    {
                        if (qrCode.Contains(gvList.GetRowCellValue(i, "CODE").NullString()) && gvList.GetRowCellValue(i, "CODE").NullString() != "")
                        {
                            gvList.SelectRow(i);
                            isChecked = true;
                            rowHandle = i;
                            break;
                        }
                    }

                    if (isChecked && rowHandle >= 0)
                    {
                        txtID.EditValue = gvList.GetRowCellValue(rowHandle, "ID").NullString();
                        txtCode.EditValue = gvList.GetRowCellValue(rowHandle, "CODE").NullString();
                        txtName.EditValue = gvList.GetRowCellValue(rowHandle, "NAME").NullString();
                        stlDeptCode.EditValue = gvList.GetRowCellValue(rowHandle, "DEPT_CODE").NullString();
                        txtCalamviec.EditValue = gvList.GetRowCellValue(rowHandle, "CA_LAM_VIEC").NullString();
                        cheTested.Checked = true;

                        string OK_NG_PCR = gvList.GetRowCellValue(rowHandle, "PCR_OK").NullString();
                        string OK_NG_QUICK = gvList.GetRowCellValue(rowHandle, "QUICK_OK").NullString();

                        if (cboPCR_Quick.Text == "PCR")
                        {
                            OK_NG_PCR = cheTested.Checked ? "OK" : "NG";
                        }
                        else
                        {
                            OK_NG_QUICK = cheTested.Checked ? "OK" : "NG";
                        }

                        gvList.SetRowCellValue(rowHandle, "PCR_OK", OK_NG_PCR);
                        gvList.SetRowCellValue(rowHandle, "QUICK_OK", OK_NG_QUICK);

                        if (cboPCR_Quick.Text == "PCR")
                        {
                            gvList.SetRowCellValue(rowHandle, "PCR", cboPCR_Quick.Text);
                        }
                        else
                        {
                            gvList.SetRowCellValue(rowHandle, "QUICK_TEST", cboPCR_Quick.Text);
                        }

                        gvList.SetRowCellValue(rowHandle, "THOI_GIAN_TEST", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        gvList.SetRowCellValue(rowHandle, "DIA_DIEM_TEST", txtLocation.EditValue.NullString());
                        gvList.MakeRowVisible(rowHandle);
                    }
                    else
                    {
                        txtNote.EditValue = qrCode;

                        if (qrCode.NullString().Contains(" "))
                        {
                            txtCode.EditValue = qrCode.NullString().Split(' ')[0].NullString();
                            txtName.EditValue = qrCode.NullString().Substring(qrCode.IndexOf(' ')).NullString();
                            stlDeptCode.EditValue = qrCode.NullString().Substring(qrCode.LastIndexOf(' ')).NullString();
                            txtCalamviec.Text = "";
                            cheTested.Checked = true;
                            txtID.EditValue = "";
                            txtNote.EditValue = "PHAT SINH";
                        }
                        else
                        {
                            txtCode.EditValue = qrCode.NullString();
                            txtName.EditValue = "";
                            stlDeptCode.EditValue = "";
                            txtCalamviec.Text = "";
                            cheTested.Checked = true;
                            txtID.EditValue = "";
                            txtNote.EditValue = "PHAT SINH";
                        }
                    }

                    txtQRCODE.EditValue = "";

                    UpdateDataTest(rowHandle);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void UpdateDataTest(int rowHandle, bool isClick = false)
        {
            try
            {
                if (cheIsPrintLabel.Checked && isClick)
                {
                    Print(txtCode.EditValue.NullString() + " " + txtName.EditValue.NullString());
                }

                string dept = stlDeptCode.EditValue.NullString();
                string location = txtLocation.EditValue.NullString();

                string OK_NG_PCR = "";
                string OK_NG_QUICK = "";

                if (rowHandle >= 0)
                {
                    dept = stlDeptCode.EditValue.NullString() == "" ? gvList.GetRowCellValue(rowHandle, "DEPT_CODE").NullString() : stlDeptCode.EditValue.NullString();
                    location = txtLocation.EditValue.NullString() == "" ? gvList.GetRowCellValue(rowHandle, "DIA_DIEM_TEST").NullString() : txtLocation.EditValue.NullString();

                    OK_NG_PCR = gvList.GetRowCellValue(rowHandle, "PCR_OK").NullString();
                    OK_NG_QUICK = gvList.GetRowCellValue(rowHandle, "QUICK_OK").NullString();
                }

                if (cboPCR_Quick.Text == "PCR")
                {
                    OK_NG_PCR = cheTested.Checked ? "OK" : "NG";
                }
                else
                {
                    OK_NG_QUICK = cheTested.Checked ? "OK" : "NG";
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.UPDATE_ITEM",
                    new string[] { "A_ID", "A_DATE", "A_PCR_QUICK", "A_LOCATION", "A_MA_NV", "A_NAME", "A_DEPT_CODE", "A_OKE_NG_PCR", "A_OKE_NG_QUICK", "A_NOTE", "A_CA_LAM", "A_THOI_GIAN_TEST" },
                    new string[] { txtID.EditValue.NullString(), dateCheck.EditValue.NullString(), cboPCR_Quick.Text, location, txtCode.EditValue.NullString().ToUpper(), txtName.EditValue.NullString(), dept, OK_NG_PCR, OK_NG_QUICK, txtNote.EditValue.NullString(), txtCalamviec.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });

                if (base.m_ResultDB.ReturnInt != 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
                else
                {
                    if (isClick)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    }
                    Init();

                    if (txtID.EditValue.NullString() == "")
                    {
                        string code = "";
                        for (int i = 0; i < gvList.RowCount; i++)
                        {
                            code = gvList.GetRowCellValue(i, "CODE").NullString();
                            if (txtCode.EditValue.NullString().Contains(code))
                            {
                                gvList.MakeRowVisible(i);
                                break;
                            }
                        }
                    }

                    if (txtCode.EditValue.NullString() == "") // TH k co ma
                    {
                        if (gvList.RowCount > 0)
                        {
                            gvList.MakeRowVisible(gvList.RowCount - 1);
                        }
                    }

                    txtQRCODE.Text = "";
                    txtCode.EditValue = "";
                    txtName.EditValue = "";
                    stlDeptCode.EditValue = "";
                    cheTested.Checked = false;
                    txtCalamviec.Text = "";
                    txtID.EditValue = "";
                    txtNote.EditValue = "";
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateSearch.EditValue.NullString() == "")
                {
                    MsgBox.Show("HÃY CHỌN THỜI GIAN TÌM KIẾM!", MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.INIT", new string[] { "A_DATE" }, new string[] { DateTime.Parse(dateSearch.EditValue.NullString()).ToString("yyyy-MM-dd") });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[2];
                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["PCR"].Width = 40;
                    gvList.Columns["QUICK_TEST"].Width = 70;
                    gvList.Columns["PCR_OK"].Width = 50;
                    gvList.Columns["QUICK_OK"].Width = 50;
                    gvList.Columns["NGAY_TEST"].Width = 70;
                    gvList.Columns["DEPT_CODE"].Width = 50;
                    gvList.Columns["CA_LAM_VIEC"].Width = 50;
                    gvList.Columns["THOI_GIAN_TEST"].Width = 40;
                    gvList.Columns["DIA_DIEM_TEST"].Width = 40;
                    gvList.Columns["NOTE"].Width = 150;
                    //gvList.OptionsView.ColumnAutoWidth = true;
                    gvList.OptionsSelection.MultiSelect = true;
                    gvList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
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

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (gvList.GetRowCellValue(e.RowHandle, "PCR_OK").NullString() == "OK")
            {
                e.Appearance.BackColor = Color.LightGreen;
                gvList.SelectRow(e.RowHandle);
            }
            else if (gvList.GetRowCellValue(e.RowHandle, "QUICK_OK").NullString() == "OK")
            {
                if (e.Column.FieldName == "QUICK_OK")
                {
                    e.Appearance.BackColor = Color.FromArgb(253, 235, 208);
                    gvList.SelectRow(e.RowHandle);
                }
            }

        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                txtQRCODE.EditValue = "";
                txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "ID").NullString();
                txtCode.EditValue = gvList.GetRowCellValue(e.RowHandle, "CODE").NullString();
                txtName.EditValue = gvList.GetRowCellValue(e.RowHandle, "NAME").NullString();
                stlDeptCode.EditValue = gvList.GetRowCellValue(e.RowHandle, "DEPT_CODE").NullString();
                txtNote.EditValue = gvList.GetRowCellValue(e.RowHandle, "NOTE").NullString();
                txtLocation.EditValue = txtLocation.EditValue.NullString() == "" ? gvList.GetRowCellValue(e.RowHandle, "DIA_DIEM_TEST").NullString() : txtLocation.EditValue.NullString();

                if (cboPCR_Quick.Text == "PCR")
                {
                    cheTested.Checked = gvList.GetRowCellValue(e.RowHandle, "PCR_OK").NullString() == "OK";
                }
                else
                {
                    cheTested.Checked = gvList.GetRowCellValue(e.RowHandle, "QUICK_OK").NullString() == "OK";
                }

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (dateUploadForTest.EditValue.NullString() != "")
            {
                POP.IMPORT_EXCEL import = new POP.IMPORT_EXCEL();
                import.ImportType = "1";
                import.DateTest = dateUploadForTest.EditValue.NullString();
                import.ShowDialog();
                btnSearch.PerformClick();
            }
            else
            {
                MsgBox.Show("HÃY NHẬP NGÀY DỰ KIẾN TEST COVID", MsgType.Warning);
            }
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.GETALL", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.Columns["ID"].Visible = false;
                    gvList.OptionsView.ColumnAutoWidth = true;
                    gvList.OptionsSelection.MultiSelect = true;
                    gvList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
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

        private void gvList_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (gvList.FocusedRowHandle == e.RowHandle)
            //    e.Appearance.Assign(gvList.PaintAppearance.SelectedRow);
            //else
            //    e.Appearance.Assign(gvList.PaintAppearance.Row);
            //e.HighPriority = true;
        }

        private void cboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrinterClass.SetDefaultPrinter(cboPrinter.Text.Trim());
            GetLabelTemplate();
        }

        string label;
        private void GetLabelTemplate()
        {
            try
            {
                label = string.Empty;
                string LabelCode = "QRCODE_" + cboPrinter.Text;
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_LABEL.GET_TEMP", new string[] { "A_CODE_TEMP" }, new string[] { LabelCode });//QRCODE 
                if (m_ResultDB.ReturnInt == 0)
                {
                    if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        label = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["DATA_XML"].NullString();
                    }
                    else
                    {
                        MsgBox.Show("Không có File label cho printer " + cboPrinter.Text, MsgType.Warning);
                    }
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void Print(string code)
        {
            #region print
            string designFile = string.Empty;
            string xml_content_Original = string.Empty;
            string xml_content = label;
            try
            {
                designFile = "STOCK_LABEL.xml";

                string maNV = "";
                string nameNV = "";
                if (code.NullString().Contains(" "))
                {
                    try
                    {
                        maNV = code.NullString().Split(' ')[0].NullString();
                        nameNV = code.NullString().Substring(code.IndexOf(' ')).NullString();
                    }
                    catch (Exception)
                    {
                        maNV = code;
                        nameNV = "";
                    }
                }
                else
                {
                    maNV = code;
                    nameNV = "";
                }

                XtraReport reportPrint = new XtraReport();

                ReportPrintTool pt1 = new ReportPrintTool(reportPrint);
                pt1.PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);

                List<XtraReport> reports = new List<XtraReport>();

                xml_content = xml_content.Replace("$CODE$", maNV).Replace("$NAME$", nameNV).Replace("$DATE_PRINT$", DateTime.Now.ToString("yyyy-MM-dd"));

                xml_content = xml_content.Replace("&", "&amp;");
                File.WriteAllText(designFile, xml_content);

                XtraReport report = new XtraReport();
                report.PrintingSystem.ShowPrintStatusDialog = false;
                report.PrintingSystem.ShowMarginsWarning = false;
                report.LoadLayoutFromXml(designFile);

                int leftMargine = report.Margins.Left + 0;
                int rightMargine = report.Margins.Right;
                int topMargine = report.Margins.Top + 0;
                int bottomMargine = report.Margins.Bottom;
                if (leftMargine < 0)
                {
                    leftMargine = 0;
                }
                if (topMargine < 0)
                {
                    topMargine = 0;
                }
                report.Margins = new Margins(leftMargine, rightMargine, topMargine, bottomMargine);
                report.CreateDocument();

                reports.Add(report);
                File.Delete(designFile);

                foreach (XtraReport rp in reports)
                {
                    ReportPrintTool pts = new ReportPrintTool(rp);
                    pts.PrintingSystem.StartPrint += new PrintDocumentEventHandler(reportsStartPrintEventHandler);
                }

                pt1.PrintDialog();
                foreach (XtraReport rport in reports)
                {
                    ReportPrintTool pts = new ReportPrintTool(rport);
                    pts.Print();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            #endregion
        }

        private PrinterSettings prnSettings;
        private void reportsStartPrintEventHandler(object sender, PrintDocumentEventArgs e)
        {
            int pageCount = e.PrintDocument.PrinterSettings.ToPage;
            e.PrintDocument.PrinterSettings = prnSettings;

            // The following line is required if the number of pages for each report varies, 
            // and you consistently need to print all pages.
            e.PrintDocument.PrinterSettings.ToPage = pageCount;
        }

        private void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            prnSettings = e.PrintDocument.PrinterSettings;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDataTest(-1, true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQRCODE.Text = "";
            txtCode.EditValue = "";
            txtName.EditValue = "";
            stlDeptCode.EditValue = "";
            cheTested.Checked = false;
            txtCalamviec.Text = "";
            txtID.EditValue = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.DELETE", new string[] { "A_ID" }, new string[] { txtID.EditValue.NullString() });
                    if (m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        Init();
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                    }
                }
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
                string fileName = "";
                fileName = "DANH_SACH_TEST.xlsx";

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
