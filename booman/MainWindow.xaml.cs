using System;
using System.Windows;
using booman.ViewModels;

namespace booman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }
    }
}
