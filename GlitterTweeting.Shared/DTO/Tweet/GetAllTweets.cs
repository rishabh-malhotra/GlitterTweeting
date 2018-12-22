using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO.Tweet
{
    public class GetAllTweetsDTO
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public System.DateTime CreatedAt { get; set; }


    }
}