using System;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.LOGDAT.POP
{
    public partial class POP_LOGDAT002 : FormType
    {

        string fromDate = string.Empty;
        string toDate = string.Empty;
        string formCode = string.Empty;
        string userId = string.Empty;
        public POP_LOGDAT002()
        {
            InitializeComponent();
        }

        public POP_LOGDAT002(string _fromDate, string _toDate, string _formCode, string _userId)
        {
            InitializeComponent();

            fromDate = _fromDate;
            toDate = _toDate;
            formCode = _formCode;
            userId = _userId;

            Init_Control();
        }





        private void Init_Control()
        {
            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_LOGDAT002.POP_GET_LIST"
                    , new string[] { "A_PLANT",
                        "A_FROM_DATE",
                        "A_TO_DATE",
                        "A_FORM_CODE",
                        "A_USER_ID",
                        "A_LANG"
                    }
                    , new string[] { Consts.PLANT,
                        fromDate,
                        toDate,
                        formCode,
                        userId,
                        Consts.USER_INFO.Language
                    }
                    );
                if (mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridView(gcList,
                        base.mResultDB.ReturnDataSet.Tables[0]
                        , false
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }








    }
}
