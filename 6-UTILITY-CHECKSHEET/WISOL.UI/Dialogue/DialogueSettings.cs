using PROJ_B_DLL.Objects;
using System;
using System.IO;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Dialog
{
    public partial class DialogueSettings : FormType
    {
        public DialogueSettings()
        {
            InitializeComponent();

            txtFactory.EditValue = Consts.PLANT;
            txtUserId.EditValue = Consts.USER_INFO.Id;
            txtUserName.EditValue = Consts.USER_INFO.Name;
            txtUserIp.EditValue = Consts.LOCAL_SYSTEM_INFO.IpAddress;
            txtFileVersion.EditValue = Consts.VERSION;
            rdgMenuType.EditValue = Consts.MUNU_TYPE;
        }

        /// <summary>
        /// 저장 버튼 클릭이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Consts.MUNU_TYPE = rdgMenuType.EditValue.NullString();
                if (File.Exists(Consts.COMPORT_FILE))
                {
                    File.Delete(Consts.COMPORT_FILE);
                }
                File.WriteAllText(Consts.COMPORT_FILE, Consts.LOCAL_SYSTEM_INFO.SerialPort + "/" + Consts.COM_X.NullString() + "/" + Consts.CON_Y.NullString());
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnChgPwd_Click(object sender, EventArgs e)
        {
            try
            {
                DialoguePasswordChange chgPassword = new DialoguePasswordChange(Consts.USER_INFO.Id);
                chgPassword.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnResetGlsr_Click(object sender, EventArgs e)
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_COMM.GET_GLSR"
                    , new string[] { "A_PLANT" }
                    , new string[] { Consts.PLANT }

                    );

                if (base.mResultDB.ReturnInt == 0)
                {
                    Consts.GLOSSARY = base.mResultDB.ReturnDataSet.Tables[0].Copy();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnPdaDownload_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = "PDA 프로그램.zip";
                    sfd.Title = "Save Excel File";
                    DialogResult result = sfd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Wisol.DataAcess.FileAccess fileAccess = new Wisol.DataAcess.FileAccess(Consts.SERVICE_INFO.ServiceIp);
                        FileObject fileObject = fileAccess.GetFile("COMMON/PDAPC.zip");
                        File.WriteAllBytes(sfd.FileName, fileObject.FileContent);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnResDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "PROJ-B-INSP";
                process.StartInfo.Verb = "Open";
                process.Start();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
