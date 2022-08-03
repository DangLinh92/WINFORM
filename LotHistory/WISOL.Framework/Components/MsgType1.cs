using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Wisol.Components
{
    partial class MsgType1 : XtraForm
    {
        private BackgroundWorker bgwCheck = new BackgroundWorker();
        public MsgType1()
        {
            InitializeComponent();
        }

        public MsgType1(string _Msg)
        {
            InitializeComponent();
            lblMsg.Text = _Msg;
            picImage.Image = imgList.Images["INFORMATION.png"];
        }

        public MsgType1(string _Msg, MsgType msgType)
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

        public MsgType1(string _Msg, string _Caption, MsgType msgType)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
