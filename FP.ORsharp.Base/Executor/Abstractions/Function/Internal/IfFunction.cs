using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class IfFunction : IFunction
    {
        private int _argumentNumber = 1;
        private bool _executeIf;
        private IFunction _statementIf;

        public IFunction Clone()
        {
            return new IfFunction
            {
                _argumentNumber = _argumentNumber,
                _executeIf = _executeIf,
                _statementIf = _statementIf?.Clone()
            };
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            switch(_argumentNumber)
            {
                case 1:
                    argument = FunctionUtils.Expand(argument, context);
                    if (argument is NumberFunction arg)
                    {
                        _executeIf = arg.Value != 0;
                    }
                    else
                    {
                        throw new Exception($"Wrong first argument type '{argument.GetType().Name}'");
                    }
                    _argumentNumber = 2;
                    return this;
                case 2:
                    _statementIf = argument;
                    _argumentNumber = 3;
                    return this;
                case 3:
                    return _executeIf ? _statementIf : argument;
                default: throw new Exception("Wrong argument number");
            }
        }
    }
}
