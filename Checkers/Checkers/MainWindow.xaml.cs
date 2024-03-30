using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Checkers;
using Checkers.View;


namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Height = 720;
            this.Width = 1100;
            MainFrame.Navigate(new Game());
            MainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}