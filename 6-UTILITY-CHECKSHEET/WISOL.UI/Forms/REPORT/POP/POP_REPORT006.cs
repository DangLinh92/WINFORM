using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT006 : FormType
    {

        public POP_REPORT006()
        {
            InitializeComponent();
            //Init_Control();
        }

        public POP_REPORT006(string deviceId)
        {
            InitializeComponent();

            Init_Control(deviceId);
        }

        private void Init_Control(string deviceId)
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT006.POP_GET_ITEM"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_DEVICE_ID"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        deviceId
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridView(gcList,
                       base.mResultDB.ReturnDataSet.Tables[0]
                       );
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

    }
}
