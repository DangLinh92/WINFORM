using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wisol.Components;
using Wisol.MES.Dialog;
using Wisol.MES.Inherit;

namespace Wisol.MES
{
    public partial class POP_MENU : FormType
    {
        public string buttonTag { get; set; }
        public string buttonText { get; set; }
        public POP_MENU()
        {
            InitializeComponent();
            //Init_Control();
        }

        public POP_MENU(string index, string textHeaer, DataTable input)
        {
            InitializeComponent();
            this.Text = textHeaer.ToUpper();
            bar3.Visible = false;
            if(index == "1")
            {
                bar2.Visible = true;
            }
            else
            {
                bar2.Visible = false;
            }
            Init_Control(input);
            //if (Consts.USER_INFO.Id.ToUpper() != "H2002001" && Consts.USER_INFO.Id.ToUpper() != "231017")
            //{
            //    barLogOut.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}
        }

        private void Init_Control(DataTable input)
        {
            SimpleButton simpleButton1 = new SimpleButton();
            Controls.Add(simpleButton1);
            simpleButton1.Text = "Button hide 1";


            SimpleButton simpleButton2 = new SimpleButton();
            Controls.Add(simpleButton2);
            simpleButton2.Text = "Button hide 2";


            SimpleButton simpleButton3 = new SimpleButton();
            Controls.Add(simpleButton3);
            simpleButton3.Text = "Button hide 3";

            SimpleButton simpleButton4 = new SimpleButton();
            Controls.Add(simpleButton4);
            simpleButton4.Text = "Button hide 4";

            LayoutControlItem item1 = layoutControlGroup1.AddItem();
            item1.SizeConstraintsType = SizeConstraintsType.Custom;
            item1.MinSize = new Size(150, 150);
            item1.MaxSize = new Size(150, 150);
            item1.Control = simpleButton1;


            LayoutControlItem item2 = layoutControlGroup1.AddItem();
            item2.SizeConstraintsType = SizeConstraintsType.Custom;
            item2.MinSize = new Size(150, 150);
            item2.MaxSize = new Size(150, 150);
            item2.Control = simpleButton2;
            item2.Move(item1, InsertType.Bottom);


            LayoutControlItem item3 = layoutControlGroup1.AddItem();
            item3.SizeConstraintsType = SizeConstraintsType.Custom;
            item3.MinSize = new Size(150, 150);
            item3.MaxSize = new Size(150, 150);
            item3.Control = simpleButton3;
            item3.Move(item2, InsertType.Bottom);

            LayoutControlItem item4 = layoutControlGroup1.AddItem();
            item4.SizeConstraintsType = SizeConstraintsType.Custom;
            item4.MinSize = new Size(150, 150);
            item4.MaxSize = new Size(150, 150);
            item4.Control = simpleButton4;
            item4.Move(item3, InsertType.Bottom);

            for (int i = 0; i < input.Rows.Count; i++)
            {
                SimpleButton simpleButton = new SimpleButton();
                Controls.Add(simpleButton);
                simpleButton.Text = input.Rows[i]["MENUNAME"].ToString();
                simpleButton.Tag = input.Rows[i]["FORM"].ToString();
                simpleButton.ImageOptions.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\" + input.Rows[i]["IMAGE"].ToString());
                simpleButton.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                simpleButton.Click += SimpleButton_Click;

                LayoutControlItem item = layoutControlGroup1.AddItem();
                item.SizeConstraintsType = SizeConstraintsType.Custom;
                item.MinSize = new Size(180, 180);
                item.MaxSize = new Size(180, 180);
                item.Control = simpleButton;
                item.Padding = new DevExpress.XtraLayout.Utils.Padding(10);
                if (i < 8)
                {
                    item.Move(item1, InsertType.Left);
                }
                else if (i >= 8 && i < 16)
                {
                    item.Move(item2, InsertType.Left);
                }
                else if (i >= 16 && i < 24)
                {
                    item.Move(item3, InsertType.Left);
                }
                else if (i >= 24)
                {
                    item.Move(item4, InsertType.Left);
                }
            }


            item1.Visibility = LayoutVisibility.Never;
            item2.Visibility = LayoutVisibility.Never;
            item3.Visibility = LayoutVisibility.Never;
            item4.Visibility = LayoutVisibility.Never;
        }

        private void SimpleButton_Click(object sender, EventArgs e)
        {
                    Console.WriteLine("click");
            SimpleButton simpleButton = sender as SimpleButton;
            //MessageBox.Show(simpleButton.Tag.ToString());
            this.buttonTag = simpleButton.Tag.ToString();
            this.buttonText = simpleButton.Text.ToString();
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void barLogOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            buttonTag = "LOG-OUT";
            this.Close();
        }

        private void barChangePassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void POP_MENU_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SimpleButton simpleButton = sender as SimpleButton;
            //this.buttonTag = buttonTag;
            //this.buttonText = buttonText;
        }
    }
}
