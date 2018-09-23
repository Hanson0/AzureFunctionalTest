using MasterGPSLocator.Config;
using MasterGPSLocator.Uart;
using OnlineWritingProcess;
using Production.ProductionTest;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterGPSLocator.ProductTest
{
    class ProductionTestFlow
    {
        private MainForm frmMain;
        private int StandardCN0;

        private string configPath = ConfigInfo.ConfigPath;            //配置文件路径
        private bool flagCreateNewRow;

        private bool runState;//运行状态     true为运行中，false为未运行
        private ReadWriteHandle readWriteIdHandle;
        private string labelSn;
        public int StandardCN01
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                Win32API.GetPrivateProfileString("Standard", "Parameter", "", stringBuilder, 256, configPath);
                try
                {
                    StandardCN0 = int.Parse(stringBuilder.ToString().Trim());
                }
                catch (Exception)
                {
                    StandardCN0 = 40;
                }
                return StandardCN0;
            }
        }
        public ProductionTestFlow(MainForm frmMain)
        {
            this.frmMain = frmMain;

            runState = false;
            readWriteIdHandle = new ReadWriteHandle(frmMain);

            InitTimeOutTimer();
        }
        private System.Timers.Timer timeoutTimer = new System.Timers.Timer();
        private void InitTimeOutTimer()
        {
            timeoutTimer.AutoReset = false;
            timeoutTimer.Interval = ProductionInfo.TimeOut;
            timeoutTimer.Enabled = true;
            timeoutTimer.Elapsed += TimeoutTimer_Elapsed;
        }
        private void TimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            readWriteIdHandle.IsTimeOut = true;

        }

        public void StartTimeoutTimer()
        {
            readWriteIdHandle.IsTimeOut = false;
            timeoutTimer.Start();
        }
        public void StopTimeOutTimer()
        {
            timeoutTimer.Stop();
        }

        /// <summary>
        /// 检测模块上电
        /// </summary>
        public void CheckModulePowerOn()
        {
            readWriteIdHandle.CheckModulePowerOn();
        }
        /// <summary>
        /// 检测模块掉电
        /// </summary>
        public void CheckModulePowerOff()
        {
            readWriteIdHandle.CheckModulePowerOff();
        }

        /// <summary>
        /// 任务入口及任务流执行后处理
        /// </summary>
        public void TestTaskMain(string labelSn)
        {
            this.labelSn = labelSn;
            runState = true;
            flagCreateNewRow = false;

            readWriteIdHandle.FlagDisplayUart = true;

            //执行流程
            int ret = TestTaskFlow();

            //frmMain.DisplayLog(string.Format("标签IMEI：{0}\r\n", labelImei));
            //结果判断
            Production.Result.ResultJudge resultJudge = Production.Result.ResultJudge.GetResultJudge(frmMain);
            if (ProductionInfo.Type == ProductionInfo.SystemType.iMES || ProductionInfo.Type == ProductionInfo.SystemType.GSMMES)
            {
                resultJudge.Sn = labelSn;
                //resultJudge.Sn = snRead;
                //resultJudge.Eid = eidRead;
                //resultJudge.Iccid = iccidRead;
            }
            resultJudge.PutResult(labelSn, ret);

            runState = false;
            readWriteIdHandle.FlagDisplayUart = false;
            frmMain.IsPressSpace = false;
        }

        /// <summary>
        /// 任务流
        /// </summary>
        /// <returns></returns>
        private int TestTaskFlow()
        {
            int ret = -1;
            //string pattern = null;
            //eidRead = "NULL";

            //两种情况进入此处:1、超时跳出了do while,2、按了空格确认上电
            if (readWriteIdHandle.IsTimeOut)
            {
                frmMain.DisplayLog("测试超时\r\n");
                return ret;
            }

            do
            {
                frmMain.DisplayLog("测试中...\r\n");
                //获取OS版本
                CmdProcess cmdProcess = new CmdProcess();   //cmd类
                string strRet = cmdProcess.ExeCommand("", "azsphere device show-ota-status");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device.") )
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                if (strRet.Contains("is not a valid value"))
                {
                    frmMain.DisplayLog(string.Format("设备未claim  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }

                //对比模块MAC地址-标签MAC地址
                strRet = cmdProcess.ExeCommand("", "azsphere device wifi show-status");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                string mouduleMac = cmdProcess.GetValueByKeyword(strRet, "MAC Address");
                if (mouduleMac != labelSn)
                {
                    frmMain.DisplayLog(string.Format("模块MAC:{0}——模块MAC:{1} 对比不一致  FAIL\r\n", mouduleMac, labelSn));
                    break;
                }
                frmMain.DisplayLog(string.Format("模块MAC:{0}——模块MAC:{1} 对比一致 PASS\r\n", mouduleMac, labelSn));

                //检查是否有测试程序

                //azsphere device sideload show-status
                //azsphere device sideload delete
                //azsphere device sideload deploy -p mt_app.img

                //Side load（加载测试固件）
                strRet = cmdProcess.ExeCommand("", "azsphere device sideload deploy -p mt_app.img");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                if (!strRet.Contains("Command completed successfully"))
                {
                    frmMain.DisplayLog(string.Format("烧录失败  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                frmMain.DisplayLog(string.Format("测试固件烧录成功\r\n", strRet));
                
                //串口发送Reset System\r\n
                eidRead = ReadWriteHandle.Read(ATReadCmd.ReadIdType.EidRead);


                //List<string> listValue = readWriteIdHandle.ReadId();
                //if (listValue.Count <= 3)
                //{
                //    break;
                //}
                ////多组：1,1,01,01,,,40,0*61
                //int sum = 0;
                //string[] arryData;
                //try
                //{
                //    foreach (string strData in listValue)
                //    {
                //        arryData = strData.Split(',');
                //        应滤除其他可视卫星
                //        int cn0Value = int.Parse(arryData[6]);
                //        sum += cn0Value;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    frmMain.DisplayLog(string.Format("数据解析错误：{0}\r\n", ex.Message));
                //    break;
                //}
                //int argVale = sum / listValue.Count;
                //frmMain.DisplayLog(string.Format("获取C/N0值：{0}\r\n", argVale));
                //if (argVale < StandardCN01)
                //{
                //    frmMain.DisplayLog(string.Format("C/N0值：{0},小于设定范围{1}  FAIL\r\n", argVale, StandardCN01));
                //    break;
                //}
                //frmMain.DisplayLog(string.Format("C/N0值：{0}，大于设定范围{1}   PASS\r\n", argVale,StandardCN01));
                //ret = 0;

                ////从芯片获取eid
                ////eidRead = "081603FFFFF35293" + eidlast.ToString("X").PadLeft(4,'0');
                //eidRead = readWriteIdHandle.ReadId(ATReadCmd.ReadIdType.EidRead);
                //pattern = @"[0-9A-Z]{20}";
                //if (string.IsNullOrEmpty(eidRead) || !Regex.IsMatch(eidRead, pattern))
                //{
                //    frmMain.DisplayLog("EID读取失败\r\n");
                //    break;
                //}
                //frmMain.DisplayLog(string.Format("已获取模块EID：{0}\r\n", eidRead));
                //frmMain.SetText(AllForms.EnumControlWidget.txtEid.ToString(), eidRead, false);



            } while (false);



            ProductionInfo.SystemType systemType = ProductionInfo.Type;

            //使用do..while(false)的原因，是为了当测试流程为fail时，使用break跳出该结构，仍然执行结果上报

            return ret;
        }








    }
}
