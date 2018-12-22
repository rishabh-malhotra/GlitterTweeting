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
                Guid id = Guid.Parse(newTweetModel.UserID);
                newTweetDTO.UserID = id;
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
        [HttpPost]
        [Route("api/user/playground")]
        public IList<GetAllTweetsDTO> Post([FromBody]  FetchUserId id )
       {
            //new Guid(Session["UserID"]);
            //var Id = ;
            //string id1 = Id.ToString();

            //string userid = HttpContext.Current.Session["UserID"].ToString();
            Guid userId = Guid.Parse(id.UserId);
            //Guid userId = new Guid(HttpContext.Current.Session["UserID"].ToString());
            IList<GetAllTweetsDTO> gd = tweetBusinessContext.GetAllTweets(userId);
            return gd;
        }

        [HttpDelete]
        //   [Route("api/user/{UserId}/{tweetid}")]
        [Route("api/user/deletetweet")]
        public bool Delete([FromBody]DeleteTweetModel deleteTweetModel)
        {

            Guid uid = Guid.Parse(deleteTweetModel.UserID);
            Guid tid = Guid.Parse(deleteTweetModel.MessageID);

            return tweetBusinessContext.DeleteTweet(uid, tid);
        }
        [HttpPut]
        [Route("api/user/updatetweet")]
        public bool Put([FromBody] EditTweetModel updatedTweet)
        {
            
            EditTweetDTO editTweetDTO = new EditTweetDTO();
            editTweetDTO.Message = updatedTweet.Message;
            editTweetDTO.MessageID= Guid.Parse(updatedTweet.MessageID);
            editTweetDTO.UserID= Guid.Parse(updatedTweet.UserID);
            return tweetBusinessContext.UpdateTweet(editTweetDTO);

        }


    }
}
