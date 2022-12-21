using System.Threading.Tasks;
using JetBrains.Annotations;
using Quinbay.API;
using Quinbay.API.Response;
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

            FetchProductDetailsForItem(hoveredItem);
        }

        private async Task FetchProductDetailsForItem(Item hoveredItem)
        {
            lastHoveredItem = hoveredItem;
            currentItemSku = hoveredItem.CatalogItem.ItemSku;

            // hide canvas
            productDetailsUIController.HideProductDetails();
            productDetailsUIController.ResetProductDetails();

            // reposition canvas
            Transform hoveredItemTransform = hoveredItem.transform;
            Vector3 offset = (hoveredItem.GetComponent<BoxCollider>().bounds.extents.z + 0.2f)
                             * hoveredItem.CatalogItem.InteractionTriggerScale * hoveredItemTransform.up;
            productDetailsUIController.SetFollowTargetAndOffset(hoveredItemTransform, offset);

            // populate UI with product details
            ProductSummaryResponse response = await blibliClient.FetchProductDetailsForItemSku(hoveredItem.CatalogItem);
            productDetailsUIController.SetProductDetails(response);

            // show canvas
            productDetailsUIController.ShowProductDetails();
        }
    }
}