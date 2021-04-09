//----------------------------------------
//    YaSdk_0.20
//    disigned by  deviousfox and StasRV
//    developed by deviousfox
//    used this asset you automaticle accepted license 
//    (Plugins/FGL_YaSDK/Other/LICENSE)
//----------------------------------------
using UnityEngine;
using UnityEngine.UI;
using FGL_YaSdk;
using FGL_YaSdk.Ads;
using System;

public class Sample : MonoBehaviour, IYaAdsListener
{
    public int PlacemetID = 6775443;

    public Text SaveOutputText;
    public Text RewardAdsOutput;
    public InputField SaveInputText;
    public Button UsageButton;
    public Button ShowFullscreenButton;
    public Button ShowRewardButton;

    private void Start()
    {
        UsageButton.onClick.AddListener(LoadData);
        ShowFullscreenButton.onClick.AddListener(ShowFullscreen);
        ShowRewardButton.onClick.AddListener(ShowReward);

        YaAds.AddListener(this); // Subscribe on ads callbacks;
    }


    public void LoadData()
    {
        SaveOutputText.text = ExternToWebgl.GetString(SaveInputText.text);
    }

    public void ShowReward()
    {
        YaAds.ShowRewardAds(PlacemetID);
    }
    public void ShowFullscreen()
    {
        YaAds.ShowFullScreenAds();
    }

    public void OnYaAdsDidFinish(int placementId, ShowResult showResult)
    {
        if (placementId == PlacemetID && showResult == ShowResult.success)// cheking placement id and ads result
        {
            RewardAdsOutput.text = (Convert.ToInt32( RewardAdsOutput.text) + 200).ToString();
        }
    }
}
