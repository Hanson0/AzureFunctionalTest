using MasterGPSLocator.Config;
using Production.IdReadWrite.Cmd;
using Production.SerialPortNS;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterGPSLocator.Uart
{
    class ReadWriteHandle
    {
        private MainForm frmMain;

        private SerialPort sp;
        private string recive;                      //接收字符串

        private bool flagCyclic;                    //开关循环检测模块上掉电的标志位

        private bool flagDisplayUart;               //是否显示串口输出信息

        private static int checkDeviceInterval;
        private bool isTimeOut = false;
        private string poweronFlag;
        private string powerDownFlag;

        private string testItemFlag;

        private static ATReadCmd atReset;
        private static ATReadCmd atIccidRead;
        private static ATReadCmd atGPIOTest;
        private static ATReadCmd atEidRead;
        private static ATReadCmd atVersonRead;


        //private static ATWriteCmd atImeiWrite;
        //private static ATWriteCmd atSnWrite;
        public string PoweronFlag
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                Win32API.GetPrivateProfileString("KeyWords", "PowerOnFlag", "", stringBuilder, 256, ConfigInfo.ConfigPath);
                poweronFlag = stringBuilder.ToString().Trim();
                return poweronFlag;
            }
        }
        public string PowerDownFlag
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                Win32API.GetPrivateProfileString("KeyWords", "PowerDownFlag", "", stringBuilder, 256, ConfigInfo.ConfigPath);
                powerDownFlag = stringBuilder.ToString().Trim();
                powerDownFlag=powerDownFlag.Replace("\"","");
                return powerDownFlag;
            }
        }

        public string TestItemFlag
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                Win32API.GetPrivateProfileString("KeyWords", "TestItemFlag", "", stringBuilder, 256, ConfigInfo.ConfigPath);
                testItemFlag = stringBuilder.ToString().Trim();
                return testItemFlag;
            }
        }

        public bool IsTimeOut
        {
            get { return isTimeOut; }
            set { isTimeOut = value; }
        }
        public bool FlagDisplayUart
        {
            get { return flagDisplayUart; }
            set { flagDisplayUart = value; }
        }

        public ReadWriteHandle(MainForm frmMain)
        {
            ReadWriteHandInfo.ReadConfig();
            checkDeviceInterval = ReadWriteHandInfo.CheckDeviceInterval;

            this.frmMain = frmMain;
            flagDisplayUart = true;
            //sp = SerialPortFactory.GetSerialPort();
            //sp.DataReceived += Sp_DataReceived;
            //SpOpen();
        }
         //<summary>
         //静态构造函数
         //</summary>
        static ReadWriteHandle()
        {
            atReset = new ATReadCmd(ATReadCmd.ReadIdType.Reset, "Reset System\r\n", "Get RST command");
            //atIccidRead = new ATReadCmd(ATReadCmd.ReadIdType.IccidRead, "AT+CCID\r\n", "+CCID:");
            atGPIOTest = new ATReadCmd(ATReadCmd.ReadIdType.GPIOTest, "Output Result\r\n", "PIN");
            //atEidRead = new ATReadCmd(ATReadCmd.ReadIdType.EidRead, "AT+CEID\r\n", "+CEID:");
            
            
            //atVersonRead = new ATReadCmd(ATReadCmd.ReadIdType.VersonRead, "AT+CGMR\r\n", VersonStart);

            //atImeiWrite = new ATWriteCmd(ATWriteCmd.WriteIdType.ImeiWrite, "AT+EGMR=1,7,\"", "\"\r\n");
            //atSnWrite = new ATWriteCmd(ATWriteCmd.WriteIdType.SnWrite, "AT+EGMR=1,5,\"", "\"\r\n");
        }


        public void SpOpen()
        {
            try
            {
                sp.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 串口接收处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = sp.BytesToRead;
            byte[] buf = new byte[n];
            sp.Read(buf, 0, n);
            string reciveRaw = Encoding.ASCII.GetString(buf);

            if (FlagDisplayUart)
            {
                frmMain.DisplayUart(reciveRaw);
            }
            recive += reciveRaw;
        }
        /// <summary>
        /// 检测模块上电状态
        /// </summary>
        /// <returns></returns>
        public void CheckModulePowerOn()
        {
            ////string readEid = atEidRead.Cmd;
            flagCyclic = true;
            //sp.DiscardInBuffer();
            //recive = string.Empty;

            do
            {
                ////sp.Write(readEid);
                //Console.WriteLine("正在检测上电");
                Thread.Sleep(checkDeviceInterval);
            } while (!frmMain.IsPressSpace && flagCyclic && !isTimeOut);
            //while (!recive.Contains(PoweronFlag) && flagCyclic && !isTimeOut) ;


            //do
            //{
            //    Console.WriteLine("监测到模块已上电，正在采集可用数据");
            //    Thread.Sleep(checkDeviceInterval);
            //} while (!recive.Contains("$TDINF,Techtotop Multi-GNSS Receiver") && flagCyclic && !isTimeOut);

            //return 
        }

        /// <summary>
        /// 检测模块掉电
        /// </summary>
        public void CheckModulePowerOff()
        {
            //string readEid = atEidRead.Cmd;
            flagCyclic = true;
            sp.DiscardInBuffer();
            recive = string.Empty;

            do
            {
                recive = string.Empty;
                //sp.Write(readEid);
                Console.WriteLine("正在检测掉电");
                Thread.Sleep(checkDeviceInterval);
            } while (!recive.Contains("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0") && flagCyclic);
        }


        /// <summary>
        /// 读取模块ID
        /// </summary>
        /// <param name="atReadCmd"></param>
        /// <returns></returns>
        public List<string> ReadId()
        {
            string keyWord = TestItemFlag;
            string[] spit = { "\r\n" };
            int pos;
            string[] line;
            string temp;
            //string dataValue;
            List<string> listValue = new List<string>();
            //    Console.WriteLine("监测到模块已上电，正在采集可用数据");
            do
            {
                //借助关键字符串提取出相应的ID
                line = recive.Split(spit, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < line.Length; i++)
                {
                    if ((pos = line[i].IndexOf(keyWord)) >= 0)
                    {
                        temp = line[i].Substring(pos + keyWord.Length);
                        if (temp.Length > 14)
                        {
                            //dataValue = temp;
                            listValue.Add(temp);
                        }
                        //id = System.Text.RegularExpressions.Regex.Replace(temp, @"[^0-9A-Z]", "");
                    }
                }
                Console.WriteLine("监测到模块已上电，正在采集可用数据");
                Thread.Sleep(checkDeviceInterval);
            } while (!(listValue.Count > 3) && flagCyclic && !isTimeOut);

            return listValue;

        }

        /// <summary>
        /// 读取模块ID
        /// </summary>
        /// <param name="atReadCmd"></param>
        /// <returns></returns>
        public string Read(ATReadCmd.ReadIdType readIdType)
        {
            string id = null;

            ATReadCmd atReadCmd = null;

            switch (readIdType)
            {
                case ATReadCmd.ReadIdType.Reset:
                    atReadCmd = atReset;
                    break;
                case ATReadCmd.ReadIdType.GPIOTest:
                    atReadCmd = atGPIOTest;
                    break;
                case ATReadCmd.ReadIdType.EidRead:
                    atReadCmd = atEidRead;
                    break;
                case ATReadCmd.ReadIdType.IccidRead:
                    atReadCmd = atIccidRead;
                    break;
                case ATReadCmd.ReadIdType.VersonRead:
                    atReadCmd = atVersonRead;
                    break;

                default:
                    throw new NullReferenceException("ATReadCmd.ReadIdType引用异常");
            }

            //发命令获取数据
            string readCmd = atReadCmd.Cmd;         //读取指令
            string idKeySubstr = atReadCmd.IdKeySubstr;      //提取的关键字符串
            if (ATCmdCommunication(readCmd, idKeySubstr) != 0)
            {
                return null;
            }

            //借助关键字符串提取出相应的ID
            //string idKeySubstr = atReadCmd.IdKeySubstr.ToUpper();      //提取的关键字符串
            if (readIdType == ATReadCmd.ReadIdType.VersonRead)
            {
                #region AT+CGMR\r\n后面的
                //int positon;
                //if ((positon = recive.ToUpper().LastIndexOf(idKeySubstr)) >= 0)
                //{
                //    id = recive.Substring(positon + idKeySubstr.Length);
                //    //id = recive.Substring(positon);

                //    string[] sp = { "\r\n" };
                //    id = id.Split(sp, StringSplitOptions.RemoveEmptyEntries)[0];
                //}
                #endregion

                #region 以CMIOT为关键字
                string[] spit = { "\r\n" };
                string[] line = recive.Split(spit, StringSplitOptions.RemoveEmptyEntries);
                int positon;
                for (int i = 0; i < line.Length; i++)
                {
                    if ((positon = line[i].IndexOf(idKeySubstr)) >= 0)
                    {
                        id = line[i].Substring(positon);
                        //id = System.Text.RegularExpressions.Regex.Replace(temp, @"[^0-9A-Z]", "");
                    }
                }
                #endregion
            }
            else
            {
                string[] spit = { "\r\n" };
                string[] line = recive.Split(spit, StringSplitOptions.RemoveEmptyEntries);
                int pos;

                for (int i = 0; i < line.Length; i++)
                {
                    if ((pos = line[i].IndexOf(idKeySubstr)) >= 0)
                    {
                        id = line[i].Substring(pos);
                        //string temp = line[i].Substring(pos + idKeySubstr.Length);
                        //id = System.Text.RegularExpressions.Regex.Replace(temp, @"[^0-9A-Z]", "");
                    }
                }
            }

            return id;
        }



        public void ReadRestInfo(string successFlag)
        {
            do
            {
                if (recive.Contains(successFlag))
                    {
                        break;
                    }
            } while (true);
        }

        /// <summary>
        /// AT指令通信
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private int ATCmdCommunication(string cmd, string correctInfo)
        {
            int ret = -1;
            int retry = 5;                //循环4此没收到，重发AT指令
            sp.DiscardInBuffer();
            recive = string.Empty;

            try
            {
                for (int i = 0; i < retry; i++)
                {
                    if (recive.Contains(correctInfo))
                    {
                        ret = 0;
                        break;
                    }
                    else if (recive.ToUpper().Contains("ERROR"))
                    {
                        sp.Write(cmd);
                    }
                    else
                    {
                        sp.Write(cmd);
                    }

                    Thread.Sleep(300);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return ret;
        }



    }
}
