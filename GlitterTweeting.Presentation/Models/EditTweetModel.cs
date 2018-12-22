using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class EditTweetModel
    {
        public string UserID { get; set; }
        public string Message { get; set; }
        public string MessageID { get; set; }
    }
}