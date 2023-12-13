using Photon.Pun;
using Sportsverse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSpawner : MonoBehaviour
{
    public Vector2 horizontalBoundary;
    public Vector2 verticalBoundary;

    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab != null)
        {

            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(horizontalBoundary.x, horizontalBoundary.y), 0, Random.Range(verticalBoundary.x, verticalBoundary.y));

            RaycastHit hitInfo;
            if(Physics.Raycast(spawnPosition, Vector2.down,out hitInfo))
            {
                spawnPosition = hitInfo.point;
            }

            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
            //Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            //AgoraVoiceChatHandler.Instance.Join();
        }
    }
    private void OnDisable()
    {
        //AgoraVoiceChatHandler.Instance.Leave();
    }
}
