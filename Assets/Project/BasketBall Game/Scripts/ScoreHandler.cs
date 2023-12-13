using System.Collections;
using TMPro;
using UnityEngine;

namespace Yudiz.VRBasketBall.Core
{
	public class ScoreHandler : MonoBehaviour
	{
		#region PIBLIC VAR
		public int totalScore { get; private set; }
		public int highScore { get; private set; }
		public GameData gamedata { get; private set; }
		public int teleportPoint { get; private set; }
		#endregion

		#region PRIVATE VAR
		[SerializeField] private GameObject pointObj;
		[SerializeField] private Transform pointPos;
		[SerializeField] private Transform timePointPos;
		#endregion

		#region UNITY CALLBACKS
		private void Start()
		{
			gamedata = new GameData();
			SaveLoadSystem.instance.LoadData(gamedata);
			totalScore = 0;
			teleportPoint = 0;
		}
		private void OnEnable()
		{
			Events.onGameplayStart += ResetScore;
			Events.onScoreAdd += IncreaseScore;
			Events.onScoreAdded += AddSamePositionPoint;
			Events.onScoreAdd += ShowGainPoint;
			Events.onTimeAdded += ShowGainTime;
		}
		private void OnDisable()
		{
			Events.onGameplayStart -= ResetScore;
			Events.onScoreAdd -= IncreaseScore;
			Events.onScoreAdded -= AddSamePositionPoint;
			Events.onScoreAdd -= ShowGainPoint;
			Events.onTimeAdded -= ShowGainTime;
		}
		private void OnApplicationQuit()
		{
			SaveLoadSystem.instance.SaveData(gamedata);
		}
		#endregion
		private void IncreaseScore(int value)
		{
			totalScore += value;
			CheckForHighScore(totalScore);
			Events.onScoreAdded?.Invoke(totalScore);
		}
		private void CheckForHighScore(int totalScore)
		{
			if (totalScore > gamedata.highscore)
			{
				gamedata.highscore = totalScore;
				Events.onHighscoreChanged?.Invoke(gamedata.highscore);
			}
		}
		private void ResetScore()
		{
			totalScore = 0;
			teleportPoint = 0;
		}
		public void ShowGainTime(int value)
		{
			GameObject point = Instantiate(pointObj, timePointPos.position, Quaternion.identity);
			TextMeshPro point_text = point.GetComponent<TextMeshPro>();
			point_text.text = "+" + value.ToString();
			point_text.fontSize = 5;
			point_text.alignment = TextAlignmentOptions.Center;
			StartCoroutine(ShowGainPointAnimation(point_text, 2f));
		}
		public void ShowGainPoint(int value)
		{
			GameObject point = Instantiate(pointObj, pointPos.position, Quaternion.identity);
			TextMeshPro point_text = point.GetComponent<TextMeshPro>();
			point_text.text = "+" + value.ToString();
			point_text.fontSize = 5;
			point_text.alignment = TextAlignmentOptions.Center;
			StartCoroutine(ShowGainPointAnimation(point_text, 2f));
		}
		private IEnumerator ShowGainPointAnimation(TextMeshPro text, float duration)
		{
			float time = 0f;
			while (time < duration)
			{
				time += Time.deltaTime;
				text.transform.Translate(Vector3.up * Time.deltaTime);
				text.alpha = Mathf.Lerp(1, 0, time / duration);
				yield return null;
			}
			text.alpha = 0;
			Destroy(text.gameObject);
		}
		private void AddSamePositionPoint(int score)
		{
			Events.onChangeActiveNet?.Invoke();
		}
#if UNITY_EDITOR
		[ContextMenu("Show Animation")]
		private void ShowAnimation()
		{
			ShowGainPoint(2);
			ShowGainTime(10);
		}
#endif
	}
		[System.Serializable]
		public class GameData
		{
			public int highscore;
		}
}