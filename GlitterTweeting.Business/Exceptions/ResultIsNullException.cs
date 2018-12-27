using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Exceptions
{
    class ResultIsNullException:Exception
    {
        public ResultIsNullException()
        {

        }
        public ResultIsNullException(string message) : base(message) { }
        public ResultIsNullException(string message, Exception inner) : base(message, inner) { }
    }
}
