using System;

namespace Quinbay.API.Response
{
    [Serializable]
    public class ProductSummaryResponse
    {
        public ProductSummaryResponse.Data data;

        [Serializable]
        public class Data
        {
            public string name;
            public string uniqueSellingPoint;
            public long stock;
            public ProductSummaryResponse.Data.Price price;

            [Serializable]
            public class Price
            {
                public long offered;
            }
        }
    }
}