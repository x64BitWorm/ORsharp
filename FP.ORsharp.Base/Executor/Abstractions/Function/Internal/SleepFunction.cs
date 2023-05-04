using System;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class SleepFunction : IFunction
    {
        public IFunction Clone()
        {
            return new SleepFunction();
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            if(argument is NumberFunction number)
            {
                System.Threading.Thread.Sleep(number.Value);
            }
            else
            {
                throw new Exception("Only number type allowed in sleep");
            }
            return argument;
        }
    }
}
