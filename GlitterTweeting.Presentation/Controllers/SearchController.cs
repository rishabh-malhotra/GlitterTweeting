using AutoMapper;
using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.Search;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GlitterTweeting.Presentation.Controllers
{
    public class SearchController : ApiController
    {
        private SearchBusinessContext searchBusinessContext;
        IMapper mapper;


        public SearchController()
        {
            searchBusinessContext = new SearchBusinessContext();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthorModel, UserBasicDTO>();
            });
            mapper = new Mapper(config);
        }

        // GET: api/Search
        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/searchUser")]
        public IList<SearchDTO> Post([FromBody] SearchModel SearchString)
        {
            SearchDTO Dto = new SearchDTO();
            Dto.SearchString = SearchString.SearchString;
            IList<SearchDTO> AllResults = searchBusinessContext.SearchAllUsers(Dto.SearchString);

            return AllResults;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/searchHashTag")]
        public IList<SearchDTO> Search([FromBody] SearchModel SearchString)
        {
            SearchDTO Dto = new SearchDTO();
            Dto.SearchString = SearchString.SearchString;
            IList<SearchDTO> AllResults = searchBusinessContext.SearchAllHashTag(Dto.SearchString);
            return AllResults;
        }

    }
}
