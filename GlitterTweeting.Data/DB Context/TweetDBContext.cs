﻿using AutoMapper;
using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Shared.DTO.Tweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.DB_Context
{
    
        public class TweetDBContext : IDisposable
        {
            glitterEntities DBContext;
            private IMapper TweetMapper, tweetMapper;
            public TweetDBContext()
            {
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
                // Tweet newTweet = tweetMapper.Map<NewTweetDTO, Tweet>(tweetInput);
                Tweet newTweet = new Tweet();
                newTweet.ID = System.Guid.NewGuid();
                newTweet.Message = tweetInput.Message;
                newTweet.UserID = tweetInput.UserID;
                newTweet.CreatedAt = System.DateTime.Now;
                DBContext.Tweet.Add(newTweet);
                await DBContext.SaveChangesAsync();
                // NewTweetDTO newTweets = TweetMapper.Map<Tweet, NewTweetDTO>(newTweet);
                return tweetInput;
            }
            public IList<GetAllTweetsDTO> GetAllTweets(Guid id)
            {
                IList<GetAllTweetsDTO> tweetList = new List<GetAllTweetsDTO>();
                GetAllTweetsDTO getAllTweets;
                User user = DBContext.User.Where(ds => ds.ID == id).FirstOrDefault();
                var author = user.FirstName + user.LastName;
                IEnumerable<Tweet> tweet = DBContext.Tweet.Where(de => de.UserID == user.ID);
                foreach (var item in tweet)
                {
                    getAllTweets = new GetAllTweetsDTO();
                    getAllTweets.Message = item.Message;
                    getAllTweets.CreatedAt = item.CreatedAt;
                    getAllTweets.UserName = author;
                    tweetList.Add(getAllTweets);
                }
                return tweetList;
            }

        public bool DeleteTweet(Guid uid, Guid tid)
        {
            Tweet tweet = DBContext.Tweet.Where(ds => ds.ID == tid).FirstOrDefault();
            User user = DBContext.User.Where(dr => dr.ID == uid).FirstOrDefault();
            if (user.ID == tweet.UserID)
            {
                DBContext.Tweet.Remove(tweet);
                DBContext.SaveChanges();
                return true;
            }
            else { return false; }
        }

        public void UpdateTweet(NewTweetDTO updatedTweet, Guid tid)
        {
            Tweet tweet = DBContext.Tweet.Where(ds => ds.ID == tid).FirstOrDefault();
            tweet.Message = updatedTweet.Message;
            tweet.CreatedAt = System.DateTime.Now;
            DBContext.SaveChanges();
            // Tweet newTweet = tweetMapper.Map<NewTweetDTO, Tweet>(tweetInput);

        }

        public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
}