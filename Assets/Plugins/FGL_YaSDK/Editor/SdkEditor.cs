#if UNITY_EDITOR
//----------------------------------------
//    YaSdk_0.20
//    disigned by  deviousfox and StasRV
//    developed by deviousfox
//    used this asset you automaticle accepted license 
//    (Plugins/FGL_YaSDK/Other/LICENSE)
//----------------------------------------
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Compression;
using System.IO;

using FGL_YaSdk;

namespace FGL_YaSDK.Editor
{
    public class BuildWindow : EditorWindow
    {
        [MenuItem("FGL/YndexSdk build window")]
        public static void ShowWindow()
        {
            BuildWindow window;

            window =  GetWindow<BuildWindow>("YndexSDK Build");
            window.minSize = new Vector2(1000,500);
            window.maxSize = new Vector2(1000, 500);
        }
        private string path = null;
        private string gameTitle = null;
        private string buildPath = null;
        private string indexData = null;

        [System.Obsolete]
        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            path = GUILayout.TextArea(path, GUILayout.Width(800), GUILayout.Height(20));
            
            //if (GUILayout.Button("SelectFolder", GUILayout.Width(100)))
            //{

            //}
            GUILayout.EndHorizontal();
            gameTitle = GUILayout.TextArea(gameTitle, GUILayout.Width(300), GUILayout.Height(20));

            

            GUILayout.BeginHorizontal();
            GUILayout.Space(800);
            if (GUILayout.Button("Build", GUILayout.Width(100)))
            {
                if (path == null || gameTitle == null)
                {
                    Debug.LogError("Path or Title is null");
                    return;
                }
                buildPath = Path.Combine(path, gameTitle);

                Scene[] scenes = SceneManager.GetAllScenes();
                string[] scenesName = new string[scenes.Length];

                for (int i = 0; i < scenes.Length; i++)
                {
                    scenesName[i] = scenes[i].path;
                }

                // makeBuild;
                BuildPipeline.BuildPlayer(scenesName, buildPath, BuildTarget.WebGL, BuildOptions.None);
                //IntegrationServices;
                IntegrationServices(Path.Combine(buildPath + "/index.html"));
                //MakeZip;
                ZipFile.CreateFromDirectory(buildPath,  buildPath +".zip");
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(500);
            GUILayout.Label("FGL_webgl_ex 0.20");
        }

        private void IntegrationServices(string buildPath)
        {
            
            indexData = null;

            #region Read
            Debug.Log("Index reading...");
            try
            {
                using (StreamReader sr = new StreamReader(buildPath))
                {
                    indexData = sr.ReadToEnd();
                }
            }
            catch (System.Exception e)
            {

                Debug.LogError(e.Message);
            }
            #endregion

            Debug.Log("Index integration...");
            indexData = IntegrationText(indexData, "<head>", Template.tempaltes[2]);
            indexData = IntegrationText(indexData, "<head>", Template.tempaltes[1]);
            indexData = IntegrationText(indexData, "<head>", Template.tempaltes[0]);
            indexData = IntegrationText(indexData, "</body>", "<!-- MAKE BY FOXGL_YANDEX_SDK_0.01 -->");


            #region Write
            Debug.Log("Index writning...");
            try
            {
                using (StreamWriter sw = new StreamWriter(buildPath))
                {
                    sw.Write(indexData);
                }
            }
            catch (System.Exception e)
            {

                Debug.LogError(e);
            }
            #endregion
            Debug.Log("Integration complite");
        }

        private string IntegrationText(string data, string tag, string integrationText)
        {
            return data = data.Insert(data.LastIndexOf(tag) + tag.Length, integrationText +"\n");
        }

    }
    #region supportData
    [System.Serializable]
    public class LocalizationData
    {
        public LocalizationItem[] items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
    #endregion
    public class SdkSettings : EditorWindow
    {

        private bool revardAdsOnEnable;
        private bool fullScreenAdsOnEnable;
        private bool onOpenAds;



        [MenuItem("FGL/YndexSdk settings window")]
        public static void ShowWindow()
        {
            SdkSettings window;
            window = GetWindow<SdkSettings>("YndexSDK settings");
            window.minSize = new Vector2(1000, 500);
           // window.maxSize = new Vector2(1000, 500);
        }

        private void OnGUI()
        {
            GUILayout.Label("YaSdkSettings");
            GUILayout.Space(20);
           // revardAdsOnEnable = GUILayout.Toggle(revardAdsOnEnable, "Enable revard ads");
           // fullScreenAdsOnEnable = GUILayout.Toggle(fullScreenAdsOnEnable, "Enable fullscreen ads");
           // onOpenAds = GUILayout.Toggle(onOpenAds, "Enable on opening ads");


            if (GUILayout.Button("Initialize", GUILayout.Width(200)))
            {
                if (FindObjectOfType<InternalFromJs>() == null)
                {
                    new GameObject("InternalFromJs").AddComponent<InternalFromJs>();
                }
            }


            GUILayout.BeginHorizontal();
            GUILayout.Space(500);
            GUILayout.Label("FGL_webgl_ex 0.20");

        }

        public void InititalizeFullScreen()
        {

        }
    }

    public class Template
    {
        public static string[] tempaltes = new string [] { "<script src='https://yandex.ru/games/sdk/v2'></script>", 
            "<script>  function ShowFullscreen() { YaGames.init({ adv: { onAdvClose: (wasShown) => { console.info('adv closed!'); },},}).then((ysdk) => { ysdk.adv.showFullscreenAdv(); }); } </script>",
        "<script> function ShowRewardAds(placementID){ YaGames.init({ adv: { onAdvClose: wasShown => { console.info('adv closed!'); } } }) .then(ysdk => { ysdk.adv.showRewardedVideo({ callbacks: { onOpen: () => { console.log('Video ad open.'); }, onRewarded: () => { unityInstance.SendMessage('InternalFromJs','AdsSecsess', placementID); console.log('Rewarded!'); }, onClose: () => { unityInstance.SendMessage('InternalFromJs','AdsCloset',placementID); console.log('Video ad closed.'); }, onError: (e) => { unityInstance.SendMessage('InternalFromJs','AdsException',placementID); console.log('Error while open video ad:', e); } } }); }); } </script>"
        };
    }
}
#endif
