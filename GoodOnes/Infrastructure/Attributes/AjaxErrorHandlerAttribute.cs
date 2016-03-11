using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoodOnes.Infrastructure.Attributes
{
    public class AjaxErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public int StatusCode = 469;

        public void OnException(ExceptionContext context)
        {
            var q = context.HttpContext.Request;
            var r = context.HttpContext.Response;

            if (q.IsAjaxRequest())
            {
                context.ExceptionHandled = true;

                r.StatusCode = StatusCode;
                r.Write(context.Exception.Message);
            }
        }
    }
}