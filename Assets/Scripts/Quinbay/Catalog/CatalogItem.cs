using Quinbay.API.Response;
using UnityEngine;

namespace Quinbay.Catalog.Data
{
    [CreateAssetMenu(fileName = FileName)]
    public class CatalogItem : ScriptableObject
    {
        [SerializeField] private string itemSku;
        [SerializeField] private string pickupPointCode;
        [SerializeField] private GameObject prefab;
        [SerializeField][Range(0.8f, 1.5f)] private float interactionTriggerScale = 1f;

        public string ItemSku => itemSku;
        public string PickupPointCode => pickupPointCode;
        public GameObject Prefab => prefab;
        public float InteractionTriggerScale => interactionTriggerScale;

        public const string FileName = "CatalogItem_DoNotRename";
    }
}
