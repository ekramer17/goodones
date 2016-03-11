using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GoodOnes.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AjaxAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpRequestBase b = filterContext.HttpContext.Request;

            try
            {
                if (b.IsAjaxRequest())
                    ValidateRequestHeader(b); // run the validation.
                else
                    AntiForgery.Validate();
            }
            catch (HttpAntiForgeryException e)
            {
                throw new HttpAntiForgeryException("Anti forgery token not found");
            }
       }

        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string formToken = String.Empty;
            string cookieToken = String.Empty;
            string tokenValue = request.Headers["RequestVerificationToken"];

            if (!String.IsNullOrEmpty(tokenValue))
            {
                string[] tokens = tokenValue.Split(':');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }

            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}