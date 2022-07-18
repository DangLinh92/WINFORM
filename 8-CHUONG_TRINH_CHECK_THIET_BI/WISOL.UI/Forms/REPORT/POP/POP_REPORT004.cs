using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT004 : FormType
    {
        DataTable dtCode = new DataTable();
        DataTable dtItem = new DataTable();

        public POP_REPORT004()
        {
            InitializeComponent();
        }
        public POP_REPORT004(string input)
        {
            InitializeComponent();
            try
            {


            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
           
        }

        private void POP_REPORT004_Load(object sender, EventArgs e)
        {
            base.mBindData.BindGridView(gcList1,
                                            REPORT001.dt_detail
                                         );
        }
    }
}
