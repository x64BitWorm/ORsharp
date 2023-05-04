namespace FP.ORsharp.Base.Executor.Abstractions.Function
{
    public interface IFunction: IClonable<IFunction>
    {
        public IFunction PassArgument(Context context, IFunction argument);

        public virtual string ToString(Context context, int depth)
        {
            return GetType().Name;
        }
    }
}
