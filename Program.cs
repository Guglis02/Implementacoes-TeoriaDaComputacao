using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MaquinaDeTuringReversivel
{
    /// <summary>
    /// Pra rodar o programa é só ir na pasta que ele ta e usar
    /// .\MaquinaDeTuringReversivel.exe .\entrada-quintupla.txt
    /// </summary>
    internal class Program
    {
        static int numberOfStates;
        static int inputAlphabetSize;
        static int tapeAlphabetSize;
        static int numberOfTransitions;

        static string input;
        static string[] states;
        static string[] inputAlphabet;
        static string[] tapeAlphabet;

        static void GetQuantities(string line)
        {
            string[] quantities = line.Split(' ');

            numberOfStates = Int32.Parse(quantities[0]);
            inputAlphabetSize = Int32.Parse(quantities[1]);
            tapeAlphabetSize = Int32.Parse(quantities[2]);
            numberOfTransitions = Int32.Parse(quantities[3]);
        }

        static void Main(string[] args)
        {
            List<string> lines = File.ReadAllLines(args[0]).ToList();

            GetQuantities(lines[0]);
            lines.RemoveAt(0);

            states = lines[0].Split(' ');
            lines.RemoveAt(0);

            inputAlphabet = lines[0].Split(' ');
            lines.RemoveAt(0);

            tapeAlphabet = lines[0].Split(' ');
            lines.RemoveAt(0);

            input = lines.Last();
            lines.RemoveAt(lines.Count() - 1);

            Regex pattern = new Regex(@"\((?<InputState>.+),(?<InputSymbol>.+)\)=\((?<OutputState>.+),(?<OutputSymbol>.+),(?<OutputDirection>.+)\)");

            Transitions transitions = new Transitions(states);

            foreach (string line in lines)
            {
                Match match = pattern.Match(line);
                if (match.Success)
                {
                    transitions.AddQuintuple(match.Groups["InputState"].Value,
                                            match.Groups["InputSymbol"].Value[0],
                                            match.Groups["OutputState"].Value,
                                            match.Groups["OutputSymbol"].Value[0],
                                            match.Groups["OutputDirection"].Value);
                }
            }

            Console.WriteLine("Ordinária:");

            Console.Write(transitions.ToString());

            TuringMachine turingMachine = new TuringMachine(input, transitions);

            if (turingMachine.Run())
            {
                Console.Write("Computou");
            }
            else
            {
                Console.Write("Não computou");
            }

            ReversibleTransitions rTransitions = Reversifier.MakeReversible(transitions, tapeAlphabet);

            ReversibleTuringMachine rTM = new ReversibleTuringMachine(input.ToCharArray(), rTransitions);

            Console.WriteLine("\n\nReversível:");

            Console.Write(rTransitions.ToString());

            if (rTM.Run())
            {
                Console.Write("Computou\n");
            }
            else
            {
                Console.Write("Não computou\n");
            }
        }
    }
}
