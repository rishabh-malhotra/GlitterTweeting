﻿using AutoMapper;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Data.DB_Context
{
  public  class UserDBContext : IDisposable
    {
        glitterEntities DBContext;
        private IMapper UserAuthMapper;
        private IMapper UserBasicMapper;

        public UserDBContext()
        {
            DBContext = new glitterEntities();

            var userAuthMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserAuthDTO>()
                   .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
            });
            UserAuthMapper = new Mapper(userAuthMapperConfig);

            var userBasicMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserBasicDTO>();
            });
            UserBasicMapper = new Mapper(userBasicMapperConfig);
        }

        /// <summary>
        /// Creates new user in the database.
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public async Task<Guid> CreateNewUser(UserRegisterDTO userInput)
        {
            User user = ObjectFactory.CreateNewUserObject(userInput);
            DBContext.User.Add(user);
            await DBContext.SaveChangesAsync();
            return user.ID;
        }
        /// <summary>
        /// Checks if username is entered is unique and not already taken
        /// </summary>
        /// <param name="userName">A string, username to check</param>
        /// <returns></returns>
        public bool UserEmailExists(string Email)
        {
            if (DBContext.User.Where(u => u.Email == Email).Any())
                return false;
            return true;
        }

        /// <summary>
        /// Check if the email entered is unique and not already taken
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailExists(string email)
        {
            if (DBContext.User.Where(u => u.Email == email).Any())
                return false;
            return true;
        }

        public UserAuthDTO GetCredentialsByEmail(string email)
        {
            User user = DBContext.User.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {

                UserAuthDTO userAuthInfo = UserAuthMapper.Map<User, UserAuthDTO>(user);
                return userAuthInfo;
            }
            return null;
        }


        /// <summary>
        /// Returns the user with the given ID.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>Basic info about the user</returns>
        async public Task<UserBasicDTO> GetUser(Guid id)
        {
            User user = await DBContext.User.FindAsync(id);
            UserBasicDTO userBasicInfo = UserBasicMapper.Map<User, UserBasicDTO>(user);
            return userBasicInfo;
        }




        async public Task<UserCompleteDTO> GetUserCompleteInfo(UserAuthDTO userAuthDTO)
        {
            User user = await DBContext.User.FindAsync(userAuthDTO.ID);
            UserCompleteDTO userCompleteDTO = new UserCompleteDTO();
            userCompleteDTO.ID = user.ID;
            userCompleteDTO.FirstName = user.FirstName;
            userCompleteDTO.LastName = user.LastName;
            userCompleteDTO.PhoneNumber = user.PhoneNumber;
            userCompleteDTO.Country = user.Country;
            userCompleteDTO.Email = user.Email;
            userCompleteDTO.Image = user.Image;
            return userCompleteDTO;
        }

        /// <summary>
        /// Checks if a user with given ID exists in the database. If it exists, true is returned, else false is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async public Task<bool> UserExists(Guid id)
        {
            User user = await DBContext.User.FindAsync(id);
            if (user != null)
            {
                return true;
            }
            return false;
        }



        public bool UnFollow(Guid loggedinuserid, Guid usertounfollow)
        {
            Follow unfollow = DBContext.Follow.Where(ds => ds.Followed_UserID == usertounfollow).FirstOrDefault();
            DBContext.Follow.Remove(unfollow);
            DBContext.SaveChanges();
            return true;
        }

        public IList<UserBasicDTO> GetAllFollowers(Guid userloggedinid)
        {
            IList<UserBasicDTO> followersList = new List<UserBasicDTO>();
            UserBasicDTO followers;
            User user;
            IEnumerable<Follow> followeduser = DBContext.Follow.Where(ds => ds.Follower_UserID == userloggedinid);

            var i = 0;
            foreach (var item in followeduser)
            {
                followers = new UserBasicDTO();
                user = new User();
                Follow Followers = DBContext.Follow.Where(de => de.Followed_UserID == item.Followed_UserID).FirstOrDefault();

                user = DBContext.User.Where(dr => dr.ID == Followers.Followed_UserID).FirstOrDefault();
                followers.Email = user.Email;
                followers.FirstName = user.FirstName;
                followers.LastName = user.LastName;
                followers.Image = user.Image;
                followers.Count = i + 1;
                followersList.Add(followers);
                i++;
            }
            return followersList;
        }

        public bool Follow(Guid loggedinuserid, Guid usertofollow)
        {
            Follow follow1 = DBContext.Follow.Where(ds => ds.Followed_UserID == usertofollow).FirstOrDefault();
            if (follow1 != null)
            {
                return false;
            }

            else
            {
                Follow follow = new Follow();
                follow.ID = System.Guid.NewGuid();
                follow.Follower_UserID = loggedinuserid;
                follow.Followed_UserID = usertofollow;
                DBContext.Follow.Add(follow);
                DBContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Dispose function to clean up
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// virtual dispose to class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                }
            }
        }




        /// <summary>
        /// Destructor to class
        /// </summary>
        ~UserDBContext()
        {
            Dispose(false);
        }





    }
}
