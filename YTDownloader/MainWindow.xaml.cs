﻿using System;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "YT Downloader";
        }

        private void OnOpenSettings(object sender, EventArgs e)
        {
            settings.Visibility = Visibility.Visible;
            downloader.Visibility = Visibility.Hidden;
            Title = "YT Downloader - Settings";
        }

        private void OnReturnSettings(object sender, EventArgs e)
        {
            settings.Visibility = Visibility.Hidden;
            downloader.Visibility = Visibility.Visible;
            Title = "YT Downloader";
        }
    }
}
