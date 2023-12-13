using UnityEngine;

namespace Yudiz.VRBasketBall.Core
{
	public class Ground : MonoBehaviour
	{
		[SerializeField] private GameObject bounceParticleObj;

		private void OnCollisionEnter(Collision collision)
		{
			ContactPoint contact = collision.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
			Vector3 pos = contact.point;
			GameObject bounceParticle = Instantiate(bounceParticleObj, pos, rot);
			Destroy(bounceParticle, 1f);
		}
	}
}