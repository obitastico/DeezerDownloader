namespace DeezerDownloaderForm
{
    partial class DeezerDownloaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeezerDownloaderForm));
            this.DDFTitleLabel = new System.Windows.Forms.Label();
            this.DDFDownloadButton = new System.Windows.Forms.Button();
            this.DDFLinkTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DDFTitleLabel
            // 
            this.DDFTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.DDFTitleLabel.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DDFTitleLabel.Location = new System.Drawing.Point(89, 9);
            this.DDFTitleLabel.Name = "DDFTitleLabel";
            this.DDFTitleLabel.Size = new System.Drawing.Size(233, 31);
            this.DDFTitleLabel.TabIndex = 1;
            this.DDFTitleLabel.Text = "Bitte Link eintragen:";
            this.DDFTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DDFDownloadButton
            // 
            this.DDFDownloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.DDFDownloadButton.Location = new System.Drawing.Point(158, 75);
            this.DDFDownloadButton.Name = "DDFDownloadButton";
            this.DDFDownloadButton.Size = new System.Drawing.Size(83, 27);
            this.DDFDownloadButton.TabIndex = 2;
            this.DDFDownloadButton.Text = "Download";
            this.DDFDownloadButton.UseVisualStyleBackColor = true;
            // 
            // DDFLinkTextBox
            // 
            this.DDFLinkTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.DDFLinkTextBox.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DDFLinkTextBox.Location = new System.Drawing.Point(46, 43);
            this.DDFLinkTextBox.Name = "DDFLinkTextBox";
            this.DDFLinkTextBox.Size = new System.Drawing.Size(310, 23);
            this.DDFLinkTextBox.TabIndex = 3;
            // 
            // DeezerDownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(398, 119);
            this.Controls.Add(this.DDFLinkTextBox);
            this.Controls.Add(this.DDFDownloadButton);
            this.Controls.Add(this.DDFTitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeezerDownloaderForm";
            this.Text = "Deezer Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox DDFLinkTextBox;

        private System.Windows.Forms.TextBox textBox1;

        private System.Windows.Forms.Button DDFDownloadButton;

        private System.Windows.Forms.Label DDFTitleLabel;

        private System.Windows.Forms.RichTextBox richTextBox1;

        #endregion
    }
}