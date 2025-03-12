using System.Reflection;

namespace SharedKernel.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject? obj1, ValueObject? obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;
        if (obj1 is null || obj2 is null) return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(ValueObject obj1, ValueObject obj2) => !(obj1 == obj2);

    public bool Equals(ValueObject? other) =>
        other is not null && Equals(other as object);

    public override bool Equals(object? obj) =>
        obj is not null && GetType() == obj.GetType()
&& GetProperties().All(p => PropertiesAreEqual(obj, p))
            && GetFields().All(f => FieldsAreEqual(obj, f));

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            foreach (var prop in GetProperties())
            {
                var value = prop.GetValue(this, null);
                hash = HashValue(hash, value);
            }

            foreach (var field in GetFields())
            {
                var value = field.GetValue(this);
                hash = HashValue(hash, value);
            }

            return hash;
        }
    }

    protected static void CheckRule(IValidationRule rule)
    {
        if (rule.IsBroken()) throw new ValidationRuleException(rule.Message);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new BusinessRuleException(rule.Message);
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo p) => Equals(p.GetValue(this, null), p.GetValue(obj, null));
    private bool FieldsAreEqual(object obj, FieldInfo f) => Equals(f.GetValue(this), f.GetValue(obj));
    private List<PropertyInfo> GetProperties() =>
    [.. GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)];

    private List<FieldInfo> GetFields() =>
        [.. GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)];

    private static int HashValue(int seed, object? value)
    {
        var currentHash = value?.GetHashCode() ?? 0;
        return (seed * 23) + currentHash;
    }
}