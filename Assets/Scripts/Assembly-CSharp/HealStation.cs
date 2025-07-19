using UnityEngine;

public class HealStation : MonoBehaviour
{
	private bool inZone;

	private float tick;

	private Player player;

	public float tickTime = 0.5f;

	public int healAmount = 1;

	public Vector2 position;

	public float entrySpeed = 3f;

	public float minStayTime = 5f;

	public float maxStayTime = 15f;

	private float stayTimer;

	private bool healing = true;

	public Vector2 endPos;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
		stayTimer = Random.Range(minStayTime, maxStayTime);
	}

	private void Update()
	{
		stayTimer -= Time.deltaTime;
		if (stayTimer <= 0f)
		{
			healing = false;
		}
		if (healing && !Object.FindFirstObjectByType<PauseMenu>().paused)
		{
			base.transform.position = Vector2.Lerp(base.transform.position, position, entrySpeed * Time.deltaTime);
			if (inZone)
			{
				MonoBehaviour.print("yes");
				tick += Time.deltaTime;
				if (tick >= tickTime)
				{
					tick = 0f;
					player.health += healAmount;
				}
			}
			else
			{
				tick = 0f;
			}
		}
		else
		{
			base.transform.position = Vector2.Lerp(base.transform.position, endPos, entrySpeed * Time.deltaTime);
			if (Mathf.Round(base.transform.position.y) == endPos.y)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			inZone = true;
			tick = tickTime;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			inZone = false;
		}
	}
}
