using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    internal class TuringMachine
    {
        private string[] tape;
        private int head;
        private States quintuples;

        public TuringMachine(string[] tape, string[] states)
        {
            this.tape = tape;
            this.head = 0;
            this.quintuples = new States();
        }
    }
}
