using System;

using Wisol.Common;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT.POP
{
    public partial class POP_SMT002_2 : FormType
    {

        public POP_SMT002_2(string fromP, string toP, string Server, string lId, string Machine_Name, string ucTable, string Segment)
        {
            InitializeComponent();



            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT002.GET_POP_2"
                    , new string[] { "A_FROM",
                        "A_TO",
                        "A_SERVER",
                        "A_LID",
                        "A_MACHINE_NAME",
                        "A_UCTABLE",
                        "A_SEGMENT"
                    }
                    , new string[] { fromP,
                                     toP,
                                     Server,
                                     lId,
                                     Machine_Name,
                                     ucTable,
                                     Segment
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
