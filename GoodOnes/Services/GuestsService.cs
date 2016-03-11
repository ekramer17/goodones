using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.Models;
using GoodOnes.Entities;
using GoodOnes.Helpers;
using GoodOnes.Models.Guests;
using GoodOnes.DAL.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoodOnes.Services
{
    public class GuestsService
    {
        IPersonRepository repo;
        EventsService eventsrv;

        public GuestsService(IPersonRepository r, EventsService es)
        {
            repo = r;
            eventsrv = es;
        }

        public Event SelectEvent(int eventID)
        {
            Event e = eventsrv.GetById(eventID);

            if (e == null)
                throw new ApplicationException("Event not found. Please select another event.");
            else
                return e;
        }

        public void CompleteSurvey(string email, string firstName, string lastName, int eventID, string jsonAnswers, string token)
        {
            // Get person
            Person p = repo.Get<Person>(email, Person.INCLUDES.ANSWERS);

            // If person is member, exception
            if (p != null && p is Member)
            {
                StringBuilder s = new StringBuilder();
                s.AppendFormat("Email address {0} belongs to a current GoodOnes member. ", email);
                s.Append("Please log in to the members area and reserve your spot!");
                throw new ArgumentException(s.ToString());
            }

            // Cast person as gues or create new guest
            Guest g = (p != null && p is Guest) ? (Guest)p : new Guest() { Answers = new List<Answer>() };
            g.FirstName = firstName;
            g.LastName = lastName;
            g.Email = email;

            // Insert guest if new
            if (g.ID == 0) repo.Insert(g);

            // Add / replace answers
            foreach (JObject o in JArray.Parse(jsonAnswers))
            {
                int qid = Int32.Parse((String)o["q"]);
                string text = (String)o["a"];

                Answer answer = g.Answers.FirstOrDefault(w => w.QuestionID == qid);
                if (answer != null)
                    answer.Text = text;
                else
                    g.Answers.Add(new Answer { PersonID = g.ID, QuestionID = qid, Text = text });
            }

            // Save
            repo.SaveChanges();

            // Already have reservation?
            if (eventsrv.ReservationExists(g.ID, eventID))
                throw new InvalidOperationException(String.Format("Email address {0} has already made a reservation to this event", email)); 

            // Create Stripe customer or update card
            if (g.StripeID != null)
            {
                StripeHelper.UpdateCreditCard(g.StripeID, token);
            }
            else
            {
                g.StripeID = StripeHelper.CreateCustomer(g, token);
                repo.SaveChanges();
            }

            // Make reservation
            eventsrv.MakeReservation(g.ID, eventID);

            // Charge card
            StripeHelper.PurchaseGuestPass(g.StripeID, eventsrv.GetById(eventID));

            // Save
            repo.SaveChanges();
        }
    }
}