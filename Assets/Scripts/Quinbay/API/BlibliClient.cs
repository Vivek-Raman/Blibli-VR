using System;
using System.Collections;
using System.Threading.Tasks;
using Quinbay.API.Request;
using Quinbay.API.Response;
using Quinbay.Catalog.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Quinbay.API
{
    public class BlibliClient : MonoBehaviour
    {
        private const string BLIBLI_BASE_URL = "blibli.com";
        private const string BLIBLI_XCART_BASE_URL = "xcart.qa1-sg.cld";

        public IEnumerator FetchProductDetailsForItemSku(CatalogItem item, UnityAction<ProductSummaryResponse> onSuccess)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = BLIBLI_BASE_URL,
                // Port = 80,
                Path = "/backend/product-detail/products/is--" + item.ItemSku + "/_summary",
                Query = "pickupPointCode=" + item.PickupPointCode,
            };
            Uri uri = builder.Uri;

            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                Debug.Log("step 2.2, " + www.uri);
                yield return www.Send();

                Debug.Log("step 2.3, " + www.downloadHandler.isDone);
                Debug.Log("step 2.3.1, " + www.downloadHandler.text);
                Debug.Log("step 2.3.2, " + www.downloadHandler.error);
                ProductSummaryResponse response = JsonUtility.FromJson<ProductSummaryResponse>(www.downloadHandler.text);
                onSuccess?.Invoke(response);
            }
        }

        public IEnumerator AddItemToBag(CatalogItem item, UnityAction onSuccess)
        {
            UriBuilder builder = new()
            {
                Scheme = "http",
                Host = BLIBLI_XCART_BASE_URL,
                // Port = 80,
                Path = "/x-cart/pending-bag/add-item-to-bag",
                Query = "storeId=10001&channelId=web&requestId=viv123&clientId=blibli-VR&username=vivek.raman&forceReplace=true",
            };
            Uri uri = builder.Uri;
            AddItemToBagRequest request = new()
            {
                cartId = "vivek.raman@gdn-commerce.com",
                id = item.ItemSku,
                pickupPointCode = item.PickupPointCode,
                quantity = 1,
                cartItemType = "REGULAR",
            };

            using (UnityWebRequest www = UnityWebRequest.Post(uri, JsonUtility.ToJson(request)))
            {
                yield return www.SendWebRequest();
                onSuccess?.Invoke();
            }
        }
    }
}