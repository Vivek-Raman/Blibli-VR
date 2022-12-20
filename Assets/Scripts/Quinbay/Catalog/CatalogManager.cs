using System;
using JetBrains.Annotations;
using Quinbay.Assets;
using UnityEngine;

namespace Quinbay.Catalog
{
    public class CatalogManager : MonoBehaviour
    {
        [SerializeField] private AssetBundlePrefabManager assetBundlePrefabManager;

        [CanBeNull] private Item _hoveredItem = null;

        private void OnEnable()
        {
            Item.OnItemHovered += HandleItemHovered;
        }

        private void OnDisable()
        {
            Item.OnItemHovered -= HandleItemHovered;
        }

        private void HandleItemHovered(Item hoveredItem)
        {
            _hoveredItem = hoveredItem;
        }
    }
}