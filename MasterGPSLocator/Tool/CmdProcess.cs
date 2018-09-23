using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineWritingProcess
{
    class CmdProcess
    {
        private string outputString;
        /// <summary>
        /// 获取 控制台输出结果
        /// </summary>
        /// <param name="toolFolder"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public string ExeCommand(string toolFolder, string commandText)
        {
            Process p = new Process();  //创建并实例化一个操作进程的类：Process
            p.StartInfo.FileName = "cmd.exe";    //设置要启动的应用程序
            p.StartInfo.UseShellExecute = false;   //设置是否使用操作系统shell启动进程
            p.StartInfo.RedirectStandardInput = true;  //指示应用程序是否从StandardInput流中读取
            p.StartInfo.RedirectStandardOutput = true; //将应用程序的输入写入到StandardOutput流中
            p.StartInfo.RedirectStandardError = true;  //将应用程序的错误输出写入到StandarError流中
            p.StartInfo.CreateNoWindow = true;    //是否在新窗口中启动进程
            string strOutput = null;
            try
            {

                p.Start();
                //string strRootPath = "E:";
                //string path = "CD 2_工作和软件\\1_工作存档\\01长虹爱联\\必联 BL-M3438BS1\\2_相关资料\\wl_tool";
                //p.StandardInput.WriteLine(strRootPath);    //将CMD命令写入StandardInput流中
                //p.StandardInput.WriteLine(path);    //将CMD命令写入StandardInput流中


                //p.StandardInput.WriteLine("CD " + toolFolder);    //将CMD命令写入StandardInput流中

                p.StandardInput.WriteLine("CD " + "Microsoft Azure Sphere SDK");    //将CMD命令写入StandardInput流中

                p.StandardInput.WriteLine("InitializeCommandPrompt");    //将CMD命令写入StandardInput流中

                //Microsoft Azure Sphere SDK
                //if (isDelay)
                //{
                //    //timer1.Enabled = true;
                //    //timer1.Start();
                //    updateUI = delegate
                //    {


                //        label3.Visible = true;
                //    };
                //    this.Invoke(updateUI);

                //}
                //Thread.Sleep(1000);


                p.StandardInput.WriteLine(commandText);    //将CMD命令写入StandardInput流中
                p.StandardInput.WriteLine("exit");         //将 exit 命令写入StandardInput流中
                strOutput = p.StandardOutput.ReadToEnd();   //读取所有输出的流的所有字符
                p.WaitForExit();                           //无限期等待，直至进程退出
                p.Close();                                  //释放进程，关闭进程

                //Console.WriteLine(strOutput);
                //Console.ReadKey();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;

        }

        /// <summary>
        /// 通过关键字获取值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string GetValueByKeyword(string input, string keyword)
        {
            int pos;
            string moduleMac = null;

            string[] spit = { "\r\n" };
            string[] macLine;
            string[] lines = input.Split(spit, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                if ((pos = lines[i].IndexOf(keyword)) >= 0)
                {
                    macLine = lines[i].Split(':');
                    //将MAC地址间的多余字符去掉
                    moduleMac = System.Text.RegularExpressions.Regex.Replace(macLine[1], @"[^0-9A-F]", "");
                }
            }
            return moduleMac;

        }




    }
}
