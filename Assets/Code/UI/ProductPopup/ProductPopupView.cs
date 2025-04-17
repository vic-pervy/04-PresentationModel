using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProductPopupView : MonoBehaviour
    {
        public event Action<GameObject> OnClosePopup;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _closeButton;

        private IProductPopupModel _viewModel;

        private List<IDisposable> _disposables = new();


        public void Show(IProductPopupModel viewModel)
        {
            gameObject.SetActive(true);
            _closeButton.onClick.AddListener(Hide);
            _buyButton.onClick.AddListener(Buy);

            _viewModel = viewModel;

            _title.text = viewModel.Title;
            _description.text = viewModel.Description;
            _icon.sprite = viewModel.Icon;
            _price.text = viewModel.Price;

            _viewModel.AddListener(gameObject);
            _disposables.Add(_viewModel.BuyButtonIsInteractable.SubscribeToInteractable(_buyButton));
        }

        private void Buy()
        {
            _viewModel.Buy();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
            _buyButton.onClick.RemoveListener(Buy);
            foreach (var disposable in _disposables)
                disposable.Dispose();
            _viewModel.RemoveListener(gameObject);
            OnClosePopup?.Invoke(gameObject);
        }
    }
}