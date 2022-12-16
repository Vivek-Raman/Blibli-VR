using Quinbay.API;
using UnityEngine;

public class LoadAssetFromBundle : MonoBehaviour
{
    [SerializeField] private APIClient apiClient;

    [ContextMenu(nameof(CallAPI))]
    private void CallAPI()
    {
        Debug.Log(apiClient.DownloadAssetBundles());
    }
}
