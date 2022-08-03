using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraCharts;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Spreadsheet;
using System.Drawing.Imaging;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT004 : PageType
    {
        public REPORT004()
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
            

        }

        public override void SearchPage()
        {

        }

        
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
