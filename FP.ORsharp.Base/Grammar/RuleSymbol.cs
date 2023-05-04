using System.Collections.Generic;
using System.Linq;

namespace FP.ORsharp.Base.Grammar
{
    public class RuleSymbol
    {
        public string Name { get; set; }
        public RuleSymbolType Type { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();

        public static RuleSymbolType GetTypeFromName(string name)
        {
            switch(name)
            {
                case "p": return RuleSymbolType.Pattern;
                case "r": return RuleSymbolType.Regex;
                default: return RuleSymbolType.Unknown;
            }
        }

        public override string ToString()
        {
            return $"[{Name} {Type} {string.Join(' ', Parameters.Select(x => $"'{x}'"))}]";
        }
    }
}
