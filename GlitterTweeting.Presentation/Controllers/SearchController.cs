using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.Search;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GlitterTweeting.Presentation.Controllers
{
    public class SearchController : ApiController
    {
        private SearchBusinessContext searchBusinessContext;
        
        public SearchController()
        {
            searchBusinessContext = new SearchBusinessContext();
        }

        // GET: api/Search
        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/searchUser")]
        public IList<SearchDTO> SearchUser([FromBody] SearchModel SearchString)
            {

            SearchDTO Dto = new SearchDTO();
            Dto.UserID = Guid.Parse(SearchString.UserID);
            Dto.SearchString = SearchString.SearchString;
            IList<SearchDTO> AllResults = searchBusinessContext.SearchAllUsers(Dto.SearchString,Dto.UserID);

            return AllResults;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/searchHashTag")]
        public IList<SearchDTO> SearchTag([FromBody] SearchModel SearchString)
        {
            SearchDTO Dto = new SearchDTO();
            Dto.UserID = Guid.Parse(SearchString.UserID);
            Dto.SearchString = SearchString.SearchString;
            IList<SearchDTO> AllResults = searchBusinessContext.SearchAllHashTag(Dto.SearchString,Dto.UserID);
            return AllResults;
        }

    }
}
