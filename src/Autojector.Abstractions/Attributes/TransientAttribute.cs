namespace Autojector.Abstractions;

public class TransientAttribute : BaseInjectionAttribute
{
    public TransientAttribute(Type abstractionType) : base(abstractionType)
    {
    }
}
