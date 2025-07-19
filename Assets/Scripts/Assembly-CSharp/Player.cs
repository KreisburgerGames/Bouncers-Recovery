using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float speed = 25f;

	[HideInInspector]
	public float dirx;

	[HideInInspector]
	public float diry;

	public int health = 100;

	public GameManager manager;

	public Camera camera;

	private ScreenShake screenShake;

	public int easyDamageMin = 5;

	public int easyDamageMax = 15;

	public int mediumDamageMin = 10;

	public int mediumDamageMax = 25;

	public int hardDamageMin = 20;

	public int hardDamageMax = 35;

	public int maxHealth = 100;

	private bool mouseControls;

	public float width;

	public float height;

	private float padding = 0.5f;

	private bool pushingx;

	private bool pushingy;

	public ParticleSystem bloodSplash;

	public ParticleSystem EasySpawn;

	public ParticleSystem MediumSpawn;

	public ParticleSystem HardSpawn;

	public bool canMove = true;

	public float friction = 1.5f;

	public float force = 20f;

	private bool right;

	private bool up;

	private Vector2 mousePos;

	public static string scene;

	public int easyHealMin = 15;

	public int easyHealMax = 25;

	public int mediumHealMin = 10;

	public int mediumHealMax = 20;

	public int hardHealMin = 5;

	public int hardHealMax = 20;

	private bool wall;

	public GameObject floatText;

	private PostProcessVolume vol;

	public Sprite defaultNoShield;

	public Sprite sheild;

	[HideInInspector]
	public ChromaticAberration chromatic;

	[HideInInspector]
	public Grain grain;

	public int lives = 1;

	[HideInInspector]
	public Rigidbody2D rb;

	[HideInInspector]
	public bool hasSheild;

	public GameObject shieldLoss;

	[Header("Skins")]
	public Sprite defaultSprite;

	public Material defaultMaterial;

	public Material defaultTrailMaterial;

	public Sprite defaultShieldTexture;

	public Sprite WTCSprite;

	public Material WTCMaterial;

	public Material WTCTrailMaterial;

	public Sprite WTCShieldTexture;

	public Sprite CoolSprite;

	public Material CoolMaterial;

	public Material CoolTrailMaterial;

	public Sprite CoolShieldTexture;

	public Sprite VOIDSprite;

	public Material VOIDMaterial;

	public Material VOIDTrailMaterial;

	public Sprite VOIDShieldTexture;

	public Sprite AQUASprite;

	public Material AQUAMaterial;

	public Material AQUATrailMaterial;

	public Sprite AQUAShieldTexture;

	public Sprite JoshSprite;

	public Material JoshMaterial;

	public Material JoshTrailMaterial;

	public Sprite JoshShieldTexture;

	public Sprite SISprite;

	public Material SIMaterial;

	public Material SITrailMaterial;

	public Sprite SIShieldTexture;

	public void ShieldBreak()
	{
		if (PlayerPrefs.GetString("Skin") == "Default")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
		}
		else if (PlayerPrefs.GetString("Skin") == "WTC")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = WTCSprite;
		}
		else if (PlayerPrefs.GetString("Skin") == "Cool")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = CoolSprite;
		}
		else if (PlayerPrefs.GetString("Skin") == "VOID")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = VOIDSprite;
		}
		else if (PlayerPrefs.GetString("Skin") == "Aqua")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = AQUASprite;
		}
		else if (PlayerPrefs.GetString("Skin") == "Josh")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = JoshSprite;
		}
		else if (PlayerPrefs.GetString("Skin") == "SkillIssue")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = SISprite;
		}
	}

	private void Awake()
	{
		if (PlayerPrefs.GetInt("mouseControls") == 1)
		{
			mouseControls = true;
		}
		else
		{
			mouseControls = false;
			Cursor.visible = false;
		}
		if (PlayerPrefs.GetString("Skin") == "Default")
		{
			GetComponent<SpriteRenderer>().sprite = defaultSprite;
			GetComponent<SpriteRenderer>().material = defaultMaterial;
			GetComponent<TrailRenderer>().material = defaultTrailMaterial;
		}
		else if (PlayerPrefs.GetString("Skin") == "WTC")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = WTCSprite;
			GetComponent<SpriteRenderer>().material = WTCMaterial;
			GetComponent<TrailRenderer>().material = WTCTrailMaterial;
		}
		else if (PlayerPrefs.GetString("Skin") == "Cool")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = CoolSprite;
			GetComponent<SpriteRenderer>().material = CoolMaterial;
			GetComponent<TrailRenderer>().material = CoolTrailMaterial;
		}
		else if (PlayerPrefs.GetString("Skin") == "VOID")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = VOIDSprite;
			GetComponent<SpriteRenderer>().material = VOIDMaterial;
			GetComponent<TrailRenderer>().material = VOIDTrailMaterial;
		}
		else if (PlayerPrefs.GetString("Skin") == "Aqua")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = AQUASprite;
			GetComponent<SpriteRenderer>().material = AQUAMaterial;
			GetComponent<TrailRenderer>().material = AQUATrailMaterial;
		}
		else if (PlayerPrefs.GetString("Skin") == "Josh")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = JoshSprite;
			GetComponent<SpriteRenderer>().material = JoshMaterial;
			GetComponent<TrailRenderer>().material = JoshTrailMaterial;
		}
		else if (PlayerPrefs.GetString("Skin") == "SkillIssue")
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = SISprite;
			GetComponent<SpriteRenderer>().material = SIMaterial;
			GetComponent<TrailRenderer>().material = SITrailMaterial;
		}
		pushingx = false;
		pushingy = false;
		canMove = false;
		lives = 1;
		vol = GetComponent<PostProcessVolume>();
		vol.profile.TryGetSettings<ChromaticAberration>(out chromatic);
		chromatic.intensity.Override(0f);
		vol.profile.TryGetSettings<Grain>(out grain);
		grain.intensity.Override(0f);
		scene = SceneManager.GetActiveScene().name;
		screenShake = camera.GetComponent<ScreenShake>();
		if (SceneManager.GetActiveScene().name == "Game")
		{
			if (PlayerPrefs.GetString("diff") == "Easy")
			{
				Object.Instantiate(EasySpawn, base.transform.position, Quaternion.identity);
			}
			else if (PlayerPrefs.GetString("diff") == "Medium")
			{
				Object.Instantiate(MediumSpawn, base.transform.position, Quaternion.identity);
			}
			else if (PlayerPrefs.GetString("diff") == "Hard")
			{
				Object.Instantiate(HardSpawn, base.transform.position, Quaternion.identity);
			}
		}
	}

	private void Start()
	{
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		manager.score = 0;
		width = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0f)).x;
		height = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0f)).y;
	}

	private void Update()
	{
		mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
		if (health > maxHealth)
		{
			health = maxHealth;
		}
		if (!mouseControls && !pushingx && !pushingy && canMove)
		{
			dirx = Input.GetAxisRaw("Horizontal");
			diry = Input.GetAxisRaw("Vertical");
		}
		if (canMove)
		{
			base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, 0f - width + padding, width - padding), Mathf.Clamp(base.transform.position.y, 0f - height + padding, height - padding), base.transform.position.z);
		}
		if (base.transform.position.x >= width - padding && !wall && canMove && !manager.falling)
		{
			if (!hasSheild)
			{
				int num = Random.Range(3, 8);
				health -= num;
				wall = true;
				rb.velocity = new Vector2(0f - force, rb.velocity.y);
				pushingx = true;
				manager.multBounce = manager.score;
				right = true;
				screenShake.start = true;
				Object.FindFirstObjectByType<AudioManager>().Play("wall");
				Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
				Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num, Color.red, "false", null, 2f, 4f);
			}
			else
			{
				hasSheild = false;
				ShieldBreak();
				Object.Instantiate(shieldLoss, base.transform.position, Quaternion.identity);
			}
		}
		if (base.transform.position.x <= 0f - width + padding && !wall && canMove && !manager.falling)
		{
			if (!hasSheild)
			{
				int num = Random.Range(3, 8);
				health -= num;
				rb.velocity = new Vector2(force, rb.velocity.y);
				pushingx = true;
				wall = true;
				right = false;
				screenShake.start = true;
				manager.multBounce = manager.score;
				Object.FindFirstObjectByType<AudioManager>().Play("wall");
				Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
				Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num, Color.red, "true", null, 2f, 4f);
			}
			else
			{
				hasSheild = false;
				ShieldBreak();
				Object.Instantiate(shieldLoss, base.transform.position, Quaternion.identity);
			}
		}
		if (base.transform.position.y >= height - padding && !wall && canMove && !manager.falling)
		{
			if (!hasSheild)
			{
				int num = Random.Range(3, 8);
				health -= num;
				rb.velocity = new Vector2(rb.velocity.x, 0f - force);
				pushingy = true;
				wall = true;
				up = true;
				screenShake.start = true;
				manager.multBounce = manager.score;
				Object.FindFirstObjectByType<AudioManager>().Play("wall");
				Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
				Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num, Color.red, "false", null, 2f, 4f);
			}
			else
			{
				hasSheild = false;
				ShieldBreak();
				Object.Instantiate(shieldLoss, base.transform.position, Quaternion.identity);
			}
		}
		if (base.transform.position.y <= 0f - height + padding && !wall && canMove && !manager.falling)
		{
			if (!hasSheild)
			{
				int num = Random.Range(3, 7);
				health -= num;
				wall = true;
				rb.velocity = new Vector2(rb.velocity.x, force);
				pushingy = true;
				up = false;
				manager.multBounce = manager.score;
				screenShake.start = true;
				Object.FindFirstObjectByType<AudioManager>().Play("wall");
				Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
				Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num, Color.red, "true", null, 2f, 4f);
			}
			else
			{
				hasSheild = false;
				ShieldBreak();
				Object.Instantiate(shieldLoss, base.transform.position, Quaternion.identity);
			}
		}
	}

	public void FixedUpdate()
	{
		if (!pushingx && !pushingy && !mouseControls && canMove)
		{
			rb.velocity = new Vector2(dirx * speed * 0.7f, diry * speed * 0.7f);
		}
		else if (!pushingx && !pushingy && canMove)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, mousePos, speed * Time.deltaTime);
		}
		else if (pushingx)
		{
			if (right)
			{
				if (rb.velocity.x >= 0f)
				{
					pushingx = false;
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x + friction, rb.velocity.y);
				}
			}
			else if (rb.velocity.x <= 0f)
			{
				pushingx = false;
			}
			else
			{
				rb.velocity = new Vector2(rb.velocity.x - friction, rb.velocity.y);
			}
		}
		else if (pushingy)
		{
			if (up)
			{
				if (rb.velocity.y >= 0f)
				{
					pushingy = false;
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + friction);
				}
			}
			else if (rb.velocity.y <= 0f)
			{
				pushingy = false;
			}
			else
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - friction);
			}
		}
		if (!pushingx && !pushingy)
		{
			wall = false;
		}
	}
}
