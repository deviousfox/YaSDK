//----------------------------------------
//    YaSdk_0.20
//    disigned by  deviousfox and StasRV
//    developed by deviousfox
//    used this asset you automaticle accepted license 
//    (Plugins/FGL_YaSDK/Other/LICENSE)
//----------------------------------------

using UnityEngine;
using FGL_YaSdk.Ads;

namespace FGL_YaSdk
{
    public class InternalFromJs : MonoBehaviour
    {

        public void AdsCloset(int placementId)
        {
            YaAds.SendAdsCallback(placementId, ShowResult.closed);
        }
        public void AdsException(int placementId)
        {
            YaAds.SendAdsCallback(placementId, ShowResult.exception);
        }
        public void AdsSecsess(int placementId)
        {
            YaAds.SendAdsCallback(placementId, ShowResult.success);
        }

    }
 
}
