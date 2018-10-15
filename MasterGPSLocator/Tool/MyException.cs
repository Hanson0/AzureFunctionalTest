using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionHelp
{
    class MyException : ApplicationException
    {

        //public MyException(){}

        public MyException(string message) : base(message) { }



        public override string Message
        {

            get
            {

                return base.Message;

            }

        }

    }

}
