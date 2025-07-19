using System;
using System.Collections;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{
	public TextMeshProUGUI[] rNames;

	public TextMeshProUGUI[] rScores;

	public RawImage[] rAvatars;

	public bool scoresDisplayed;

	private HighScores myScores;

	private void Start()
	{
		SteamAPI.Init();
		for (int i = 0; i < rNames.Length; i++)
		{
			rNames[i].text = i + 1 + ". Fetching...";
		}
		myScores = GetComponent<HighScores>();
	}

	private void Update()
	{
		if (SteamManager.Initialized && !scoresDisplayed)
		{
			StartCoroutine("RefreshHighscores");
			scoresDisplayed = true;
		}
	}

	public void SetScoresToMenu(PlayerScore[] highscoreList)
	{
		for (int i = 0; i < rNames.Length; i++)
		{
			rNames[i].text = i + 1 + ". ";
			if (highscoreList.Length > i)
			{
				rScores[i].text = highscoreList[i].score.ToString();
				SteamFriends.RequestUserInformation((CSteamID)Convert.ToUInt64(highscoreList[i].username), bRequireNameOnly: false);
				rNames[i].text = i + 1 + ". " + SteamFriends.GetFriendPersonaName((CSteamID)Convert.ToUInt64(highscoreList[i].username)) + " - ";
				if (SteamFriends.GetFriendPersonaName((CSteamID)Convert.ToUInt64(highscoreList[i].username)) == "[Unknown]")
				{
					rNames[i].text = i + 1 + ". Loading... - ";
				}
				int largeFriendAvatar = SteamFriends.GetLargeFriendAvatar((CSteamID)Convert.ToUInt64(highscoreList[i].username));
				if (largeFriendAvatar == -1)
				{
					break;
				}
				rAvatars[i].texture = GetSteamImageAstexture(largeFriendAvatar);
			}
		}
	}

	private Texture2D GetSteamImageAstexture(int imageID)
	{
		Texture2D texture2D = null;
		if (SteamUtils.GetImageSize(imageID, out var pnWidth, out var pnHeight))
		{
			byte[] array = new byte[pnWidth * pnHeight * 4];
			if (SteamUtils.GetImageRGBA(imageID, array, (int)(pnWidth * pnHeight * 4)))
			{
				texture2D = new Texture2D((int)pnWidth, (int)pnHeight, TextureFormat.RGBA32, mipChain: false, linear: true);
				texture2D.LoadRawTextureData(array);
				texture2D.Apply();
			}
		}
		return texture2D;
	}

	private IEnumerator RefreshHighscores()
	{
		while (true)
		{
			myScores.DownloadScores();
			yield return new WaitForSeconds(1f);
		}
	}
}
