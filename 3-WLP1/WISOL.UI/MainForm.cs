using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.DataAcess;
using Wisol.MES.Dialog;
using Wisol.MES.Inherit;
using Wisol.MES.Interfaces;
using System.Net;
using System.Xml;

namespace Wisol.MES
{
    public partial class MainForm : XtraForm
    {
        private PageType m_PageType;

        private bool loginFlag = false;

        private DBAccess m_DBAccess = null;

        private DataTable m_Menus = null;


        public MainForm()
        {
            InitializeComponent();
            InitializeTitlet();
            InitializeVersion();
            Settings.SettingWrite();

            m_DBAccess = new DBAccess();
            m_DBAccess.Address = Consts.SERVICE_INFO.ServiceIp;
            m_DBAccess.ClientAddress = Consts.LOCAL_SYSTEM_INFO.IpAddress;
            m_DBAccess.Port = Converter.ParseValue<int>(Consts.SERVICE_INFO.ServicePort);
            m_DBAccess.ServerID = Consts.SERVICE_INFO.UserId;
            m_DBAccess.ServerPW = Consts.SERVICE_INFO.Password;

            loginFlag = false;
            UserLookAndFeel.Default.StyleChanged += new EventHandler(Default_StyleChanged);

            //ExtractFromAssembly();
        }

        //private void ExtractFromAssembly()
        //{
        //    string strPath = Directory.GetCurrentDirectory() + "\\Itenso.TimePeriod.dll";
        //    if (File.Exists(strPath)) File.Delete(strPath);
        //    var allRessources = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
        //    //In the next line you should provide      NameSpace.FileName.Extension that you have embedded
        //    var input = Assembly.GetExecutingAssembly().GetManifestResourceStream("Wisol.MES.Resources.Itenso.TimePeriod.dll");
        //    var output = File.Open(strPath, FileMode.CreateNew);
        //    CopyStream(input, output);
        //    input.Dispose();
        //    output.Dispose();
        //    //System.Diagnostics.Process.Start(strPath);
        //}

        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            while (true)
            {
                int read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return;
                output.Write(buffer, 0, read);
            }
        }

        private void InitializeVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Consts.VERSION = fvi.FileVersion;
            myTextVersion.Caption = "Version : " + Consts.VERSION;

            //if(Program.connectionString == "Data Source = 10.70.10.97;Initial Catalog = WHNP1_TEST;User Id = sa;Password = Wisol@123;Connect Timeout=3")
            //{
            //    myTextVersion.Caption = "TEST VERSION                   TEST VERSION                    TEST VERSION                   TEST VERSION                    TEST VERSION";
            //}
        }

        private void InitializeTitlet()
        {
            //this.Text = "WHC법인_W1-Part Leader";
            this.Text = "WHC법인";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var controlColor = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default).Colors.GetColor("Control");

                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.Text = this.Text.Translation();

                ControlsTranslation();

                this.navBarControl.Groups.Clear();
                this.navBarControl.Appearance.GroupHeader.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.navBarControl.Appearance.GroupHeaderActive.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
                this.navBarControl.Appearance.GroupHeaderHotTracked.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
                this.navBarControl.Appearance.GroupHeaderPressed.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
                this.navBarControl.Appearance.NavigationPaneHeader.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

                if (Consts.MENU_EXPANDED != "N")
                {
                    this.navBarControl.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
                }
                else
                {
                    this.navBarControl.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
                }

                if (Consts.MUNU_TYPE == "T")
                {
                    m_MenuBar.Visible = true;
                    splitContainerControl1.Panel1.Visible = false;
                    navBarControl.Visible = false;
                    splitContainerControl1.SplitterPosition = 0;
                    splitContainerControl1.IsSplitterFixed = true;

                    this.m_MenuBar.DockCol = 0;
                    this.m_MenuBar.DockRow = 0;
                    this.m_IconBar.DockCol = 0;
                    this.m_IconBar.DockRow = 1;
                }
                else
                {
                    m_MenuBar.Visible = false;
                    splitContainerControl1.Panel1.Visible = true;
                    navBarControl.Visible = true;
                    splitContainerControl1.SplitterPosition = 215;
                    splitContainerControl1.IsSplitterFixed = false;
                }

                barLogin.Caption = barLogin.Tag.NullString().Translation();
                barFavorite.Caption = barFavorite.Tag.NullString().Translation();
                barSettings.Caption = barSettings.Tag.NullString().Translation();
                barNewExcute.Caption = barNewExcute.Tag.NullString().Translation();
                barBtnInit.Caption = barBtnInit.Tag.NullString().Translation();
                barBtnSearch.Caption = barBtnSearch.Tag.NullString().Translation();
                barBtnLoadCDT.Caption = barBtnLoadCDT.Tag.NullString().Translation();
                barBtnSaveCDT.Caption = barBtnSaveCDT.Tag.NullString().Translation();
                barBtnLoadLayout.Caption = barBtnLoadLayout.Tag.NullString().Translation();
                barBtnSaveLayout.Caption = barBtnSaveLayout.Tag.NullString().Translation();
                barBtnClose.Caption = barBtnClose.Tag.NullString().Translation();
                barBtnCloseAll.Caption = barBtnCloseAll.Tag.NullString().Translation();
                barBtnManual.Caption = barBtnManual.Tag.NullString().Translation();
                barBtnGo.Caption = barBtnGo.Tag.NullString().Translation();
                barSkin.Caption = barSkin.Tag.NullString().Translation();
                this.barLogin.PerformClick();

                if (Consts.DEPARTMENT == "WLP1")
                {
                    DialogueNoticeMinChemical di = new DialogueNoticeMinChemical();
                    di.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
        private void ControlsTranslation()
        {
            var controls = Common.Common.GetAllControls(this);

            foreach (var control in controls)
            {
                if (control.Name == String.Empty) continue;

                var baseControl = control as BaseControl;
                if (baseControl != null) { baseControl.ToolTip = baseControl.Text.Translation("KOR"); }    

                if (control is LabelControl || control is SimpleButton || control is GroupControl || control is CheckEdit || control is TabPage)        
                {
                    control.Text = control.Text.Translation();
                }
                else if (control is RadioGroup)  
                {
                    RadioGroup radioGroup = control as RadioGroup;

                    foreach (RadioGroupItem item in radioGroup.Properties.Items)
                    {
                        item.Description = item.Description.Translation();
                    }
                }
                else if (control is LayoutControl)   
                {
                    LayoutControl layoutCtrl = control as LayoutControl;
                    layoutCtrl.AllowCustomization = false;
                    layoutCtrl.BeginUpdate();

                    foreach (BaseLayoutItem item in layoutCtrl.Items)
                    {
                        item.OptionsToolTip.ToolTip = item.Text.Translation("KOR");    
                        item.Text = item.Text.Translation();
                        item.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;

                        if (item.IsGroup)      
                        {
                            var layoutCtrlGrp = item as LayoutControlGroup;
                            layoutCtrlGrp.AppearanceGroup.Font = new Font(layoutCtrlGrp.AppearanceGroup.Font.Name, layoutCtrlGrp.AppearanceGroup.Font.Size, FontStyle.Bold);
                            layoutCtrlGrp.Padding = new DevExpress.XtraLayout.Utils.Padding(3);
                        }
                    }
                    layoutCtrl.EndUpdate();
                }
            }
        }



        private void SetLanguage()
        {
            try
            {
                m_DBAccess = new DBAccess();
                m_DBAccess.Address = Consts.SERVICE_INFO.ServiceIp;
                m_DBAccess.ClientAddress = Consts.LOCAL_SYSTEM_INFO.IpAddress;
                m_DBAccess.Port = Converter.ParseValue<int>(Consts.SERVICE_INFO.ServicePort);
                m_DBAccess.ServerID = Consts.SERVICE_INFO.UserId;
                m_DBAccess.ServerPW = Consts.SERVICE_INFO.Password;
                this.Text = this.Text.Translation();

                ControlsTranslation();

                barLogin.Caption = barLogin.Tag.NullString().Translation();
                barFavorite.Caption = barFavorite.Tag.NullString().Translation();
                barSettings.Caption = barSettings.Tag.NullString().Translation();
                barNewExcute.Caption = barNewExcute.Tag.NullString().Translation();
                barBtnInit.Caption = barBtnInit.Tag.NullString().Translation();
                barBtnSearch.Caption = barBtnSearch.Tag.NullString().Translation();
                barBtnLoadCDT.Caption = barBtnLoadCDT.Tag.NullString().Translation();
                barBtnSaveCDT.Caption = barBtnSaveCDT.Tag.NullString().Translation();
                barBtnLoadLayout.Caption = barBtnLoadLayout.Tag.NullString().Translation();
                barBtnSaveLayout.Caption = barBtnSaveLayout.Tag.NullString().Translation();
                barBtnClose.Caption = barBtnClose.Tag.NullString().Translation();
                barBtnCloseAll.Caption = barBtnCloseAll.Tag.NullString().Translation();
                barBtnManual.Caption = barBtnManual.Tag.NullString().Translation();
                barBtnGo.Caption = barBtnGo.Tag.NullString().Translation();
                barSkin.Caption = barSkin.Tag.NullString().Translation();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void NewPage(string moduleCode, string moduleName, string moduleAuth, string accessType)
        {
            try
            {
                if (Consts.ACCESS_TYPE == "Y" && accessType == "N")
                {
                    MsgBox.Show("MSG_ERR_113".Translation(), MsgType.Warning);
                    return;
                }

                Assembly assembly = Assembly.GetExecutingAssembly();
                var type = assembly.GetTypes().FirstOrDefault(x => x.Name == moduleCode);

                PageType pageType = assembly.CreateInstance(type.FullName, true) as PageType;
                if (pageType == null)
                {
                    MsgBox.Show("MSG_ERR_021".Translation(), MsgType.Warning);
                    return;
                }
                var row = m_Menus.Rows().FirstOrDefault(x => x["FORM"].ToString() == moduleCode);
                var lookup = m_Menus.Rows.Cast<DataRow>().ToLookup(x => x["MENUSEQ"].ToString());
                var result = lookup[row["UPRSEQ"].ToString()].SelectRecursive(x => lookup[x["UPRSEQ"].ToString()]).OrderBy(x => x["MENUSEQ"]).Select(x => x["MENUNAME"].ToString());

                //if (Consts.USER_INFO.Id.ToUpper() == "H2002001")
                //{
                //    pageType.ModuleCode = result.Aggregate((x, y) => { return x + " > " + y; }) + " > " + moduleName + " (" + moduleCode + ")";
                //}
                //else
                //{
                if (Consts.USER_INFO.Id.ToUpper() == "H2002001") 
                {
                    pageType.ModuleCode = result.Aggregate((x, y) => { return x + " > " + y; }) + " > " + moduleName + " (" + moduleCode + ")";
                }
                else
                {
                    pageType.ModuleCode = result.Aggregate((x, y) => { return x + " > " + y; }) + " > " + moduleName;// + " (" + moduleCode + ")";
                }
                //}
                pageType.ModuleName = moduleName;
                pageType.ModuleAuth = moduleAuth;
                pageType.Dock = DockStyle.Fill;

                tabForm.PaintStyleName = "Default";

                var page = new DevExpress.XtraTab.XtraTabPage();

                page.PageVisible = false;
                page.Tooltip = page.Text.Translation("KOR");      

                if (tabForm.TabPages.Count > 0)
                {
                    for (int i = 0; i < tabForm.TabPages.Count; i++)
                    {
                        if (tabForm.TabPages[i].Tag.NullString() == moduleCode)
                        {
                            tabForm.TabPages[i].BringToFront();
                            tabForm.TabPages[i].Show();
                            return;
                        }
                    }
                }

                page.Tag = moduleCode;
                page.Text = moduleName;
                page.Controls.Add(pageType);

                tabForm.TabPages.Add(page);

                tabForm.TabPages[tabForm.TabPages.Count - 1].Show();
                ((PageType)tabForm.TabPages[tabForm.TabPages.Count - 1].Controls[0]).Form_Show();

                page.PageVisible = true;
                tabForm.SelectedTabPageIndex = tabForm.TabPages.Count - 1;

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void GetMenu()
        {
            DataTable dtTemp = null;
            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("A_PLANT", Consts.PLANT);
                parameters.Add("A_USER_ID", Consts.USER_INFO.Id);
                parameters.Add("A_LANG", Consts.USER_INFO.Language);
                parameters.Add("A_ACCESS_IP", Consts.LOCAL_SYSTEM_INFO.IpAddress);
                parameters.Add("A_ACCESS_PC", Consts.LOCAL_SYSTEM_INFO.Name);
                //parameters.Add("A_DEPARTMENT", Consts.DEPARTMENT);
                var result = m_DBAccess.ExcuteProc("PKG_COMM.GET_MENU", parameters);

                if (result.ReturnInt == 0)
                {

                    m_Menus = result.ReturnDataSet.Tables[0].Copy();
                    var topMenu = result.ReturnDataSet.Tables[0].Select("MENUSEQ < 100");
                    var temp = result.ReturnDataSet.Tables[0].Clone();
                    for (int i = 0; i < topMenu.Length; i++)
                    {
                        temp.Rows.Add(topMenu[i].ItemArray);
                    }
                    temp.DefaultView.Sort = "DISPSEQ";
                    temp = temp.DefaultView.ToTable();

                    var bigitem = new BarSubItem[temp.Rows.Count];
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        bigitem[i] = new BarSubItem(barManager1, temp.Rows[i]["MENUNAME"].NullString());
                        DataRow[] drSmallList = result.ReturnDataSet.Tables[0].Select("UPRSEQ = " + temp.Rows[i]["MENUSEQ"].NullString());
                        dtTemp = result.ReturnDataSet.Tables[0].Clone();
                        for (int j = 0; j < drSmallList.Length; j++)
                        {
                            dtTemp.Rows.Add(drSmallList[j].ItemArray);
                        }

                        for (int j = 0; j < dtTemp.Rows.Count; j++)
                        {
                            DataRow[] drItem = result.ReturnDataSet.Tables[0].Select("UPRSEQ = " + drSmallList[j]["MENUSEQ"].NullString());
                            if (drItem.Length == 0)
                            {
                                BarButtonItem item = new BarButtonItem(barManager1, CN41Display(drSmallList[j]["FORM"].NullString()) + drSmallList[j]["MENUNAME"].NullString());
                                item.ItemClick += new ItemClickEventHandler(item_ItemClick);
                                item.Description = dtTemp.Rows[j]["FORM"].NullString() + "/" +
                                                               dtTemp.Rows[j]["MENUNAME"].NullString() + "/" +
                                                               dtTemp.Rows[j]["FORMROLE"].NullString() + "/" +
                                                               dtTemp.Rows[j]["ACCESS_TYPE"].NullString();
                                bigitem[i].AddItem(item);
                            }
                            else
                            {
                                BarSubItem item = new BarSubItem(barManager1, CN41Display(drSmallList[j]["FORM"].NullString()) + drSmallList[j]["MENUNAME"].NullString());
                                BarButtonItem[] smallItem = new BarButtonItem[drItem.Length];
                                for (int k = 0; k < drItem.Length; k++)
                                {
                                    smallItem[k] = new BarButtonItem(barManager1, CN41Display(drItem[k]["FORM"].NullString()) + drItem[k]["MENUNAME"].NullString());
                                    smallItem[k].ItemClick += new ItemClickEventHandler(item_ItemClick);
                                    smallItem[k].Description = drItem[k]["FORM"].NullString() + "/" +
                                                                                drItem[k]["MENUNAME"].NullString() + "/" +
                                                                                drItem[k]["FORMROLE"].NullString() + "/" +
                                                                                drItem[k]["ACCESS_TYPE"].NullString();
                                    item.AddItem(smallItem[k]);
                                }
                                bigitem[i].AddItem(item);
                            }
                        }
                        m_MenuBar.AddItem(bigitem[i]);
                    }



                    navBarControl.BeginUpdate();
                    navBarControl.Groups.Clear();


                    NavBarGroup navibarGrp = new NavBarGroup();
                    this.navBarControl.Groups.Add(navibarGrp);

                    navibarGrp.Name = "FAVORITE".Translation();
                    navibarGrp.Caption = "FAVORITE".Translation();
                    navibarGrp.SmallImageSize = new System.Drawing.Size(25, 25);
                    navibarGrp.SmallImage = menuImageList.Images[0];
                    navibarGrp.AppearanceHotTracked.ForeColor = System.Drawing.Color.Fuchsia;
                    navibarGrp.AppearanceHotTracked.Options.UseForeColor = true;

                    navibarGrp.GroupStyle = NavBarGroupStyle.ControlContainer;
                    navibarGrp.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    navibarGrp.AppearanceBackground.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    navibarGrp.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    navibarGrp.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    TreeList tl = new TreeList();
                    navibarGrp.ControlContainer.Controls.Add(tl);

                    Color controlColor = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default).Colors.GetColor("Control");
                    tl.Appearance.Empty.BackColor = controlColor;
                    tl.Appearance.Row.BackColor = controlColor;

                    tl.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
                    tl.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tl.Appearance.FocusedRow.Options.UseBackColor = true;
                    tl.Appearance.FocusedRow.Options.UseFont = true;
                    tl.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tl.Appearance.Row.Options.UseFont = true;
                    tl.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    tl.Appearance.SelectedRow.Options.UseFont = true;

                    tl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                    tl.Dock = DockStyle.Fill;

                    tl.OptionsBehavior.Editable = false;
                    tl.OptionsSelection.EnableAppearanceFocusedCell = false;
                    tl.OptionsView.ShowColumns = false;
                    tl.OptionsView.ShowHorzLines = false;
                    tl.OptionsView.ShowVertLines = false;
                    tl.OptionsView.ShowIndentAsRowStyle = true;
                    tl.OptionsView.ShowIndicator = false;
                    tl.TabStop = false;
                    tl.RowHeight = 25;

                    tl.DataSource = dtTemp;

                    tl.ParentFieldName = "UPRSEQ";
                    tl.KeyFieldName = "MENUSEQ";

                    for (int j = 0; j < tl.Columns.Count; j++)
                    {
                        if (tl.Columns[j].FieldName != "MENUNAME")
                        {
                            tl.Columns[j].Visible = false;
                        }
                    }

                    tl.SelectImageList = this.navBarImageCollection;
                    tl.NodesReloaded += new EventHandler(tl_NodesReloaded);
                    tl.MouseDown += tl_MouseDown;
                    tl.ExpandAll();

                    if (tl.Nodes.Count > 0)
                        this.navBarControl.Groups[0].Visible = true;
                    else
                        this.navBarControl.Groups[0].Visible = false;


                    topMenu = result.ReturnDataSet.Tables[0].Select("MENUSEQ < 100");

                    temp = result.ReturnDataSet.Tables[0].Clone();
                    for (int i = 0; i < topMenu.Length; i++)
                    {
                        temp.Rows.Add(topMenu[i].ItemArray);
                    }
                    temp.DefaultView.Sort = "MENUSEQ, DISPSEQ";
                    temp = temp.DefaultView.ToTable();

                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        navibarGrp = new NavBarGroup();
                        this.navBarControl.Groups.Add(navibarGrp);

                        navibarGrp.Name = temp.Rows[i]["MENUSEQ"].NullString();
                        navibarGrp.Caption = temp.Rows[i]["MENUNAME"].NullString();
                        navibarGrp.SmallImageSize = new System.Drawing.Size(25, 25);
                        navibarGrp.SmallImage = menuImageList.Images[Converter.ParseValue<int>(temp.Rows[i]["IMGIDX"])];
                        navibarGrp.AppearanceHotTracked.ForeColor = System.Drawing.Color.Fuchsia;
                        navibarGrp.AppearanceHotTracked.Options.UseForeColor = true;

                        navibarGrp.GroupStyle = NavBarGroupStyle.ControlContainer;
                        navibarGrp.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        navibarGrp.AppearanceBackground.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        navibarGrp.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        navibarGrp.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        tl = new TreeList();

                        DataRow[] drSmallList = result.ReturnDataSet.Tables[0].Select("UPRSEQ = " + temp.Rows[i]["MENUSEQ"].NullString());

                        dtTemp = result.ReturnDataSet.Tables[0].Clone();
                        for (int j = 0; j < drSmallList.Length; j++)
                        {
                            dtTemp.Rows.Add(drSmallList[j].ItemArray);
                            DataRow[] drSmallSmallList = result.ReturnDataSet.Tables[0].Select("UPRSEQ = " + drSmallList[j]["MENUSEQ"].NullString());
                            for (int k = 0; k < drSmallSmallList.Length; k++)
                            {
                                dtTemp.Rows.Add(drSmallSmallList[k].ItemArray);
                            }
                        }
                        navibarGrp.ControlContainer.Controls.Add(tl);

                        controlColor = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default).Colors.GetColor("Control");
                        tl.Appearance.Empty.BackColor = controlColor;
                        tl.Appearance.Row.BackColor = controlColor;

                        tl.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
                        tl.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        tl.Appearance.FocusedRow.Options.UseBackColor = true;
                        tl.Appearance.FocusedRow.Options.UseFont = true;
                        tl.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        tl.Appearance.Row.Options.UseFont = true;
                        tl.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        tl.Appearance.SelectedRow.Options.UseFont = true;

                        tl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        tl.Dock = DockStyle.Fill;

                        tl.OptionsBehavior.Editable = false;
                        tl.OptionsSelection.EnableAppearanceFocusedCell = false;
                        tl.OptionsView.ShowColumns = false;
                        tl.OptionsView.ShowHorzLines = false;
                        tl.OptionsView.ShowVertLines = false;
                        tl.OptionsView.ShowIndentAsRowStyle = true;
                        tl.OptionsView.ShowIndicator = false;
                        tl.TabStop = false;
                        tl.RowHeight = 25;

                        tl.DataSource = dtTemp;

                        tl.ParentFieldName = "UPRSEQ";
                        tl.KeyFieldName = "MENUSEQ";

                        for (int j = 0; j < tl.Columns.Count; j++)
                        {
                            if (tl.Columns[j].FieldName != "MENUNAME")
                            {
                                tl.Columns[j].Visible = false;
                            }
                        }

                        tl.SelectImageList = this.navBarImageCollection;
                        tl.NodesReloaded += new EventHandler(tl_NodesReloaded);
                        tl.MouseDown += tl_MouseDown;
                        tl.ExpandAll();

                        if (tl.Nodes.Count > 0)
                            this.navBarControl.Groups[i].Visible = true;
                        else
                            this.navBarControl.Groups[i].Visible = false;
                    }

                    navBarControl.EndUpdate();

                }

                var assembly = Assembly.GetExecutingAssembly();
                var forms = assembly.GetTypes().Where(x => x.Namespace != null && x.Namespace.Contains("Wisol.MES.Forms")).ToList();
                m_SearchMenuEdit.Items.AddRange(forms.Select(x => x.Name).ToArray());
                m_SearchMenuEdit.Items.AddRange(m_Menus.Rows.Cast<DataRow>().Where(x => !String.IsNullOrEmpty(x["FORM"].ToString())).Select(x => x["MENUNAME"]).ToArray());
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private static string CN41Display(String formCode)
        {
            try
            {
                if (Consts.ACCESS_PLANT == "CN41")
                {
                    return (formCode + " ");
                }
                return String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }

        void tl_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button.NullString() != "Left")
                {
                    return;
                }

                TreeList treelist = sender as TreeList;
                TreeListHitInfo hitinfo = treelist.CalcHitInfo(new Point(e.X, e.Y));
                TreeListNode node = hitinfo.Node;

                if (node != null)
                {
                    if (node["FORM"].NullString() == string.Empty)
                    {
                        return;
                    }
                    this.NewPage(node["FORM"].NullString(), node["MENUNAME"].NullString(), node["FORMROLE"].NullString(), node["ACCESS_TYPE"].NullString());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void tl_NodesReloaded(object sender, EventArgs e)
        {
            try
            {
                TreeList treelist = sender as TreeList;

                foreach (TreeListNode treeNode in treelist.Nodes)
                {
                    if (treeNode.HasChildren == true)
                    {
                        treeNode.Expanded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        void item_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                m_SplashScreenManager.ShowWaitForm();
                string[] strTemp = e.Item.Description.Split(new string[] { "/" }, StringSplitOptions.None);
                string moduleCode = strTemp[0];
                string moduleName = strTemp[1];
                string moduleAuth = strTemp[2];
                string accessType = strTemp[3];

                this.NewPage(moduleCode, moduleName, moduleAuth, accessType);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                SplashScreenToggle(SplashScreenStatus.Off);
            }
        }







        private void btnExcute_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath, "AUTOUPDATE_PASS");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void winBtn_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                m_SplashScreenManager.ShowWaitForm();
                if (tabForm.TabPages.Count == 0)
                {
                    return;
                }
                switch (e.Button.Properties.Tag.NullString())
                {
                    case "INIT[F2]":

                        ((IButton)m_PageType).InitializePage();

                        break;

                    case "SEARCH[F3]":

                        ((IButton)m_PageType).SearchPage();
                        break;
                    case "CHART":
                        ((IButton)m_PageType).OpenChart();
                        break;

                    case "CLOSE[F9]":
                        tabForm.TabPages[tabForm.SelectedTabPageIndex].Controls.RemoveAt(0);
                        tabForm.TabPages[tabForm.SelectedTabPageIndex].Dispose();
                        break;

                    case "CLOSE_ALL[F10]":
                        for (int i = tabForm.TabPages.Count - 1; i >= 0; i--)
                        {
                            tabForm.TabPages[i].Controls.RemoveAt(0);
                            tabForm.TabPages[i].Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                SplashScreenToggle(SplashScreenStatus.Off);
            }
        }

        private void tabForm_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (tabForm.TabPages.Count == 0)
            {
                tabForm.PaintStyleName = "Flat";
                m_PageType = null;

                return;
            }
            this.tabForm.BringToFront();
            m_PageType = ((PageType)tabForm.TabPages[tabForm.SelectedTabPageIndex].Controls[0]);
        }

        private void Login(string userId)
        {
            var proc = m_DBAccess.ExcuteProc("PKG_COMM.GET_LOGIN"
                    , new string[] { "A_PLANT"}
                    , new string[] { Consts.PLANT });
            if (proc.ReturnInt == 0)
            {
                Consts.USER_INFO.Id = userId;
                Consts.USER_INFO.Language = "KOR";
                Consts.USER_INFO.Name = proc.ReturnDataSet.Tables[0].Rows[0]["USER_NAME"].NullString();
                Consts.INOUT_FLAG = proc.ReturnDataSet.Tables[0].Rows[0]["INOUT_FLAG"].NullString();
                Consts.WHERE_HOUSE = proc.ReturnDataSet.Tables[0].Rows[0]["WH_CODE"].NullString();
                Consts.WHERE_HOUSE = proc.ReturnDataSet.Tables[0].Rows[0]["VENDOR_CODE"].NullString();
                Consts.USER_INFO.Role = proc.ReturnDataSet.Tables[1].Copy();
                Settings.SaveUser();

                GetGlsr();
            }
        }

        private void GetGlsr()
        {
            try
            {
                var resultDB = m_DBAccess.ExcuteProc("PKG_COMM.GET_GLSR"
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
            catch { }
        }

        private void barLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!loginFlag)
                {
                    var args = Environment.GetCommandLineArgs();
                    if (args.Length == 4)
                    {
                        Login(args[3]);
                    }
                    else
                    {
                        FrmLogin login = new FrmLogin();
                        if (login.ShowDialog() == DialogResult.Cancel) return;
                    }
                    barLogin.Tag = "LOG-OUT";
                    loginFlag = true;
                    SetLanguage();
                    GetMenu();
                }
                else if (loginFlag)
                {
                    for (int i = tabForm.TabPages.Count - 1; i >= 0; i--)
                    {
                        tabForm.TabPages[i].Controls.RemoveAt(0);
                        tabForm.TabPages[i].Dispose();
                    }


                    barLogin.Caption = "LOG-IN".Translation();
                    loginFlag = false;

                    m_MenuBar.ItemLinks.Clear();
                }
                if (Consts.ACCESS_MSG == "DEV")
                {
                    this.Text = Consts.PROJECT_NAME.Translation() + "(" + Consts.ACCESS_PLANT + ")" + "개발" + " - " + Consts.USER_INFO.Name;
                }
                else
                {
                    this.Text = Consts.PROJECT_NAME.Translation() + "(" + Consts.ACCESS_PLANT + ")" + " - " + Consts.USER_INFO.Name;
                }
                //this.Text = "WHC법인_W1-Part Leader" + " - " + Consts.USER_INFO.Name;
                this.Text = "WHC법인" + "_" + Consts.DEPARTMENT + " - " +  Consts.USER_INFO.Name;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void barBtnGo_ItemClick(object sender, ItemClickEventArgs e)
        {
            GoPage();
        }

        private void GoPage()
        {
            try
            {
                if (m_SearchMenuBarItem.EditValue != null)
                {
                    var parameters = new Dictionary<string, string>();
                    parameters.Add("A_PLANT", Consts.PLANT);
                    parameters.Add("A_USER_ID", Consts.USER_INFO.Id);
                    parameters.Add("A_LANG", Consts.USER_INFO.Language);
                    parameters.Add("A_ACCESS_IP", Consts.LOCAL_SYSTEM_INFO.IpAddress);
                    parameters.Add("A_ACCESS_PC", Consts.LOCAL_SYSTEM_INFO.Name);
                   //parameters.Add("A_DEPARTMENT", Consts.DEPARTMENT);
                    var result = m_DBAccess.ExcuteProc("PKG_COMM.GET_MENU", parameters);

                    if (result.ReturnInt == 0)
                    {
                        m_Menus = result.ReturnDataSet.Tables[0].Copy();
                    }

                    m_SplashScreenManager.ShowWaitForm();
                    var row = m_Menus.Rows.Cast<DataRow>().FirstOrDefault(x => x["FORM"].NullString() == m_SearchMenuBarItem.EditValue.NullString() || x["MENUNAME"].NullString() == m_SearchMenuBarItem.EditValue.NullString());
                    if (row != null)
                    {
                        string moduleCode = row["FORM"].NullString();
                        string moduleName = row["MENUNAME"].NullString();
                        string moduleAuth = row["FORMROLE"].NullString();
                        string accessType = row["ACCESS_TYPE"].NullString();
                        this.NewPage(moduleCode, moduleName, moduleAuth, accessType);
                    }
                    else
                    {
                        MsgBox.Show("MSG_ERR_114".Translation(), MsgType.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                SplashScreenToggle(SplashScreenStatus.Off);
            }
        }

        private void barNewExcute_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath, Consts.ORIGINAL_ACCESS_MSG);
        }

        private void barSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Consts.USER_INFO.Id == string.Empty)
            {
                MsgBox.Show("MSG_ERR_100".Translation(), MsgType.Warning);
                return;
            }
            DialogueSettings settings = new DialogueSettings();
            if (DialogResult.OK == settings.ShowDialog())
            {
                if (Consts.MUNU_TYPE == "T")
                {
                    m_MenuBar.Visible = true;
                    splitContainerControl1.Panel1.Visible = false;
                    navBarControl.Visible = false;
                    splitContainerControl1.SplitterPosition = 0;
                    splitContainerControl1.IsSplitterFixed = true;
                }
                else
                {
                    m_MenuBar.Visible = false;
                    splitContainerControl1.Panel1.Visible = true;
                    navBarControl.Visible = true;
                    splitContainerControl1.SplitterPosition = 215;

                    splitContainerControl1.IsSplitterFixed = false;
                }
            }
        }
        private void barUsr_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabForm.TabPages.Count == 0)
            {
                return;
            }
            if (e.Control == true && e.KeyData == Keys.F9)
            {
                barBtnClose.PerformClick();
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.F11:
                    Control[] ctrls = Common.Common.GetAllControls(tabForm.TabPages[tabForm.SelectedTabPageIndex].Controls[0]);
                    for (int i = 0; i < ctrls.Length; i++)
                    {
                        Control ctrl = ctrls[i];
                        if (ctrl is SimpleButton || ctrl is Wisol.XSimpleButton)
                        {
                            SimpleButton btn = ctrl as SimpleButton;
                            if (btn.Name == "btnSave")
                            {
                                btn.PerformClick();
                            }
                        }
                    }
                    break;
            }
        }

        private void barBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

                m_SplashScreenManager.ShowWaitForm();
                if (m_PageType == null)
                {
                    if (e.Item.Tag.NullString() == "MANUAL[F12]")
                    {
                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {
                            sfd.FileName = "[MES] Manual.pptx";
                            sfd.Title = "Save Excel File";
                            DialogResult result = sfd.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                Wisol.DataAcess.FileAccess fileAccess = new Wisol.DataAcess.FileAccess(Consts.SERVICE_INFO.ServiceIp);
                                FileObject fileObject = fileAccess.GetFile("MANUAL/[MES] Manual.pptx");
                                File.WriteAllBytes(sfd.FileName, fileObject.FileContent);
                            }
                        }
                    }

                    return;
                }
                switch (e.Item.Tag.NullString())
                {
                    case "INIT[F2]":
                        m_PageType.InitializePage();
                        break;
                    case "SEARCH[F3]":
                        m_PageType.SearchPage();
                        break;
                    case "SAVE_CDT[F6]":
                        m_PageType.SaveCodes();
                        break;
                    case "CLOSE[F9]":
                        tabForm.TabPages[tabForm.SelectedTabPageIndex].Controls.RemoveAt(0);
                        tabForm.TabPages[tabForm.SelectedTabPageIndex].Dispose();
                        break;
                    case "CLOSE_ALL[F10]":
                        for (int i = tabForm.TabPages.Count - 1; i >= 0; i--)
                        {
                            tabForm.TabPages[i].Controls.RemoveAt(0);
                            tabForm.TabPages[i].Dispose();
                        }
                        break;
                    case "MANUAL[F12]":
                        m_PageType.OpenManual();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                SplashScreenToggle(SplashScreenStatus.Off);
            }
        }


        private void tabForm_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                XtraTabControl tabControl = sender as XtraTabControl;
                var arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                (arg.Page as XtraTabPage).Controls.RemoveAt(0);
                (arg.Page as XtraTabPage).Dispose();
            }
            catch
            {
            }
        }

        private void navBarControl_NavPaneMinimizedGroupFormShowing(object sender, NavPaneMinimizedGroupFormShowingEventArgs e)
        {
            if (e.NavPaneForm.IsHandleCreated == true)
            {
                e.NavPaneForm.Location = new Point(e.NavPaneForm.Location.X, this.Location.Y + 116);

                e.NavPaneForm.Size = new Size(230
                                             , this.navBarControl.Size.Height - 2);
            }
        }

        private void navBarControl_NavPaneStateChanged(object sender, EventArgs e)
        {
            Consts.MENU_EXPANDED = this.navBarControl.OptionsNavPane.NavPaneState == NavPaneState.Expanded ? "Y" : "N";

        }

        private void txtnotify_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void Default_StyleChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Consts.DEFAULT_SKIN_INFO))
                {
                    File.Delete(Consts.DEFAULT_SKIN_INFO);
                }
                File.WriteAllText(Consts.DEFAULT_SKIN_INFO, UserLookAndFeel.Default.SkinName);
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        public void SplashScreenToggle(SplashScreenStatus onoff)
        {
            if (onoff == SplashScreenStatus.On)
            {
                m_SplashScreenManager.ShowWaitForm();
            }
            else
            {
                if (m_SplashScreenManager.IsSplashFormVisible) m_SplashScreenManager.CloseWaitForm();
            }
            Focus();
        }

        public void SplashScreenToggle()
        {
            if (m_SplashScreenManager.IsSplashFormVisible)
            {
                m_SplashScreenManager.CloseWaitForm();
            }
            else
            {
                m_SplashScreenManager.ShowWaitForm();
            }
        }

        private void SearchMenuBarItem_EditValueChanged(object sender, EventArgs e)
        {
            GoPage();
        }

        private void SearchMenuBarItem_ItemPress(object sender, ItemClickEventArgs e)
        {
        }
    }

    public enum SplashScreenStatus
    {
        On,
        Off,
    }
}
