using MasterGPSLocator.Config;
using Production.SerialPortNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterGPSLocator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //运行环境检查
            FactoryAuto.CommonFunction.CheckSystemDrive();
            ConfigInfo.Init();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            Application.ApplicationExit += Application_ApplicationExit;
        }
        /// <summary>
        /// 应用程序关闭前触发事件
        /// 执行程序的善后操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            SerialPortFactory.GetSerialPort().Close();
        }


    }
}
