using GlitterTweeting.Data.DB_Context;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business.Business_Objects
{
    public class SearchBusinessContext
    {
        private SearchDBContext searchDBContext;
        public SearchBusinessContext()
        {
            searchDBContext = new SearchDBContext();
        }
        public IList<UserBasicDTO> SearchAllUsers(UserBasicDTO username)
        {
            IList<UserBasicDTO> getAllUsers = searchDBContext.GetAllUsers(username);
            return getAllUsers;
        }

    }
}
