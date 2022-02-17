namespace EchoDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.UrlInputBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OutputPathBox = new System.Windows.Forms.TextBox();
            this.UrlBrowseButton = new System.Windows.Forms.Button();
            this.FolderBrowseButton = new System.Windows.Forms.Button();
            this.OutputPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.BrowserSelectBox = new System.Windows.Forms.ComboBox();
            this.OpenExplorer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // UrlInputBox
            // 
            this.UrlInputBox.Location = new System.Drawing.Point(129, 15);
            this.UrlInputBox.Name = "UrlInputBox";
            this.UrlInputBox.Size = new System.Drawing.Size(484, 19);
            this.UrlInputBox.TabIndex = 1;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(575, 174);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(120, 25);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "開始";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "コースURL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "ダウンロード先";
            // 
            // OutputPathBox
            // 
            this.OutputPathBox.Location = new System.Drawing.Point(129, 51);
            this.OutputPathBox.Name = "OutputPathBox";
            this.OutputPathBox.Size = new System.Drawing.Size(484, 19);
            this.OutputPathBox.TabIndex = 5;
            // 
            // UrlBrowseButton
            // 
            this.UrlBrowseButton.Enabled = false;
            this.UrlBrowseButton.Location = new System.Drawing.Point(620, 13);
            this.UrlBrowseButton.Name = "UrlBrowseButton";
            this.UrlBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.UrlBrowseButton.TabIndex = 6;
            this.UrlBrowseButton.Text = "参照";
            this.UrlBrowseButton.UseVisualStyleBackColor = true;
            this.UrlBrowseButton.Click += new System.EventHandler(this.UrlBrowseButton_Click);
            // 
            // FolderBrowseButton
            // 
            this.FolderBrowseButton.Location = new System.Drawing.Point(620, 49);
            this.FolderBrowseButton.Name = "FolderBrowseButton";
            this.FolderBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.FolderBrowseButton.TabIndex = 7;
            this.FolderBrowseButton.Text = "参照";
            this.FolderBrowseButton.UseVisualStyleBackColor = true;
            this.FolderBrowseButton.Click += new System.EventHandler(this.FolderBrowseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "使用ブラウザ";
            // 
            // BrowserSelectBox
            // 
            this.BrowserSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BrowserSelectBox.FormattingEnabled = true;
            this.BrowserSelectBox.Location = new System.Drawing.Point(129, 90);
            this.BrowserSelectBox.Name = "BrowserSelectBox";
            this.BrowserSelectBox.Size = new System.Drawing.Size(566, 20);
            this.BrowserSelectBox.TabIndex = 10;
            // 
            // OpenExplorer
            // 
            this.OpenExplorer.AutoSize = true;
            this.OpenExplorer.Checked = true;
            this.OpenExplorer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OpenExplorer.Location = new System.Drawing.Point(483, 152);
            this.OpenExplorer.Name = "OpenExplorer";
            this.OpenExplorer.Size = new System.Drawing.Size(212, 16);
            this.OpenExplorer.TabIndex = 11;
            this.OpenExplorer.Text = "完了時にエクスプローラーでファイルを開く";
            this.OpenExplorer.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 211);
            this.Controls.Add(this.OpenExplorer);
            this.Controls.Add(this.BrowserSelectBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FolderBrowseButton);
            this.Controls.Add(this.UrlBrowseButton);
            this.Controls.Add(this.OutputPathBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UrlInputBox);
            this.Controls.Add(this.StartButton);
            this.Name = "MainForm";
            this.Text = "EchoDownloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UrlInputBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OutputPathBox;
        private System.Windows.Forms.Button UrlBrowseButton;
        private System.Windows.Forms.Button FolderBrowseButton;
        private System.Windows.Forms.FolderBrowserDialog OutputPathBrowserDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox BrowserSelectBox;
        private System.Windows.Forms.CheckBox OpenExplorer;
    }
}

