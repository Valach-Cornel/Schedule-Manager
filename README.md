## Despre Proiect
**UniSchedule Manager** este o aplicație desktop dezvoltată în **C#** și **WPF (Windows Presentation Foundation)**. A fost creată pentru a ajuta studenții să își organizeze eficient timpul, combinând un orar clasic de facultate cu un manager de task-uri (To-Do list) integrat la nivelul fiecărei materii.

Spre deosebire de calendarele generice, această aplicație este gândită specific pentru viața de student, luând în calcul tipul activității (curs/seminar/laborator) și task-urile punctuale pentru fiecare oră în parte (ex: "Predare referat", "Temă seminar").

## Funcționalități Principale

Următoarele funcționalități sunt planificate a fi implementate în cadrul acestui proiect:

* **Vizualizare Interactivă a Orarului:** O interfață tabelară (săptămânală) unde utilizatorul își poate vedea clar programul pe zile și ore.
* **Gestiunea Task-urilor per Materie:** Fiecare activitate din orar (ex: *Seminar Matematică, Luni 10:00*) are propria listă de task-uri atașată. Utilizatorul poate adăuga, bifa sau șterge sarcini specifice acelei ore.
* **Sistem "Săptămâna Pară / Impară":** Un comutator simplu (toggle) pentru a adapta afișarea la orarele alternante, specifice mediului universitar.
* **Codare Vizuală pe Culori:** Diferențierea rapidă și vizuală între Cursuri (ex: albastru), Seminarii (ex: galben) și Laboratoare (ex: verde).
* **Persistența Datelor (Salvare Locală):** Toate informațiile introduse (orar + task-uri) sunt salvate automat într-un fișier local de tip `.json` pentru a nu se pierde la închiderea aplicației.
