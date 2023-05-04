using System.Collections.Generic;
using System.Text;

namespace FP.ORsharp.Base.Grammar
{
    public class Lexem
    {
        public string Name { get; set; }
        public Dictionary<string, Lexem> Childs { get; set; } = new Dictionary<string, Lexem>();

        public override string ToString()
        {
            return ToString(0);
        }

        private string ToString(int offset)
        {
            var buffer = new StringBuilder();
            var offsetString = new string(' ', offset);
            foreach(var child in Childs)
            {
                buffer.Append(offsetString).Append(child.Key).Append('\n');
                buffer.Append(child.Value.ToString(offset + 2));
            }
            if(Childs.Count == 0)
            {
                buffer.Append(offsetString).Append('=').Append(Name).Append('\n');
            }
            return buffer.ToString();
        }
    }
}
