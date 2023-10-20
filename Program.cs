using SmartEnum;

CreditCard card = CreditCard.Gold;

Console.WriteLine($"Discount fo {card} is {card.Discount:P}");