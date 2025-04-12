namespace Code
{
    public sealed class GemObserver : CurrencyObserver
    {
        public GemObserver(GemStorage storage, CurrencyView view):base(storage, view) { }
    }
}
