using DataAccess;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface
{
    public static class StocareFactory
    {
        private const string FORMAT_SALVARE = "FormatSalvare";
        private const string NUME_FISIER = "NumeFisier";

        public static IStocareData GetManager()
        {
            string formatSalvare = ConfigurationManager.AppSettings[FORMAT_SALVARE] ?? "";
            string numeFisier = ConfigurationManager.AppSettings[NUME_FISIER] ?? "";

            string directorCurent = Directory.GetCurrentDirectory();
            string folderSolutie = Directory.GetParent(directorCurent)?.Parent?.Parent?.Parent?.FullName ?? "";

            string caleCompletaFisier = Path.Combine(folderSolutie, "DataAccess", $"{numeFisier}.{formatSalvare}");

            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "memorie":
                        return new EventsManager();
                    case "txt":
                        return new EventsManagerText(caleCompletaFisier);
                }
            }

            return null;
        }
    }
}
