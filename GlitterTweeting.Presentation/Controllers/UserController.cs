using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
    }
}
