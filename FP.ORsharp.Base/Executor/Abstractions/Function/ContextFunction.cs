namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public class ContextFunction : IFunction
    {
        public IFunction Function { get; }
        public Context Context { get; }

        public ContextFunction(IFunction function, Context context)
        {
            Function = function;
            Context = context;
        }

        public IFunction Clone()
        {
            return new ContextFunction(Function.Clone(), Context.Clone());
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            return Function.PassArgument(Context, argument);
        }

        string IFunction.ToString(Context context, int depth)
        {
            if (depth < 0)
            {
                return "...";
            }
            return Function.ToString(Context, depth);
        }
    }
}
