using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO.Tweet
{
    public class GetAllTweetsDTO
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Guid TweetID { get; set; }
        public bool IsAuthor { get; set; }
        public bool isLiked { get; set; }

    }
}