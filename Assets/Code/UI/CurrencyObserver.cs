using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Code
{
    public abstract class CurrencyObserver: IInitializable, IDisposable
    {
        private readonly CurrencyView _view;

        private readonly CurrencyStorage _storage;

        private List<IDisposable> _disposables = new();

        public CurrencyObserver(CurrencyStorage storage, CurrencyView view)
        {
            _storage = storage;
            _view = view;
        }

        public void Initialize()
        {
            _disposables.Add(_storage.Count.Subscribe(value => _view.UpdateCurrency(value)));
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }

    }
}
