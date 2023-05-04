using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Executor.Abstractions.Operation
{
    public interface IOperation: IClonable<IOperation>
    {
        public Context Process(Context context);
    }
}
