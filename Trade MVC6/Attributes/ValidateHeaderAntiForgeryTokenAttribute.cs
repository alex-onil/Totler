using System;
using System.Diagnostics.Contracts;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Antiforgery;
using Microsoft.Extensions.OptionsModel;


namespace Trade_MVC6.Attributes
    {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateHeaderAntiForgeryTokenAttribute : ActionFilterAttribute, IAuthorizationFilter
        {
        public void OnAuthorization(AuthorizationContext filterContext)
            {
            Contract.Requires(filterContext != null);

            var httpContext = filterContext.HttpContext;
            var antiforgeryService = (IAntiforgery) httpContext.ApplicationServices.GetService(typeof(IAntiforgery));
            string requestVerification = httpContext.Request.Headers["__RequestVerificationToken"];
            string cookieToken = null;
            string formToken = null;

            if (!string.IsNullOrWhiteSpace(requestVerification))
                {
                    if (requestVerification.IndexOf(':') > -1)
                    {
                        var tokens = requestVerification.Split(':');

                        if (tokens.Length == 2)
                        {
                            cookieToken = tokens[0];
                            formToken = tokens[1];
                        }
                    }
                    else
                    {
                        var antiforgeryOptions = (IOptions<AntiforgeryOptions>)
                            httpContext.RequestServices.GetService(typeof (IOptions<AntiforgeryOptions>)) ;
                        var cookieName = antiforgeryOptions.Value.CookieName;

                        formToken = requestVerification;
                        cookieToken = httpContext.Request.Cookies[cookieName];
                    }
                }
            var afTokenSet = new AntiforgeryTokenSet(formToken, cookieToken);

            antiforgeryService.ValidateTokens(httpContext, afTokenSet);

            }
        }
    }
