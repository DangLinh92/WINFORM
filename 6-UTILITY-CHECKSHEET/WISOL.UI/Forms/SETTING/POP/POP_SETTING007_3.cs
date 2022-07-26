﻿using System;
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
    public partial class POP_SETTING007_3 : FormType
    {
        public string luongchuanhap { get; set; }
        public string soluongwafer { get; set; }

        DataTable dt_factory = new DataTable();
        DataTable dt_location = new DataTable();

        public POP_SETTING007_3(DataTable factory, DataTable location)
        {
            InitializeComponent();
            //this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            dt_factory = factory;
            dt_location = location;

            base.mBindData.BindGridLookEdit(gleDepartment, dt_factory, "ID", "DEPARTMENT_NAME");
            base.mBindData.BindGridLookEdit(gleLocation, dt_location, "LOCATION_ID", "LOCATION_NAME");

            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        //public POP_SETTING007_3(DataTable location)
        //{
        //    InitializeComponent();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text.Trim()) 
                  || string.IsNullOrWhiteSpace(txtDeviceName.Text.Trim())
                  || string.IsNullOrWhiteSpace(gleDepartment.EditValue.ToString())
                  || string.IsNullOrWhiteSpace(gleLocation.EditValue.ToString())
                  //|| string.IsNullOrWhiteSpace(dtpTimeSetup.Text.Trim())
                  )
                {
                    MsgBox.Show("Hãy nhập vào thông tin đầy đủ\r\nPlease enter all infomation".Translation(), MsgType.Warning);
                    return;
                }
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.POP_PUT_DEVICE"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_DEVICE_CODE",
                        "A_DEVICE_NAME",
                        "A_LOCATION_ID",
                        //"A_TIME_SETUP",
                        "A_DEPARTMENT_ID"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        txtCode.Text.Trim(),
                        txtDeviceName.Text.Trim(),
                        gleLocation.EditValue.ToString(),
                        //dtpTimeSetup.EditValue.ToString(),
                        gleDepartment.EditValue.ToString()
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

        private void gleFactory_EditValueChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(gleDepartment.EditValue.ToString()))
            //{
            //    return;
            //}

            //DataTable dt_F = new DataTable();

            //dt_F = dt_location.Select("FACTORY_ID = " + gleDepartment.EditValue.ToString()).CopyToDataTable();

            //base.mBindData.BindGridLookEdit(gleLocation, dt_F, "LOCATION_ID", "LOCATION_NAME");
        }
    }
}