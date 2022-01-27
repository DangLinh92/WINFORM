using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class NHAP_XUAT_KHO : PageType
    {
        public NHAP_XUAT_KHO()
        {
            InitializeComponent();
            this.Load += NHAP_XUAT_KHO_Load;
        }

        private void NHAP_XUAT_KHO_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "NHAP_XUAT_KHO");
            InitData();
        }

        private void InitData()
        {
            dateInput.EditValue = DateTime.Now;
            dateTranfer.EditValue = DateTime.Now;
            dateSearch.EditValue = DateTime.Now;
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@INIT_NHAP_XUAT", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection tableCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridLookEdit(stlDepartment, tableCollection[0], "CODE", "DEPARTMENT");
                    m_BindData.BindGridLookEdit(stlCode, tableCollection[1], "Id", "Name");

                    m_BindData.BindGridLookEdit(stlDeptFrom, tableCollection[0], "CODE", "DEPARTMENT");
                    m_BindData.BindGridLookEdit(stlDeptTo, tableCollection[0], "CODE", "DEPARTMENT");
                    m_BindData.BindGridLookEdit(stlCodeBelow, tableCollection[1], "Id", "Name");

                    gcList.DataSource = tableCollection[2];

                    gcReturnList.DataSource = tableCollection[3];
                    gvReturnList.OptionsView.ColumnAutoWidth = true;

                    gvList.OptionsView.ColumnAutoWidth = true;
                    gvList.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void cheInput_CheckedChanged(object sender, EventArgs e)
        {
            cheOutput.Checked = !cheInput.Checked;
        }

        private void cheOutput_CheckedChanged(object sender, EventArgs e)
        {
            cheInput.Checked = !cheOutput.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.EditValue.NullString() != "")
                {
                    MsgBox.Show("KHÔNG CHO PHÉP SỬA!".Translation(), MsgType.Warning);
                    return;
                }

                if (stlCode.EditValue.NullString() == "" || dateInput.EditValue.NullString() == "" || stlDepartment.EditValue.NullString() == "" || txtQuantity.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                if (!cheInput.Checked && !cheOutput.Checked)
                {
                    MsgBox.Show("CHỌN NHẬP HOẶC XUẤT".Translation(), MsgType.Warning);
                    return;
                }

                DateTime date = DateTime.Now;
                string dateIn = "";
                if (DateTime.TryParse(dateInput.EditValue.NullString(), out date))
                {
                    dateIn = date.ToString("yyyy-MM-dd");
                }

                string InOut = cheInput.Checked ? "IN" : "OUT";

                if (InOut == "IN")
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@NHAP_XUAT",
                    new string[] { "A_ID", "A_INOUT", "A_DEPT_CODE", "A_GAS_CODE", "A_QUANTITY", "A_DATE_TRAN", "A_USER", "A_NOTE" },
                    new string[]
                    {
                        txtID.EditValue.NullString(),
                        InOut,
                        stlDepartment.EditValue.NullString(),
                        stlCode.EditValue.NullString(),
                        txtQuantity.EditValue.NullString(),
                        dateIn,
                        txtUser.EditValue.NullString(),
                        txtNote.EditValue.NullString()
                    });
                }
                else
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@XUAT_KHO",
                       new string[] { "A_DEPT_CODE", "A_INOUT", "A_GAS_CODE", "A_QUANTITY", "A_DATE_TRAN", "A_USER", "A_NOTE" },
                       new string[]
                       {
                        stlDepartment.EditValue.NullString(),
                        InOut,
                        stlCode.EditValue.NullString(),
                        txtQuantity.EditValue.NullString(),
                        dateIn,
                        txtUser.EditValue.NullString(),
                        txtNote.EditValue.NullString()
                       });
                }

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    btnReload.PerformClick();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    if (txtID.EditValue.NullString() == "" || stlCode.EditValue.NullString() == "" || stlDepartment.EditValue.NullString() == "")
                    {
                        MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                        return;
                    }

                    string InOut = cheInput.Checked ? "IN" : "OUT";

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS.DELETE_NHAP_XUAT",
                       new string[] { "A_ID", "A_INOUT", "A_DEPT_CODE", "A_USER", "A_LOT_NO", "A_GAS_ID" },
                       new string[]
                       {
                        txtID.EditValue.NullString(),
                        InOut,
                        stlDepartment.EditValue.NullString(),
                        txtUser.EditValue.NullString(),
                        txtLotNo.EditValue.NullString(),
                         stlCode.EditValue.NullString(),
                       });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        btnReload.PerformClick();
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtID.EditValue = "";
            stlDepartment.EditValue = "";
            stlCode.EditValue = "";
            txtQuantity.EditValue = 0;
            txtLotNo.EditValue = "";
            dateInput.EditValue = DateTime.Now;
            txtUser.EditValue = "";
            txtNote.EditValue = "";
            cheInput.Checked = false;
            cheOutput.Checked = false;

            stlDepartment.Enabled = true;
            stlCode.Enabled = true;
            txtQuantity.Enabled = true;
            dateInput.Enabled = true;
            txtUser.Enabled = true;
            txtNote.Enabled = true;
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "Id");
                stlDepartment.EditValue = gvList.GetRowCellValue(e.RowHandle, "DEPARTMENT_ID");
                stlCode.EditValue = gvList.GetRowCellValue(e.RowHandle, "GAS_ID");
                txtQuantity.EditValue = gvList.GetRowCellValue(e.RowHandle, "QUANTITY");
                txtLotNo.EditValue = gvList.GetRowCellValue(e.RowHandle, "LOT_NO");
                txtNote.EditValue = gvList.GetRowCellValue(e.RowHandle, "NOTE");
                dateInput.EditValue = gvList.GetRowCellValue(e.RowHandle, "DATE_TRAN");
                cheInput.Checked = gvList.GetRowCellValue(e.RowHandle, "INOUT").NullString() == "NHAP" ? true : false;
                txtUser.EditValue = gvList.GetRowCellValue(e.RowHandle, "USER_UPDATE");

                stlDepartment.Enabled = false;
                stlCode.Enabled = false;
                txtQuantity.Enabled = false;
                txtLotNo.Enabled = false;
                dateInput.Enabled = false;
                txtUser.Enabled = false;
                txtNote.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSaveMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (stlCodeBelow.EditValue.NullString() == "" || stlDeptFrom.EditValue.NullString() == "" || stlDeptTo.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                DateTime date = DateTime.Now;
                string dateIn = "";
                if (DateTime.TryParse(dateTranfer.EditValue.NullString(), out date))
                {
                    dateIn = date.ToString("yyyy-MM-dd");
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS.LOAN_GAS",
                   new string[] { "A_GAS_ID", "A_QUANTITY", "A_DEPT_FROM", "A_DEPT_TO", "A_DATE", "A_USER" },
                   new string[]
                   {
                       stlCodeBelow.EditValue.NullString(),
                       txtQuantityLoan.EditValue.NullString(),
                       stlDeptFrom.EditValue.NullString(),
                       stlDeptTo.EditValue.NullString(),
                       dateIn,
                       Consts.USER_INFO.Id
                   });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    btnReload.PerformClick();
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

        private void btnClearMove_Click(object sender, EventArgs e)
        {
            stlCodeBelow.EditValue = "";
            stlDeptFrom.EditValue = "";
            stlDeptTo.EditValue = "";
            dateTranfer.EditValue = "";
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Clear();
            btnClearMove.PerformClick();
            InitData();
        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            POP.PRINT_LABEL pop = new POP.PRINT_LABEL();
            pop.ShowDialog();
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (e.Column.FieldName == "INOUT")
            {
                if (e.CellValue.NullString() == "NHAP")
                {
                    e.Appearance.BackColor = Color.FromArgb(46, 204, 113);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(250, 215, 160);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@SEARCH_NHAP_XUAT", new string[] { "A_DATE" }, new string[] { dateSearch.EditValue.NullString() });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection tableCollection = base.m_ResultDB.ReturnDataSet.Tables;

                    gcList.DataSource = tableCollection[0];
                    gvList.OptionsView.ColumnAutoWidth = true;
                    gvList.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
