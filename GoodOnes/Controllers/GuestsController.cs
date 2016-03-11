using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodOnes.Models.Guests;
using GoodOnes.Models.Shared;
using GoodOnes.Services;
using GoodOnes.Entities;
using GoodOnes.Helpers;
using GoodOnes.Infrastructure.Attributes;
using Newtonsoft.Json;

namespace GoodOnes.Controllers
{
    public class GuestsController : Controller
    {
        private GuestsService guestsrv;
        private EventsService eventsrv;
        private QuestionsService questionsrv;

        public GuestsController(GuestsService gs, EventsService es, QuestionsService qs)
        {
            guestsrv = gs;
            eventsrv = es;
            questionsrv = qs;
        }

        /// <summary>
        /// Provides guest instructions
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Render the party selection page
        /// </summary>
        [HttpGet]
        public ActionResult Party(int? id)
        {
            Event party = null;

            if (id != null)
                party = eventsrv.GetById((int)id);

            if (party == null)
                party = eventsrv.GetNextEvent(DateTime.Today.AddDays(1));

            return View(new PartyViewModel
            {
                Party = party,
                Calendar = eventsrv.GetEventsCalendar(party, 2)
            });
        }

        /// <summary>
        /// Select a party to attend
        /// </summary>
        [HttpPost]
        [AjaxAntiForgery]
        [AjaxErrorHandler]
        public ActionResult Party(int id)
        {
            guestsrv.SelectEvent(id);
            return new EmptyResult();
        }

        /// <summary>
        /// Render the survey page
        /// </summary>
        [HttpGet]
        public ActionResult Survey(int? party)
        {
            if (party == null)
                return RedirectToAction("Index", "Home");
            else if (eventsrv.GetById((int)party) == null)
                return RedirectToAction("Index", "Home");

            return View(new SurveyViewModel
            {
                PartyID = (int)party,
                Questions = questionsrv.GetQuestions().ToList()
            });
        }

        /// <summary>
        /// Complete the survey, make reservation
        /// </summary>
        [HttpPost]
        [AjaxAntiForgery]
        [AjaxErrorHandler]
        public ActionResult Survey(string email, string firstName, string lastName, int party, string answers, string token)
        {
            guestsrv.CompleteSurvey(email, firstName, lastName, party, answers, token);
            return new EmptyResult();
        }

        /// <summary>
        /// Render the rules page
        /// </summary>
        [HttpGet]
        public ActionResult Rules()
        {
            return View();
        }
        //// GET: Guests/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Guests/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Guests/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Guests/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Guests/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Guests/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Guests/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
