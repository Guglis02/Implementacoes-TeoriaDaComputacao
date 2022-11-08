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
        public char symbol1
        {
            get
            {
                if (head1 < tape1.Length)
                {
                    return tape1[head1];
                }
                return 'B';
            }
        }

        private int head1;
        private char[] tape1;

        public char symbol2
        {
            get
            {
                if (head2 < tape2.Length)
                {
                    return tape2[head2];
                }
                return 'B';
            }
        }

        private int head2;
        private char[] tape2;
        
        public char symbol3
        {
            get
            {
                if (head3 < tape3.Length)
                {
                    return tape3[head3];
                }
                return 'B';
            }
        }

        private int head3;
        private char[] tape3;

        private Transitions transitions;
    }
}
