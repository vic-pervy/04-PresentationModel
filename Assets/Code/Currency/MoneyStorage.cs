using System;

namespace Code
{
    public sealed class MoneyStorage:CurrencyStorage
    {
        public MoneyStorage(long money) : base(money) { }
    }
}
