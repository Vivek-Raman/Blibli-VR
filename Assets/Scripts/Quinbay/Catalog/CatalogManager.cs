using System.Collections;
using JetBrains.Annotations;
using Quinbay.API;
using Quinbay.UI;
using UnityEngine;

namespace Quinbay.Catalog
{
    public class CatalogManager : MonoBehaviour
    {
        [SerializeField] private BlibliClient blibliClient;
        [SerializeField] private ProductDetailsUIController productDetailsUIController;

        [CanBeNull] private Item lastHoveredItem = null;
        [CanBeNull] private string currentItemSku = null;

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
            if (hoveredItem.CatalogItem.ItemSku.Equals(currentItemSku))
            {
                return;
            }

            lastHoveredItem = hoveredItem;
            currentItemSku = hoveredItem.CatalogItem.ItemSku;
            StartCoroutine(nameof(FetchProductDetailsForItem));
        }

        private IEnumerator FetchProductDetailsForItem()
        {
            // hide canvas
            productDetailsUIController.HideProductDetails();
            productDetailsUIController.ResetProductDetails();

            // reposition canvas
            Transform hoveredItemTransform = lastHoveredItem.transform;
            Vector3 offset = (lastHoveredItem.GetComponent<BoxCollider>().bounds.extents.y + 0.2f)
                             * lastHoveredItem.CatalogItem.InteractionTriggerScale * hoveredItemTransform.up;
            productDetailsUIController.SetFollowTargetAndOffset(hoveredItemTransform, offset);

            // yield return blibliClient.FetchProductDetailsForItemSku(lastHoveredItem.CatalogItem, response =>
            // {
                // populate UI with product details
                productDetailsUIController.SetProductDetails(lastHoveredItem.CatalogItem.ProductInfo);

                // show canvas
                productDetailsUIController.ShowProductDetails();
            // });
            yield break;
        }

        #region Debug

        [SerializeField] private Item debugItem;

        [ContextMenu(nameof(Debug_ForceHoverOn))]
        private void Debug_ForceHoverOn()
        {
            lastHoveredItem = debugItem;
            currentItemSku = debugItem.CatalogItem.ItemSku;
            StartCoroutine(nameof(FetchProductDetailsForItem));
        }

        #endregion
    }
}