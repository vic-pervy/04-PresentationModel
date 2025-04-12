using System.Collections;
using Code;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;

public class TestPurchase 
{
    public class MockProductPopup
    {
        private IProductPopupModel _viewModel;
        public void Show(IProductPopupModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Buy()
        {
            _viewModel.Buy();
        }
    }

    public class MockProductInfo : IProductInfo
    {
        public Sprite Icon => null;

        public string Title => "Test product";

        public string Description => "Test product desctiption";

        public int MoneyPrice => 10;
    }


    [Test]
    public void TestPurchaseSimplePasses()
    {
        var productInfo = new MockProductInfo();

        var moneyStorage = new MoneyStorage(13);
        var buyer = new ProductBuyer(moneyStorage);
        var purchaseWindow = new MockProductPopup();
        var viewModel = new ProductPopupModel(productInfo, buyer, moneyStorage);
        purchaseWindow.Show(viewModel);

        Assert.AreEqual(13, moneyStorage.Count.Value);
        Assert.IsTrue(viewModel.BuyButtonIsInteractable.Value);

        purchaseWindow.Buy();

        Assert.AreEqual(3, moneyStorage.Count.Value);
        Assert.IsFalse(viewModel.BuyButtonIsInteractable.Value);

    }

}
