using System.Runtime.InteropServices;

#pragma warning disable CA1416 //Das Programm Funktioniert nur unter Windows glaube ich Mindestens !

namespace Weihnachtsbaum
{
    public class Program
    {
        //Import für fullscreen Start
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static readonly IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;
        //Import für fullscreen Ende

        public static void Main()//string[] args
        {
            Console.Title = "Weihnachtsbaum";
            Fullscreen();
            int auswahl = ReadNumberInt($"Willkommen bei meinem Weihnachtsbaum bitte geben Sie eine Zahl zwischen 4 - {MaxHeight()} ein ({MaxHeight()} Empfohlen)", 4, MaxHeight());
            Console.Clear();
            Console.WriteLine();
            Console.Clear();
            TreeTop(auswahl);
            TreeMid(auswahl);
            TreeBottom(auswahl);
            CheckDateText(24, 12, $"Weihnachten", auswahl, true);
            Music(2);
        }

        //Methoden Start

        //Zahl Einlesen
        static int ReadNumberInt(string text, int min = -2147483648, int max = 2147483647)
        {
            int output;
            bool didParse;

            Console.WriteLine(text);
            do
            {
                string? eingabe = Console.ReadLine(); //? weil dadurch auch NULL möglich ist
                didParse = int.TryParse(eingabe, out output);
                if (didParse == false || output < min || output > max)
                    Console.WriteLine("Eingabe ungültig bitte nochmal:");
            } while (didParse == false || output < min || output > max);
            return output;
        }

        //Baum Krone erzeugen
        static void TreeTop(int count, char iconInFill0 = '*', char iconInFill1 = '0', char iconInFill2 = '+', char iconInFill3 = 'o')
        {
            for (int i = 1; i < count; i++)
            {
                for (int j = 0; j < (count - i + GetOffsetWidth(count)); j++)
                    Console.Write(' ');
                for (int k = 0; k < i * 2; k++)
                {
                    int randomInt = Random(0, 3);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (k == 0)
                        Console.Write('/');
                    else if (k + 1 == i * 2)
                        Console.Write('\\');
                    else if (k < i * 2)
                    {
                        if (randomInt == 0 || (k % 2) == 0)
                            Console.Write(iconInFill0);
                        else
                        {
                            RandomColor();
                            if (randomInt == 1)
                                Console.Write(iconInFill1);
                            else if (randomInt == 2)
                                Console.Write(iconInFill2);
                            else if (randomInt == 3)
                                Console.Write(iconInFill3);
                        }
                    }
                    Console.ResetColor();
                }
                Console.Write('\n');
                Thread.Sleep(1);
            }
        }

        //Baum Stamm erzeugen
        static void TreeMid(int count, char icontribe = '#')
        {
            int tribeWide = count / 4;
            if (tribeWide < 2)
                tribeWide++;
            for (int i = 0; i < (count / 4) + 0; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int j = 0; j < (count - tribeWide / 2 + GetOffsetWidth(count)); j++)
                    Console.Write(' ');
                for (int k = 0; k < tribeWide; k++)
                    Console.Write(icontribe);
                Console.Write('\n');
                Thread.Sleep(1);
            }
            Console.ResetColor();
        }

        //Baum Boden erzeugen
        static void TreeBottom(int count, char iconground = '=')
        {
            for (int j = 0; j < (count - (count - 1) + GetOffsetWidth(count)); j++)
                Console.Write(' ');
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < count * 2 - 1; i++)
                Console.Write(iconground);
            Console.Write("\n");
            Console.ResetColor();
        }

        //Musik
        static void Music(int cycle)
        {
            // Noten zu frequenz vom beep 
            int D4 = 293, E4 = 329, F4 = 349, G4 = 392, A4 = 440, B4 = 493, B3 = 245;

            // Ton dauer setzten
            int half = 1000, quarter = half / 2, eight = quarter / 2;
            for (int i = 0; i < cycle; i++) // Start Ablauf Musik
            {
                Thread.Sleep(2000);

                Console.Beep(B3, quarter);

                Console.Beep(F4, quarter);
                Console.Beep(F4, eight);
                Console.Beep(G4, eight);
                Console.Beep(F4, eight);
                Console.Beep(E4, eight);

                Console.Beep(D4, quarter);
                Console.Beep(D4, quarter);
                Console.Beep(D4, quarter);

                Console.Beep(G4, quarter);
                Console.Beep(G4, eight);
                Console.Beep(A4, eight);
                Console.Beep(G4, eight);
                Console.Beep(F4, eight);

                Console.Beep(E4, quarter);
                Console.Beep(E4, quarter);
                Console.Beep(E4, quarter);

                Console.Beep(A4, quarter);
                Console.Beep(A4, eight);
                Console.Beep(B4, eight);
                Console.Beep(A4, eight);
                Console.Beep(G4, eight);

                Console.Beep(F4, quarter);
                Console.Beep(D4, quarter);
                Console.Beep(B3, eight);
                Console.Beep(B3, eight);

                Console.Beep(D4, quarter);
                Console.Beep(G4, quarter);
                Console.Beep(E4, quarter);

                Console.Beep(F4, half);
            }
        }

        //Fullscreen
        static void Fullscreen()
        {
            Console.WriteLine("Wollen sie in denn Vollbildmodus wechseln ? (V=Vollbild)");
            string? doFullscreen = Console.ReadLine();
            if (doFullscreen == "V" || doFullscreen == "v")
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                ShowWindow(ThisConsole, MAXIMIZE);
            }
        }

        //Zufalls Zahl generieren
        static int Random(int min, int max)
        {
            Random randomNr = new();
            int randomInt = randomNr.Next(min, max + 1);//Achtung zahlen werden bei 0,6 von 0 bis 5 generiert daher +1
            return randomInt;
        }

        //Text Farbe zufällig auswählen
        static void RandomColor()
        {
            int randomInt = Random(0, 5);
            if (randomInt == 0) Console.ForegroundColor = ConsoleColor.Red;
            if (randomInt == 1) Console.ForegroundColor = ConsoleColor.Yellow;
            if (randomInt == 2) Console.ForegroundColor = ConsoleColor.Blue;
            if (randomInt == 3) Console.ForegroundColor = ConsoleColor.Cyan;
            if (randomInt == 4) Console.ForegroundColor = ConsoleColor.Magenta;
            if (randomInt == 5) Console.ForegroundColor = ConsoleColor.White;
        }

        //Check ob Datum stimmt oder nicht mit Text Ausgabe
        static void CheckDateText(int day, int month, string name, int offset = 0, bool doRandomColor = false)
        {
            DateTime input = DateTime.Parse($"{day}/{month}/{DateTime.Now.Year}");
            if (DateTime.Compare(DateTime.Now, input) >= 0)
                input = DateTime.Parse($"{day}/{month}/{DateTime.Now.Year + 1}");
            int days = Convert.ToInt32(Math.Ceiling(input.Subtract(DateTime.Now).TotalDays));
            if (doRandomColor)
                RandomColor();
            string space = "";
            if (offset != 0)
                space = OffsetText(offset);
            if (days == 365)
                Console.WriteLine($"{space}Heute ist {name} wir wünschen ihnen Frohe {name} !");
            else
                Console.WriteLine($"{space}Es sind noch {days} Tage bis {name} !");
            if (doRandomColor)
                Console.ResetColor();
        }

        //Offset + Fenster größe hälfte
        static int GetOffsetWidth(int count)
        {
            int offset = (Console.WindowWidth / 2) - count;
            return offset;
        }

        //Text offset
        static string OffsetText(int count)
        {
            String output = "";
            int offset = GetOffsetWidth(count) + 1;
            for (int i = 0; i < offset; i++)
                output += ' ';
            return output;
        }

        //Max Größe auf Display
        static int MaxHeight()
        {
            int output = Console.WindowHeight;
            output -= (output / 4) + 1; // /4 für Stamm und Boden +1 für Pointer
            return output;
        }

        //Methoden Ende

    }
}