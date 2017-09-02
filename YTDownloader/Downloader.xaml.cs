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

namespace YTDownloader
{
    /// <summary>
    /// Interaktionslogik für Downloader.xaml
    /// </summary>
    public partial class Downloader : UserControl
    {
        public event EventHandler OpenSettings;

        public Downloader()
        {
            SettingsManager.Instance.OutputDirectory = "test";
            InitializeComponent();
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            OpenSettings?.Invoke(this, new EventArgs());
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            this.videoList.Children.Add(new VideoView());
        }
    }
}
