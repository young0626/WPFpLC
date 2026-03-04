using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reflection;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using WPF实现PLC触摸屏程序.Provider;
using S7.Net;

namespace WPF实现PLC触摸屏程序.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        #region 依赖属性之数据
        private Control currentUserControl;
        /// <summary>
        /// 当前展示的用户控件
        /// </summary>
        public Control CurrentUserControl
        {
            get { return currentUserControl; }
            set { currentUserControl = value; OnPropertyChanged(); }
        }

        private string backgroundColor = "#8080c0";
        /// <summary>
        /// 底部导航的背景颜色
        /// </summary>
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; OnPropertyChanged(); }
        }
        #endregion

        #region 依赖属性之事件
        /// <summary>
        /// 打开并切换用户控件
        /// </summary>
        public RelayCommand<string> OpenCommand
        {
            get
            {
                return new RelayCommand<string>(OpenUserControl);
            }
        }

        /// <summary>
        /// 打开待机页窗体
        /// </summary>
        public RelayCommand<Window> OpenWindowCommand
        {
            get
            {
                return new RelayCommand<Window>((win) =>
                {
                    Type type = Assembly.GetExecutingAssembly().GetType($"WPF实现PLC触摸屏程序.Views.StartWindow");
                    Window startWindow = (Window)Activator.CreateInstance(type);
                    startWindow.Show();
                    win.Close();
                });
            }
        }
        /// <summary>
        /// 使用Esc快捷键关闭当前窗体
        /// </summary>
        public RelayCommand<Window> CloseWindowCommand
        {
            get
            {
                return new RelayCommand<Window>((win) =>
                {
                    win.Close();
                });
            }
        }
        #endregion

        #region 构造函数
        public MainViewModel()
        {
            OpenUserControl("IndexUserControl");
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 打开某个用户控件，并设置底部导航背景
        /// </summary>
        /// <param name="controlName">用户控件名称</param>
        private void OpenUserControl(string controlName)
        {
            Type type = Assembly.GetExecutingAssembly().GetType($"WPF实现PLC触摸屏程序.Views.UserControls.{controlName}");
            CurrentUserControl = (UserControl)Activator.CreateInstance(type);

            if (controlName == "ServerXYUserControl")
                BackgroundColor = "#009b9b";
            else
                BackgroundColor = "#8080c0";
        }
        #endregion
    }
}
