using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterGPSLocator.Tool
{
    class StringHelp
    {
        public static string SubCentre(string input, string strSart, string strEnd)
        {
            int start = input.IndexOf(strSart) + strSart.Length;
            int end = input.LastIndexOf(strEnd);
            input = input.Substring(start, end - start);
            return input;
        }
    }
}
