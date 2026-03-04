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
using WPF实现PLC触摸屏程序.Models;
using WPF实现PLC触摸屏程序.Provider;

namespace WPF实现PLC触摸屏程序.ViewModels.UserControls
{
    public class ServerZRViewModel : ObservableObject
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
        /// <summary>
        /// 判断输入框控件是否获取焦点
        /// </summary>
        private bool IsGotFocus { get; set; } = false;
        #endregion

        #region 属性之数据
        private Zaxis zaxis;
        /// <summary>
        /// Z轴数据实体
        /// </summary>
        public Zaxis ZaxisModel
        {
            get { return zaxis; }
            set { zaxis = value; OnPropertyChanged(); }
        }

        private Raxis raxis;
        /// <summary>
        /// R轴数据实体
        /// </summary>
        public Raxis RaxisModel
        {
            get { return raxis; }
            set { raxis = value; OnPropertyChanged(); }
        }
        #endregion

        #region 属性之事件
        /// <summary>
        /// 当前用户控件卸载时
        /// </summary>
        public RelayCommand UnloadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    cts.Cancel();
                });
            }
        }

        /// <summary>
        /// Z，R轴MouseDown
        /// </summary>
        public RelayCommand<string> MouseDownCommand
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write(s, true);
                });
            }
        }

        /// <summary>
        /// Z，R轴MouseUp
        /// </summary>
        public RelayCommand<string> MouseUpCommand
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
        /// Z，R轴 GotFocus
        /// </summary>
        public RelayCommand GotFocus
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsGotFocus = true;
                });
            }
        }

        /// <summary>
        /// Z，R轴 LostFocus
        /// </summary>
        public RelayCommand<string[]> LostFocus
        {
            get
            {
                return new RelayCommand<string[]>((obj) =>
                {
                    plc.Write(obj[0], Convert.ToSingle(obj[1]));
                    IsGotFocus = false;
                });
            }
        }

        #endregion

        #region 构造函数
        public ServerZRViewModel()
        {
            try
            {
                plc = PlcProvider.Instance.CurrentPlc;
                if (plc.IsConnected)
                {
                    InitialData();
                    RealTimeReadData(cts.Token).Start();
                }
                else
                {
                    Task task = plc.OpenAsync();
                    task.ContinueWith(t =>
                    {
                        if (plc.IsConnected)
                        {
                            InitialData();
                            RealTimeReadData(cts.Token).Start();
                        }
                        else
                        {
                            MessageBox.Show("连接目标主机失败，请检查IP等信息！");
                        }
                    });
                }

                ZaxisModel = new Zaxis()
                {
                    CurrentLocation = "12.1234",
                    LocalSpeed = "13.1234",
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接目标主机失败，请检查IP等信息！" + ex.Message);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitialData()
        {
            ZaxisModel = new Zaxis()
            {
                CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD76")).ConvertToFloat().ToString("0.000"),
                CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD80")).ConvertToFloat().ToString("0.00000000"),
                JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD46")).ConvertToFloat().ToString("0"),
                Location = Convert.ToUInt32(plc.Read("DB8.DBD84")).ConvertToFloat().ToString("0.00"),
                LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD50")).ConvertToFloat().ToString("0"),
                PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD126")).ConvertToFloat().ToString("0.00"),
                PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD130")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD134")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD138")).ConvertToFloat().ToString("0.00"),
                ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD142")).ConvertToFloat().ToString("0.00"),
                ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD146")).ConvertToFloat().ToString("0.00"),
            };
            RaxisModel = new Raxis()
            {
                CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD88")).ConvertToFloat().ToString("0.000"),
                CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD92")).ConvertToFloat().ToString("0.00000000"),
                JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD54")).ConvertToFloat().ToString("0"),
                Location = Convert.ToUInt32(plc.Read("DB8.DBD96")).ConvertToFloat().ToString("0.00"),
                LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD58")).ConvertToFloat().ToString("0"),
                PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD154")).ConvertToFloat().ToString("0.00"),
                PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD158")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD162")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD166")).ConvertToFloat().ToString("0.00"),
                ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD170")).ConvertToFloat().ToString("0.00"),
                ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD174")).ConvertToFloat().ToString("0.00"),
            };
        }

        /// <summary>
        /// 线程中实时读取数据
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        private Task RealTimeReadData(CancellationToken ct)
        {
            return new Task(() =>
            {
                while (plc != null && !ct.IsCancellationRequested)
                {
                    ZaxisModel.CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD76")).ConvertToFloat().ToString("0.000");
                    ZaxisModel.CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD80")).ConvertToFloat().ToString("0.00000000");
                    RaxisModel.CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD88")).ConvertToFloat().ToString("0.000");
                    RaxisModel.CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD92")).ConvertToFloat().ToString("0.00000000");

                    if (!IsGotFocus)
                    {

                        ZaxisModel.JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD46")).ConvertToFloat().ToString("0");
                        ZaxisModel.Location = Convert.ToUInt32(plc.Read("DB8.DBD84")).ConvertToFloat().ToString("0.00");
                        ZaxisModel.LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD50")).ConvertToFloat().ToString("0");
                        ZaxisModel.PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD126")).ConvertToFloat().ToString("0.00");
                        ZaxisModel.PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD130")).ConvertToFloat().ToString("0.00");
                        ZaxisModel.MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD134")).ConvertToFloat().ToString("0.00");
                        ZaxisModel.MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD138")).ConvertToFloat().ToString("0.00");
                        ZaxisModel.ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD142")).ConvertToFloat().ToString("0.00");
                        ZaxisModel.ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD146")).ConvertToFloat().ToString("0.00");

                        RaxisModel.JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD54")).ConvertToFloat().ToString("0");
                        RaxisModel.Location = Convert.ToUInt32(plc.Read("DB8.DBD96")).ConvertToFloat().ToString("0.00");
                        RaxisModel.LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD58")).ConvertToFloat().ToString("0");
                        RaxisModel.PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD154")).ConvertToFloat().ToString("0.00");
                        RaxisModel.PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD158")).ConvertToFloat().ToString("0.00");
                        RaxisModel.MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD162")).ConvertToFloat().ToString("0.00");
                        RaxisModel.MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD166")).ConvertToFloat().ToString("0.00");
                        RaxisModel.ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD170")).ConvertToFloat().ToString("0.00");
                        RaxisModel.ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD174")).ConvertToFloat().ToString("0.00");
                    }
                }
            });
        }
        #endregion
    }
}
