using UnityEngine;

public class HealthPack : MonoBehaviour
{
	private Player player;

	public ParticleSystem heal;

	private GameManager manager;

	public GameObject floatText;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>().GetComponent<Player>();
		manager = player.gameObject.GetComponent<GameManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			int num;
			if (PlayerPrefs.GetString("diff") == "Easy")
			{
				num = Random.Range(player.easyHealMin, player.easyHealMax + 1);
				player.health += num;
				Object.Instantiate(heal, base.transform.position, Quaternion.identity);
				manager.tried = false;
				Object.Destroy(base.gameObject);
			}
			else if (PlayerPrefs.GetString("diff") == "Medium")
			{
				num = Random.Range(player.mediumHealMin, player.mediumHealMax + 1);
				player.health += num;
				Object.Instantiate(heal, base.transform.position, Quaternion.identity);
				manager.tried = false;
				Object.Destroy(base.gameObject);
			}
			else if (PlayerPrefs.GetString("diff") == "Hard")
			{
				num = Random.Range(player.hardHealMin, player.hardHealMax + 1);
				player.health += num;
				Object.Instantiate(heal, base.transform.position, Quaternion.identity);
				manager.tried = false;
				Object.Destroy(base.gameObject);
			}
			else
			{
				num = 0;
			}
			Object.Instantiate(floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("+" + num, Color.green, "true", null, 2f, 4f);
			Object.FindFirstObjectByType<AudioManager>().GetComponent<AudioManager>().Play("health");
		}
	}
}
