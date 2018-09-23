using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Production.Result;
using Production.ProductionTest;
using System.Windows.Forms;
namespace MasterGPSLocator.Config
{
    class ConfigInfo
    {
        private static string configPath;

        public static string ConfigPath
        {
            get { return configPath; }
            set { configPath = value; }
        }


        static ConfigInfo()
        {
            configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Setup.ini");
        }
        /// <summary>
        /// 初始化所有基础类的配置
        /// </summary>
        public static void Init()
        {
            try
            {
                ResultInfo.ReadConfig();
                ProductionInfo.ReadConfig();
                //Server.HttpServerInfo.ReadConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }
        }


    }
}
