using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Yudiz.BaseFramework
{

    public class SceneHandler : IndestructibleSingleton<SceneHandler>
    {
        public string mainScene = "Sportsverse";
        public static Action<float> SceneLoadProgressing;

        public void LoadScene(string SceneName, Action OnSceneLoaded)
        {

            AsyncOperation asyncLoad;
            IEnumerator LoadAsyncScene()
            {
                if (SceneManager.GetActiveScene().name != "Loader")
                {
                    yield return SceneManager.LoadSceneAsync("Loader", LoadSceneMode.Single);
                }

                asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
                asyncLoad.allowSceneActivation = false;

                while (asyncLoad.progress < 0.9f)
                {
                    //scene has loaded as much as possible,
                    // the last 10% can't be multi-threaded
                    Debug.Log(asyncLoad.progress);
                    SceneLoadProgressing?.Invoke(asyncLoad.progress);

                    yield return null;
                }

                asyncLoad.allowSceneActivation = true;
                SceneLoadProgressing?.Invoke(1);
                OnSceneLoaded?.Invoke();

            }


            StartCoroutine(LoadAsyncScene()); //call to begin loading scene
                                              //wait for bLoadDone==true
        }
    }
}
