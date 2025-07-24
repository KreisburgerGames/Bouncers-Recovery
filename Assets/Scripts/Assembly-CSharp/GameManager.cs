using System;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public int score;

	public int lastHit;

	public int bouncers;

	public Player player;

	private GameObject playerRef;

	[HideInInspector]
	[SerializeField]
	public Rigidbody2D rb;

	public static float width;

	public static float height;

	public float scoreGoal = 5f;

	public float scoreGoalMultiplier = 3f;

	public Bouncer bouncer;

	public ParticleSystem bouncerSpawn;

	public float spawnPadding = 1f;

	public bool canScore = true;

	private int starsSpawned;

	public bool falling;

	public int easyHealthMin = 85;

	public int mediumHealthMin = 65;

	public int hardHealthMin = 50;

	public string scene;

	private float cooldown;

	public float minWait = 5f;

	public float maxWait = 10f;

	public float wait;

	public int easyMinPassiveHealth = 60;

	public int mediumMinPassiveHealth = 45;

	public int hardMinPassiveHealth = 20;
	public int unfairMinPassiveHealth = 5;

	public int easyPassiveHealMax = 85;

	public int mediumPassiveHealMax = 65;

	public int hardPassiveHealMax = 45;
	public int unfairPassiveHealthMax = 20;

	public int minPassiveHealAmount = 5;

	public int maxPassiveHealAmount = 10;

	public int multiplierGoal = 10;

	public int multiplier = 1;

	[Range(3f, 10f)]
	public float multiplierGoalMultiplier;

	public float minHealWait = 3f;

	public float maxHealWait = 7f;

	private float healWait;

	private float healCooldown;

	private float canHealCooldown;

	private float canHealCooldownTime;

	public int easyPowerupScoreMin;

	public float asteroidCooldownTime;

	public float asteroidTimer;

	public float minAsteroidCooldown = 10f;

	public float maxAsteroidCooldown = 15f;

	private float asteroidsSpawned;

	private float spawenedDiffIncrease = 5f;

	public int easyPowerupScoreMax;

	public int mediumPowerupScoreMin;

	public int mediumPowerupScoreMax;

	public int hardPowerupScoreMin;

	public int hardPowerupScoreMax;
	public int unfairPowerupScoreMax, unfairPowerupScoreMin;

	private int easyScoreGoal;

	public int easyMaxBouncers = 5;

	public int mediumMaxBouncers = 7;

	public int hardMaxBouncers = 9;
	public int unfairMaxBouncers = 12;

	private int mediumScoreGoal;

	private int hardScoreGoal;
	private int unfairScoreGoal;

	private int plusLivesSpawned;

	public int multBounce;

	public GameObject dasher;

	public float minDasherCooldown;

	public float maxDasherCooldown;

	private float dasherTimer;

	private float dasherCooldown;

	public GameObject star;

	public GameObject speed;

	private int speedsSpawned;

	public int oneUps;

	public int maxOneUps = 3;

	private TMP_Text countdown;

	private TMP_Text avoid;

	public GameObject asteroid;

	public ParticleSystem plusScore;

	public Image redHealth;

	[HideInInspector]
	public bool isCountdown;

	public float countdownTime = 3f;

	public Canvas canvas;

	public GameObject plusOneLife;

	private bool start = true;

	[HideInInspector]
	public bool healing;

	[HideInInspector]
	public bool canHeal = true;

	[HideInInspector]
	public bool tried;

	public GameObject healthPack;

	[Range(0f, 1000f)]
	public int easyHealthPackChance;

	[Range(0f, 1000f)]
	public int mediumHealthPackChance;

	[Range(0f, 1000f)]
	public int hardHealthPackChance;
	[Range(0f, 1000f)]
	public int unfairHealthPackChance;

	[Range(0f, 1f)]
	public float deathChromaticSpeed;

	private bool played;

	public ParticleSystem miniSquareBuff;

	private int diffUp;

	public int maxDiffUp = 5;

	public GameObject shield;

	private int shieldsSpawned;

	private bool mainThemeStarted;

	public GameObject small;

	private int smallsSpawned;

	public GameObject rainbowBouncer;

	public GameObject thief;

	public float thiefTimeMin;

	public float thiefTimeMax;

	private float thiefTimer;

	public GameObject purge;

	private int purgesSpawned;

	public GameObject healStation;

	public float minHealStationWait = 30f;

	public float maxHealStationWait = 60f;

	private float healStationWait;

	public Vector2 healStationStartPos;

	public float timeSurvived;

	[HideInInspector]
	public bool shieldCollected;

	[HideInInspector]
	public bool speedCollected;

	[HideInInspector]
	public bool smallCollected;

	[HideInInspector]
	public bool starCollected;

	[HideInInspector]
	public bool plusLifeCollected;

	[HideInInspector]
	public bool purgeCollected;
    private int unfairHealthMin;
    private int unfairPassiveHealMax;

    private void Start()
	{
		SteamAPI.Init();
		score = 0;
		lastHit = 1;
		bouncers = 1;
		isCountdown = true;
		UnityEngine.Object.FindFirstObjectByType<Bouncer>().gameObject.GetComponent<TrailRenderer>().enabled = false;
		UnityEngine.Object.FindFirstObjectByType<Bouncer>().gameObject.transform.position = new Vector2(UnityEngine.Random.Range(UnityEngine.Object.FindFirstObjectByType<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * -1f + 1f, UnityEngine.Object.FindFirstObjectByType<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 1f), UnityEngine.Random.Range(UnityEngine.Object.FindFirstObjectByType<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y * -1f + 1f, UnityEngine.Object.FindFirstObjectByType<Camera>().ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y - 1f));
		UnityEngine.Object.FindFirstObjectByType<Bouncer>().gameObject.GetComponent<TrailRenderer>().enabled = true;
	}

	private void Awake()
	{
		instance = this;
		canScore = true;
		timeSurvived = 0f;
		healStationWait = UnityEngine.Random.Range(minHealStationWait, maxHealStationWait);
		dasherCooldown = UnityEngine.Random.Range(minDasherCooldown, maxDasherCooldown + 1f);
		isCountdown = true;
		asteroidCooldownTime = UnityEngine.Random.Range(minAsteroidCooldown, maxAsteroidCooldown);
		oneUps = 0;
		speedsSpawned = 0;
		starsSpawned = 0;
		wait = UnityEngine.Random.Range(minWait, maxWait + 1f);
		thiefTimer = UnityEngine.Random.Range(thiefTimeMin, thiefTimeMax);
		isCountdown = true;
		player.canMove = false;
		countdown = GameObject.Find("/Main Camera/UI/HUD/Countdown/").GetComponent<TMP_Text>();
		avoid = GameObject.Find("/Main Camera/UI/HUD/AVOID/").GetComponent<TMP_Text>();
		countdownTime = 3f;
		plusLivesSpawned = 0;
		width = player.camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0f)).x;
		height = player.camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0f)).y;
		rb = player.gameObject.GetComponent<Rigidbody2D>();
		playerRef = player.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			scoreGoalMultiplier = 6f;
			easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax + 1);
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			scoreGoalMultiplier = 5.5f;
			mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax + 1);
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			scoreGoalMultiplier = 4.5f;
			hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax + 1);
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			scoreGoalMultiplier = 5f;
			unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax + 1);
		}
		played = false;
	}

	private void Update()
	{
		if (!isCountdown)
		{
			timeSurvived += Time.deltaTime;
		}
		if (shieldCollected && speedCollected && smallCollected && purgeCollected && plusLifeCollected && starCollected && timeSurvived < 120f && PlayerPrefs.GetString("diff") == "Hard")
		{
			UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("OP");
		}
		healStationWait -= Time.deltaTime;
		if (healStationWait <= 0f)
		{
			healStationWait = UnityEngine.Random.Range(minHealStationWait, maxHealStationWait);
			if (UnityEngine.Random.Range(1, 6) == 5)
			{
				UnityEngine.Object.Instantiate(healStation, healStationStartPos, Quaternion.identity);
			}
		}
		thiefTimer -= Time.deltaTime;
		if (thiefTimer <= 0f)
		{
			thiefTimer = UnityEngine.Random.Range(thiefTimeMin, thiefTimeMax);
			if (UnityEngine.Random.Range(1, 6) == 3)
			{
				UnityEngine.Object.Instantiate(thief, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
			}
		}
		if (countdownTime >= 0f)
		{
			if (!played)
			{
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("beggining");
				played = true;
			}
			countdownTime -= Time.deltaTime;
			if (countdownTime <= 0f)
			{
				avoid.enabled = false;
				countdown.enabled = false;
				isCountdown = false;
				countdownTime = 0f;
				player.canMove = true;
				if (!mainThemeStarted)
				{
					UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("main theme");
					mainThemeStarted = true;
				}
			}
			else
			{
				countdown.text = Mathf.Ceil(countdownTime).ToString();
			}
		}
		asteroidTimer += Time.deltaTime;
		if (asteroidTimer >= asteroidCooldownTime)
		{
			switch (UnityEngine.Random.Range(1, 5))
			{
			case 1:
				UnityEngine.Object.Instantiate(asteroid, new Vector2(UnityEngine.Random.Range(0f - width, width), height + 1f), Quaternion.identity).GetComponent<Asteroid>().Spawn();
				asteroidsSpawned += 1f;
				break;
			case 2:
				UnityEngine.Object.Instantiate(asteroid, new Vector2(UnityEngine.Random.Range(0f - width, width), 0f - height - 1f), Quaternion.identity).GetComponent<Asteroid>().Spawn();
				asteroidsSpawned += 1f;
				break;
			case 3:
				UnityEngine.Object.Instantiate(asteroid, new Vector2(0f - width - 1f, UnityEngine.Random.Range(0f - height, height)), Quaternion.identity).GetComponent<Asteroid>().Spawn();
				asteroidsSpawned += 1f;
				break;
			case 4:
				UnityEngine.Object.Instantiate(asteroid, new Vector2(width + 1f, UnityEngine.Random.Range(0f - height, height)), Quaternion.identity).GetComponent<Asteroid>().Spawn();
				asteroidsSpawned += 1f;
				break;
			}
			asteroidTimer = 0f;
			asteroidCooldownTime = UnityEngine.Random.Range(minAsteroidCooldown, maxAsteroidCooldown);
		}
		if (isCountdown)
		{
			return;
		}
		dasherTimer += Time.deltaTime;
		if (dasherTimer >= dasherCooldown)
		{
			dasherTimer = 0f;
			for (int i = 0; i < UnityEngine.Random.Range(1, 6); i++)
			{
				switch (UnityEngine.Random.Range(1, 5))
				{
				case 1:
					UnityEngine.Object.Instantiate(dasher, new Vector2(UnityEngine.Random.Range(0f - width, width), height + 1f), Quaternion.identity);
					dasherCooldown = UnityEngine.Random.Range(minDasherCooldown, maxDasherCooldown + 1f);
					break;
				case 2:
					UnityEngine.Object.Instantiate(dasher, new Vector2(UnityEngine.Random.Range(0f - width, width), 0f - height - 1f), Quaternion.identity);
					dasherCooldown = UnityEngine.Random.Range(minDasherCooldown, maxDasherCooldown + 1f);
					break;
				case 3:
					UnityEngine.Object.Instantiate(dasher, new Vector2(0f - width - 1f, UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					dasherCooldown = UnityEngine.Random.Range(minDasherCooldown, maxDasherCooldown + 1f);
					break;
				case 4:
					UnityEngine.Object.Instantiate(dasher, new Vector2(width + 1f, UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					dasherCooldown = UnityEngine.Random.Range(minDasherCooldown, maxDasherCooldown + 1f);
					break;
				}
			}
			minDasherCooldown -= 0.05f;
			maxDasherCooldown -= 0.05f;
			minDasherCooldown = Mathf.Clamp(minDasherCooldown, 1f, 10f);
			maxDasherCooldown = Mathf.Clamp(maxDasherCooldown, 3f, 15f);
		}
		if (PlayerPrefs.GetString("diff") == "Easy" && bouncers < score / 100 && score / 100 < easyMaxBouncers)
		{
			Vector3 position = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
			UnityEngine.Object.Instantiate(this.bouncer, position, Quaternion.identity).Spawned();
			UnityEngine.Object.Instantiate(bouncerSpawn, position, Quaternion.identity);
			bouncers++;
			if (UnityEngine.Random.Range(1, 26) == 5)
			{
				UnityEngine.Object.Instantiate(rainbowBouncer, position, Quaternion.identity);
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Medium" && bouncers < score / 100 && score / 100 < mediumMaxBouncers)
		{
			Vector3 position2 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
			UnityEngine.Object.Instantiate(this.bouncer, position2, Quaternion.identity).Spawned();
			UnityEngine.Object.Instantiate(bouncerSpawn, position2, Quaternion.identity);
			bouncers++;
			if (UnityEngine.Random.Range(1, 26) == 5)
			{
				UnityEngine.Object.Instantiate(rainbowBouncer, position2, Quaternion.identity);
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Hard" && bouncers < score / 100 && score / 100 < hardMaxBouncers)
		{
			Vector3 position3 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
			UnityEngine.Object.Instantiate(this.bouncer, position3, Quaternion.identity).Spawned();
			UnityEngine.Object.Instantiate(bouncerSpawn, position3, Quaternion.identity);
			bouncers++;
			if (UnityEngine.Random.Range(1, 26) == 5)
			{
				UnityEngine.Object.Instantiate(rainbowBouncer, position3, Quaternion.identity);
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair" && bouncers < score / 1000 && score / 10 < unfairMaxBouncers)
		{
			print("spawn");
			Vector3 position3 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
			UnityEngine.Object.Instantiate(this.bouncer, position3, Quaternion.identity).Spawned();
			UnityEngine.Object.Instantiate(bouncerSpawn, position3, Quaternion.identity);
			bouncers++;
			if (UnityEngine.Random.Range(1, 26) == 5)
			{
				UnityEngine.Object.Instantiate(rainbowBouncer, position3, Quaternion.identity);
			}
		}
		if (PlayerPrefs.GetString("diff") == "Easy" && !isCountdown)
		{
			Bouncer[] array = UnityEngine.Object.FindObjectsOfType<Bouncer>();
			float num = 0f;
			Bouncer[] array2 = array;
			foreach (Bouncer bouncer in array2)
			{
				num += MathF.Abs(bouncer.GetComponent<Rigidbody2D>().velocity.x) + MathF.Abs(bouncer.GetComponent<Rigidbody2D>().velocity.y);
			}
			if (num / (float)array.Length < this.bouncer.easyStartSpeed - 0.5f && score > 100)
			{
				Vector3 position4 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position4, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position4, Quaternion.identity);
				bouncers++;
				if (UnityEngine.Random.Range(1, 26) == 5)
				{
					UnityEngine.Object.Instantiate(rainbowBouncer, position4, Quaternion.identity);
				}
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Medium" && !isCountdown && score > 100)
		{
			Bouncer[] array3 = UnityEngine.Object.FindObjectsOfType<Bouncer>();
			float num2 = 0f;
			Bouncer[] array2 = array3;
			foreach (Bouncer bouncer2 in array2)
			{
				num2 += MathF.Abs(bouncer2.GetComponent<Rigidbody2D>().velocity.x) + MathF.Abs(bouncer2.GetComponent<Rigidbody2D>().velocity.y);
			}
			if (num2 / (float)array3.Length < this.bouncer.mediumStartSpeed - 0.5f - 1f)
			{
				Vector3 position5 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position5, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position5, Quaternion.identity);
				bouncers++;
				if (UnityEngine.Random.Range(1, 26) == 5)
				{
					UnityEngine.Object.Instantiate(rainbowBouncer, position5, Quaternion.identity);
				}
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Hard" && !isCountdown && score > 100)
		{
			Bouncer[] array4 = UnityEngine.Object.FindObjectsOfType<Bouncer>();
			float num3 = 0f;
			Bouncer[] array2 = array4;
			foreach (Bouncer bouncer3 in array2)
			{
				num3 += MathF.Abs(bouncer3.GetComponent<Rigidbody2D>().velocity.x) + MathF.Abs(bouncer3.GetComponent<Rigidbody2D>().velocity.y);
			}
			if (num3 / (float)array4.Length < this.bouncer.hardStartSpeed - 0.5f - 1f)
			{
				Vector3 position6 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position6, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position6, Quaternion.identity);
				bouncers++;
				if (UnityEngine.Random.Range(1, 26) == 5)
				{
					UnityEngine.Object.Instantiate(rainbowBouncer, position6, Quaternion.identity);
				}
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair" && !isCountdown && score > 100 && bouncers < unfairMaxBouncers)
		{
			Bouncer[] array4 = UnityEngine.Object.FindObjectsOfType<Bouncer>();
			float num3 = 0f;
			Bouncer[] array2 = array4;
			foreach (Bouncer bouncer3 in array2)
			{
				num3 += MathF.Abs(bouncer3.GetComponent<Rigidbody2D>().velocity.x) + MathF.Abs(bouncer3.GetComponent<Rigidbody2D>().velocity.y);
			}
			if (num3 / ((float)array4.Length/3) < this.bouncer.unfairStartSpeed - 1f)
			{
				Vector3 position6 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position6, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position6, Quaternion.identity);
				print("other");
				bouncers++;
				if (UnityEngine.Random.Range(1, 26) == 5)
				{
					UnityEngine.Object.Instantiate(rainbowBouncer, position6, Quaternion.identity);
				}
			}
		}
		if ((float)score >= scoreGoal)
		{
			if (PlayerPrefs.GetString("diff") == "Easy" && bouncers < easyMaxBouncers)
			{
				Vector3 position7 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position7, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position7, Quaternion.identity);
				bouncers++;
				scoreGoal *= scoreGoalMultiplier;
				scoreGoal += UnityEngine.Random.Range(MathF.Round(-score / 3), MathF.Round(score / 3));
				if (UnityEngine.Random.Range(1, 6) == 3)
				{
					tried = false;
					cooldown = 0f;
				}
				if (UnityEngine.Random.Range(1, 26) == 5)
				{
					UnityEngine.Object.Instantiate(rainbowBouncer, position7, Quaternion.identity);
				}
			}
			if (PlayerPrefs.GetString("diff") == "Medium" && bouncers < mediumMaxBouncers)
			{
				Vector3 position8 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position8, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position8, Quaternion.identity);
				bouncers++;
				scoreGoal *= scoreGoalMultiplier;
				scoreGoal += UnityEngine.Random.Range(MathF.Round(-score / 3), MathF.Round(score / 3) + 1f);
				if (UnityEngine.Random.Range(1, 6) == 3)
				{
					tried = false;
					cooldown = 0f;
				}
			}
			if (PlayerPrefs.GetString("diff") == "Hard" && bouncers < hardMaxBouncers)
			{
				Vector3 position9 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position9, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position9, Quaternion.identity);
				bouncers++;
				scoreGoal *= scoreGoalMultiplier;
				scoreGoal += UnityEngine.Random.Range(MathF.Round(-score / 3), MathF.Round(score / 3) + 1f);
				if (UnityEngine.Random.Range(1, 7) == 3)
				{
					tried = false;
					cooldown = 0f;
				}
			}
			if (PlayerPrefs.GetString("diff") == "Unfair" && bouncers < unfairMaxBouncers)
			{
				Vector3 position9 = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1f, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z);
				UnityEngine.Object.Instantiate(this.bouncer, position9, Quaternion.identity).Spawned();
				UnityEngine.Object.Instantiate(bouncerSpawn, position9, Quaternion.identity);
				bouncers++;
				scoreGoal *= scoreGoalMultiplier;
				scoreGoal += UnityEngine.Random.Range(MathF.Round(-score / 5), MathF.Round(score / 2) + 1f);
				if (UnityEngine.Random.Range(1, 10) == 3)
				{
					tried = false;
					cooldown = 0f;
				}
			}
			if (score - multBounce >= multiplierGoal)
			{
				multiplierGoal = (int)MathF.Round((float)multiplierGoal * UnityEngine.Random.Range(multiplierGoalMultiplier / 2f, multiplierGoalMultiplier + 1f));
				multBounce = score;
				UnityEngine.Object.Instantiate(plusScore, base.transform.position, Quaternion.identity);
				FloatText component = UnityEngine.Object.Instantiate(player.floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>();
				multiplier++;
				component.Spawn("x" + multiplier + " Score Multiplier!", Color.green, "true", null, 2f, 4f);
			}
			if (isCountdown)
			{
				countdownTime -= Time.deltaTime;
				if (!played)
				{
					UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("beggining");
					played = true;
				}
				if (countdownTime <= 0f)
				{
					Time.timeScale = 1f;
					countdown.gameObject.SetActive(value: false);
					avoid.gameObject.SetActive(value: false);
					isCountdown = false;
					player.canMove = true;
					UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("main theme");
				}
				countdown.text = MathF.Ceiling(countdownTime).ToString();
			}
		}
		if (player.health <= 0 && !falling)
		{
			if (player.lives == 1 || player.lives == 0)
			{
				player.lives = 0;
				player.canMove = false;
				rb.AddTorque(360f, ForceMode2D.Impulse);
				float value = base.transform.position.x - player.camera.ScreenToWorldPoint(Input.mousePosition).x;
				rb.AddForce(base.transform.up * 500f + base.transform.right * Math.Clamp(value, -1f, 1f) * -250f);
				rb.gravityScale = 1f;
				canScore = false;
				player.chromatic.intensity.Override(0f);
				player.grain.intensity.Override(0f);
				falling = true;
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("death");
				Array.Find(UnityEngine.Object.FindFirstObjectByType<AudioManager>().sounds, (Sound x) => x.name == "main theme").audioSource.Stop();
				UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("Ouch");
				if (timeSurvived <= 5f && PlayerPrefs.GetString("diff") == "Easy")
				{
					UnityEngine.Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("SkillIssue");
				}
			}
			else
			{
				player.lives--;
				player.health = player.maxHealth;
				player.transform.position = new Vector2(0f, 0f);
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("death");
				if (player.lives + 1 == 2)
				{
					GameObject obj = UnityEngine.Object.Instantiate(GameObject.Find("HUD").GetComponent<LivesCount>().breakEffect, GameObject.Find("HUD").GetComponent<LivesCount>().twoLives.transform.position, Quaternion.identity);
					obj.transform.SetParent(canvas.transform, worldPositionStays: false);
					obj.transform.position = new Vector2(GameObject.Find("HUD").GetComponent<LivesCount>().twoLives.transform.position.x, GameObject.Find("HUD").GetComponent<LivesCount>().twoLives.transform.position.y);
				}
				else
				{
					GameObject obj2 = UnityEngine.Object.Instantiate(GameObject.Find("HUD").GetComponent<LivesCount>().breakEffect, GameObject.Find("HUD").GetComponent<LivesCount>().threeLives.transform.position, Quaternion.identity);
					obj2.transform.SetParent(canvas.transform, worldPositionStays: false);
					obj2.transform.position = new Vector2(GameObject.Find("HUD").GetComponent<LivesCount>().threeLives.transform.position.x, GameObject.Find("HUD").GetComponent<LivesCount>().threeLives.transform.position.y);
				}
			}
		}
		if (falling)
		{
			player.canMove = false;
			player.chromatic.intensity.Override(player.chromatic.intensity.value + deathChromaticSpeed * Time.deltaTime);
			player.grain.intensity.Override(player.chromatic.intensity.value + deathChromaticSpeed * Time.deltaTime);
		}
		if (player.transform.position.y <= 0f - height - 30f && falling)
		{
			UnityEngine.Object.DontDestroyOnLoad(playerRef);
			scene = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene("Death");
		}
	}

	private void FixedUpdate()
	{
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			if (player.health < easyHealthMin)
			{
				if (UnityEngine.Random.Range(1, 1001) <= easyHealthPackChance && !tried && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null && !healing)
				{
					UnityEngine.Object.Instantiate(position: new Vector3(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f), 0f), original: healthPack, rotation: Quaternion.identity);
				}
				else if (player.health < easyMinPassiveHealth && UnityEngine.Random.Range(1, 200) <= 5)
				{
					healing = true;
				}
				tried = true;
			}
			if (score >= easyScoreGoal)
			{
				int num = UnityEngine.Random.Range(1, 7);
				if (num == 1 && plusLivesSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(plusOneLife, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax);
					easyScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(easyScoreGoal / 2), (int)MathF.Round(easyScoreGoal * 2));
					easyScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					plusLivesSpawned++;
				}
				else if (num == 2 && starsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(star, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax);
					easyScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(easyScoreGoal / 2), (int)MathF.Round(easyScoreGoal * 2));
					easyScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					starsSpawned++;
				}
				else if (num == 3 && speedsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(speed, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax);
					easyScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(easyScoreGoal / 2), (int)MathF.Round(easyScoreGoal * 2));
					easyScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num == 4 && shieldsSpawned < 5 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(shield, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax);
					easyScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(easyScoreGoal / 2), (int)MathF.Round(easyScoreGoal * 2));
					easyScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num == 5 && smallsSpawned < 1 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(small, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax);
					easyScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(easyScoreGoal / 2), (int)MathF.Round(easyScoreGoal * 2));
					easyScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					smallsSpawned++;
				}
				else if (num == 6 && purgesSpawned < 3 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(purge, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					easyScoreGoal = UnityEngine.Random.Range(easyPowerupScoreMin, easyPowerupScoreMax);
					easyScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(easyScoreGoal / 2), (int)MathF.Round(easyScoreGoal * 2));
					easyScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					purgesSpawned++;
				}
			}
		}
		if (PlayerPrefs.GetString("diff") == "Medium")
		{
			if (player.health < mediumHealthMin)
			{
				if (UnityEngine.Random.Range(1, 1001) <= mediumHealthPackChance && !tried && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null && !healing)
				{
					UnityEngine.Object.Instantiate(position: new Vector3(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f), 0f), original: healthPack, rotation: Quaternion.identity);
				}
				else if (player.health < mediumMinPassiveHealth && UnityEngine.Random.Range(1, 200) <= 5)
				{
					healing = true;
				}
				tried = true;
			}
			if (score >= mediumScoreGoal)
			{
				int num2 = UnityEngine.Random.Range(1, 7);
				if (num2 == 1 && plusLivesSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && (bool)UnityEngine.Object.FindFirstObjectByType<Shield>() && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(plusOneLife, new Vector2(UnityEngine.Random.Range(0f - width, width), UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax + 1);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					plusLivesSpawned++;
				}
				else if (num2 == 2 && starsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && (bool)UnityEngine.Object.FindFirstObjectByType<Shield>() && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(star, new Vector2(UnityEngine.Random.Range(0f - width, width), UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax + 1);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					starsSpawned++;
				}
				else if (num2 == 3 && speedsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && (bool)UnityEngine.Object.FindFirstObjectByType<Shield>() && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(speed, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num2 == 4 && shieldsSpawned < 5 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(shield, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num2 == 5 && smallsSpawned < 1 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(small, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					smallsSpawned++;
				}
				else if (num2 == 6 && purgesSpawned < 3 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(purge, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					mediumScoreGoal = UnityEngine.Random.Range(mediumPowerupScoreMin, mediumPowerupScoreMax);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					purgesSpawned++;
				}
			}
		}
		if (PlayerPrefs.GetString("diff") == "Hard")
		{
			if (player.health < hardHealthMin)
			{
				if (UnityEngine.Random.Range(1, 1001) <= hardHealthPackChance && !tried && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null && !healing)
				{
					UnityEngine.Object.Instantiate(position: new Vector3(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f), 0f), original: healthPack, rotation: Quaternion.identity);
				}
				else if (player.health < hardMinPassiveHealth && UnityEngine.Random.Range(1, 201) <= 5)
				{
					healing = true;
				}
				tried = true;
			}
			if (score >= hardScoreGoal)
			{
				int num3 = UnityEngine.Random.Range(1, 7);
				if (num3 == 1 && plusLivesSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(plusOneLife, new Vector2(UnityEngine.Random.Range(0f - width, width), UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax + 1);
					hardScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(hardScoreGoal / 2), (int)MathF.Round(hardScoreGoal * 2));
					hardScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					plusLivesSpawned++;
				}
				else if (num3 == 2 && starsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(star, new Vector2(UnityEngine.Random.Range(0f - width, width), UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax + 1);
					hardScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(hardScoreGoal / 2), (int)MathF.Round(hardScoreGoal * 2));
					hardScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					starsSpawned++;
				}
				else if (num3 == 3 && speedsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(speed, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax);
					hardScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(hardScoreGoal / 2), (int)MathF.Round(hardScoreGoal * 2));
					hardScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num3 == 4 && shieldsSpawned < 5 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(shield, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax);
					hardScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(hardScoreGoal / 2), (int)MathF.Round(hardScoreGoal * 2));
					hardScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num3 == 5 && smallsSpawned < 1 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(small, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax);
					hardScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(hardScoreGoal / 2), (int)MathF.Round(hardScoreGoal * 2));
					hardScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					smallsSpawned++;
				}
				else if (num3 == 6 && purgesSpawned < 3 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(purge, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					hardScoreGoal = UnityEngine.Random.Range(hardPowerupScoreMin, hardPowerupScoreMax);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					purgesSpawned++;
				}
			}
		}
		if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			if (player.health < unfairHealthMin)
			{
				if (UnityEngine.Random.Range(1, 1001) <= unfairHealthPackChance && !tried && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null && !healing)
				{
					UnityEngine.Object.Instantiate(position: new Vector3(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f), 0f), original: healthPack, rotation: Quaternion.identity);
				}
				else if (player.health < unfairMinPassiveHealth && UnityEngine.Random.Range(1, 201) <= 5)
				{
					healing = true;
				}
				tried = true;
			}
			if (score >= unfairScoreGoal)
			{
				int num3 = UnityEngine.Random.Range(1, 7);
				if (num3 == 1 && plusLivesSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(plusOneLife, new Vector2(UnityEngine.Random.Range(0f - width, width), UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax + 1);
					unfairScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(unfairScoreGoal / 2), (int)MathF.Round(unfairScoreGoal * 2));
					unfairScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					plusLivesSpawned++;
				}
				else if (num3 == 2 && starsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(star, new Vector2(UnityEngine.Random.Range(0f - width, width), UnityEngine.Random.Range(0f - height, height)), Quaternion.identity);
					unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax + 1);
					unfairScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(unfairScoreGoal / 2), (int)MathF.Round(unfairScoreGoal * 2));
					unfairScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					starsSpawned++;
				}
				else if (num3 == 3 && speedsSpawned < 2 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(speed, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax);
					unfairScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(unfairScoreGoal / 2), (int)MathF.Round(unfairScoreGoal * 2));
					unfairScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num3 == 4 && shieldsSpawned < 5 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(shield, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax);
					unfairScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(unfairScoreGoal / 2), (int)MathF.Round(unfairScoreGoal * 2));
					unfairScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					speedsSpawned++;
				}
				else if (num3 == 5 && smallsSpawned < 1 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(small, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax);
					unfairScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(unfairScoreGoal / 2), (int)MathF.Round(unfairScoreGoal * 2));
					unfairScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					smallsSpawned++;
				}
				else if (num3 == 6 && purgesSpawned < 3 && UnityEngine.Object.FindFirstObjectByType<Purge>() == null && UnityEngine.Object.FindFirstObjectByType<SmallPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<Shield>() == null && UnityEngine.Object.FindFirstObjectByType<LifePowerup>() == null && UnityEngine.Object.FindFirstObjectByType<StarPowerup>() == null && UnityEngine.Object.FindFirstObjectByType<SpeedPowerup>() == null)
				{
					UnityEngine.Object.Instantiate(purge, new Vector2(UnityEngine.Random.Range(0f - width + 1f, width - 1f), UnityEngine.Random.Range(0f - height + 1f, height - 1f)), Quaternion.identity);
					unfairScoreGoal = UnityEngine.Random.Range(unfairPowerupScoreMin, unfairPowerupScoreMax);
					mediumScoreGoal = score + UnityEngine.Random.Range((int)MathF.Round(mediumScoreGoal / 2), (int)MathF.Round(mediumScoreGoal * 2));
					mediumScoreGoal += UnityEngine.Random.Range((int)Mathf.Round(score / 4), (int)Mathf.Round(score / 4));
					purgesSpawned++;
				}
			}
		}
		if (tried && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null)
		{
			cooldown += Time.deltaTime;
			if (cooldown >= wait)
			{
				if (UnityEngine.Random.Range(1, 4) == 2)
				{
					tried = false;
				}
				cooldown = 0f;
				wait = UnityEngine.Random.Range(minWait, maxWait + 1f);
			}
		}
		if (healing && canHeal)
		{
			if (healWait == 0f)
			{
				healWait = UnityEngine.Random.Range(minHealWait, maxHealWait + 1f);
				healCooldown = 0f;
			}
			else
			{
				healCooldown += Time.deltaTime;
				if (healCooldown >= healWait)
				{
					healWait = 0f;
					healCooldown = 0f;
					if (PlayerPrefs.GetString("diff") == "Easy" && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null)
					{
						int num4 = UnityEngine.Random.Range(minPassiveHealAmount, maxPassiveHealAmount + 1);
						player.health += num4;
						UnityEngine.Object.Instantiate(player.floatText.GetComponent<FloatText>(), base.transform.position, Quaternion.identity).Spawn("+" + num4, Color.green, null, null, 2f, 4f);
						if (player.health >= easyPassiveHealMax)
						{
							player.health = easyPassiveHealMax;
							healing = false;
						}
					}
					if (PlayerPrefs.GetString("diff") == "Medium" && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null)
					{
						int num5 = UnityEngine.Random.Range(minPassiveHealAmount, maxPassiveHealAmount + 1);
						player.health += num5;
						UnityEngine.Object.Instantiate(player.floatText.GetComponent<FloatText>(), base.transform.position, Quaternion.identity).Spawn("+" + num5, Color.green, null, null, 2f, 4f);
						if (player.health >= mediumPassiveHealMax)
						{
							player.health = mediumPassiveHealMax;
							healing = false;
						}
					}
					if (PlayerPrefs.GetString("diff") == "Hard" && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null)
					{
						int num6 = UnityEngine.Random.Range(minPassiveHealAmount, maxPassiveHealAmount + 1);
						player.health += num6;
						UnityEngine.Object.Instantiate(player.floatText.GetComponent<FloatText>(), base.transform.position, Quaternion.identity).Spawn("+" + num6, Color.green, null, null, 2f, 4f);
						if (player.health >= hardPassiveHealMax)
						{
							player.health = hardPassiveHealMax;
							healing = false;
						}
					}
					if (PlayerPrefs.GetString("diff") == "Unfair" && UnityEngine.Object.FindFirstObjectByType<HealthPack>() == null)
					{
						int num6 = UnityEngine.Random.Range(minPassiveHealAmount, maxPassiveHealAmount + 1);
						player.health += num6;
						UnityEngine.Object.Instantiate(player.floatText.GetComponent<FloatText>(), base.transform.position, Quaternion.identity).Spawn("+" + num6, Color.green, null, null, 2f, 4f);
						if (player.health >= unfairPassiveHealMax)
						{
							player.health = unfairPassiveHealMax;
							healing = false;
						}
					}
				}
			}
		}
		if (!canHeal)
		{
			if (canHealCooldownTime == 0f)
			{
				canHealCooldownTime = UnityEngine.Random.Range(5f, 11f);
			}
			canHealCooldown += Time.deltaTime;
			if (canHealCooldown >= canHealCooldownTime)
			{
				canHealCooldownTime = 0f;
				canHealCooldown = 0f;
				canHeal = true;
			}
		}
	}
}
