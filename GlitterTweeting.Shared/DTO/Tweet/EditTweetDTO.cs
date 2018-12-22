using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO.Tweet
{
    public class EditTweetDTO
    {
        public Guid UserID { get; set; }
        public string Message { get; set; }
        public Guid MessageID { get; set; }
    }
}
