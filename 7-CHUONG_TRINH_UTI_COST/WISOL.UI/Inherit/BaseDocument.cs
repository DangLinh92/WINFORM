using DevExpress.XtraReports.UI;
using System;
using Wisol.Common;
using Wisol.Components;

namespace Wisol.MES.Inherit
{
    public partial class BaseDocument : DevExpress.XtraReports.UI.XtraReport
    {
        public BaseDocument()
        {
            InitializeComponent();
        }

        public void PrintReport()
        {
            try
            {
                this.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.ToString(), MsgType.Warning);
            }
        }

        protected void SetLanguage()
        {
            foreach (XRControl con in this.fXRControls)
            {
                if ((con is Band) == false)
                    continue;

                foreach (XRControl subCon in con.Band.Controls)
                {
                    if (subCon is XRLabel)
                    {
                        subCon.Text = subCon.Text.NullString().Trim().Translation();
                        continue;
                    }

                    if ((subCon is XRTable) == false)
                        continue;

                    foreach (XRControl tableRows in subCon.Controls)
                    {
                        if ((tableRows is XRTableRow) == false)
                            continue;

                        foreach (XRControl cell in tableRows.Controls)
                        {
                            if ((cell is XRTableCell) == false)
                                continue;

                            cell.Text = cell.Text.NullString().Trim().Translation();
                        }
                    }
                }
            }
        }
    }
}
