using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF实现PLC触摸屏程序.Provider;

namespace WPF实现PLC触摸屏程序.ViewModels.UserControls
{
    public class ServerStateViewModel : ObservableObject
    {
        #region 私有字段和属性
        /// <summary>
        /// Plc实例
        /// </summary>
        private readonly Plc plc = null;
        /// <summary>
        /// 取消任务标识
        /// </summary>
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        #endregion

        #region 属性之数据

        #region 故障指示
        private string troubleXColor = "Red";
        /// <summary>
        /// 故障指示 X轴颜色
        /// </summary>
        public string TroubleXColor
        {
            get { return troubleXColor; }
            set { troubleXColor = value; OnPropertyChanged(); }
        }

        private string troubleYColor = "Red";
        /// <summary>
        /// 故障指示 Y轴颜色
        /// </summary>
        public string TroubleYColor
        {
            get { return troubleYColor; }
            set { troubleYColor = value; OnPropertyChanged(); }
        }

        private string troubleZColor = "Red";
        /// <summary>
        /// 故障指示 Z轴颜色
        /// </summary>
        public string TroubleZColor
        {
            get { return troubleZColor; }
            set { troubleZColor = value; OnPropertyChanged(); }
        }

        private string troubleRColor = "Red";
        /// <summary>
        /// 故障指示 R轴颜色
        /// </summary>
        public string TroubleRColor
        {
            get { return troubleRColor; }
            set { troubleRColor = value; OnPropertyChanged(); }
        }
        #endregion

        #region 上电指示
        private string powerOnXColor = "Gray";
        /// <summary>
        /// 上电指示 X轴颜色
        /// </summary>
        public string PowerOnXColor
        {
            get { return powerOnXColor; }
            set { powerOnXColor = value; OnPropertyChanged(); }
        }

        private string powerOnYColor = "Gray";
        /// <summary>
        /// 上电指示 Y轴颜色
        /// </summary>
        public string PowerOnYColor
        {
            get { return powerOnYColor; }
            set { powerOnYColor = value; OnPropertyChanged(); }
        }

        private string powerOnZColor = "Gray";
        /// <summary>
        /// 上电指示 Z轴颜色
        /// </summary>
        public string PowerOnZColor
        {
            get { return powerOnZColor; }
            set { powerOnZColor = value; OnPropertyChanged(); }
        }

        private string powerOnRColor = "Gray";
        /// <summary>
        /// 上电指示 R轴颜色
        /// </summary>
        public string PowerOnRColor
        {
            get { return powerOnRColor; }
            set { powerOnRColor = value; OnPropertyChanged(); }
        }
        #endregion

        #region 还原指示
        private string restoreXColor = "Gray";
        /// <summary>
        /// 还原指示 X轴颜色
        /// </summary>
        public string RestoreXColor
        {
            get { return restoreXColor; }
            set { restoreXColor = value; OnPropertyChanged(); }
        }

        private string restoreYColor = "Gray";
        /// <summary>
        /// 还原指示 Y轴颜色
        /// </summary>
        public string RestoreYColor
        {
            get { return restoreYColor; }
            set { restoreYColor = value; OnPropertyChanged(); }
        }

        private string restoreZColor = "Gray";
        /// <summary>
        /// 还原指示 Z轴颜色
        /// </summary>
        public string RestoreZColor
        {
            get { return restoreZColor; }
            set { restoreZColor = value; OnPropertyChanged(); }
        }

        private string restoreRColor = "Gray";
        /// <summary>
        /// 还原指示 R轴颜色
        /// </summary>
        public string RestoreRColor
        {
            get { return restoreRColor; }
            set { restoreRColor = value; OnPropertyChanged(); }
        }
        #endregion

        #endregion

        #region 属性之事件
        /// <summary>
        /// 故障指示命令
        /// </summary>
        public RelayCommand<string> TroubleCommand
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write(s, false);
                });
            }
        }

        /// <summary>
        /// 上电指示，还原指示命令
        /// </summary>
        public RelayCommand<string> Command
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write(s, true);
                });
            }
        }
        #endregion

        #region 构造函数
        public ServerStateViewModel()
        {
            try
            {
                plc = PlcProvider.Instance.CurrentPlc;
                if (plc.IsConnected)
                {
                    RealTimeReadData(cts.Token).Start();
                }
                else
                {
                    Task task = plc.OpenAsync();
                    task.ContinueWith(t =>
                    {
                        if (plc.IsConnected)
                        {
                            RealTimeReadData(cts.Token).Start();
                        }
                        else
                        {
                            MessageBox.Show("连接目标主机失败，请检查IP等信息！");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接目标主机失败，请检查IP等信息！" + ex.Message);
            }
        }

        private Task RealTimeReadData(CancellationToken ct)
        {
            return new Task(() =>
            {
                while (plc != null && !ct.IsCancellationRequested) {
                    bool troubleX = Convert.ToBoolean(plc.Read("DB8.DBX0.0"));
                    bool troubleY = Convert.ToBoolean(plc.Read("DB8.DBX0.1"));
                    bool troubleZ = Convert.ToBoolean(plc.Read("DB8.DBX0.2"));
                    bool troubleR = Convert.ToBoolean(plc.Read("DB8.DBX0.3"));

                    bool powerOnX = Convert.ToBoolean(plc.Read("DB9.DBX0.0"));
                    bool powerOnY = Convert.ToBoolean(plc.Read("DB9.DBX0.1"));
                    bool powerOnZ = Convert.ToBoolean(plc.Read("DB9.DBX0.2"));
                    bool powerOnR = Convert.ToBoolean(plc.Read("DB9.DBX0.3"));

                    bool restoreX = Convert.ToBoolean(plc.Read("DB8.DBX0.6"));
                    bool restoreY = Convert.ToBoolean(plc.Read("DB8.DBX0.7"));
                    bool restoreZ = Convert.ToBoolean(plc.Read("DB8.DBX1.0"));
                    bool restoreR = Convert.ToBoolean(plc.Read("DB8.DBX1.1"));

                    TroubleXColor = troubleX ? "Red" : "Green";
                    TroubleYColor = troubleY ? "Red" : "Green";
                    TroubleZColor = troubleZ ? "Red" : "Green";
                    TroubleRColor = troubleR ? "Red" : "Green";

                    PowerOnXColor = powerOnX ? "Green" : "Gray";
                    PowerOnYColor = powerOnY ? "Green" : "Gray";
                    PowerOnZColor = powerOnZ ? "Green" : "Gray";
                    PowerOnRColor = powerOnR ? "Green" : "Gray";

                    RestoreXColor = restoreX ? "Green" : "Gray";
                    RestoreYColor = restoreY ? "Green" : "Gray";
                    RestoreZColor = restoreZ ? "Green" : "Gray";
                    RestoreRColor = restoreR ? "Green" : "Gray";
                }
            });
        }
        #endregion

    }
}
