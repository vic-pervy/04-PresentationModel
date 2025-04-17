namespace Code
{
    public sealed class MoneyObserver : CurrencyObserver
    {
        public MoneyObserver(MoneyStorage storage, CurrencyView view):base(storage, view)
        {
        }
    }
}
