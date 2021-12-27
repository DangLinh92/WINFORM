using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;
using Wisol.MES.Classes;
using Wisol.Objects;
using PROJ_B_DLL.Objects;
using System.Windows.Forms;

namespace Wisol.MES.UserClass
{
    public class PrintLabel
    {
        public ResultDB resultDB = null;
        public DBAccess dBaccess = null;

        public PrintLabel(DBAccess _mDBaccess)
        {
            this.dBaccess = _mDBaccess;
        }


        public void PrintTest(int leftPoint, int topPoint, DataTable dtPrint)
        {
            string designFile = string.Empty;
            string xml_content_Original = string.Empty;
            string xml_content = string.Empty;

            try
            {
                SaveLeftTopPoint(leftPoint, topPoint);
                resultDB = dBaccess.ExcuteProc("PKG_COMM.GET_DESIGNER",
                    new string[]{
                        "A_NAME_OF_LABEL"
                    },
                    new string[]{"LABEL_STOCK"
                    }
                    );
                if (resultDB.ReturnInt == 0)
                {
                    xml_content_Original = resultDB.ReturnDataSet.Tables[0].Rows[0]["XML_CONTENT"].NullString();
                }
                if (xml_content_Original == string.Empty)
                {
                    return;
                }
                designFile = "STOCK_LABEL.xml";


                XtraReport reportPrint = new XtraReport();
                for (int i = 0; i < dtPrint.Rows.Count; i++)
                {
                    xml_content = xml_content_Original.Replace("$Lot_No$", "LOT: " +  dtPrint.Rows[i]["LOT_NO"].ToString().ToUpper());
                    if (Consts.DEPARTMENT.ToUpper() == "WLP1")
                    {
                        xml_content = xml_content.Replace("$EXP$", "EXP: " + dtPrint.Rows[i]["EXP_DATE"].ToString().ToUpper());
                    }
                    else
                    {
                        xml_content = xml_content.Replace("$EXP$", "SPEC: " + dtPrint.Rows[i]["SPEC"].ToString().ToUpper());
                    }
                    xml_content = xml_content.Replace("$BARCODE$", dtPrint.Rows[i]["LOT_NO"].ToString().ToUpper()) ;

                    xml_content = SetLanguage(xml_content);
                    xml_content = xml_content.Replace("&", "&amp;");
                    File.WriteAllText((i + 1).NullString() + designFile, xml_content);

                    XtraReport report = new XtraReport();

                    report.PrintingSystem.ShowPrintStatusDialog = false;
                    report.PrintingSystem.ShowMarginsWarning = false;

                    report.LoadLayoutFromXml((i + 1).NullString() + designFile);
                    int leftMargine = report.Margins.Left + leftPoint;
                    int rightMargine = report.Margins.Right;
                    int topMargine = report.Margins.Top + topPoint;
                    int bottomMargine = report.Margins.Bottom;
                    if (leftMargine < 0)
                    {
                        leftMargine = 0;
                    }
                    if (topMargine < 0)
                    {
                        topMargine = 0;
                    }
                    report.Margins = new System.Drawing.Printing.Margins(leftMargine, rightMargine, topMargine, bottomMargine);
                    report.CreateDocument();
                    reportPrint.Pages.AddRange(report.Pages);
                    File.Delete((i + 1).NullString() + designFile);
                }
                reportPrint.PrintingSystem.ShowPrintStatusDialog = false;
                reportPrint.PrintingSystem.ShowMarginsWarning = false;
                //reportPrint.CreateDocument();
                //reportPrint.ShowPreview();
                reportPrint.Print();
                reportPrint.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, new object[] { true });
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        public void PrintUTI(int leftPoint, int topPoint, string input)
        {
            string designFile = string.Empty;
            string xml_content_Original = string.Empty;
            string xml_content = string.Empty;

            try
            {
                SaveLeftTopPoint(leftPoint, topPoint);
                resultDB = dBaccess.ExcuteProc("PKG_COMM.GET_DESIGNER_UTI",
                    new string[]{
                        "A_NAME_OF_LABEL"
                    },
                    new string[]{"LABEL_BCH"
                    }
                    );
                if (resultDB.ReturnInt == 0)
                {
                    xml_content_Original = resultDB.ReturnDataSet.Tables[0].Rows[0]["XML_CONTENT"].NullString();
                }
                if (xml_content_Original == string.Empty)
                {
                    return;
                }
                designFile = "LABEL_BCH.xml";


                XtraReport reportPrint = new XtraReport();
                for (int i = 0; i < 1; i++)
                {
                    //xml_content = xml_content_Original.Replace("$Lot_No$", "LOT: " + dtPrint.Rows[i]["LOT_NO"].ToString().ToUpper());
                    //xml_content = xml_content.Replace("$CODE$", "EXP: " + dtPrint.Rows[i]["EXP_DATE"].ToString().ToUpper());
                    //xml_content = xml_content.Replace("$BARCODE$", dtPrint.Rows[i]["LOT_NO"].ToString().ToUpper());
                    xml_content = xml_content_Original.Replace("$CODE$", input.ToUpper());
                    xml_content = xml_content.Replace("$BARCODE$", input.ToUpper());

                    xml_content = SetLanguage(xml_content);
                    xml_content = xml_content.Replace("&", "&amp;");
                    File.WriteAllText((i + 1).NullString() + designFile, xml_content);

                    XtraReport report = new XtraReport();

                    report.PrintingSystem.ShowPrintStatusDialog = false;
                    report.PrintingSystem.ShowMarginsWarning = false;

                    report.LoadLayoutFromXml((i + 1).NullString() + designFile);
                    int leftMargine = report.Margins.Left + leftPoint;
                    int rightMargine = report.Margins.Right;
                    int topMargine = report.Margins.Top + topPoint;
                    int bottomMargine = report.Margins.Bottom;
                    if (leftMargine < 0)
                    {
                        leftMargine = 0;
                    }
                    if (topMargine < 0)
                    {
                        topMargine = 0;
                    }
                    report.Margins = new System.Drawing.Printing.Margins(leftMargine, rightMargine, topMargine, bottomMargine);
                    report.CreateDocument();
                    reportPrint.Pages.AddRange(report.Pages);
                    File.Delete((i + 1).NullString() + designFile);
                }
                reportPrint.PrintingSystem.ShowPrintStatusDialog = false;
                reportPrint.PrintingSystem.ShowMarginsWarning = false;
                //reportPrint.CreateDocument();
                //reportPrint.ShowPreview();
                reportPrint.Print();
                reportPrint.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, new object[] { true });
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void SaveLeftTopPoint(int leftPoint, int topPoint)
        {
            try
            {
                Consts.X_POINT = leftPoint;
                Consts.Y_POINT = topPoint;
                if (File.Exists(Consts.LABELPOINT))
                {
                    File.Delete(Consts.LABELPOINT);
                }
                File.WriteAllText(Consts.LABELPOINT, leftPoint.NullString() + "/" + topPoint.NullString());
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private string SetLanguage(string fileContent)
        {
            try
            {
                string[] glsrList = fileContent.Split('!');
                string content = string.Empty;
                for (int i = 0; i < glsrList.Length; i++)
                {
                    if (glsrList[i].Length < 30)
                    {
                        glsrList[i] = glsrList[i].Translation();
                    }
                    content += glsrList[i];
                }
                return content;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
                return string.Empty;
            }
        }
    }
}
