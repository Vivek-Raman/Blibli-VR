using System;
using System.Threading.Tasks;
using Quinbay.API.Response;
using Quinbay.Catalog.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Quinbay.API
{
    public class BlibliClient : MonoBehaviour
    {
        private const string BLIBLI_BASE_URL = "wwwuata.gdn-app.com";

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

                Debug.Log(www.downloadHandler.text);
                return JsonUtility.FromJson<ProductSummaryResponse>(www.downloadHandler.text);
            }
        }
    }
}