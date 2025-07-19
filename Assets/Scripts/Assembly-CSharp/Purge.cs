using UnityEngine;

public class Purge : MonoBehaviour
{
	private Player player;

	private GameManager manager;

	public GameObject bouncer;

	public ParticleSystem bouncerDeath;

	private float width;

	private float height;

	public float spawnPadding = 1f;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
		manager = player.GetComponent<GameManager>();
		width = player.camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0f)).x;
		height = player.camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0f)).y;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Bouncer[] array = Object.FindObjectsOfType<Bouncer>();
			foreach (Bouncer bouncer in array)
			{
				Object.Destroy(bouncer.gameObject);
				manager.score += 10;
				Object.Instantiate(bouncerDeath, bouncer.gameObject.transform.position, Quaternion.identity);
			}
			for (int j = 0; j < 3; j++)
			{
				Object.Instantiate(position: new Vector3(Random.Range((width - spawnPadding) * -1f, width - spawnPadding), Random.Range((height - spawnPadding) * -1f, height - spawnPadding), base.transform.position.z), original: this.bouncer, rotation: Quaternion.identity);
			}
			player.GetComponentInChildren<SlowDown>().slowDown = false;
			player.manager.purgeCollected = true;
			Object.FindFirstObjectByType<AudioManager>().Play("powerup");
			Object.Destroy(base.gameObject);
		}
	}
}
