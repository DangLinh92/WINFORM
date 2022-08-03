using System;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT007 : PageType
    {
        public SMT007()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
        }



        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT007.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            txtMaintenanceDay.Properties.MinValue = 0;
            txtMaintenanceDay.Properties.MaxValue = 999;
            txtMaintenanceDay.Properties.Mask.EditMask = "\\d+";
            txtMaintenanceDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            txtPickupCount.Properties.MinValue = 0;
            txtPickupCount.Properties.MaxValue = 99999999;
            txtPickupCount.Properties.Mask.EditMask = "###,###,##0";
            //txtPickupCount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT007.GET_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            gvList.OptionsView.ShowFooter = false;
            gvList.Columns["Maintenance_Day"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Maintenance_Day"].DisplayFormat.FormatString = "n0";// "{0:##.#;;\"\"}";
            gvList.Columns["Pickup_Count"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["Pickup_Count"].DisplayFormat.FormatString = "n0";// "{0:##.#;;\"\"}";
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtLine.EditValue = string.Empty;
                txtMachineName.EditValue = string.Empty;
                txtMaintenanceDay.EditValue = 0;
                txtPickupCount.EditValue = 0;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLine.EditValue.NullString()) == true)
                {
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT007.PUT_ITEM"
                    , new string[] { "A_LINE",
                        "A_MACHINE",
                        "A_MAINTENANCE_DAY",
                        "A_HEAD",
                        "PICKUP_COUNT",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { txtLine.EditValue.NullString(),
                        txtMachineName.EditValue.NullString(),
                        txtMaintenanceDay.EditValue.NullString(),
                        txtHead.EditValue.NullString(),
                        txtPickupCount.EditValue.NullString(),
                        Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    SearchPage();
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
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
                else
                {
                    txtLine.EditValue = gvList.GetDataRow(e.RowHandle)["Line"].NullString();
                    txtMachineName.EditValue = gvList.GetDataRow(e.RowHandle)["Machine_Name"].NullString();
                    txtHead.EditValue = gvList.GetDataRow(e.RowHandle)["Head"].NullString();
                    txtMaintenanceDay.EditValue = gvList.GetDataRow(e.RowHandle)["Maintenance_Day"].NullString();
                    txtPickupCount.EditValue = gvList.GetDataRow(e.RowHandle)["Pickup_Count"].NullString();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLine.Text.Trim()))
            {
                return;
            }
            string Lot = txtLine.Text;
            string Machine_Name = txtMachineName.Text;
            string Head = txtHead.Text;
            DialogResult dialogResult = MsgBox.Show("Lot: " + Lot + "\r\n" + "Machine: " + Machine_Name + ", Head: " + Head + "\r\n" +  "Xác nhận Reset?", MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT007.PUT_RESET"
                        , new string[] { "A_LINE", "A_MACHINE", "A_HEAD", "A_TRAN_USER_ID"
                        }
                        , new string[] { Lot, Machine_Name, Head, Consts.USER_INFO.Id
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show("Reset thành công.", MsgType.Information);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
                SearchPage();
            }
        }
    }
}
