using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	private void Start()
	{
		SteamAPI.Init();
	}

	public void LoadScene(string sceneName)
	{
		Object.FindFirstObjectByType<AudioManager>().Play("select");
		SceneManager.LoadScene(sceneName);
	}

	public void Restart()
	{
		Object.FindFirstObjectByType<AudioManager>().Play("select");
		SceneManager.LoadScene("Game");
	}

	public void Leaderboards(string diff)
	{
		PlayerPrefs.SetString("diff", diff);
		LoadScene("Leaderboard");
		Object.FindFirstObjectByType<HighScores>().UploadScore(SteamUser.GetSteamID(), 0);
	}
}
