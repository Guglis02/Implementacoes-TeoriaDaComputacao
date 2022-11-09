using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    internal class ReversibleTransitions
    {
        public string InitialState => states.First();
        public string EndState => states.Last();

        private readonly Dictionary<QuadrupleIn, QuadrupleOut> quadruples;
        private readonly string[] states;

        public ReversibleTransitions(string[] states)
        {
            this.states = states;

            quadruples = new Dictionary<QuadrupleIn, QuadrupleOut>();
        }

        public void AddQuadruple(string inputState, char inputSymbol1, char inputSymbol2, char inputSymbol3, string outputSate, char outputAction1, char outputAction2, char outputAction3)
        {
            QuadrupleIn input = new QuadrupleIn(inputState, inputSymbol1, inputSymbol2, inputSymbol3);
            QuadrupleOut output = new QuadrupleOut(outputSate, outputAction1, outputAction2, outputAction3);

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
