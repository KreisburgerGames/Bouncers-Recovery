using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class DiffHandler : MonoBehaviour
{
	public List<string> diffSelectStatuses = new List<string>();
	public void SetDifficulty(string difficulty)
	{
		PlayerPrefs.SetString("diff", difficulty);
	}

    void Start()
    {
        SteamAPI.Init();
		if(!Application.isEditor)
			SteamFriends.SetRichPresence("st", diffSelectStatuses[UnityEngine.Random.Range(0, diffSelectStatuses.Count)]);
    }
}
