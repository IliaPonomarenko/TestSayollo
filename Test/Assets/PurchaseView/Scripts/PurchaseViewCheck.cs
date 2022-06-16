using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RequestAPI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static RequestAPI.JsonData;

namespace PurchaseView.Scripts
{
    internal sealed class PurchaseViewCheck : MonoBehaviour
    {
        private const string PurchasePostURL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/action";
        
        [SerializeField] private RequestPerformer requestPerformer;
        [SerializeField] private InputField email;
        [SerializeField] private InputField creditCardNumber;
        [SerializeField] private InputField expiration;

        [SerializeField] private List<InputField> inputFields;
        
        private string _json;

        public void PostPurchaseData()
        {
            if (!Check(inputFields)) 
            {

                Debug.Log("Please type all credentials in input fields!");
                return;
            }

            var data = new PurchaseFieldsData
            {
                Email = email.text,
                CreditCardNumber = creditCardNumber.text,
                ExpirationDate = expiration.text
            };

            _json = JsonConvert.SerializeObject(data);
            requestPerformer.Post(PurchasePostURL, _json, OnPurchase);
        }

        private void OnPurchase(DownloadHandler downloadHandler) => 
            Debug.Log(downloadHandler.text);

        private static bool Check(IEnumerable<InputField> fields) 
        {
            var firstNullOrEmpty = fields.FirstOrDefault(firstNullOrEmpty => firstNullOrEmpty.gameObject.GetComponent<FieldCheck>().CheckIsNullOrEmpty());
            return firstNullOrEmpty == null;
        }
    }
}
