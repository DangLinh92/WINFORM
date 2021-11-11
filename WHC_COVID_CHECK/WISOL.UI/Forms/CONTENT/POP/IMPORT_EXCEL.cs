using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT.POP
{
    public partial class IMPORT_EXCEL : FormType
    {
        public IMPORT_EXCEL()
        {
            InitializeComponent();
        }

        public string ImportType = "";
        public string DateTest = "";
        DataTable Data;

        private void IMPORT_EXCEL_Load(object sender, EventArgs e)
        {
            Data = new DataTable();
            Classes.Common.SetFormIdToButton(null, "IMPORT_EXCEL", this);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (Data.Rows.Count > 0)
            {
                Import();
            }
        }

        private void Import()
        {
            try
            {
                DialogResult dialogResult = MsgBox.Show("MSG_IMPORT_EXCEL".Translation(), MsgType.Information, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    if (ImportType == "0")
                    {
                        base.mResultDB = base.mDBaccess.ExcuteProcWithTableParam("PKG_BUSINESS_NHANVIEN.IMPORT", new string[] { }, "A_DATA", new string[] { }, Data);
                    }
                    else
                    {
                        if (DateTest != "")
                        {
                            base.mResultDB = base.mDBaccess.ExcuteProcWithTableParam("PKG_BUSINESS_NHANVIEN_TEST.IMPORT", new string[] { "A_DATE" }, "A_DATA", new string[] { DateTest }, Data);
                        }
                        else
                        {
                            MsgBox.Show("HÃY CHỌN NGÀY DỰ KIẾN TEST COVID!", MsgType.Warning);
                        }
                    }

                    if (mResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Information);
                        this.Close();
                    }
                    else
                    {
                        MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            //string name file;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel Files (.xls*)|*.xls*|All Files (*.*)|*.*";
            dlg.Multiselect = false;

            DialogResult dlgResult = dlg.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                txtFilePath.Text = dlg.FileName;
                if (txtFilePath.Text.Equals(string.Empty))
                {
                    lblMsg.Text = "Please Load File First!!!";
                    return;
                }
                if (!File.Exists(txtFilePath.Text))
                {
                    lblMsg.Text = "Can not Open File!!!";
                    return;
                }
                string filePath = txtFilePath.Text;
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
                    conexcel.Close();

                    Data.Rows.RemoveAt(0);

                    base.mBindData.BindGridView(gcList, Data);
                    //gvList.DeleteRow(0);
                }
                catch (Exception ex)
                {
                    conexcel.Close();
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
