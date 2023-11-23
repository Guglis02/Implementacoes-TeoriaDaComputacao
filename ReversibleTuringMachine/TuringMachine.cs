using System;
using System.Linq;

namespace MaquinaDeTuringReversivel
{
    internal class TuringMachine
    {
        public string state;
        public char Symbol
        {
            get
            {
                if (head < tape.Length)
                {
                    return tape[head];
                }
                return 'B';
            }
        }

        private int head;
        private char[] tape;
        private readonly Transitions transitions;

        public TuringMachine(string tape, Transitions transitions)
        {
            this.tape = tape.ToCharArray();
            this.transitions = transitions;
            this.head = 0;
            this.state = transitions.InitialState;
        }

        public bool Run()
        {
            while (!string.Equals(state, transitions.EndState))
            {
                if (!Compute())
                {
                    Console.WriteLine(tape.ToString());
                    return false;
                }
            }

            Console.WriteLine(String.Join(" | ", tape));
            return true;
        }

        public bool Compute()
        {
            QuintupleIn inp = new QuintupleIn(state, Symbol);

            if (!transitions.ValidTransition(inp))
            {
                Console.WriteLine(inp.ToString());
                return false;
            }

            QuintupleOut res = transitions.GetTransition(inp);

            Write(res.outputSymbol);
            Move(res.direction);
            ChangeState(res.outputState);

            return true;
        }

        private void Write(char symbol)
        {
            if (head < tape.Length)
            {
                tape[head] = symbol;
            }
            else
            {
                tape = tape.Append(symbol).ToArray();
            }
        }

        private void ChangeState(string state)
        {
            this.state = state;
        }

        private void Move(string dir)
        {
            switch (dir)
            {
                case "R":
                    head++;
                    break;
                case "L":
                    head--;
                    break;
            }
            if (head < 0)
            {
                head = 0;
            }
        }
    }
}
