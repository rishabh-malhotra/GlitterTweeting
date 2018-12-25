using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class NewTweetModel
    {

        public string UserID { get; set; }
        public string Message { get; set; }
        public Guid TweetID { get; set; }

    }
}