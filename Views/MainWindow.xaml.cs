﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Empro.Views;

namespace Empro.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenAddEmployeeWindow(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
        }
    }
}


