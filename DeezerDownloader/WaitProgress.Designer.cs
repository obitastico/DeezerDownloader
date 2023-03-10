using System.ComponentModel;

namespace DeezerDownloader
{
    partial class WaitProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitProgress));
            this.WaitProgressLabel = new System.Windows.Forms.Label();
            this.WaitProgressBar = new System.Windows.Forms.ProgressBar();
            this.WaitProgressStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WaitProgressLabel
            // 
            this.WaitProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.WaitProgressLabel.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaitProgressLabel.Location = new System.Drawing.Point(94, 9);
            this.WaitProgressLabel.Name = "WaitProgressLabel";
            this.WaitProgressLabel.Size = new System.Drawing.Size(285, 23);
            this.WaitProgressLabel.TabIndex = 0;
            this.WaitProgressLabel.Text = "Downloading...";
            this.WaitProgressLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WaitProgressBar
            // 
            this.WaitProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.WaitProgressBar.Location = new System.Drawing.Point(28, 54);
            this.WaitProgressBar.Name = "WaitProgressBar";
            this.WaitProgressBar.Size = new System.Drawing.Size(426, 24);
            this.WaitProgressBar.TabIndex = 1;
            // 
            // WaitProgressStatusLabel
            // 
            this.WaitProgressStatusLabel.Location = new System.Drawing.Point(28, 33);
            this.WaitProgressStatusLabel.Name = "WaitProgressStatusLabel";
            this.WaitProgressStatusLabel.Size = new System.Drawing.Size(426, 18);
            this.WaitProgressStatusLabel.TabIndex = 2;
            this.WaitProgressStatusLabel.Text = "Starting Download ...";
            // 
            // WaitProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 92);
            this.Controls.Add(this.WaitProgressStatusLabel);
            this.Controls.Add(this.WaitProgressBar);
            this.Controls.Add(this.WaitProgressLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WaitProgress";
            this.Text = "Please wait...";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label WaitProgressStatusLabel;

        private System.Windows.Forms.ProgressBar WaitProgressBar;

        private System.Windows.Forms.Label WaitProgressLabel;

        #endregion
    }
}