using FP.ORsharp.Base.Executor.Abstractions.Function;
using FP.ORsharp.Base.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Operation
{
    public class Variable : IDerivable, IOperation
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
            context.AddVariable(_name, _expression);
            return context;
        }

        public IOperation Clone()
        {
            return new Variable
            {
                _name = _name,
                _expression = _expression.Clone()
            };
        }
    }
}
