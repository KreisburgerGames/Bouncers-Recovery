using System;
using Steamworks;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
	private Rigidbody2D rb;

	public float maxVelocity = 13f;

	public float easyStartSpeed = 6f;

	public float mediumStartSpeed = 7f;

	public float hardStartSpeed = 8f;

	public float startSpeed = 8f;

	private string lastBounce;

	public Player player;

	[HideInInspector]
	public GameManager manager;

	public float paddingMultiplier = 1f;

	public float cornerPaddingMultiplier = 2f;

	private System.Random r = new System.Random();

	public ParticleSystem bloodSplash;

	public int directionRange = 3;

	private float previousx;

	private float previousy;

	public Camera camera;

	private float width;

	private float height;

	private ScreenShake shake;

	private bool vertical;

	private bool damage;

	public float left;

	private bool started;

	private bool set;

	public GameObject floatText;

	public bool menu;

	private bool setPlayer;
    public float unfairStartSpeed;

    public void Spawned()
	{
		SteamAPI.Init();
		if (PlayerPrefs.GetInt("rainbowBouncers") == 1)
		{
			float h = UnityEngine.Random.Range(0f, 1.01f);
			base.gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(h, 1f, 0.5f);
			base.gameObject.GetComponent<TrailRenderer>().material.color = Color.HSVToRGB(h, 1f, 1f);
		}
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			startSpeed = easyStartSpeed;
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			startSpeed = mediumStartSpeed;
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			startSpeed = hardStartSpeed;
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			startSpeed = unfairStartSpeed;
		}
		else
		{
			startSpeed = 7f;
		}
		if (!menu)
		{
			player = GameObject.Find("Player").gameObject.GetComponent<Player>();
		}
		manager = player.gameObject.GetComponent<GameManager>();
		camera = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>();
		shake = camera.gameObject.GetComponent<ScreenShake>();
		if (!manager.isCountdown && manager.countdownTime <= 0f)
		{
			rb.velocity = new Vector2(startSpeed, 0f);
		}
		previousx = base.transform.position.x;
		previousy = base.transform.position.y;
	}

	private void Start()
	{
		SteamAPI.Init();
		previousx = base.transform.position.x;
		previousy = base.transform.position.y;
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		set = false;
		player = UnityEngine.Object.FindFirstObjectByType<Player>().gameObject.GetComponent<Player>();
		manager = player.gameObject.GetComponent<GameManager>();
		camera = GameObject.Find("/Main Camera").gameObject.GetComponent<Camera>();
		shake = camera.gameObject.GetComponent<ScreenShake>();
		if (menu)
		{
			Spawned();
			player.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		float x = base.transform.position.x;
		float y = base.transform.position.y;
		float num = x - previousx;
		float num2 = y - previousy;
		float num3 = num * paddingMultiplier;
		float num4 = num2 * paddingMultiplier;
		float num5 = (num + num2) * cornerPaddingMultiplier;
		bool flag = false;
		if (!setPlayer && UnityEngine.Object.FindFirstObjectByType<Player>() != null)
		{
			player = UnityEngine.Object.FindFirstObjectByType<Player>().gameObject.GetComponent<Player>();
			manager = player.gameObject.GetComponent<GameManager>();
			setPlayer = true;
			setPlayer = true;
		}
		if ((setPlayer && !started && !manager.isCountdown) || (menu && manager.countdownTime <= 0f))
		{
			if (PlayerPrefs.GetString("diff") == "Easy")
			{
				startSpeed = easyStartSpeed;
			}
			else if (PlayerPrefs.GetString("diff") == "Medium")
			{
				startSpeed = mediumStartSpeed;
			}
			else if (PlayerPrefs.GetString("diff") == "Hard")
			{
				startSpeed = hardStartSpeed;
			}
			else if (PlayerPrefs.GetString("diff") == "Unfair")
			{
				startSpeed = unfairStartSpeed;
			}
			else
			{
				startSpeed = 7f;
			}
			rb.velocity = new Vector2(startSpeed, 0f);
			started = true;
		}
		_ = Screen.width;
		if (!set)
		{
			width = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0f)).x;
			height = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0f)).y;
			left = camera.ScreenToWorldPoint(new Vector3(0f, Screen.height / 2, 0f)).x;
			set = true;
		}
		previousx = base.transform.position.x;
		previousy = base.transform.position.y;
		if (base.transform.position.x + num3 + base.transform.lossyScale.x >= width && lastBounce != "right" && manager.countdownTime <= 0f)
		{
			if (base.transform.position.y - num5 <= 0f - height && base.transform.position.y - num5 <= 0f && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f + 1f);
				flag = true;
			}
			else if (base.transform.position.y + num5 >= height && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f - 1f);
				flag = true;
			}
			rb.velocity = new Vector2(UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed + 2f), rb.velocity.y);
			if (base.transform.position.y > 0f && !flag && !vertical)
			{
				if (base.transform.position.y < 3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(startSpeed - 2f, maxVelocity));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed));
				}
			}
			else if (!flag && !vertical)
			{
				if (base.transform.position.y > -3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(directionRange * -1, directionRange));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(0, directionRange));
				}
			}
			UnityEngine.Object.FindFirstObjectByType<AchivementManager>().AddBounce();
			lastBounce = "right";
			if (manager.canScore)
			{
				manager.score += manager.multiplier;
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("bounce");
				if (PlayerPrefs.GetString("diff") == "Medium" && manager.score >= 1000)
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("!");
				}
			}
			damage = true;
			vertical = false;
		}
		else if (base.transform.position.x + num3 - base.transform.lossyScale.x <= 0f - width && lastBounce != "left" && manager.countdownTime <= 0f)
		{
			if (base.transform.position.y - num5 <= 0f - height && base.transform.position.y - num5 <= 0f && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f + 1f);
				flag = true;
			}
			else if (base.transform.position.y + num5 >= height && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f - 1f);
				flag = true;
			}
			rb.velocity = new Vector2(UnityEngine.Random.Range(startSpeed - 2f, maxVelocity), rb.velocity.y);
			if (base.transform.position.y > 0f && !flag && !vertical)
			{
				if (base.transform.position.y < 3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(startSpeed - 2f, maxVelocity));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed));
				}
			}
			else if (!flag && !vertical)
			{
				if (base.transform.position.y > -3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(directionRange * -1, directionRange));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(0, directionRange));
				}
			}
			lastBounce = "left";
			UnityEngine.Object.FindFirstObjectByType<AchivementManager>().AddBounce();
			damage = true;
			if (manager.canScore)
			{
				manager.score += manager.multiplier;
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("bounce");
				if (PlayerPrefs.GetString("diff") == "Medium" && manager.score >= 500)
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("Impressive");
				}
				if (PlayerPrefs.GetString("diff") == "Unfair" && manager.score >= 500)
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("hellandback");
				}
				if (PlayerPrefs.GetString("diff") == "Easy" && manager.score >= 100)
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("FirstSteps");
				}
			}
		}
		else if (base.transform.position.y + num4 - base.transform.lossyScale.y <= 0f - height && lastBounce != "down" && !flag && manager.countdownTime <= 0f)
		{
			vertical = false;
			rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(startSpeed - 2f, maxVelocity));
			lastBounce = "down";
			UnityEngine.Object.FindFirstObjectByType<AchivementManager>().AddBounce();
			if (UnityEngine.Random.Range(1, 6) == 3)
			{
				rb.velocity = new Vector2(rb.velocity.x * -1f + (float)UnityEngine.Random.Range(-2, 2), rb.velocity.y);
			}
			if (manager.canScore)
			{
				manager.score += manager.multiplier;
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("bounce");
				if (PlayerPrefs.GetString("diff") == "Medium" && manager.score >= 1000)
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("Impressive");
				}
			}
			vertical = true;
			damage = true;
		}
		else if (base.transform.position.y + num4 + base.transform.lossyScale.y >= height && lastBounce != "up" && !flag && manager.countdownTime <= 0f)
		{
			vertical = false;
			rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed + 2f));
			lastBounce = "up";
			UnityEngine.Object.FindFirstObjectByType<AchivementManager>().AddBounce();
			if (UnityEngine.Random.Range(1, 6) == 3)
			{
				rb.velocity = new Vector2(rb.velocity.x * -1f + (float)UnityEngine.Random.Range(-2, 2), rb.velocity.y);
			}
			if (manager.canScore)
			{
				manager.score += manager.multiplier;
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("bounce");
				if (PlayerPrefs.GetString("diff") == "Medium" && manager.score >= 1000)
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("Impressive");
				}
			}
			damage = true;
			vertical = true;
		}
		if (setPlayer && rb.velocity.x < maxVelocity * -1f && !manager.isCountdown && manager.countdownTime <= 0f)
		{
			rb.velocity = new Vector2(maxVelocity * -1f, rb.velocity.y);
		}
		if (setPlayer && rb.velocity.x > maxVelocity && !manager.isCountdown && manager.countdownTime <= 0f)
		{
			rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
		}
		if (setPlayer && rb.velocity.y < maxVelocity * -1f && !manager.isCountdown && manager.countdownTime <= 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, maxVelocity * -1f);
		}
		if (setPlayer && rb.velocity.y > maxVelocity && !manager.isCountdown && manager.countdownTime <= 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
		}
		flag = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!(collision.gameObject.tag == "Player") || !damage || !player.canMove)
		{
			return;
		}
		if (!player.hasSheild)
		{
			if (manager.lastHit < manager.score - manager.bouncers)
			{
				int num;
				if (PlayerPrefs.GetString("diff") == "Easy")
				{
					num = r.Next(player.easyDamageMin, player.easyDamageMax);
					player.health -= num;
				}
				else if (PlayerPrefs.GetString("diff") == "Medium")
				{
					num = r.Next(player.mediumDamageMin, player.mediumDamageMax);
					player.health -= num;
				}
				else if (PlayerPrefs.GetString("diff") == "Hard")
				{
					num = r.Next(player.hardDamageMin, player.hardDamageMax);
					player.health -= num;
				}
				else if (PlayerPrefs.GetString("diff") == "Unfair")
				{
					num = r.Next(player.unfairDamageMin, player.unfairDamageMax);
					player.health -= num;
				}
				else
				{
					num = 0;
				}
				UnityEngine.Object.Instantiate(bloodSplash, player.transform.position, Quaternion.identity);
				UnityEngine.Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num, Color.red, null, null, 2f, 4f);
				manager.lastHit = manager.score;
				manager.multBounce = manager.score;
				player.GetComponentInChildren<SlowDown>().speedUp = true;
				player.GetComponentInChildren<SlowDown>().slowDown = false;
				if (player.health > 0)
				{
					UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("hit");
				}
				shake.start = true;
			}
		}
		else
		{
			player.hasSheild = false;
			UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("break");
			player.ShieldBreak();
			UnityEngine.Object.Instantiate(player.shieldLoss, base.transform.position, Quaternion.identity);
		}
	}
}
