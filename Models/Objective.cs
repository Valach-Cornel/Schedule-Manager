using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Schedule_Manager
{
    

    public class Objective : INotifyPropertyChanged
    {

        public Guid Id { get; private set; }
        private string _title;
        public string Title
        {
            get => _title;
            set 
            {
                _title = value; 
                OnPropertyChanged(nameof(Title)); 
            }
        }
        public string Category { get; set; }
        public string Description { get; set; }
        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set 
            { 
                _isCompleted = value; 
                OnPropertyChanged(nameof(IsCompleted)); 
            }
        }
        public int PrioritateObiectiv { get; set; }

        public Objective(Guid id, string title, string category, string description, bool isCompleted, Priority prioritate)
        {
            Id = id;
            Title = title;
            Category = category;
            Description = description;
            IsCompleted = isCompleted;
            PrioritateObiectiv = (int)prioritate;
        }

        public Objective(string linieFisier)
        {
            string[] date = linieFisier.Split('^');

            Id = Guid.Parse(date[0]);
            Title = date[1];
            Category = date[2];
            Description = date[3];
            IsCompleted = bool.Parse(date[4]);
            PrioritateObiectiv = int.Parse(date[5]);
        }

        public string Info()
        {
            string status = IsCompleted ? "[X]" : "[ ]";
            return $"{status} Obiectiv [{(Priority)PrioritateObiectiv}] [{Category}] | {Title} ({Description})";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
