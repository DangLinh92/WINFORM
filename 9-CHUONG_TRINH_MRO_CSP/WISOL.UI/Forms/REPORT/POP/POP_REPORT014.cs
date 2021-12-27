using System;
using System.Data;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT014 : FormType
    {

        string feeder = string.Empty;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public POP_REPORT014()
        {
            InitializeComponent();
        }

        public POP_REPORT014(string _feeder)
        {
            InitializeComponent();

            feeder = _feeder;

            Init_Control();
        }

        private void Init_Control()
        {
            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT014.POP_GET_LIST"
                    , new string[] { "A_FEEDER"
                    }
                    , new string[] { feeder
                    }
                    );
                if (mResultDB.ReturnInt == 0)
                {
                    dt1 = base.mResultDB.ReturnDataSet.Tables[0];
                    //dt2 = base.mResultDB.ReturnDataSet.Tables[1];
                    //dt1 = SetColumnsOrder(dt1, dt2);
                    base.mBindData.BindGridView(gcList,
                        dt1
                        , true
                        );
                }
                //gvList.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                //gvList.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                //gvList.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                //gvList.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                //for (int i = 4; i < gvList.Columns.Count; i++)
                //{
                //    gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //    gvList.Columns[i].DisplayFormat.FormatString = "n4";
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private static DataTable SetColumnsOrder(DataTable table, DataTable columnNames)
        {
            int columnIndex = 4;

            for (int i = 0; i < columnNames.Rows.Count; i++)
            {
                table.Columns[columnNames.Rows[i][0].ToString()].SetOrdinal(columnIndex);
                columnIndex++;
            }
            return table;
        }

    }
}
