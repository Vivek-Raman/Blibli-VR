using System;
using JetBrains.Annotations;
using Quinbay.API.Response;
using Quinbay.Catalog.Data;
using TMPro;
using UnityEngine;

namespace Quinbay.UI
{
    public class ProductDetailsUIController : MonoBehaviour
    {
        [SerializeField] private Canvas productDetailsCanvas;
        
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text priceText;

        private bool isActive = false;

        private void Update()
        {
            if (isActive)
            {
                // TODO: canvas always rotates to face player
            }
        }

        public void HideProductDetails()
        {
            isActive = false;
            productDetailsCanvas.enabled = false;
        }

        public void ShowProductDetails()
        {
            isActive = true;
            productDetailsCanvas.enabled = true;
        }
        
        public void SetProductDetails(ProductSummaryResponse productSummary)
        {
            titleText.text = productSummary.data.name ?? "";
            priceText.text = "Price: Rp" + productSummary.data.price.offered.ToString() ?? "";
        }
        
        public void ResetProductDetails()
        {
            titleText.text = "";
            priceText.text = "";
        }

        public void SetItemOrigin(Vector3 transformPosition)
        {
            productDetailsCanvas.GetComponent<RectTransform>().position = transformPosition;
        }
    }
}