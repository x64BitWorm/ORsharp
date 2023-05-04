using FP.ORsharp.Base.Executor.Abstractions.Function;

namespace FP.ORsharp.Base.Executor.Abstractions
{
    public class ContextAtom
    {
        public IFunction Function { get; set; }
        public Context Context { get; set; }
    }
}
