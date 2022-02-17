namespace EchoDownloader
{
    partial class FfmpegCheckerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CancellationButton = new System.Windows.Forms.Button();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DownloadProgressBar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CancellationButton);
            this.splitContainer1.Size = new System.Drawing.Size(800, 32);
            this.splitContainer1.SplitterDistance = 675;
            this.splitContainer1.TabIndex = 0;
            // 
            // CancellationButton
            // 
            this.CancellationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancellationButton.Location = new System.Drawing.Point(0, 0);
            this.CancellationButton.Name = "CancellationButton";
            this.CancellationButton.Size = new System.Drawing.Size(121, 32);
            this.CancellationButton.TabIndex = 0;
            this.CancellationButton.Text = "キャンセル";
            this.CancellationButton.UseVisualStyleBackColor = true;
            this.CancellationButton.Click += new System.EventHandler(this.CancellationButton_Click);
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadProgressBar.Location = new System.Drawing.Point(0, 0);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(675, 32);
            this.DownloadProgressBar.TabIndex = 0;
            // 
            // FfmpegCheckerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 32);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FfmpegCheckerDialog";
            this.Text = "ffmpegダウンロード - EchoDownloader";
            this.Load += new System.EventHandler(this.FfmpegCheckerDialog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Button CancellationButton;
    }
}