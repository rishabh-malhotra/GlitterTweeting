using GlitterTweeting.Presentation.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace GlitterTweeting.Presentation
{

    public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

                var provider = new AuthorizationServiceProvider();
                OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/api/login"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                    Provider = provider
                };

                app.UseOAuthAuthorizationServer(options);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    AuthenticationType = "Bearer"
                });

                HttpConfiguration config = new HttpConfiguration();
                WebApiConfig.Register(config);
            }
        }
}