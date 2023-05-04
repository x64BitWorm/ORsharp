using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Function.Internal
{
    public class WhileFunction : IFunction
    {
        public IFunction Clone()
        {
            return new WhileFunction();
        }

        public IFunction PassArgument(Context context, IFunction argument)
        {
            IFunction bodyResult;
            int i;
            for(i = 0; ; i++)
            {
                bodyResult = FunctionUtils.Expand((argument.Clone() as CustomFunction).PassArgument(context, new NumberFunction(i)), context);
                if(bodyResult is BreakFunction)
                {
                    break;
                }
            }
            return (bodyResult as BreakFunction).Result;
        }
    }
}
