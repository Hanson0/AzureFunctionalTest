using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

/// <summary>
/// Author:lh
/// Date:20170519
/// Version:V1.2
/// </summary>
namespace FactoryAuto.CommonFunc
{
    class Common
    {
        private static Semaphore semaphore = new Semaphore(1, 1);

        public static int CheckMacFormat(string mac)
        {
            string format = "^[A-F0-9]{12}$";

            if (string.IsNullOrEmpty(mac))
            {
                return -1;
            }

            if (Regex.IsMatch(mac, format))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 每隔2个字符添加:,最后两个不添加
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seprator"></param>
        /// <returns></returns>
        public static string AppendSepratorColon(string source)
        {
            return Regex.Replace(source, @"(.{2}(?!$))", "$1" + ":");
        }

        /// <summary>
        /// 每隔2个字符添加空格,最后两个不添加
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string AppendSepratorSpace(string source)
        {
            return Regex.Replace(source, @"(.{2}(?!$))", "$1" + " ");
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="lpKeyName"></param>
        /// <param name="lpString"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        public static int WriteProfileString(string lpKeyName, string lpString, string lpFileName)
        {
            int ret = -1;

            if (string.IsNullOrEmpty(lpKeyName) || string.IsNullOrEmpty(lpString))
            {
                return ret;
            }

            try
            {
                string[] strContent = File.ReadAllLines(lpFileName, Encoding.Default);

                for (int i = 0; i < strContent.Length; i++)
                {
                    int index = -1;
                    string strValid = null;
                    index = strContent[i].IndexOf("//");
                    if (index == 0)
                    {
                        continue;
                    }
                    else if (index > 0)
                    {
                        strValid = strContent[i].Substring(0, index);
                    }
                    else
                    {
                        strValid = strContent[i];
                    }

                    if (string.IsNullOrEmpty(strValid))
                    {
                        continue;
                    }

                    index = strValid.IndexOf("=");

                    if (index < 0)
                    {
                        continue;
                    }

                    //StringBuilder strKeyName = new StringBuilder(strValid.Substring(0, index));

                    //strKeyName.Replace("\t", "");
                    //strKeyName.Replace(" ", "");
                    string strKeyName = strValid.Substring(0, index);
                    strKeyName = strKeyName.Trim();

                    if (strKeyName == lpKeyName)
                    {
                        string strTemp = strValid.Substring(index + 1, strValid.Length - (index + 1));
                        string strTempValue = strTemp.Trim();                    
                        if (!string.IsNullOrEmpty(strTempValue))
                        {
                            strTemp = strTemp.Replace(strTempValue, lpString);
                        }
                        else
                        {
                            strTemp = lpString + strTemp;
                        }

                        string line = strValid.Substring(0, index + 1) + strTemp;
                        index = strContent[i].IndexOf("//");
                        line = line +　strContent[i].Substring(index, strContent[i].Length - index);

                        //index = index + 1;

                        //string strHead = strValid.Substring(0, index);
                        //string strEnd = strValid.Substring(index, strValid.Length - index);
                        //bool findData = false;
                        //foreach (char charItem in strEnd)
                        //{
                        //    if (charItem == ' ' || charItem == '\t')
                        //    {
                        //        strHead = strHead + charItem.ToString();
                        //    }
                        //    else
                        //    {
                        //        if (!findData)
                        //        {
                        //            findData = true;
                        //            strHead += lpString;
                        //        }
                        //        continue;
                        //    }
                        //}

                        //if (!findData)
                        //{
                        //    findData = true;
                        //    strHead += lpString;
                        //}

                        //index = strContent[i].IndexOf("//");
                        //if (index > 0)
                        //{
                        //    strHead += strContent[i].Substring(index, strContent[i].Length - index);
                        //}

                        //if (findData)
                        //{
                        //    strContent[i] = strHead;
                        //}

                        strContent[i] = line;
                        File.WriteAllLines(lpFileName, strContent, Encoding.Default);
                        ret = 0;
                        break;
                    }
                }
            }
            catch (FileNotFoundException fne)
            {
                Console.WriteLine(fne.ToString());
                //MessageBox.Show(fne.ToString(), "错误", MessageBoxButtons.OK);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine(dnfe.ToString());
                //MessageBox.Show(dnfe.ToString(), "错误", MessageBoxButtons.OK);
            }

            return ret;
        }

        /// <summary>
        /// 读
        /// </summary>
        /// <param name="lpKeyName"></param>
        /// <param name="lpReturnedString"></param>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        public static int ReadProfileString(string lpKeyName, ref string lpReturnedString, string lpFileName)
        {
            int ret = -1;

            if (string.IsNullOrEmpty(lpKeyName))
            {
                return ret;
            }

            try
            {
                string[] strContent = File.ReadAllLines(lpFileName, Encoding.Default);

                for (int i = 0; i < strContent.Length; i++)
                {
                    int index = -1;
                    string strValid = null;
                    index = strContent[i].IndexOf("//");
                    if (index == 0)
                    {
                        continue;
                    }
                    else if (index > 0)
                    {
                        strValid = strContent[i].Substring(0, index);
                    }
                    else
                    {
                        strValid = strContent[i];
                    }

                    if (string.IsNullOrEmpty(strValid))
                    {
                        continue;
                    }

                    //index = strValid.IndexOf("=");

                    //if (index < 0)
                    //{
                    //    continue;
                    //}

                    string[] separator = new string[1] { "=" };
                    string[] strLineList = strValid.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    if (strLineList.Length >= 2)
                    {
                        if (strLineList[0].Trim() == lpKeyName)
                        {
                            lpReturnedString = strLineList[1];

                            lpReturnedString = lpReturnedString.Trim();

                            ret = 0;
                            break;
                        }
                    }
                }
            }
            catch (FileNotFoundException fne)
            {
                Console.WriteLine(fne.ToString());
                //MessageBox.Show(fne.ToString(), "错误", MessageBoxButtons.OK);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine(dnfe.ToString());
                //MessageBox.Show(dnfe.ToString(), "错误", MessageBoxButtons.OK);
            }

            return ret;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="isPassFail"></param>
        /// <param name="log"></param>
        public static void WriteLog(string sn, int isPassFail, string log)
        {
            semaphore.WaitOne();
            int retry = 100;
            do
            {
                retry--;

                string strTempName;
                Process process = Process.GetCurrentProcess();
                string path = process.MainModule.FileName;
                path = path.Substring(0, path.LastIndexOf("\\") + 1) + "log\\";
                if (isPassFail == 0)
                {
                    strTempName = path + sn +
                        "_" + DateTime.Now.ToString("yyyyMMdd") +
                        "_" + DateTime.Now.ToString("HHmmss") +
                        "_" + "PASS" + ".LOG";
                }
                else
                {
                    strTempName = path + sn +
                        "_" + DateTime.Now.ToString("yyyyMMdd") +
                        "_" + DateTime.Now.ToString("HHmmss") +
                        "_" + "FAIL" + ".LOG";
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (File.Exists(strTempName))
                {
                    Thread.Sleep(1000);
                    continue;
                }

                try
                {
                    FileStream fs = new FileStream(strTempName, FileMode.OpenOrCreate);

                    log = log.Replace("\n", "\r\n");
                    byte[] byteArray = Encoding.UTF8.GetBytes(log);
                    fs.Write(byteArray, 0, byteArray.Length);
                    fs.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                    //MessageBox.Show(e.ToString());
                }

            } while (retry > 0);
            semaphore.Release();
        }

        public static void WriteLogForiMes(string imei, string iccid, string imsi, string eid, string sn, int isPassFail, string log)
        {
            semaphore.WaitOne();
            int retry = 100;
            do
            {
                retry--;

                string strTempName;
                Process process = Process.GetCurrentProcess();
                string path = process.MainModule.FileName;
                path = path.Substring(0, path.LastIndexOf("\\") + 1) + "log\\";

                DateTime dateTime = DateTime.Now;
                string template = string.Format("01_{0}-{1}-{2}-{3}-{4}_{5}_{6}_", imei, iccid, imsi, eid, sn,
                    dateTime.ToString("yyyyMMdd"), dateTime.ToString("HHmmss"));

                string type = ".log";

                if (isPassFail == 0)
                {
                    strTempName = path + template + "pass" + type;
                }
                else
                {
                    strTempName = path + template + "fail" + type;
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (File.Exists(strTempName))
                {
                    Thread.Sleep(1000);
                    continue;
                }

                try
                {
                    FileStream fs = new FileStream(strTempName, FileMode.OpenOrCreate);

                    log = log.Replace("\n", "\r\n");
                    byte[] byteArray = Encoding.UTF8.GetBytes(log);
                    fs.Write(byteArray, 0, byteArray.Length);
                    fs.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                    //MessageBox.Show(e.ToString());
                }

            } while (retry > 0);
            semaphore.Release();
        }

        public static int WriteToTxt(string[] data)
        {
            int ret = -1;

            string fileName = "./excel/default.txt";
            string content = null;
            string temp = null;
            string[] lines = null;

            if (data == null)
            {
                return ret;
            }

            if (File.Exists(fileName))
            {
                content = File.ReadAllText(fileName);            
            }
        
            for (int i = 0; i < data.Length; i++)
            {

                if (i <= 1 && !string.IsNullOrEmpty(content))
                {
                    if (content.IndexOf(data[i]) >= 0)
                    {
                        ret = -2;
                        return ret;
                    }
                }

                if (i == data.Length - 1)
                {
                    temp += string.Format("{0}\r\n", data[i]);
                }
                else
                {
                    temp += string.Format("{0},", data[i]);
                }
            }

            if (!string.IsNullOrEmpty(content))
            {
                lines = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                ret = lines.Length + 1;
            }
            else
            {
                ret = 1;
            }     

            File.WriteAllText(fileName, content + temp);

            return ret;
        }
    }
}
