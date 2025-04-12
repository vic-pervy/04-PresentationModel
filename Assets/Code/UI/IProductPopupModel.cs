using System;
using UniRx;
using UnityEngine;

namespace Code
{
    public interface IProductPopupModel : IDisposable
    {
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
        string Price { get; }
        IReadOnlyReactiveProperty<bool> BuyButtonIsInteractable { get; }
        IProductInfo ProductInfo { get; }


        void Buy();
    }
}
