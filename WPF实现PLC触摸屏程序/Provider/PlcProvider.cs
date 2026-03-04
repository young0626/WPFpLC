using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF实现PLC触摸屏程序.Provider
{
    public sealed class PlcProvider
    {
        /// <summary>
        /// 全局的plc实例
        /// </summary>
        public Plc CurrentPlc;
        
        private static readonly Lazy<PlcProvider> lazy = new Lazy<PlcProvider>(() => new PlcProvider());
        /// <summary>
        /// 全局的PlcProvider
        /// </summary>
        public static PlcProvider Instance { get { return lazy.Value; } }
        private  PlcProvider()
        {
            CurrentPlc = new Plc(CpuType.S71200, "192.168.8.10", 102, 0, 1);
        }
    }
}
