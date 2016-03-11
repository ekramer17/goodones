using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.Entities;
using Stripe;

namespace GoodOnes.Helpers
{
    public class StripeHelper
    {
        public static void PurchaseGuestPass(string stripeID, Event e)
        {
            var options = new StripeChargeCreateOptions();
            options.Amount = (int)(e.Price * 100);
            options.Currency = "USD";
            options.CustomerId = stripeID;
            options.Capture = true;
            options.Description = String.Format("Guess pass for {0} (ID: {1})", e.Title, e.ID);
            new StripeChargeService().Create(options);
        }

        public static string CreateCustomer(Guest guest, string token)
        {
            var options = new StripeCustomerCreateOptions();
            options.Email = guest.Email;
            options.Description = String.Format("{0} {1} ({2})", guest.FirstName, guest.LastName, guest.Email);
            options.Source = new StripeSourceOptions { TokenId = token };
            return new StripeCustomerService().Create(options).Id;
        }

        public static void UpdateCreditCard(string stripeID, string token)
        {
            var scs = new StripeCardService();
            var options = new StripeCardCreateOptions();
            options.Source = new StripeSourceOptions() { TokenId = token };
            StripeCard card = scs.Create(stripeID, options);

            foreach (StripeCard c in scs.List(stripeID))
                if (c.Id != card.Id)
                    scs.Delete(stripeID, c.Id);
        }
    }
}