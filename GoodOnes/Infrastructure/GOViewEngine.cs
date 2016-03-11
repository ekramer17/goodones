using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GoodOnes.Infrastructure
{
    public class GOViewEngine : RazorViewEngine
    {
        public GOViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(new[] {
                "~/Views/{1}/Partials/{0}.cshtml",
                "~/Views/Shared/Partials/{0}.cshtml"
            }).ToArray();
        }
    }
}