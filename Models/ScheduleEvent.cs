using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using static System.Net.WebRequestMethods;

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
        private const char SEPARATOR_FISIER = ';';
        private const int ID = 0;
        private const int TITLE = 1;
        private const int DESCRIPTION = 2;
        private const int STARTTIME = 3;
        private const int ENDTIME = 4;
        private const int ISCOMPLETED = 5;
        private const int PARENTOBJECTIVE = 6;
        private const int OPTIONS = 7;

        private const int TITLE_OBJ = 0;
        private const int CATEGORY_OBJ = 1;
        private const int DESCRIPTION_OBJ = 2;
        private const int ISACHIEVED_OBJ = 3;
        private const int PRIORITATEOBIECTIV_OBJ = 4;

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

        public ScheduleEvent(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR_FISIER);

            this.Id = Guid.Parse(date[ID]);
            this.Title = date[TITLE];
            this.StartTime = DateTime.Parse(date[STARTTIME]);
            this.Description = date[DESCRIPTION];
            this.EndTime = DateTime.Parse(date[ENDTIME]);
            this.IsCompleted = bool.Parse(date[ISCOMPLETED]);

            this.Options = (EventOptions)Enum.Parse(typeof(EventOptions), date[OPTIONS]);

            string textObiectiv = date[PARENTOBJECTIVE];

            if(!string.IsNullOrEmpty(textObiectiv))
            {
                string[] dateObj = textObiectiv.Split('|');

                string objTitle = dateObj[TITLE_OBJ];
                string objCategory = dateObj[CATEGORY_OBJ];
                string objDesc = dateObj[DESCRIPTION_OBJ];
                Priority objPrio = (Priority)int.Parse(dateObj[PRIORITATEOBIECTIV_OBJ]);

                ParentObjective = new Objective(objTitle, objCategory, objDesc, objPrio);
            }else
            {
                ParentObjective = null;
            }
        }

        public string ConversieLaSirPentruFisier()
        {
            string dateObiectiv = string.Empty;
            if (ParentObjective != null)
                dateObiectiv = $"{ParentObjective.Title}|{ParentObjective.Category}|{ParentObjective.Description}|{ParentObjective.IsAchieved}|{(int)ParentObjective.PrioritateObiectiv}";

            return $"{Id}{SEPARATOR_FISIER}{Title}{SEPARATOR_FISIER}{Description}{SEPARATOR_FISIER}{StartTime}{SEPARATOR_FISIER}{EndTime}{SEPARATOR_FISIER}{IsCompleted}{SEPARATOR_FISIER}{dateObiectiv}{SEPARATOR_FISIER}{Options}";
        }
        public string Info()
        {
            string trimTitle = Title.Length > 20 ? Title.Substring(0, 17) + "..." : Title;
            string dataStart = StartTime.ToString("yyyy-MM-dd HH:mm");
            string dataEnd = EndTime.ToString("yyyy-MM-dd HH:mm"); ;
            string status = IsCompleted ? "Da [X]" : "Nu [ ]";

            string objectiveName = "---";
            if (ParentObjective != null)
            {
                string objectiveTitle = ParentObjective.Title;
                objectiveName = objectiveTitle.Length > 20 ? objectiveTitle.Substring(0, 17) + "..." : objectiveTitle;
            }

            return string.Format("| {0,-20} | {1,-16} | {2,-16} | {3,-10} | {4,-20} |", trimTitle, dataStart, dataEnd, status, objectiveName);
        }
    }
}
