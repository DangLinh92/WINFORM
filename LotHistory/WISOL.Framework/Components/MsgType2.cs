using DevExpress.XtraEditors;
using System;
using System.Drawing;

namespace Wisol.Components
{
    partial class MsgType2 : XtraForm
    {
        public MsgType2()
        {
            InitializeComponent();
        }

        public MsgType2(string _Msg)
        {
            InitializeComponent();
            lblMsg.Text = _Msg;
            picImage.Image = imgList.Images["INFORMATION.png"];
        }

        public MsgType2(string _Msg, MsgType msgType)
        {
            InitializeComponent();
            lblMsg.Text = _Msg;
            switch (msgType)
            {
                case MsgType.Error:
                    picImage.Image = imgList.Images["ERROR.png"];
                    this.panel2.BackColor = Color.FromArgb(255, 216, 216);
                    break;
                case MsgType.Information:
                    picImage.Image = imgList.Images["INFORMATION.png"];
                    this.panel2.BackColor = Color.FromArgb(218, 217, 255);
                    break;
                case MsgType.Warning:
                    picImage.Image = imgList.Images["WARNING.png"];
                    this.panel2.BackColor = Color.FromArgb(255, 255, 210);
                    break;
            }
        }
        public MsgType2(string _Msg, string _Caption, MsgType msgType)
        {
            InitializeComponent();
            lblMsg.Text = _Msg;
            this.Text = _Caption;
            switch (msgType)
            {
                case MsgType.Error:
                    picImage.Image = imgList.Images["ERROR.png"];
                    this.panel2.BackColor = Color.FromArgb(255, 216, 216);
                    break;
                case MsgType.Information:
                    picImage.Image = imgList.Images["INFORMATION.png"];
                    this.panel2.BackColor = Color.FromArgb(218, 217, 255);
                    break;
                case MsgType.Warning:
                    picImage.Image = imgList.Images["WARNING.png"];
                    this.panel2.BackColor = Color.FromArgb(255, 255, 210);
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
