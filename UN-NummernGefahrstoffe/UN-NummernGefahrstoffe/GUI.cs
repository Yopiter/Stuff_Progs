using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace UN_NummernGefahrstoffe
{
    public partial class GUI : Form
    {
        #region globale Variablen
        List<Eintrag> DatList = new List<Eintrag>(); //Verzeichnis aller vorhandenen Einträge
        Eintrag emptEintr = new Eintrag(0, 0, "", "", "Kein Eintrag mit dieser Nummer gefunden"); //leerer Eintrag als Platzhalter für erfolglose Suchen
        Eintrag curEintr;
        #endregion

        public GUI()
        {
            //Startpuntk des Programms. Initiales laden der UN-Nummern aus der Datei
            InitializeComponent();
            Getdata();
        }
        private void Getdata() //Import der Nummer/Bezeichnungen etc. aus der CSV-Datei
        {
            if (File.Exists("Daten.csv"))
            {
                DatList.Clear();
                List<string> Alldata = File.ReadAllLines("Daten.csv").ToList(); //Datei als Text importieren
                foreach (string str in Alldata) //Jede zeile...
                {
                    string[] Teile = str.Split(new char[] { ';' }); //Trennung der Zeile in die Informationsbestände
                    if (Teile[4]=="")   //Es gibt einen Datensatz (Nummer1700) dem ein Wert in der Mitte fehlt, weil Wikipedia den weggelassen hat. Hier wird der Wert ergänzt als leere Zeichenkette
                    {
                        List<string> TempList = Teile.ToList();
                        TempList.Insert(2, string.Empty);
                        Teile = TempList.ToArray();
                    }
                    DatList.Add(new Eintrag(int.Parse(Teile[0]), int.Parse(Teile[1]), Teile[2], Teile[3], Teile[4])); //Erstellen der Eintrag-Objekte. Siehe Eintrag.cs
                }
            }
            else MessageBox.Show("Datenverzeichnis Daten.csv fehlt! Bitte über 'Update' nachladen oder manuell in das Verzeichnis einfügen!"); //fehlermeldung wenn Datei fehlt
        }

        private void btUpdate_Click(object sender, EventArgs e) //Klick auf "Update"
        {
            GetWikipedia(); //Aktuelle Informationen von Wikipedia holen
            Getdata(); //Neue CSV-datei importiern und nutzen
            btUpdate.Text = "Update erfolgreich!";
        }

        private void GetWikipedia(String Adresse = "https://de.wikipedia.org/wiki/Liste_der_UN-Nummern") //Runterladen der aktuellen Infos von Wikipedia und Export als CSV-datei
        {
            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(Adresse);
            newRequest.Method = "POST";
            HttpWebResponse resp = (HttpWebResponse)newRequest.GetResponse();
            StreamReader StrReader = new StreamReader(resp.GetResponseStream());    //Lauter Http-zeugs, um eine Anfrage zu stellen.
            String Content = StrReader.ReadToEnd();                                 //Im Wesentlichen bloß das Herunterladen des Quelltextes der angegebenen URL
            String[] TabellenRoh = Content.Split(new String[] { "<table class=\"prettytable\">", "</table>" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> Tabellen = new List<string>(); //Im Folgenden werden die überflüssigen Bestandteile der Wikipedi-Seite entfernt.
            foreach (String str in TabellenRoh)         //Hier Filterung nach den verschiedenen tabellen, die auf der Seite gezeigt sind
            {
                if (str.Contains("<td>"))       //<td> ist der Tag für eine Tabellenzelle -> Ist eine tabelle; S. HTML-tags auf Wikipedia oder so
                {
                    Tabellen.Add(str);
                }
            }
            List<string> newData = new List<string>();
            foreach (String str in Tabellen)    //Abarbeiten der einzelnen tabellen
            {
                List<string> Zeilen = str.Split(new String[] { "<tr>", "</tr>\\n" }, StringSplitOptions.RemoveEmptyEntries).ToList(); //Aufspalten in zeilen
                foreach (string Zeile in Zeilen)
                {
                    if (!Zeile.Contains("<td>")) continue;  //Wenn in Zeilen Inhalte stehen -> keine Tabellenköpfe mit importieren
                    List<string> Teile = Zeile.Replace("\n", "").Split(new String[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries).ToList(); //Zeilenumbrüche (\n) entfernen und Zeile in die Infos aufteilen
                    Teile.Insert(1, "");                                                                //Der ganze Teil dient der Auftrennung der UN-Nummern, weil Wikipedia
                    if (Teile[0].Contains('+') || Teile[0].Contains('–') || Teile[0].Contains('-'))     //das so bescheuert mit 1-3 oder 1+2 angegeben hat. Deshalb trennung
                    {                                                                                   //in untere und obere Nummern, wenn nur eine Nummer vorhanden dann wird die kopiert.
                        Teile[1] = Teile[0].Split(new char[] { '+', '–', '-' })[1].Trim();
                        Teile[0] = Teile[0].Split(new char[] { '+', '–', '-' })[0].Trim();
                    }
                    else
                    {
                        Teile[1] = Teile[0];
                    }
                    newData.Add(DeHTML(Teile[0] + ";" + Teile[1] + ";" + Teile[2] + ";" + Teile[3] + ";" + Teile[4]));  //Speichern der aktuellen zeile in einer Liste, vorher mit DeHTML() noch mal überarbeiten. Siehe DeHTML unten
                }

            }
            File.WriteAllLines("Daten.csv", newData, Encoding.UTF8); //fertige Liste als CSV abspeichern
        }

        private string DeHTML(string Text) //Methode, um nervige HTML-Tags und HTML Entitäten zu entfernen
        {
            Text = Regex.Replace(Text, "<[^<]*>", String.Empty); //Alle HTML-tags aus dem Code entfernen. Die Methode heißt Ersetzen mittels regulärer Ausdrücke. Darauf wird er stehen. ^^
            Text = Text.Replace("&#160;", String.Empty);         //Sind im Wesentlich Suchmasken, die nach einem Muster im Text suchen und in dem Fall mit einer leeren Zeichenkette (also nichts) ersetzen.
            return WebUtility.HtmlDecode(Text);                  //Wenn du ihm den regulären Ausdruck <[^<]*> erklären kannst, ist er glücklich. Wenns Probleme gibt, kannst du mich auch noch mal fragen
        }

        private void Eintragen() //Suchen des gewünschten Eintrags und Anzeige im GUI
        {
            btUpdate.Text = "Update";
            int Nummer = (int)numPad.Value; //Nummer auslesen
            bool gefunden = false;
            foreach (Eintrag E in DatList)  //Suche nach passendem Eintrag
            {
                if (E.isPart(Nummer))
                {
                    gefunden = true;
                    curEintr = E;           //eintrag merken
                    break;
                }
            }
            if (!gefunden) curEintr = emptEintr;    //Wenn nicht gefunden, dann den leeren Eintrag verwenden
            tbBeschr.Text = curEintr.Bezeichnung;   //Eintragen der Werte
            tbGefZahl.Text = curEintr.GefZahl;
            tbKlasse.Text = curEintr.Klasse;
        }

        private void btSearch_Click(object sender, EventArgs e) //Klick auf "Bezeichnung suchen"
        {
            Eintragen();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) //Ändern der UN-Nummer im Feld
        {
            Eintragen();
        }
    }
}
