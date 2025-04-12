using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class ProductHelper : MonoBehaviour
    {
        [SerializeField] private ProductInfo _productInfo;
        [SerializeField] private ProductPopup _productPopup;

        
        private ProductBuyer _buyer;
        private MoneyStorage _moneyStorage;

        [Inject]
        private void Construct(ProductBuyer buyer, MoneyStorage moneyStorage)
        {
            _buyer = buyer;
            _moneyStorage = moneyStorage;
        }


        public void ProductPopupShow()
        {
            var viewModel = new ProductPopupModel(_productInfo, _buyer, _moneyStorage);
            _productPopup.Show(viewModel);
        }

    }
}
