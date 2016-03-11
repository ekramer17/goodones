using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.Entities;
using GoodOnes.Helpers;

namespace GoodOnes.Models.Guests
{
    public class SurveyViewModel
    {
        private string _s;

        public int PartyID { get; set; }
        public List<Question> Questions { get; set; }
        public string StripePublic { get { return _s; } }

        public SurveyViewModel()
        {
            _s = ConfigurationHelper.StripePublic;
        }
    }
}