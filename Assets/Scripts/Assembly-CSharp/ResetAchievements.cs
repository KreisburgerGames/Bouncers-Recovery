using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class ResetAchievements : MonoBehaviour
{
	public GameObject confirm;

	public GameObject originalButton;
	public List<string> achievements = new List<string>();

	private void Start()
	{
		SteamAPI.Init();
	}

	private void Awake()
	{
		confirm.SetActive(value: false);
	}

	public void ShowConfirm()
	{
		confirm.SetActive(value: true);
	}

	public void Confirm()
	{
		foreach(string s in achievements)
		{
			SteamUserStats.ClearAchievement(s);
		}
		SteamUserStats.StoreStats();
		confirm.SetActive(value: false);
		PlayerPrefs.SetString("Skin", "Default");
		originalButton.gameObject.SetActive(value: false);
		Application.Quit();
	}
}
