using Quinbay.Assets;
using UnityEngine;

namespace Quinbay.Catalog
{
    public class CatalogManager : MonoBehaviour, ICatalogManager
    {
        [SerializeField] private AssetBundlePrefabManager assetBundlePrefabManager;
        
        public void GetCategoryList()
        {
            throw new System.NotImplementedException();
        }
    }
}