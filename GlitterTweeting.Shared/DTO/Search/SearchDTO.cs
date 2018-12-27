using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO.Search
{
    public class SearchDTO
    {

        public string SearchString { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid TweetID { get; set; }
        public Guid UserId { get; set; }
        public bool isLiked { get; set; }
        public bool isAuthor { get; set; }
        public bool isFollowed { get; set; }
        public Guid UserID { get; set; }
    }
}
