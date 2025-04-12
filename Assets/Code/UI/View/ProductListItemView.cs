using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ProductListItemView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private TMP_Text _description;
        [SerializeField]
        private TMP_Text _price;
        [SerializeField]
        private Button _buttonBuy;

        public Button ButtonBuy => _buttonBuy;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetTitile(string  title)
        {
            _title.text = title;
        }

        public void SetPrice(string price)
        {
            _price.text = price;
        }
        public void SetDescription(string description)
        {
            _description.text = description;
        }
    }
}
