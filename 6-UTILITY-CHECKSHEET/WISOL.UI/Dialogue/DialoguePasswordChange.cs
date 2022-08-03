using System;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Dialog
{
    public partial class DialoguePasswordChange : FormType
    {
        private string userId = string.Empty;
        public DialoguePasswordChange()
        {
            InitializeComponent();
        }
        public DialoguePasswordChange(string _userId)
        {
            InitializeComponent();
            userId = _userId;
            txtUserId.EditValue = userId;
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
                if (txtUserId.EditValue.NullString() == "toe")
                {
                    //mode COM1: baud=9600 parity=n data=8 stop=1 to=on xon=off octs= off odsr=off idsr=off dtr=off rts=off
                    Barcode bar = new Barcode(Consts.LOCAL_SYSTEM_INFO.SerialPort);
                    bar.PrintLabel(@"^XA
^FO00,20^BXN,2,200,20,20^FD[BARCODE]^FS
^FO45,20^A0,15,15^FD[TEXT1]^FS
^FO45,35^A0,15,15^FD[TEXT2]^FS
^FO45,50^A0,15,15^FD[TEXT3]^FS
^FO93,50^A0,15,15^FD[TEXT4]^FS
^PQ1
^XZ");
                    return;
                }
                if (string.IsNullOrEmpty(txtUserId.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_008".Translation(), MsgType.Warning);
                    txtUserId.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtCurrPwd.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_009".Translation(), MsgType.Warning);
                    txtCurrPwd.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtChgPwd1.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_010".Translation(), MsgType.Warning);
                    txtChgPwd1.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtChgPwd2.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_011".Translation(), MsgType.Warning);
                    txtChgPwd2.Focus();
                    return;
                }
                else if (txtChgPwd1.EditValue.NullString() != txtChgPwd2.EditValue.NullString())
                {
                    MsgBox.Show("MSG_ERR_016".Translation(), MsgType.Warning);
                    txtChgPwd1.EditValue = string.Empty;
                    txtChgPwd2.EditValue = string.Empty;
                    txtChgPwd1.Focus();
                    return;
                }

                mResultDB = mDBaccess.ExcuteProc("PKG_COMM.CHG_PWD",
                    new string[]{"A_PLANT",
                        "A_USER_ID",
                        "A_CURR_PWD",
                        "A_CHG_PWD"
                    },
                    new string[]{Consts.PLANT,
                        userId,
                        txtCurrPwd.EditValue.NullString(),
                        txtChgPwd1.EditValue.NullString()
                    }
                    );

                if (mResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Information);
                    this.Close();
                }
                else
                {
                    MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Warning);
                }

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        /// <summary>
        /// 취소버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
