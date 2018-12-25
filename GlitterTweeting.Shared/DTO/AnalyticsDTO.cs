using GlitterTweeting.Shared.DTO.Tweet;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Shared.DTO
{
    public class AnalyticsDTO
    {
        public string MostTrending { get; set; }
        public int TotalTweetsToday { get; set; }
        public UserBasicDTO MostTweetsBy { get; set; }
        public GetAllTweetsDTO MostLiked { get; set; }
    }
}
