using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Business_Objects
{
    public class AnalyticsBusinessContext
    {
        // HashTags hashTag;
        //  GetTweets getTweet = new GetTweets();
        // UserOperation mostActive;
        TweetDBContext tbc = new TweetDBContext();
        UserDBContext ubc = new UserDBContext();

        public AnalyticsDTO Analytic()
        {
            AnalyticsDTO bonus = new AnalyticsDTO();
            bonus.MostTrending = tbc.MostTrending();
            bonus.MostLiked = tbc.MostLiked();
            bonus.MostTweetsBy = ubc.MostTweetsBy();
            bonus.TotalTweetsToday = tbc.TotalTweetsToday();
            return bonus;
        }

    }
}
