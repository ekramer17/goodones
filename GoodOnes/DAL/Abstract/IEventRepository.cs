using System;
using System.Collections.Generic;
using GoodOnes.Entities;
using GoodOnes.Models.Shared;

namespace GoodOnes.DAL.Abstract
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetCalendarEvents(Calendar c);

        Event GetNext(DateTime startdate);

        Event GetById(int id);

        Event GetByDate(string date);

        void MakeReservation(int personID, int eventID);

        bool ReservationExists(int personID, int eventID);
    }
}
