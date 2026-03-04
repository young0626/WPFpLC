using System.Windows;
using WPF实现PLC触摸屏程序.ViewModels;

namespace WPF实现PLC触摸屏程序.Views
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new StartViewModel();
        }
    }
}
