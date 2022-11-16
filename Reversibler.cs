﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuringReversivel
{
    internal class Reversifier
    {
        private static ReversibleTransitions MakeReversible(Transitions transitions, string[] alphabet)
        {
            string[] states = { "A1", $"A{transitions.EndState}", "C1" };

            ReversibleTransitions rTransitions = new ReversibleTransitions(states);

            int step = 0;
            string lastState = "";

            // computa
            foreach (KeyValuePair<QuintupleIn, QuintupleOut> pair in transitions.GetAllTransitions())
            {
                step++;

                QuintupleIn input = pair.Key;
                QuintupleOut output = pair.Value;

                QuadrupleIn firstIn = new QuadrupleIn($"A{input.inputState}", input.inputSymbol.ToString(), "/", "B");
                QuadrupleOut firstOut = new QuadrupleOut($"A{step}'", output.outputSymbol.ToString(), "R", "B");

                QuadrupleIn secondIn = new QuadrupleIn($"A{step}'", "/", "B", "/");
                QuadrupleOut secondOut = new QuadrupleOut($"A{output.outputState}", output.direction, step.ToString(), "_");

                lastState = $"A{output.outputState}";

                rTransitions.AddQuadruple(firstIn, firstOut);
                rTransitions.AddQuadruple(secondIn, secondOut);
            }

            // ultima??

            // copia
            QuadrupleIn inp = new QuadrupleIn(lastState, "B", step.ToString(), "B");
            QuadrupleOut outp = new QuadrupleOut("B1'", "B", step.ToString(), "B");

            rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn("B1'", "/", "/", "/");
            outp = new QuadrupleOut("B1", "R", "_", "R");

            rTransitions.AddQuadruple(inp, outp);

            foreach (string symbol in alphabet)
            {
                inp = new QuadrupleIn("B1", symbol, step.ToString(), "B");
                outp = new QuadrupleOut("B1'", symbol, step.ToString(), symbol);

                rTransitions.AddQuadruple(inp, outp);
            }

            inp = new QuadrupleIn("B1", "B", step.ToString(), "B");
            outp = new QuadrupleOut("B2'", "B", step.ToString(), "B");

            rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn("B2'", "/", "/", "/");
            outp = new QuadrupleOut("B2", "L", "_", "L");

            rTransitions.AddQuadruple(inp, outp);

            foreach (string symbol in alphabet)
            {
                inp = new QuadrupleIn("B2", symbol, step.ToString(), symbol);
                outp = new QuadrupleOut("B2'", symbol, step.ToString(), symbol);

                rTransitions.AddQuadruple(inp, outp);
            }

            inp = new QuadrupleIn("B2", "B", step.ToString(), "B");
            outp = new QuadrupleOut($"C{transitions.EndState}", "B", step.ToString(), "B");

            // reverte
            // fazer

            return rTransitions;
        }
    }
}
