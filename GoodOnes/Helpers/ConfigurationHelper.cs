using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodOnes.Helpers
{
    public static class ConfigurationHelper
    {
        public static string StripePrivate
        {
            get { return ConfigurationManager.AppSettings["StripePrivate"]; }
        }

        public static string StripePublic
        {
            get { return ConfigurationManager.AppSettings["StripePublic"]; }
        }
    }
}