using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WHC_FinaceService
{
    public static class ExchangeRateDownload
    {
        /// <summary>
        /// yyyyMMdd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static async Task<string> DownloadAsync(string date)
        {
            string exchangeRate = await GetWebContent("https://woori.com.vn/vn/hs/os/HSOS211_01C_01.frag?cnvCrcd&basCrcd=VND&staDt=" + date + "&brCd=100++");
            return exchangeRate;
        }

        /// In ra thông tin các Header của HTTP Response
        public static void ShowHeaders(HttpHeaders headers)
        {
            Console.WriteLine("CÁC HEADER:");
            foreach (var header in headers)
            {
                foreach (var value in header.Value)
                {
                    Console.WriteLine($"{header.Key,25} : {value}");

                }
            }
            Console.WriteLine();
        }

        // Tải về trang web và trả về chuỗi nội dung
        public static async Task<string> GetWebContent(string url)
        {
            // Khởi tạo http client
            using (var httpClient = new HttpClient())
            {
                // Thiết lập các Header nếu cần
                httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                try
                {
                    // Thực hiện truy vấn GET
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    // Hiện thị thông tin header trả về
                    ShowHeaders(response.Headers);

                    // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"Tải thành công - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                    Console.WriteLine("Starting read data");

                    // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG
                    string htmltext = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Nhận được {htmltext.Length} ký tự");
                    Console.WriteLine();

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmltext);

                    HtmlNode[] nodes = doc.DocumentNode.SelectNodes("//table/tbody/tr").ToArray();
                    string usd = "";
                    string krw = "";
                    foreach (HtmlNode item in nodes)
                    {
                        if (item.InnerHtml.Contains("USD"))
                        {
                            var tds = item.Elements("td").ToList();
                            usd = tds[4].InnerText;
                        }
                        else if (item.InnerHtml.Contains("KRW"))
                        {
                            var tds = item.Elements("td").ToList();
                            krw = tds[4].InnerText;
                        }
                    }

                    return usd.Trim().Replace(",", "") + " USD-" + krw.Trim().Replace(",", "") + " KRW";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    }
}
