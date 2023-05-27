using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.Common.Exception
{
    public class InternalException : IOException
    {
        public InternalException()
        {

        }

        public InternalException(string message) : base(message)
        {

        }
    }
}
