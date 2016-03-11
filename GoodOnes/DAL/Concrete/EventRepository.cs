using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodOnes.DAL;
using GoodOnes.Entities;
using GoodOnes.DAL.Abstract;
using GoodOnes.Models.Shared;

namespace GoodOnes.DAL.Concrete
{
    public class EventRepository : IEventRepository
    {
        private GOContext db;

        public EventRepository(GOContext dbcontext)
        {
            db = dbcontext;
        }

        public IEnumerable<Event> GetCalendarEvents(Calendar c)
        {
            return db.Events.Where(e => e.Date.CompareTo(c.MinDate) >= 0 && e.Date.CompareTo(c.MaxDate) <= 0)
                .OrderBy(e => e.Date).AsEnumerable();
        }

        public Event GetNext(DateTime startdate)
        {
            DateTime today = DateTime.Today;
            String date = (startdate < today ? today : startdate).ToString("yyyy-MM-dd");
            return db.Events.OrderBy(e => e.Date).FirstOrDefault(e => e.Date.CompareTo(date) >= 0);
        }

        public Event GetById(int id)
        {
            return db.Events.FirstOrDefault(e => e.ID == id);
        }

        public Event GetByDate(string date)
        {
            return db.Events.FirstOrDefault(e => e.Date.Equals(date));
        }

        public void MakeReservation(int personID, int eventID)
        {
            db.Reservations.Add(new Reservation
            {
                PersonID = personID,
                EventID = eventID,
                Date = DateTime.UtcNow
            });
        }

        public bool ReservationExists(int personID, int eventID)
        {
            Reservation r = db.Reservations
                .FirstOrDefault(x => x.PersonID == personID && x.EventID == eventID);
            return r != null;
        }
    }
}