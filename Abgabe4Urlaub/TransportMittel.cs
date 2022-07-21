namespace Abgabe4Urlaub
{
    public class TransportMittel
    {
        public string Fahrzeug { get; set; }

        public decimal KostenProKilometer { get; set; }

        public double Geschwindigkeit { get; set; }

        public int AnzahlMitFahrer { get; set; }

        public Urlaub Urlaub { get; set; }
    }
}