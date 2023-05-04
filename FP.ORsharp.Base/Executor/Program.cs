using FP.ORsharp.Base.Executor.Abstractions;
using FP.ORsharp.Base.Grammar;
using System;
using System.IO;

namespace FP.ORsharp.Base.Executor
{
    public class Program
    {
        private Lexem _rootLexem;

        public Program(string sourceCode)
        {
            var grammarPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var grammarContent = File.ReadAllText($"{grammarPath}\\Languages\\ORsharp.grm");
            var grammar = new Parser(grammarContent, false);
            _rootLexem = grammar.Parse("funcBody", sourceCode);
            if(_rootLexem == null)
            {
                throw new Exception("Syntax error");
            }
        }

        public int Execute()
        {
            //Console.WriteLine(_rootLexem.ToString());
            var operations = new Operations();
            operations.Derive(_rootLexem);
            //Console.WriteLine(operations.ToString());
            return operations.Execute();
        }
    }
}
