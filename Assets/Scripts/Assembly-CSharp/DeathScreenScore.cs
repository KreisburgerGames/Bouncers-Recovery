using System.Collections.Generic;
using System.Linq;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScore : MonoBehaviour
{
	private int score;

	public TMP_Text text;

	public Text diff;

	public GameObject player;

	private BoxCollider2D box;

	private Rigidbody2D rb;

	public Camera camera;

	public TMP_Text highscore;

	private bool started;

	private HighScores leader;

	private bool highScoreSet;
	public List<string> deathStatuses = new List<string>();
	[SerializeField]
	public List<StringArrayWrapper> deathReasons = new List<StringArrayWrapper>();
	private Dictionary<string, string[]> deathReasonsDict = new Dictionary<string, string[]>
	{

	};

	[System.Serializable]
	public class StringArrayWrapper
	{
		public string attacker, reason;
	}

	private void Awake()
	{
		print(deathReasons);
		foreach(StringArrayWrapper reason in deathReasons)
		{
			string[] old;	
			try
			{
				old = deathReasonsDict[reason.attacker];
			}
			catch
			{
				old = new string[0];
			}
			string[] newReasons = old.Concat(new string[] { reason.reason }).ToArray();
			deathReasonsDict[reason.attacker] = newReasons;
			foreach(string reasonF in newReasons.ToList()) print(reasonF);
		}
		Cursor.visible = true;
		score = GameObject.Find("Player").GetComponent<GameManager>().score;
		print(deathReasonsDict.TryGetValue(PlayerPrefs.GetString("deathReason"), out string[] reasonList));
		string reasonUsing = reasonList[Random.Range(0, reasonList.Length)];
		SteamAPI.Init();
		if(!Application.isEditor)
			SteamFriends.SetRichPresence("st", reasonUsing + " and " + deathStatuses[Random.Range(0, deathStatuses.Count)] + ". " + score + " score on " + PlayerPrefs.GetString("diff"));
		if(PlayerPrefs.HasKey("tark") && PlayerPrefs.GetInt("tark") == 1 && PlayerPrefs.GetString("diff") == "Unfair")
		{
			int choice = UnityEngine.Random.Range(1, 3);
			if (choice == 1)
				SteamFriends.SetRichPresence("st", "Got tarkov'd");
			else if (choice == 2)
				SteamFriends.SetRichPresence("st", "(head, eyes)");
		}
		started = false;
		Object.Destroy(GameObject.Find("Player"));
		text.text = "Score: " + score;
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			if (PlayerPrefs.HasKey("ehighscore"))
			{
				if (score > PlayerPrefs.GetInt("ehighscore"))
				{
					PlayerPrefs.SetInt("ehighscore", score);
					highscore.color = Color.green;
				}
				else
				{
					highscore.color = Color.white;
				}
			}
			else
			{
				PlayerPrefs.SetInt("ehighscore", score);
				highscore.color = Color.white;
			}
			highscore.text = "Highscore: " + PlayerPrefs.GetInt("ehighscore");
		}
		if (PlayerPrefs.GetString("diff") == "Medium")
		{
			if (PlayerPrefs.HasKey("mhighscore"))
			{
				if (score > PlayerPrefs.GetInt("mhighscore"))
				{
					PlayerPrefs.SetInt("mhighscore", score);
					highscore.color = Color.green;
				}
				else
				{
					highscore.color = Color.white;
				}
			}
			else
			{
				PlayerPrefs.SetInt("mhighscore", score);
				highscore.color = Color.white;
			}
			highscore.text = "Highscore: " + PlayerPrefs.GetInt("mhighscore");
		}
		if (PlayerPrefs.GetString("diff") == "Hard")
		{
			if (PlayerPrefs.HasKey("hhighscore"))
			{
				if (score > PlayerPrefs.GetInt("hhighscore"))
				{
					PlayerPrefs.SetInt("hhighscore", score);
					highscore.color = Color.green;
				}
				else
				{
					highscore.color = Color.white;
				}
			}
			else
			{
				PlayerPrefs.SetInt("hhighscore", score);
				highscore.color = Color.white;
			}
			highscore.text = "Highscore: " + PlayerPrefs.GetInt("hhighscore");
		}
		if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			if (PlayerPrefs.HasKey("uhighscore"))
			{
				if (score > PlayerPrefs.GetInt("uhighscore"))
				{
					PlayerPrefs.SetInt("uhighscore", score);
					highscore.color = Color.green;
				}
				else
				{
					highscore.color = Color.white;
				}
			}
			else
			{
				PlayerPrefs.SetInt("uhighscore", score);
				highscore.color = Color.white;
			}
			highscore.text = "Highscore: " + PlayerPrefs.GetInt("uhighscore");
		}
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			diff.text = "Easy Difficulty";
			diff.color = Color.green;
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			diff.text = "Medium Difficulty";
			diff.color = Color.yellow;
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			diff.text = "Hard Difficulty";
			diff.color = Color.red;
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			diff.text = "Unfair Difficulty";
			diff.color = Color.blue;
		}
	}

	private void Update()
	{
		if (Object.FindFirstObjectByType<AudioManager>() != null && !started)
		{
			Object.FindFirstObjectByType<AudioManager>().Play("purgatory");
			started = true;
		}
		if (Object.FindFirstObjectByType<HighScores>() != null && !highScoreSet)
		{
			leader = Object.FindFirstObjectByType<HighScores>();
			leader.UploadScore(SteamUser.GetSteamID(), score);
			highScoreSet = true;
		}
	}
}
