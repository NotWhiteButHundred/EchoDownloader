using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WebClientHelper;

namespace EchoDownloader
{
    public partial class FfmpegCheckerDialog : Form
    {
        WebClientEx downloadClient = null;
        private const string FFMPEG_ZIP_PATH = "ffmpeg.zip";
        private const string FFMPEG_URL = "https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip";

        public FfmpegCheckerDialog()
        {
            InitializeComponent();
        }

        private void FfmpegCheckerDialog_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo info = new()
                {
                    FileName = "ffmpeg",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                Process p = Process.Start(info);
                p.WaitForExit();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                if (MessageBox.Show("ffmpegが実行できません。ダウンロードしますか？" + Environment.NewLine + "（ダウンロードしない場合は終了します）", "EchoDownloader", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (Directory.Exists("ffmpeg"))
                    {
                        Directory.Delete("ffmpeg", true);
                    }

                    if (downloadClient == null)
                    {
                        downloadClient = new WebClientEx();
                        downloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadClient_DownloadProgressChanged);
                        downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadClient_DownloadFileCompleted);
                    }
                    downloadClient.DownloadFileAsync(new(FFMPEG_URL), FFMPEG_ZIP_PATH);
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
            }
        }

        private void DownloadClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressBar.Value = e.ProgressPercentage;
        }

        private void DownloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                if(File.Exists(FFMPEG_ZIP_PATH))
                {
                    File.Delete(FFMPEG_ZIP_PATH);
                }

                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            if(e.Error != null)
            {
                MessageBox.Show("エラーが発生しました。終了します。", "EchoDownloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            string folder = "ffmpeg-temp";
            ZipFile.ExtractToDirectory(FFMPEG_ZIP_PATH, folder);
            if (Directory.GetDirectories(folder).Count() != 1)
            {
                throw new Exception("ffmpegのファイルの取得に失敗しました。");
            }
            Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly).ToList().ForEach(d => Directory.Move(d, "ffmpeg"));
            File.Delete(FFMPEG_ZIP_PATH);
            Directory.Delete(folder);

            DialogResult = DialogResult.OK;
            MessageBox.Show("ffmpegのダウンロードが完了しました。", "EchoDownloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void CancellationButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("キャンセルしますか？" + Environment.NewLine + "キャンセルすると起動できません", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                downloadClient.CancelAsync();
            }
        }
    }
}
