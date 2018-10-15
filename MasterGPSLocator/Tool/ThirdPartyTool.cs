using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterGPSLocator.Tool
{
    class ThirdPartyTool
    {
        private string toolName;

        private string toolDirectory;
        private string cmd;


        public string ToolName
        {
            get { return toolName; }
            set { toolName = value; }
        }
        public string ToolDirectory
        {
            get { return toolDirectory; }
            set { toolDirectory = value; }
        }
        public string Cmd
        {
            get { return cmd; }
            set { cmd = value; }
        }



    }
}
