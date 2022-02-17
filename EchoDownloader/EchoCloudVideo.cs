using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using ListHelper;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace EchoDownloader
{
    internal class EchoCloudVideo
    {
        public string Host { get; set; }
        public IWebDriver Driver { get; set; }
        public bool IsMultipart { get; private set; }
        public EchoCloudVideo[] SubVideos { get; private set; }
        public string Date { get; private set; }
        public string VideoId { get; private set; }
        protected string title;
        public string[] Url { get; private set; }
        public System.Net.Cookie[] Cookies { get; set; }
        public string Title
        {
            get
            {
                return this.title;
            }
        }
        public EchoCloudVideo(JObject json, System.Net.Cookie[] cookies, IWebDriver driver, string host)
        {
            Driver = driver;
            Host = host;
            SubVideos = new[] { this };
            if (json["lessons"] != null)
            {
                SubVideos = json["lessons"].Select(a => new EchoCloudSubVideo((JObject)a, cookies,Driver, Host, (string)a["groupInfo"]["name"])).ToArray();
                IsMultipart = true;
                Date = GetDateFromJson(json);
                return;
            }
            VideoId = json["lesson"]["lesson"]["id"].ToString();
            Driver.Navigate().GoToUrl(GetVideoUrl());
            Url = LoopFindM3u8Url(json, GetVideoUrl(), 30);

            Date = GetDateFromJson(json);
            title = json["lesson"]["lesson"]["name"].ToString();
            Cookies = cookies;
        }

        public static string GetDateFromJson(JObject json)
        {
            var jsonStartTime = json["startTime"];
            if (jsonStartTime != null && DateTime.TryParse(jsonStartTime.ToString(), out DateTime startTime))
            {
                return startTime.ToString("D");
            }
            else
            {
                return "1970-01-01";
            }
        }
        private string GetVideoUrl()
        {
            return $"{Host}/lesson/{VideoId}/classroom";
        }
        private string[] LoopFindM3u8Url(JObject json, string videoUrl, ushort maxAttempts = 5)
        {
            if (FromJsonMp4(json, out string url))
            {
                return new string[] { url };
            }
            FromJsonM3u8(json, out string[] m3u8urls);
            if (BruteForceGetMp4Url(videoUrl, maxAttempts, out string[] urls))
            {
                return urls;
            }
            if (!BruteForceGetUrl("m3u8", videoUrl, maxAttempts, out m3u8urls))
            {
                throw new Exception("動画URLの取得に失敗しました");
            }

            m3u8urls = m3u8urls.Where(a => a.EndsWith("av.m3u8")).ToArray();
            if (m3u8urls.Length == 0)
            {
                return null;
            }
            m3u8urls = m3u8urls.Reverse().ToArray();
            return new string[] { m3u8urls[0], m3u8urls[1] };
        }

        private bool BruteForceGetUrl(string Suffix, string videoUrl, ushort maxAttempts, out string[] urls)
        {
            try
            {
                int refreshAttempt = 1;
                int staleAttempt = 1;
                while (true)
                {
                    Driver.Navigate().GoToUrl(videoUrl);
                    string html = Driver.PageSource;
                    Console.WriteLine(html);
                    try
                    {
                        urls = Regex.Matches(html.Replace("\\", "/"), $"https://[^,\"]*?[.]{Suffix}").AsEnumerable().Select(a => a.Value.Replace("//", "/")).ToArray();
                        return true;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        if (refreshAttempt >= maxAttempts)
                        {
                            urls = null;
                            return false;
                        }
                        refreshAttempt++;
                    }
                    catch (StaleElementReferenceException)
                    {
                        if (staleAttempt >= maxAttempts)
                        {
                            urls = null;
                            return false;
                        }
                        staleAttempt++;
                    }
                }

            }
            catch (Exception)
            {
                urls = null;
                return false;
            }
        }

        private bool BruteForceGetMp4Url(string videoUrl, ushort maxAttempts, out string[] urls)
        {
            try
            {
                bool result = BruteForceGetUrl("mp4", videoUrl, maxAttempts, out urls);
                urls = new string[] { urls[0], urls[1] };
                return result;

            }
            catch (Exception)
            {
                urls = null;
                return false;
            }
        }

        private bool FromJsonM3u8(JObject json, out string[] urls)
        {
            try
            {
                if ((!(bool)json["lesson"]["hasVideo"]) || (!(bool)json["lesson"]["hasAvailableVideo"]))
                {
                    urls = null;
                    return false;
                }

                urls = json["lesson"]["video"]["media"]["versions"][0]["manifests"]
                    .Select(a => new Uri(a["uri"].ToString()))
                    .Select(a => $"{a.Scheme}://content.{(new Uri(Host)).Authority}{a.PathAndQuery}")
                    .ToArray();
                return true;

            }
            catch (Exception)
            {
                urls = null;
                return false;
            }
        }

        private bool FromJsonMp4(JObject json, out string url)
        {
            if (json["lesson"] != null &&
                json["lesson"]["video"] != null &&
                json["lesson"]["video"]["media"] != null &&
                json["lesson"]["video"]["media"]["media"] != null &&
                json["lesson"]["video"]["media"]["media"]["current"] != null &&
                json["lesson"]["video"]["media"]["media"]["current"]["primaryFiles"] != null
                )
            {
                string[] urls = json["lesson"]["video"]["media"]["media"]["current"]["primaryFiles"].Select(a => a["s3Url"].ToString()).ToArray();
                if (urls.Length == 0)
                {
                    url = null;
                    return false;
                }
                url = urls.Last();
                return true;
            }
            else
            {
                url = null;
                return false;
            }
        }
        public bool Download(string outputDirectory, string fileName)
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var client = new WebClient();
            var cookies = string.Join("; ", Cookies.ToList().Select(cookie => $"{cookie.Name}={cookie.Value}").ToArray());

            string url = Url[0];

            return DownloadSingle(cookies, url, outputDirectory, fileName);
        }
        public static bool DownloadSingle(string cookies, string url, string outputDirectory, string fileName)
        {
            string option = "-y -headers \"Cookie: " + cookies + $"\" -i \"{url}\" -c copy -y -nostdin \"" + Path.Combine(outputDirectory, fileName) + "\"";
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "ffmpeg",
                Arguments = option,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            Process p = Process.Start(info);
            p.WaitForExit();
            return true;
        }
    }


    internal class EchoCloudSubVideo : EchoCloudVideo
    {
        public string Group { get; set; }
        public EchoCloudSubVideo(JObject json, System.Net.Cookie[] cookies, IWebDriver driver, string host, string group) : base(json,cookies, driver, host)
        {
            this.Group = group;
        }
        public new string Title
        {
            get
            {
                return $"{Group} - {title}";
            }
        }
    }
}
