using System;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class DivFucntion : IFunction
    {
        private int _argumentNumber = 1;
        private int _operand1 = 0;

        public IFunction Clone()
        {
            return new DivFucntion
            {
                _argumentNumber = _argumentNumber,
                _operand1 = _operand1
            };
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            switch (_argumentNumber)
            {
                case 1:
                    argument = FunctionUtils.Expand(argument, context);
                    if (argument is NumberFunction arg)
                    {
                        _operand1 = arg.Value;
                    }
                    else
                    {
                        throw new Exception($"Wrong first argument type '{argument.GetType().Name}'");
                    }
                    _argumentNumber = 2;
                    return this;
                case 2:
                    int operand2;
                    argument = FunctionUtils.Expand(argument, context);
                    if (argument is NumberFunction arg2)
                    {
                        operand2 = arg2.Value;
                    }
                    else
                    {
                        throw new Exception($"Wrong second argument type '{argument.GetType().Name}'");
                    }
                    _argumentNumber = 3;
                    return new NumberFunction(_operand1 / operand2);
                default: throw new Exception("Wrong argument number");
            }
        }
    }
}
