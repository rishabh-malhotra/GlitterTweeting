using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GlitterTweeting.Data.DB_Context
{
    public class TagDBContext : IDisposable
    {
        glitterEntities dbCOntext = new glitterEntities();


        /// <summary>
        /// db context function to add tags to the db contained in a particular tweet
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="tweetid"></param>
        /// <returns></returns>
        public bool AddTags(List<string> tags, Guid tweetid)
        {
            using (glitterEntities dbcontext = new glitterEntities())
            {
                foreach (string s in tags)
                {
                    Tag newtag = new Tag();
                    newtag.ID = Guid.NewGuid();
                    newtag.TweetID = tweetid;
                    newtag.TagName = s;
                    dbcontext.Tag.Add(newtag);
                    dbcontext.SaveChanges();

                }
            }
            return true;
        }
        /// <summary>
        /// Db Context dunction to delete all tags of a particular tweet
        /// </summary>
        /// <param name="tweetId"></param>
        /// <returns></returns>
        public bool DeleteTag(Guid tweetId)
        {
            using (glitterEntities DBContext = new glitterEntities())
            {
                IList<Tag> taglist = DBContext.Tag.Where(dr => dr.TweetID == tweetId).ToList();
                if (taglist.Count > 0)
                {
                    foreach (var item in taglist)
                    {
                        DBContext.Entry(item).State = EntityState.Deleted;
                        DBContext.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
                if (dbCOntext != null)
                {
                    dbCOntext.Dispose();
                }
            }
        }

        /// <summary>
        /// Destructor to class
        /// </summary>
        ~TagDBContext()
        {
            Dispose(false);
        }
    }
}