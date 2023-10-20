// author Milan Jovanovic

using System.Reflection;

namespace SmartEnum;

internal abstract class CreditCard : Enumerator<CreditCard>
{
    public static readonly CreditCard Standard = new StandardCreditCard(); 
    public static readonly CreditCard Premium = new PremiumCreditCard();
    public static readonly CreditCard Platinum = new PlatinumCreditCard();
    public static readonly CreditCard Gold = new GoldCreditCard();

    private CreditCard(int value, string name) : base(value, name) { }

    public abstract double Discount { get; }

    private sealed class StandardCreditCard : CreditCard
    {
        public StandardCreditCard() : base(1, "Standard") { }
        public override double Discount => 0.01;
    }

    private sealed class PremiumCreditCard : CreditCard
    {
        public PremiumCreditCard() : base(2, "Premium") { }
        public override double Discount => 0.05;
    }

    private sealed class PlatinumCreditCard : CreditCard
    {
        public PlatinumCreditCard() : base(3, "Platinum") { }
        public override double Discount => 0.1;
    }

    private sealed class GoldCreditCard : CreditCard
    {
        public GoldCreditCard() : base(4, "Gold") { }
        public override double Discount => 0.2;
    }
}

internal abstract class Enumerator<TEnum> : IEquatable<Enumerator<TEnum>>
    where TEnum : Enumerator<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    public int Value { get; protected init; }
    public string Name { get; protected init; }

    protected Enumerator(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static TEnum? FromValue(int value)
    {
        return Enumerations.TryGetValue(value, out TEnum? result) ?
            result : default;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(item => item.Name == name);
    }

    public bool Equals(Enumerator<TEnum>? other)
    {
        return other != null &&
            GetType() == other.GetType() &&
            Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && Equals(obj as Enumerator<TEnum>);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        Type enumerationType = typeof(TEnum);
        IEnumerable<TEnum> fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
        return fieldsForType.ToDictionary(key => key.Value);
    }
}
