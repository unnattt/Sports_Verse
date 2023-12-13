using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;

public class SplashLoader : MonoBehaviour
{
    private void Start()
    {
        SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainScene, null);
    }
}
