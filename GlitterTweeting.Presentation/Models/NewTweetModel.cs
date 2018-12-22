using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class NewTweetModel
    {

        public Guid UserID { get; set; }
        public string Message { get; set; }

    }
}