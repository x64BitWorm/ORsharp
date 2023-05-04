using System;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class OrFunction : IFunction
    {
        private int _argumentNumber = 1;
        private bool _result = false;

        public IFunction Clone()
        {
            return new OrFunction
            {
                _argumentNumber = _argumentNumber,
                _result = _result
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
                        _result = arg.Value != 0;
                    }
                    else
                    {
                        throw new Exception($"Wrong first argument type '{argument.GetType().Name}'");
                    }
                    _argumentNumber = 2;
                    return this;
                case 2:
                    if(_result)
                    {
                        _argumentNumber = 3;
                        return new NumberFunction(1);
                    }
                    else
                    {
                        argument = FunctionUtils.Expand(argument, context);
                        if (argument is NumberFunction arg2)
                        {
                            return new NumberFunction(arg2.Value != 0 ? 1 : 0);
                        }
                        else
                        {
                            throw new Exception($"Wrong second argument type '{argument.GetType().Name}'");
                        }
                    }
                default: throw new Exception("Wrong argument number");
            }
        }
    }
}
