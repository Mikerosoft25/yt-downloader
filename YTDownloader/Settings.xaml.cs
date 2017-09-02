using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace YTDownloader
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : System.Windows.Controls.UserControl
    {
        public event EventHandler ReturnSettings;

        public Settings()
        {
            InitializeComponent();
        }

        private void OnReturnClick(object sender, RoutedEventArgs e)
        {
            ReturnSettings?.Invoke(this, new EventArgs());
        }

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog downloadDirectory = new FolderBrowserDialog();
            downloadDirectory.Description = "Set the directory where your files should be downloaded.";
            if (downloadDirectory.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                downloadDirectoryTextbox.Text = downloadDirectory.SelectedPath;
                SettingsManager.Instance.OutputDirectory = downloadDirectory.SelectedPath;
            }
        }

    }
}
