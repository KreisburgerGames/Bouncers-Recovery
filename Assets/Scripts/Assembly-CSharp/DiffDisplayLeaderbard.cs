using TMPro;
using UnityEngine;

public class DiffDisplayLeaderbard : MonoBehaviour
{
	public TMP_Text text;

	private void Awake()
	{
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			text.text = "Easy Difficulty Leaderboard";
			text.color = Color.green;
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			text.text = "Medium Difficulty Leaderboard";
			text.color = Color.yellow;
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			text.text = "Hard Difficulty Leaderboard";
			text.color = Color.red;
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			text.text = "Unfair Difficulty Leaderboard";
			text.color = Color.blue;
		}
		Object.FindFirstObjectByType<HighScores>().DownloadScores();
		Object.FindFirstObjectByType<DisplayHighscores>().SetScoresToMenu(Object.FindFirstObjectByType<HighScores>().scoreList);
	}
}
