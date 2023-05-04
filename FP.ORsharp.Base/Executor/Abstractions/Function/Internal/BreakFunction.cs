namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class BreakFunction : IFunction
    {
        public IFunction Result { get; private set; }

        public IFunction Clone()
        {
            return new BreakFunction
            {
                Result = Result?.Clone()
            };
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            return new BreakFunction
            {
                Result = FunctionUtils.Expand(argument, context)
            };
        }
    }
}
