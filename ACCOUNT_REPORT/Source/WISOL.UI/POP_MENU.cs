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
            string[] COLORS = new[] { "#336699" };//{"#c4424f", "#4B4B4B","#5D626E","#E3CAA6","#455EB2", "#1768C4", "#FFD16B", "#B5D38E", "#2E9482", "#399FE4", "#FA8072", "#1C4E80", "#488A99", "#6AB187" };
            Random random = new Random();

            for (int i = 0; i < input.Rows.Count; i++)
            {
                int start2 = random.Next(0, COLORS.Length);
                /**SimpleButton simpleButton = new SimpleButton();
                Controls.Add(simpleButton);
                simpleButton.Text = input.Rows[i]["MENUNAME"].ToString();
                simpleButton.Tag = input.Rows[i]["FORM"].ToString();
                simpleButton.BackColor = Color.MediumSeaGreen;
                simpleButton.ImageOptions.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\" + input.Rows[i]["IMAGE"].ToString());
                simpleButton.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                simpleButton.Click += SimpleButton_Click;**/

                Button button1 = new System.Windows.Forms.Button();
                button1.BackColor = System.Drawing.ColorTranslator.FromHtml(COLORS[start2]);
                button1.ForeColor = System.Drawing.Color.White;
                button1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\" + input.Rows[i]["IMAGE"].ToString());
                button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                button1.Location = new System.Drawing.Point(85, 87);
                button1.Padding = new System.Windows.Forms.Padding(5);
                button1.Size = new System.Drawing.Size(185, 104);
                button1.Tag = input.Rows[i]["FORM"].ToString();
                button1.TabIndex = 4;
                button1.Text = input.Rows[i]["MENUNAME"].ToString();
                button1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                button1.UseVisualStyleBackColor = false;
                button1.Click += SimpleButton_Click;
                button1.FlatAppearance.BorderSize = 0;
                button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

                LayoutControlItem item = layoutControlGroup1.AddItem();
                item.SizeConstraintsType = SizeConstraintsType.Custom;
                item.MinSize = new Size(180, 120);
                item.MaxSize = new Size(180, 120);
                item.Control = button1;
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
            Button button1 = sender as Button;
            //MessageBox.Show(simpleButton.Tag.ToString());
            this.buttonTag = button1.Tag.ToString();
            this.buttonText = button1.Text.ToString();
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
