using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT.POP
{
    public partial class POP_SMT002_1 : FormType
    {

        public POP_SMT002_1(string fromP, string toP, string Server, string lId, string Machine_Name, string ucTable, string sTrack, string partNumber)
        {
            InitializeComponent();

            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT002.GET_POP_1"
                    , new string[] { "A_FROM",
                        "A_TO",
                        "A_SERVER",
                        "A_LID",
                        "A_MACHINE_NAME",
                        "A_UCTABLE",
                        "A_STRACK",
                        "A_PART_NUMBER"
                    }
                    , new string[] { fromP,
                                     toP,
                                     Server,
                                     lId,
                                     Machine_Name,
                                     ucTable,
                                     sTrack,
                                     partNumber
                    }
                    );
                if (mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridView(gcList,
                        base.mResultDB.ReturnDataSet.Tables[0]
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
