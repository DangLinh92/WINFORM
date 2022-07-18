using DevExpress.XtraEditors.Controls;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING003 : PageType
    {
        private string b64 = string.Empty;
        public SETTING003()
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
            //try
            //{
            //    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING003.INT_LIST"
            //        , new string[] { "A_PLANT", "A_LANGUAGE"
            //        }
            //        , new string[] { Consts.PLANT, Consts.USER_INFO.Language
            //        }
            //        );
            //    if (base.m_ResultDB.ReturnInt == 0)
            //    {
            //        base.m_BindData.BindGridView(gcList,
            //            base.m_ResultDB.ReturnDataSet.Tables[0]
            //            );
            //        base.m_BindData.BindGridLookEdit(gleUnit, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE_GROUP", "NAME_OF_CODE");
            //        base.m_BindData.BindGridLookEdit(gleMaker, base.m_ResultDB.ReturnDataSet.Tables[2], "CODE", "MAKER");
            //        base.m_BindData.BindGridLookEdit(gleStageUse, base.m_ResultDB.ReturnDataSet.Tables[3], "CODE", "NAME_OF_CODE");
            //        Init_Control(true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
            //this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

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

            this.SearchPage();
            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING003.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    gvList.OptionsView.ShowFooter = false;
                    gvList.Columns["ID"].Visible = false;
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
                txtID.EditValue = string.Empty;
                txtCode.EditValue = string.Empty;
                cbType.EditValue = string.Empty;
                dtpTimeSetup.EditValue = string.Empty;
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
                if (string.IsNullOrEmpty(txtCode.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_114".Translation(), MsgType.Warning);
                    return;
                }
                if(cbType.EditValue.NullString() != "Bình bột" && cbType.EditValue.NullString() != "Bình CO2")
                {
                    MsgBox.Show("Loại bình cứu hỏa không đúng".Translation(), MsgType.Warning);
                    return;
                }
                if (dtpTimeSetup.EditValue.NullString() == string.Empty)
                {
                    MsgBox.Show("TIME SETUP không được để trống\r\nTIME SETUP cannot be empty".Translation(), MsgType.Warning);
                    return;
                }

                string afterLoc = LocDau(txtCode.Text.Trim());

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING003.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ID",
                        "A_CODE",
                        "A_TYPE",
                        "A_TIME_SETUP"
                    }
                    , new string[] {
                        Consts.PLANT,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        txtID.Text.Trim(),
                        afterLoc,
                        cbType.EditValue.NullString(),
                        dtpTimeSetup.DateTime.ToString("yyyy-MM-dd")
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

            if (e.RowHandle < 0)
                return;
            else
            {
                string id = gvList.GetDataRow(e.RowHandle)["ID"].NullString();
                string code = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                string type = gvList.GetDataRow(e.RowHandle)["TYPE"].NullString();
                string time_setup = gvList.GetDataRow(e.RowHandle)["TIME_SETUP"].NullString();
                txtID.Text = id.Trim();
                txtCode.Text = code.Trim();
                cbType.Text = type;
                dtpTimeSetup.EditValue = time_setup;
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtCode.Text.Trim() == string.Empty)
            {
                return;
            }

            string id = txtID.Text.Trim();
            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING003.DELETE_ITEM"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_ID"
                        }
                        , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, id
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
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
                SearchPage();
            }
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            POP.POP_SETTING003_1 popup = new POP.POP_SETTING003_1("");
            popup.ShowDialog();
            this.SearchPage();
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
