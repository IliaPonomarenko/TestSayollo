using System.IO;
using Newtonsoft.Json;
using RequestAPI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static RequestAPI.JsonData;

namespace PurchaseView.Scripts
{
    [RequireComponent(typeof(ItemViewData))]
    internal sealed class PurchaseItemView : MonoBehaviour
    {
        private const string PostUrl = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/v1/gcom/ad";
        
        [SerializeField] private RequestPerformer requestPerformer;
        [SerializeField] private Button buy;

        private PostRequestData _testPostRequestData;
        private ItemJsonData _itemJsonData;
        private ItemViewData _purchaseViewData;

        private string _downloadedImagePath;
        
        private void Awake()
        {
            _purchaseViewData = GetComponent<ItemViewData>();
            _downloadedImagePath = Application.persistentDataPath + "Image.jpeg";

            buy.interactable = false;
            buy.onClick.AddListener(OnClickBuyBtn);
        }

        private void Start() => 
            PostTestJsonBody();

        private void PostTestJsonBody()
        {
            _testPostRequestData = new PostRequestData
            {
                data = "TestData"
            };
            
            var testJson = JsonConvert.SerializeObject(_testPostRequestData);
            requestPerformer.Post(PostUrl, testJson, OnRequestPassed);
        }

        private void OnClickBuyBtn() => 
            _purchaseViewData.ShowScreen(true);

        private void OnRequestPassed(DownloadHandler downloadHandler)
        {
            _itemJsonData = JsonConvert.DeserializeObject<ItemJsonData>(downloadHandler.text);
            LoadImage(_itemJsonData.item_image);
        }

        private void LoadImage(string imageUrl) => 
            requestPerformer.Get(imageUrl, new DownloadHandlerTexture(), OnLoadedImage);

        private void OnLoadedImage(DownloadHandler downloadHandler)
        {
            buy.interactable = true;
            
            var imageBytes = downloadHandler.data;
            File.WriteAllBytes(_downloadedImagePath, imageBytes);
            var fileData = File.ReadAllBytes(_downloadedImagePath);
            
            var texture2D = new Texture2D(2, 2);
            texture2D.LoadImage(fileData);
            var s = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero, 1f);
            
            SetData(_itemJsonData, s);
        }

        private void SetData(ItemJsonData itemJsonData, Sprite s) => 
            _purchaseViewData.SetData(itemJsonData, s);
    }
}