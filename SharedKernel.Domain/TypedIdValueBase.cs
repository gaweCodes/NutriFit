namespace SharedKernel.Domain;

public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Value { get; }

    protected TypedIdValueBase(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidOperationException("Id value cannot be empty!");
        Value = value;
    }

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || (obj is TypedIdValueBase other && Equals(other));

    public bool Equals(TypedIdValueBase? other) =>
        other is not null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(TypedIdValueBase? obj1, TypedIdValueBase? obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;
        if (obj1 is null || obj2 is null) return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(TypedIdValueBase? obj1, TypedIdValueBase? obj2) => !(obj1 == obj2);
}
