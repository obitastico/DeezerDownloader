using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DeezerDownloader.Core;
using DeezerDownloader.Core.Models;

namespace DeezerDownloader
{
    public partial class DeezerDownloaderForm : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Downloader Deezer { get; }
        public WaitProgress ProgressView { get; set; }
        public CancellationTokenSource CancelTokenSource { get; set; }
        private bool DownloadSuccessfull { get; set; }

        public string SavePath
        {
            get
            {
                if (String.IsNullOrEmpty(Properties.Settings.Default.SavePath))
                    SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                return Properties.Settings.Default.SavePath;
            }
            set
            {
                Properties.Settings.Default.SavePath = value;
                if (SavePath == value)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SavePath"));
            }
        }

        public DeezerDownloaderForm()
        {
            InitializeComponent();
            DDFPathTextBox.DataBindings.Add("Text", this, "SavePath", true, DataSourceUpdateMode.OnPropertyChanged);
            Deezer = new Downloader();
            Deezer.ProgressChangedEvent += DeezerOnProgressChangedEvent;
        }

        private void DeezerOnProgressChangedEvent(double p, Track track)
        {
            ProgressView.PerfomStep(p, $"Downloading... {track.Artist.Name} - {track.Title} {(int)Math.Round(p * 100)}%");
        }

        private void DDFChoosePathButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = SavePath;
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK || !String.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    SavePath = folderBrowserDialog.SelectedPath;
            }
        }

        private async void DDFDownloadButton_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(SavePath);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(@"Der Pfad konnte nicht gefunden werden.", 
                    @"Kein valider Pfad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (String.IsNullOrWhiteSpace(DDFLinkTextBox.Text) && 
                !Uri.IsWellFormedUriString(DDFLinkTextBox.Text, UriKind.Absolute) &&
                !new Uri(DDFLinkTextBox.Text).Host.Contains("deezer"))
            {
                MessageBox.Show(@"Bitte gib einen validen Deezer Link ein, der gedownloadet werden soll.", 
                    @"Kein valider Link", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string linkType = DDFLinkTextBox.Text.ContainsAny("loved", "profile", "playlist", "album", "track");
            if (linkType == null)
            {
                MessageBox.Show(@"Der angegebene Link kann leider nicht gedownloadet werden.", 
                    @"Download nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            SetupWaitProgress();
            CancelTokenSource = new CancellationTokenSource();
            ProgressView.Show();
            SetControlsEnabled(false);
            long id = Helper.GetIdByParameterName(DDFLinkTextBox.Text, linkType);
            DownloadSuccessfull = false;
            await Deezer.DownloadDeezerUrlOfType(SavePath, id, linkType, CancelTokenSource.Token);
            DownloadSuccessfull = true;
            SetControlsEnabled(true);
            ProgressView.Close();

            MessageBox.Show("Download abgeschlossen", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetupWaitProgress()
        {
            ProgressView = new WaitProgress();
            
            ProgressView.FormClosing += (s, e) =>
            {
                if (!DownloadSuccessfull)
                {
                    CancelTokenSource.Cancel();
                    SetControlsEnabled(true);
                }
            };
        }

        private void SetControlsEnabled(bool enabled)
        {
            DDFDownloadButton.Enabled = enabled;
            DDFChoosePathButton.Enabled = enabled;
            DDFLinkTextBox.Enabled = enabled;
            DDFPathTextBox.Enabled = enabled;
        }

        private void DDFLinkTextBox_GotFocus(object sender, EventArgs e)
        {
            if (DDFLinkTextBox.Text == "Link")
            {
                DDFLinkTextBox.ForeColor = Color.Black;
                DDFLinkTextBox.Text = "";
            }
        }

        private void DDFLinkTextBox_LostFocus(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(DDFLinkTextBox.Text))
            {
                DDFLinkTextBox.ForeColor = Color.DimGray;
                DDFLinkTextBox.Text = "Link";
            }
        }
    }
}