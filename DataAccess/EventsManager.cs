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

        public bool CompleteazaEveniment(string titluEv)
        {
            List<ScheduleEvent> events = ObtineEvenimente();
            bool isFound = false;

            foreach (var item in events)
            {
                if (item.Title.Equals(titluEv, StringComparison.OrdinalIgnoreCase))
                {
                    item.IsCompleted = true;
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }

        public bool StergereEveniment(string titluEv)
        {
            List<ScheduleEvent> events = ObtineEvenimente();

            ScheduleEvent deleteEv = events.Where(ev => ev.Title.Equals(titluEv, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (deleteEv != null)
            {
                events.Remove(deleteEv);
                return true;
            }

            return false;
        }
    }
}
