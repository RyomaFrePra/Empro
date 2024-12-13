using System.Windows;
using Empro.Views;

namespace Empro.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
        }

        private void OpenAddEmployeeWindow(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeeRegisterPage());
        }
    }
}
