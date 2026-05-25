using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Schedule_Manager
{
    public class ScheduleEvent : INotifyPropertyChanged
    {

        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set 
            {
                if (_id != value) 
                { 
                    _id = value;
                    OnPropertyChanged();
                } 
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            { 
                if (_title != value)
                { 
                    _title = value;
                    OnPropertyChanged();
                } 
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set 
            { 
                if (_description != value)
                { 
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set 
            { 
                if (_startTime != value)
                { 
                    _startTime = value; 
                    OnPropertyChanged(); 
                } 
            }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set 
            { 
                if (_endTime != value) 
                { 
                    _endTime = value; 
                    OnPropertyChanged(); 
                } 
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set 
            { 
                if (_isCompleted != value)
                { 
                    _isCompleted = value;
                    OnPropertyChanged(); 
                } 
            }
        }

        private EventOptions _options;
        public EventOptions Options
        {
            get { return _options; }
            set 
            { 
                if (_options != value)
                { 
                    _options = value; 
                    OnPropertyChanged();
                } 
            }
        }

        private Objective _parentObjective;
        public Objective ParentObjective
        {
            get { return _parentObjective; }
            set 
            {
                if (_parentObjective != value) 
                { 
                    _parentObjective = value;
                    OnPropertyChanged(); 
                } 
            }
        }

        public DateTime ZiuaEvenimentului => StartTime.Date;

        public ScheduleEvent(string title, string description, DateTime startTime, DateTime endTime, EventOptions options, bool state, Objective parentObjective = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            IsCompleted = state;

            ParentObjective = parentObjective;
            Options = options;
        }

        public ScheduleEvent(string linieFisier)
        {
            string[] date = linieFisier.Split(';');

            this.Id = Guid.Parse(date[0]);
            this.Title = date[1];
            this.Description = date[2];
            this.StartTime = DateTime.Parse(date[3]);
            this.EndTime = DateTime.Parse(date[4]);
            this.IsCompleted = bool.Parse(date[5]);

            string textObiectiv = date[6];

            if (!string.IsNullOrEmpty(textObiectiv) && textObiectiv != "NULL")
            {
                string[] dateObj = textObiectiv.Split('|');

                Guid objId = Guid.Parse(dateObj[0]);
                string objTitle = dateObj[1];
                string objCategory = dateObj[2];
                string objDesc = dateObj[3];
                bool objIsCompleted = bool.Parse(dateObj[4]);
                Priority objPrio = (Priority)int.Parse(dateObj[5]);

                ParentObjective = new Objective(objId, objTitle, objCategory, objDesc, objIsCompleted, objPrio);
            }
            else
            {
                ParentObjective = null;
            }

            this.Options = (EventOptions)Enum.Parse(typeof(EventOptions), date[7]);
        }

        public string ConversieLaSirPentruFisier()
        {
            string dateObiectiv = string.Empty;
            if (ParentObjective != null)
                dateObiectiv = $"{ParentObjective.Id}|{ParentObjective.Title}|{ParentObjective.Category}|{ParentObjective.Description}|{ParentObjective.IsCompleted}|{(int)ParentObjective.PrioritateObiectiv}";

            return $"{Id};{Title};{Description};{StartTime};{EndTime};{IsCompleted};{dateObiectiv};{Options}";
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
