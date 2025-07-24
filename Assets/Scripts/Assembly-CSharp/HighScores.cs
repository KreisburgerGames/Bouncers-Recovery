using System;
using System.Collections;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.IO;

public class HighScores : MonoBehaviour
{
	private string privateCode = "Private Key";

	private string publicCode = "Public Key";

	private const string webURL = "http://dreamlo.com/lb/";

	public PlayerScore[] scoreList;

	private DisplayHighscores myDisplay;

	public Text highscoreText;

	private static HighScores instance;

	private void Start()
	{
		SteamAPI.Init();
	}

	private void Awake()
	{
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			publicCode = "687da6ca8f40bb1624f13564";
			privateCode = MainMenu.Decrypt("WUZXzM+/7Eu2Ubd53cHtB6/4l87WgUDnTm30utCN8cwZJ1hqLb8JWUzTUSUaSdu3", "Kburger25", "dumbfounded");
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			publicCode = "687da6eb8f40bb1624f13571";
			privateCode = MainMenu.Decrypt("xqch/BvDzoSEp1xq1gYBpGFOO3GGTEoyqVmgwBaCpxk9h1PRInQUoY1xrZ3wFGHF", "Kburger25", "was your interest lost in translation");
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			publicCode = "687da7128f40bb1624f13587";
			privateCode = MainMenu.Decrypt("wkwKxGdqkAnBl297h+l7B3J4WcjCKK02EezYC86QyB6ZUzcaS6AggI0pGR3J1KaL", "Kburger25", "I cant find it anywhere no");
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			publicCode = "688196c18f40bb1624f479b1";
			privateCode = MainMenu.Decrypt("B32Mo8Rl2aga5NM2zbkN2SREj2J4QKZ+EL4YtfSiqmoJZApf8LrLwTGGELbDf7UF", "Kburger25", "you will not survive");
		}
		instance = this;
		myDisplay = GetComponent<DisplayHighscores>();
	}

	public void UploadScore(CSteamID username, int score)
	{
		FindFirstObjectByType<AntiCheat>().QuickCheck();
		instance.StartCoroutine(instance.DatabaseUpload(SteamUser.GetSteamID().m_SteamID.ToString(), score));
	}

	private IEnumerator DatabaseUpload(string userame, int score)
	{
		WWW www = new WWW("http://dreamlo.com/lb/" + privateCode + "/add/" + WWW.EscapeURL(userame) + "/" + score);
		yield return www;
		if (string.IsNullOrEmpty(www.error))
		{
			MonoBehaviour.print("Upload Successful");
			highscoreText.text = "Score uploaded";
			highscoreText.color = Color.green;
		}
		else
		{
			MonoBehaviour.print("Error uploading" + www.error);
			highscoreText.text = "Score upload failed";
			highscoreText.color = Color.red;
		}
	}

	public void DownloadScores()
	{
		print("downloading");
		StartCoroutine(DatabaseDownload());
	}

	private IEnumerator DatabaseDownload()
	{
		WWW www = new WWW("http://dreamlo.com/lb/" + publicCode + "/pipe/0/5");
		yield return www;
		if (string.IsNullOrEmpty(www.error))
		{
			print(www.text);
			OrganizeInfo(www.text);
			print(scoreList);
			myDisplay.SetScoresToMenu(scoreList);
			StartCoroutine(Refresh());
		}
		else
		{
			MonoBehaviour.print("Error downloading " + www.error);
		}
	}

	private IEnumerator Refresh()
	{
		new WaitForSeconds(0.1f);
		myDisplay.SetScoresToMenu(scoreList);
		yield return 0;
	}

	private void OrganizeInfo(string rawData)
	{
		string[] array = rawData.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		print(array.Length);
		scoreList = new PlayerScore[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[1] { '|' });
			string username = array2[0];
			int score = int.Parse(array2[1]);
			scoreList[i] = new PlayerScore(username, score);
			print(scoreList[i].username + ": " + scoreList[i].score);
		}
	}
}
