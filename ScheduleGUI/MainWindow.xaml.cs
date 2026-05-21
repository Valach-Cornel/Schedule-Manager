using DataAccess;
using Models.Enums;
using Schedule_Manager;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScheduleGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStocareData manager;
        private DispatcherTimer timerNotificari;
        private HashSet<string> alarmeDeclansate = new HashSet<string>();
        private List<ScheduleEvent> events;
        public MainWindow()
        {
            InitializeComponent();
            manager = StocareFactory.GetManager();

            events = manager.ObtineEvenimente();
            timerNotificari = new DispatcherTimer();
            timerNotificari.Interval = TimeSpan.FromSeconds(30);
            timerNotificari.Tick += TimerNotificari_Tick;
            timerNotificari.Start();

            IncarcaDate();
        }

        private void BtnDeschideAdaugare_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow fereastraNoua = new AddEventWindow();
            fereastraNoua.ShowDialog();

            IncarcaDate();
        }

        private void IncarcaDate()
        {
            events = manager.ObtineEvenimente();
            listaCarduri.ItemsSource = events;
        }

        private void TimerNotificari_Tick(object sender, EventArgs e)
        {
            if (events == null) return;

            DateTime acum = DateTime.Now;

            foreach (var ev in events)
            {
                bool areReminder = ev.Options.HasFlag(EventOptions.Reminder) || ev.Options == EventOptions.Reminder;

                if (areReminder && !ev.IsCompleted)
                {
                    if (ev.StartTime.Date == acum.Date && ev.StartTime.Hour == acum.Hour && ev.StartTime.Minute == acum.Minute)
                    {
                        string cheieAlarma = $"{ev.Title}_{ev.StartTime:yyyyMMddHHmm}";

                        if (!alarmeDeclansate.Contains(cheieAlarma))
                        {
                            alarmeDeclansate.Add(cheieAlarma);
                            System.Media.SystemSounds.Exclamation.Play();
                            MessageBox.Show($"REMINDER:\n\nEvenimentul \"{ev.Title}\" trebuie sa inceapa acum!\n\nOra inceput: {ev.StartTime:HH:mm}","Alarma Manager", MessageBoxButton.OK,MessageBoxImage.Information);
                        }
                    }
                }
            }
        }

        private void ActualizeazaListaEvenimente()
        {
            
            if (events == null) return;

            List<ScheduleEvent> evenimenteFiltrate = events;

            string textCautat = txtCauta.Text.ToLower().Trim();
            if (!string.IsNullOrEmpty(textCautat))
                evenimenteFiltrate = evenimenteFiltrate.Where(ev => ev.Title.ToLower().Contains(textCautat)).ToList();

            if (cmbFiltruStare != null && cmbFiltruStare.SelectedItem is ComboBoxItem itemStare)
            {
                string selectieStare = itemStare.Content.ToString();

                if (selectieStare == "Active / Viitoare")
                    evenimenteFiltrate = evenimenteFiltrate.Where(ev => !ev.IsCompleted).ToList();
                else if (selectieStare == "Finalizate")
                    evenimenteFiltrate = evenimenteFiltrate.Where(ev => ev.IsCompleted).ToList();
            }

            if (cmbSortare != null && cmbSortare.SelectedItem is ComboBoxItem itemSortare)
            {
                string selectieSortare = itemSortare.Content.ToString();

                if (selectieSortare == "Data (Crescator)")
                    evenimenteFiltrate = evenimenteFiltrate.OrderBy(ev => ev.StartTime).ToList();
                else if (selectieSortare == "Data (Descrescator)")
                    evenimenteFiltrate = evenimenteFiltrate.OrderByDescending(ev => ev.StartTime).ToList();
            }

            listaCarduri.ItemsSource = evenimenteFiltrate.ToList();
        }

        private void txtCauta_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualizeazaListaEvenimente();
        }
        private void cmbFiltruStare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizeazaListaEvenimente();
        }
        private void cmbSortare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizeazaListaEvenimente();
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textCautat = txtCauta.Text.ToLower();

            var evenimenteFiltrate = events.Where(ev => ev.Title.ToLower().Contains(textCautat)).ToList();

            listaCarduri.ItemsSource = evenimenteFiltrate;
        }

        private void ListaCarduri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaCarduri.SelectedItem == null)
                return;

            ScheduleEvent evenimentSelectat = listaCarduri.SelectedItem as ScheduleEvent;

            if(evenimentSelectat != null)
            {
                AddEventWindow fereastra = new AddEventWindow(evenimentSelectat);
                fereastra.ShowDialog();
                IncarcaDate();
            }

            listaCarduri.SelectedItem = null;
        }
    }
}