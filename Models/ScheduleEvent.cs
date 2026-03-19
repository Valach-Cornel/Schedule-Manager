using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Schedule_Manager
{

    [Flags]
    public enum EventOptions
    {
        Niciuna = 0,
        Online = 1,
        Reminder = 2,
        Recurent = 4
    }
    public class ScheduleEvent
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCompleted { get; set; }
        public Objective ParentObjective { get; set; }
        public EventOptions Options { get; set; }

        public ScheduleEvent(string title, string description, DateTime startTime, DateTime endTime, EventOptions options, Objective parentObjective = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            IsCompleted = false;

            ParentObjective = parentObjective;
            Options = options;
        }

        public string Info()
        {
            string status = IsCompleted ? "[X]" : "[ ]";

            string objectiveInfo = "";
            if (ParentObjective != null)
            {
                objectiveInfo = $" => [Legat de: {ParentObjective.Category} - {ParentObjective.Title}]";
            }

            return $"{status} {StartTime:dd.MM.yyyy HH:mm} - {EndTime:HH:mm} | {Title} ({Description}){objectiveInfo} [Optiuni: {Options}]";
        }
    }
}
