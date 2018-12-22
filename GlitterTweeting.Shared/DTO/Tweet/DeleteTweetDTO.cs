using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO.Tweet
{
    class DeleteTweetDTO
    {
        public string UserID { get; set; }
        public Guid MessageID { get; set; }

    }
}
