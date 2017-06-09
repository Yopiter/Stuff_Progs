using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN_NummernGefahrstoffe
{
    class Eintrag //Klasse Eintrag: Für jeden Eintrag aus der Wikipedia-Seite wird ein entsprechendes Objekt erstellt
    {
        //Eigentschaften der UN-Nummern -> Attribute der Eintrags-Objekte
        private int min, max; //Un-Nummern der gespeicherten Einträge. Obere und untere Nummern entsprechend dem Wikipedia-Verzeichnis. Z.B. "Zünder, sprengkräftig, mit Sicherungsvorrichtungen" haben Nummer 408-410
        public string GefZahl, Klasse, Bezeichnung;

        public Eintrag(int Min, int Max, string gefzahl, string klasse, string bezeichnung) //Methode zum erstellen eines neuen Eintrags-Objektes, Zuweisung der Werte. S. GetData()-Methode
        {
            this.min = Min;
            this.max = Max;
            this.GefZahl = gefzahl;
            this.Klasse = klasse;
            this.Bezeichnung = bezeichnung;
        }

        public bool isPart(int Nummer) //Abfrage, ob dieses Objekt eine bestimmte UN-Nummer besitzt.
        {
            if (Nummer == max || Nummer == min || (Nummer < max && Nummer > min)) { return true; }
            return false;
        }
    }
}
