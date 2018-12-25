using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlitterTweeting.Presentation.Models
{
    public class AuthorModel
    {
        public string Email { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Count { get; set; }
        public string ID { get; set; }
    }
}