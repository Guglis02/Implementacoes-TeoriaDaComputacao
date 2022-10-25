using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    /// <summary>
    /// Pra rodar o programa é só ir na pasta que ele ta e usar
    /// .\MaquinaDeTuringReversivel.exe .\entrada-quintupla.txt
    /// </summary>
    internal class Program
    {
        //private static string path = @"./entrada-quintupla.txt";
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

            states = lines[0].Split();
            lines.RemoveAt(0);

            inputAlphabet = lines[0].Split(' ');
            lines.RemoveAt(0);

            tapeAlphabet = lines[0].Split(' ');
            lines.RemoveAt(0);

            input = lines.Last().Split();
            lines.RemoveAt(lines.Count() - 1);

            Console.WriteLine($"Numero de estados = {numberOfStates}\n" +
                $"Tamanho alfabeto de entrada = {inputAlphabetSize}\n" +
                $"Tamanho alfabeto da fita = {tapeAlphabetSize}\n" +
                $"N de transicoes = {numberOfTransitions}\n" +
                $"Estados = {states[0]}\n" +
                $"Alfabeto de entrada = {inputAlphabet}\n" +
                $"Alfabeto da fita = {tapeAlphabet}\n" +
                $"Entrada = {input}\n");

            /*foreach (string line in lines)
            {
                Console.WriteLine(line);
            }*/
        }
    }
}
