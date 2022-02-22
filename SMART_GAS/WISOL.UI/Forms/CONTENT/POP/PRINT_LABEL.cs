using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Classes;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT.POP
{
    public partial class PRINT_LABEL : FormType
    {
        public PRINT_LABEL()
        {
            InitializeComponent();
            this.Load += PRINT_LABEL_Load;
        }

        private void PRINT_LABEL_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(null, "PRINT_LABEL", this);
            Classes.Common.SelectPrinter(cboPrinter);
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_BUSINESS@INIT_PRINTLABEL",
                 new string[] { },
                 new string[] { });

                if (mResultDB.ReturnInt != 0)
                {
                    
                    MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Error);
                }
                else
                {
                    mBindData.BindGridLookEdit(stlGas, mResultDB.ReturnDataSet.Tables[0], "Id", "Name");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtQuantity.EditValue.NullString() == "")
            {
                return;
            }

            #region print
            string designFile = string.Empty;
            string xml_content_Original = string.Empty;
            string xml_content = label;
            try
            {
                string code = stlGas.EditValue.NullString();
                designFile = "STOCK_LABEL.xml";


                XtraReport reportPrint = new XtraReport();

                ReportPrintTool pt1 = new ReportPrintTool(reportPrint);
                pt1.PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);

                List<XtraReport> reports = new List<XtraReport>();

                for (int i = 0; i < int.Parse(txtQuantity.EditValue.NullString()); i++)
                {

                    xml_content = xml_content.Replace("$CODE$", code).Replace("$BARCODE$", code).Replace("$QUANTITY$", "QTY: 1EA");
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
                }

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
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_BUSINESS_LABEL.GET_TEMP", new string[] { }, new string[] {  });//QRCODE 
                if (mResultDB.ReturnInt == 0)
                {
                    if (base.mResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        label = base.mResultDB.ReturnDataSet.Tables[0].Rows[0]["CONTENT_LABEL"].NullString();
                    }
                    else
                    {
                        MsgBox.Show("Không có File label cho printer " + cboPrinter.Text, MsgType.Warning);
                    }
                }
                else
                {
                    MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
