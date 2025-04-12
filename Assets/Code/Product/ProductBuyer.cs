using UniRx;
using UnityEngine;

namespace Code
{
    public sealed class ProductBuyer
    {
        private readonly MoneyStorage _moneyStorage;

        public ProductBuyer(MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }

        public void Buy(IProductInfo product)
        {
            if (CanBuy(product))
            {
                _moneyStorage.Spend(product.MoneyPrice);
                Debug.Log($"<color=green>Product {product.Title} successfully purchased!</color>");
            }
            else
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.Title}!</color>");
            }
        }

        public bool CanBuy(IProductInfo product)
        {
            return _moneyStorage.Count.Value >= product.MoneyPrice;
        }
    }
}
