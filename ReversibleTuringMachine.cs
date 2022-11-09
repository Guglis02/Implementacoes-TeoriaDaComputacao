using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    internal class ReversibleTuringMachine
    {
        public string state;

        private bool readHistory;

        private int head1;
        private char[] tape1;
        public char Symbol1
        {
            get
            {
                if (readHistory)
                {
                    return '/';
                }
                if (head1 < tape1.Length)
                {
                    return tape1[head1];
                }
                return 'B';
            }
        }

        private int head2;
        private char[] tape2;
        public char Symbol2
        {
            get
            {
                if (!readHistory)
                {
                    return '/';
                }
                if (head2 < tape2.Length)
                {
                    return tape2[head2];
                }
                return 'B';
            }
        }

        private int head3;
        private char[] tape3;
        public char Symbol3
        {
            get
            {
                if (readHistory)
                {
                    return '/';
                }
                if (head3 < tape3.Length)
                {
                    return tape3[head3];
                }
                return 'B';
            }
        }

        private readonly ReversibleTransitions transitions;

        public ReversibleTuringMachine(char[] tape, ReversibleTransitions transitions)
        {
            this.head1 = 0;
            this.head2 = 0;
            this.head3 = 0;

            this.tape1 = tape;
            this.tape2 = "".ToCharArray();
            this.tape3 = "".ToCharArray();

            this.transitions = transitions;

            this.state = transitions.InitialState;

            readHistory = false;
        }

        public bool Run(out string input, out string history, out string output)
        {
            while (!string.Equals(state, transitions.EndState))
            {
                if (!Compute())
                {
                    input = tape1.ToString();
                    history = tape2.ToString();
                    output = tape3.ToString();

                    return false;
                }
            }

            input = tape1.ToString();
            history = tape2.ToString();
            output = tape3.ToString();

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

        private void Act(char action1, char action2, char action3)
        {
            if (readHistory)
            {
                Move1(action1);
                Write2(action2);
                Move3(action3);

                readHistory = false;
            }
            else
            {
                Write1(action1);
                Move2(action2);
                Write3(action3);

                readHistory = true;
            }
        }

        private void ChangeState(string state)
        {
            this.state = state;
        }

        private void Write1(char symbol)
        {
            if (head1 < tape1.Length)
            {
                tape1[head1] = symbol;
            }
            else
            {
                tape1 = tape1.Append(symbol).ToArray();
            }
        }

        private void Write2(char symbol)
        {
            if (head2 < tape2.Length)
            {
                tape2[head2] = symbol;
            }
            else
            {
                tape2 = tape2.Append(symbol).ToArray();
            }
        }

        private void Write3(char symbol)
        {
            if (head3 < tape3.Length)
            {
                tape3[head3] = symbol;
            }
            else
            {
                tape3 = tape3.Append(symbol).ToArray();
            }
        }

        private void Move1(char direction)
        {

            switch (direction)
            {
                case 'R':
                    head1++;
                    break;
                case 'L':
                    head1--;
                    break;
                case '_':
                    break;
            }
            if (head1 < 0)
            {
                head1 = 0;
            }
        }

        private void Move2(char direction)
        {
            switch (direction)
            {
                case 'R':
                    head2++;
                    break;
                case 'L':
                    head2--;
                    break;
                case '_':
                    break;
            }
            if (head2 < 0)
            {
                head2 = 0;
            }
        }

        private void Move3(char direction)
        {
            switch (direction)
            {
                case 'R':
                    head3++;
                    break;
                case 'L':
                    head3--;
                    break;
                case '_':
                    break;
            }
            if (head3 < 0)
            {
                head3 = 0;
            }
        }
    }
}
