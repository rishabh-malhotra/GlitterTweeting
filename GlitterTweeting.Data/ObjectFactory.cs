using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data
{
    class ObjectFactory
    {
        public static User CreateNewUserObject(UserRegisterDTO user)
        {
            return new User
            {
                
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,                
                Image = user.Image,
                Country=user.Country,
                PasswordHash = user.Password,
                ID = Guid.NewGuid()
            };
        }
    }
}
