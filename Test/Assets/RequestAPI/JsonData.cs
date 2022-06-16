using System;

namespace RequestAPI
{
    internal static class JsonData
    {
        [Serializable]
        public struct PostRequestData
        {
            public string data;
        }

        [Serializable]
        public struct PurchaseFieldsData
        {
            public string Email;
            public string CreditCardNumber;
            public string ExpirationDate;
        }

        [Serializable]
        public struct ItemJsonData
        {
            public string title;
            public string item_id;
            public string item_name;
            public string item_image;
            public string currency;
            public string currency_sign;
            public string status;
            public float price;
            public int error_code;
        }

    }
}
