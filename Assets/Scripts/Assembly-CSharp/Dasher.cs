using UnityEngine;

public class Dasher : MonoBehaviour
{
	private Rigidbody2D rb;

	private Player player;

	public GameObject bloodSplash;

	public float speed = 10f;

	public int easyMinDamage = 15;

	public int easyMaxDamage = 20;

	public int mediumMinDamage = 20;

	public int mediumMaxDamage = 35;

	public int hardMinDamage = 35;

	public int hardMaxDamage = 45;

	private Vector2 randomPos;

	private Vector2 dasherPos;
    public int unfairMinDamage = 45;
    public int unfairMaxDamage = 60;

    private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
		rb = GetComponent<Rigidbody2D>();
		switch (Random.Range(1, 5))
		{
		case 1:
			randomPos = new Vector2(Random.Range(0f - player.width, player.width), player.height + 1f);
			break;
		case 2:
			randomPos = new Vector2(Random.Range(0f - player.width, player.width), 0f - player.height - 1f);
			break;
		case 3:
			randomPos = new Vector2(0f - player.width - 1f, Random.Range(0f - player.height, player.height));
			break;
		case 4:
			randomPos = new Vector2(player.width + 1f, Random.Range(0f - player.height, player.height));
			break;
		}
		dasherPos = new Vector2(base.gameObject.transform.position.x, base.gameObject.transform.position.y);
		Vector2 vector = randomPos - dasherPos;
		vector.Normalize();
		rb.velocity = vector * speed;
	}

	private void Update()
	{
		if (base.transform.position.x < 0f - player.width - 5f || base.transform.position.x > player.width + 5f || base.transform.position.y < 0f - player.height - 5f || base.transform.position.y > player.height + 5f)
		{
			Object.Destroy(base.gameObject);
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
			if (PlayerPrefs.GetString("diff") == "Unfair")
			{
				player.health -= Random.Range(unfairMinDamage, unfairMaxDamage);
			}
			Object.Instantiate(bloodSplash, base.transform.position, Quaternion.identity);
			Object.FindFirstObjectByType<ScreenShake>().start = true;
			Object.FindFirstObjectByType<AudioManager>().Play("hit");
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
