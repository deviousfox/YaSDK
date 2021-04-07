//----------------------------------------
//    YaSdk_0.20
//    disigned by  deviousfox and StasRV
//    developed by deviousfox
//    used this asset you automaticle accepted license 
//    (Plugins/FGL_YaSDK/Other/LICENSE)
//----------------------------------------
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FGL_YaSdk
{
    public class ExternToWebgl : MonoBehaviour
    {

        [DllImport("__Internal")]
        public static extern void ShowRewardedAds(string placement);


        [DllImport("__Internal")]
        public static extern void ShowFullscreenAds();
       
        [DllImport("__Internal")]
        public static extern string GetString(string key);

        [DllImport("__Internal")]
        public static extern void SetString(string key, string value);
    }
}

namespace FGL_YaSdk.Ads
{
    public enum ShowResult { closed, success, exception }
    public interface IYaAdsListener
    {
       void OnYaAdsDidFinish(string placementId, ShowResult showResult);
    }

    public class YaAds
    {
        private static List<IYaAdsListener> adsListeners = new List<IYaAdsListener>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        public static void AddListener(IYaAdsListener listener)
        {
            adsListeners.Add(listener);
        }

        public static void ShowRewardAds(string placementId)
        {
            ExternToWebgl.ShowRewardedAds(placementId);
        }

        public static void ShowFullScreenAds()
        {

            ExternToWebgl.ShowFullscreenAds();
        }

        public static void SendAdsCallback(string placementId, ShowResult showResult)
        {
            foreach (var listener in adsListeners)
            {
                listener.OnYaAdsDidFinish(placementId, showResult);
            }
        }
    }
        
}

