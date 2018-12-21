using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Exceptions
{
    public class DoesNotExistsException:Exception
    {
        public DoesNotExistsException() { }
        public DoesNotExistsException(string message) : base(message) { }
        public DoesNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
