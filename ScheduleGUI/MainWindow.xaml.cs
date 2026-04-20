using DataAccess;
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

namespace ScheduleGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStocareData manager;
        public MainWindow()
        {
            InitializeComponent();

            manager = StocareFactory.GetManager();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            List<ScheduleEvent> events = manager.ObtineEvenimente();
            listaCarduri.ItemsSource = events;
        }

        private void BtnDeschideAdaugare_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow fereastraNoua = new AddEventWindow();
            fereastraNoua.ShowDialog(); // ShowDialog oprește interacțiunea cu fereastra principală până o închizi pe asta

            // După ce se închide fereastra de adăugare, reîncărcăm lista ca să apară noul card!
            IncarcaDate();
        }
    }
}