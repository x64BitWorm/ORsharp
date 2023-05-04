using FP.ORsharp.Base.Executor.Abstractions;
using FP.ORsharp.Base.Executor.Abstractions.Function;
using System.Collections.Generic;
using System.Linq;

namespace FP.ORsharp.Base.Executor
{
    public class Context: IClonable<Context>
    {
        private Dictionary<string, IFunction> _vars = new Dictionary<string, IFunction>();
        public IFunction Result { get; set; }

        public void AddVariable(string name, IFunction value)
        {
            if(_vars.ContainsKey(name))
            {
                _vars[name] = value;
            }
            else
            {
                _vars.Add(name, value);
            }
        }

        public IFunction GetVariable(string name)
        {
            return _vars[name].Clone();
        }

        public bool CheckVariable(string name)
        {
            return _vars.ContainsKey(name);
        }

        public Context Clone()
        {
            return new Context
            {
                _vars = _vars.ToDictionary(entry => entry.Key, entry => entry.Value),
                Result = Result?.Clone()
            };
        }

        public override string ToString()
        {
            return string.Join(", ", _vars.Select(v => $"{v.Key}: {v.Value.ToString(this, 10)}"));
        }
    }
}
