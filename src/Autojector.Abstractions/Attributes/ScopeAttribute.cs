namespace Autojector.Abstractions;

public class ScopeAttribute : BaseInjectionAttribute
{
    public ScopeAttribute(Type abstractionType) : base(abstractionType)
    {
    }
}
