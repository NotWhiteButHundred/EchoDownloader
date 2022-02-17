using System;
using System.IO;
using System.Windows.Forms;

namespace EchoDownloader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Environment.CurrentDirectory = Directory.GetParent(Application.ExecutablePath).FullName;
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + Path.Combine(Environment.CurrentDirectory, "ffmpeg", "bin"));
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (UrlInputBox.Text == string.Empty)
            {
                MessageBox.Show("URLが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (OutputPathBox.Text == string.Empty)
            {
                MessageBox.Show("ダウンロード先が選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (BrowserSelectBox.SelectedIndex == -1)
            {
                MessageBox.Show("ブラウザーが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var downloaded = new ProgressDialog(UrlInputBox.Text, OutputPathBox.Text, (Browser)BrowserSelectBox.SelectedItem, OpenExplorer.Checked);
            downloaded.ShowDialog();
        }

        private void FolderBrowseButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == OutputPathBrowserDialog.ShowDialog())
            {
                OutputPathBox.Text = OutputPathBrowserDialog.SelectedPath;
            }
        }

        private void UrlBrowseButton_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FfmpegCheckerDialog dialog = new();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                Close();
            }

            foreach (Browser browser in Enum.GetValues(typeof(Browser)))
            {
                BrowserSelectBox.Items.Add(browser);
            }

            UrlInputBox.Text = Properties.Settings.Default.CourseUrl;
            OutputPathBox.Text = Properties.Settings.Default.OutputDirectory;
            if (BrowserSelectBox.Items.Contains((Browser)Properties.Settings.Default.Browser))
            {
                BrowserSelectBox.SelectedItem = (Browser)Properties.Settings.Default.Browser;
            }
            OpenExplorer.Checked = Properties.Settings.Default.IsOpenExplorer;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.CourseUrl = UrlInputBox.Text;
            Properties.Settings.Default.OutputDirectory = OutputPathBox.Text;
            Properties.Settings.Default.Browser = (int)BrowserSelectBox.SelectedItem;
            Properties.Settings.Default.IsOpenExplorer = OpenExplorer.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
