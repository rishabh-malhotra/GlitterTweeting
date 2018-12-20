using GlitterTweeting.Presentation.Models;
using GlitterTweeting.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation
{
    public class ModelFactory
    {


        public AuthorModel Create(UserBasicDTO userBasicInfo)
        {
            return new AuthorModel
            {
                Image = userBasicInfo.Image,
                Email = userBasicInfo.Email
            };
        }

    }
}