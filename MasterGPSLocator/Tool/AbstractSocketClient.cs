using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace SocketHelp
{
    /// <summary>
    /// Class for communicating with the server on the device
    /// </summary>

    //class AbstractSocketClient:Socket
    //{

    class AbstractSocketClient
    {

        public Socket ClientSocket;
        public bool connected = false;
        ManualResetEvent timeoutObject = new ManualResetEvent(false);
        //static Socket ClientSocket;
        public AbstractSocketClient()
        {
            //ClientSocket.s
            //ClientSocket.sock = None;
            //ClientSocket=false;
            this.ClientSocket = null;
            this.connected = false;
        }

        //private void exit()//exc_type,exc_value,traceback
        //{
        //    ClientSocket.disconnect();
        //}

        /// <summary>
        /// ClientSocket function sets the timeout for socket operations timeout: to set for the socket
        /// </summary>
        /// <param name="timeout"></param>
        public void set_sock_timeout(int timeout)
        {
            this.ClientSocket.ReceiveTimeout = timeout;
        }
        /// <summary>
        /// his function retrieves the socket timeout
        /// </summary>
        /// <returns>current timeout</returns>
        public int get_sock_timeout()
        {
            return this.ClientSocket.ReceiveTimeout;
        }
        /// <summary>
        /// This function closes the socket if we are connected 
        /// </summary>
        public void disconnet()
        {
            if (this.connected && this.ClientSocket != null)
            {
                this.ClientSocket.Shutdown(SocketShutdown.Receive);
                this.ClientSocket.Close();
                this.connected = false;
                this.ClientSocket = null;
            }
        }

        /// <summary>
        ///  This function will try to connect to the server on the device (ip, port)
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="default_sock_timeout"></param>
        public bool connect(out string errorInfo, int timeout = 20, string host = "192.168.35.2", int port = 22222, int default_sock_timeout = 20)
        {
            //IPAddress ip = IPAddress.Parse(host);  //将IP地址字符串转换成IPAddress实例
            //this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议
            //IPEndPoint endPoint = new IPEndPoint(ip, port); // 用指定的ip和端口号初始化IPEndPoint实例
            //this.ClientSocket.Connect(endPoint);  //与远程主机建立连接
            errorInfo = "";
            if (this.connected)
            {
                this.ClientSocket.Disconnect(true);
            }
            long endtime = GetTimeStamp(true) + timeout;
            while (!this.connected && GetTimeStamp(true) < endtime)
            {
                try
                {
                    timeoutObject.Reset();
                    IPAddress ip = IPAddress.Parse(host);  //将IP地址字符串转换成IPAddress实例
                    this.ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议
                    IPEndPoint endPoint = new IPEndPoint(ip, port); // 用指定的ip和端口号初始化IPEndPoint实例
                    #region
                    //this.ClientSocket.BeginConnect(endPoint, CallBackMethod, ClientSocket);
                    //if (timeoutObject.WaitOne(default_sock_timeout * 1000, false))
                    //{
                    //    this.connected = true;
                    //}
                    //else
                    //{
                    //    errorInfo = "连接超时";
                    //}
                    #endregion

                    #region
                    this.ClientSocket.Connect(endPoint);  //与远程主机建立连接
                    this.connected = true;
                    #endregion
                }
                catch (Exception ex)
                {
                    this.ClientSocket = null;
                    Thread.Sleep(1);
                    errorInfo = ex.Message;
                }
            }

            return this.connected;

        }

        private void CallBackMethod(IAsyncResult ar)
        {
           timeoutObject.Set();
        }


        /// <summary>  
        /// 获取当前时间戳  
        /// </summary>  
        /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.bool bflag = true</param>  
        /// <returns></returns>  
        public static long GetTimeStamp(bool bflag)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //string ret = string.Empty;
            long ret = 0;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);

            return ret;
        }

    }
}
