using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Shared.DTO.Tweet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Business_Objects
{
    public class TweetBusinessContext
    {
        private TweetDBContext tweetDBContext;
        private TagBusinessContext tagBusinnessContext;
        private TagDBContext tagDBContext;

        /// <summary>
        /// Constructor, initializes DB context objects and automappers.
        /// </summary>
        public TweetBusinessContext()
        {
            tagDBContext = new TagDBContext();
            tweetDBContext = new TweetDBContext();
            tagBusinnessContext = new TagBusinessContext();

        }

        /// <summary>
        /// Business context function to create new tweet
        /// </summary>
        /// <param name="tweetInput"></param>
        /// <returns></returns>
        public async Task<NewTweetDTO> CreateNewTweet(NewTweetDTO tweetInput)
        {
            NewTweetDTO newtweetdto = await tweetDBContext.CreateNewTweet(tweetInput);
            if (newtweetdto != null)
            {

                bool result = tagBusinnessContext.CreateNewTags(newtweetdto);
            }
            return newtweetdto;
        }


        /// <summary>
        /// business context functions to get all tweets
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<GetAllTweetsDTO> GetAllTweets(Guid id)
        {
            IList<GetAllTweetsDTO> gdto = tweetDBContext.GetAllTweets(id);
            return gdto;
        }

        /// <summary>
        /// function to delete tweet
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public bool DeleteTweet(Guid uid, Guid tid)
        {
            return tweetDBContext.DeleteTweet(uid, tid);
        }

        /// <summary>
        /// business context function to update tweet
        /// </summary>
        /// <param name="newTweetDTO"></param>
        /// <returns></returns>
        public bool UpdateTweet(NewTweetDTO newTweetDTO)
        {
            Guid result = tweetDBContext.UpdateTweet(newTweetDTO);
            tagDBContext.DeleteTag(result);
            tagBusinnessContext.CreateNewTags(newTweetDTO);
            return true;
        }

        /// <summary>
        /// function to like a particular tweet
        /// </summary>
        /// <param name="like"></param>
        /// <returns></returns>
        public bool LikeTweet(LikeTweetDTO like)
        {
            tweetDBContext.LikeTweet(like);
            return true;
        }


        /// <summary>
        /// function to dislike a tweet if it is already being liked by the same user
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="tweetid"></param>
        /// <returns></returns>
        public bool DisLikeTweet(Guid userid, Guid tweetid)
        {
            tweetDBContext.DisLikeTweet(userid, tweetid);
            return true;
        }
    }
}
