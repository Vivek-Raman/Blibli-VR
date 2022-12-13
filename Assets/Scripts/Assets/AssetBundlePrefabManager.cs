using System.Collections;
using System.Collections.Generic;
using System.IO;
using API;
using Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets
{
    public class AssetBundlePrefabManager : MonoBehaviour
    {
        [SerializeField] private APIClient apiClient = null;

        #region Download Item Catalog

        private readonly Dictionary<string, CatalogItem> _catalog = new();

        private async void Start()
        {
            await apiClient.DownloadAssetBundles();
            StartCoroutine(LoadAllDownloadedBundles());
        }

        private void OnDestroy()
        {
            AssetBundle.UnloadAllAssetBundles(false);
        }

        private IEnumerator LoadAllDownloadedBundles()
        {
            List<Coroutine> coroutines = new List<Coroutine>();
            foreach (string filePath in Directory.GetFiles(Application.streamingAssetsPath))
            {
                if (filePath.EndsWith(".meta")) continue;
                coroutines.Add(StartCoroutine(LoadBundleFromFilePath(filePath)));
            }
            foreach (Coroutine coroutine in coroutines)
            {
                yield return coroutine;
            }
        }

        private IEnumerator LoadBundleFromFilePath(string filePath)
        {
            AssetBundleCreateRequest loader = AssetBundle.LoadFromFileAsync(filePath);
            yield return loader;
            AssetBundle bundle = loader.assetBundle;
            AssetBundleRequest assetRequest = bundle.LoadAssetAsync<CatalogItem>("CatalogItem");
            yield return assetRequest;
            CatalogItem item = assetRequest.asset as CatalogItem;
            _catalog.Add(item.ItemSku, item);
        }

        #endregion

        public GameObject InstantiateItemFromCatalog(string itemSku)
        {
            return Instantiate(GetItemFromCatalog((itemSku))?.Prefab);
        }

        [CanBeNull]
        private CatalogItem GetItemFromCatalog(string itemSku)
        {
            return _catalog.ContainsKey(itemSku) ? _catalog[itemSku] : null;
        }
    }
}
