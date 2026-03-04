using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF实现PLC触摸屏程序.Models;
using WPF实现PLC触摸屏程序.Provider;

namespace WPF实现PLC触摸屏程序.ViewModels.UserControls
{
    public class ServerXYViewModel : ObservableObject
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
        private Xaxis xaxis;
        /// <summary>
        /// X轴数据实体
        /// </summary>
        public Xaxis XaxisModel
        {
            get { return xaxis; }
            set { xaxis = value; OnPropertyChanged(); }
        }

        private Yaxis yaxis;
        /// <summary>
        /// Y轴数据实体
        /// </summary>
        public Yaxis YaxisModel
        {
            get { return yaxis; }
            set { yaxis = value; OnPropertyChanged(); }
        }
        #endregion

        #region 属性之事件

        #region 窗体
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
        #endregion

        #region X轴
        /// <summary>
        /// X轴向前MouseDown
        /// </summary>
        public RelayCommand ForwardMouseDownCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.4", true);
                });
            }
        }

        /// <summary>
        /// X轴向前MouseUp
        /// </summary>
        public RelayCommand ForwardMouseUpCommand
        {
            get
            {

                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.4", false);
                });
            }
        }

        /// <summary>
        /// X轴向后MouseDown
        /// </summary>
        public RelayCommand BackwardMouseDownCommand
        {
            get
            {

                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.5", true);
                });
            }
        }

        /// <summary>
        /// X轴向后MouseUp
        /// </summary>
        public RelayCommand BackwardMouseUpCommand
        {
            get
            {

                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.5", false);
                });
            }
        }

        /// <summary>
        /// X轴定位启动MouseDown
        /// </summary>
        public RelayCommand XMouseDownCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX2.6", true);
                });
            }
        }

        /// <summary>
        /// X轴定位启动MouseUp
        /// </summary>
        public RelayCommand XMouseUpCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX2.6", false);
                });
            }
        }

        /// <summary>
        /// X轴点动速度 GotFocus
        /// </summary>
        public RelayCommand GotFocusXJogSpeed
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
        /// X轴点动速度 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXJogSpeed
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD30", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X轴定位速度 GotFocus
        /// </summary>
        public RelayCommand GotFocusXLocalSpeed
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
        /// X轴定位速度 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXLocalSpeed
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD34", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X轴定位位置 GotFocus
        /// </summary>
        public RelayCommand GotFocusXLocation
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
        /// X轴定位位置 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXLocation
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB8.DBD60", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X取料位 GotFocus
        /// </summary>
        public RelayCommand GotFocusXPickupLocation
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
        /// X取料位 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXPickupLocation
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD70", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X拍照位 GotFocus
        /// </summary>
        public RelayCommand GotFocusXPhotographyPosition
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
        /// X拍照位 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXPhotographyPosition
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD74", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X放料位1 GotFocus
        /// </summary>
        public RelayCommand GotFocusXMaterialPlacementPosition1
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
        /// X放料位1 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXMaterialPlacementPosition1
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD78", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X放料位2 GotFocus
        /// </summary>
        public RelayCommand GotFocusXMaterialPlacementPosition2
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
        /// X放料位2 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXMaterialPlacementPosition2
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD82", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X预留位1 GotFocus
        /// </summary>
        public RelayCommand GotFocusXReservedPosition1
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
        /// X预留位1 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXReservedPosition1
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD86", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// X预留位2 GotFocus
        /// </summary>
        public RelayCommand GotFocusXReservedPosition2
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
        /// X预留位2 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusXReservedPosition2
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD90", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }
        #endregion

        #region Y轴
        /// <summary>
        /// 向左MouseDown
        /// </summary>
        public RelayCommand LeftMouseDownCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.6", true);
                });
            }
        }

        /// <summary>
        /// 向左MouseUp
        /// </summary>
        public RelayCommand LeftMouseUpCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.6", false);
                });
            }
        }

        /// <summary>
        /// 向右MouseDown
        /// </summary>
        public RelayCommand RightMouseDownCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.7", true);
                });
            }
        }

        /// <summary>
        /// 向右MouseUp
        /// </summary>
        public RelayCommand RightMouseUpCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX1.7", false);
                });
            }
        }

        /// <summary>
        /// Y轴定位启动MouseDown
        /// </summary>
        public RelayCommand YMouseDownCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX2.7", true);
                });
            }
        }

        /// <summary>
        /// Y轴定位启动MouseUp
        /// </summary>
        public RelayCommand YMouseUpCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    plc.Write("DB8.DBX2.7", false);
                });
            }
        }

        /// <summary>
        /// Y轴点动速度 GotFocus
        /// </summary>
        public RelayCommand GotFocusYJogSpeed
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
        /// Y轴点动速度 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYJogSpeed
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD38", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y轴定位速度 GotFocus
        /// </summary>
        public RelayCommand GotFocusYLocalSpeed
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
        /// Y轴定位速度 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYLocalSpeed
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD42", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y轴定位位置 GotFocus
        /// </summary>
        public RelayCommand GotFocusYLocation
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
        /// Y轴定位位置 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYLocation
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB8.DBD72", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y取料位 GotFocus
        /// </summary>
        public RelayCommand GotFocusYPickupLocation
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
        /// Y取料位 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYPickupLocation
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD98", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y拍照位 GotFocus
        /// </summary>
        public RelayCommand GotFocusYPhotographyPosition
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
        /// Y拍照位 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYPhotographyPosition
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD102", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y放料位1 GotFocus
        /// </summary>
        public RelayCommand GotFocusYMaterialPlacementPosition1
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
        /// Y放料位1 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYMaterialPlacementPosition1
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD106", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y放料位2 GotFocus
        /// </summary>
        public RelayCommand GotFocusYMaterialPlacementPosition2
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
        /// Y放料位2 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYMaterialPlacementPosition2
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD110", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y预留位1 GotFocus
        /// </summary>
        public RelayCommand GotFocusYReservedPosition1
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
        /// Y预留位1 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYReservedPosition1
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD114", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }

        /// <summary>
        /// Y预留位2 GotFocus
        /// </summary>
        public RelayCommand GotFocusYReservedPosition2
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
        /// Y预留位2 LostFocus
        /// </summary>
        public RelayCommand<string> LostFocusYReservedPosition2
        {
            get
            {
                return new RelayCommand<string>((s) =>
                {
                    plc.Write("DB9.DBD118", Convert.ToSingle(s));
                    IsGotFocus = false;
                });
            }
        }
        #endregion

        #endregion

        #region 构造函数
        public ServerXYViewModel()
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
            XaxisModel = new Xaxis()
            {
                CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD52")).ConvertToFloat().ToString("0.000"),
                CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD56")).ConvertToFloat().ToString("0.00000000"),
                JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD30")).ConvertToFloat().ToString("0"),
                Location = Convert.ToUInt32(plc.Read("DB8.DBD60")).ConvertToFloat().ToString("0.00"),
                LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD34")).ConvertToFloat().ToString("0"),
                PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD70")).ConvertToFloat().ToString("0.00"),
                PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD74")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD78")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD82")).ConvertToFloat().ToString("0.00"),
                ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD86")).ConvertToFloat().ToString("0.00"),
                ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD90")).ConvertToFloat().ToString("0.00"),
            };
            YaxisModel = new Yaxis()
            {
                CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD64")).ConvertToFloat().ToString("0.000"),
                CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD68")).ConvertToFloat().ToString("0.00000000"),
                JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD38")).ConvertToFloat().ToString("0"),
                Location = Convert.ToUInt32(plc.Read("DB8.DBD72")).ConvertToFloat().ToString("0.00"),
                LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD42")).ConvertToFloat().ToString("0"),
                PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD98")).ConvertToFloat().ToString("0.00"),
                PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD102")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD106")).ConvertToFloat().ToString("0.00"),
                MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD110")).ConvertToFloat().ToString("0.00"),
                ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD114")).ConvertToFloat().ToString("0.00"),
                ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD118")).ConvertToFloat().ToString("0.00"),
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
                    XaxisModel.CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD52")).ConvertToFloat().ToString("0.000");
                    XaxisModel.CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD56")).ConvertToFloat().ToString("0.00000000");
                    YaxisModel.CurrentLocation = Convert.ToUInt32(plc.Read("DB8.DBD64")).ConvertToFloat().ToString("0.000");
                    YaxisModel.CurrentSpeed = Convert.ToUInt32(plc.Read("DB8.DBD68")).ConvertToFloat().ToString("0.00000000");

                    if (!IsGotFocus)
                    {
                        XaxisModel.JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD30")).ConvertToFloat().ToString("0");
                        XaxisModel.Location = Convert.ToUInt32(plc.Read("DB8.DBD60")).ConvertToFloat().ToString("0.00");
                        XaxisModel.LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD34")).ConvertToFloat().ToString("0");
                        XaxisModel.PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD70")).ConvertToFloat().ToString("0.00");
                        XaxisModel.PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD74")).ConvertToFloat().ToString("0.00");
                        XaxisModel.MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD78")).ConvertToFloat().ToString("0.00");
                        XaxisModel.MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD82")).ConvertToFloat().ToString("0.00");
                        XaxisModel.ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD86")).ConvertToFloat().ToString("0.00");
                        XaxisModel.ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD90")).ConvertToFloat().ToString("0.00");

                        YaxisModel.JogSpeed = Convert.ToUInt32(plc.Read("DB9.DBD38")).ConvertToFloat().ToString("0");
                        YaxisModel.Location = Convert.ToUInt32(plc.Read("DB8.DBD72")).ConvertToFloat().ToString("0.00");
                        YaxisModel.LocalSpeed = Convert.ToUInt32(plc.Read("DB9.DBD42")).ConvertToFloat().ToString("0");
                        YaxisModel.PickupLocation = Convert.ToUInt32(plc.Read("DB9.DBD98")).ConvertToFloat().ToString("0.00");
                        YaxisModel.PhotographyPosition = Convert.ToUInt32(plc.Read("DB9.DBD102")).ConvertToFloat().ToString("0.00");
                        YaxisModel.MaterialPlacementPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD106")).ConvertToFloat().ToString("0.00");
                        YaxisModel.MaterialPlacementPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD110")).ConvertToFloat().ToString("0.00");
                        YaxisModel.ReservedPosition1 = Convert.ToUInt32(plc.Read("DB9.DBD114")).ConvertToFloat().ToString("0.00");
                        YaxisModel.ReservedPosition2 = Convert.ToUInt32(plc.Read("DB9.DBD118")).ConvertToFloat().ToString("0.00");
                    }
                }
            });
        }
        #endregion
    }
    
}
