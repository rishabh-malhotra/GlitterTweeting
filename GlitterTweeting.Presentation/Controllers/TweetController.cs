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
                NewTweetDTO newTweetDTO = TweetMapper.Map<NewTweetModel, NewTweetDTO>(newTweetModel);
                // string ass  = HttpContext.Current.Session["UserID"].ToString();            
                // newTweetDTO.UserID = Guid.Parse(ass);
                Guid abc = Guid.Parse("84559e52-6ffd-4db7-a1eb-1ca25995cee0");
                newTweetDTO.UserID = abc;
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
        [Route("api/user/playground")]
        public IList<GetAllTweetsDTO> Get()
        { Guid abc = Guid.Parse("84559e52-6ffd-4db7-a1eb-1ca25995cee0");
            IList<GetAllTweetsDTO> gd = tweetBusinessContext.GetAllTweets(abc);
            return gd;
        }

        [HttpDelete]
        //   [Route("api/user/{UserId}/{tweetid}")]
        [Route("api/user/deletetweet")]
        public bool Delete([FromBody]NewTweetModel ng)
        {

            Guid uid = ng.UserID;
            Guid tid = Guid.Parse("f08c2f05-80c6-4def-a681-66c36adb86bc");

            return tweetBusinessContext.DeleteTweet(uid, tid);
        }
        [HttpPut]
        [Route("api/user/updatetweet")]
        public bool Put([FromBody] NewTweetDTO updatedTweet)
        {
            Guid tid = Guid.Parse("34052bc5-ebd5-4a07-8eb4-6824c38cd24b");
            return tweetBusinessContext.UpdateTweet(updatedTweet, tid);

        }


    }
}
