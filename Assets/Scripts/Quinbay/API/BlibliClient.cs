using System;
using System.Threading.Tasks;
using Quinbay.API.Request;
using Quinbay.API.Response;
using Quinbay.Catalog.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Quinbay.API
{
    public class BlibliClient : MonoBehaviour
    {
        private const string BLIBLI_BASE_URL = "wwwuata.gdn-app.com";
        private const string BLIBLI_XCART_BASE_URL = "xcart.qa1-sg.cld";

        public async Task<ProductSummaryResponse> FetchProductDetailsForItemSku(CatalogItem item)
        {
            UriBuilder builder = new()
            {
                Scheme = "http",
                Host = BLIBLI_BASE_URL,
                Port = 80,
                Path = "/backend/product-detail/products/is--" + item.ItemSku + "/_summary",
                Query = "pickupPointCode=" + item.PickupPointCode,
            };
            Uri uri = builder.Uri;

            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                www.SendWebRequest();
                while (!www.isDone)
                {
                    await Task.Delay(100);
                }

                return JsonUtility.FromJson<ProductSummaryResponse>(www.downloadHandler.text);
            }
        }

        public async Task<bool> AddItemToBag(CatalogItem item)
        {
            UriBuilder builder = new()
            {
                Scheme = "http",
                Host = BLIBLI_XCART_BASE_URL,
                Port = 80,
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
                www.SendWebRequest();
                while (!www.isDone)
                {
                    await Task.Delay(100);
                }

                return www.result == UnityWebRequest.Result.Success;
            }
        }
    }
}