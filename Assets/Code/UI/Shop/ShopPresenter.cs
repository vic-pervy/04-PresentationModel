using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    /*public interface IShopPresenter
    {
        void Show();
    }*/
    public class ShopPresenter : IDisposable
    {
        public event Action<GameObject> OnClosePopup;
        
        private ShopPopupView _popupView;
        private IProductCatalog _catalog;
        IPrefabsProvider _prefabsProvider;
        ProductListItemPresenter.Factory _listItemsFactory;

        private List<ProductListItemPresenter> _spawnedListItems = new();

        public ShopPresenter(IProductCatalog catalog, IPrefabsProvider prefabsProvider, ProductListItemPresenter.Factory listItemsFactory)
        {
            //_view = view;
            _catalog = catalog;
            _prefabsProvider = prefabsProvider;
            _listItemsFactory = listItemsFactory;
        }

        public void Show(ShopPopupView popupView)
        {
            _popupView = popupView;
            
            _popupView.Show();

            _popupView.Closed += PopupViewClosed;

            foreach(var product in _catalog.Products)
            {
                var listItemViewInstance = GameObject.Instantiate(_prefabsProvider.GetProductListItemView());
                var listItemPresenter = _listItemsFactory.Create(product, listItemViewInstance);
                _spawnedListItems.Add(listItemPresenter);
            }


            _popupView.SetProducts(_spawnedListItems.Select(p=> p.View).ToList());
        }

        private void PopupViewClosed()
        {
            _popupView.Closed -= PopupViewClosed;
            foreach(var listItem in _spawnedListItems)
            {
                listItem.Dispose();
            }
            _spawnedListItems.Clear();
            Dispose();
            OnClosePopup?.Invoke(_popupView.gameObject);
        }

        public void Dispose()
        {
            
        }
    }
}
