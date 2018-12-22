using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Business.Exceptions;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

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
                HttpContext.Current.Session["UserID"] = loginUser.ID;
                HttpContext.Current.Session["FirstName"] = loginUser.FirstName;

                var Id = HttpContext.Current.Session["UserID"];
                var UserName = HttpContext.Current.Session["FirstName"];

                return Ok(new { ID = Id, Username = UserName });
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

