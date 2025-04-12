using System;
using UniRx;

namespace Code
{
    public abstract class CurrencyStorage
    {
        public IReadOnlyReactiveProperty<long> Count => _count;
        private ReactiveProperty<long> _count = new ReactiveProperty<long>();

        public CurrencyStorage(long gem)
        {
            _count.Value = gem; 
        }

        public void Add(long count)
        {
            _count.Value += count;
        }

        public void Spend(long count)
        {
            if (_count.Value < count)
            {
                throw new InvalidOperationException($"");
            }
            _count.Value -= count;
        }

        public bool CanSpend(long count)
        {
            return _count.Value >= count;
        }
    }
}
