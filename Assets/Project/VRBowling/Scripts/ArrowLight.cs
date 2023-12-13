using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Yudiz.VRBowling
{
    public class ArrowLight : MonoBehaviour
    {
        public MeshRenderer arrowMesh;
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

        public void OnTriggerEnter(Collider other)
        {
            arrowMesh.material.SetColor("_EmissionColor", Color.white);
        }
    }
}
