using Zenject;
using System;
using UniRx;
using System.Collections.Generic;

namespace Code
{
    public sealed class ProductListItemPresenter : IDisposable
    {
        private ProductListItemView _view;
        private IProductInfo _productInfo;
        private ProductBuyer _productBuyer;
        private MoneyStorage _moneyStorage;
        private PopupManager _popupManager;
        
        private List<IDisposable> _disposables = new();

        public ProductListItemView View => _view;

        public ProductListItemPresenter(IProductInfo productInfo, ProductListItemView view, ProductBuyer productBuyer, MoneyStorage moneyStorage, PopupManager popupManager)
        {
            _popupManager = popupManager;
            _moneyStorage = moneyStorage;
            _moneyStorage = moneyStorage;
            _productBuyer = productBuyer;
            _view = view;
            _productInfo = productInfo;
            _view.SetIcon(productInfo.Icon);
            _view.SetTitile(productInfo.Title);
            _view.SetDescription(productInfo.Description);
            _view.SetPrice(productInfo.MoneyPrice.ToString());

            _view.ButtonBuy.onClick.AddListener(Buy);

            _disposables.Add( moneyStorage.Count.Subscribe(count=> MoneyStorage_OnMoneyChanged(count)));

        }

        private void MoneyStorage_OnMoneyChanged(long obj)
        {
            _view.ButtonBuy.interactable = _productBuyer.CanBuy(_productInfo);
        }

        private void Buy()
        {
            if (_productBuyer.CanBuy(_productInfo))
                _popupManager.CreateProductPopup(_productInfo);
            //_productBuyer.Buy(_productInfo);
        }

        public void Dispose()
        {
            _view.ButtonBuy.onClick.RemoveListener(Buy);
            foreach (var disposable in _disposables)
                disposable.Dispose();

        }

        public class Factory : PlaceholderFactory<IProductInfo, ProductListItemView,  ProductListItemPresenter>
        {

        }
    }
}
