using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ShopPopupView : MonoBehaviour 
    {
        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private Transform _content;

        public event Action Closed;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            Closed?.Invoke();
        }

        public void SetProducts(IReadOnlyList<ProductListItemView> items)
        {
            foreach(var listItem in items)
            {
                listItem.transform.SetParent(_content); 
            }
        }

    }
}
