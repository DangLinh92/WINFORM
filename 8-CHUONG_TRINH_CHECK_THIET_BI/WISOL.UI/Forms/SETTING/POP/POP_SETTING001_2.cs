using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Wisol.Common;
using Wisol.Components;
using Wisol.Objects;

using Wisol.MES.Inherit;
using Wisol.MES.Classes;
using Wisol.MES.Dialog;
using System.Text.RegularExpressions;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_SETTING001_2 : FormType
    {
        public string luongchuanhap { get; set; }
        public string soluongwafer { get; set; }

        public POP_SETTING001_2()
        {
            InitializeComponent();
            //this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public POP_SETTING001_2(string device_id, string code, string device_name, string maintenance_id, string maintenance_name, string maintenance_days) : this()
        {
            this.txtDeviceID.Text = device_id;
            this.txtCode.Text = code;
            this.txtDeviceName.Text = device_name;
            this.txtMaintenanceID.Text = maintenance_id;
            this.txtMaintenanceName.Text = maintenance_name;
            this.txtMaintenanceDays.Text = maintenance_days;
            //this.txtMaintenanceHours.Text = maintenance_hours;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaintenanceName.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_045".Translation(), MsgType.Warning);
                    return;
                }
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING001.POP_PUT_ITEM_MAINTENANCE"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_DEVICE_ID",
                        "A_MAINTENANCE_ID",
                        "A_MAINTENANCE_NAME",
                        "A_MAINTENANCE_DAYS"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        txtDeviceID.Text,
                        txtMaintenanceID.Text,
                        txtMaintenanceName.Text.Trim(),
                        txtMaintenanceDays.Text.Trim()
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Information);
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void POP_SETTING001_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.soluongwafer = txtSoLuongWafer.EditValue.ToString();
            //DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
