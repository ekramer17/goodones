using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.Entities;

namespace GoodOnes.Models.Shared
{
    public class Calendar
    {
        public String MinDate { get; set; }     // yyyy-MM-dd
        public String MaxDate { get; set; }     // yyyy-MM-dd
        public String Default { get; set; }     // yyyy-MM-dd
        public List<Event> Events { get; set; }
    }
}