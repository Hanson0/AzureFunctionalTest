using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Threading;
namespace SocketHelp
{
    class HeaderTestClient : AbstractSocketClient
    {
        //public static Socket ClientSocket;
        //HeaderTestClient()
        //{
        //    //=HeaderTestClient.ClientSocket
        //    //this.max_amount_of_data_allowed_to_read = 8192;
        //    //this.chunk_data_size = 1024;
        //    //this.encoding = 'ascii';

        //}

        public bool send_command_and_read_result(string cmd_line,out string retInfo,string additional_args = null)
        {
            if (additional_args != null)
            {
                cmd_line = cmd_line + " " + additional_args;
            }
            this.send_command(cmd_line);
            Thread.Sleep(1000);

            bool ret= this.read_result(out retInfo);
            return ret;
        }

        public void stop_server()
        {
            string cmd_line = "stop_server";
            this.send_command(cmd_line);
        }


        private void send_command(string cmd_line)
        {
            if (cmd_line.Length == 0)
            {
                throw new ExceptionHelp.MyException("Empty command");
            }
            if (cmd_line.Substring(cmd_line.Length - 1, 1) != "\n")
            {
                cmd_line += "\n";
            }
            byte[] byte_cmd_line;
            if (cmd_line.GetType().ToString() == "System.String")
            {
                byte_cmd_line = Encoding.ASCII.GetBytes(cmd_line);
            }
            else
            {
                throw new ExceptionHelp.MyException("Provided command is not a string type");
            }

            if (ClientSocket.Connected)
            {
                this.set_sock_timeout(30);
                this.ClientSocket.Send(byte_cmd_line);
            }
            else
            {
                throw new ExceptionHelp.MyException("Not connected to the header test server");
            }
        }
        /// <summary>
        /// 从测试应用程序中读取数据
        /// </summary>
        /// <param name="success_string">success_string:检测到该字符串时，将停止读取;我们还真</param>
        /// <param name="failure_string">failure_string:检测到这个字符串时，读取停止;我们返回假</param>
        /// <param name="timeout">返回:成功时为真，失败时为假</param>
        /// <returns></returns>
        public bool read_result(out string retInfo, string success_string = "Test succeeded", string failure_string = "Test failed", int timeout = 180)
        {
            string data = "";
            byte[] data_buffer = new byte[1024];//new byte[1024 * 1024 * 3];

            long endtime = 0;
            if (timeout != 0)
            {
                endtime = GetTimeStamp(true) + timeout;
            }
            long nowtime;
            while (!data.Contains(success_string) && !data.Contains(failure_string))
            {
                nowtime = GetTimeStamp(true);
                if (timeout != 0 && nowtime > endtime)
                {
                    throw new ExceptionHelp.MyException(string.Format("Reading test app result, timed out after {0} s",timeout));
                }
                    try
                    {
                        int len = this.ClientSocket.Receive(data_buffer);
                        //byte[] buffer = new byte[1024 * 1024 * 3];
                        //实际接收到的有效字节数
                        // int len = socketSend.Receive(buffer);
                        // if (len == 0)
                        //{
                        //   break;
                        // }     

                        if (len == 0)
                        {
                            throw new ExceptionHelp.MyException("Did the test app crash ? Or was it removed from the device?");
                        }
                        data += Encoding.ASCII.GetString(data_buffer);

                        if (data.Length >= 8192)
                        {
                            MessageBox.Show("length of data read:" + data.Length.ToString());
                            throw new ExceptionHelp.MyException("The maximum amount of data we can read at the moment is:8192");
                        }

                    }
                    catch (Exception ex)
                    {
                        //重启会导致只收到一部分数据
                        if (data.Contains("init completed"))
                        {
                            break;
                        }
                        throw;
                    }


            }

            retInfo = data;
            if (data.Contains(failure_string))
            {
                return false;
            }
            return true;

        }


    }
}
