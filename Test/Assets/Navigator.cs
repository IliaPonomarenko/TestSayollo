using UnityEngine;
using UnityEngine.UI;

internal sealed class Navigator : MonoBehaviour
{
    [SerializeField] private Button ad;
    [SerializeField] private Button store;
    [SerializeField] private Button buy;
    
    [SerializeField] private GameObject adScreen;
    [SerializeField] private GameObject storeScreen;
    [SerializeField] private GameObject purchaseScreen;
    [SerializeField] private GameObject entranceScreen;
    
    private enum Screen
    {
        AD,
        Store,
        Purchase
    }

    private void Start()
    {
        ad.onClick.AddListener(() => ShowScreen(Screen.AD));
        store.onClick.AddListener(() => ShowScreen(Screen.Store));
        buy.onClick.AddListener(() => ShowScreen(Screen.Purchase));
    }

    private void ShowScreen(Screen screen)
    {
        adScreen.SetActive(screen == Screen.AD);
        purchaseScreen.SetActive(screen == Screen.Purchase);
        storeScreen.SetActive(screen == Screen.Store);
        entranceScreen.SetActive(false);
    }
}
