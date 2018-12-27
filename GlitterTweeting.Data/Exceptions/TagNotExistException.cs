using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.Exceptions
{
    public class TagNotExistException:Exception
    {
        public TagNotExistException()
        {

        }
        public TagNotExistException(string message) : base(message) { }
        public TagNotExistException(string message, Exception inner) : base(message, inner) { }
    }
}
