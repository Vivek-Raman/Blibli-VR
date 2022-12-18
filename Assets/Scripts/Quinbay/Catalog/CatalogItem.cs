using UnityEngine;

namespace Quinbay.Catalog.Data
{
    [CreateAssetMenu(fileName = FileName)]
    public class CatalogItem : ScriptableObject
    {
        [SerializeField] private string itemSku;
        [SerializeField] private GameObject prefab;
        [SerializeField][Range(0.8f, 1.5f)] private float interactionTriggerScale = 1f;

        public string ItemSku => itemSku;
        public GameObject Prefab => prefab;
        public float InteractionTriggerScale => interactionTriggerScale;

        public const string FileName = "CatalogItem_DoNotRename";
    }
}
