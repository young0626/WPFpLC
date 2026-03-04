using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using WPF实现PLC触摸屏程序.Views;

namespace WPF实现PLC触摸屏程序.ViewModels
{
    public class StartViewModel : ObservableObject
    {
        #region 私有的字段和属性
        private readonly Dictionary<string, Window> dict = new Dictionary<string, Window>();
        #endregion

        #region 依赖属性之事件
        public RelayCommand<Window> GoMainWindowCommand
        {
            get
            {
                return new RelayCommand<Window>((win) =>
                {
                    Type type = Assembly.GetExecutingAssembly().GetType("WPF实现PLC触摸屏程序.Views.MainWindow"); 
                    if (!dict.ContainsKey("MainWindow"))
                        dict.Add("MainWindow", (Window)Activator.CreateInstance(type));

                    Window mainWindow = dict["MainWindow"];
                    mainWindow.Show();
                    win.Close();
                });
            }
        }

        /// <summary>
        /// 关闭窗体
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
    }
}
