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
using WPF实现PLC触摸屏程序.ViewModels.UserControls;

namespace WPF实现PLC触摸屏程序.Views.UserControls
{
    /// <summary>
    /// ServerZRUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ServerZRUserControl : UserControl
    {
        public ServerZRUserControl()
        {
            InitializeComponent();
            DataContext = new ServerZRViewModel();
        }
    }
}
