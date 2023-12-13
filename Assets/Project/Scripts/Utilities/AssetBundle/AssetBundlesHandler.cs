
using DG.Tweening.Plugins.Core.PathCore;
using Sportsverse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Yudiz.BaseFramework;

namespace Yudiz.BaseFramework
{
    public class AssetBundlesHandler : IndestructibleSingleton<AssetBundlesHandler>
    {
        public static string URL;

        public List<GameTypeScenebundleLink> GameTypeScenebundleLinks;

        Dictionary<GameTypes, BundleConfig> BundleConfigDictionary;

        public override void OnAwake()
        {
            base.OnAwake();
#if UNITY_STANDALONE_WIN
            URL =  GetStreamingAssetsPath() + "/GameBundles/StandaloneWindows";
#elif UNITY_ANDROID
            URL =  GetStreamingAssetsPath() + "/GameBundles/Android";
#endif
            PrepareDictionary();
        }


        void PrepareDictionary()
        {
            BundleConfigDictionary = new Dictionary<GameTypes, BundleConfig>();

            foreach (var link in GameTypeScenebundleLinks)
            {
                BundleConfigDictionary.Add(link.GameType, new BundleConfig(link.BundleID, System.IO.Path.Combine(URL, link.BundleID)));
            }
        }



        public void LoadGame(GameTypes game)
        {
            Debug.Log("loading" + game.ToString());
            StartCoroutine(BundleConfigDictionary[game].LoadScene((scene) =>
            {

                Debug.Log("Got Scene");
                SceneHandler.Instance.LoadScene(scene, () =>
                {
                    BundleConfigDictionary[game].UnloadAsset();
                });

            }));
        }


        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        LoadGame(GameTypes.Cricket);
        //    }
        //}

        private static string GetStreamingAssetsPath()
        {
            if (Application.isEditor)
            {
                return "file:///" + Application.streamingAssetsPath;
            }
            else if (Application.isMobilePlatform || Application.isConsolePlatform)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    return "jar:file://" + Application.dataPath + "!/assets";
                }
                else
                {
                    return "file://" + Application.streamingAssetsPath;
                }
            }
            else // For standalone player.
                return "file://" + Application.streamingAssetsPath;
        }

    }


    [Serializable]
    public class GameTypeScenebundleLink
    {
        public GameTypes GameType;
        public string BundleID;

        public GameTypeScenebundleLink(GameTypes type, string name)
        {
            GameType = type;
            BundleID = name;
        }
    }




    [Serializable]
    public class BundleConfig
    {
        public string Name;
        public AssetBundle Bundle;
        public string BundlePath;


        public bool isLoading;

        public Action onComplete;
        public Action onFailure;



        public BundleConfig(string name, string Bundlepath)
        {
            Name = name;
            BundlePath = Bundlepath;
        }

        public void SetupListeners(Action onComplete, Action onFailure)
        {
            if (this.onComplete != null)
            {
                this.onComplete += onComplete;
            }
            else
            {
                this.onComplete = onComplete;
            }

            if (this.onFailure != null)
            {
                this.onFailure += onFailure;
            }
            else
            {
                this.onFailure = onFailure;
            }
        }

        public void ClearListeners()
        {
            onComplete = null;
            onFailure = null;
        }


        public IEnumerator LoadBundleFile()
        {
            Debug.Log(BundlePath);
            UnityWebRequest bundleRequest = UnityWebRequestAssetBundle.GetAssetBundle(BundlePath);

            isLoading = true;

            bundleRequest.SendWebRequest();

            while (!bundleRequest.isDone)
            {
                yield return null;
            }


            isLoading = false;

            if (bundleRequest.result == UnityWebRequest.Result.Success)
            {
                Bundle = DownloadHandlerAssetBundle.GetContent(bundleRequest);
            }
        }



        public IEnumerator LoadPrefab<T>()
        {
            if (Bundle == null)
            {
                yield return LoadBundleFile();
            }

            var names = Bundle.GetAllAssetNames();

            var assetRequest = Bundle.LoadAssetAsync<T>("Item");

            yield return assetRequest;

            var item = assetRequest.asset;

            GameObject.Instantiate(item, GameObject.Find("Root").transform);

            UnloadAsset();
            Bundle = null;
        }

        public IEnumerator LoadBundle()
        {
            if (Bundle == null)
            {
                yield return LoadBundleFile();
            }
        }




        public IEnumerator LoadScene(Action<string> callback)
        {
            if (SceneManager.GetActiveScene().name != "Loader")
            {
                yield return SceneManager.LoadSceneAsync("Loader", LoadSceneMode.Single);
            }

            if (isLoading)
            {
                yield break;
            }
            if (Bundle == null)
            {


                BundleConfig databundleConfig = new BundleConfig(Name + "data", BundlePath + "data");

                yield return databundleConfig.LoadBundleFile();

                yield return LoadBundleFile();
            }

            string[] scenePaths = Bundle.GetAllScenePaths();

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);

            callback?.Invoke(sceneName);
        }

        public void UnloadAsset()
        {
            Bundle.UnloadAsync(false);
        }

        public string VerifyDirectory(string DirectoryName)
        {
            string VerifiedPath = System.IO.Path.Combine(Application.persistentDataPath, DirectoryName);

            if (!Directory.Exists(VerifiedPath))
            {
                Directory.CreateDirectory(VerifiedPath);
            }

            return VerifiedPath;
        }
    }
}