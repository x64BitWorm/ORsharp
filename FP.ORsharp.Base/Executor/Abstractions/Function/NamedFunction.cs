using FP.ORsharp.Base.Grammar;

namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public class NamedFunction : IFunction
    {
        public string Name { get; }

        public NamedFunction(string name)
        {
            Name = name;
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            var prefferedContext = context;
            return prefferedContext.GetVariable(Name).PassArgument(context, argument);
        }

        public IFunction Clone()
        {
            return new NamedFunction(Name);
        }

        string IFunction.ToString(Context context, int depth)
        {
            if (depth < 0)
            {
                return "...";
            }
            return context.CheckVariable(Name) ? context.GetVariable(Name).ToString(context, depth - 1) : Name;
        }
    }
}
