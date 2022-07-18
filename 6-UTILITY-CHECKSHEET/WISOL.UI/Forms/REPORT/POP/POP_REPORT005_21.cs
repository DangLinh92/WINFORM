using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT005_21 : FormType
    {

        public POP_REPORT005_21()
        {
            InitializeComponent();
            //Init_Control();
        }

        public POP_REPORT005_21(DataTable dt)
        {
            InitializeComponent();

            Init_Control(dt);
        }

        private void Init_Control(DataTable dt)
        {
            base.mBindData.BindGridView(gcList, dt);
            gvList.Columns["FROM_DATE"].Visible = false;
            gvList.Columns["TO_DATE"].Visible = false;
            gvList.Columns["ITEM_CHECK_ID"].Visible = false;
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            var gridView = sender as GridView;
            int row_index = gridView.FocusedRowHandle;

            string c_code = gvList.GetRowCellDisplayText(row_index, "DEVICE_CODE");
            string c_name = gvList.GetRowCellDisplayText(row_index, "DEVICE_NAME");
            string c_item_name = gvList.GetRowCellDisplayText(row_index, "ITEM_CHECK");
            string c_item_id = gvList.GetRowCellDisplayText(row_index, "ITEM_CHECK_ID");
            string p = gvList.GetRowCellDisplayText(row_index, "FROM_DATE");
            string previous_date = p.Substring(5, 5);
            string c = gvList.GetRowCellDisplayText(row_index, "TO_DATE");
            string current_date = c.Substring(5, 5);
            POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name, previous_date, current_date);
            popup.ShowDialog();
        }
    }
}
