using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule_Manager
{
    public class Objective
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsAchieved { get; set; }


        public Objective(string title, string category, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Category = category;
            Description = description;
            IsAchieved = false;
        }

        public string Info()
        {
            string status = IsAchieved ? "[X]" : "[ ]";
            return $"{status} Obiectiv [{Category}] | {Title} ({Description})";
        }
    }
}
