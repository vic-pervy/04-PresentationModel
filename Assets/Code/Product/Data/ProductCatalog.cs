using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public interface IProductCatalog
    {
        public IReadOnlyList<IProductInfo> Products { get; }
    }

    [CreateAssetMenu(fileName = "ProductCatalog", menuName = "Data/New ProductCatalog")]
    public sealed class ProductCatalog : ScriptableObject, IProductCatalog
    {
        public ProductInfo[] Products;

        IReadOnlyList<IProductInfo> IProductCatalog.Products => Products;
    }
}
