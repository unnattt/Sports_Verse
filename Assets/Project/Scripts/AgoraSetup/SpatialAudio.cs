using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using System.Linq;

namespace Sportsverse.Audio
{
	public class SpatialAudio : MonoBehaviour
	{
		[SerializeField] float radius;

		PhotonView photonView;

		static Dictionary<Player, SpatialAudio> spatialAudioFromPlayers = new Dictionary<Player, SpatialAudio>();


		void Awake()
		{
			photonView = GetComponent<PhotonView>();

			spatialAudioFromPlayers[photonView.Owner] = this;
		}

		void OnDestroy()
		{
			foreach (var item in spatialAudioFromPlayers.Where(x => x.Value == this).ToList())
			{
				spatialAudioFromPlayers.Remove(item.Key);
			}
		}

		void Update()
		{
			if (!photonView.IsMine)
				return;

			foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
			{
				if (player.IsLocal)
					continue;

				if (player.CustomProperties.TryGetValue(StringConstants.agoraIdKey, out object agoraID))
				{
					if (spatialAudioFromPlayers.ContainsKey(player))
					{
						SpatialAudio other = spatialAudioFromPlayers[player];

						float gain = GetGain(other.transform.position);
						float pan = GetPan(other.transform.position);

						AgoraVoiceChatHandler.Instance.SetRemotePlayerSpatialAudio(uint.Parse((string)agoraID), pan, gain);
					}
					else
					{
                        AgoraVoiceChatHandler.Instance.SetRemotePlayerSpatialAudio(uint.Parse((string)agoraID), 0, 0);
					}
				}
			}
		}

		float GetGain(Vector3 otherPosition)
		{
			float distance = Vector3.Distance(transform.position, otherPosition);
			float gain = Mathf.Max(1 - (distance / radius), 0) * 100f;
			return gain;
		}

		float GetPan(Vector3 otherPosition)
		{
			Vector3 direction = otherPosition - transform.position;
			direction.Normalize();
			float dotProduct = Vector3.Dot(transform.right, direction);
			return dotProduct;
		}
	}
}