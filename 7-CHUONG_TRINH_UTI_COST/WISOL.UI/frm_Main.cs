using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.Skins;
using DevExpress.UserSkins;
namespace phongtro
{
    public partial class frm_Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frm_Main()
        {
            InitializeComponent();
        }
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

        }


        public class TabAdd
        {

            // Xây dựng hàm dùng để add XtratabPage và XtraTabcontrol (Hàm này đặt trong Project có dạng Library để Build thành file .dll . Mục đích : viết 1 lần nhưng sử dụng cho nhiều Project khác nhau)
            // Sử dụng 4 đối số truyền vào cho hàm này gồm có:


            //1> DevExpress.XtraTab.XtraTabControl XtraTabCha : Tạm gọi là Tab Cha vì XtraTabControl này để mình quăng tabcon vào
            //2> string icon : Khi add Tab con vào Tab cha thì đối số này để quy định icon hình cho tabCon(XtraTabpage)
            //3> string TabNameAdd : Khi add tab con vào thì đối số này quy định tên của Tabcon vừa Add vào đó.
            //4> System.Windows.Forms.UserControl UserControl: Cái này dùng để Add cái UserControl do ta tạo vào Tabcon
            // Đướng đi: Khi gọi hàm này phải truyền vào 4 đối số để biết: 
            //Anh muốn add 1 cái Tab con vào cái tab Cha nào?
            // Anh muốn đặt tên Tab con đó tên là gì?
            // anh muốn cái Tab con khi add vào thì Icon của nó là gì?
            // Anh muốn đặt cái gì vào trong cái Tab Con đó trước khi quăng nó lên TAb Cha?
            // Note : Các bạn có thể tùy biến nhiều đối số khác nữa nhé.
            public void AddTab(DevExpress.XtraTab.XtraTabControl XtraTabCha, string icon, string TabNameAdd, System.Windows.Forms.UserControl UserControl)
            {
                // Khởi tạo 1 Tab Con (XtraTabPage) 
                DevExpress.XtraTab.XtraTabPage TAbAdd = new DevExpress.XtraTab.XtraTabPage();

                // Đặt đại cái tên cho nó là TestTab (Đây là tên nhé)
                TAbAdd.Name = "TestTab";
                // Tên của nó là đối số như đã nói ở trên
                TAbAdd.Text = TabNameAdd;
                // Add đối số UserControl vào Tab con vừa khởi tạo ở trên
                TAbAdd.Controls.Add(UserControl);
                // Dock cho nó tràn hết TAb con đó
                UserControl.Dock = DockStyle.Fill;
                try
                {
                    // Icon của Tab con khi add vào Tab cha sẽ được quy định ở đây(cái này các bác tự chọn đường dẫn đến file Icon đó nhé)
                    TAbAdd.Image = System.Drawing.Bitmap.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + @"\Icons\" + icon);
                }
                catch (Exception e)
                {
                }
                // Quăng nó lên TAb Cha (XtraTabCha là đối số thứ nhất như đã nói ở trên) 
                XtraTabCha.TabPages.Add(TAbAdd);
            }
        }


        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
          DialogResult dl=  MessageBox.Show("Bạn có muốn thoát không?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            {
                if (dl == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Trạng thái thuê")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {

            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Thu tiền phòng")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            radialMenu1.ShowPopup(new Point(1360, 750));
        }

        private void frm_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                radialMenu1.ShowPopup(new Point(1370, 780));
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            
          
        }

        private void imageSlider1_Click(object sender, EventArgs e)
        {
            
        }

        private void imageSlider1_MouseCaptureChanged(object sender, EventArgs e)
        {
            //if (imageSlider1.CurrentImageIndex.ToString() == "0")
            //{
            //    MessageBox.Show("0");
            //}
            //else if (imageSlider1.CurrentImageIndex.ToString() == "1")
            //{
            //    MessageBox.Show("1");
            //}
        }

        private void imageSlider1_BackgroundImageChanged(object sender, EventArgs e)
        {
        }

        private void barButtonItem9_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Thống kê thu tiền")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void barButtonItem11_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            radialMenu1.ShowPopup(new Point(600, 780));
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Trả phòng")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Dịch vụ phòng")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Hình ảnh phòng")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            DevExpress.UserSkins.BonusSkins.Register();
            

            //DevExpress.UserSkins.OfficeSkins.Register();

            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(ribbonGalleryBarItem5, true);

           



            // Viết thêm cái dòng này để tạo ra cái button header đó
            this.xtraTabControl1.HeaderButtons = DevExpress.XtraTab.TabButtons.Close;
            // them dong nay de cai nut do xuat hien o tat ca cac tab
            xtraTabControl1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 145);
            //LookAndFeel.SkinName = "Black";



            //Load form trạng thái thuê lên đầu tiên...
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Trạng thái thuê")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.XtraTabControl tabControl = sender as DevExpress.XtraTab.XtraTabControl;
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
            (arg.Page as DevExpress.XtraTab.XtraTabPage).Dispose();
        }

        private void xtraTabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            // Khi add vào thì Focus tới ngay Tab vừa Add
            xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
        }

        private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            
        }

        private void navBarGroup1_ItemChanged(object sender, EventArgs e)
        {
            //Load form trạng thái thuê lên đầu tiên...
            int t = 0;
            foreach (DevExpress.XtraTab.XtraTabPage tab in xtraTabControl1.TabPages)
            {
                if (tab.Text == "Danh sách thu tiền")
                {
                    xtraTabControl1.SelectedTabPage = tab;
                    t = 1;
                }
            }
            if (t == 1)
            {

            }
            else
            {

            }
        }

        private void navBarGroup2_ItemChanged(object sender, EventArgs e)
        {
            radialMenu1.ShowPopup(new Point(100, 300));
        }
    }
}