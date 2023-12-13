using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yudiz.BaseFramework;

namespace Yudiz.UI
{
    public class SplashUI : UIScreenView
    {
        public Slider ProgressBar;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            Show();
        }

        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();
        }

        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(.2f);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                //Output the current progress
                ProgressBar.value = asyncOperation.progress;

                if (asyncOperation.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(1f);
                    Hide();
                    yield return new WaitForSeconds(1f);
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}