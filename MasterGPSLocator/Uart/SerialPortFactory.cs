using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Production.SerialPortNS
{
    class SerialPortFactory
    {
        private int max = SerialPortInfo.SpPortNames.Count;
        private static SerialPort sp;
        //private int spId;

             

        /// <summary>
        /// 获取唯一串口
        /// </summary>
        /// <returns></returns>
        public static SerialPort GetSerialPort()
        {
            if (sp == null)
            {
                SerialPortInfo.ReadConfig();
                sp = new SerialPort();
                sp.PortName = SerialPortInfo.SpPortNames[0];
                sp.BaudRate = SerialPortInfo.SpBaudRate;
                sp.DataBits = SerialPortInfo.SpDataBits;
                sp.Parity = SerialPortInfo.SpParity;
                sp.StopBits = SerialPortInfo.SpStopBits;
            }

            return sp;
        }

    }
}
