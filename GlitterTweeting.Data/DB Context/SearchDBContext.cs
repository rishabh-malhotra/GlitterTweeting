using GlitterTweeting.Shared.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlitterTweeting.Data.DB_Context
{
    public class SearchDBContext : IDisposable
    {
        glitterEntities DBContext=new glitterEntities();
        TweetDBContext tbc;
        public SearchDBContext()
        {
            tbc = new TweetDBContext();
        }

        /// <summary>
        /// db context function to get all users through a search string
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<SearchDTO> GetAllUsers(string searchString, Guid UserId)
        {
           
            if (searchString != null && searchString != "")
            {

                IList<SearchDTO> resultList = new List<SearchDTO>();
                SearchDTO getAllUsers;
                IList<User> user = new List<User>();
                user = DBContext.User.Where(ds => (ds.FirstName.Contains(searchString) || ds.LastName.Contains(searchString)) && (ds.ID != UserId)).ToList();
                if (user.Count > 0)
                {
                    foreach (var item in user)
                    {
                        getAllUsers = new SearchDTO();
                        getAllUsers.Image = item.Image;
                        getAllUsers.LastName = item.LastName;
                        getAllUsers.FirstName = item.FirstName;
                        getAllUsers.Email = item.Email;
                        getAllUsers.UserId = item.ID;
                        Follow f = DBContext.Follow.Where(fo => (fo.Follower_UserID == UserId) && (fo.Followed_UserID == getAllUsers.UserId)).FirstOrDefault();
                        if (f != null)
                        {
                            getAllUsers.isFollowed = true;
                        }
                        else
                            getAllUsers.isFollowed = false;

                        resultList.Add(getAllUsers);
                    }

                    return resultList;
                }
                else
                {
                    return null;
                }
            }
            else return null;
        }



        /// <summary>
        /// db context function to get all posts if it contains a particular hashtag through a search string
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SearchDTO> GetAllHashTag(string searchString, Guid userId)
        {
            if (searchString != null && searchString != "")
            {
                
                    IList<Tag> tag = DBContext.Tag.Where(de => de.TagName.Contains(searchString)).ToList();
                    IList<SearchDTO> resultList = new List<SearchDTO>();

                    SearchDTO getAllTags;
                    if (tag.Count > 0)
                    {
                        foreach (var item in tag)
                        {
                            getAllTags = new SearchDTO();
                            tbc.updateSearchCount(item);
                            IList<Tweet> tweet = DBContext.Tweet.Where(dr => dr.ID == item.TweetID).ToList();
                            foreach (var item1 in tweet)
                            {
                                User user1 = DBContext.User.Where(dw => dw.ID == item1.UserID).FirstOrDefault();
                                getAllTags.Message = item1.Message;
                                getAllTags.CreatedAt = item1.CreatedAt;
                                getAllTags.UserName = user1.FirstName + user1.LastName;

                            }
                            resultList.Add(getAllTags);
                        }
                        return resultList;
                    }

                    else return null;
                
            }
            else return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// virtual dispose to class
        /// </summary>
        /// <param name="disposing"></param>
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

        /// <summary>
        /// Destructor to class
        /// </summary>
        ~SearchDBContext()
        {
            Dispose(false);
        }
    }
}
