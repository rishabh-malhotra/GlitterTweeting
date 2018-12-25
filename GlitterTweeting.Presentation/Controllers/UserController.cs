using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.User;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using GlitterTweeting.Data.DB_Context;
using System.Collections.Generic;
using GlitterTweeting.Shared.DTO.Relationship;

namespace GlitterTweeting.Presentation.Controllers
{

    public class UserController : ApiController
    {
        private UserBusinessContext UserBusinessContext;
        IMapper UserMapper;
        ModelFactory ModelFactory;

        /// <summary>
        /// Constructor, initializes user business objects, automappers and ModelFactory.
        /// </summary>
        public UserController()
        {
            UserBusinessContext = new UserBusinessContext();

            var userMappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterModel, UserRegisterDTO>();
            });
            UserMapper = new Mapper(userMappingConfig);

            ModelFactory = new ModelFactory();
        }


        /// <summary>
        /// login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<IHttpActionResult> Post([FromBody] UserLoginModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid passed data");
                }

                if (!ModelState.IsValid)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(string.Join(" | ", ModelState.Values))));

                }
                UserLoginDTO useLoginDTO = UserMapper.Map<UserLoginModel, UserLoginDTO>(user);
                UserCompleteDTO loginUser = await UserBusinessContext.LoginUserCheck(useLoginDTO);
                //HttpContext.Current.Session["ProfileImage"] = loginUser.Image;
                //var Image = HttpContext.Current.Session["ProfileImage"];
                //HttpContext.Current.Session["UserID"] = loginUser.ID;
                //HttpContext.Current.Session["FirstName"] = loginUser.FirstName;
                //var Id = HttpContext.Current.Session["UserID"];
                //var UserName = HttpContext.Current.Session["FirstName"];


                return Ok(new { ID = loginUser.ID, Username = loginUser.FirstName });
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        /// <summary>
        /// register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        
        public async Task<IHttpActionResult> Post([FromBody] UserRegisterModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid passed data");
                }

                if (!ModelState.IsValid)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(string.Join(" | ", ModelState.Values))));
                                                                                                                             
                }
                UserRegisterDTO userPostDTO = UserMapper.Map<UserRegisterModel, UserRegisterDTO>(user);
                UserCompleteDTO newUser = await UserBusinessContext.CreateNewUser(userPostDTO);
                return Ok(new { User = newUser });
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(e.Message)));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/follow")]
        public bool Post(FollowModel followModel)
        {
            //fetch user to follow's userid from url and fetch  loggedin user id from session
            // string ass  = HttpContext.Current.Session["UserID"].ToString();            

            FollowDTO followdto = new FollowDTO();
            followdto.UserID = Guid.Parse(followModel.UserID);
            followdto.UserToFollowID = Guid.Parse(followModel.UserToFollowID);
            bool result = UserBusinessContext.Follow(followdto);
            return result;
            
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/unfollow")]
        public bool Unfollow(FollowModel followModel)
        {
            FollowDTO followdto = new FollowDTO();
            followdto.UserID = Guid.Parse(followModel.UserID);
            followdto.UserToFollowID = Guid.Parse(followModel.UserToFollowID);
            UserBusinessContext.UnFollow(followdto);
            return true;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("api/user/followers/{userId}")]
        public IList<UserBasicDTO> Get(string userId)
        {
           
            Guid loggedinuserid = Guid.Parse(userId);
            IList<UserBasicDTO> gd = UserBusinessContext.GetAllFollowers(loggedinuserid);

            return gd;
        }

        [Route("api/user/following/{userId}")]
        public IList<UserBasicDTO> Following(string userId)
        {
            Guid loggedinuserid = Guid.Parse(userId);
            IList<UserBasicDTO> gd = UserBusinessContext.GetAllFollowing(loggedinuserid);

            return gd;
        }

        /// <summary>
        /// Authorizes and returns basic info of the user with given ID.
        /// </summary>
        /// <returns>Basic info or error</returns>
        //[Authorize]
        //public async Task<IHttpActionResult> Get()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    Guid id = Guid.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).First());

        //    try
        //    {
        //        UserBasicDTO userInfo = await UserBusinessContext.GetUser(id);
        //        AuthorModel userBasicInfo = ModelFactory.Create(userInfo);
        //        return Ok(new { user = userBasicInfo });
        //    }
        //    catch (DoesNotExistsException ex)
        //    {
        //        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(ex.Message)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(ex.Message)));
        //    }
        //}
    }
}

