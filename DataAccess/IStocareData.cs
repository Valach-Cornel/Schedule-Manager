using Schedule_Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IStocareData
    {
        void AdaugaEveniment(ScheduleEvent ev);
        List<ScheduleEvent> ObtineEvenimente();
        List<ScheduleEvent> CautaDupaCategorie(string categorieCautata);
        bool UpdateEveniment(ScheduleEvent evActualizat);
        bool CompleteazaEveniment(string titluEv);
        bool StergereEveniment(string titluEv);
    }
}
