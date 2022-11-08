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

        public void AddQuintuple(string inputState, string inputSymbol, string outputState, string outputSymbol, string direction)
        {
            QuintupleIn input = new QuintupleIn(inputState, inputSymbol);
            QuintupleOut output = new QuintupleOut(outputState, outputSymbol, direction);

            quintuples.Add(input, output);
        }

        public bool ValidTransition(QuintupleIn input)
        {
            return quintuples.ContainsKey(input);
        }

        public QuintupleOut GetTransition(string inputState, string inputSymbol)
        {
            QuintupleIn input = new QuintupleIn(inputState, inputSymbol);
            return GetTransition(input);
        }

        public QuintupleOut GetTransition(QuintupleIn input)
        {
            return quintuples[input];
        }
    }
}
