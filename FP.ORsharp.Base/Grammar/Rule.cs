using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ORsharp.Base.Grammar
{
    public class Rule
    {
        public string Name { get; set; }
        public List<RuleSymbol> Symbols { get; set; } = new List<RuleSymbol>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name).Append(" =");
            foreach(var symbol in Symbols)
            {
                sb.Append(' ').Append(symbol.ToString());
            }
            return sb.ToString();
        }
    }
}
