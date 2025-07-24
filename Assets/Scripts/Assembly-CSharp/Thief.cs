using UnityEngine;

public class Thief : MonoBehaviour
{
	private float speed;

	public float speedAdd = 2f;

	public Player player;

	private GameManager manager;

	public int easyMinDamage = 10;

	public int easyMaxDamage = 15;

	public int mediumMinDamage = 20;

	public int mediumMaxDamage = 25;

	public int hardMinDamage = 30;

	public int hardMaxDamage = 35;
	public int unfairMinDamage = 30;

	public int unfairMaxDamage = 35;

	public int minScoreErase;

	public int maxScoreErase;

	public GameObject floatText;

	public float timer = 30f;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
		manager = player.gameObject.GetComponent<GameManager>();
		speed = Object.FindFirstObjectByType<MiniSquare>().speed + speedAdd;
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void FixedUpdate()
	{
		if (player.health > 0 && !manager.isCountdown)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, player.gameObject.transform.position, speed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			int num = Random.Range(minScoreErase, maxScoreErase + 1);
			manager.score -= num;
			if (manager.score < 0)
			{
				manager.score = 0;
			}
			Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("-" + num + " Score", new Color(0.4745098f, 0f, 1f), null, null, 2f, 4f);
			Object.Destroy(base.gameObject);
		}
	}
}
