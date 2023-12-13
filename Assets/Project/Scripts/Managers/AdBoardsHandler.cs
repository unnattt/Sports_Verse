using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdBoardsHandler : MonoBehaviour
{
    [SerializeField] private List<Renderer> adBoards;
    [SerializeField] private List<Texture> adsTextures;

    private const string _baseMapString = "_BaseMap";
    private void Start()
    {
        SetupAdBoards();
        InvokeRepeating(nameof(UpdateRandomBoardAd), 2, 2);
    }
    private void SetupAdBoards()
    {
        List<Texture> ads = new List<Texture>(adsTextures);
        foreach (var adBoard in adBoards)
        {
            if (ads.Count > 0)
            {
                int randomAdIndex = Random.Range(0, ads.Count);
                adBoard.material.SetTexture(_baseMapString, ads[randomAdIndex]);
                ads.RemoveAt(randomAdIndex);
            }
            else
            {
                int randomAdIndex = Random.Range(0, adsTextures.Count);
                adBoard.material.SetTexture(_baseMapString, ads[randomAdIndex]);
            }
        }
    }

    public void UpdateRandomBoardAd()
    {
        Debug.Log("Board Ad Updated");
        int randomAdIndex = Random.Range(0, adsTextures.Count);
        int randomBoard = Random.Range(0, adBoards.Count);
        adBoards[randomBoard].material.SetTexture(_baseMapString, adsTextures[randomAdIndex]);
    }
}
