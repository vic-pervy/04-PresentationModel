using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Object = UnityEngine.Object;

namespace Code
{
    public class PopupManager : MonoBehaviour
    {
        public ShopPopupView shopPopupViewPrefab;
        public ProductPopupView ProductPopupViewPrefab;

        public class PopupFactory
        {
            DiContainer _container;

            [Inject]
            public PopupFactory(DiContainer container)
            {
                _container = container;
            }

            Dictionary<Type, Dictionary<object, object>> _viewModelsWithArgs = new();

            public VM CreatePopupViewModelWithArgs<VM>(params object[] args)
            {
                if (!_viewModelsWithArgs.ContainsKey(typeof(VM))) _viewModelsWithArgs.Add(typeof(VM), new Dictionary<object, object>());
                if (_viewModelsWithArgs[typeof(VM)].ContainsKey(args[0])) return (VM)_viewModelsWithArgs[typeof(VM)][args[0]];
                var viewModel = _container.Instantiate(typeof(VM), new[] { args[0] });
                _viewModelsWithArgs[typeof(VM)].Add(args[0], viewModel);
                return (VM)viewModel;
            }

            Dictionary<Type, object> _viewModels = new Dictionary<Type, object>();

            public VM CreatePopupViewModel<VM>()
            {
                if (_viewModels.ContainsKey(typeof(VM))) return (VM)_viewModels[typeof(VM)];
                var viewModel = _container.Instantiate(typeof(VM));
                _viewModels.Add(typeof(VM), viewModel);
                return (VM)viewModel;
            }

            public TPresenter CreatePopupPresenter<TPresenter>()
            {
                var presenter = _container.Instantiate(typeof(TPresenter));
                return (TPresenter)presenter;
            }

            public TView CreatePopupView<TView>(TView prefab) where TView : MonoBehaviour
            {
                var view = _container.InstantiatePrefab(prefab).GetComponent<TView>();
                return view;
            }
        }

        PopupFactory _factory;

        [Inject]
        void Construct(PopupFactory factory)
        {
            _factory = factory;
        }

        public void CreateShopPopup()
        {
            var shopPresenter = _factory.CreatePopupPresenter<ShopPresenter>();
            var shopView = _factory.CreatePopupView<ShopPopupView>(shopPopupViewPrefab);
            shopView.transform.SetParent(transform, false);
            shopPresenter.OnClosePopup += DestroyShopPopup;
            shopPresenter.Show(shopView);
        }

        public void DestroyShopPopup(GameObject obj)
        {
            Object.Destroy(obj);
        }

        public void CreateProductPopup(IProductInfo productInfo)
        {
            var shopViewModel = _factory.CreatePopupViewModelWithArgs<ProductPopupVM>(productInfo);
            var shopView = _factory.CreatePopupView<ProductPopupView>(ProductPopupViewPrefab);
            shopView.transform.SetParent(transform, false);
            shopView.OnClosePopup += DestroyProductPopup;
            shopView.Show(shopViewModel);
        }

        public void DestroyProductPopup(GameObject obj)
        {
            Object.Destroy(obj);
        }
    }
}