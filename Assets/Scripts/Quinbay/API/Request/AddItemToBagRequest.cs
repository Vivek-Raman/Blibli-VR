namespace Quinbay.API.Request
{
    public class AddItemToBagRequest
    {
        public string cartId;
        public string id;
        public string pickupPointCode;
        public int quantity;
        public string cartItemType;
    }
}