using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code
{
    public sealed class ProductHelper : MonoBehaviour
    {
        [SerializeField] private ProductInfo _productInfo;
        [FormerlySerializedAs("_productPopup")] [SerializeField] private ProductPopupView productPopupView;

        
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
            var viewModel = new ProductPopupVM(_productInfo, _buyer, _moneyStorage);
            productPopupView.Show(viewModel);
        }

    }
}
