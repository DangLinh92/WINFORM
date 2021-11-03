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
using DevExpress.XtraEditors.Controls;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_SETTING003_1 : FormType
    {
        public POP_SETTING003_1(string code)
        {
            InitializeComponent();
            //this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ComboBoxItemCollection coll = cbType.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add("Bình bột");
                coll.Add("Bình CO2");
            }
            finally
            {
                coll.EndUpdate();
            }
        }

        //public POP_SETTING003_1(DataTable location)
        //{
        //    InitializeComponent();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text.Trim()))
                {
                    MsgBox.Show("CODE không được để trống\r\nCODE cannot be empty".Translation(), MsgType.Warning);
                    return;
                }
                if (cbType.EditValue.NullString() != "Bình bột" && cbType.EditValue.NullString() != "Bình CO2")
                {
                    MsgBox.Show("Loại bình cứu hỏa không đúng".Translation(), MsgType.Warning);
                    return;
                }
                if(dtpTimeSetup.EditValue.NullString() == string.Empty)
                {
                    MsgBox.Show("TIME SETUP không được để trống\r\nTIME SETUP cannot be empty".Translation(), MsgType.Warning);
                    return;
                }

                string afterLoc = LocDau(txtCode.Text.Trim());
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING003.POP_PUT_ITEM"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ID",
                        "A_TYPE",
                        "A_TIME_SETUP"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        afterLoc,
                        cbType.EditValue.ToString(),
                        dtpTimeSetup.DateTime.ToString("yyyy-MM-dd")
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

        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
    }
}
