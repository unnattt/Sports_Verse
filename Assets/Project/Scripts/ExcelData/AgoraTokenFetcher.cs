using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sportsverse.Audio
{
    public class AgoraTokenFetcher : MonoBehaviour
    {
        private string excellSheetURL = "https://docs.google.com/spreadsheets/d/1s6LtQ0_NTBYq7ninqHkFGfpe9K8Vq258s2LLaAOoHik/export?format=csv";
        public delegate void OnTokenRecieved(string token);
        public static event OnTokenRecieved onTokenRecieved;

        [SerializeField] string token;

        void Start()
        {
            GetDataFromGoogle();
        }

        public void GetDataFromGoogle()
        {
            StartCoroutine(CSVDownloader.DownloadData(excellSheetURL, OnExcellDataRecieved));
        }
        void OnExcellDataRecieved(string sheetData)
        {
            onTokenRecieved?.Invoke(sheetData);
        }
    }
}