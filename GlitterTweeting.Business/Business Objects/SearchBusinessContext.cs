using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.Search;
using System;
using System.Collections.Generic;

namespace GlitterTweeting.Business.Business_Objects
{
    public class SearchBusinessContext
    {
        private SearchDBContext searchDBContext;
        public SearchBusinessContext()
        {
            searchDBContext = new SearchDBContext();
        }


        //search by users
        public IList<SearchDTO> SearchAllUsers(string searchString,Guid UserId)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllUsers(searchString,UserId);
            if (getAllResults != null)
            {
                return getAllResults;
            }
            else
            {
                return null;
            }

        }

        //serching by hashtags
        public IList<SearchDTO> SearchAllHashTag(string searchString,Guid UserId)
        {
            IList<SearchDTO> getAllResults = searchDBContext.GetAllHashTag(searchString,UserId);
            if (getAllResults != null)
            {
                return getAllResults;
            }
            else
            {
                return null;
            }
        }

    }
}
