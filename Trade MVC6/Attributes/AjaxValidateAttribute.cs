using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace Trade_MVC6.Attributes
    {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxValidateAttribute : ActionFilterAttribute
        {
        public bool RedirectToHome { get; set; } = false;
        public string RedirectViewPath { get; set; } = null;

        public override void OnActionExecuting(ActionExecutingContext context)
            {
            if (!context.HttpContext.Request.Headers.ContainsKey("X-Requested-With") &&
                !context.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            {
                context.Result = RedirectToHome ? (IActionResult) new RedirectToActionResult("Index", "Home", null) 
                                                : new ViewResult { ViewName = RedirectViewPath ?? "~/Views/Home/Index.cshtml" }; 
            } else
                {
                base.OnActionExecuting(context);
                }
            }
        }
    }
