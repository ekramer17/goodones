using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.Models.Shared;
using GoodOnes.Entities;

namespace GoodOnes.Models.Guests
{
    public class PartyViewModel
    {
        public Event Party { get; set; }
        public Calendar Calendar { get; set; }
        
        public string DayOfWeek
        {
            get
            {
                if (Party == null)
                    return String.Empty;
                else
                    return DateTime.Parse(Party.Date).DayOfWeek.ToString();
            }
        }
    }
}