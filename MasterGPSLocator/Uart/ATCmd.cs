using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Production.IdReadWrite.Cmd
{

    class ATCmd
    {
        protected string type;
        protected string okStr;
        protected string errorStr;

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string OkStr
        {
            get
            {
                return okStr;
            }

            set
            {
                okStr = value;
            }
        }

        public string ErrorStr
        {
            get
            {
                return errorStr;
            }

            set
            {
                errorStr = value;
            }
        }
    }


    

}
