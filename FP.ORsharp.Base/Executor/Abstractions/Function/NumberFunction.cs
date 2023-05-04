using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public class NumberFunction : IFunction
    {
        public int Value { get; }

        public NumberFunction(int value)
        {
            Value = value;
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            throw new Exception("Cannot pass argument to number");
        }

        public IFunction Clone()
        {
            return new NumberFunction(Value);
        }

        string IFunction.ToString(Context context, int depth)
        {
            return $"{Value}";
        }
    }
}
