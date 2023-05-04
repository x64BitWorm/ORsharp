using FP.ORsharp.Base.Grammar;

namespace FP.ORsharp.Base.Executor.Abstractions.Operation
{
    class Comment : IDerivable, IOperation
    {
        private string _text;

        public void Derive(Lexem lexem)
        {
            lexem = lexem.Childs["entry"];
            _text = lexem.Childs["text"].Name;
        }

        public Context Process(Context context)
        {
            return context;
        }

        public IOperation Clone()
        {
            return new Comment
            {
                _text = _text
            };
        }

        public override string ToString()
        {
            return $"{{'comment': '{_text}'}}";
        }
    }
}
