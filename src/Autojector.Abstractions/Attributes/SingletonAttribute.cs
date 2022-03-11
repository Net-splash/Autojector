namespace Autojector.Abstractions;

public class SingletonAttribute : BaseInjectionAttribute
{
    public SingletonAttribute(Type abstractionType) : base(abstractionType)
    {
    }
}
