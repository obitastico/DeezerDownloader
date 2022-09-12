using System;
using System.Windows.Forms;

namespace DeezerDownloader
{
    public partial class WaitProgress : Form
    {
        public WaitProgress()
        {
            InitializeComponent();
        }
        
        private void WaitProgress_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                WaitProgressBar.PerformStep();
            }
        }
    }
}