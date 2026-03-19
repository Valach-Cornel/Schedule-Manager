namespace Schedule_Manager
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<Objective> objectives = new List<Objective>();
            List<ScheduleEvent> events = new List<ScheduleEvent>();

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
                        CitireEveniment(objectives, events);
                        break;
                    case "A":
                        AfisareEvenimente(events);
                        break;
                    case "S":
                        CautareCategorie(events);
                        break;
                    default:
                        Console.WriteLine("Optiunea aleasa nu este valida!");
                        break;
                }
            } while (optiune.ToUpper() != "X");

        }

        public static void CitireEveniment(List<Objective> objectives, List<ScheduleEvent> events)
        {
            Console.WriteLine("Adaugare Obiectiv");
            Console.Write("Titlu: ");
            string objTitle = Console.ReadLine();

            Console.Write("Categorie: ");
            string objCategory = Console.ReadLine();

            Console.Write("Descriere: ");
            string objDesc = Console.ReadLine();

            Objective obiectivNou = new Objective(objTitle, objCategory, objDesc);
            objectives.Add(obiectivNou);


            Console.WriteLine("Adaugare Eveniment");
            Console.Write("Titlu: ");
            string evTitle = Console.ReadLine();

            Console.Write("Descriere: ");
            string evDesc = Console.ReadLine();

            Console.Write("Data de inceput(yyyy-mm-dd hh:mm) ");
            DateTime startTime = DateTime.Parse(Console.ReadLine());

            Console.Write("Data de final(yyyy-mm-dd hh:mm) ");
            DateTime endTime = DateTime.Parse(Console.ReadLine());

            ScheduleEvent evenimentNou = new ScheduleEvent(evTitle, evDesc, startTime, endTime, obiectivNou);

            events.Add(evenimentNou);

            Console.WriteLine("Date au fost salvate cu succes!");
        }

        public static void AfisareEvenimente(List<ScheduleEvent> events)
        {
            foreach(var item in events)
                Console.WriteLine(item.Info());
        }

        public static void CautareCategorie(List<ScheduleEvent> events)
        {
            Console.WriteLine("Categoria cautata: ");
            string? categorieCautata = Console.ReadLine();
            bool categorieGasita = false;
            foreach (var item in events)
            {
                if(item.ParentObjective.Category.ToLower() == categorieCautata.ToLower())
                {
                    Console.WriteLine(item.Info());
                    categorieGasita = true;
                }
            }

            if (!categorieGasita)
                Console.WriteLine("Categoria nu a fost gasita!");
        }
    }
}
