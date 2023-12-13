using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.Simple;
using UnityEditor;
using Yudiz;
using Yudiz.BaseFramework;

namespace Sportsverse
{
	public class PhotonManager : PhotonIndestructableSingleton<PhotonManager>
	{
		#region PRIVATE_VAR
		[SerializeField] private string gameVersion;
		private string mainScene = "Sportsverse";
		private string launcherScene = "Launcher";
		#endregion

		#region PUBLIC_VAR
		#endregion
				
		#region UNITY_CALLBACKS
		#endregion

		#region PRIVATE_METHODS
		#endregion

		#region PUBLIC_METHODS

		public void ConnectToPhotonServers()
		{
			PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = gameVersion;
		}

		public void JoinRoom()
		{
			PhotonNetwork.JoinRandomRoom();
		}

		public void SetPlayerNickname(string name)
		{
			PhotonNetwork.NickName = name;
		}

		public Player[] GetPlayerDictionary()
		{
			return PhotonNetwork.PlayerList;
		}

		#endregion

		#region PUN_CALLBACKS
		public void LoadSportsverse()
		{
            SceneHandler.Instance.LoadScene(mainScene, null);
        }
		public override void OnConnectedToMaster()
		{
			Debug.Log("Connected to master.....");

			PhotonNetwork.JoinRandomRoom();
			// Join any random Room
			//PhotonNetwork.CreateRoom(null);
		}

		public override void OnDisconnected(DisconnectCause cause)
		{
			Debug.Log($"Disconnected, Reason : {cause}");
			SceneHandler.Instance.LoadScene(launcherScene, null);
		}

		public override void OnMasterClientSwitched(Player newMasterClient)
		{
			Debug.Log("Client has been switched to : " + newMasterClient.NickName);
			//PhotonNetwork.Disconnect();
		}

		public override void OnJoinedRoom()
		{
			Debug.Log($"Room Join, Person joined: {PhotonNetwork.NickName}");
			Debug.Log("Room name: " + PhotonNetwork.CurrentRoom.Name);

            PhotonNetwork.LoadLevel(mainScene);

			/*if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
			{
				// Load Ground Scene
			}*/
		}

		public override void OnPlayerLeftRoom(Player otherPlayer)
		{
			base.OnPlayerLeftRoom(otherPlayer);
			Debug.Log("Player Left: " + otherPlayer.NickName);
		}

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("Player Joined: " + newPlayer.NickName);

        }

        public override void OnLeftRoom()
		{
			base.OnLeftRoom();
			Debug.Log("You left Room!");
		}


		public override void OnJoinRandomFailed(short returnCode, string message)
		{
			Debug.Log($"Return code : {returnCode}, error Message :{message}");

			// if Random room join does not work and it fails then we create another room 
			PhotonNetwork.CreateRoom(null);
		}
		#endregion
	}
}

