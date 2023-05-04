using FP.ORsharp.Base.Executor.Abstractions.Function;
using FP.ORsharp.Base.Executor.Abstractions.Operation;
using FP.ORsharp.Base.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions
{
    public class Operations : IDerivable, IFunction, IOperation
    {
        private List<IOperation> _operations;

        public void Derive(Lexem lexem)
        {
            if(!lexem.Childs.ContainsKey("operations"))
            {
                throw new Exception($"'operations' lexem not found");
            }
            _operations = new List<IOperation>();
            var operation = lexem.Childs["operations"];
            while(true)
            {
                object currentOperation;
                switch(operation.Childs["type"].Childs.First().Key)
                {
                    case "comment": currentOperation = new Comment(); break;
                    case "expression": currentOperation = new Expression(); break;
                    case "variable": currentOperation = new Variable(); break;
                    case "expand": currentOperation = new Expand(); break;
                    default: throw new Exception($"Undefined operation type '{operation.Childs["type"].Childs.First().Key}'");
                }
                (currentOperation as IDerivable).Derive(operation);
                _operations.Add(currentOperation as IOperation);
                if(operation.Childs.ContainsKey("next"))
                {
                    operation = operation.Childs["next"];
                }
                else
                {
                    break;
                }
            }
        }

        public int Execute()
        {
            Context context = new Context();
            foreach(var operation in _operations)
            {
                context = operation.Process(context);
            }
            context.Result = FunctionUtils.Expand(context.Result, context);
            if(context.Result is NumberFunction number)
            {
                return number.Value;
            }
            throw new Exception($"Return value of main function has to be integer, but '{context.Result.GetType().Name}' found");
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            throw new Exception("Cannot pass argument to operations");
        }

        public Context Process(Context context)
        {
            foreach (var operation in _operations)
            {
                context = operation.Process(context);
            }
            context.Result = new ContextFunction(context.Result, context.Clone());
            return context;
        }

        IFunction IClonable<IFunction>.Clone() => Clone();

        IOperation IClonable<IOperation>.Clone() => Clone();

        public Operations Clone()
        {
            return new Operations
            {
                _operations = _operations.Select(operation => operation.Clone()).ToList()
            };
        }

        public override string ToString()
        {
            var result = "{'operations': [";
            foreach(var operation in _operations)
            {
                result += $"{operation},";
            }
            result = result.TrimEnd(',');
            result += "]}";
            return result;
        }

        string IFunction.ToString(Context context, int depth)
        {
            if (depth < 0)
            {
                return "...";
            }
            return $"{string.Join(", ", _operations.Select(arg => arg is IFunction func ? func.ToString(context, depth - 1) : arg.ToString()).ToArray())}";
        }
    }
}
