using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Trade_MVC6.Models
    {
    public static class DefaultBehavior
        {
        public static bool IsAjaxRequest(this HttpRequest request)
            {
            IReadableStringCollection query = request.Query;
            if (query != null)
                {
                if (query["X-Requested-With"] == "XMLHttpRequest")
                    {
                    return true;
                    }
                }

            IHeaderDictionary headers = request.Headers;
            if (headers != null)
                {
                if (headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                    return true;
                    }
                }
            return false;
            }
        }
    }

