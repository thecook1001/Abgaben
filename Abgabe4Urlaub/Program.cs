using System;

namespace Abgabe4Urlaub // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static bool debug = true;


        static void Main(string[] args)
        {

            //if (args != null && args.Length >0)
            //{
            //    if (args[0] == "debug")
            //    {
            //        debug = true;
            //    }
            //}

            Urlaub urlaub = new Urlaub();

            FillPerson(urlaub, debug);
            FillUrlaubsZiel(urlaub);
            FillTransportMittel(urlaub);
            

            PrintPeron(urlaub);
            PrintUrlaubsZiel(urlaub);
            PrintTransportMittel(urlaub);
            urlaub.Dauer();
            Console.WriteLine("Die Kosten für den Urelaub sind gesammt: " + urlaub.Kosten + " Währung");
            Console.WriteLine("Die Kosten für den Urelaub sind pro Person: " + urlaub.KostenProPerson + " Währung");


        }
        static public void FillPerson(Urlaub urlaub,bool debug)
        {
            bool iWantMore = false;
            if (debug)
            {
                for (int i = 0; i < 14; i++)
                {
                    Person person = new Person();
                    person.Urlaub = urlaub;
                    person.Name = "Test" + i;
                    person.Alter = 30 + i;
                    urlaub.Personen.Add(person);
                }

            }
            else
            {
                do
                {
                    string answer = null;
                    Person person = new Person();
                    person.Urlaub = urlaub;
                    Console.WriteLine("Bitte geben Sie denn Namen ein:");
                    person.Name = Console.ReadLine();
                    Console.WriteLine("Bitte geben Sie das Alter ein:");
                    person.Alter = Int32.Parse(Console.ReadLine());
                    //person.Urlaub = urlaub;
                    urlaub.Personen.Add(person);
                    Console.WriteLine("Willst du mehr dann sag Ja");
                    answer = Console.ReadLine();
                    if (answer == "Ja")
                    {
                        iWantMore = true;
                    }
                    else
                    {
                        iWantMore = false;
                    }
                } while (iWantMore);
            }

        }

        static public void FillUrlaubsZiel(Urlaub urlaub)
        {
            urlaub.UrlaubsZiel = new UrlaubsZiel();
            Console.WriteLine("Bitte geben Sie die Kosten des Urlaubs ein:");
            urlaub.UrlaubsZiel.Kosten = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Bitte geben Sie das Urlaubsland ein:");
            urlaub.UrlaubsZiel.Land = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie die Entfernung in Kilometern ein:");
            urlaub.UrlaubsZiel.distance = Double.Parse(Console.ReadLine());
        }

        static public void FillTransportMittel(Urlaub urlaub)
        {
            bool giveMeMore = false;
            int MitFahrerGesammt = urlaub.Personen.Count;
            int zaehler = 0;
            if (debug)
            {
                do
                {
                    TransportMittel transportMittel = new TransportMittel();
                    transportMittel.Urlaub = urlaub;
                    if (MitFahrerGesammt > 5)
                    {
                        giveMeMore = true;
                        MitFahrerGesammt = MitFahrerGesammt - 5;
                        transportMittel.AnzahlMitFahrer = 5;
                    }
                    else
                    {
                        giveMeMore = false;
                        transportMittel.AnzahlMitFahrer = MitFahrerGesammt;
                    }
                    transportMittel.Fahrzeug = "Test " + zaehler;
                    transportMittel.Geschwindigkeit = zaehler * 10;
                    transportMittel.KostenProKilometer = zaehler * 11;
                    urlaub.Transportmittels.Add(transportMittel);
                    zaehler++;
                } while (giveMeMore);
            }
            else
            {
                do
                {
                    TransportMittel transportMittel = new TransportMittel();
                    transportMittel.Urlaub = urlaub;
                    if (MitFahrerGesammt > 5)
                    {
                        giveMeMore = true;
                        MitFahrerGesammt = MitFahrerGesammt - 5;
                        transportMittel.AnzahlMitFahrer = 5;
                    }
                    else
                    {
                        giveMeMore = false;
                        transportMittel.AnzahlMitFahrer = MitFahrerGesammt;
                    }
                    Console.WriteLine("Bitte geben Sie die Fahrzeug bezeichnung ein:");
                    transportMittel.Fahrzeug = Console.ReadLine();
                    Console.WriteLine("Bitte geben Sie die Geschwindigkeit in Kilometer/Min ein:");
                    transportMittel.Geschwindigkeit = Double.Parse(Console.ReadLine());
                    Console.WriteLine("Bitte geben Sie die Kosten pro Kilometer ein:");
                    transportMittel.KostenProKilometer = Convert.ToDecimal(Console.ReadLine());
                    urlaub.Transportmittels.Add(transportMittel);
                } while (giveMeMore);
            }
        }

        static public void PrintPeron(Urlaub urlaub)
        {
            foreach (var item in urlaub._personen)
            {
                Console.Write("Name: ");
                Console.WriteLine(item.Name);
                Console.Write("Alter: ");
                Console.WriteLine(item.Alter);
            }
        }

        static public void PrintUrlaubsZiel(Urlaub urlaub)
        {
            Console.Write("Urlaubs Ziel Kosten: ");
            Console.WriteLine(urlaub.UrlaubsZiel.Kosten);
            Console.Write("Urlaubs Ziel Land: ");
            Console.WriteLine(urlaub.UrlaubsZiel.Land);
            Console.Write("Urlaubs Ziel entfernung: ");
            Console.WriteLine(urlaub.UrlaubsZiel.distance);
        }

        static public void PrintTransportMittel(Urlaub urlaub)
        {
            int zaehler = 0;
            foreach (var item in urlaub._transportMittel)
            {
                Console.Write("Transport Mittel " + zaehler + " Anzahl mit Fahrer: ");
                Console.WriteLine(item.AnzahlMitFahrer);
                Console.Write("Transport Mittel " + zaehler + " Geschwindigkeit: ");
                Console.WriteLine(item.Geschwindigkeit);
                Console.Write("Transport Mittel " + zaehler + " Kosten pro Kilometer: ");
                Console.WriteLine(item.KostenProKilometer);
                Console.Write("Transport Mittel " + zaehler + " Fahrzeug: ");
                Console.WriteLine(item.Fahrzeug);
                zaehler++;
            }
        }

    }
}