using AutoMapper;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.DB_Context
{
    public class SearchDBContext
    {
        glitterEntities DBContext;
        IMapper userMapper;
        public SearchDBContext()
        {
            DBContext = new glitterEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserBasicDTO>();
            });
            userMapper = new Mapper(config);
        }

        public IList<UserBasicDTO> GetAllUsers(UserBasicDTO username)
        {
            IList<User> user = DBContext.User.Where(ds => ds.FirstName.Contains(username.UserName) || ds.LastName.Contains(username.UserName)).ToList();

            IList<UserBasicDTO> userdto = userMapper.Map<IList<User>, IList<UserBasicDTO>>(user);

            return userdto;
        }
    }
}
