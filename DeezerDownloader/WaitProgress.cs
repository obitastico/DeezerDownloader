using System;
using System.Windows.Forms;

namespace DeezerDownloader
{
    public partial class WaitProgress : Form
    {
        public WaitProgress()
        {
            InitializeComponent();
            WaitProgressBar.Minimum = 0;
            WaitProgressBar.Maximum = 100;
        }

        public void PerfomStep(double p, string text)
        {
            WaitProgressBar.Step = (int)Math.Round(p * 100) - WaitProgressBar.Value;
            WaitProgressBar.PerformStep();
            WaitProgressStatusLabel.Text = text;
        }
    }
}