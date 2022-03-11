namespace Autojector.Abstractions;

public abstract class BaseInjectionAttribute : Attribute {
    public BaseInjectionAttribute(Type abstractionType)
    {
        this.AbstractionType = abstractionType;
    }

    public Type AbstractionType { get; }
}
