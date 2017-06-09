using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaRechner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Willkommen zum Tool zum Berechnen von Pizza-Volumen pro Euro!");
            while (true)
            {
                Console.WriteLine("Bitte wählen sie einen Modus aus:");
                Console.WriteLine("1 - Berechnung für Einzelpizza");
                Console.WriteLine("2 - Berechnung für versch. Pizzagrößen");
                Console.WriteLine("3 - Berechnung für verschiedene Pizzatypen");
                Console.WriteLine("4 - Programm beenden");
                int MenuChoice = 0;
                while (!int.TryParse(Console.ReadLine(), out MenuChoice) || MenuChoice > 4 || MenuChoice < 1)
                {
                    Console.WriteLine("Ungültige Auswahl!");
                }
                switch (MenuChoice)
                {
                    case (1):
                        CalculateSinglePizza();
                        break;
                    case (2):
                        CalculateMultipleSizes();
                        break;
                    case (3):
                        break;
                    case (4):
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Eingabe irgendwie unklar");
                        break;
                }
            }
        }

        private static void CalculateMultipleSizes()
        {
            Console.Clear();
            Console.WriteLine("Bitte geben sie nun die verschiedenen Größen mit den zugehörigen Preisen an und beenden sie anschließend die Eingabe mit 'end'");
            int i = 1;
            List<float> durchmesser = new List<float>(); ;
            List<float> preise = new List<float>();
            while (true)
            {
                Console.WriteLine("Durchmesser von Pizza " + i + " in cm:");
                string Eingabe = Console.ReadLine();
                if (Eingabe.Contains("end")) { break; }
                float fdurchmesser, fpreis=0;
                if (!float.TryParse(Eingabe, out fdurchmesser)) { Console.WriteLine("Ungültige Eingabe! Eingabe für Pizza " + i + " wiederholen!"); continue; }
                Console.WriteLine("Preis von Pizza " + i + " in €:");
                Eingabe = Console.ReadLine();
                if (!float.TryParse(Eingabe, out fpreis)) { Console.WriteLine("Ungültige Eingabe! Eingabe für Pizza " + i + " wiederholen!"); continue; }
                //Eingaben erfolgreich
                i++;
                durchmesser.Add(fdurchmesser);
                preise.Add(fpreis);
            }
            List<float> verhältnisse = new List<float>();
            Dictionary<float, float[]> DictGes = new Dictionary<float, float[]>();
            List<float> Flächen = new List<float>();
            foreach(float fl in durchmesser)
            {
                Flächen.Add(GetFläche(fl));
                verhältnisse.Add(GetFläche(fl) / preise[durchmesser.IndexOf(fl)]);
                DictGes.Add(GetFläche(fl) / preise[durchmesser.IndexOf(fl)], new float[] { fl, GetFläche(fl), preise[durchmesser.IndexOf(fl)] });
            }
            List<float> sortedList = DictGes.Keys.ToList();
            sortedList = sortedList.OrderByDescending(o => o).ToList();
            i = 1;
            Console.WriteLine("Platz  Durchmesser Preis Fläche    Fläche/Preis");
            foreach (float key in sortedList)
            {
                float[] Infos = DictGes[key];
                Console.WriteLine(i + ":     " + Math.Round(Infos[0], 2) + " cm       " + Infos[2] + " €  " + Math.Round(Infos[1], 2) + " cm²  " + Math.Round(key, 2) + " cm²/€");
                i++;
            }
            Console.WriteLine("*Fortfahren mit beliebiger Taste*");
            Console.ReadLine();
            Console.Clear();
        }

        private static void CalculateSinglePizza()
        {
            Console.Clear();
            Console.WriteLine("Bitte geben sie den Durchmesser der Pizza in cm an:");
            float durchmesser;
            while (!float.TryParse(Console.ReadLine(), out durchmesser) || durchmesser <= 0) { Console.WriteLine("Fehlerhafte Eingabe!"); }
            Console.WriteLine("Bitte geben sie den Preis der Pizza in Euro an:");
            float preis;
            while (!float.TryParse(Console.ReadLine(), out preis) || preis <= 0) { Console.WriteLine("Fehlerhafte Eingabe!"); }
            float Fläche = GetFläche(durchmesser);
            float AperE = Fläche / preis;
            Console.WriteLine("Ihre Pizza besitzt eine Fläche von " + Math.Round(Fläche, 1) + " cm² bei einem Preis von " + preis + " €.");
            Console.WriteLine("Daraus ergibt sich ein Fläche/Preis-Verhältnis von " + AperE + " cm²/€.");
            Console.WriteLine("*Fortfahren mit beliebiger Taste*");
            Console.ReadLine();
            Console.Clear();
        }

        private static float GetFläche(float durchmesser)
        {
            return (float)(Math.PI * durchmesser * durchmesser / 4);
        }
    }
}