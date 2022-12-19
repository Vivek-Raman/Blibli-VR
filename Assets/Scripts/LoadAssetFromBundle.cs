using System;
using Quinbay.API;
using Quinbay.Assets;
using UnityEngine;

public class LoadAssetFromBundle : MonoBehaviour
{
    [SerializeField] private string itemSku = "VIS-70001-00004-00001";
    
    [SerializeField] private AssetBundlePrefabManager assetBundlePrefabManager;

    private void Start()
    {
        Invoke(nameof(Test), 2f);
    }

    private void Test()
    {
        assetBundlePrefabManager.InstantiateItemFromCatalog(itemSku).transform
            .SetPositionAndRotation(transform.position, transform.rotation);
    }
}
