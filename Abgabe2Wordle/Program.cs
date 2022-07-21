using System;
using System.Collections.Generic;
using System.Linq;

namespace Wordle // Name vom Projekt
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            //Globale Variablen Setzten Start prefix gloVar = Globale Variable z.b. gloVarTest1

            String gloVarZiel = "TEST"; // Hier das wort zum raten eingeben Achtung keine Leerzeichen und nur Groß also TEST nicht Test
            int gloVarVersuche = 5; // Anzahl an versuchen bis "Game Over"
            int gloVarVersuchNr = 0; //Nichts eingeben
            String gloVarEingabe = ""; //Nichts eingeben

            //Globale Variablen Setzten Ende


                Console.WriteLine("Hallo Willkommen bei Wordle sie haben " + (gloVarVersuche) + " Versuche um das Wort zu erraten"); //Begrüßung nur ein mal beim Start

            for (Console.WriteLine("Bitte geben Sie den ersten versuche ein das Wort hat " + gloVarZiel.Length + " Buchstaben") ; gloVarZiel != gloVarEingabe && gloVarVersuchNr < gloVarVersuche;) //Haupt Schleife läuft bis das Spiel gewonnen wurde
            {
                gloVarVersuchNr++; // Versuche werden gezählt
                Console.WriteLine("Das ist Ihr " + gloVarVersuchNr + ". Versuch"); // Ausgabe nach jeden versuch
                do //Check ob die Eingabe stimmen kann zu klein oder groß
                {
                    gloVarEingabe = Console.ReadLine().ToUpper(); // Einlesen aus Console und dann werden noch Großbuchstaben draus gemacht
                    if (gloVarEingabe.Length != gloVarZiel.Length) Console.WriteLine("Ihr Eingabe war nicht im Berreich. Das Wort ist " + gloVarZiel.Length + " Buchstaben lang!"); //Meldung da falsche länge
                } while (gloVarEingabe.Length != gloVarZiel.Length); //mach es noch mal wenn falsche länge
                Console.WriteLine(); //Abstand
                for (int i = 0; i < gloVarZiel.Length && gloVarZiel != gloVarEingabe; i++) //Hier werden die Buchstaben auf Richtige Position gecheckt
                {
                    if (gloVarEingabe[i] == gloVarZiel[i]) Console.WriteLine("Der Buchstabe " + gloVarZiel[i] + " an Stelle " + (i + 1) + " steht am richtigen Platz!"); // Buchstaben sind an der richtigen stelle
                    else
                    {
                        for (int j = 0; j < gloVarZiel.Length; j++) //Wenn nicht an der richtigen stelle eventuell ist der Buchstabe an einer anderen Stelle ?
                        {
                            if (gloVarEingabe[i] == gloVarZiel[j]) 
                            {
                                Console.WriteLine("Der Buchstabe " + gloVarEingabe[i] + " an Stelle " + (i + 1) + " steht am falschen Platz!"); // Ausgabe +1 für Normale Menschen weil Stelle 0 kenne die nicht...
                                j = gloVarZiel.Length; //falls ein Buchstabe mehrmals vorkommt scheibe wir ihn nur ein mal... (geht wahrscheinlich auch hübscher)
                            }
                        }
                    }
                }
                Console.WriteLine();
                if (gloVarZiel == gloVarEingabe) // Check auf Win
                {
                    if (gloVarVersuchNr == 1) Console.WriteLine("Das war richtig Sie haben " + gloVarVersuchNr + " Versuch gebraucht!"); //Spiel gewonnen
                    else Console.WriteLine("Das war richtig Sie haben " + gloVarVersuchNr + " Versuche gebraucht!");// Ausgabe an versuchen

                }
                else 
                {
                    if (gloVarVersuchNr < gloVarVersuche) Console.WriteLine("Das war leider nicht richtig Sie haben noch " + (gloVarVersuche - gloVarVersuchNr) + " Versuche"); //Ausgabe versuch es nochmal
                    else Console.WriteLine("Sie haben Verloren"); // Game Over...
                }
                Console.WriteLine();
            }
        }
    }
}