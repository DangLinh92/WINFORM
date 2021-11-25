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
using Wisol.MES.Classes;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class FRM_EXCHANGE_RATE : PageType
    {
        public FRM_EXCHANGE_RATE()
        {
            InitializeComponent();
            this.Load += FRM_EXCHANGE_RATE_Load;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeReal.EditValue = DateTime.Now;
        }

        private async void FRM_EXCHANGE_RATE_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_EXCHANGE_RATE");

            dateFrom.EditValue = DateTime.Now.AddDays(-5);
            dateTo.EditValue = DateTime.Now;
            dateSearch.EditValue = DateTime.Now;

            string date = DateTime.Now.ToString("yyyyMMdd");
            await GetLatestExchange(date);

            InitData();
        }

        private async Task GetLatestExchange(string date)
        {
            try
            {
                var exchange = await ExchangeRateDownload.DownloadAsync(date);

                if (exchange != null && exchange.Contains("-"))
                {
                    txtUSD_Latest.Text = exchange.Split('-')[0].Split(' ')[0];

                    if (exchange.Split('-')[1].Split(' ')[0].Trim() != "")
                    {
                        txtKrw_Latest.Text = (float.Parse(exchange.Split('-')[1].Split(' ')[0].Trim()) / 100).ToString();
                    }
                    else
                    {
                        txtKrw_Latest.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_EXCHANGE.INIT", new string[] { "A_FROM_DATE", "A_TO_DATE" }, new string[] { dateFrom.EditValue.NullString(), dateTo.EditValue.NullString() });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    //base.m_BindData.BindGridView(gcList, data);
                    gcList.DataSource = data;

                    gvList.Columns["RATE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["RATE"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["SYS_DATE"].Visible = false;
                    gvList.Columns["USER_UPDATE"].Visible = false;
                    gvList.Columns["MONTH"].Visible = false;
                    gvList.Columns["YEAR"].Visible = false;
                    gvList.Columns["DAY"].Visible = false;

                    gvList.OptionsView.ColumnAutoWidth = true;

                    DateTime searchTime = DateTime.Parse(dateSearch.EditValue.NullString());
                    foreach (DataRow row in data.Rows)
                    {
                        if (int.Parse(row["DAY"].NullString()) == searchTime.Day &&
                            int.Parse(row["MONTH"].NullString()) == searchTime.Month &&
                            int.Parse(row["YEAR"].NullString()) == searchTime.Year &&
                            row["EX_FROM"].NullString() == "USD" &&
                            row["EX_TO"].NullString() == "VND")
                        {
                            txtUSD.Text = row["RATE"].NullString();
                        }

                        if (int.Parse(row["DAY"].NullString()) == searchTime.Day &&
                            int.Parse(row["MONTH"].NullString()) == searchTime.Month &&
                            int.Parse(row["YEAR"].NullString()) == searchTime.Year &&
                            row["EX_FROM"].NullString() == "KRW" &&
                            row["EX_TO"].NullString() == "VND")
                        {
                            txtKRW.Text = row["RATE"].NullString();
                        }

                        if (txtUSD.Text.NullString() != "" && txtKRW.Text != "")
                        {
                            break;
                        }
                    }

                    if (dateSearch.EditValue.NullString() != "" && searchTime.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        bool needUpdate = false;
                        if (txtUSD.Text.NullString() == "")
                        {
                            txtUSD.Text = txtUSD_Latest.Text.Trim();
                            needUpdate = true;
                        }

                        if (txtKRW.Text.NullString() == "")
                        {
                            txtKRW.Text = txtKrw_Latest.Text.Trim();
                            needUpdate = true;
                        }

                        if (needUpdate)
                        {
                            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_EXCHANGE.PUT",
                                                                            new string[] { "A_DATE", "A_USD", "A_KRW", "A_USER" },
                                                                            new string[]
                                                                            {
                                                                               dateSearch.EditValue.NullString(),
                                                                               txtUSD.EditValue.NullString(),
                                                                               txtKRW.EditValue.NullString(),
                                                                               Consts.USER_INFO.Id
                                                                            });

                            if (base.m_ResultDB.ReturnInt != 0)
                            {
                                MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                            }
                        }
                    }
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (dateSearch.EditValue.NullString() == "")
            {
                MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                dateSearch.Focus();
                return;
            }

            try
            {
                var exchange = await ExchangeRateDownload.DownloadAsync(DateTime.Parse(dateSearch.EditValue.NullString()).ToString("yyyyMMdd"));

                if (exchange.Contains("-"))
                {
                    txtUSD.Text = exchange.Split('-')[0].Split(' ')[0];
                    txtKRW.Text = (float.Parse(exchange.Split('-')[1].Split(' ')[0].Trim()) / 100).ToString();

                    MsgBox.Show("MSG_COM_004".Translation(), MsgType.Information);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateSearch.EditValue.NullString() == "" || txtUSD.EditValue.NullString() == "" || txtKRW.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    dateSearch.Focus();
                    return;
                }

                DialogResult dialogResult = MsgBox.Show("XÁC NHẬN CẬP NHẬT TỈ GIÁ".Translation(), MsgType.Warning, Components.DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_EXCHANGE.PUT",
                    new string[] { "A_DATE", "A_USD", "A_KRW", "A_USER" },
                    new string[]
                    {
                        dateSearch.EditValue.NullString(),
                        txtUSD.EditValue.NullString(),
                        txtKRW.EditValue.NullString(),
                        Consts.USER_INFO.Id
                    });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSearchList_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_EXCHANGE.INIT", new string[] { "A_FROM_DATE", "A_TO_DATE" }, new string[] { dateFrom.EditValue.NullString(), dateTo.EditValue.NullString() });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    //base.m_BindData.BindGridView(gcList, data);
                    gcList.DataSource = data;

                    gvList.Columns["RATE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["RATE"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["SYS_DATE"].Visible = false;
                    gvList.Columns["USER_UPDATE"].Visible = false;
                    gvList.Columns["MONTH"].Visible = false;
                    gvList.Columns["YEAR"].Visible = false;
                    gvList.Columns["DAY"].Visible = false;

                    gvList.OptionsView.ColumnAutoWidth = true;
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dateFrom.EditValue = null;
            dateTo.EditValue = null;
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.Column.FieldName == "DATE" && e.CellValue.NullString() == DateTime.Now.ToString("yyyy/MM/dd"))
            {
                e.Appearance.BackColor = Color.FromArgb(42, 190, 37);
            }
        }
    }
}
