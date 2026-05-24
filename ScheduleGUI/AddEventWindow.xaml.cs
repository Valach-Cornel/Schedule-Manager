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

            cmbObiective.ItemsSource = managerObiective.ObtineObiective();

            if (ev != null)
            {
                evenimentCurent = ev;
                titluOriginal = ev.Title;
                esteEditare = true;
                this.Title = "Editează Evenimentul";
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
            if (string.IsNullOrWhiteSpace(evenimentCurent.Title))
            {
                MessageBox.Show("Te rog sa introduci un titlu pentru eveniment!", "Eroare Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            if (esteEditare)
                manager.StergereEveniment(titluOriginal);
            manager.AdaugaEveniment(evenimentCurent);

            this.Close();
        }

        private void Optiune_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb == null || cb.Content == null) return;

            string numeOptiune = cb.Content.ToString();

            if (Enum.TryParse(numeOptiune, out EventOptions optiuneAleasa))
            {
                if (cb.IsChecked == true)
                {
                    if (!optiuniSelectate.Contains(optiuneAleasa))
                        optiuniSelectate.Add(optiuneAleasa);
                }
                else
                    optiuniSelectate.Remove(optiuneAleasa);
            }
        }

        private void BtnAnuleaza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
