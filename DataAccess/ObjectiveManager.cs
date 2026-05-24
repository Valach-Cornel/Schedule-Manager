using Models.Enums;
using Schedule_Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ObjectiveManager
    {
        private List<Objective> objectives = new List<Objective>();

        public ObjectiveManager()
        {
            objectives.Add(new Objective(Guid.NewGuid(), "Licență", "Facultate", "Proiect final", false, (Priority)1));
            objectives.Add(new Objective(Guid.NewGuid(), "Renovare", "Personal", "Bucătărie", false, (Priority)2));
        }
        public void AdaugaObiectiv(Objective obj)
        {
            objectives.Add(obj);
        }

        public List<Objective> ObtineObiective()
        {
            return objectives;
        }
    }
}
