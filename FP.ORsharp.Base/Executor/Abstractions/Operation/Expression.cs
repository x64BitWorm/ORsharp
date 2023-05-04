using FP.ORsharp.Base.Executor.Abstractions.Function;
using FP.ORsharp.Base.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Operation
{
    public class Expression : IDerivable, IOperation, IFunction
    {
        private IFunction _function;
        private List<IFunction> _arguments;

        public bool Expand { get; set; }

        public void Derive(Lexem lexem)
        {
            if (lexem.Childs.ContainsKey("flags"))
            {
                if(lexem.Childs["flags"].Childs.ContainsKey("expand"))
                {
                    Expand = lexem.Childs["flags"].Childs["expand"].Name == "!";
                }
            }
            lexem = lexem.Childs["entry"];
            _function = FunctionUtils.Derive(lexem);
            _arguments = new List<IFunction>();
            while(lexem.Childs.ContainsKey("next"))
            {
                lexem = lexem.Childs["next"];
                _arguments.Add(FunctionUtils.Derive(lexem));
            }
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            //context = context.Clone();
            foreach (var arg in _arguments)
            {
                _function = _function.PassArgument(context, arg);
            }
            return _function.PassArgument(context, argument);
        }

        public Context Process(Context context)
        {
            foreach (var argument in _arguments)
            {
                _function = _function.PassArgument(context, argument);
            }
            context.Result = _function;
            return context;
        }

        IFunction IClonable<IFunction>.Clone() => Clone();

        IOperation IClonable<IOperation>.Clone() => Clone();

        public Expression Clone()
        {
            return new Expression
            {
                _function = _function.Clone(),
                _arguments = _arguments.Select(arg => arg.Clone()).ToList(),
                Expand = Expand
            };
        }

        string IFunction.ToString(Context context, int depth)
        {
            if (depth < 0)
            {
                return "...";
            }
            return $"{_function.ToString(context, depth - 1)} {string.Join(' ', _arguments.Select(arg => '('+arg.ToString(context, depth - 1)+')').ToArray())}";
        }
    }
}
