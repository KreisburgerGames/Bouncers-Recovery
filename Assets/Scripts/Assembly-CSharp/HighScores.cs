using System;
using System.Collections;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

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
			publicCode = "646e27e68f40bb7d84ece24c";
			privateCode = "I0Nc4AV_eUiADRpvxybrRg5aXRDaogK0ynIGRAU_R0Tg";
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			publicCode = "646e28cb8f40bb7d84ece3cd";
			privateCode = "rFx6ypRhnk2p-N3vHfFESQAsCnonmDP0y1skvLGRAguQ";
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			publicCode = "646e28d78f40bb7d84ece3dd";
			privateCode = "N8uI4mGc1UOMiugEd2dPWwWKuD7iJcMkaPtX2sMLVYTQ";
		}
		instance = this;
		myDisplay = GetComponent<DisplayHighscores>();
	}

	public void UploadScore(CSteamID username, int score)
	{
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
		StartCoroutine("DatabaseDownload");
	}

	private IEnumerator DatabaseDownload()
	{
		WWW www = new WWW("http://dreamlo.com/lb/" + publicCode + "/pipe/0/5");
		yield return www;
		if (string.IsNullOrEmpty(www.error))
		{
			OrganizeInfo(www.text);
			myDisplay.SetScoresToMenu(scoreList);
			StartCoroutine(Refresh());
		}
		else
		{
			MonoBehaviour.print("Error uploading" + www.error);
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
		scoreList = new PlayerScore[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[1] { '|' });
			string username = array2[0];
			int score = int.Parse(array2[1]);
			scoreList[i] = new PlayerScore(username, score);
			MonoBehaviour.print(scoreList[i].username + ": " + scoreList[i].score);
		}
	}
}
