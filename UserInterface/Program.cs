using DataAccess;
using System.Security.AccessControl;
using UserInterface;

namespace Schedule_Manager
{
    internal class Program
    {

        static void Main(string[] args)
        {
            IStocareData eventsManager = StocareFactory.GetManager();
            ObjectiveManager objectiveManager = new ObjectiveManager();
            string optiune;
            do
            {
                Console.WriteLine("C. Citire eveniment");
                Console.WriteLine("A. Afisare evenimente");
                Console.WriteLine("S. Cautare eveniment dupa categorie");

                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "C":
                        CitireEveniment(eventsManager, objectiveManager);
                        break;
                    case "A":
                        AfisareEvenimente(eventsManager);
                        break;
                    case "S":
                        CautareCategorie(eventsManager);
                        break;
                    default:
                        Console.WriteLine("Optiunea aleasa nu este valida!");
                        break;
                }
            } while (optiune.ToUpper() != "X");

        }

        public static void CitireEveniment(IStocareData evManager, ObjectiveManager obManager)
        {
            Console.WriteLine("Adaugare Obiectiv");
            Console.Write("Titlu: ");
            string objTitle = Console.ReadLine();

            Console.Write("Categorie: ");
            string objCategory = Console.ReadLine();

            Console.Write("Descriere: ");
            string objDesc = Console.ReadLine();

            Console.WriteLine("Alege Prioritatea (1. Scazuta, 2. Medie, 3. Ridicata, 4. Critica): ");
            int alegerePrioritate = int.Parse(Console.ReadLine() ?? "1");

            Priority prioritateAleasa = (Priority)alegerePrioritate;

            Objective obiectivNou = new Objective(objTitle, objCategory, objDesc, prioritateAleasa);
            obManager.AdaugaObiectiv(obiectivNou);

            Console.WriteLine("Adaugare Eveniment");
            Console.Write("Titlu: ");
            string evTitle = Console.ReadLine();

            Console.Write("Descriere: ");
            string evDesc = Console.ReadLine();

            Console.Write("Data de inceput(yyyy-mm-dd hh:mm) ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Data de final(yyyy-mm-dd hh:mm) ");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            EventOptions optiuniAlese = EventOptions.Niciuna;

            Console.Write("Este acest eveniment Online? (da/nu): ");
            if (Console.ReadLine()?.ToLower() == "da")
            {
                optiuniAlese = optiuniAlese | EventOptions.Online; 
            }

            Console.Write("Vrei sa primesti Reminder? (da/nu): ");
            if (Console.ReadLine()?.ToLower() == "da")
            {
                optiuniAlese = optiuniAlese | EventOptions.Reminder; 
            }

            Console.Write("Este un eveniment Recurent? (da/nu): ");
            if (Console.ReadLine()?.ToLower() == "da")
            {
                optiuniAlese = optiuniAlese | EventOptions.Recurent;
            }

            ScheduleEvent evenimentNou = new ScheduleEvent(evTitle, evDesc, startTime, endTime, optiuniAlese, obiectivNou);
            evManager.AdaugaEveniment(evenimentNou);

            Console.WriteLine("Date au fost salvate cu succes!");
        }

        public static void AfisareEvenimente(IStocareData evManager)
        {
            foreach (var item in evManager.ObtineEvenimente())
                Console.WriteLine(item.Info());
        }

        public static void CautareCategorie(IStocareData evManager)
        {
            Console.Write("Categoria cautata: ");
            string? categorieCautata = Console.ReadLine();

            List<ScheduleEvent> listaCategorii = evManager.CautaDupaCategorie(categorieCautata);
            foreach (var item in listaCategorii)
                Console.WriteLine(item.Info());
        }
    }
}
