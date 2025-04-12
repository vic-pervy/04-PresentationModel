using UnityEngine;

namespace Code
{
    public interface IProductInfo
    {
        public Sprite Icon { get; }
        public string Title { get; }
        public string Description { get; }
        public int MoneyPrice { get; }
    }

    [CreateAssetMenu(fileName = "Product", menuName = "Data/New Product")]
    public sealed class ProductInfo : ScriptableObject, IProductInfo
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title; 
        [SerializeField] [TextArea(3, 5)] private string _description;
        [Space]
        [SerializeField] private int _moneyPrice;

        public Sprite Icon => _icon; 
        public string Title => _title; 
        public string Description => _description;
        public int MoneyPrice => _moneyPrice;
    }
}
