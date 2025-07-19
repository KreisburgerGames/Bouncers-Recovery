using UnityEngine;

public class LifePowerup : MonoBehaviour
{
	private Player player;

	public ParticleSystem plusLife;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>().GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && player.lives < 3)
		{
			player.lives++;
			Object.FindFirstObjectByType<AudioManager>().Play("powerup");
			Object.Instantiate(plusLife, base.transform.position, Quaternion.identity);
			if (player.lives == 3)
			{
				Object.FindFirstObjectByType<AchivementManager>().GiveAchivement("BulkedUp");
			}
			Object.Destroy(base.gameObject);
			player.manager.plusLifeCollected = true;
		}
	}
}
