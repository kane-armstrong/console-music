using BeepBeep.Models;
using BeepBeep.Sheets;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BeepBeep
{
    internal class Program
    {
        private static Dictionary<string, string> Sheets = new Dictionary<string, string>
        {
            {"1", "super_mario.json"},
            {"2", "imperial_march.json"},
            {"3", "tetris.json"},
        };

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Pick a song: ");
                Console.WriteLine("1. Super Mario");
                Console.WriteLine("2. Imperial March");
                Console.WriteLine("3. Tetris");
                Console.WriteLine("q. quit");
                Console.Write("Selection: ");

                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid selection!");
                    continue;
                }
                
                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }

                if (!Sheets.ContainsKey(input))
                {
                    Console.WriteLine("Invalid selection!");
                    continue;
                }

                try
                {
                    var sheet = SheetFile.Load(Sheets[input]);
                    Console.Write($"Now playing: {sheet.Name} ... ");
                    PlaySheet(sheet);
                    Console.WriteLine("done!");
                }
                catch (Exception)
                {
                    Console.WriteLine("An error occurred while playing your selection. Looks like that songs busted!");
                }
            }
        }

        private static void PlaySheet(ConsoleMusicSheet sheet)
        {
            foreach (var configuration in sheet.Steps)
            {
                if (configuration.Type == StepType.Pause)
                {
                    Thread.Sleep(configuration.Action.Duration);
                }
                else
                {
                    var frequency = configuration.Action.Frequency ?? throw new ArgumentException(nameof(sheet.Steps));
                    Console.Beep(frequency, configuration.Action.Duration);
                }
            }
        }
    }
}