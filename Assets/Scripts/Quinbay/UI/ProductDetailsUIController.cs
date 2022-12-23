using System;
using JetBrains.Annotations;
using Quinbay.API.Response;
using TMPro;
using UnityEngine;

namespace Quinbay.UI
{
    public class ProductDetailsUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text priceText;

        [CanBeNull] private Transform followTarget = null;
        private Transform lookTarget;
        private Vector3 offset = Vector3.zero;
        private bool isActive = false;
        RectTransform myTransform;

        private void Awake()
        {
            lookTarget = this.GetComponent<Canvas>().worldCamera.transform;
            myTransform = this.GetComponent<RectTransform>();
            HideProductDetails();
            ResetProductDetails();
        }

        private void Update()
        {
            if (isActive && followTarget != null)
            {
                myTransform.position = followTarget.position + offset;
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, 
                    Quaternion.LookRotation(myTransform.position - lookTarget.position), 0.85f);
            }
        }

        public void HideProductDetails()
        {
            isActive = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void ShowProductDetails()
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        
        public void SetProductDetails(ProductSummaryResponse productSummary)
        {
            titleText.text = productSummary.data.name ?? "";
            descriptionText.text = productSummary.data.uniqueSellingPoint ?? "";
            priceText.text = "Price: Rp" + productSummary.data.price.offered.ToString() ?? "";
        }
        
        public void ResetProductDetails()
        {
            titleText.text = "";
            descriptionText.text = "";
            priceText.text = "";
            SetFollowTargetAndOffset(null, Vector3.zero);
        }

        public void SetFollowTargetAndOffset(Transform followTarget, Vector3 offset)
        {
            this.followTarget = followTarget;
            this.offset = offset;
        }
    }
}