using MasterGPSLocator;
using Production.ProductionTest;
using Production.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Production.Result
{
    //结果判定类
    class ResultJudge
    {
        private MainForm frmMain;
        private static string folderLog = ResultInfo.FolderLog;
        private static object obj = new object();
        private static ResultJudge resultJudge;

        private string imei;
        private string sn;
        private string iccid;
        private string eid;

        public string Imei
        {
            get
            {
                return imei;
            }

            set
            {
                imei = value;
            }
        }

        public string Sn
        {
            get
            {
                return sn;
            }

            set
            {
                sn = value;
            }
        }

        public string Iccid
        {
            get
            {
                return iccid;
            }

            set
            {
                iccid = value;
            }
        }

        public string Eid
        {
            get
            {
                return eid;
            }

            set
            {
                eid = value;
            }
        }


        /// <summary>
        /// 私有构造函数
        /// </summary>
        /// <param name="frmMain"></param>
        private ResultJudge(MainForm frmMain)
        {
            this.frmMain = frmMain;
        }


        /// <summary>
        /// 获取唯一实例
        /// </summary>
        /// <param name="frmMain"></param>
        /// <returns></returns>
        public static ResultJudge GetResultJudge(MainForm frmMain)
        {
            if (resultJudge == null)
            {
                resultJudge = new ResultJudge(frmMain);
            }

            return resultJudge;
        }


        /// <summary>
        /// 测试线程 输出结果
        /// </summary>
        /// <param name="un"></param>
        /// <param name="result"></param>
        public void PutResult(string un, int result)
        {
            StringBuilder log = new StringBuilder();

            if (result == 0)
            {
                log.Append("\r\n");
                log.Append("########     ###     ######   ######\r\n");
                log.Append("##     ##   ## ##   ##    ## ##    ##\r\n");
                log.Append("##     ##  ##   ##  ##       ##\r\n");
                log.Append("########  ##     ##  ######   ######\r\n");
                log.Append("##        #########       ##       ##\r\n");
                log.Append("##        ##     ## ##    ## ##    ##\r\n");
                log.Append("##        ##     ##  ######   ######\r\n");

                frmMain.SetTextColor(EnumControlWidget.txtLog.ToString(),
                    Color.Green);

                lock (obj)
                {
                    frmMain.DisplayResultStatistics(ResultInfo.Pass = ResultInfo.Pass + 1, ResultInfo.Fail);
                }
            }
            else
            {
                log.Append("\r\n");
                log.Append( "########    ###     ####  ##\r\n");
                log.Append( "##         ## ##     ##   ##\r\n");
                log.Append( "##        ##   ##    ##   ##\r\n");
                log.Append( "######   ##     ##   ##   ##\r\n");
                log.Append( "##       #########   ##   ##\r\n");
                log.Append( "##       ##     ##   ##   ##\r\n");
                log.Append( "##       ##     ##  ####  ########\r\n");

                frmMain.SetTextColor(EnumControlWidget.txtLog.ToString(),
                    Color.Red);

                lock (obj)
                {
                    frmMain.DisplayResultStatistics(ResultInfo.Pass, ResultInfo.Fail = ResultInfo.Fail + 1);
                }
            }

            frmMain.DisplayLog(log.ToString());
            if (ProductionInfo.Type == ProductionInfo.SystemType.iMES )
            {
                string logTest = frmMain.ReadLog();
                FactoryAuto.CommonFunc.Common.WriteLogForiMes(null, null, null, null, sn, result, logTest);
            }
            else if (ProductionInfo.Type == ProductionInfo.SystemType.GSMMES)
            {
                string logTest = frmMain.ReadLog();
                FactoryAuto.CommonFunc.Common.WriteLogForiMes(null, null, null, null, sn, result, logTest);
            }
            else
            {
                //string logTest = frmMain.ReadLog();
                //FactoryAuto.CommonFunc.Common.WriteLogForiMes(imei, iccid, null, eid, sn, result, logTest);
                WritePassResult(un, result);
            }
        }



        /// <summary>
        /// 生成log文件
        /// </summary>
        /// <param name="un"></param>
        /// <param name="result"></param>
        public void WritePassResult(string un,int result)
        {
            string logFileName = null;

            if (string.IsNullOrEmpty(un))
            {
                frmMain.DisplayLog("唯一号为空，LOG文件生成异常");
                return;
            }

            if (result == 0)
            {
                logFileName = string.Format("{0}{1}_{2}_PASS.LOG", folderLog,un.ToUpper(),
                    DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            }
            else
            {
                logFileName = string.Format("{0}{1}_{2}_FAIL.LOG", folderLog, un.ToUpper(),
                    DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            }

            //若有创建LOG路径，则创建
            if (!Directory.Exists(folderLog))
            {
                Directory.CreateDirectory(folderLog);
            }

            //读取UI控件文本信息
            string log = frmMain.ReadLog();

            //将文本写入文件流，生成文件
            using (FileStream fs = new FileStream(logFileName, FileMode.OpenOrCreate))
            {
                byte[] byteWrite = Encoding.UTF8.GetBytes(log);
                fs.Write(byteWrite, 0, byteWrite.Length);
            }
        }


        ///// <summary>
        ///// 装箱信息计算
        ///// </summary>
        ///// <returns></returns>
        //private static int CalculateCasing()
        //{
        //    int ret = -1;
        //    string log = null;

        //    //装箱信息计算
        //    if (ManufatureInfo.CaseNum != 0)              //箱号为0 表示不产生箱号信息
        //    {
        //        ManufatureInfo.CaseNowNum++;
        //        if (ManufatureInfo.CaseNowNum == ManufatureInfo.CaseMaxNum)
        //        {
        //            ManufatureInfo.CaseNum++;
        //            ManufatureInfo.CaseNowNum = 0;

        //            //UpdateUI.SetText_label(EnumControlWidget.label_caseNowNum, ManufatureInfo.CaseNowNum.ToString());
        //            //UpdateUI.SetText_label(EnumControlWidget.label_caseNum, ManufatureInfo.CaseNum.ToString());
        //            //导出装箱清单
        //            List<ResponseInfo.PackageSmallInfo> packageSmallInfos = new List<ResponseInfo.PackageSmallInfo>();
        //            ResponseInfo.Firmware firmware = null;
        //            ret = HttpUtility.PackageSmallGet((ManufatureInfo.CaseNum - 1).ToString(), out packageSmallInfos, out firmware, out log);
        //            if (ret != 0)
        //            {
        //                MessageBox.Show(log);
        //                return ret;
        //            }

        //            //生成装箱信息
        //            Excel.ExcelHelper excel = Excel.ExcelHelper.GetExcelHelperInstance();
        //            excel.ExportExcelOneByOne();

        //        }
        //    }

        //    return ret;
        //}
    }

}