﻿using System.Reflection;

namespace SharedKernel.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    private List<PropertyInfo> _properties = [];
    private readonly List<FieldInfo> _fields = [];

    public static bool operator ==(ValueObject? obj1, ValueObject? obj2) => obj1?.Equals(obj2) ?? Equals(obj2, null);

    public static bool operator !=(ValueObject? obj1, ValueObject? obj2) => !(obj1 == obj2);

    public bool Equals(ValueObject? obj) => Equals(obj as object);

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return GetProperties().All(p => PropertiesAreEqual(obj, p))
               && GetFields().All(f => FieldsAreEqual(obj, f));
    }

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

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo p) => Equals(p.GetValue(this, null), p.GetValue(obj, null));

    private bool FieldsAreEqual(object obj, FieldInfo f) => Equals(f.GetValue(this), f.GetValue(obj));

    private List<PropertyInfo> GetProperties()
    {
        if (_properties.Count == 0)
        {
            _properties = GetType()
                .GetProperties(BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !p.IsDefined(typeof(IgnoreMemberAttribute)))
                .ToList();
        }

        return _properties;
    }

    private List<FieldInfo> GetFields() => _fields;

    private static int HashValue(int seed, object? value)
    {
        var currentHash = value?.GetHashCode() ?? 0;
        return seed * 23 + currentHash;
    }
}