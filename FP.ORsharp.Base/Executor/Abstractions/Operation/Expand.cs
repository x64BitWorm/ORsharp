using FP.ORsharp.Base.Executor.Abstractions.Function;
using FP.ORsharp.Base.Executor.Abstractions.Function.Internal;
using FP.ORsharp.Base.Grammar;

namespace FP.ORsharp.Base.Executor.Abstractions.Operation
{
    public class Expand : IDerivable, IOperation
    {
        private string _name;
        private IFunction _expression;

        public void Derive(Lexem lexem)
        {
            lexem = lexem.Childs["entry"];
            _name = lexem.Childs["name"].Name;
            _expression = new Expression();
            (_expression as IDerivable).Derive(lexem);
        }

        public Context Process(Context context)
        {
            context.AddVariable(_name, FunctionUtils.Expand(_expression, context));
            return context;
        }

        public IOperation Clone()
        {
            return new Expand
            {
                _name = _name,
                _expression = _expression.Clone()
            };
        }
    }
}
