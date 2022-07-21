using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abgabe4Urlaub
{
    public class Urlaub
    {

        public List<Person> _personen;

        public UrlaubsZiel UrlaubsZiel { get; set; }

        public List<TransportMittel> _transportMittel;

        public string Unterkunft { get; set; }

        public List<Person> Personen
        {
            get
            {
                if (_personen == null)
                {
                    _personen = new List<Person>();
                }
                return _personen;
            }
        }

        public List<TransportMittel> Transportmittels
        {
            get
            {
                if (_transportMittel == null)
                {
                    _transportMittel = new List<TransportMittel>();
                }
                return _transportMittel;
            }
        }

        //public List<Person> Personen => _personen == null ? new List<Person>() : _personen;


        public decimal Kosten
        {
            get
            {
                decimal kosten = 0m;
                foreach (var item in _transportMittel)
                {
                    kosten = kosten + (item.KostenProKilometer * ((decimal)UrlaubsZiel.distance));
                }
                kosten = kosten + UrlaubsZiel.Kosten;
                return kosten;
            }
        }

        public decimal KostenProPerson
        {
            get
            {
                decimal kosten = 0m;
                foreach (var item in _transportMittel)
                {
                    kosten = kosten + (item.KostenProKilometer * ((decimal)UrlaubsZiel.distance));
                }
                kosten = kosten + UrlaubsZiel.Kosten;
                kosten = kosten / Personen.Count;
                return kosten;
            }
        }

        public void Dauer()
        {
            double dauer = 0;
            double dauergesammt = 0;
            foreach (var item in _transportMittel)
            {
                dauer = UrlaubsZiel.distance / item.Geschwindigkeit;
                Console.WriteLine(item.Fahrzeug +" braucht ca: "+ dauer + " Minuten");
                dauergesammt = dauergesammt + dauer;
            }
            Console.WriteLine("Dauer aller Fahrzeuge zusammen sind ca: " + dauer + " Minuten");
        }

    }
}
