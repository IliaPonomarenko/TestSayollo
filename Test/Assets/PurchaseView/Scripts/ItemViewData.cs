using UnityEngine;
using UnityEngine.UI;
using static RequestAPI.JsonData;

namespace PurchaseView.Scripts
{
    internal sealed class ItemViewData : MonoBehaviour
    {
        [SerializeField] private GameObject itemViewPrefab;
        [SerializeField] private GameObject paymentViewPrefab;
        [SerializeField] private Image itemImage;
        [SerializeField] private Text title;
        [SerializeField] private Text price;

        private void Start()
        {
            itemImage.type = Image.Type.Simple;
            itemImage.preserveAspect = true;
        }

        public void SetData(ItemJsonData data, Sprite s)
        {
            title.text = data.title;
            price.text = $"{data.price} {data.currency}";
            itemImage.sprite = s;
        }

        public void ShowScreen(bool show) 
        {
            paymentViewPrefab.SetActive(show);
            itemViewPrefab.SetActive(!show);
        }
    }
}
