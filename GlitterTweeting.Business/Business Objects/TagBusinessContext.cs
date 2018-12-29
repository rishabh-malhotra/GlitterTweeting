using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Data.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Business_Objects
{
    public class TagBusinessContext
    {
        public bool CreateNewTags(NewTweetDTO newtweetdto)
        {
            string[] result = newtweetdto.Message.Split(' ');
            List<string> tagElements = new List<string>();
            foreach (string s in result)
            {

                if (s.Contains('#'))
                {
                    tagElements.Add(s);
                }
            }

            using (TagDBContext tagDBContext = new TagDBContext())
            {
                bool res = tagDBContext.AddTags(tagElements, newtweetdto.TweetID);
                return true;


            }

        }
    }
}
