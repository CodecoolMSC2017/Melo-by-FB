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
using Melo.Service.Interface;
using Melo.Service.Simple;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Melo.ViewModel;



namespace Melo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        public static List<SimpleFileInputMonitor> fileMonitors;
        public MainWindow()
        {
            InitializeComponent();
            fileMonitors = new List<SimpleFileInputMonitor>();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(SimpleFileInputMonitor.Raised > 0)
            {
                var viewModel = (MainWindowViewModel)DataContext;
                viewModel.PublishMessage();
                SimpleFileInputMonitor.Raised = 0;
            }
        }
    }
}
