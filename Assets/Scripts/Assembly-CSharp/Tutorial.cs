using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public GameObject bouncer;

	public GameObject asteroid;

	public GameObject miniSquare;

	public GameObject previous;

	public GameObject next;

	public GameObject healthPacks;

	public GameObject lifePowerup;

	public GameObject starPowerup;

	public GameObject speedPowerup;

	public GameObject shieldPowerup;

	public GameObject player;

	public GameObject dasher;

	public GameObject small;

	public GameObject rainbowBouncer;

	public GameObject purge;

	public GameObject thief;

	public GameObject healthZone;

	private int page = 1;

	private bool musicStarted;

	private void Awake()
	{
		player.SetActive(value: true);
		bouncer.SetActive(value: false);
		asteroid.SetActive(value: false);
		miniSquare.SetActive(value: false);
		healthPacks.SetActive(value: false);
		lifePowerup.SetActive(value: false);
		starPowerup.SetActive(value: false);
		speedPowerup.SetActive(value: false);
		shieldPowerup.SetActive(value: false);
		previous.SetActive(value: false);
		next.SetActive(value: true);
		small.SetActive(value: false);
		rainbowBouncer.SetActive(value: false);
		thief.SetActive(value: false);
		purge.SetActive(value: false);
		healthZone.SetActive(value: false);
	}

	private void Update()
	{
		if (!musicStarted && Object.FindFirstObjectByType<AudioManager>() != null)
		{
			Object.FindFirstObjectByType<AudioManager>().Play("tutorial");
			musicStarted = true;
		}
		if (page == 1)
		{
			player.SetActive(value: true);
			bouncer.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			healthPacks.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			starPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		if (page == 2)
		{
			player.SetActive(value: false);
			bouncer.SetActive(value: true);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			healthPacks.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			starPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 3)
		{
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: true);
			miniSquare.SetActive(value: false);
			healthPacks.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 4)
		{
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			healthPacks.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			dasher.SetActive(value: true);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 5)
		{
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: true);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
			thief.SetActive(value: false);
		}
		else if (page == 6)
		{
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			thief.SetActive(value: true);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 7)
		{
			thief.SetActive(value: false);
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			healthPacks.SetActive(value: true);
			speedPowerup.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 8)
		{
			bouncer.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: true);
			player.SetActive(value: false);
			healthPacks.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 9)
		{
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: true);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 10)
		{
			bouncer.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			player.SetActive(value: false);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: true);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 11)
		{
			bouncer.SetActive(value: false);
			asteroid.SetActive(value: false);
			player.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: true);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 12)
		{
			bouncer.SetActive(value: false);
			asteroid.SetActive(value: false);
			player.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: true);
			purge.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 13)
		{
			bouncer.SetActive(value: false);
			player.SetActive(value: false);
			asteroid.SetActive(value: false);
			miniSquare.SetActive(value: false);
			purge.SetActive(value: true);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: false);
		}
		else if (page == 14)
		{
			bouncer.SetActive(value: false);
			asteroid.SetActive(value: false);
			player.SetActive(value: false);
			miniSquare.SetActive(value: false);
			lifePowerup.SetActive(value: false);
			healthPacks.SetActive(value: false);
			starPowerup.SetActive(value: false);
			speedPowerup.SetActive(value: false);
			shieldPowerup.SetActive(value: false);
			dasher.SetActive(value: false);
			purge.SetActive(value: false);
			small.SetActive(value: false);
			rainbowBouncer.SetActive(value: true);
			healthZone.SetActive(value: false);
		}
		else if (page == 15)
		{
			rainbowBouncer.SetActive(value: false);
			healthZone.SetActive(value: true);
		}
	}

	public void Next()
	{
		Object.FindFirstObjectByType<AudioManager>().Play("select");
		page++;
		if (page == 15)
		{
			next.SetActive(value: false);
		}
		previous.SetActive(value: true);
	}

	public void Previous()
	{
		Object.FindFirstObjectByType<AudioManager>().Play("select");
		page--;
		if (page == 1)
		{
			previous.SetActive(value: false);
		}
		next.SetActive(value: true);
	}
}
