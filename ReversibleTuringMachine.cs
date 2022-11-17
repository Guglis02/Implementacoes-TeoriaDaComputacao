using System;
using System.Collections.Generic;
using System.Linq;

namespace MaquinaDeTuringReversivel
{
    internal class ReversibleTuringMachine
    {
        public string state;

        private int head1;
        private List<string> tape1;
        public string Symbol1
        {
            get
            {
                if (head1 < tape1.Count() && head1 >= 0)
                {
                    return tape1[head1];
                }
                return "B";
            }
        }

        private int head2;
        private List<string> tape2;
        public string Symbol2
        {
            get
            {
                if (head2 < tape2.Count() && head2 >= 0)
                {
                    return tape2[head2];
                }
                return "B";
            }
        }

        private int head3;
        private List<string> tape3;
        public string Symbol3
        {
            get
            {
                if (head3 < tape3.Count() && head3 >= 0)
                {
                    return tape3[head3];
                }
                return "B";
            }
        }

        private readonly ReversibleTransitions transitions;

        public ReversibleTuringMachine(char[] tape, ReversibleTransitions transitions)
        {
            this.head1 = 0;
            this.head2 = 0;
            this.head3 = 0;

            this.tape1 = tape.Select(c => c.ToString()).ToList();
            this.tape2 = new List<string>();
            this.tape3 = new List<string>();
            this.tape2.Add("B");
            this.tape3.Add("B");

            this.transitions = transitions;

            this.state = transitions.InitialState;
        }

        public bool Run()
        {
            string input;
            string history;
            string output;

            while (!string.Equals(state, transitions.EndState))
            {
                if (!Compute())
                {
                    return false;
                }

                input = String.Join(" | ", tape1);
                history = String.Join(" | ", tape2);
                output = String.Join(" | ", tape3);

                Console.Clear();
                Console.WriteLine("\n Input: " + input);
                Console.WriteLine("\n History: " + history);
                Console.WriteLine("\n Output: " + output);

                System.Threading.Thread.Sleep(200);
            }

            return true;
        }

        public bool Compute()
        {
            QuadrupleIn input = new QuadrupleIn(state, Symbol1, Symbol2, Symbol3);

            if (!transitions.ValidTransition(input))
            {
                return false;
            }

            QuadrupleOut output = transitions.GetTransition(input);

            Act(output.outputAction1, output.outputAction2, output.outputAction3);
            ChangeState(output.outputState);

            return true;
        }

        private void Act(string action1, string action2, string action3)
        {
            if (string.Equals(action1, "R") || string.Equals(action1, "L") || string.Equals(action1, "_"))
            {
                Move1(action1);
            }
            else
            {
                Write1(action1);
            }

            if (string.Equals(action2, "R") || string.Equals(action2, "L") || string.Equals(action2, "_"))
            {
                Move2(action2);
            }
            else
            {
                Write2(action2);
            }

            if (string.Equals(action3, "R") || string.Equals(action3, "L") || string.Equals(action3, "_"))
            {
                Move3(action3);
            }
            else
            {
                Write3(action3);
            }
        }

        private void ChangeState(string state)
        {
            this.state = state;
        }

        private void Write1(string symbol)
        {
            if (head1 < 0 && string.Equals(symbol, "B"))
                return;

            if (head1 < tape1.Count())
            {
                tape1[head1] = symbol;
            }
            else
            {
                tape1.Add(symbol);
            }
        }

        private void Write2(string symbol)
        {
            if (head2 < 0 && string.Equals(symbol, "B"))
                return;

            if (head2 < tape2.Count())
            {
                tape2[head2] = symbol;
            }
            else
            {
                tape2.Add(symbol);
            }
        }

        private void Write3(string symbol)
        {
            if (head3 < 0 && string.Equals(symbol, "B"))
                return;

            if (head3 < tape3.Count())
            {
                tape3[head3] = symbol;
            }
            else
            {
                tape3.Add(symbol);
            }
        }

        private void Move1(string direction)
        {

            switch (direction)
            {
                case "R":
                    head1++;
                    break;
                case "L":
                    head1--;
                    break;
                case "_":
                    break;
            }

            while (head1 >= tape1.Count())
            {
                tape1.Add("B");
            }
        }

        private void Move2(string direction)
        {
            switch (direction)
            {
                case "R":
                    head2++;
                    break;
                case "L":
                    head2--;
                    break;
                case "_":
                    break;
            }

            while (head2 >= tape2.Count())
            {
                tape2.Add("B");
            }
        }

        private void Move3(string direction)
        {
            switch (direction)
            {
                case "R":
                    head3++;
                    break;
                case "L":
                    head3--;
                    break;
                case "_":
                    break;
            }

            while (head3 >= tape3.Count())
            {
                tape3.Add("B");
            }
        }
    }
}
