using FP.ORsharp.Base.Executor.Abstractions.Operation;
using FP.ORsharp.Base.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public class CustomFunction : IFunction, IDerivable
    {
        private List<string> _arguments;
        private object _expression;

        public IFunction Clone()
        {
            return new CustomFunction
            {
                _arguments = _arguments.ToList(),
                _expression = (_expression as IFunction).Clone()
            };
        }

        public void Derive(Lexem lexem)
        {
            var args = lexem.Childs["arguments"];
            _arguments = new List<string>();
            while(true)
            {
                _arguments.Add(args.Childs["name"].Name);
                if(args.Childs.ContainsKey("next"))
                {
                    args = args.Childs["next"];
                }
                else
                {
                    break;
                }
            }
            _expression = new Operations();
            (_expression as IDerivable).Derive(lexem);
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            //context = context.Clone();
            var argumentName = _arguments.First();
            if(context.CheckVariable(argumentName))
            {
                if (argument is Expression expression && expression.Expand)
                {
                    context.AddVariable(argumentName, FunctionUtils.Expand(argument, context.Clone()));
                }
                else
                {
                    context.AddVariable(argumentName, new ContextFunction(argument, context.Clone()));
                }
                _arguments.RemoveAt(0);
                return _arguments.Count == 0 ? (_expression as IOperation).Process(context.Clone()).Result : this;
            }
            context.AddVariable(argumentName, argument);
            _arguments.RemoveAt(0);
            return _arguments.Count == 0 ? (_expression as IOperation).Process(context.Clone()).Result : this;
        }

        string IFunction.ToString(Context context, int depth)
        {
            if (depth < 0)
            {
                return "...";
            }
            return $"({string.Join(',', _arguments)} -> {(_expression as IFunction).ToString(context, depth - 1)})";
        }
    }
}
