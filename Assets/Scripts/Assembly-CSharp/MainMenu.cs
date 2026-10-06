using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Steamworks;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour
{
	public GameObject[] keepOnSettingsLoad;
	

	private bool started;
	public AudioMixer mixer;
	private float value;
	public List<string> menuStatuses = new List<string>();

    public static string Decrypt(string cipherText, string password, string salt)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            aes.Key = key.GetBytes(32);
            aes.IV = key.GetBytes(16);

            byte[] buffer = Convert.FromBase64String(cipherText);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs)) {
                return sr.ReadToEnd();
            }
        }
    }

	public static string Encrypt(string plainText, string password, string salt)
	{
		using (Aes aes = Aes.Create())
		{
			byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
			// Match the 10,000 iterations used in your Decrypt method
			var key = new Rfc2898DeriveBytes(password, saltBytes, 10000);
			aes.Key = key.GetBytes(32);
			aes.IV = key.GetBytes(16);

			var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
			using (var ms = new MemoryStream())
			{
				using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				using (var sw = new StreamWriter(cs))
				{
					sw.Write(plainText);
				}
				
				// Convert the encrypted bytes in the memory stream to a Base64 string
				return Convert.ToBase64String(ms.ToArray());
			}
		}
	}

	private void Awake()
	{
		SteamAPI.Init();
		SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
		SteamUserStats.GetUserStat(SteamUser.GetSteamID(), "BouncesSurvived", out int bS);
		if(Application.isEditor)
			SteamFriends.SetRichPresence("st", "In his fuckass lil Unity project");
		else
			SteamFriends.SetRichPresence("st", menuStatuses[UnityEngine.Random.Range(0, menuStatuses.Count)]);
		SteamUserStats.GetAchievement("SVRPT", out bool svrpt);
		if(svrpt && UnityEngine.Random.Range(1, 11) == 3)
			SteamFriends.SetRichPresence("st", "Hopping on SteamVR Performance Test");
		SteamFriends.SetRichPresence("steam_display", "#status");
		if (!PlayerPrefs.HasKey("mouseControls"))
		{
			PlayerPrefs.SetInt("mouseControls", 1);
		}
		started = false;
		if (GameObject.FindGameObjectsWithTag("Audio").Length > 1)
		{
			UnityEngine.Object.Destroy(GameObject.FindGameObjectsWithTag("Audio")[0]);
		}
		if (PlayerPrefs.HasKey("MasterVol"))
		{
			float volume = PlayerPrefs.GetFloat("MasterVol");
			value = Mathf.Lerp(-80f, -5f, volume);
		}
		else
		{
			mixer.SetFloat("Master", -5f);
			PlayerPrefs.SetFloat("MasterVol", 1f);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!PlayerPrefs.HasKey("rainbowBouncers"))
		{
			PlayerPrefs.SetInt("rainbowBouncers", 0);
		}
		DateTime dateTime = new DateTime(2023, 6, 20);
		if (Application.version.Contains("Beta") && DateTime.Today > dateTime)
		{
			Application.Quit();
		}
		if (!PlayerPrefs.HasKey("shakeMultiplier"))
		{
			PlayerPrefs.SetFloat("shakeMultiplier", 0.5f);
		}
		if (!PlayerPrefs.HasKey("Skin"))
		{
			PlayerPrefs.SetString("Skin", "Default");
		}
		SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
		SteamUserStats.GetUserStat(SteamUser.GetSteamID(), "EasyHS", out int eHS);
		SteamUserStats.GetUserStat(SteamUser.GetSteamID(), "MediumHS", out int mHS);
		SteamUserStats.GetUserStat(SteamUser.GetSteamID(), "HardHS", out int hHS);
		SteamUserStats.GetUserStat(SteamUser.GetSteamID(), "UnfairHS", out int uHS);
		SyncHighScore(eHS, "ehighscore"); SyncHighScore(eHS, "mhighscore"); SyncHighScore(eHS, "hhighscore"); SyncHighScore(eHS, "uhighscore");
		string folder = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bouncers");
		string path = Path.Join(folder, "skilltree.json");
		if(!File.Exists(path))
		{
			PlayerSkills newPlayerSkills = new PlayerSkills();
			newPlayerSkills.playerType = "test";
			newPlayerSkills.dasherSkillTree = new PlayerSkills.DasherSkills(1, false, 1, false, 1);
			string rawFile = JsonUtility.ToJson(newPlayerSkills);
			if(!Directory.Exists(folder)) Directory.CreateDirectory(folder);
			File.WriteAllText(path, rawFile);
		}
	}

	private void SyncHighScore(int steamHS, string localHSKey)
	{
		if (PlayerPrefs.HasKey(localHSKey))
		{
			if (PlayerPrefs.GetInt(localHSKey) > steamHS)
			{
				SteamUserStats.SetStat("EasyHS", PlayerPrefs.GetInt(localHSKey));
				SteamUserStats.StoreStats();
			}
			else if(PlayerPrefs.GetInt(localHSKey) < steamHS)
			{
				PlayerPrefs.SetInt(localHSKey, steamHS);
			}
		}
		else
		{
			PlayerPrefs.SetInt(localHSKey, steamHS);
		}
	}

	private void Update()
	{
		if (UnityEngine.Object.FindFirstObjectByType<AudioManager>() != null && !started)
		{
			mixer.SetFloat("Master", value);
			UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("theme");
			started = true;
		}
	}
}

[System.Serializable]
public class PlayerSkills
{
	public string playerType;
	public DasherSkills dasherSkillTree;

	[Serializable]
	public struct DasherSkills
	{
		public float speedMultiplier;
		public bool hasDash;
		public float dashForceMultiplier;
		public bool hasChargedDash;
		public float slowMotionRangeMultiplier;

		public DasherSkills(float speedMultiplier, bool hasDash, float dashForceMultiplier, bool hasChargedDash, float slowMotionRangeMultiplier)
		{
			this.speedMultiplier = speedMultiplier;
			this.hasDash = hasDash;
			this.dashForceMultiplier = dashForceMultiplier;
			this.hasChargedDash = hasChargedDash;
			this.slowMotionRangeMultiplier = slowMotionRangeMultiplier;
		}
	}

	[Serializable]
	public struct LooterSkills
	{
		public int scorePerPowerup;
		public bool hasStonks; // 1/10 chance for another powerup to spawn when one is collected
		public int stonksChance;
		public float scoreReqReduction;
		public bool startPowerup;

		public LooterSkills(int scorePerPowerup, bool hasStonks, int stonksChance, float scoreReqReduction, bool startPowerup)
		{
			this.scorePerPowerup = scorePerPowerup;
			this.hasStonks = hasStonks;
			this.stonksChance = stonksChance;
			this.scoreReqReduction = scoreReqReduction;
			this.startPowerup = startPowerup;
		}
	}

	[Serializable]
	public struct TankClass
	{
		public int healthBoost;
		public bool shieldBuff;
		public int shieldLives;
		public int thornsChanceBoost;
		public bool thorns; // 1/10 chance attacker dies, +20 score when triggered

		public TankClass(int healthBoost, bool shieldBuff, int shieldLives, int thornsChanceBoost, bool thorns)
		{
			this.healthBoost = healthBoost;
			this.shieldBuff = shieldBuff;
			this.shieldLives = shieldLives;
			this.thornsChanceBoost = thornsChanceBoost;
			this.thorns = thorns;
		}
	}
}
