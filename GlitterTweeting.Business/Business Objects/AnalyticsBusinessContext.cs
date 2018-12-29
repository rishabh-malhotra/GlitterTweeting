using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO;

namespace GlitterTweeting.Business.Business_Objects
{
    public class AnalyticsBusinessContext
    {
        TweetDBContext tweetDBContextObject = new TweetDBContext();
        UserDBContext UserDBContextObject = new UserDBContext();


        //getting analytics data
        public AnalyticsDTO Analytic()
        {
            AnalyticsDTO bonus = new AnalyticsDTO();
            bonus.MostTrending = tweetDBContextObject.MostTrending();
            bonus.MostLiked = tweetDBContextObject.MostLiked();
            bonus.MostTweetsBy = UserDBContextObject.MostTweetsBy();
            bonus.TotalTweetsToday = tweetDBContextObject.TotalTweetsToday();
            return bonus;
        }

    }
}
