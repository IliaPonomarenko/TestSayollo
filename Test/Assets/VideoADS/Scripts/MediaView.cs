using System.IO;
using System.Xml;
using RequestAPI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace VideoADS.Scripts
{
    [RequireComponent(typeof(VideoPlayer))]
    internal sealed class MediaView : MonoBehaviour
    {
        private const string AdsURL = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast";
    
        [SerializeField] private RequestPerformer requestPerformer;

        private VideoPlayer _videoPlayer;

        private static string _downloadedXmlFilePath;
        private static string _adsPath;


        private void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _adsPath = Path.Combine(Application.persistentDataPath, "ads_media.webm");
            _downloadedXmlFilePath = Path.Combine(Application.persistentDataPath, "file.xml");
        }

        public void ShowAds() => 
            requestPerformer.Get(AdsURL, OnRequestPassed);

        private void OnRequestPassed(DownloadHandler downloadHandler) => 
            requestPerformer.DownLoadFile(ParseXML(downloadHandler.text), _adsPath, OnMediaFileLoaded);

        private string ParseXML(string xmlText)
        {
            File.WriteAllText(_downloadedXmlFilePath, xmlText);
            var doc = new XmlDocument();
            doc.Load(_downloadedXmlFilePath);
            return doc.GetElementsByTagName("MediaFile")[0].InnerText;
        }

        private void OnMediaFileLoaded() => 
            ShowVideo(_adsPath);

        private void ShowVideo(string videoFilePath)
        {
            _videoPlayer.url = videoFilePath;
            _videoPlayer.Play();
        }
    }
}