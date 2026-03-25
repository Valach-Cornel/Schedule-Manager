using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule_Manager
{
    public enum Priority
    {
        Scazuta,
        Medie,
        Ridicata,
        Critica
    }

    public class Objective
    {

        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsAchieved { get; set; }
        public int PrioritateObiectiv { get; set; }

        public Objective(string title, string category, string description, Priority prioritate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Category = category;
            Description = description;
            IsAchieved = false;
            PrioritateObiectiv = (int)prioritate;
        }

        public string Info()
        {
            string status = IsAchieved ? "[X]" : "[ ]";
            return $"{status} Obiectiv [{(Priority)PrioritateObiectiv}] [{Category}] | {Title} ({Description})";
        }
    }
}
