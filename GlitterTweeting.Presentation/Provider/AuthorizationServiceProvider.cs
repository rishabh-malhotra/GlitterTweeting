using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Business.Exceptions;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GlitterTweeting.Presentation.Provider
{
    public class AuthorizationServiceProvider:OAuthAuthorizationServerProvider
    {
        public AuthorizationServiceProvider()
        {

        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            //using (UserBusinessContext userBusinessContext = new UserBusinessContext())
            //{
            //    try
            //    {
            //        Guid id = await userBusinessContext.LoginUserCheck(context.UserName, context.Password);
            //        identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //        identity.AddClaim(new Claim(ClaimTypes.Sid, id.ToString()));
            //        context.Validated(identity);
            //    }
            //    catch (InvalidCredentialsException e)
            //    {
            //        context.SetError("Invalid_grant", e.Message);
            //        return;
            //    }
            //    catch (Exception ex)
            //    {
            //        context.SetError("Error", "Something went wrong. Try again later.");
            //        return;
            //    }
            //};
        }
    }
}