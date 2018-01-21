using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace MoLUpdateChecker
{
    class Program
    {
        static string DatName = "getLastChap.dat";
        static string LogName = "log.txt";
        static void Main(string[] args)
        {
            int lastChap = getLastChap();
            if (lastChap == 1)
            {
                //Keine Daten vorhanden
                int newChap = lookForLastChap();
                if (newChap != 1)
                {
                    //Gibt doch mehr als ein Chap... Duh!
                    Console.WriteLine("Es wurde das letzte Chap ermittelt: " + newChap.ToString());
                    Console.WriteLine("Die aktuelle Überschrift ist " + getChapHeadline(newChap));
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Es konnte nur ein Kapitel der Serie gefunden werden. Bitte manuell prüfen!");
                    Console.ReadLine();
                }
            }
            else
            {
                if (checkForChap(lastChap + 1))
                {
                    Console.WriteLine("Es wurde ein neues Kapitel gefunden: " + getChapHeadline(lastChap + 1));
                    Console.WriteLine("Um dieses Kapitel als gelesen zu markieren, bitte mit y bestätigen.");
                    string yOrNot = Console.ReadLine();
                    string[] yZeichen = { "y", "Y", "j", "J", "k", "K" };
                    foreach (string zeichen in yZeichen)
                    {
                        if (yOrNot.Contains(zeichen))
                        {
                            saveLastChap(lastChap + 1);
                        }
                    }
                }
                else
                {
                    //Prüfen, ob es das LastChap überhaupt wirklich gibt
                    if (!checkForChap(lastChap))
                    {
                        int trueLastChap = lookForLastChap();
                        Console.WriteLine("Gespeichertes Letztes Kapitel existierte nicht. Eintrag wurde aktualisiert, letztes gefundenes Kapitel ist " + getChapHeadline(trueLastChap));
                        Console.ReadLine();
                    }
                }
            }
        }

        private static string getChapHeadline(int Chap)
        {
            string response = getChapContent(Chap);
            string regular = "<strong>[^<>]+</strong>";
            MatchCollection matches = Regex.Matches(response, regular);
            if (matches.Count <= 0)
            {
                return "No Title Available";
            }
            string Header = "";
            foreach (Match curMatch in matches)
            {
                Header += curMatch.ToString().Replace("<strong>", "").Replace("</strong>", "") + " - ";
            }
            return Header.Substring(0, Header.Length - 3);
        }

        static int lookForLastChap()
        {
            int i = getLastChap();
            while (checkForChap(i))
            {
                Console.WriteLine("Chap " + i.ToString() + " gefunden");
                i++;
            }
            i--;
            saveLastChap(i);
            logSomething("Letztes Chap ermittelt!", i);
            return i;
        }

        static string getChapContent(int Chap)
        {
            string uri = "https://www.fictionpress.com/s/2961893/ChapName/Mother-of-Learning";
            string response = "Connection Error!";
            using (WebClient WC = new WebClient())
            {
                try
                {
                    response = WC.DownloadString(uri.Replace("ChapName", Chap.ToString()));
                }
                catch (WebException e)
                {
                    Console.WriteLine("Connection Error: " + e.Message);
                    logSomething(response, Chap);
                }
            }
            return response;
        }

        static bool checkForChap(int Chap)
        {
            return getChapContent(Chap).Contains("Zorian");
        }

        static int getLastChap()
        {
            if (File.Exists(DatName))
            {
                string content = File.ReadAllText(DatName);
                int.TryParse(content, out int Chap);
                return Chap <= 0 ? 1 : Chap;
            }
            else
            {
                return 1;
            }
        }

        static void saveLastChap(int Number)
        {
            File.WriteAllText(DatName, Number.ToString());
        }

        static void logSomething(string content, int Chap = 0)
        {
            string Prefix = DateTime.Now.ToShortDateString() + " - Chapter: " + (Chap > 0 ? Chap.ToString() : "No Chap") + " || ";
            File.AppendAllText(LogName, Prefix + content + Environment.NewLine);
        }
    }
}
