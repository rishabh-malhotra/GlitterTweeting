using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class GetAllTweetsModel
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public System.DateTime CreatedAt { get; set; }
    }
}