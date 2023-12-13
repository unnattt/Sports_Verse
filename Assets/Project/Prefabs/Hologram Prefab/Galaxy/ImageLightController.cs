using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLightController : MonoBehaviour
{
    public Image arrowMesh;
    public async void LightOn()
    {
        StartCoroutine("Light");

    }
    public void LightOff()
    {
        arrowMesh.material.SetColor("_EmissionColor", Color.black);
    }
    public IEnumerator Light()
    {
        arrowMesh.material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(1f);
        LightOff();
    }
}
