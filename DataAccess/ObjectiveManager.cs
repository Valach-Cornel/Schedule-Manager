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
            AdaugaObiectiv(new Objective(Guid.NewGuid(), "Licenta", "Facultate", "Proiect final", false, Priority.Ridicata));
            AdaugaObiectiv(new Objective(Guid.NewGuid(), "Sport", "Personal", "Sala de forta", false, Priority.Medie));
            AdaugaObiectiv(new Objective(Guid.NewGuid(), "Cumparaturi", "Casa", "Materiale renovare", false, Priority.Medie));
            AdaugaObiectiv(new Objective(Guid.NewGuid(), "Curs C#", "Educație", "Invatare WPF", false, Priority.Ridicata));
            AdaugaObiectiv(new Objective(Guid.NewGuid(), "Facturi", "Finante", "Plata utilitati", false, Priority.Scazuta));
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
