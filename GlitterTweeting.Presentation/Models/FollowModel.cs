using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class FollowModel
    {
        public string UserID { get; set; }
        public string UserToFollowID { get; set; }
    }
}