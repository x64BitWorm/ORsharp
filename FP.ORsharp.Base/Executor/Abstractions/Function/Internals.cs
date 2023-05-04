using FP.ORsharp.Base.Executor.Abstractions.Function.Internal;

namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public static class Internals
    {
        public static IFunction GetByName(string name)
        {
            return name switch
            {
                "plus" => new PlusFucntion(),
                "minus" => new MinusFucntion(),
                "mul" => new MulFucntion(),
                "div" => new DivFucntion(),
                "if" => new IfFunction(),
                "bigger" => new BiggerFunction(),
                "equal" => new EqualFunction(),
                "print" => new PrintFunction(),
                "input" => new InputFunction(),
                "sleep" => new SleepFunction(),
                "break" => new BreakFunction(),
                "while" => new WhileFunction(),
                "or" => new OrFunction(),
                _ => null,
            };
        }
    }
}
