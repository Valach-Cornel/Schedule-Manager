using Schedule_Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class EventsManager : IStocareData
    {
        private List<ScheduleEvent> events = new List<ScheduleEvent>();

        public void AdaugaEveniment(ScheduleEvent ev)
        {
            events.Add(ev);
        }

        public List<ScheduleEvent> ObtineEvenimente()
        {
            return events;
        }

        public List<ScheduleEvent> CautaDupaCategorie(string categorieCautata)
        {
            return events.Where(ev => ev.ParentObjective.Category == categorieCautata).ToList();
        }

        public bool UpdateEveniment(ScheduleEvent evActualizat)
        {
            throw new Exception("Optiunea UpdateEveniment nu este implementata");
        }
    }
}
