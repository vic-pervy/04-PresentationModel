using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    public interface IShopPresenter
    {
        void Show();
    }
    public class ShopPresenter : IShopPresenter
    {
        private ShopView _view;
        private IProductCatalog _catalog;
        IPrefabsProvider _prefabsProvider;
        ProductListItemPresenter.Factory _listItemsFactory;

        private List<ProductListItemPresenter> _spawnedListItems = new();

        public ShopPresenter(ShopView view, IProductCatalog catalog, IPrefabsProvider prefabsProvider, ProductListItemPresenter.Factory listItemsFactory)
        {
            _view = view;
            _catalog = catalog;
            _prefabsProvider = prefabsProvider;
            _listItemsFactory = listItemsFactory;
        }

        public void Show()
        {
            _view.Show();

            _view.Closed += View_Closed;

            foreach(var product in _catalog.Products)
            {
                var listItemViewInstance = GameObject.Instantiate(_prefabsProvider.GetProductListItemView());
                var listItemPresenter = _listItemsFactory.Create(product, listItemViewInstance);
                _spawnedListItems.Add(listItemPresenter);
            }


            _view.SetProducts(_spawnedListItems.Select(p=> p.View).ToList());
        }

        private void View_Closed()
        {
            _view.Closed -= View_Closed;
            foreach(var listItem in _spawnedListItems)
            {
                listItem.Dispose();
            }
            _spawnedListItems.Clear();
        }
    }
}
