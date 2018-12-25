using AutoMapper;
using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Shared.DTO.Tweet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.DB_Context
{
    
        public class TweetDBContext : IDisposable
        {
            glitterEntities DBContext;
            TagDBContext tagdb;
            private IMapper TweetMapper, tweetMapper;
            public TweetDBContext()
            {
                tagdb = new TagDBContext();
                DBContext = new glitterEntities();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tweet, NewTweetDTO>();

                });
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NewTweetDTO, Tweet>();

                });
                TweetMapper = new Mapper(config);
                tweetMapper = new Mapper(configuration);



            }

            public async Task<NewTweetDTO> CreateNewTweet(NewTweetDTO tweetInput)
            {
                
                Tweet newTweet = new Tweet();
                newTweet.ID = System.Guid.NewGuid();
                newTweet.Message = tweetInput.Message;
                newTweet.UserID = tweetInput.UserID;
                newTweet.CreatedAt = System.DateTime.Now;
                DBContext.Tweet.Add(newTweet);
                await DBContext.SaveChangesAsync();
                tweetInput.TweetID = newTweet.ID;
            
                return tweetInput;
            }

        public bool updateSearchCount(Tag item)
        {
            Tag updateTag = DBContext.Tag.Where(dr => dr.ID == item.ID).FirstOrDefault();
            if (updateTag.SearchCount == null)
            {
                updateTag.SearchCount = 1;
            }
            else
            {
                updateTag.SearchCount = updateTag.SearchCount + 1;
            }
            DBContext.SaveChanges();
            return true;
        }

        public IList<GetAllTweetsDTO> GetAllTweets(Guid id)
            {
                IList<GetAllTweetsDTO> tweetList = new List<GetAllTweetsDTO>();
                GetAllTweetsDTO getAllTweets;
                User user = DBContext.User.Where(ds => ds.ID == id).FirstOrDefault();
                var author = user.FirstName +" " + user.LastName;
                IEnumerable<Tweet> tweet = DBContext.Tweet.Where(de => de.UserID == user.ID).OrderByDescending(cd => cd.CreatedAt);
                foreach (var item in tweet)
                {
                    getAllTweets = new GetAllTweetsDTO();
                    getAllTweets.MessageId = item.ID;
                    getAllTweets.Message = item.Message;
                    getAllTweets.CreatedAt = item.CreatedAt;
                    getAllTweets.TweetID = item.ID;
                    getAllTweets.UserName = author;
                    tweetList.Add(getAllTweets);
                }
            IEnumerable<Follow> followers = DBContext.Follow.Where(de => de.Follower_UserID == id);

            foreach (var iter in followers)
            {
                IEnumerable<Follow> followed = DBContext.Follow.Where(de => de.Followed_UserID == iter.Followed_UserID);
                foreach (var iter2 in followed)
                {
                    IEnumerable<Tweet> msg = DBContext.Tweet.Where(df => df.UserID == iter2.Followed_UserID).OrderByDescending(cd => cd.CreatedAt);
                    foreach (var iter1 in msg)
                    {
                        User us = DBContext.User.Where(re => re.ID == iter1.UserID).FirstOrDefault();
                        getAllTweets = new GetAllTweetsDTO();
                        getAllTweets.Message = iter1.Message;
                        getAllTweets.CreatedAt = iter1.CreatedAt;
                        getAllTweets.TweetID = iter1.ID;
                        getAllTweets.UserName = us.FirstName + us.LastName;
                        tweetList.Add(getAllTweets);
                    }
                }

            }
            return tweetList;
            }

        public bool DeleteTweet(Guid uid, Guid tid)
        {
            Tweet tweet = DBContext.Tweet.Where(ds => ds.ID == tid).FirstOrDefault();
            User user = DBContext.User.Where(dr => dr.ID == uid).FirstOrDefault();
            if (user.ID == tweet.UserID)
            {
                tagdb.DeleteTag(tweet);
                // DBContext.Tweet.DeleteObject(tweet);
                DBContext.Entry(tweet).State = EntityState.Deleted;
                DBContext.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public void UpdateTweet(NewTweetDTO updatedTweet)
        {
            Tweet tweet = DBContext.Tweet.Where(ds => ds.ID == updatedTweet.TweetID).FirstOrDefault();
            tweet.Message = updatedTweet.Message;
            tweet.CreatedAt = System.DateTime.Now;
            DBContext.SaveChanges();
            // Tweet newTweet = tweetMapper.Map<NewTweetDTO, Tweet>(tweetInput);

        }
        public bool LikeTweet(LikeTweetDTO liketweetdto)
        {
            LikeTweet liketweet1 = DBContext.LikeTweet.Where(ds => ds.UserID == liketweetdto.LoggedInUserID).FirstOrDefault();
            if (liketweet1 != null)
            {
                return false;
            }

            else
            {
                LikeTweet liketweet = new LikeTweet();
               
                liketweet.ID = System.Guid.NewGuid();
                liketweet.TweetID = liketweetdto.TweetID;
                liketweet.UserID = liketweetdto.LoggedInUserID;
                DBContext.LikeTweet.Add(liketweet);
                DBContext.SaveChanges();
                return true;
            }

        }
        public bool DisLikeTweet(Guid userid, Guid tweetid)
        {
            LikeTweet tweet = DBContext.LikeTweet.Where(ds => ds.UserID == userid).FirstOrDefault();
            DBContext.LikeTweet.Remove(tweet);
            DBContext.SaveChanges();
            return true;
        }
        public string MostLiked()
        {

            Guid maxid = DBContext.LikeTweet.GroupBy(x => x.TweetID).OrderByDescending(x => x.Count()).First().Key;
            Tweet t = DBContext.Tweet.Where(ds => ds.ID == maxid).FirstOrDefault();
            return t.Message;
        }

        public int TotalTweetsToday()
        {
            DateTime sysDate = DateTime.Today;
            int count = DBContext.Tweet.Count(i => DbFunctions.TruncateTime(i.CreatedAt) == System.DateTime.Today);

            // int count = DBContext.Tweet.Where(x => DbFuntions.TruncateTime(x.CreatedAt) == DateTime.Today).Count();
            return count;
        }

        public string MostTrending()
        {
            IEnumerable<Tag> tagbyName = DBContext.Tag.OrderByDescending(re => re.SearchCount).ThenByDescending(re => re.TagName);
            return tagbyName.ElementAt(0).TagName;
        }


            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
}
