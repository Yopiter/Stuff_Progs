using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
namespace csv_generieren
{
    class Program
    {
        static void Main(string[] args)
        {
            GetWikipedia();
            String datei = Console.ReadLine();
            List<string> AllData = File.ReadAllLines(datei).ToList();
            List<string> NewData = new List<string>();
            foreach (string str in AllData)
            {
                String[] Teile = str.Split(new char[] { ';' }, 4,StringSplitOptions.None);
                if (Teile[0].Contains('+') || Teile[0].Contains('?'))
                {
                    Teile[1] = Teile[0].Split(new char[] { '+', '?' })[1].Trim();
                    Teile[0] = Teile[0].Split(new char[] { '+', '?' })[0].Trim();
                }
                else
                {
                    Teile[1] = Teile[0];
                }
                NewData.Add(Teile[0] + ";" + Teile[1] + ";" + Teile[2] + ";" + Teile[3]);
            }
            File.WriteAllLines(datei + ".csv", NewData);
        }

        private static void GetWikipedia(String Adresse = "https://de.wikipedia.org/wiki/Liste_der_UN-Nummern")
        {
            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(Adresse);
            newRequest.Method = "POST";
            HttpWebResponse resp = (HttpWebResponse)newRequest.GetResponse();
            StreamReader StrReader = new StreamReader(resp.GetResponseStream());
            String Content = StrReader.ReadToEnd();
            String[] TabellenRoh = Content.Split(new String[] { "<table class=\"prettytable\">", "</table>" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> Tabellen = new List<string>();
            foreach (String str in TabellenRoh)
            {
                if(str.Contains("<td>"))
                {
                    Tabellen.Add(str);
                }
            }
            List<string> newData = new List<string>();
            foreach (String str in Tabellen)
            {
                List<string> Zeilen = str.Split(new String[] { "<tr>", "</tr>\\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string Zeile in Zeilen) 
                { 
                    if(!Zeile.Contains("<td>")) continue;
                    List<string> Teile= Zeile.Replace("\n","").Split(new String[] {"<td>","</td>"}, StringSplitOptions.RemoveEmptyEntries).ToList();
                    Teile.Insert(1, "");
                    if (Teile[0].Contains('+') || Teile[0].Contains('–') || Teile[0].Contains('-'))
                    {
                        Teile[1] = Teile[0].Split(new char[] { '+', '–', '-' })[1].Trim();
                        Teile[0] = Teile[0].Split(new char[] { '+', '–', '-' })[0].Trim();
                    }
                    else
                    {
                        Teile[1] = Teile[0];
                    }
                    newData.Add(DeHTML(Teile[0] + ";" + Teile[1] + ";" + Teile[2] + ";" + Teile[3]+";"+Teile[4]));
                }
                
            }
            File.WriteAllLines("Daten.csv", newData, Encoding.UTF8);
        }

        private static string DeHTML(string Text)
        {
            if (Text.Contains("</a>"))
            {
                Text=Regex.Replace(Text, "</*a*[^<]*>", String.Empty);
            }
            Text = Text.Replace("<sup>", string.Empty);
            Text = Text.Replace("</sup>", string.Empty);
            Text = Text.Replace("&#160;", String.Empty);
            return WebUtility.HtmlDecode(Text);
        }
    }
}
