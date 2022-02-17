using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Net;
using System.Text;
using WebClientHelper;

namespace EchoDownloader
{
    internal class EchoCloudCourse
    {
        public string Uuid { get; set; }
        public string Host { get; set; }
        public IWebDriver Driver { get; set; }
        public System.Net.Cookie[] Cookies { get; set; }
        public string Name
        {
            get
            {
                Driver.Navigate().GoToUrl($"{Host}/section/{Uuid}/home");
                return Driver.FindElements(By.TagName("h1"))[0].FindElements(By.TagName("span"))
                    .Where(x => x.GetAttribute("class") != "dash")
                    .First()
                    .Text;
            }
        }
        public EchoCloudVideo[] Videos
        {
            get
            {
                return ((JArray)GetCourseData()["data"]).AsQueryable()
                    .Select(a => new EchoCloudVideo((JObject)a,Cookies, Driver, Host))
                    .OrderBy(a => a.Date)
                    .ToArray();
            }
        }
        public EchoCloudCourse(IWebDriver driver, System.Net.Cookie[] cookies,string uuid, string host)
        {
            Uuid = uuid;
            Host = host;
            Driver = driver;
            Cookies = cookies;
        }

        private JObject GetCourseData()
        {
            var client = new WebClientEx
            {
                Encoding = Encoding.UTF8,
                CookieContainer = new CookieContainer()
            };
            Cookies.ToList().ForEach(cookie => client.CookieContainer.Add(new Uri(Host), cookie));
            string json = client.DownloadString($"{Host}/section/{Uuid}/syllabus");
            return JObject.Parse(json);
        }
    }
}
