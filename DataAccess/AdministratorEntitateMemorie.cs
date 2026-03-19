using Schedule_Manager;
using System.Collections.Generic;
using System.Linq;

namespace Schedule_Manager
{
    public class AdministratorEntitateMemorie
    {
        private List<Objective> objectives = new List<Objective>();
        private List<ScheduleEvent> events = new List<ScheduleEvent>();

        public void AdaugaObiectiv(Objective obj)
        {
            objectives.Add(obj);
        }

        public void AdaugaEveniment(ScheduleEvent ev)
        {
            events.Add(ev);
        }

        public List<ScheduleEvent> ObtineEvenimente()
        {
            return events;
        }

        public ScheduleEvent CautaDupaCategorie(string categorieCautata)
        {
            return events.Where(ev => ev.ParentObjective.Category == categorieCautata).FirstOrDefault();
        }
    }
}