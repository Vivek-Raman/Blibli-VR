using UnityEngine;

namespace Data
{
    [CreateAssetMenu]
    public class CatalogItem : ScriptableObject
    {
        [SerializeField] private string itemSku;
        [SerializeField] private GameObject prefab;

        public string ItemSku => itemSku;
        public GameObject Prefab => prefab;
    }
}
