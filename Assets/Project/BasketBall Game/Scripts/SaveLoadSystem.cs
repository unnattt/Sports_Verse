using System.IO;
using UnityEngine;
namespace Yudiz.VRBasketBall.Core
{
	public class SaveLoadSystem : MonoBehaviour
	{
		public static SaveLoadSystem instance;
		private string saveFile;
		private void Awake()
		{
			if (instance == null)
				instance = this;
			saveFile = Application.persistentDataPath + "/BasketBallData.json";
		}
		public void SaveData(GameData gameData)
		{
			string data = JsonUtility.ToJson(gameData);
			File.WriteAllText(saveFile, data);
			Debug.Log("<color=green>Data is Saved</color>");
		}

		[ContextMenu("Load Data")]
		public void LoadData(GameData gameData)
		{
			if (File.Exists(saveFile))
			{
				string filedata = File.ReadAllText(saveFile);
				Debug.Log(filedata);
				JsonUtility.FromJsonOverwrite(filedata, gameData);
			}
			else
			{
				Debug.LogError("File Does not Exist");
			}
		}
	}
}