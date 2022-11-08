using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    class States
    {
        private readonly Dictionary<QuintupleIn, QuintupleOut> quintuples;

        public States()
        {
            quintuples = new Dictionary<QuintupleIn, QuintupleOut>();
        }

        public void AddQuintuples(string[] states)
        {
            for (int i = 0; i < states.Length; i++)
            {
                // quebra cada string em partes de input apropriado para AddQuintuple
            }
        }

        public void AddQuintuple(string inputState, string inputSymbol, string outputState, string outputSymbol, string direction)
        {
            QuintupleIn input = new QuintupleIn(inputState, inputSymbol);
            QuintupleOut output = new QuintupleOut(outputState, outputSymbol, direction);

            quintuples.Add(input, output);
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
