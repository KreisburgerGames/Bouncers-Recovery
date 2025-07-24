using System;
using UnityEngine;

public class MiniSquare : MonoBehaviour
{
	public float speed = 5f;

	public Player player;

	public Camera camera;

	private ScreenShake shake;

	private GameManager manager;

	private Rigidbody2D rb;

	public int easyMinDamage = 10;

	public int easyMaxDamage = 15;

	public int mediumMinDamage = 20;

	public int mediumMaxDamage = 25;

	public int hardMinDamage = 30;

	public int hardMaxDamage = 35;

	public ParticleSystem spawn;

	public ParticleSystem destroy;

	public ParticleSystem bloodSplash;

	public GameObject floatText;

	public float cooldownTime = 1f;

	private int date = DateTime.Now.Day;
    public int unfairMinDamage;
    public int unfairMaxDamage;

    private void Start()
	{
		manager = player.gameObject.GetComponent<GameManager>();
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		shake = camera.gameObject.GetComponent<ScreenShake>();
	}

	private void Awake()
	{
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			speed = 5f;
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			speed = 7f;
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			speed = 8f;
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			speed = 9f;
		}
	}

	private void FixedUpdate()
	{
		if (player.health > 0 && !manager.isCountdown)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, player.gameObject.transform.position, speed * Time.deltaTime);
		}
	}

	private void CooldownSpawn()
	{
		new WaitForSeconds(cooldownTime);
		base.transform.position = new Vector2(0f, 0f);
		UnityEngine.Object.Instantiate(spawn, base.transform.position, Quaternion.identity);
		GetComponent<TrailRenderer>().enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!(collision.gameObject.tag == "Player") || !player.canMove)
		{
			return;
		}
		if (!player.hasSheild)
		{
			int num;
			if (PlayerPrefs.GetString("diff") == "Easy")
			{
				num = UnityEngine.Random.Range(easyMinDamage, easyMaxDamage + 1);
				player.health -= num;
			}
			else if (PlayerPrefs.GetString("diff") == "Medium")
			{
				num = UnityEngine.Random.Range(mediumMinDamage, mediumMaxDamage + 1);
				player.health -= num;
			}
			else if (PlayerPrefs.GetString("diff") == "Hard")
			{
				num = UnityEngine.Random.Range(hardMinDamage, hardMaxDamage + 1);
				player.health -= num;
			}
			else if (PlayerPrefs.GetString("diff") == "Unfair")
			{
				num = UnityEngine.Random.Range(unfairMinDamage, unfairMaxDamage + 1);
				player.health -= num;
			}
			else
			{
				num = 0;
			}
			shake.start = true;
			UnityEngine.Object.Instantiate(destroy, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num, Color.red, null, null, 2f, 4f);
			manager.multBounce = manager.score;
			if (player.health > 0)
			{
				UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("hit");
			}
			GetComponent<TrailRenderer>().enabled = false;
			base.transform.position = new Vector2(200f, 200f);
		}
		else
		{
			player.hasSheild = false;
			player.ShieldBreak();
			UnityEngine.Object.Instantiate(player.shieldLoss, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Instantiate(destroy, base.transform.position, Quaternion.identity);
			GetComponent<TrailRenderer>().enabled = false;
			base.transform.position = new Vector2(200f, 200f);
			UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("break");
		}
		Invoke("CooldownSpawn", cooldownTime);
	}
}
