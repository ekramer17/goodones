using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Text;

namespace GoodOnes.Helpers
{
    public static class AntiForgeryHelper
    {
        public static MvcHtmlString GetAntiForgeryToken(this HtmlHelper html)
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return MvcHtmlString.Create(cookieToken + ":" + formToken);
        }
    }
}