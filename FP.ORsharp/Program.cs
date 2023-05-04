using System;
using System.IO;

namespace FP.ORsharp
{
    class Program
    {
        static int Main(string[] args)
        {
            /*var grammarContent = System.IO.File.ReadAllText("C:\\Users\\User\\Desktop\\MAI\\FP-23\\FP.ORsharp\\FP.ORsharp.Base\\Languages\\ORsharp.grm");
            var languageContent = System.IO.File.ReadAllText("C:\\Users\\User\\Desktop\\MAI\\FP-23\\FP.ORsharp\\SquareTest.osp");
            var grm = new Parser(grammarContent, false);
            var grmOut = grm.Parse("funcBody", languageContent);
            if (grmOut == null)
            {
                Console.WriteLine("Wrong language text");
            }
            else
            {
                Console.WriteLine(grmOut.ToString());
            }*/
            //Console.WriteLine("-= ORsharp =-");
            var path = args.Length == 0 ? "C:\\Users\\User\\Desktop\\MAI\\FP-23\\Test.osp" : args[0];
            var languageContent = System.IO.File.ReadAllText(path);
            var program = new Base.Executor.Program(languageContent);
            var exitCode = program.Execute();
            return exitCode;
        }
    }
}
