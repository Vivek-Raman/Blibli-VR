using System;
using System.Collections;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Quinbay.API;
using Quinbay.API.Response;
using Quinbay.Assets;
using Quinbay.Catalog.Data;
using Quinbay.UI;
using UnityEngine;

namespace Quinbay.Catalog
{
    public class CatalogManager : MonoBehaviour
    {
        [SerializeField] private AssetBundlePrefabManager assetBundlePrefabManager;
        [SerializeField] private BlibliClient blibliClient;
        [SerializeField] private ProductDetailsUIController productDetailsUIController;

        [CanBeNull] private Item lastHoveredItem = null;
        [CanBeNull] private Task currentTask = null;
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

            currentTask?.Dispose();
            currentTask = FetchProductDetailsForItem(hoveredItem);
            currentTask.Start();
        }

        private async Task FetchProductDetailsForItem(Item hoveredItem)
        {
            lastHoveredItem = hoveredItem;

            // begin fetch product details in parallel
            Task<ProductSummaryResponse> task = blibliClient.FetchProductDetailsForItemSku(hoveredItem.CatalogItem);
            task.Start();

            // TODO: hide canvas
            productDetailsUIController.HideProductDetails();
            productDetailsUIController.ResetProductDetails();

            // TODO: reposition canvas
            productDetailsUIController.SetItemOrigin(hoveredItem.transform.position);

            // TODO: populate UI with product details once ready
            ProductSummaryResponse response = await task;
            productDetailsUIController.SetProductDetails(response);

            // TODO: show canvas
            productDetailsUIController.ShowProductDetails();

            currentTask = null;
        }
    }
}