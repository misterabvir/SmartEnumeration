﻿// author Milan Jovanovic

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
