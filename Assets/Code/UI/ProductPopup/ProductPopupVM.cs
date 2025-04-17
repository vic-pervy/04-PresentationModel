using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public class ProductPopupVM : IProductPopupModel
    {
        public string Title => ProductInfo.Title;

        public string Description => ProductInfo.Description;

        public Sprite Icon => ProductInfo.Icon;

        public string Price => ProductInfo.MoneyPrice.ToString();

        public IReadOnlyReactiveProperty<bool> BuyButtonIsInteractable => _buyButtonIsInteractable;
        private ReactiveProperty<bool> _buyButtonIsInteractable = new ReactiveProperty<bool>();

        public IProductInfo ProductInfo { get; private set; }

        private List<IDisposable> _disposables = new();
        private List<GameObject> _listeners = new();
        

        public event Action StateChanged;

        private ProductBuyer _productBuyer;
        private MoneyStorage _moneyStorage;

        public ProductPopupVM(IProductInfo productInfo, ProductBuyer productBuyer, MoneyStorage moneyStorage)
        {
            ProductInfo = productInfo;
            _productBuyer = productBuyer;
            _moneyStorage = moneyStorage;
        }

        public void Buy()
        {
            _productBuyer.Buy(ProductInfo);
        }

        public void AddListener(GameObject o)
        {
            if (_listeners.Count == 0) AwakeSubscriptions();
            _listeners.Add(o);
        }

        public void RemoveListener(GameObject o)
        {
            _listeners.Remove(o);
            if (_listeners.Count == 0) SleepSubscriptions();
        }

        public void AwakeSubscriptions()
        {
            var subsctiprion = _moneyStorage.Count.Subscribe(count =>
            {
                Debug.Log("_buyButtonIsInteractable Update");
                _buyButtonIsInteractable.Value = _productBuyer.CanBuy(ProductInfo);
            });
            _disposables.Add(subsctiprion);
        }
        
        public void SleepSubscriptions()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }

        public void Dispose()
        {
            SleepSubscriptions();
        }
    }
}
