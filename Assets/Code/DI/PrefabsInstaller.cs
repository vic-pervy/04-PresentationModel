using System;
using Code;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName ="UIPrefabs", menuName ="Create/UIPrefabs")]
public class PrefabsInstaller : ScriptableObjectInstaller
{

    [SerializeField]
    private UIPrefabs _uiPrefabs;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<UIPrefabs>().FromInstance(_uiPrefabs);
    }
}

[Serializable]
public class UIPrefabs : IPrefabsProvider
{
    public ProductListItemView _productsListItemPrefab;

    public ProductListItemView GetProductListItemView()
    {
        return _productsListItemPrefab;
    }
}
public interface IPrefabsProvider
{
    public ProductListItemView GetProductListItemView();
}