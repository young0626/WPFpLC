using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WPF实现PLC触摸屏程序.Models
{
    public class Zaxis : ObservableObject
    {
        #region 依赖属性
        private string currentLocation;
        /// <summary>
        /// 当前位置
        /// </summary>
        public string CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; OnPropertyChanged(); }
        }

        private string currentSpeed;
        /// <summary>
        /// 当前速度
        /// </summary>
        public string CurrentSpeed
        {
            get { return currentSpeed; }
            set { currentSpeed = value; OnPropertyChanged(); }
        }

        private string jogSpeed;
        /// <summary>
        /// 点动速度
        /// </summary>
        public string JogSpeed
        {
            get { return jogSpeed; }
            set { jogSpeed = value; OnPropertyChanged(); }
        }

        private string location;
        /// <summary>
        /// 定位位置
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; OnPropertyChanged(); }
        }

        private string localSpeed;
        /// <summary>
        /// 定位速度
        /// </summary>
        public string LocalSpeed
        {
            get { return localSpeed; }
            set { localSpeed = value; OnPropertyChanged(); }
        }

        private string pickupLocation;
        /// <summary>
        /// 取料位
        /// </summary>
        public string PickupLocation
        {
            get { return pickupLocation; }
            set { pickupLocation = value; OnPropertyChanged(); }
        }

        private string photographyPosition;
        /// <summary>
        /// 拍照位
        /// </summary>
        public string PhotographyPosition
        {
            get { return photographyPosition; }
            set { photographyPosition = value; OnPropertyChanged(); }
        }

        private string materialPlacementPosition1;
        /// <summary>
        /// 放料位1
        /// </summary>
        public string MaterialPlacementPosition1
        {
            get { return materialPlacementPosition1; }
            set { materialPlacementPosition1 = value; OnPropertyChanged(); }
        }

        private string materialPlacementPosition2;
        /// <summary>
        /// 放料位2
        /// </summary>
        public string MaterialPlacementPosition2
        {
            get { return materialPlacementPosition2; }
            set { materialPlacementPosition2 = value; OnPropertyChanged(); }
        }

        private string reservedPosition1;
        /// <summary>
        /// 预留位1，已经设成：安全位
        /// </summary>
        public string ReservedPosition1
        {
            get { return reservedPosition1; }
            set { reservedPosition1 = value; OnPropertyChanged(); }
        }

        private string reservedPosition2;
        /// <summary>
        /// 预留位2
        /// </summary>
        public string ReservedPosition2
        {
            get { return reservedPosition2; }
            set { reservedPosition2 = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
