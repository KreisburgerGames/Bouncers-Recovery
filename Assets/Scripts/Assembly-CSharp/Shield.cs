using UnityEngine;

public class Shield : MonoBehaviour
{
	private Player player;

	public GameObject shieldCollect;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			player.hasSheild = true;
			if (PlayerPrefs.GetString("Skin") == "Default")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.defaultShieldTexture;
			}
			if (PlayerPrefs.GetString("Skin") == "WTC")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.WTCShieldTexture;
			}
			if (PlayerPrefs.GetString("Skin") == "Cool")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.CoolShieldTexture;
			}
			if (PlayerPrefs.GetString("Skin") == "VOID")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.VOIDShieldTexture;
			}
			if (PlayerPrefs.GetString("Skin") == "Aqua")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.AQUAShieldTexture;
			}
			if (PlayerPrefs.GetString("Skin") == "Josh")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.JoshShieldTexture;
			}
			if (PlayerPrefs.GetString("Skin") == "SkillIssue")
			{
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.SIShieldTexture;
			}
			player.manager.shieldCollected = true;
			Object.FindFirstObjectByType<AudioManager>().Play("powerup");
			Object.Instantiate(shieldCollect, base.transform.position, Quaternion.identity);
			Object.Destroy(base.gameObject);
		}
	}
}
