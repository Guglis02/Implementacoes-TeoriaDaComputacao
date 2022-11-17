using System.Collections.Generic;
using System.Linq;

namespace MaquinaDeTuringReversivel
{
    internal class ReversibleTransitions
    {
        public string InitialState => states.First();
        public string EndState => states.Last();

        private readonly Dictionary<QuadrupleIn, QuadrupleOut> quadruples;
        private readonly List<string> states;

        public ReversibleTransitions(string[] states)
        {
            this.states = states.ToList();

            quadruples = new Dictionary<QuadrupleIn, QuadrupleOut>();
        }

        public void AddQuadruple(string inputState, string inputSymbol1, string inputSymbol2, string inputSymbol3, string outputSate, string outputAction1, string outputAction2, string outputAction3)
        {
            QuadrupleIn input = new QuadrupleIn(inputState, inputSymbol1, inputSymbol2, inputSymbol3);
            QuadrupleOut output = new QuadrupleOut(outputSate, outputAction1, outputAction2, outputAction3);

            AddQuadruple(input, output);
        }

        public void AddQuadruple(QuadrupleIn input, QuadrupleOut output)
        {
            quadruples.Add(input, output);
        }

        public bool ValidTransition(QuadrupleIn input)
        {
            return quadruples.ContainsKey(input);
        }

        public QuadrupleOut GetTransition(QuadrupleIn input)
        {
            return quadruples[input];
        }

        public override string ToString()
        {
            string str = "";

            foreach (KeyValuePair<QuadrupleIn, QuadrupleOut> pair in quadruples.AsEnumerable())
            {
                str = str + pair.Key.ToString() + " -> " + pair.Value.ToString() + "\n";
            }

            return str;
        }
    }
}
