using Schedule_Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ObjectiveManager
    {
        private List<Objective> objectives = new List<Objective>();

        public void AdaugaObiectiv(Objective obj)
        {
            objectives.Add(obj);
        }
    }
}
