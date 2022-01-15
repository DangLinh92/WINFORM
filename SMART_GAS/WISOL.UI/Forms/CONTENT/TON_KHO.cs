using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class TON_KHO : PageType
    {
        public TON_KHO()
        {
            InitializeComponent();
            this.Load += TON_KHO_Load;
        }

        private void TON_KHO_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "TON_KHO");
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@INIT_TONKHO", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection datas = base.m_ResultDB.ReturnDataSet.Tables;
                    gcList.DataSource = datas[0];

                    m_BindData.BindGridLookEdit(stlDepartment, datas[1],"Id","Name");

                    gvList.OptionsView.ColumnAutoWidth = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@SEARCH_TONKHO", 
                    new string[] { "A_DEPT_CODE", "A_MONTH" }, 
                    new string[] {stlDepartment.EditValue.NullString(),dateSearch.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.OptionsView.ColumnAutoWidth = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
