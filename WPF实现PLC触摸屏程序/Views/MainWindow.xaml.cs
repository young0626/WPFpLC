using System.Windows;
using WPF实现PLC触摸屏程序.Provider;
using WPF实现PLC触摸屏程序.ViewModels;

namespace WPF实现PLC触摸屏程序.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
