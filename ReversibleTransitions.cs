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
        private string[] states;


    }
}
