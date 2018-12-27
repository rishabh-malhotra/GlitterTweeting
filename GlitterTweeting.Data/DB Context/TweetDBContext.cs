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
                IList<GetAllTweetsDTO> tweetList1 = new List<GetAllTweetsDTO>();
            tweetList = (from u in DBContext.Follow.Where(ds => ds.Follower_UserID == id)
                         join uf in DBContext.Follow on u.Followed_UserID equals uf.Followed_UserID
                         join t in DBContext.Tweet on uf.Followed_UserID equals t.UserID
                         join user in DBContext.User on t.UserID equals user.ID
                         orderby t.CreatedAt descending
                         select new GetAllTweetsDTO() {
                             MessageId=t.ID,
                             Message = t.Message,
                             CreatedAt = t.CreatedAt,
                             UserName = user.FirstName + user.LastName,
                             IsAuthor = false,
                             TweetID = t.ID
                         }).ToList();
            foreach (var iter in tweetList)
            { LikeTweet t = DBContext.LikeTweet.Where(x => (x.UserID == id) && (x.TweetID == iter.TweetID)).FirstOrDefault();
                if (t != null)
                {
                    iter.isLiked = true;
                }
                else iter.isLiked = false;
            }
        
        tweetList1 = (from u in DBContext.User.Where(tr=>tr.ID==id)
                         join t in DBContext.Tweet on u.ID equals t.UserID
                         orderby t.CreatedAt descending
                         select new GetAllTweetsDTO()
                        {   MessageId=t.ID,
                             Message = t.Message,
                             CreatedAt = t.CreatedAt,
                             UserName = u.FirstName + u.LastName,
                             IsAuthor = true,
                             isLiked = false,
                             TweetID = t.ID
                         }).ToList();
        tweetList = tweetList.Concat(tweetList1).ToList(); 
        
            return tweetList;
        }


            

        public bool DeleteTweet(Guid uid, Guid tid)
        {

            Tweet tweet = DBContext.Tweet.Where(ds => ds.ID == tid && ds.UserID == uid).FirstOrDefault();
            
            if (tweet!=null)
            {
                tagdb.DeleteTag(tweet);
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
            LikeTweet liketweet1 = DBContext.LikeTweet.Where(ds => ds.UserID == liketweetdto.LoggedInUserID && ds.TweetID == liketweetdto.TweetID).FirstOrDefault();
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
            LikeTweet tweet = DBContext.LikeTweet.Where(ds => ds.UserID == userid && ds.TweetID == tweetid).FirstOrDefault();
            if (tweet != null)
            {
                DBContext.LikeTweet.Remove(tweet);
                DBContext.SaveChanges();
                return true;
            }
            else
                return false;
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
            int count = DBContext.Tweet.Count(i => DbFunctions.TruncateTime(i.CreatedAt) == DateTime.Today);
            return count;
        }

        public string MostTrending()
        {
            IEnumerable<Tag> tagbyName = DBContext.Tag.OrderByDescending(re => re.SearchCount).ThenByDescending(re => re.TagName);
            return tagbyName.ElementAt(0).TagName;
            
        }


            public void Dispose()
            {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                }
            }
        }
        ~TweetDBContext()
        {
            Dispose(false);
        }
    }
}
