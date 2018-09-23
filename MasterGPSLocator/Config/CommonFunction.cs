using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FactoryAuto
{
    class CommonFunction
    {
        /// <summary>
        /// 程序启动检查
        /// </summary>
        /// 在DEBUG模式下，该方法不会运行
        [Conditional("RELEASE")]
        public static void StartCheck()
        {
            //检查程序是否正在运行
            CheckApplicationRunning();

            //检查运行环境是否为系统盘
            CheckSystemDrive();
        }

        /// <summary>
        /// 检查运行环境是否为系统盘
        /// </summary>
        public static void CheckSystemDrive()
        {
            string systemPath = Environment.ExpandEnvironmentVariables("%systemdrive%");
            string currentPath = Environment.CurrentDirectory;

            if (systemPath.Substring(0, 1) == currentPath.Substring(0, 1))
            {
                MessageBox.Show("请勿将应用程序放在系统盘！", "系统信息", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 检查程序是否正在运行
        /// </summary>
        public static void CheckApplicationRunning()
        {
            //获取当前进程
            Process current = Process.GetCurrentProcess();
            //获取当前进程的运行路劲和应用程序文件名
            string fileName = current.MainModule.FileName;
            //Process[] processes = Process.GetProcessesByName(current.ProcessName);
            Process[] processes = Process.GetProcesses();

            int count = 0;
            foreach (var item in processes)
            {
                try
                {
                    if (item.MainModule.FileName == fileName)
                    {
                        count++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            if (count >= 2)
            {
                MessageBox.Show("程序正在运行，请先退出！", "系统信息", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }
    }
}
