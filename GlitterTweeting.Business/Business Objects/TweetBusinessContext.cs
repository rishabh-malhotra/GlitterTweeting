using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Shared.DTO.Tweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Business_Objects
{
   public class TweetBusinessContext
    {
        private TweetDBContext tweetDBContext;
        private TagBusinessContext tagBusinnessContext;
        /// <summary>
        /// Constructor, initializes DB context objects and automappers.
        /// </summary>
        public TweetBusinessContext()
        {
            tweetDBContext = new TweetDBContext();
            tagBusinnessContext = new TagBusinessContext();

        }
        public async Task<NewTweetDTO> CreateNewTweet(NewTweetDTO tweetInput)
        {
            NewTweetDTO newtweetdto = await tweetDBContext.CreateNewTweet(tweetInput);
            if (newtweetdto != null)
            {

                bool result = tagBusinnessContext.CreateNewTags(newtweetdto);
            }
            return newtweetdto;
        }
        public IList<GetAllTweetsDTO> GetAllTweets(Guid id)
        {
            IList<GetAllTweetsDTO> gdto = tweetDBContext.GetAllTweets(id);
            return gdto;
        }
        public bool DeleteTweet(Guid uid, Guid tid)
        {
            return tweetDBContext.DeleteTweet(uid, tid);
        }

        public bool UpdateTweet(NewTweetDTO newTweetDTO)
        {
            tweetDBContext.UpdateTweet(newTweetDTO);
            return true;

        }
        public bool LikeTweet(LikeTweetDTO like)
        {
            tweetDBContext.LikeTweet(like);
            return true;
        }
        public bool DisLikeTweet(Guid userid, Guid tweetid)
        {
            tweetDBContext.DisLikeTweet(userid, tweetid);
            return true;
        }
    }
}
