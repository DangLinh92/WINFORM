using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT017 : PageType
    {
        public SMT017()
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

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT017.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {

                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    base.m_BindData.BindGridView(gcList2,
                        base.m_ResultDB.ReturnDataSet.Tables[1]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            base.InitializePage();

        }

        public override void SearchPage()
        {
            if(txtLotNo.Text.Trim() == string.Empty && txtMaterial.Text.Trim() == string.Empty)
            {
                return;
            }

            base.SearchPage();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT017.GET_LIST"
                , new string[] { "A_LOT_NO", "A_MATERIAL"},
                  new string[] { txtLotNo.Text.Trim(), txtMaterial.Text.Trim()  }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                      base.m_ResultDB.ReturnDataSet.Tables[0]
                      );
                    base.m_BindData.BindGridView(gcList2,
                     base.m_ResultDB.ReturnDataSet.Tables[1]
                     );
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

        }


        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.AbsoluteIndex == 7 )
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        if(cellValue.ToUpper() == "ĐANG SẢN XUẤT")
            //        {
            //            e.Appearance.BackColor = Color.Lime;
            //        }
            //        if (cellValue.ToUpper() == "CẦN HỦY")
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
            //        }
            //    }
            //}
        }

    }
}
