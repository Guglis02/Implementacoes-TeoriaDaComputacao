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
        public readonly char inputSymbol;

        public QuintupleIn(string inputState, char inputSymbol)
        {
            this.inputState = inputState;
            this.inputSymbol = inputSymbol;
        }

        public bool Equals(QuintupleIn other)
        {
            return string.Equals(other.inputState, inputState)
                && string.Equals(other.inputSymbol, inputSymbol);
        }

        public override string ToString()
        {
            return $"({inputState},{inputSymbol})";
        }
    }

    public struct QuintupleOut
    {
        public readonly string outputState;
        public readonly char outputSymbol;
        public readonly string direction;

        public QuintupleOut(string outputState, char outputSymbol, string direction)
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

    public struct QuadrupleIn : IEquatable<QuadrupleIn>
    {
        public readonly string inputState;
        public readonly char inputSymbol1;
        public readonly char inputSymbol2;
        public readonly char inputSymbol3;

        public QuadrupleIn(string inputState, char inputSymbol1, char inputSymbol2, char inputSymbol3)
        {
            this.inputState = inputState;
            this.inputSymbol1 = inputSymbol1;
            this.inputSymbol2 = inputSymbol2;
            this.inputSymbol3 = inputSymbol3;
        }

        public bool Equals(QuadrupleIn other)
        {
            return string.Equals(other.inputState, inputState)
                && string.Equals(other.inputSymbol1, inputSymbol1)
                && string.Equals(other.inputSymbol2, inputSymbol2)
                && string.Equals(other.inputSymbol3, inputSymbol3);
        }

        public override string ToString()
        {
            return $"{inputState}[{inputSymbol1},{inputSymbol2},{inputSymbol3}]";
        }
    }

    public struct QuadrupleOut
    {
        public readonly string outputState;
        public readonly char outputAction1;
        public readonly char outputAction2;
        public readonly char outputAction3;

        public QuadrupleOut(string outputState, char outputAction1, char outputAction2, char outputAction3)
        {
            this.outputState = outputState;
            this.outputAction1 = outputAction1;
            this.outputAction2 = outputAction2;
            this.outputAction3 = outputAction3;
        }

        public override string ToString()
        {
            return $"[{outputAction1},{outputAction2},{outputAction3}]{outputState}";
        }
    }
}
