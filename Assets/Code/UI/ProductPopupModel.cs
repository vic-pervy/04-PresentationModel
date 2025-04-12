using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

namespace Code
{
    public class ProductPopupModel : IProductPopupModel
    {
        public string Title => ProductInfo.Title;

        public string Description => ProductInfo.Description;

        public Sprite Icon => ProductInfo.Icon;

        public string Price => ProductInfo.MoneyPrice.ToString();

        public IReadOnlyReactiveProperty<bool> BuyButtonIsInteractable => _buyButtonIsInteractable;
        private ReactiveProperty<bool> _buyButtonIsInteractable = new ReactiveProperty<bool>();

        public IProductInfo ProductInfo { get; private set; }

        public List<IDisposable> _disposables = new();
        

        public event Action StateChanged;

        private ProductBuyer _productBuyer;
        private MoneyStorage _moneyStorage;

        public ProductPopupModel(IProductInfo productInfo, ProductBuyer productBuyer, MoneyStorage moneyStorage)
        {
            ProductInfo = productInfo;
            _productBuyer = productBuyer;
            _moneyStorage = moneyStorage;

            var subsctiprion = moneyStorage.Count.Subscribe(count => _buyButtonIsInteractable.Value = productBuyer.CanBuy(ProductInfo));
            _disposables.Add(subsctiprion);
        }

        public void Buy()
        {
            _productBuyer.Buy(ProductInfo);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
