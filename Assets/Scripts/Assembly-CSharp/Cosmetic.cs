using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class Cosmetic : MonoBehaviour
{
	private Image icon;

	public Sprite unlockedIcon;

	private Button button;

	public string achivementName;

	public string skinName;

	public Sprite lockedIcon;

	private void Start()
	{
		SteamAPI.Init();
	}

	private void Awake()
	{
		icon = GetComponent<Image>();
		button = GetComponent<Button>();
	}

	private void Update()
	{
		SteamUserStats.GetUserAchievement(SteamUser.GetSteamID(), achivementName, out var pbAchieved);
		if (pbAchieved)
		{
			icon.sprite = unlockedIcon;
			button.interactable = true;
		}
		else
		{
			icon.sprite = lockedIcon;
			button.interactable = false;
		}
	}

	public void Equip()
	{
		Object.FindFirstObjectByType<AudioManager>().Play("select");
		SteamUserStats.GetUserAchievement(SteamUser.GetSteamID(), achivementName, out var pbAchieved);
		if (pbAchieved)
		{
			PlayerPrefs.SetString("Skin", skinName);
		}
	}
}
