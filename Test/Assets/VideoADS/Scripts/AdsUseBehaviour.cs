using UnityEngine;

namespace VideoADS.Scripts
{
    internal sealed class AdsUseBehaviour : MonoBehaviour
    {
        [SerializeField] private MediaView adsLoader;

        private void Start() => 
            adsLoader.ShowAds();
    }
}