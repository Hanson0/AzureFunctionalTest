using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterGPSLocator.Tool
{
    class MyStopwatch
    {
        private Action<TimeSpan> DisplayTime;
        private Stopwatch st;

        public MyStopwatch(Action<TimeSpan> DisplayTime)
        {
            st = new Stopwatch();
            this.DisplayTime = DisplayTime;
        }

        public void ReStart()
        {
            st.Restart();
            //new Thread(Display) { IsBackground = true}.Start();
            ThreadPool.QueueUserWorkItem(new WaitCallback(Display));
        }
        public void Stop()
        {
            st.Stop();
        }

        private void Display(object obj)
        {
            while (st.IsRunning)
            {
                DisplayTime(st.Elapsed);
                Thread.Sleep(10);
            }
        }
        
    }
}
