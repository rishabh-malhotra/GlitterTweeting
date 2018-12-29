using GlitterTweeting.Business.Business_Objects;
using GlitterTweeting.Shared.DTO;
using System.Web.Http;

namespace GlitterTweeting.Presentation.Controllers
{

    public class AnalyticsController : ApiController
        {
            /// <summary>
            /// Getting the results of analytics Section
            /// </summary>
            /// <returns></returns>
            /// get
           
            [HttpGet]
            [Route("api/analytics")]
            public AnalyticsDTO Bonus()
            {
                AnalyticsBusinessContext analytics = new AnalyticsBusinessContext();
                return analytics.Analytic();
            }

        }
}
