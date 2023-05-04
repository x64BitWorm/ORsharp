using System;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class InputFunction : IFunction
    {
        public IFunction Clone()
        {
            return new InputFunction();
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            var inputNumber = Console.ReadLine();
            int number;
            return new NumberFunction(int.TryParse(inputNumber, out number) ? number : 0);
        }
    }
}
