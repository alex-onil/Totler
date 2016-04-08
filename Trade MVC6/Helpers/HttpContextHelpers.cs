using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Http;

namespace Trade_MVC6.Helpers
{
    public static class HttpContextHelpers
    {
        public static string RequestVerificationToken(this HttpContext context)
        {
            var antiforgeryService = (IAntiforgery) context.ApplicationServices.GetService(typeof(IAntiforgery));
            var antiforgeryTokenSet = antiforgeryService.GetTokens(context);
        
            // string tokenSet = antiforgeryTokenSet.CookieToken + ":" + antiforgeryTokenSet.FormToken;
            // var aft = string.Format("ncg-request-verification-token={0}", tokenSet);
            return antiforgeryTokenSet.FormToken;
        }
    }
}
