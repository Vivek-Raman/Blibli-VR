using System;
using Quinbay.API;
using Quinbay.Assets;
using UnityEngine;

public class LoadAssetFromBundle : MonoBehaviour
{
    [SerializeField] private AssetBundlePrefabManager assetBundlePrefabManager;

    private void Start()
    {
        Invoke(nameof(Test), 2f);
    }

    private void Test()
    {
        assetBundlePrefabManager.InstantiateItemFromCatalog("VIS-70001-00004-00001");
    }
}
