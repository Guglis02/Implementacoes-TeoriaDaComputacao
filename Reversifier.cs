using System.Collections.Generic;
using System.Linq;

namespace MaquinaDeTuringReversivel
{
    internal class Reversifier
    {
        public static ReversibleTransitions MakeReversible(Transitions transitions, string[] alphabet)
        {
            string[] states = { $"A{transitions.InitialState}", $"A{transitions.EndState}", $"C{transitions.InitialState}" };

            ReversibleTransitions rTransitions = new ReversibleTransitions(states);

            int step = 0;
            string lastState = "";

            // computa
            foreach (KeyValuePair<QuintupleIn, QuintupleOut> pair in transitions.GetAllTransitions())
            {

                step++;

                QuadrupleIn firstIn = new QuadrupleIn($"A{pair.Key.inputState}", pair.Key.inputSymbol.ToString(), "/", "B");
                QuadrupleOut firstOut = new QuadrupleOut($"A{step}'", pair.Value.outputSymbol.ToString(), "R", "B");

                QuadrupleIn secondIn = new QuadrupleIn($"A{step}'", "/", "B", "/");
                QuadrupleOut secondOut = new QuadrupleOut($"A{pair.Value.outputState}", pair.Value.direction, step.ToString(), "_");

                lastState = $"A{pair.Value.outputState}";

                rTransitions.AddQuadruple(firstIn, firstOut);
                rTransitions.AddQuadruple(secondIn, secondOut);
            }

            // move para esquerda

            QuadrupleIn inp = new QuadrupleIn(lastState, "B", step.ToString(), "B");
            QuadrupleOut outp = new QuadrupleOut("R1", "L", "_", "_");

            rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn("R1", "B", step.ToString(), "B");
            outp = new QuadrupleOut("R2", "_", "_", "_");

            rTransitions.AddQuadruple(inp, outp);

            foreach (string symbol in alphabet)
            {
                if (string.Equals(symbol, "B")) continue;

                inp = new QuadrupleIn("R1", symbol, "/", "/");
                outp = new QuadrupleOut("R1", "L", "_", "_");

                rTransitions.AddQuadruple(inp, outp);
            }

            // copia
            inp = new QuadrupleIn("R2", "B", step.ToString(), "B");
            outp = new QuadrupleOut("B1'", "B", step.ToString(), "B");

            rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn("B1'", "/", "/", "/");
            outp = new QuadrupleOut("B1", "R", "_", "R");

            rTransitions.AddQuadruple(inp, outp);

            foreach (string symbol in alphabet)
            {
                if (string.Equals(symbol, "B")) continue;

                inp = new QuadrupleIn("B1", symbol, step.ToString(), "B");
                outp = new QuadrupleOut("B1'", symbol, step.ToString(), symbol);

                rTransitions.AddQuadruple(inp, outp);
            }

            inp = new QuadrupleIn("B1", "B", step.ToString(), "B");
            outp = new QuadrupleOut("B2'", "B", step.ToString(), "B");

            rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn("B2'", "/", "/", "/");
            outp = new QuadrupleOut("B2", "_", "_", "_");

            rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn("B2", "B", step.ToString(), "B");
            outp = new QuadrupleOut($"C{transitions.EndState}", "B", step.ToString(), "B");

            rTransitions.AddQuadruple(inp, outp);

            // reverte

            KeyValuePair<QuintupleIn, QuintupleOut> first = transitions.GetAllTransitions().Last();

            QuintupleIn input = first.Key;
            QuintupleOut output = first.Value;

            inp = new QuadrupleIn($"C{output.outputState}", "/", step.ToString(), "/");
            outp = new QuadrupleOut($"C{step}'", "_", "B", "_");

            /*rTransitions.AddQuadruple(inp, outp);

            inp = new QuadrupleIn($"C{step}'", "B", "/", "B");
            outp = new QuadrupleOut($"C{input.inputState}", "B", "L", "B");

            rTransitions.AddQuadruple(inp, outp);

            step--;*/
            foreach (KeyValuePair<QuintupleIn, QuintupleOut> pair in transitions.GetAllTransitions().Reverse())
            {

                input = pair.Key;
                output = pair.Value;

                string dir = string.Equals(output.direction, "R") ? "L" : string.Equals(output.direction, "L") ? "R" : "_";

                inp = new QuadrupleIn($"C{output.outputState}", "/", step.ToString(), "/");
                outp = new QuadrupleOut($"C{step}'", dir, "B", "_");

                rTransitions.AddQuadruple(inp, outp);

                inp = new QuadrupleIn($"C{step}'", output.outputSymbol.ToString(), "/", "B");
                outp = new QuadrupleOut($"C{input.inputState}", input.inputSymbol.ToString(), "L", "B");

                rTransitions.AddQuadruple(inp, outp);

                step--;
            }

            return rTransitions;
        }
    }
}
