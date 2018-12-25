using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO.Tweet
{
   public class LikeTweetDTO
    {
        public Guid TweetID { get; set; }
        public Guid LoggedInUserID { get; set; }
    }
}
