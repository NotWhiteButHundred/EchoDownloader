namespace EchoDownloader
{
    partial class ProgressDialog
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.LogArea = new System.Windows.Forms.RichTextBox();
            this.DownloadWorker = new System.ComponentModel.BackgroundWorker();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.CancellationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LogArea);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.DownloadProgressBar);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.CancellationButton);
            this.splitContainer2.Size = new System.Drawing.Size(800, 25);
            this.splitContainer2.SplitterDistance = 701;
            this.splitContainer2.TabIndex = 0;
            // 
            // LogArea
            // 
            this.LogArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogArea.Location = new System.Drawing.Point(0, 0);
            this.LogArea.Name = "LogArea";
            this.LogArea.Size = new System.Drawing.Size(800, 421);
            this.LogArea.TabIndex = 0;
            this.LogArea.Text = "";
            // 
            // DownloadWorker
            // 
            this.DownloadWorker.WorkerReportsProgress = true;
            this.DownloadWorker.WorkerSupportsCancellation = true;
            this.DownloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadWorker_DoWork);
            this.DownloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DownloadWorker_ProgressChanged);
            this.DownloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadWorker_RunWorkerCompleted);
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadProgressBar.Location = new System.Drawing.Point(0, 0);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(701, 25);
            this.DownloadProgressBar.TabIndex = 0;
            // 
            // CancellationButton
            // 
            this.CancellationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancellationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancellationButton.Location = new System.Drawing.Point(0, 0);
            this.CancellationButton.Name = "CancellationButton";
            this.CancellationButton.Size = new System.Drawing.Size(95, 25);
            this.CancellationButton.TabIndex = 0;
            this.CancellationButton.Text = "キャンセル";
            this.CancellationButton.UseVisualStyleBackColor = true;
            this.CancellationButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancellationButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ProgressDialog";
            this.Text = "進行状況 - EchoDownloader";
            this.Shown += new System.EventHandler(this.ProgressDialog_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox LogArea;
        private System.ComponentModel.BackgroundWorker DownloadWorker;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Button CancellationButton;
    }
}