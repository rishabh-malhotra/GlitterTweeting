using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlitterTweeting.Shared.DTO.Search;

namespace GlitterTweeting.Business.Business_Objects
{
    public class SearchBusinessContext
    {
        private SearchDBContext searchDBContext;
        public SearchBusinessContext()
        {
            searchDBContext = new SearchDBContext();
        }
        public IList<SearchDTO> SearchAllUsers(string searchString)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllUsers(searchString);
            return getAllResults;
        }

    }
}
