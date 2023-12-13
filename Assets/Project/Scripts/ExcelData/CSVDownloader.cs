using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// CSVDownloader.cs
using UnityEngine.Networking;

namespace Sportsverse.Audio
{
    public static class CSVDownloader
    {

        private const string url = "https://docs.google.com/spreadsheets/d/1UrU8FyY5QsC8K2khEOAIcg7gpv1hwRn447VkAFbvcNs/export?format=csv";
        internal static IEnumerator DownloadData(string url, System.Action<string> onCompleted)
        {
            yield return new WaitForEndOfFrame();

            string downloadData = null;
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.DataProcessingError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    downloadData = PlayerPrefs.GetString("LastDataDownloaded", null);
                    string versionText = PlayerPrefs.GetString("LastDataDownloadedVersion", null);
                }
                else
                {
                    downloadData = webRequest.downloadHandler.text;
                }
            }

            onCompleted(downloadData);
        }
    }
}