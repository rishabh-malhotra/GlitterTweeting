using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.DB_Context
{
    public class TagDBContext : IDisposable
    {
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
                    //  newtag.Count = 1;
                    dbcontext.Tag.Add(newtag);
                    dbcontext.SaveChanges();

                }
            }
            return true;
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
