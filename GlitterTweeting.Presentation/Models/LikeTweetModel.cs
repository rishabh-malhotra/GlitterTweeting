using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class LikeTweetModel
    {
        public string TweetID { get; set; }
        public string LoggedInUserID { get; set; }
    }
}