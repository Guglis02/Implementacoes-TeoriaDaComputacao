﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    public struct QuintupleIn : IEquatable<QuintupleIn>
    {
        public readonly string inputState;
        public readonly string inputSymbol;

        public QuintupleIn(string inputState, string inputSymbol)
        {
            this.inputState = inputState;
            this.inputSymbol = inputSymbol;
        }

        public bool Equals(QuintupleIn other)
        {
            return string.Equals(other.inputState, inputState) && string.Equals(other.inputSymbol, inputSymbol);
        }

        public override string ToString()
        {
            return $"({inputState},{inputSymbol})";
        }
    }

    public struct QuintupleOut
    {
        public readonly string outputState;
        public readonly string outputSymbol;
        public readonly string direction;

        public QuintupleOut(string outputState, string outputSymbol, string direction)
        {
            this.outputState = outputState;
            this.outputSymbol = outputSymbol;
            this.direction = direction;
        }

        public override string ToString()
        {
            return $"({outputState},{outputSymbol},{direction})";
        }
    }
}
