using MasterGPSLocator.Tool;
using MasterGPSLocator.Uart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterGPSLocator.ProductTest
{
    class ProductTestFactory
    {
        private static ProductTestFactory productionTestFactoty;
        private MainForm frmMain;

        private static MyStopwatch stopwatch;//秒表
        private static ProductionTestFlow flow;
        /// <summary>
        /// 私有构造函数
        /// </summary>
        /// <param name="frmMain"></param>
        private ProductTestFactory(MainForm frmMain)
        {
            this.frmMain = frmMain;
            stopwatch = new MyStopwatch(frmMain.DisplayStopwatch);

            flow = new ProductionTestFlow(frmMain);

        }
        /// <summary>
        /// 获取唯一实例
        /// </summary>
        /// <param name="frmMain"></param>
        /// <returns></returns>
        public static ProductTestFactory GetProductTestFactory(MainForm frmMain)
        {
            if (productionTestFactoty==null)
            {
                productionTestFactoty = new ProductTestFactory(frmMain);
            }
            return productionTestFactoty;
        }

        /// <summary>
        /// 循环检测测试线程状态并循环开启测试线程
        /// </summary>
        public void CheckProductionTestState(object labelImeiIn)
        {
            //秒表伴随测试线程
            stopwatch.ReStart();
            flow.StartTimeoutTimer();

            string labelImei = labelImeiIn as string;

            //HttpAllCheck httpImeiSnDecorrelation = new HttpAllCheck();
            //Test
            //int ret = httpImeiSnDecorrelation.DataGetAndAnalysis("PLANTEST", "898602C99916C0362528", "864867040002025", "CH04027410010001", "898602C99916C0362528");
            //frmMain.ClearUILastTestState();
            //循环检测模块上电
            frmMain.DisplayLog("正在确认模块上电，请插入模块后按下空格键...\r\n");
            flow.CheckModulePowerOn();

            //开始测试
            flow.TestTaskMain(labelImei);
            flow.StopTimeOutTimer();
            stopwatch.Stop();
            frmMain.SetTextBoxReadOnly(EnumControlWidget.txtLabelSn.ToString(), false);

            //////循环检测模块掉电
            //frmMain.DisplayLog("正在检测模块掉电，请断电拔下模块...\r\n");
            //flow.CheckModulePowerOff();
            //frmMain.DisplayLog("已断电\r\n");
            //frmMain.ClearUILastTestState();

            Thread.Sleep(200);


        }



    }
}
