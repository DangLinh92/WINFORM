using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT002 : PageType
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        public SMT002()
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
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT002.INT_LIST");
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

            dtpFrom.EditValue = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
            dtpFrom.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

            dtpTo.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dtpTo.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

            txtMinutes.Properties.MinValue = 1;
            txtMinutes.Properties.MaxValue = 999;
            txtMinutes.Properties.Mask.EditMask = "\\d+";
            txtMinutes.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtMinutes.EditValue = 5;

            txtLostRate.Text = "0.12";
            txtLostRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLostRate.Properties.Mask.EditMask = "n2";

            this.timer1.Enabled = false;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            LineC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LineD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LineE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LineF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LineG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LineH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            LineI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();

            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("172.22.100.102", "LineC");
            //dic.Add("172.22.100.109", "LineD");
            //dic.Add("172.22.100.114", "LineE");
            //dic.Add("172.22.100.113", "LineF");
            //dic.Add("172.22.100.116", "LineG");
            //dic.Add("172.22.100.101", "LineH");
            //dic.Add("172.22.100.118", "LineI");


            //foreach (KeyValuePair<string, string> item in dic)
            //{
            //    Ping ping = new Ping();
            //    PingReply pingReply = ping.Send(item.Key, 200);
            //    int roundtripTime = checked((int)pingReply.RoundtripTime);
            //    if (pingReply.Status == IPStatus.TimedOut)
            //    {
            //        if (item.Value == "LineC")
            //        {
            //            LineC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl1.ForeColor = Color.Red;
            //        }
            //        if (item.Value == "LineD")
            //        {
            //            LineD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl2.ForeColor = Color.Red;
            //        }
            //        if (item.Value == "LineE")
            //        {
            //            LineE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl3.ForeColor = Color.Red;
            //        }
            //        if (item.Value == "LineF")
            //        {
            //            LineF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl4.ForeColor = Color.Red;
            //        }
            //        if (item.Value == "LineG")
            //        {
            //            LineG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl5.ForeColor = Color.Red;
            //        }
            //        if (item.Value == "LineH")
            //        {
            //            LineH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl6.ForeColor = Color.Red;
            //        }
            //        if (item.Value == "LineI")
            //        {
            //            LineI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //            labelControl7.ForeColor = Color.Red;
            //        }
            //    }
            //    else
            //    {
            //        if (item.Value == "LineC")
            //        {
            //            LineC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //        if (item.Value == "LineD")
            //        {
            //            LineD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //        if (item.Value == "LineE")
            //        {
            //            LineE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //        if (item.Value == "LineF")
            //        {
            //            LineF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //        if (item.Value == "LineG")
            //        {
            //            LineG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //        if (item.Value == "LineH")
            //        {
            //            LineH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //        if (item.Value == "LineI")
            //        {
            //            LineI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        }
            //    }
            //}

            if (Math.Floor((dtpTo.DateTime - dtpFrom.DateTime).TotalDays) > 31)
            {
                MsgBox.Show("Khoảng thời gian tối đa 31 ngày. \r\n\r\n Time range is max to 31 days.", MsgType.Warning);
                return;
            }

            //if (dtpFrom.DateTime.Month != dtpTo.DateTime.Month)
            //{
            //    MsgBox.Show("Khoảng thời gian phải trong cùng 1 tháng. \r\n\r\n Time range must be within 1 month.", MsgType.Warning);
            //    return;
            //}

            string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            string year = dtpFrom.DateTime.Year.ToString();
            string month = dtpFrom.DateTime.Month.ToString();
            if(month.Length == 1)
            {
                month = "0" + month;
            }
            try
            {
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable feeder_table = new DataTable();

                //for (int i = 0; i < 7; i++)
                //{
                //    using(SqlConnection conn = new SqlConnection(Program.connectionString))
                //    {
                //        StringBuilder strSqlString = new StringBuilder();

                //        //strSqlString.AppendFormat("Select  t1.Server, t1.lId, t1.Line, t1.Machine_Name, t1.Gantry, t1.Feeder, t1.PartNumber, t2.[Type] as [Type], t2.PRICE as Price, t1.Total_Pickup,t1.Pickup_Error, t1.Vision_Error, t1.Track_Empty, t1.Total_Loss, t1.Total_Loss * t2.price as Total_Loss_Cost, t1.Loss_Rate from \n");
                //        strSqlString.AppendFormat("Select 'N' as [Check], '' as Feeder_ID,  t1.Server, t1.lId, t1.Line, t1.Machine_Name, t1.Gantry, t1.Feeder, t1.PartNumber, t2.[Type] as [Type], t2.PRICE as Price, t1.Total_Pickup,t1.Pickup_Error, t1.Vision_Error, t1.Track_Empty, t1.Total_Loss, t1.Total_Loss * t2.price as Total_Loss_Cost, t1.Loss_Rate from \n");
                //        strSqlString.AppendFormat("( SELECT '[PCCLIENT" + i + "]' as Server, \n");
                //        strSqlString.AppendFormat("compBlock.lId as lId, \n");
                //        strSqlString.AppendFormat("right(Station.strline, charindex('\\', reverse(Station.strline)) - 1) as Line, \n");
                //        strSqlString.AppendFormat("Station.strStation as Machine_Name, \n");
                //        strSqlString.AppendFormat("cast(compPosition.ucTable as smallint) as Gantry, \n");
                //        strSqlString.AppendFormat("compPosition.sTrack as Feeder, \n");
                //        strSqlString.AppendFormat("right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) as PartNumber, \n");
                //        //strSqlString.AppendFormat("EWIPSMTPRI.[TYPE] AS [Type], \n");
                //        //strSqlString.AppendFormat("EWIPSMTPRI.PRICE AS Price, \n");
                //        strSqlString.AppendFormat("sum(compDetail.sAccessTotal) as Total_Pickup, \n");
                //        strSqlString.AppendFormat("sum(compdetail.sRejectVacuum) as Pickup_Error, \n");
                //        strSqlString.AppendFormat("sum(compdetail.sRejectIdent) as Vision_Error, \n");
                //        strSqlString.AppendFormat("sum(compdetail.sTrackEmpty) as Track_Empty, \n");
                //        strSqlString.AppendFormat("(sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty)) as Total_Loss, \n");
                //        //strSqlString.AppendFormat("(sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty))*EWIPSMTPRI.PRICE as Total_Loss_Cost, \n");
                //        strSqlString.AppendFormat("replace(Cast(Round(CAST((sum(compdetail.sRejectIdent)+ sum(compdetail.sRejectVacuum) + sum(compdetail.sTrackEmpty)) as float)/CAST( sum(compDetail.sAccessTotal) as float),4)*100 as numeric(5,2)), '','') as Loss_Rate");
                //        strSqlString.AppendFormat(" FROM \n");
                //        strSqlString.AppendFormat("[PCCLIENT" + i + "].SiplaceOIS.dbo.compBlock  inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.compdetail on compBlock.lIdBlock = compDetail.lIdBlock  \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.compPosition on compDetail.lIdPosition = compPosition.lIdPosition \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.Station on compblock.lId = Station.lId \n");
                //        strSqlString.AppendFormat("inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.PARTNUMBER on PARTNUMBER.lPartNumber = Compposition.lPartNumber \n");
                //        //strSqlString.AppendFormat("inner join EWIPSMTPRI on EWIPSMTPRI.[COMMCODE] =  right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) collate SQL_Latin1_General_CP1_CI_AS \n");
                //        strSqlString.AppendFormat(" WHERE \n");
                //        strSqlString.AppendFormat("dttime >= '{0}' and dttime <= '{1}' \n", fromP, toP);
                //        strSqlString.AppendFormat("group by Station.strline,Station.strStation,compPosition.ucTable,compPosition.sTrack,compblock.lId,PARTNUMBER.strPartNumber \n");// ,EWIPSMTPRI.[TYPE],EWIPSMTPRI.PRICE \n");
                //        strSqlString.AppendFormat(") t1 \n");
                //        strSqlString.AppendFormat(" inner join EWIPSMTPRI t2 on t2.COMMCODE = t1.PartNumber collate SQL_Latin1_General_CP1_CI_AS \n");
                //        strSqlString.AppendFormat(" where t2.YEAR = '{0}' and t2.MONTH = '{1}' \n", year, month);

                //        new SqlDataAdapter(strSqlString.ToString(), conn).Fill(dt);
                //    }
                //}
                //base.m_BindData.BindGridView(gcList, dt);

                //using (SqlConnection conn = new SqlConnection(Program.connectionString))
                //{
                //    SqlCommand cmd = new SqlCommand("PKG_SMT002@GET_LIST_PICKUP", conn);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@A_FROM", SqlDbType.VarChar, 50).Value = fromP;
                //    cmd.Parameters.Add("@A_TO", SqlDbType.VarChar, 50).Value = toP;
                //    cmd.Parameters.Add("@A_YEAR", SqlDbType.VarChar, 50).Value = year;
                //    cmd.Parameters.Add("@A_MONTH", SqlDbType.VarChar, 50).Value = month;
                //    SqlParameter N_RETURN = new SqlParameter("@N_RETURN", SqlDbType.Int)
                //    {
                //        Direction = ParameterDirection.Output
                //    };
                //    SqlParameter V_RETURN = new SqlParameter("@V_RETURN", SqlDbType.NVarChar, 4000)
                //    {
                //        Direction = ParameterDirection.Output
                //    };
                //    cmd.Parameters.Add(N_RETURN);
                //    cmd.Parameters.Add(V_RETURN);

                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    //DataSet ds = new DataSet();
                //    da.Fill(dt);
                //}
                //base.m_BindData.BindGridView(gcList, dt);

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT002.GET_LIST_PICKUP",
                        new string[]
                        {
                            "A_FROM",
                            "A_TO",
                            "A_YEAR",
                            "A_MONTH"
                        },
                        new string[]
                        {
                            fromP,
                            toP,
                            year,
                            month
                        }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    }
                    else
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                    }
                }
                catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }


                var checkEdit = new RepositoryItemCheckEdit
                {
                    Tag = gvList,
                    ValueChecked = "Y",
                    ValueUnchecked = "N",
                    ValueGrayed = "N"
                };
                gcList.RepositoryItems.Add(checkEdit);
                checkEdit.CheckedChanged += CheckEdit_CheckedChanged;

                GridColumn gridColumn = gvList.Columns["Check"];
                gridColumn.ColumnEdit = checkEdit;
                gridColumn.OptionsColumn.AllowEdit = true;
                gridColumn.OptionsColumn.AllowSort = DefaultBoolean.False;
                gridColumn.Width = 70;

                gvList.OptionsBehavior.Editable = true;



                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT002.GET_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    feeder_table = base.m_ResultDB.ReturnDataSet.Tables[0];
                    for (int i = 0; i < gvList.RowCount; i++)
                    {
                        for (int j = 0; j < feeder_table.Rows.Count; j++)
                        {
                            if (gvList.GetDataRow(i)["Line"].ToString() == feeder_table.Rows[j]["Line"].ToString() &&
                               gvList.GetDataRow(i)["Machine_Name"].ToString() == feeder_table.Rows[j]["Machine_Name"].ToString() &&
                               gvList.GetDataRow(i)["Gantry"].ToString() == feeder_table.Rows[j]["Gantry"].ToString() &&
                               gvList.GetDataRow(i)["Feeder"].ToString() == feeder_table.Rows[j]["Feeder"].ToString())
                            {
                                gvList.SetRowCellValue(i, "Feeder_ID", feeder_table.Rows[j]["Feeder_ID"].ToString());
                                gvList.SetRowCellValue(i, "Check", "Y");
                            }
                        }
                    }
                }
                gvList.Columns["Feeder_ID"].Visible = false;

                gvList.Columns["Server"].Visible = false;
                gvList.Columns["lId"].Visible = false;
                gvList.Columns["Price"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Price"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Total_Pickup"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Total_Pickup"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Pickup_Error"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Pickup_Error"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Vision_Error"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Vision_Error"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Track_Empty"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Track_Empty"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Total_Loss"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Total_Loss"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Total_Loss_Cost"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList.Columns["Total_Loss_Cost"].DisplayFormat.FormatString = "n0";
                gvList.Columns["Loss_Rate"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;


                gvList.BeginSort();
                try
                {
                    gvList.ClearSorting();
                    gvList.Columns["Check"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                    gvList.Columns["Total_Loss_Cost"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                }
                finally
                {
                    gvList.EndSort();
                }

                //for(int j = 0; j < 7; j++)
                //{
                //    using (SqlConnection conn2 = new SqlConnection(Program.connectionString))
                //    {
                //        StringBuilder strSqlString = new StringBuilder();

                //        strSqlString.AppendFormat("SELECT '[PCCLIENT" + j + "]' as Server, \n");
                //        strSqlString.AppendFormat("PICKUPERROR.lId as lId, \n");
                //        strSqlString.AppendFormat("right(Station.strline, charindex('\\', reverse(Station.strline)) - 1) as Line, \n");
                //        strSqlString.AppendFormat("Station.strStation as Machine_Name, \n");
                //        strSqlString.AppendFormat("cast(ucTable as smallint) as Gantry, \n");
                //        strSqlString.AppendFormat("cast(ucSegment as smallint) as Segment, \n");
                //        strSqlString.AppendFormat("count(ucSegment) as Total_Error \n");
                //        strSqlString.AppendFormat(" FROM \n");
                //        strSqlString.AppendFormat("[PCCLIENT" + j + "].SiplaceOIS.dbo.PICKUPERROR  inner join [PCCLIENT" + j + "].SiplaceOIS.dbo.Station on PICKUPERROR.lId = Station.lId   \n");
                //        strSqlString.AppendFormat(" WHERE \n");
                //        strSqlString.AppendFormat("dttime >= '{0}' and dttime <= '{1}' AND ucSegment > 0 \n", fromP, toP);
                //        strSqlString.AppendFormat("group by PICKUPERROR.lId,ucTable,ucSegment, Station.strStation ,Station.strline \n");

                //        new SqlDataAdapter(strSqlString.ToString(), conn2).Fill(dt2);
                //    }
                //}

                //base.m_BindData.BindGridView(gcList2, dt2);

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT002.GET_LIST_PICKUP_RIGHT",
                        new string[]
                        {
                            "A_FROM",
                            "A_TO"
                        },
                        new string[]
                        {
                            fromP,
                            toP
                        }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        base.m_BindData.BindGridView(gcList2, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    }
                    else
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                    }
                }
                catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }

                gvList2.Columns["Server"].Visible = false;
                gvList2.Columns["lId"].Visible = false;
                gvList2.Columns["Total_Error"].DisplayFormat.FormatType = FormatType.Numeric;
                gvList2.Columns["Total_Error"].DisplayFormat.FormatString = "n0";

                gvList2.BeginSort();
                try
                {
                    gvList2.ClearSorting();
                    gvList2.Columns["Total_Error"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                }
                finally
                {
                    gvList2.EndSort();
                }

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            bool currenValue = edit.Checked;
            int[] x = gvList.GetSelectedRows();
            string gantry = gvList.GetDataRow(x[0])["Gantry"].NullString();
            string feeder = gvList.GetDataRow(x[0])["Feeder"].NullString();
            string lid = gvList.GetDataRow(x[0])["lId"].NullString();
            string server = gvList.GetDataRow(x[0])["Server"].NullString();
            string feeder_id = gvList.GetDataRow(x[0])["Feeder_ID"].NullString();
            string line = gvList.GetDataRow(x[0])["Line"].NullString();
            string machine_name = gvList.GetDataRow(x[0])["Machine_Name"].NullString();
            //MsgBox.Show(currenValue.ToString() + " Server:  " + server + " Feeder_ID: " + feeder_id + " Gantry: " + gantry + " Feeder: " + feeder + " lid: " + lid );

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT002.PUT_CHECK_ITEM"
                    , new string[] { "A_SERVER",
                        "A_LINE",
                        "A_MACHINE_NAME",
                        "A_FEEDER_ID",
                        "A_CHECK",
                        "A_LID",
                        "A_GANTRY",
                        "A_FEEDER"
                    }
                    , new string[] { server,
                        line,
                        machine_name,
                        feeder_id,
                        currenValue.ToString(),
                        lid,
                        gantry,
                        feeder
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {

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
            try
            {
                double loss_rate = Double.Parse(txtLostRate.EditValue.ToString());

                if (e.RowHandle < 0)
                {
                    return;
                }
                if (e.Column.FieldName == "Loss_Rate")
                {
                    if (Double.Parse(gvList.GetDataRow(e.RowHandle)["Loss_Rate"].NullString()) > loss_rate)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Transparent;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void chkRealTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.EditValue = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
            dtpTo.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (chkRealTime.Checked)
            {
                int minute = Int32.Parse(txtMinutes.EditValue.ToString());
                timer1.Interval = minute*60*1000;
                timer1.Enabled = true;
                this.SearchPage();
            }
            else
            {
                timer1.Enabled = false;
                this.SearchPage();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtpFrom.EditValue = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
            dtpTo.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.SearchPage();
        }


        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0)
            //        return;
            //    else
            //    {
            //        string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            //        string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            //        string Server = gvList.GetDataRow(e.RowHandle)["Server"].NullString();
            //        string lId = gvList.GetDataRow(e.RowHandle)["lId"].NullString();
            //        string Machine_Name = gvList.GetDataRow(e.RowHandle)["Machine_Name"].NullString();
            //        string ucTable = gvList.GetDataRow(e.RowHandle)["Gantry"].NullString();
            //        string sTrack = gvList.GetDataRow(e.RowHandle)["Feeder"].NullString();

            //        POP.POP_SMT002_1 popup = new POP.POP_SMT002_1(fromP, toP, Server, lId, Machine_Name, ucTable, sTrack);
            //        popup.ShowDialog();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
        }

        private void gvList2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0)
            //        return;
            //    else
            //    {
            //        string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            //        string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            //        string Server = gvList2.GetDataRow(e.RowHandle)["Server"].NullString();
            //        string lId = gvList2.GetDataRow(e.RowHandle)["lId"].NullString();
            //        string Machine_Name = gvList2.GetDataRow(e.RowHandle)["Machine_Name"].NullString();
            //        string ucTable = gvList2.GetDataRow(e.RowHandle)["Gantry"].NullString();
            //        string Segment = gvList2.GetDataRow(e.RowHandle)["Segment"].NullString();

            //        POP.POP_SMT002_2 popup = new POP.POP_SMT002_2(fromP, toP, Server, lId, Machine_Name, ucTable, Segment);
            //        popup.ShowDialog();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
        }

        private void txtMinutes_EditValueChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                dtpFrom.EditValue = DateTime.Now.AddHours(-2).ToString("yyyy-MM-dd HH:mm:ss");
                dtpTo.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                timer1.Enabled = false;
                int minute = Int32.Parse(txtMinutes.EditValue.ToString());
                timer1.Interval = minute * 60 * 1000;
                timer1.Enabled = true;
            }
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            int indexRow = gvList.FocusedRowHandle;

            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
                string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
                string Server = gvList.GetDataRow(indexRow)["Server"].NullString();
                string lId = gvList.GetDataRow(indexRow)["lId"].NullString();
                string Machine_Name = gvList.GetDataRow(indexRow)["Machine_Name"].NullString();
                string ucTable = gvList.GetDataRow(indexRow)["Gantry"].NullString();
                string sTrack = gvList.GetDataRow(indexRow)["Feeder"].NullString();
                string partNumber = gvList.GetDataRow(indexRow)["PartNumber"].NullString();

                POP.POP_SMT002_1 popup = new POP.POP_SMT002_1(fromP, toP, Server, lId, Machine_Name, ucTable, sTrack, partNumber);
                popup.ShowDialog();
            }
        }

        private void gvList2_DoubleClick(object sender, EventArgs e)
        {
            int indexRow = gvList2.FocusedRowHandle;

            if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
                string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
                string Server = gvList2.GetDataRow(indexRow)["Server"].NullString();
                string lId = gvList2.GetDataRow(indexRow)["lId"].NullString();
                string Machine_Name = gvList2.GetDataRow(indexRow)["Machine_Name"].NullString();
                string ucTable = gvList2.GetDataRow(indexRow)["Gantry"].NullString();
                string Segment = gvList2.GetDataRow(indexRow)["Segment"].NullString();

                POP.POP_SMT002_2 popup = new POP.POP_SMT002_2(fromP, toP, Server, lId, Machine_Name, ucTable, Segment);
                popup.ShowDialog();
            }
        }
    }
}
