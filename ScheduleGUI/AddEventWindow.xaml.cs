using DataAccess;
using Models.Enums;
using Schedule_Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScheduleGUI
{
    public partial class AddEventWindow : Window
    {
        private IStocareData manager;
        private ObjectiveManager managerObiective;
        private ScheduleEvent evenimentCurent;
        List<EventOptions> optiuniSelectate;

        private string titluOriginal;
        private bool esteEditare;

        public AddEventWindow(ScheduleEvent ev = null)
        {
            InitializeComponent();
            manager = StocareFactory.GetManager();
            managerObiective = new ObjectiveManager();
            optiuniSelectate = new List<EventOptions>();

            var toateEvenimentele = manager.ObtineEvenimente();

            // extragem obiectele deja existente (fara duplicate)
            var obiectiveUnice = toateEvenimentele
                .Where(e => e.ParentObjective != null)
                .Select(e => e.ParentObjective)
                .GroupBy(o => o.Title.Trim().ToLower())
                .Select(g => g.First())
                .ToList();

            cmbObiective.ItemsSource = obiectiveUnice;

            if (ev != null)
            {
                evenimentCurent = ev;
                titluOriginal = ev.Title;
                esteEditare = true;
                this.Title = "Editează Evenimentul";

                dpDataInceput.SelectedDate = evenimentCurent.StartTime.Date;
                txtOraInceput.Text = evenimentCurent.StartTime.ToString("HH:mm");

                dpDataSfarsit.SelectedDate = evenimentCurent.EndTime.Date;
                txtOraSfarsit.Text = evenimentCurent.EndTime.ToString("HH:mm");

                if (evenimentCurent.ParentObjective != null)
                {
                    foreach (var obj in obiectiveUnice)
                    {
                        if (obj.Title.Trim().ToLower() == evenimentCurent.ParentObjective.Title.Trim().ToLower())
                        {
                            cmbObiective.SelectedItem = obj;
                            break;
                        }
                    }
                }
            }
            else
            {
                esteEditare = false;
                this.Title = "Gestionare Eveniment";
                evenimentCurent = new ScheduleEvent("", "", DateTime.Now, DateTime.Now.AddHours(1), EventOptions.Niciuna, false);
            }

            this.DataContext = evenimentCurent;
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            txtMesajEroare.Text = "";

            if (!ValideazaFormular(out string eroareGasita))
            {
                txtMesajEroare.Text = eroareGasita;
                return;
            }

            DateTime dataStart = dpDataInceput.SelectedDate ?? DateTime.Now.Date;
            DateTime dataEnd = dpDataSfarsit.SelectedDate ?? DateTime.Now.Date;

            TimeSpan oraStart = TimeSpan.TryParse(txtOraInceput.Text, out TimeSpan tsStart) ? tsStart : TimeSpan.Zero;
            TimeSpan oraEnd = TimeSpan.TryParse(txtOraSfarsit.Text, out TimeSpan tsEnd) ? tsEnd : TimeSpan.Zero;

            evenimentCurent.StartTime = dataStart.Add(oraStart);
            evenimentCurent.EndTime = dataEnd.Add(oraEnd);

            if (esteEditare)
                manager.StergereEveniment(titluOriginal);
            manager.AdaugaEveniment(evenimentCurent);

            this.Close();
        }

        private void BtnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValideazaFormular(out string mesajEroare)
        {
            mesajEroare = string.Empty;

            if (string.IsNullOrWhiteSpace(evenimentCurent.Title))
            {
                mesajEroare = "Te rog să introduci un titlu pentru eveniment!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(evenimentCurent.Description))
            {
                mesajEroare = "Te rog să introduci o scurtă descriere!";
                return false;
            }
            if (evenimentCurent.ParentObjective == null)
            {
                mesajEroare = "Trebuie să asociezi evenimentul cu un Obiectiv!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtOraInceput.Text) || string.IsNullOrWhiteSpace(txtOraSfarsit.Text))
            {
                mesajEroare = "Te rog să completezi ora de început și de finalizare!";
                return false;
            }

            if (!TimeSpan.TryParse(txtOraInceput.Text, out TimeSpan oraStart) ||
                !TimeSpan.TryParse(txtOraSfarsit.Text, out TimeSpan oraEnd))
            {
                mesajEroare = "Formatul orei este invalid! Folosește formatul HH:mm (ex: 14:30).";
                return false;
            }

            DateTime dataStart = dpDataInceput.SelectedDate ?? DateTime.Now.Date;
            DateTime dataEnd = dpDataSfarsit.SelectedDate ?? DateTime.Now.Date;

            DateTime startTimeComplet = dataStart.Add(oraStart);
            DateTime endTimeComplet = dataEnd.Add(oraEnd);

            if (endTimeComplet < startTimeComplet)
            {
                mesajEroare = "Data și ora de finalizare nu pot fi înaintea celor de început!";
                return false;
            }

            evenimentCurent.StartTime = startTimeComplet;
            evenimentCurent.EndTime = endTimeComplet;

            return true;
        }
    }
}
