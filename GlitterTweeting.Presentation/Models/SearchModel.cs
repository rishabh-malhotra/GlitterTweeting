﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class SearchModel
    {
        public string SearchString { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}