using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    class Transitions
    {
        public string InitialState => states.First();
        public string EndState => states.Last();

        private readonly Dictionary<QuintupleIn, QuintupleOut> quintuples;
        private string[] states;

        public Transitions(string[] states)
        {
            this.states = states;
            quintuples = new Dictionary<QuintupleIn, QuintupleOut>();
        }

        public void AddQuintuple(string inputState, char inputSymbol, string outputState, char outputSymbol, string direction)
        {
            QuintupleIn input = new QuintupleIn(inputState, inputSymbol);
            QuintupleOut output = new QuintupleOut(outputState, outputSymbol, direction);

            quintuples.Add(input, output);
        }

        public bool ValidTransition(QuintupleIn input)
        {
            return quintuples.ContainsKey(input);
        }

        public QuintupleOut GetTransition(string inputState, char inputSymbol)
        {
            QuintupleIn input = new QuintupleIn(inputState, inputSymbol);
            return GetTransition(input);
        }

        public QuintupleOut GetTransition(QuintupleIn input)
        {
            return quintuples[input];
        }

        public override string ToString()
        {
            string str = "";

            foreach (KeyValuePair<QuintupleIn, QuintupleOut> pair in quintuples.AsEnumerable())
            {
                str = str + pair.Key.ToString() + "=" + pair.Value.ToString() + "\n";
            }

            return str;
        }
    }
}
