using DataAccess;
using Schedule_Manager;
using System;
using System.Collections.Generic;
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
        private const int MIN_LUNGIME_TITLU = 3;
        private const int MAX_LUNGIME_TITLU = 30;
        private const int MAX_LUNGIME_DESCRIERE = 100;

        private IStocareData manager;

        public AddEventWindow()
        {
            InitializeComponent();
            manager = StocareFactory.GetManager();
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            ReseteazaCulori();
            string mesajEroare = "";
            bool dateValide = true;

            string titlu = txtTitlu.Text.Trim();
            string descriere = txtDescriere.Text.Trim();
            DateTime dataStartParsata = DateTime.Now;

            if (titlu.Length < MIN_LUNGIME_TITLU || titlu.Length > MAX_LUNGIME_TITLU)
            {
                lblTitlu.Foreground = Brushes.Red;
                mesajEroare += $"- Titlul trebuie să aibă între {MIN_LUNGIME_TITLU} și {MAX_LUNGIME_TITLU} caractere.\n";
                dateValide = false;
            }

            if (descriere.Length > MAX_LUNGIME_DESCRIERE)
            {
                lblDescriere.Foreground = Brushes.Red;
                mesajEroare += $"- Descrierea nu poate depăși {MAX_LUNGIME_DESCRIERE} caractere.\n";
                dateValide = false;
            }
            if (dpData.SelectedDate == null)
            {
                lblDataInceput.Foreground = Brushes.Red;
                mesajEroare += "- Te rog să selectezi o dată din calendar.\n";
                dateValide = false;
            }
            else
            {
                string ziSelectata = dpData.SelectedDate.Value.ToString("dd/MM/yyyy");
                string oraIntrodusa = txtOra.Text.Trim();

                string dataSiOraComplete = $"{ziSelectata} {oraIntrodusa}";

                if (!DateTime.TryParseExact(dataSiOraComplete, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataStartParsata))
                {
                    lblDataInceput.Foreground = Brushes.Red;
                    mesajEroare += "- Formatul orei este invalid. Folosește exact formatul HH:MM (ex: 14:30).\n";
                    dateValide = false;
                }
            }

            if (!dateValide)
            {
                txtErori.Text = "Erori găsite:\n" + mesajEroare;
            }
            else
            {
                DateTime dataFinal = dataStartParsata.AddHours(1);

                ScheduleEvent evenimentNou = new ScheduleEvent(titlu, descriere, dataStartParsata, dataFinal, EventOptions.Niciuna, null);

                manager.AdaugaEveniment(evenimentNou);

                this.Close();
            }
        }

        private void ReseteazaCulori()
        {
            lblTitlu.Foreground = Brushes.Black;
            lblDescriere.Foreground = Brushes.Black;
            lblDataInceput.Foreground = Brushes.Black;
            txtErori.Text = "";
        }
    }
}
