using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.NewTweet;
using GlitterTweeting.Shared.DTO.Tweet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GlitterTweeting.Presentation.Controllers
{
    public class TweetController : ApiController
    {
        private TweetBusinessContext tweetBusinessContext;
        IMapper TweetMapper;

        public TweetController()
        {
            tweetBusinessContext = new TweetBusinessContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewTweetModel, NewTweetDTO>();
            });
            TweetMapper = new Mapper(config);
        }


        // GET: api/Tweet
        [AllowAnonymous]
        [Route("api/user/newTweet")]
        public async Task<IHttpActionResult> Post([FromBody] NewTweetModel newTweetModel)
        {
            try
            {
                NewTweetDTO newTweetDTO = new NewTweetDTO();
                newTweetDTO.UserID = Guid.Parse(newTweetModel.UserID);
                newTweetDTO.Message = newTweetModel.Message;
                newTweetDTO = await tweetBusinessContext.CreateNewTweet(newTweetDTO);
                return Ok(new { Tweet = newTweetDTO });
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/user/playground/{userId}")]
        public IList<GetAllTweetsDTO> Get(string userId )
       {
            Guid userid = Guid.Parse(userId);
            IList<GetAllTweetsDTO> gd = tweetBusinessContext.GetAllTweets(userid);
            return gd;
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("api/user/deletetweet/{UserID}/{TweetID}")]
        public bool Delet(string UserID, string TweetID)
        {

            Guid uid = Guid.Parse(UserID);
            Guid tid = Guid.Parse(TweetID);

            return tweetBusinessContext.DeleteTweet(uid, tid);
        }
        [HttpPut]
        [Route("api/user/updatetweet")]
        public bool Put([FromBody] NewTweetModel model)
        {

            NewTweetDTO dto = new NewTweetDTO();
            dto.UserID = Guid.Parse(model.UserID);
            dto.TweetID = model.TweetID;
            dto.Message = model.Message;
            return tweetBusinessContext.UpdateTweet(dto);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/like")]
        public bool Post(LikeTweetModel likeTweetModel)
        {
            LikeTweetDTO liketweetdto = new LikeTweetDTO();
            liketweetdto.LoggedInUserID = Guid.Parse(likeTweetModel.LoggedInUserID);
            liketweetdto.TweetID = Guid.Parse(likeTweetModel.TweetID);
            tweetBusinessContext.LikeTweet(liketweetdto);
            return true;
        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("api/user/dislike/{UserID}/{TweetID}")]
        public bool Delete(string UserID, string TweetID)
        {
            //fetch tweetid from url and fetch user id from session
            // string ass  = HttpContext.Current.Session["UserID"].ToString();            

            Guid userid =  Guid.Parse(UserID);
            Guid tweetid = Guid.Parse(TweetID);
            tweetBusinessContext.DisLikeTweet(userid, tweetid);
            return true;
        }


    }
}
