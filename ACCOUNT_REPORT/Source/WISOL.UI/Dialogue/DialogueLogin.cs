using PROJ_B_DLL.Objects;
using System;
using System.Data;
using System.Windows.Forms;
using Wisol.BindDatas;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;
using Wisol.MES.Inherit;

namespace Wisol.MES.Dialog
{
    public partial class FrmLogin : FormType
    {


        /// <summary>
        /// 생성자
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            try
            {
                DataTable dtSystem = new DataTable("FACTORY");
                dtSystem.Columns.Add("FACTORY", typeof(string));
                dtSystem.Columns.Add("FACTORY_DESC", typeof(string));

                DataTable dtAccessType = new DataTable("ACCESS_TYPE");
                dtAccessType.Columns.Add("ACCESS_CODE", typeof(string));
                dtAccessType.Columns.Add("ACCESS_NAME", typeof(string));

                DataRow drAccess = dtAccessType.NewRow();
                drAccess["ACCESS_CODE"] = "PRD";
                drAccess["ACCESS_NAME"] = "운영";
                dtAccessType.Rows.Add(drAccess);

                drAccess = dtAccessType.NewRow();
                drAccess["ACCESS_CODE"] = "DEV";
                drAccess["ACCESS_NAME"] = "개발";
                dtAccessType.Rows.Add(drAccess);

                txtUserId.EditValue = Consts.USER_INFO.Id;
                switch (Consts.PLANT)
                {
                    case "KR":
                        Consts.USER_INFO.Language = "KOR";
                        break;
                    case "WHC":
                        Consts.USER_INFO.Language = "VTN";
                        break;
                    default:
                        Consts.USER_INFO.Language = "KOR";
                        break;
                }

                var proc = base.mDBaccess.ExcuteProc("PKG_COMM.GET_LANGAGUE"
                    , new string[] { }
                    , new string[] { }
                    );

                if (proc.ReturnInt == 0)
                {
                    /**base.mBindData.BindGridLookEdit(gleLanguage,
                        proc.ReturnDataSet.Tables[0],
                        "LANGUAGE",
                        "LANGUAGE_DESC"
                        );**/

                    base.mBindData.BindGridLookEdit(gleDepartment,
                        proc.ReturnDataSet.Tables[0],
                        "CODE",
                        "DEPARTMENT"
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Consts.USER_INFO.Language) == false)
            {
                gleLanguage.EditValue = Consts.USER_INFO.Language;
            }
            if (Consts.USER_INFO.Id != string.Empty)
            {
                txtUserId.EditValue = Consts.USER_INFO.Id;
                gleDepartment.EditValue = Consts.DEPARTMENT;
                //chkSaveId.Checked = true;
                txtPassword.Focus();
            }
            else
            {
                txtUserId.Focus();
            }
        }



        private void AccessTypeChange()
        {
            try
            {
                Settings.SettingChange();
                ServerInfo service = new ServerInfo();
                service.ServerIp = Consts.SERVICE_INFO.ServiceIp;
                service.ClientIp = Consts.LOCAL_SYSTEM_INFO.IpAddress;
                service.ServicePort = Converter.ParseValue<int>(Consts.SERVICE_INFO.ServicePort);
                service.ServiceID = Consts.SERVICE_INFO.UserId;
                service.ServicePassword = Consts.SERVICE_INFO.Password;

                base.mDBaccess = new DBAccess(service);
                base.mBindData = new BindData(service, Consts.USER_INFO.Language, Consts.GLOSSARY.Copy());

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void GetGlsr()
        {
            try
            {
                var resultDB = base.mDBaccess.ExcuteProc("PKG_COMM.GET_GLSR"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }

                    );

                if (resultDB.ReturnInt == 0)
                {
                    Consts.GLOSSARY = resultDB.ReturnDataSet.Tables[0].Copy();
                }
                else
                {
                    MsgBox.Show(resultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch
            {

            }
        }



        /// <summary>
        /// 로그인 버튼 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(gleLanguage.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_002".Translation(), MsgType.Warning);
                    gleLanguage.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtUserId.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_004".Translation(), MsgType.Warning);
                    txtUserId.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(gleDepartment.EditValue.NullString()) == true)
                {
                    MsgBox.Show("Hay nhap bo phan".Translation(), MsgType.Warning);
                    txtPassword.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtPassword.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_005".Translation(), MsgType.Warning);
                    txtPassword.Focus();
                    return;
                }
                var proc = base.mDBaccess.ExcuteProc("PKG_COMM.GET_LOGIN"
                    , new string[] { "A_PLANT",
                        "A_USER_ID",
                        "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT,
                        txtUserId.EditValue.NullString().ToUpper(),
                        gleDepartment.EditValue.ToString()
                    }
                    );
                if (proc.ReturnInt == 0)
                {
                    if (proc.ReturnDataSet.Tables[0].Rows.Count == 0)
                    {
                        MsgBox.Show("MSG_ERR_006".Translation(), MsgType.Warning);
                        return;
                    }
                    else if (proc.ReturnDataSet.Tables[0].Rows[0]["PASSWORD"].NullString() != txtPassword.EditValue.NullString())
                    {
                        MsgBox.Show("MSG_ERR_007".Translation(), MsgType.Warning);
                        return;
                    }
                    else  //로그인정보 일치
                    {
                        //Settings.SettingChange();
                        Consts.USER_INFO.Id = txtUserId.EditValue.NullString();
                        Consts.USER_INFO.Language = gleLanguage.EditValue.NullString();
                        Consts.USER_INFO.Name = proc.ReturnDataSet.Tables[0].Rows[0]["USER_NAME"].NullString();
                        Consts.USER_INFO.Role = proc.ReturnDataSet.Tables[1].Copy();
                        Consts.USER_INFO.UserRole = proc.ReturnDataSet.Tables[1].Rows[0]["USERROLE"].NullString();
                        Consts.DEPARTMENT = gleDepartment.EditValue.NullString();
                        Consts.USER_INFO.RollControls = proc.ReturnDataSet.Tables[2].Copy();
                        CommonRoleControl.RollControls = proc.ReturnDataSet.Tables[2].Copy();

                        DialogResult = System.Windows.Forms.DialogResult.OK;

                        //계정정보를 XML에 저장
                        Settings.SaveUser();

                        GetGlsr();

                        this.Close();
                    }
                }
                else
                {
                    MsgBox.Show(proc.ReturnString.Translation(), MsgType.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        /// <summary>
        /// 로그인 취소버튼 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanccel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Warning);
            }
        }



        /// <summary>
        /// 접속정보 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void gleAccessType_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Settings.SettingChange();
        //        ServerInfo service = new ServerInfo();
        //        service.ServerIp = Global.serviceInfo.ServiceIp;
        //        service.ClientIp = Global.pcInfo.IpAddress;
        //        service.ServicePort = Converter.ParseValue<int>(Global.serviceInfo.ServicePort);
        //        service.ServiceID = Global.serviceInfo.UserId;
        //        service.ServicePassword = Global.serviceInfo.Password;

        //        base.mDBaccess = new DBAccess(service);
        //        base.mBindData = new BindData(service, Global.userInfo.Language, Global.Glossary.Copy());
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Show(ex.Message, MsgType.Error);
        //    }
        //}

        /// <summary>
        /// 비밀번호 입력후 엔터키 입력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnLogin.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Warning);
            }
        }

        private void gleLanguage_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                Consts.USER_INFO.Language = gleLanguage.EditValue.NullString();

                if (labelControl1.Tag.NullString().Translation() == string.Empty)
                {
                    labelControl1.Text = labelControl1.Tag.NullString();
                }
                else
                {
                    labelControl1.Text = labelControl1.Tag.NullString().Translation();
                }
                if (labelControl2.Tag.NullString().Translation() == string.Empty)
                {
                    labelControl2.Text = labelControl2.Tag.NullString();
                }
                else
                {
                    labelControl2.Text = labelControl2.Tag.NullString().Translation();
                }
                //if (labelControl3.Tag.NullString().Translation() == string.Empty)
                //{
                //    labelControl3.Text = labelControl3.Tag.NullString();
                //}
                //else
                //{
                //    labelControl3.Text = labelControl3.Tag.NullString().Translation();
                //}
                if (labelControl4.Tag.NullString() == string.Empty)
                {
                    labelControl4.Text = labelControl4.Tag.NullString();
                }
                else
                {
                    labelControl4.Text = labelControl4.Tag.NullString().Translation();
                }
                if (labelControl5.Tag.NullString().Translation() == string.Empty)
                {
                    labelControl5.Text = labelControl5.Tag.NullString();
                }
                else
                {
                    labelControl5.Text = labelControl5.Tag.NullString().Translation();
                }
                //if (lblSystem.Tag.NullString().Translation() == string.Empty)
                //{
                //    lblSystem.Text = lblSystem.Tag.NullString();
                //}
                //else 
                //{
                //    lblSystem.Text = lblSystem.Tag.NullString().Translation();
                //}
                lblSystem.Text = "WHC법인\r\n                        SPARE PART";
                //if (chkSaveId.Tag.NullString().Translation() == string.Empty)
                //{
                //    chkSaveId.Text = chkSaveId.Tag.NullString().Translation();
                //}
                //else
                //{
                //    chkSaveId.Text = chkSaveId.Tag.NullString().Translation();
                //}
                if (btnLogin.Tag.NullString().Translation() == string.Empty)
                {
                    btnLogin.Text = btnLogin.Tag.NullString();
                }
                else
                {
                    btnLogin.Text = btnLogin.Tag.NullString().Translation();
                }
                if (btnCanccel.Tag.NullString().Translation() == string.Empty)
                {
                    btnCanccel.Text = btnCanccel.Tag.NullString();
                }
                else
                {
                    btnCanccel.Text = btnCanccel.Tag.NullString().Translation();
                }
                //Extend.SetColumnLanguage(gleCompany.Properties.View, "FACTORY", "FACTORY_DESC");
                //Extend.SetColumnLanguage(gleAccessType.Properties.View, "ACCESS_CODE", "ACCESS_NAME");
                //var proc = base.mDBaccess.ExcuteProc("PKG_COMM.GET_LANG_PLANT"
                //    , new string[] { "A_PLANT",
                //        "A_LANG"
                //    }
                //    , new string[] { Consts.PLANT,
                //        Consts.USER_INFO.Language
                //    }
                //    );
                //if (proc.ReturnInt == 0)
                //{

                //    string plant = gleCompany.EditValue.NullString();
                //    string accessType = gleAccessType.EditValue.NullString();
                //    base.mBindData.BindGridLookEdit(gleCompany,
                //        proc.ReturnDataSet.Tables[0],
                //        "FACTORY",
                //        "FACTORY_DESC"
                //    );
                //    base.mBindData.BindGridLookEdit(gleAccessType,
                //        proc.ReturnDataSet.Tables[1],
                //        "ACCESS_CODE",
                //        "ACCESS_NAME"
                //    );
                //    gleCompany.EditValue = plant;
                //    gleAccessType.EditValue = accessType;
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


        private void picKor_Click(object sender, EventArgs e)
        {
            gleLanguage.EditValue = "KOR";
        }

        private void picEng_Click(object sender, EventArgs e)
        {
            gleLanguage.EditValue = "ENG";
        }

        private void pciVtn_Click(object sender, EventArgs e)
        {
            gleLanguage.EditValue = "VTN";
        }

        private void pciChana_Click(object sender, EventArgs e)
        {
            gleLanguage.EditValue = "CHN";
        }

        private void gleCompany_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                AccessTypeChange();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
