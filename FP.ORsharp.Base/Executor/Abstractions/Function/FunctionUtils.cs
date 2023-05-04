using FP.ORsharp.Base.Executor.Abstractions.Function.Internal;
using FP.ORsharp.Base.Executor.Abstractions.Operation;
using FP.ORsharp.Base.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public static class FunctionUtils
    {
        public static Dictionary<(IFunction, Context), IFunction> Cache { get; } = new Dictionary<(IFunction, Context), IFunction>();

        public static IFunction Derive(Lexem lexem)
        {
            switch (lexem.Childs["type"].Childs.First().Key)
            {
                case "value":
                    return Internals.GetByName(lexem.Childs["part"].Name) ?? new NamedFunction(lexem.Childs["part"].Name);
                case "expression":
                    var expression = new Expression();
                    expression.Derive(lexem);
                    return expression;
                case "number":
                    return new NumberFunction(int.Parse(lexem.Childs["part"].Name));
                case "function":
                    var function = new CustomFunction();
                    function.Derive(lexem);
                    return function;
                default: throw new Exception($"Unknown function type '{lexem.Childs["type"].Childs.First().Key}'");
            }
        }

        public static IFunction Expand(IFunction function, Context context, long depth = -1)
        {
            for(; depth != 0; depth--)
            {
                if (function is NamedFunction variable)
                {
                    function = context.GetVariable(variable.Name);
                }
                else if (function is Expression expression)
                {
                    function = expression.Process(context).Result;
                }
                else if (function is ContextFunction contextFunction)
                {
                    context = contextFunction.Context;
                    function = contextFunction.Function;
                }
                else
                {
                    break;
                }
            }
            return function;
        }
    }
}
