using MasterGPSLocator.Config;
using Production;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.Result
{
    static class ResultInfo
    {
        private static string configPath;
        //结果统计
        private static int pass;
        private static int fail;
        private static string folderLog;
        private static object obj = new object();

        public static int Pass
        {

            get
            {
                return pass;
            }

            set
            {
                lock (obj)
                {
                    pass = value;
                    Win32API.WritePrivateProfileString("Result", "Pass", pass.ToString(), configPath);
                }

            }
        }

        public static int Fail
        {
            get
            {
                return fail;
            }

            set
            {
                lock (obj)
                {
                    fail = value;
                    Win32API.WritePrivateProfileString("Result", "Fail", fail.ToString(), configPath);
                }
            }
        }

        public static string FolderLog
        {
            get
            {
                return folderLog;
            }

            set
            {
                folderLog = value;
            }
        }



        public static void ReadConfig()
        {
            configPath = ConfigInfo.ConfigPath;            //配置文件路径
            StringBuilder stringBuilder = new StringBuilder();

            Win32API.GetPrivateProfileString("Path", "LogPath", "", stringBuilder, 256, configPath);
            folderLog = stringBuilder.ToString().Trim();

            Win32API.GetPrivateProfileString("Result", "Pass", "", stringBuilder, 256, configPath);
            pass = int.Parse(stringBuilder.ToString().Trim());
            Win32API.GetPrivateProfileString("Result", "Fail", "", stringBuilder, 256, configPath);
            fail = int.Parse(stringBuilder.ToString().Trim());
        }


        /// <summary>
        /// 清零
        /// </summary>
        public static void ClearResult()
        {
            Pass = 0;
            Fail = 0;
        }
    }
}
