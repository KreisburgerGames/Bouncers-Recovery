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

	private void Start()
	{
		SteamAPI.Init();
	}

	private void Awake()
	{
		Cursor.visible = true;
		score = GameObject.Find("Player").GetComponent<GameManager>().score;
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
