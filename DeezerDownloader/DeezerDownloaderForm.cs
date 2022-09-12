using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DeezerDownloader
{
    public partial class DeezerDownloaderForm : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private void DDFDownloadButton_Click(object sender, EventArgs e)
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

            switch (DDFLinkTextBox.Text)
            {
                case string a when a.Contains("profile"):
                    break;
                case string b when b.Contains("playlist"):
                    break;
                case string c when c.Contains("album"):
                    break;
                default:
                    MessageBox.Show(@"Der angegebene Link kann leider nicht gedownloadet werden.", 
                        @"Download nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
            }
            
            WaitProgress wp = new WaitProgress();
            wp.Show();
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