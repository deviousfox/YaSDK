mergeInto(LibraryManager.library, {
 ShowFullscreenAds: function () {
    ShowFullscreenAds();
  },

  ShowRewardedAds: function(placement) {
    ShowRewardAds(placement);
  },
  
  GetString: function (key){
  return GetData(key);
  },
  SetString: function(key, value){
  SetString(key, value);
  }
});