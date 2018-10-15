using MasterGPSLocator.Config;
using MasterGPSLocator.Tool;
using MasterGPSLocator.Uart;
using OnlineWritingProcess;
using Production.IdReadWrite.Cmd;
using Production.ProductionTest;
using Production.Windows;
using SocketHelp;
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
        private string testAppName;
        private string efuseBin;
        private int isManufactureComplete;

        

        private string configPath = ConfigInfo.ConfigPath;            //配置文件路径
        private bool flagCreateNewRow;

        private bool runState;//运行状态     true为运行中，false为未运行
        private ReadWriteHandle readWriteIdHandle;
        private string labelSn;
        ThirdPartyTool tool;
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
        public string TestAppName
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                Win32API.GetPrivateProfileString("FileName", "TestAppName", "", stringBuilder, 256, configPath);
                testAppName =stringBuilder.ToString().Trim();
                return testAppName;
            }
        }
        public string EfuseBin
        {

            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                Win32API.GetPrivateProfileString("FileName", "EfuseBin", "", stringBuilder, 256, configPath);
                efuseBin =stringBuilder.ToString().Trim();
                return efuseBin;
            }
        }
        public int IsManufactureComplete
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                Win32API.GetPrivateProfileString("Selection", "SetManufactureComplete", "", stringBuilder, 256, configPath);
                isManufactureComplete = int.Parse(stringBuilder.ToString().Trim());
                return isManufactureComplete;
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
                CmdProcess cmdProcess = new CmdProcess();   //cmd类
                tool = new ThirdPartyTool()
                {
                    ToolDirectory = "Microsoft Azure Sphere SDK",
                    ToolName = "InitializeCommandPrompt",
                    Cmd = "azsphere device wifi show-status"
                };

                string strRet;
                #region //获取OS版本
                //strRet = cmdProcess.ExeCommand("", "azsphere device show-ota-status");//"azsphere device wifi show-status"
                //if (strRet.Contains("error: Could not connect to the device."))
                //{
                //    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                //if (strRet.Contains("is not a valid value"))
                //{
                //    frmMain.DisplayLog(string.Format("未claim设备 获取OS版本失败 FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                #endregion
                //对比模块MAC地址-标签MAC地址
                strRet = cmdProcess.ExeCommand("", "azsphere device wifi show-status");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                //ret = DeviceCheck(tool, out strRet);
                //if (ret!=0)
                //{
                //    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                string mouduleMac = cmdProcess.GetValueByKeyword(strRet, "MAC Address");
                if (mouduleMac != labelSn)
                {
                    frmMain.DisplayLog(string.Format("MAC检查：模块{0}，标签{1} 对比不一致  FAIL\r\n", mouduleMac, labelSn));
                    break;
                }
                frmMain.DisplayLog(string.Format("MAC检查：模块{0}，标签{1} 对比一致  PASS\r\n", mouduleMac, labelSn));

                //检查是否有测试程序

                //azsphere device sideload show-status
                //azsphere device sideload delete
                //azsphere device sideload deploy -p mt_app.img


                //删除当前程序
                frmMain.DisplayLog("当前程序删除中...\r\n");
                strRet = cmdProcess.ExeCommand("", "azsphere device sideload delete");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                if (!strRet.Contains("Command completed successfully"))
                {
                    frmMain.DisplayLog(string.Format("当前程序删除： 失败  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                frmMain.DisplayLog("当前程序删除：成功 PASS\r\n");
                Thread.Sleep(500);

                //Side load（加载测试固件）
                frmMain.DisplayLog("测试固件加载中...\r\n");
                strRet = cmdProcess.ExeCommand("", string.Format("azsphere device sideload deploy -p ../app/{0}",TestAppName));//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }

                //tool.Cmd = "azsphere device sideload deploy -p mt_app.img";

                //ret = DeviceCheck(tool, out strRet);
                //if (ret != 0)
                //{
                //    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                if (!strRet.Contains("Command completed successfully"))
                {
                    frmMain.DisplayLog(string.Format("加载测试固件：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                frmMain.DisplayLog(string.Format("加载测试固件：成功 PASS\r\n", strRet));


                string pinTestRetinfo;
                //Socket复位
                frmMain.DisplayLog("复位测试中...\r\n");//成功 为有效信息
                bool pinTestRet = PinTestBySocket("reset_tests", out pinTestRetinfo);
                frmMain.DisplayLog(pinTestRetinfo + "\r\n");//失败 为错误信息
                if (!pinTestRet)
                {
                    frmMain.DisplayLog("复位测试：失败 FAIL\r\n");//失败 为错误信息
                    break;
                }
                frmMain.DisplayLog("复位测试：合格 PASS\r\n");//成功 为有效信息

                //Socket GPIO检查
                frmMain.DisplayLog("GPIO测试中...\r\n");
                pinTestRet = PinTestBySocket("get_gpios", out pinTestRetinfo);
                frmMain.DisplayLog(pinTestRetinfo + "\r\n");//失败 为错误信息

                if (!pinTestRet)
                {
                    frmMain.DisplayLog("GPIO测试：测试失败 FAIL\r\n");//失败 为错误信息
                    break;
                }
                string errorStr;
                bool gpioRet = GetGIPOResultBySocketRet(pinTestRetinfo, out errorStr);
                //展示返回的GPIO数据
                //strRet = strRet.Replace("P", "PASS").Replace("F", "FAIL") + "\r\n";
                //frmMain.DisplayLog(strRet);
                //GPIO结果显示
                if (!gpioRet)
                {
                    frmMain.DisplayLog("GPIO测试: 不合格  FAIL \r\n");
                    break;
                }
                frmMain.DisplayLog("GPIO测试：合格 PASS\r\n");//成功 为有效信息

                #region 串口测试 复位和GPIO
                ////串口发送Reset System\r\n
                //strRet = readWriteIdHandle.Read(ATReadCmd.ReadIdType.Reset);
                //if (string.IsNullOrEmpty(strRet))
                //{
                //    frmMain.DisplayLog(string.Format("复位命令未得到回应 串口失败  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                ////if (!strRet.Contains("Get RST command"))
                ////{
                ////    frmMain.DisplayLog(string.Format("复位未得到回应 串口失败  FAIL\r\n详细：{0}  \r\n", strRet));
                ////    break;
                ////}
                ////复位命令得到回应
                //frmMain.DisplayLog(string.Format("复位中... \r\n"));
                ////跳出循环说明复位成功
                //readWriteIdHandle.ReadRestInfo("init completed");
                //frmMain.DisplayLog(string.Format("复位成功  PASS\r\n"));


                ////GPIO测试
                ////串口发送Reset System\r\n
                //Thread.Sleep(1000);
                //strRet = readWriteIdHandle.Read(ATReadCmd.ReadIdType.GPIOTest);
                //if (string.IsNullOrEmpty(strRet))
                //{
                //    frmMain.DisplayLog("GPIO测试命令未得到回应 串口失败  FAIL\r\n");
                //    break;
                //}
                ////GPIO测试命令得到回应
                ////frmMain.DisplayLog("GPIO测试中... \r\n");
                ////string errorInfo;
                ////bool gpioRet = checkGIPOResult(strRet, out errorInfo);
                //////展示返回的GPIO数据
                ////strRet = strRet.Replace("P", "PASS").Replace("F", "FAIL") + "\r\n";
                ////frmMain.DisplayLog(strRet);
                //////GPIO结果显示
                ////if (!gpioRet)
                ////{
                ////    frmMain.DisplayLog(string.Format("GPIO测试:失败  FAIL \r\n详细：{0}\r\n", errorInfo));
                ////    break;
                ////}
                ////frmMain.DisplayLog("GPIO测试：成功  PASS\r\n");
                #endregion
                //删除APPtest
                frmMain.DisplayLog("测试固件删除中...\r\n");
                strRet = cmdProcess.ExeCommand("", "azsphere device sideload delete");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                //tool.Cmd = "azsphere device sideload deploy -p mt_app.img";

                //ret = DeviceCheck(tool, out strRet);
                //if (ret != 0)
                //{
                //    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                if (!strRet.Contains("Command completed successfully"))
                {
                    frmMain.DisplayLog(string.Format("测试固件删除：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                frmMain.DisplayLog("测试固件删除：成功 PASS\r\n");

                frmMain.DisplayLog("Wifi列表网络检查中...\r\n");
                //检查网络 wifi表
                strRet = cmdProcess.ExeCommand("", "azsphere device wifi list");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                if (!strRet.Contains("No networks found") && strRet.Contains("ID"))
                {
                    //Get ID
                    string[] wifiIdList = cmdProcess.GetListByKeyword(strRet, "ID").ToArray();

                    foreach (var item in wifiIdList)
                    {
                        frmMain.DisplayLog("WIFI列表检查：含有WIFI列表ID,删除中...\r\n");
                        //不为空
                        if (!string.IsNullOrEmpty(item))
                        {
                            //删除ID
                            strRet = cmdProcess.ExeCommand("", string.Format("azsphere device wifi delete –i {0}", item));//"azsphere device wifi show-status"
                            if (strRet.Contains("error: Could not connect to the device."))
                            {
                                frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                                return ret;
                            }
                            //不包含成功信息且不包含
                            if (!strRet.Contains("Successfully removed network") && !strRet.Contains("Command completed successfully"))
                            {
                                frmMain.DisplayLog(string.Format("WIFI列表ID删除：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                                return ret;
                            }
                        }

                    }
                }
                frmMain.DisplayLog("WIFI列表检查：PASS\r\n");
            //性能测试
                frmMain.DisplayLog("capabilties测试中...\r\n");
            Capability:
                strRet = cmdProcess.ExeCommand("", "azsphere device capability show-attached");//"azsphere device wifi show-status"
                if (strRet.Contains("error: Could not connect to the device."))
                {
                    frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                if (!strRet.Contains("Command completed successfully"))
                {
                    frmMain.DisplayLog(string.Format("capabilties测试：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                List<string> capabilitiesList = cmdProcess.GetListCapabilitiesKeyword(strRet, "Enable");
                if (capabilitiesList.Count > 0)
                {
                    //manufacture set complete status，只能设置一次，设置后就不能够RF测试和校准了，测试阶段不要轻易尝试。
                    #region
                    if (IsManufactureComplete==1)
                    {
                        strRet = cmdProcess.ExeCommand("", "manufacture set complete status");//"azsphere device wifi show-status"
                        if (strRet.Contains("error: Could not connect to the device."))
                        {
                            frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                            break;
                        }
                        frmMain.DisplayLog(string.Format("已设置制作完成状态，继续测试capabilties中...  \r\n详细：{0}  \r\n", strRet));
                        goto Capability;
                    }
                    #endregion
                    frmMain.DisplayLog(string.Format("capabilties测试：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                    break;
                }
                frmMain.DisplayLog("capabilties测试：PASS\r\n");

                //E-fuse测试
                tool = new ThirdPartyTool()
                {
                    ToolDirectory = "RFToolCli",
                    Cmd = string.Format("RfSettingsTool check --expected ../bin/{0}",EfuseBin) 
                };
                //strRet = cmdProcess.ExeCommand(tool);//"azsphere device wifi show-status"
                //if (strRet.Contains("error"))
                //{
                //    frmMain.DisplayLog(string.Format("E-fuse测试：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}

                //python
                //tool.Cmd = "python RunHeaderTest.py ";//reset_system
                //tool.ToolDirectory = "PC";
                //strRet = cmdProcess.ExeCommand(tool, "");
                //if (strRet.Contains("error"))
                //{
                //    frmMain.DisplayLog(string.Format("E-fuse测试：失败  FAIL\r\n详细：{0}  \r\n", strRet));
                //    break;
                //}
                //E-fuse具体测试内容
                frmMain.DisplayLog("e-fuse测试中...\r\n");
                strRet = cmdProcess.ExeCommand(tool, "");
                string hasReadFlag = "Comparing configurations.";
                string startFlag = "Reading configuration data from device.";

                string erroInfo = StringHelp.SubCentre(strRet, startFlag, "\r\n\r\n");

                if (!strRet.Contains(hasReadFlag))
                {
                    frmMain.DisplayLog(string.Format("E-fuse测试：命令失败 \r\n详细：{0}  \r\n", erroInfo));
                    break;
                }

                strRet = StringHelp.SubCentre(strRet, hasReadFlag, "\r\n\r\n");
                frmMain.DisplayLog(strRet+"\r\n");
                if (strRet.Contains("ERROR") || strRet.Contains("异常"))
                {
                    frmMain.DisplayLog("E-fuse测试结果： FAIL \r\n");
                    break;
                }
                
                frmMain.DisplayLog("E-fuse测试：PASS\r\n");
                ret = 0;



                //cd RFToolCli

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


        private int DeviceCheck(ThirdPartyTool tool, out string strRet)
        {
            int ret = -1;
            CmdProcess cmdProcess = new CmdProcess();   //cmd类
            strRet = cmdProcess.ExeCommand(tool);//"azsphere device wifi show-status"
            if (strRet.Contains("error: Could not connect to the device."))
            {
                //frmMain.DisplayLog(string.Format("未上电连接上设备，请检查连接或驱动  FAIL\r\n详细：{0}  \r\n", strRet));
                return ret;
            }
            ret = 0;
            return ret;

        }


//        init completed
//GPIO TEST RESULT:PIN01,P;PIN02,P;PIN03,P;PIN04,P;PIN05,P;PIN06,P;PIN07,P;PIN08,P;PIN09,P;PIN10,P;PIN11,P;PIN14,P;PIN15,P;PIN16,P;PIN23,P;PIN24,P;PIN25,P;PIN26,P;PIN22,P;PIN21,F;PIN20,F;
//Test get_gpios finished with 0
//Test succeeded
        private bool GetGIPOResultBySocketRet(string strRet, out string errorStr)
        {
            int ret = 0;
            //去掉最后一个;
            //strRet.Replace()
            int startPos = strRet.IndexOf(':');
            int endPos = strRet.LastIndexOf(';');
            strRet = strRet.Substring(startPos+1, endPos - startPos-1);
            //strRet = strRet.Remove(strRet.LastIndexOf(';'), 1).Replace("\n", "");
            string[] eachGPIO = strRet.Split(';');
            string[] GPIO_ret;
            errorStr = "";
            foreach (var item in eachGPIO)
            {
                GPIO_ret = item.Split(',');
                if (GPIO_ret[1] == "F")
                {
                    errorStr += item + "\r\n";
                    ret = -1;
                }
            }
            if (ret == -1)
            {
                errorStr.Replace("F", "FAILL");
                return false;
            }
            return true;
        }


        private bool checkGIPOResult(string strRet, out string errorStr)
        {
            int ret = 0;
            //去掉最后一个;
            //strRet.Replace()
            strRet = strRet.Remove(strRet.LastIndexOf(';'), 1).Replace("\n", "");
            string[] eachGPIO = strRet.Split(';');
            string[] GPIO_ret;
            errorStr = "";
            foreach (var item in eachGPIO)
            {
                GPIO_ret = item.Split(',');
                if (GPIO_ret[1] == "F")
                {
                    errorStr += item + "\r\n";
                    ret = -1;
                }
            }
            if (ret == -1)
            {
                errorStr.Replace("F", "FAILL");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 成功时
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="retInfo"></param>
        private bool PinTestBySocket(string cmd,out string retInfo)
        {
            bool res = false;
            //string getInfo;
            HeaderTestClient htc = new HeaderTestClient();
            //string errorInfo;
            if (htc.connect(out retInfo))//连接错误信息,连接失败不会进入获取有效信息
            {
                //res返回false:测试失败  返回true:测试成功 
                res = htc.send_command_and_read_result(cmd, out retInfo);//"reset_tests"

                //MessageBox.Show(retInfo);
                htc.stop_server();
                htc.disconnet();
            }
            else
            {
                //MessageBox.Show(string.Format("连接失败 详情：{0}", retInfo));//
                retInfo = string.Format("连接失败 详情：{0}", retInfo);

            }
            return res;


        }



    }
}
