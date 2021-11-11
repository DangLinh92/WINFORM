using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WHC_FinaceService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer = null;
        public DBAccess m_DBaccess = null;
        public ResultDB m_ResultDB = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            m_DBaccess = new DBAccess();
            m_ResultDB = new ResultDB();

            // Tạo 1 timer từ libary System.Timers
            timer = new Timer();
            // Execute mỗi 60s
            timer.Interval = 60000;
            // Những gì xảy ra khi timer đó dc tick
            timer.Elapsed += timer_Tick;
            // Enable timer
            timer.Enabled = true;
            // Ghi vào log file khi services dc start lần đầu tiên
            Utilities.WriteLogError("Test for 1st run WindowsService");

            isUpdate = false;
        }

        protected override void OnStop()
        {
            // Ghi log lại khi Services đã được stop
            timer.Enabled = false;
            Utilities.WriteLogError("1st WindowsService has been stop");
        }

        bool isUpdate = false;
        private async void timer_Tick(object sender, ElapsedEventArgs args)
        {
            try
            {
                Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd")+"---1---"+ DateTime.Now.Hour);
                if (((DateTime.Now.Hour == 8 || DateTime.Now.Hour == 16) && DateTime.Now.Minute >= 30) && !isUpdate)
                {
                    string exchange = await ExchangeRateDownload.DownloadAsync(DateTime.Now.ToString("yyyyMMdd"));

                    Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd") + "---2");

                    if (exchange.Contains("-"))
                    {
                        Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd") + "---3");

                        string usd = exchange.Split('-')[0].Split(' ')[0];
                        string krw = (float.Parse(exchange.Split('-')[1].Split(' ')[0].Trim()) / 100).ToString();

                        Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd") + " Exchange:" + exchange);

                        if (float.TryParse(usd, out _) && float.TryParse(krw, out _))
                        {
                            m_ResultDB = m_DBaccess.ExcuteProc("PKG_BUSINESS_EXCHANGE.PUT",
                                                              new string[] { "A_DATE", "A_USD", "A_KRW", "A_USER" },
                                                              new string[]
                                                              {
                                                                                  DateTime.Now.ToString("yyyy-MM-dd"),
                                                                                  usd.Trim(),
                                                                                  krw.Trim(),
                                                                                  "WHC_FinaceService"
                                                              });

                            if (m_ResultDB.ReturnInt == 0)
                            {
                                Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd") + "OK:" + m_ResultDB.ReturnString);
                                isUpdate = true;
                            }
                            else
                            {
                                isUpdate = false;
                                Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd") + "ERROR:" + m_ResultDB.ReturnString);
                            }
                        }
                    }
                }

                if (DateTime.Now.Hour == 1)
                {
                    isUpdate = false;
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteLogError(DateTime.Now.ToString("yyyyMMdd") + "ERROR1:" + ex.Message);
                isUpdate = false;
            }
        }
    }
}
