using System;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class PrintFunction : IFunction
    {
        public IFunction Clone()
        {
            return new PrintFunction();
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            Console.WriteLine(argument.ToString(context, 10));
            return argument;
        }
    }
}
