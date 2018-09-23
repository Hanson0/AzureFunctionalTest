using MasterGPSLocator.Config;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterGPSLocator.Uart
{
    class ReadWriteHandInfo
    {
        private static string configPath;
        private static int checkDeviceInterval;      //检测模块上掉电的时间间隔 ms

        public static int CheckDeviceInterval
        {
            get { return ReadWriteHandInfo.checkDeviceInterval; }
            set { ReadWriteHandInfo.checkDeviceInterval = value; }
        }

        public static void ReadConfig()
        {
            StringBuilder stringBuilder = new StringBuilder();
            configPath = ConfigInfo.ConfigPath;

            Win32API.GetPrivateProfileString("Time", "CheckDeviceInterval", "", stringBuilder, 256, configPath);
            CheckDeviceInterval = int.Parse(stringBuilder.ToString().Trim());
        }









    }
}
