using Steamworks;
using UnityEngine;

public class ResetAchievements : MonoBehaviour
{
	public GameObject confirm;

	public GameObject originalButton;

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
		SteamUserStats.ResetAllStats(bAchievementsToo: true);
		SteamUserStats.StoreStats();
		confirm.SetActive(value: false);
		originalButton.gameObject.SetActive(value: false);
		Application.Quit();
	}
}
