using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace EchoDownloader
{
    public partial class ProgressDialog : Form
    {
        public string Url { get; private set; }
        public string OutputDirectory { get; private set; }
        public Browser Browser { get; private set; }
        public bool IsOpenExplorer { get;private set; }
        private string outputPath;
        public ProgressDialog(string url, string outputDirectory, Browser browser, bool isOpenExplorer)
        {
            InitializeComponent();

            Url = url;
            OutputDirectory = outputDirectory;
            Browser = browser;
            IsOpenExplorer = isOpenExplorer;
        }

        private void DownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            var thisWorker = (BackgroundWorker)sender;
            thisWorker.ReportProgress(1, $"URLが設定されました。（{Url})");
            if (thisWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            string courseUuid = Regex.Match(Url, "[^/]([0-9a-zA-Z]+[-])+[0-9a-zA-Z]+").Value;
            string courseHostName = Regex.Match(Url, "https?:[/]{2}[^/]*").Value;
            thisWorker.ReportProgress(2, $"URLの解析が完了しました。（Host：{courseHostName}, CourseId：{courseUuid})");
            if (thisWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            IDriverConfig driverConfig = Browser switch
            {
                Browser.Firefox => new FirefoxConfig(),
                Browser.Chromium => new ChromeConfig(),
                Browser.Edge => new EdgeConfig(),
                _ => throw new NotImplementedException(),
            };
            new DriverManager().SetUpDriver(driverConfig);

            dynamic driverService = Browser switch
            {
                Browser.Firefox => FirefoxDriverService.CreateDefaultService(),
                Browser.Chromium => ChromeDriverService.CreateDefaultService(),
                Browser.Edge => EdgeDriverService.CreateDefaultService(),
                _ => throw new NotImplementedException(),
            };
            driverService.HideCommandPromptWindow = true;
            using WebDriver driver = Browser switch
            {
                Browser.Firefox => new FirefoxDriver(driverService, new FirefoxOptions(), new TimeSpan(0, 5, 0)),
                Browser.Chromium => new ChromeDriver(driverService, new ChromeOptions(), new TimeSpan(0, 5, 0)),
                Browser.Edge => new EdgeDriver(driverService, new EdgeOptions(), new TimeSpan(0, 5, 0)),
                _ => throw new NotImplementedException(),
            };
            if (thisWorker.CancellationPending)
            {
                e.Cancel = true;
                driver.Quit();
                return;
            }
            thisWorker.ReportProgress(3, $"BrowserのDriverが作成されました。(Driver:{driver.GetType().Name})");

            thisWorker.ReportProgress(4, $"ログイン画面を開きます。");
            driver.Navigate().GoToUrl(courseHostName);
            while (true)
            {
                if (new Uri(driver.Url).GetLeftPart(UriPartial.Path) == "https://echo360.net.au/courses")
                {
                    break;
                }
                if (thisWorker.CancellationPending)
                {
                    e.Cancel = true;
                    driver.Quit();
                    return;
                }
            }
            driver.Manage().Window.Minimize();
            thisWorker.ReportProgress(5, $"ログイン完了を検知しました。");
            if (thisWorker.CancellationPending)
            {
                e.Cancel = true;
                driver.Quit();
                return;
            }
            var cookies = driver.Manage().Cookies.AllCookies.ToList().Select(cookie => new System.Net.Cookie(cookie.Name, cookie.Value)).ToArray();
            var course = new EchoCloudCourse(driver, cookies, courseUuid, courseHostName);

            var videos = course.Videos;
            thisWorker.ReportProgress(6, $"動画セットを{videos.Length}つ検知しました。");
            if (thisWorker.CancellationPending)
            {
                e.Cancel = true;
                driver.Quit();
                return;
            }
            var outputDirectory = Path.Combine(OutputDirectory, GetValidFileName((course.Name + "_" + DateTime.Now.Ticks.ToString()).Trim()));
            outputPath = outputDirectory;
            thisWorker.ReportProgress(7, $"ダウンロード先を{outputDirectory}に設定しました。");
            if (thisWorker.CancellationPending)
            {
                e.Cancel = true;
                driver.Quit();
                return;
            }
            var videoToBeDownload = new List<(string FileName, EchoCloudVideo Video)>();
            for (int i = 0; i < videos.Count(); i++)
            {
                var subVideos = videos[i].SubVideos;
                if (subVideos != null)
                {
                    for (int j = 0; j < subVideos.Count(); j++)
                    {
                        if (thisWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            driver.Quit();
                            return;
                        }
                        string title = $"Lecture_{((subVideos.Length > 1) ? i + 1 : i + 1 + 0.1 * (j + 1))}_[{subVideos[j].Title}].mp4";
                        videoToBeDownload.Add((title, subVideos[j]));
                    }
                }
            }

            thisWorker.ReportProgress(10, $"動画を{videos.Length}つ検知しました。ダウンロードを開始します。");

            var downloaded = new List<string>();
            for (int i = 0; i < videoToBeDownload.Count; i++)
            {
                if (thisWorker.CancellationPending)
                {
                    e.Cancel = true;
                    driver.Quit();
                    return;
                }
                var (FileName, Video) = videoToBeDownload[i];
                if (Video.Url == null)
                {
                    continue;
                }
                thisWorker.ReportProgress(10 + i * (95 - 10) / videoToBeDownload.Count, $"{FileName}をダウンロードします。");
                if (Video.Download(outputDirectory, FileName))
                {
                    thisWorker.ReportProgress(10 + (i + 1) * (95 - 10) / videoToBeDownload.Count - 1, $"{FileName}をダウンロードしました。");
                    downloaded.Add(FileName);
                }
                else
                {
                    thisWorker.ReportProgress(10 + (i + 1) * (95 - 10) / videoToBeDownload.Count - 1, $"{FileName}のダウンロードに失敗しました。");
                }
            }
            thisWorker.ReportProgress(95, $"{downloaded.Count}件の動画のダウンロードを完了しました。");

            driver.Quit();


            thisWorker.ReportProgress(100, $"処理を終了しました。");
        }

        private void DownloadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LogArea.Text += $"【{DateTime.Now:T}】" + e.UserState + Environment.NewLine;
            DownloadProgressBar.Value = e.ProgressPercentage;
        }

        private void DownloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("ダウンロードがキャンセルされました", "キャンセル", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult=DialogResult.Abort;
            }
            else if (e.Error != null)
            {
                if (e.Error is WebDriverException)
                {
                    MessageBox.Show("ダウンロードが失敗しました。" + Environment.NewLine + $"選択されたブラウザ({Browser})が存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show("ダウンロードが失敗しました。" + Environment.NewLine + e.Error.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                DialogResult = DialogResult.No;
            }
            else
            {
                MessageBox.Show("ダウンロードが完了しました！", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                if(IsOpenExplorer)
                {
                    Process.Start(outputPath);
                }
            }
            Close();
        }

        public static string GetFileName(string courseName, string date, string title)
        {
            if (courseName != null)
            {
                return GetValidFileName($"{courseName} - {date} - {title}");
            }
            else
            {
                return GetValidFileName($"{date} - {title}");
            }
        }

        public static string GetValidFileName(string original)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                original = original.Replace(c, '_');
            }
            return original;
        }

        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            LogArea.Text = string.Empty;
            DownloadProgressBar.Value = 0;
            DownloadWorker.RunWorkerAsync();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("キャンセルしますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DownloadWorker.CancelAsync();
            }
        }
    }
}
