using UnityEngine;
using Zenject;

namespace Code
{

    public sealed class LevelInstaller : MonoInstaller
    {
        [SerializeField] private PopupManager _popupManager;
        [SerializeField] private CurrencyView _gemView;
        [SerializeField] private CurrencyView _moneyView;
        [SerializeField] private ProductHelper _productHelper;
        [SerializeField] private ButtonView _shopButtonView;
    

        //[SerializeField] private ShopView _productsListView;

        [SerializeField] private ProductCatalog _productCatalog;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProductCatalog>().FromInstance(_productCatalog).AsSingle();

            BindTopLevelUI();

            Container.BindInterfacesAndSelfTo<PopupManager.PopupFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupManager>().FromInstance(_popupManager).AsSingle();
            //Container.BindInstance(_productsListView).AsSingle();
            //Container.BindInterfacesTo<ProductPopupVM>().AsSingle();
            //Container.BindInterfacesTo<ShopPresenter>().AsSingle();
            Container.BindFactory<IProductInfo, ProductListItemView, ProductListItemPresenter, ProductListItemPresenter.Factory>().FromNew(); 

            Container.Bind<ProductBuyer>().AsSingle();

            Container.BindInstance(_productHelper).AsSingle();

        }

        private void BindTopLevelUI()
        {
            MoneyBind();
            GemBind();

            Container.Bind<MarketButtonPresenter>().AsSingle().NonLazy();
            Container.BindInstance(_shopButtonView).WhenInjectedInto<MarketButtonPresenter>();
        }

      

        private void MoneyBind()
        {
            Container.BindInstance(_moneyView).WhenInjectedInto<MoneyObserver>();
            Container
                .Bind<MoneyStorage>()
                .AsSingle()
                .WithArguments(10l)
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<MoneyObserver>()
                .AsSingle()
              //  .WithArguments(view)
                .NonLazy(); 
        }

        private void GemBind()
        {
            Container.BindInstance(_gemView).WhenInjectedInto<GemObserver>();
            Container
                .Bind<GemStorage>()
                .AsSingle()
                .WithArguments(1l)
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GemObserver>()
                .AsSingle()
             //   .WithArguments(view)
                .NonLazy(); 
        }
    }
}
