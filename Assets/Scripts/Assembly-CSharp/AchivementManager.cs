using System.IO;
using Steamworks;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
	private void Start()
	{
		SteamAPI.Init();
		GiveAchivement("Time to Bounce!");
		if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\SteamVRPerformanceTest"))
		{
			GiveAchivement("SVRPT");
		}
	}

	public void GiveAchivement(string name)
	{
		SteamUserStats.SetAchievement(name);
		SteamUserStats.StoreStats();
	}

	public void AddBounce()
	{
		SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
		SteamUserStats.SetStat("BouncesSurvived", 1);
		SteamUserStats.StoreStats();
	}
}
