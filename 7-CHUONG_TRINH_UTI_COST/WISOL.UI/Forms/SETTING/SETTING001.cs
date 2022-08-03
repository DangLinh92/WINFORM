using System;
using System.Data;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING001 : PageType
    {
        DataTable dt_item_check_detail = new DataTable();
        DataTable dt_maintenance_detail = new DataTable();
        DataTable dt_location = new DataTable();
        DataTable dt_factory = new DataTable();

        public SETTING001()
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
                //base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.INT_LIST"
                //    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
                //    }
                //    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
                //    }
                //    );
                //if (base.m_ResultDB.ReturnInt == 0)
                //{
                //    base.m_BindData.BindGridView(gcList,
                //        base.m_ResultDB.ReturnDataSet.Tables[0]
                //        );

                //    Init_Control(true);
                //}

                this.SearchPage();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.GET_LIST"
                    , new string[] { "A_PLANT","A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );

                    dt_factory = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
                    base.m_BindData.BindGridLookEdit(gleFactory, dt_factory, "FACTORY_ID", "FACTORY_NAME");

                    dt_location = base.m_ResultDB.ReturnDataSet.Tables[2].Copy();
                    base.m_BindData.BindGridLookEdit(gleLocation, dt_location, "LOCATION_ID", "LOCATION_NAME");

                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtDeviceID.EditValue = string.Empty;
                txtCode.EditValue = string.Empty;
                txtDeviceName.EditValue = string.Empty;
                gleFactory.EditValue = string.Empty;
                gleLocation.EditValue = string.Empty;
                gleItemCheck.EditValue = string.Empty;
                gleMaintenance.EditValue = string.Empty;
                dtpTimeSetup.EditValue = string.Empty;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeviceID.Text))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDeviceName.Text.Trim()))
            {
                MsgBox.Show("Hãy nhập tên thiết bị.\r\nPlease enter Device Name.", MsgType.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(gleFactory.EditValue.ToString()))
            {
                MsgBox.Show("Hãy chọn nhà máy.\r\nPlease choose Factory.", MsgType.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(gleLocation.EditValue.ToString()))
            {
                MsgBox.Show("Hãy chọn vị trí.\r\nPlease choose location.", MsgType.Warning);
                return;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.PUT_SAVE"
                    , new string[] { "A_PLANT","A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_DEVICE_ID", "A_DEVICE_NAME", "A_LOCATION_ID", "A_TIME_SETUP"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     txtDeviceID.Text, txtDeviceName.Text.Trim(), gleLocation.EditValue.ToString(),
                                     dtpTimeSetup.Text.NullString()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    this.SearchPage();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string ID = string.Empty;
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    ID = gvList.GetDataRow(e.RowHandle)["ID"].NullString();

                    GetItemDetail(ID);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void GetItemDetail(string ID)
        {
            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.GET_ITEM"
                    , new string[] { "A_PLANT","A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_ID"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, ID
                    }
                    );
            if (base.m_ResultDB.ReturnInt == 0)
            {
                txtDeviceID.Text = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["DEVICE_ID"].ToString();
                txtCode.Text = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["CODE"].ToString();
                txtDeviceName.Text = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["DEVICE_NAME"].ToString();
                gleFactory.EditValue = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["FACTORY_ID"].ToString();
                gleLocation.EditValue = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["LOCATION_ID"].ToString();
                dtpTimeSetup.EditValue = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["TIME_SETUP"].ToString();

                dt_item_check_detail = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
                dt_maintenance_detail = base.m_ResultDB.ReturnDataSet.Tables[2].Copy();
                base.m_BindData.BindGridLookEdit(gleItemCheck, base.m_ResultDB.ReturnDataSet.Tables[1], "ITEM_CHECK_ID", "ITEM_CHECK_NAME");
                base.m_BindData.BindGridLookEdit(gleMaintenance, base.m_ResultDB.ReturnDataSet.Tables[2], "MAINTENANCE_ID", "MAINTENANCE_NAME");
            }
        }

        private void btnEditItemCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.EditValue.ToString()) || string.IsNullOrWhiteSpace(gleItemCheck.EditValue.ToString()))
            {
                return;
            }

            string temp = gleItemCheck.EditValue.ToString();
            DataRow[] dr_item_check = dt_item_check_detail.Select("ITEM_CHECK_ID = " + temp);
            string device_id = dr_item_check[0].ItemArray[0].ToString();
            string code = txtCode.Text;
            string device_name = txtDeviceName.Text;
            string id_check = dr_item_check[0].ItemArray[1].ToString();
            string name = dr_item_check[0].ItemArray[4].ToString();
            string min = dr_item_check[0].ItemArray[5].ToString();
            string max = dr_item_check[0].ItemArray[6].ToString();
            string other = dr_item_check[0].ItemArray[7].ToString();

            POP.POP_SETTING001_1 popup = new POP.POP_SETTING001_1(device_id, code, device_name, id_check, name, min, max, other);
            popup.ShowDialog();
            GetItemDetail(txtDeviceID.Text);
        }

        private void btnDeleteItemCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.EditValue.ToString()) || string.IsNullOrWhiteSpace(gleItemCheck.EditValue.ToString()))
            {
                return;
            }
            string item_check_id = gleItemCheck.EditValue.ToString();
            DialogResult dialogResult = MsgBox.Show("ITEM_CHECK: " + gleItemCheck.Text + ".\r\n" + "MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.DELETE_ITEM_CHECK"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                        }
                        , new string[] { Consts.PLANT, "",  Consts.USER_INFO.Id, Consts.USER_INFO.Language, item_check_id
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        dt_item_check_detail.Reset();
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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

                GetItemDetail(txtDeviceID.Text);
            }
        }

        private void btnEditMaintenance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.EditValue.ToString()) || string.IsNullOrWhiteSpace(gleMaintenance.EditValue.ToString()))
            {
                return;
            }

            string temp = gleMaintenance.EditValue.ToString();
            DataRow[] dr_maintenance = dt_maintenance_detail.Select("MAINTENANCE_ID = " + temp);
            string device_id = dr_maintenance[0].ItemArray[0].ToString();
            string code = txtCode.Text;
            string device_name = txtDeviceName.Text;
            string maintenance_id = dr_maintenance[0].ItemArray[1].ToString();
            string maintenance_name = dr_maintenance[0].ItemArray[4].ToString();
            string maintenance_days = dr_maintenance[0].ItemArray[5].ToString();
            string maintenance_hours = dr_maintenance[0].ItemArray[6].ToString();


            //POP.POP_SETTING001_2 popup = new POP.POP_SETTING001_2(device_id, code, device_name, maintenance_id, maintenance_name, maintenance_days, maintenance_hours);
            //popup.ShowDialog();
            //GetItemDetail(txtDeviceID.Text);
        }

        private void btnDeleteMaintenance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.EditValue.ToString()) || string.IsNullOrWhiteSpace(gleMaintenance.EditValue.ToString()))
            {
                return;
            }
            string maintenance_id = gleMaintenance.EditValue.ToString();
            DialogResult dialogResult = MsgBox.Show("MAINTENANCE: " + gleMaintenance.Text + ".\r\n" + "MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.DELETE_MAINTENANCE"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                        }
                        , new string[] { Consts.PLANT, "",  Consts.USER_INFO.Id, Consts.USER_INFO.Language, maintenance_id
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        dt_maintenance_detail.Reset();
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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

                GetItemDetail(txtDeviceID.Text);
            }
        }

        private void btnAddItemCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeviceID.Text))
            {
                MsgBox.Show("Hãy chọn thiết bị trước.\r\nPlease choose device first.", MsgType.Warning);
                return;
            }

            string device_id = txtDeviceID.Text.Trim();
            string code = txtCode.Text;
            string device_name = txtDeviceName.Text;
            string id_check = "";
            string name = "";
            string min = "";
            string max = "";
            string other = "";

            POP.POP_SETTING001_1 popup = new POP.POP_SETTING001_1(device_id, code, device_name, id_check, name, min, max, other);
            popup.ShowDialog();
            GetItemDetail(txtDeviceID.Text);
        }

        private void btnAddMaintenance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeviceID.Text))
            {
                MsgBox.Show("Hãy chọn thiết bị trước.\r\nPlease choose device first.", MsgType.Warning);
                return;
            }

            string device_id = txtDeviceID.Text.Trim();
            string code = txtCode.Text;
            string device_name = txtDeviceName.Text;


            //POP.POP_SETTING001_2 popup = new POP.POP_SETTING001_2(device_id, code, device_name, maintenance_id, maintenance_name, maintenance_days, maintenance_hours);
            //popup.ShowDialog();
            //GetItemDetail(txtDeviceID.Text);
        }

        private void btnAddNewDevice_Click(object sender, EventArgs e)
        {
            POP.POP_SETTING001_3 popup = new POP.POP_SETTING001_3(dt_factory, dt_location);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void btnDeleteDevice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeviceID.Text))
            {
                MsgBox.Show("Hãy chọn thiết bị trước.\r\nPlease choose device first.", MsgType.Warning);
                return;
            }

            DialogResult dialogResult = MsgBox.Show("Code: " + txtCode.Text + "\r\nName: " + txtDeviceName.Text + "\r\n" +  "MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING001.DELETE_DEVICE"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                        }
                        , new string[] { Consts.PLANT, "",  Consts.USER_INFO.Id, Consts.USER_INFO.Language, txtDeviceID.Text
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        this.SearchPage();
                        this.Init_Control(true);
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
        }

        private void gleFactory_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dt_F;
            string factoryID = gleFactory.EditValue.ToString();

            if(factoryID == "")
            {
                return;
            }

            dt_F = dt_location.Select("FACTORY_ID = " + factoryID).CopyToDataTable();

            base.m_BindData.BindGridLookEdit(gleLocation, dt_F, "LOCATION_ID", "LOCATION_NAME");
        }
    }
}
