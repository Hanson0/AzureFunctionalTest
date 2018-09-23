using MasterGPSLocator.Config;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Production.SerialPortNS
{
    static class SerialPortInfo
    {
        private static string fileSetup = ConfigInfo.ConfigPath;            //配置文件路径

        private static List<string> spPortNames;
        private static int spBaudRate;
        private static int spDataBits;
        private static Parity spParity;
        private static StopBits spStopBits;

        public static List<string> SpPortNames
        {
            get
            {
                return spPortNames;
            }

            set
            {
                spPortNames = value;
            }
        }

        public static int SpBaudRate
        {
            get
            {
                return spBaudRate;
            }

            set
            {
                spBaudRate = value;
            }
        }

        public static int SpDataBits
        {
            get
            {
                return spDataBits;
            }

            set
            {
                spDataBits = value;
            }
        }

        public static Parity SpParity
        {
            get
            {
                return spParity;
            }

            set
            {
                spParity = value;
            }
        }

        public static StopBits SpStopBits
        {
            get
            {
                return spStopBits;
            }

            set
            {
                spStopBits = value;
            }
        }



        public static void ReadConfig()
        {
            StringBuilder stringBuilder = new StringBuilder();
            spPortNames = new List<string>();

            Win32API.GetPrivateProfileString("SerialPort", "PortName", "", stringBuilder, 256, fileSetup);
            spPortNames.Add(stringBuilder.ToString());

            Win32API.GetPrivateProfileString("SerialPort", "BaudRate", "", stringBuilder, 256, fileSetup);
            spBaudRate = int.Parse(stringBuilder.ToString());
            Win32API.GetPrivateProfileString("SerialPort", "DataBits", "", stringBuilder, 256, fileSetup);
            spDataBits = int.Parse(stringBuilder.ToString());
            Win32API.GetPrivateProfileString("SerialPort", "Parity", "", stringBuilder, 256, fileSetup);
            spParity = (System.IO.Ports.Parity)(int.Parse(stringBuilder.ToString()));
            Win32API.GetPrivateProfileString("SerialPort", "StopBits", "", stringBuilder, 256, fileSetup);
            spStopBits = (System.IO.Ports.StopBits)(int.Parse(stringBuilder.ToString()));
        }
    }
}
