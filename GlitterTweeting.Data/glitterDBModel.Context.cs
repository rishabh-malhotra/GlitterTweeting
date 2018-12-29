﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GlitterTweeting.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class glitterEntities : DbContext
    {
        public glitterEntities()
            : base("name=glitterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<LikeTweet> LikeTweet { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Tweet> Tweet { get; set; }
        public virtual DbSet<User> User { get; set; }
    
        public virtual int test1(string firstName, string lastName, string passwordHash, string email, string phoneNumber, string image, string country)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var passwordHashParameter = passwordHash != null ?
                new ObjectParameter("PasswordHash", passwordHash) :
                new ObjectParameter("PasswordHash", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var imageParameter = image != null ?
                new ObjectParameter("Image", image) :
                new ObjectParameter("Image", typeof(string));
    
            var countryParameter = country != null ?
                new ObjectParameter("Country", country) :
                new ObjectParameter("Country", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("test1", firstNameParameter, lastNameParameter, passwordHashParameter, emailParameter, phoneNumberParameter, imageParameter, countryParameter);
        }
    }
}