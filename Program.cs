using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        static string[] input;
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

            input = lines.Last().Split();
            lines.RemoveAt(lines.Count() - 1);

            Regex pattern = new Regex(@"\((?<InputState>.+),(?<InputSymbol>.+)\)=\((?<OutputState>.+),(?<OutputSymbol>.+),(?<OutputDirection>.+)\)");

            //Console.WriteLine($"Numero de estados = {numberOfStates}\n" +
            //    $"Tamanho alfabeto de entrada = {inputAlphabetSize}\n" +
            //    $"Tamanho alfabeto da fita = {tapeAlphabetSize}\n" +
            //    $"N de transicoes = {numberOfTransitions}\n");

            Transitions transitions = new Transitions(states);

            foreach (string line in lines)
            {
                Match match = pattern.Match(line);
                if (match.Success)
                {
                    transitions.AddQuintuple(match.Groups["InputState"].Value,
                                            match.Groups["InputSymbol"].Value,
                                            match.Groups["OutputState"].Value,
                                            match.Groups["OutputSymbol"].Value,
                                            match.Groups["OutputDirection"].Value);

                    //Console.WriteLine($"Estado atual {match.Groups["InputState"].Value};" +
                    //    $"Simbolo lido {match.Groups["InputSymbol"].Value};" +
                    //    $"Proximo estado {match.Groups["OutputState"].Value};" +
                    //    $"Simbolo escrito {match.Groups["OutputSymbol"].Value};" +
                    //    $"Movimento {match.Groups["OutputDirection"].Value}");
                }
            }

            Console.Write(transitions.ToString());

            TuringMachine turingMachine = new TuringMachine(input, transitions);

            if (turingMachine.Run())
            {
                Console.Write("Computou");
            } else
            {
                Console.Write("Não computou");
            }

        }
    }
}
