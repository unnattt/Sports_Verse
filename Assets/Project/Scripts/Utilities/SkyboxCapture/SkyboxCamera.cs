using UnityEngine;
using UnityEditor;
using System;
using System.IO;


#if UNITY_EDITOR

namespace Yudiz.EditorTools
{
    public class SkyboxCamera : MonoBehaviour
    {
        public string SkyboxName;
        public int TEXTURE_SIZE = 1024;

        static string SkyboxPath;
        static string SkyboxDirectory = "Generated Skybox";

        private void Start()
        {
            SkyboxPath = Path.Combine("Assets", SkyboxDirectory);
        }

        void Update()
        {
            if (Input.anyKey)
            {
                Capture();
            }
        }

        void Capture()
        {
            CheckAndCreateFolder();
            Cubemap cubemap = new Cubemap(TEXTURE_SIZE, TextureFormat.ARGB32, true);
            cubemap.name = SkyboxName;
            Camera camera = GetComponent<Camera>();
            camera.RenderToCubemap(cubemap);


            AssetDatabase.CreateAsset(
              cubemap,
              Path.Combine(SkyboxPath, SkyboxName + ".cubemap"));
        }

        private void CheckAndCreateFolder()
        {
            if (!Directory.Exists(Path.Combine(Application.dataPath, SkyboxDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(Application.dataPath, SkyboxDirectory));
            }
        }
    }
}

#endif