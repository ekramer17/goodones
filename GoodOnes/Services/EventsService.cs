using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.DAL.Abstract;
using GoodOnes.Entities;
using GoodOnes.Models.Shared;

namespace GoodOnes.Services
{
    public class EventsService
    {
        IEventRepository repo;

        public EventsService(IEventRepository repository)
        {
            repo = repository;
        }

        public Event GetById(int id)
        {
            return repo.GetById(id);
        }

        public Event GetByDate(string date)
        {
            return repo.GetByDate(date);
        }

        public Event GetNextEvent(DateTime startdate)
        {
            return repo.GetNext(startdate);
        }

        public Calendar GetEventsCalendar(Event e, int months)
        {
            DateTime defaultdate = e != null ? DateTime.Parse(e.Date) : DateTime.Today;

            DateTime today = DateTime.Today;
            DateTime mindate = new DateTime(today.Year, today.Month, 1);

            DateTime maxdate = mindate.AddMonths(months);
            maxdate = new DateTime(maxdate.Year, maxdate.Month, 
                DateTime.DaysInMonth(maxdate.Year, maxdate.Month));

            if (e != null)
            {
                DateTime d = defaultdate.AddMonths(months);
                d = new DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month));
                if (d > maxdate) maxdate = d;
            }

            Calendar c = new Calendar
            {
                MinDate = mindate.ToString("yyyy-MM-dd"),
                MaxDate = maxdate.ToString("yyyy-MM-dd"),
                Default = defaultdate.ToString("yyyy-MM-dd")
            };

            c.Events = repo.GetCalendarEvents(c).ToList();
            return c;
        }

        public void MakeReservation(int personID, int eventID)
        {
            repo.MakeReservation(personID, eventID);
        }

        public bool ReservationExists(int personID, int eventID)
        {
            return repo.ReservationExists(personID, eventID);
        }
    }
}