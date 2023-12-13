namespace Yudiz.VRBasketBall.Core
{
	public static class Events
	{
		public delegate void OnScoreAdd(int value);
		public static OnScoreAdd onScoreAdd;

		public delegate void OnScoreAdded(int totalScore);
		public static OnScoreAdded onScoreAdded;

		public delegate void OnHighscoreChanged(int highscore);
		public static OnHighscoreChanged onHighscoreChanged;

		public delegate void OnTimeAdded(int timeAmount);
		public static OnTimeAdded onTimeAdded;

		public delegate void OnGameplayStart();
		public static OnGameplayStart onGameplayStart;

		public delegate void OnGameOver();
		public static OnGameOver onGameOver;

		public delegate void OnPlayerPositionChanged(int placementPoint);
		public static OnPlayerPositionChanged onPlayerPositionChanged;

		public delegate void ChangePlayerPosition();
		public static ChangePlayerPosition onChangeActiveNet;

		public delegate void OnBallThrown();
		public static OnBallThrown onBallThrown;
	}
}