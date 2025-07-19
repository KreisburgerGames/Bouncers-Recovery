using UnityEngine;

public class Asteroid : MonoBehaviour
{
	private Player player;

	private GameManager manager;

	public ParticleSystem bloodSplash;

	public int easyMinDamage = 15;

	public int easyMaxDamage = 20;

	public int mediumMinDamage = 20;

	public int mediumMaxDamage = 35;

	public int hardMinDamage = 35;

	public int hardMaxDamage = 45;

	private Rigidbody2D rb;

	public float speed;

	private Vector2 playerPos;

	private Vector2 asteroidPos;

	public GameObject warning;

	public float countodwnTime = 1f;

	private float countdown;

	public float lockDistance = 2.5f;

	private bool locked;

	private Vector2 directionVector;

	private bool hit;

	public float speedAdd = 3f;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
		manager = player.gameObject.GetComponent<GameManager>();
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		speed = player.speed + speedAdd;
	}

	public void Spawn()
	{
		countdown = countodwnTime;
		playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
		asteroidPos = new Vector2(base.gameObject.transform.position.x, base.gameObject.transform.position.y);
		GameObject gameObject = Object.Instantiate(warning, base.transform.position, Quaternion.identity);
		if (gameObject.transform.position.x < 0f - player.width)
		{
			gameObject.transform.position = new Vector2(0f - player.width + 1f, base.transform.position.y);
		}
		if (gameObject.transform.position.x > player.width)
		{
			gameObject.transform.position = new Vector2(player.width - 1f, base.transform.position.y);
		}
		if (gameObject.transform.position.y < 0f - player.height)
		{
			gameObject.transform.position = new Vector2(base.transform.position.x, 0f - player.height + 1f);
		}
		if (gameObject.transform.position.y > player.height)
		{
			gameObject.transform.position = new Vector2(base.transform.position.x, player.height - 1f);
		}
	}

	private void Update()
	{
		if (countdown <= 0f)
		{
			float f = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - base.transform.position.x, 2f) + Mathf.Pow(player.transform.position.y - base.transform.position.y, 2f));
			if (!locked)
			{
				playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
				asteroidPos = new Vector2(base.gameObject.transform.position.x, base.gameObject.transform.position.y);
				directionVector = playerPos - asteroidPos;
				rb.velocity = directionVector.normalized * speed;
			}
			if (Mathf.Abs(f) <= lockDistance && !locked)
			{
				locked = true;
			}
			if (base.transform.position.y < 0f - player.height - 10f)
			{
				Object.Destroy(base.gameObject);
				if (!hit)
				{
					Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("Juked");
				}
			}
			if (base.transform.position.y > player.height + 10f)
			{
				Object.Destroy(base.gameObject);
			}
			if (base.transform.position.x < 0f - player.width - 10f)
			{
				Object.Destroy(base.gameObject);
			}
			if (base.transform.position.x > player.width + 10f)
			{
				Object.Destroy(base.gameObject);
			}
		}
		else
		{
			countdown -= Time.deltaTime;
			if (countdown <= 0f)
			{
				playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
				Object.Destroy(GameObject.FindGameObjectWithTag("Warning"));
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!(collision.gameObject.tag == "Player"))
		{
			return;
		}
		if (!collision.gameObject.GetComponent<Player>().hasSheild)
		{
			if (PlayerPrefs.GetString("diff") == "Easy")
			{
				player.health -= Random.Range(easyMinDamage, easyMaxDamage);
			}
			if (PlayerPrefs.GetString("diff") == "Medium")
			{
				player.health -= Random.Range(mediumMinDamage, mediumMaxDamage);
			}
			if (PlayerPrefs.GetString("diff") == "Hard")
			{
				player.health -= Random.Range(hardMinDamage, hardMaxDamage);
			}
			Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
			Object.FindFirstObjectByType<ScreenShake>().start = true;
			hit = true;
		}
		else
		{
			player.ShieldBreak();
			player.hasSheild = false;
			Object.FindFirstObjectByType<AudioManager>().Play("break");
			Object.Instantiate(player.shieldLoss, base.transform.position, Quaternion.identity);
		}
	}
}
