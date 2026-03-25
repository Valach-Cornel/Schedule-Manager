using Schedule_Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class EventsManagerText : IStocareData
    {
        private string numeFisier;

        public EventsManagerText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AdaugaEveniment(ScheduleEvent ev)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(ev.ConversieLaSirPentruFisier());
            }
        }

        public List<ScheduleEvent> ObtineEvenimente()
        {
            List<ScheduleEvent> events = new List<ScheduleEvent>();
            
            using(StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                while((linieFisier = streamReader.ReadLine()) != null)
                {
                    events.Add(new ScheduleEvent(linieFisier));
                }
            }

            return events;
        }

        public List<ScheduleEvent> CautaDupaCategorie(string categorieCautata)
        {
            List<ScheduleEvent> events = ObtineEvenimente();
            return events.Where(ev => string.Equals(ev.ParentObjective.Category, categorieCautata)).ToList();
        }

        public bool UpdateEveniment(ScheduleEvent evActualizat)
        {
            List<ScheduleEvent> events = ObtineEvenimente();
            bool actualizareCuSucces = false;
            using (StreamWriter streamWriter = new StreamWriter(numeFisier, false))
            {
                foreach(ScheduleEvent item in events)
                {
                    ScheduleEvent eventPentruSalvare = item;
                    if(item.Id == evActualizat.Id)
                    {
                        eventPentruSalvare = evActualizat;
                    }
                    streamWriter.WriteLine(eventPentruSalvare.ConversieLaSirPentruFisier());
                }
                actualizareCuSucces = true;
            }
            return actualizareCuSucces;
        }
    }
}
