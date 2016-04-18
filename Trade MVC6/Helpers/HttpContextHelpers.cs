using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;

namespace Trade_MVC6.Helpers
{
    public static class HttpContextHelpers
    {
        public static string RequestVerificationToken(this HttpContext context)
        {
            var antiforgeryService = (IAntiforgery) context.ApplicationServices.GetService(typeof(IAntiforgery));
            var antiforgeryTokenSet = antiforgeryService.GetTokens(context);

            // RC1 Antiforgery Bug("Not return Cookie Token")
            
            var configuration = (IOptions<AntiforgeryOptions>) context.RequestServices.GetService(typeof(IOptions<AntiforgeryOptions>));
            var cookieName = configuration.Value.CookieName;

            if (string.IsNullOrEmpty(antiforgeryTokenSet.CookieToken))
                antiforgeryTokenSet = new AntiforgeryTokenSet(antiforgeryTokenSet.FormToken,
                    context.Request.Cookies[cookieName]);

            return antiforgeryTokenSet.CookieToken + ":" + antiforgeryTokenSet.FormToken;



            }
    }
}
