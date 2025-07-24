using System;
using UnityEngine;

public class StarPowerup : MonoBehaviour
{
	private Player player;

	private GameManager manager;

	public ParticleSystem starPowerup;

	public MiniSquare miniSquare;

	private RectTransform redHealth;

	public ParticleSystem bouncerDeath;

	private void Awake()
	{
		player = UnityEngine.Object.FindFirstObjectByType<Player>();
		manager = player.GetComponent<GameManager>();
		miniSquare = UnityEngine.Object.FindFirstObjectByType<MiniSquare>().GetComponent<MiniSquare>();
		redHealth = GameObject.Find("Main Camera/UI/HUD/HealthRed").GetComponent<RectTransform>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!(collision.gameObject.tag == "Player"))
		{
			return;
		}
		int num = (int)MathF.Round(manager.bouncers / 3);
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = UnityEngine.Object.FindAnyObjectByType<Bouncer>().gameObject;
			UnityEngine.Object.Instantiate(bouncerDeath, gameObject.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(gameObject);
			manager.bouncers--;
		}
		if (PlayerPrefs.GetString("diff") == "Easy")
		{
			player.easyHealMin += UnityEngine.Random.Range(5, 11);
			player.easyHealMax += UnityEngine.Random.Range(10, 16);
			player.easyDamageMin -= UnityEngine.Random.Range(10, 16);
			player.easyDamageMax -= UnityEngine.Random.Range(5, 11);
			miniSquare.easyMinDamage -= UnityEngine.Random.Range(10, 16);
			miniSquare.easyMaxDamage -= UnityEngine.Random.Range(5, 11);
			if (player.easyDamageMin < 5)
			{
				player.easyDamageMin = 5;
				player.easyDamageMax = 10;
			}
			if (miniSquare.easyMinDamage < 10)
			{
				miniSquare.easyMinDamage = 10;
				miniSquare.easyMaxDamage = 20;
			}
			if (player.easyHealMin > 30)
			{
				player.easyHealMin = 30;
				player.easyHealMax = 50;
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Medium")
		{
			player.mediumHealMin += UnityEngine.Random.Range(5, 11);
			player.mediumHealMax += UnityEngine.Random.Range(10, 16);
			player.mediumDamageMin -= UnityEngine.Random.Range(10, 16);
			player.mediumDamageMax -= UnityEngine.Random.Range(5, 11);
			miniSquare.mediumMinDamage -= UnityEngine.Random.Range(10, 16);
			miniSquare.mediumMaxDamage -= UnityEngine.Random.Range(5, 11);
			if (player.mediumDamageMin < 5)
			{
				player.mediumDamageMin = 5;
				player.mediumDamageMax = 10;
			}
			if (miniSquare.mediumMinDamage < 10)
			{
				miniSquare.mediumMinDamage = 10;
				miniSquare.mediumMaxDamage = 20;
			}
			if (player.mediumHealMin > 30)
			{
				player.mediumHealMin = 30;
				player.mediumDamageMax = 50;
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Hard")
		{
			player.hardHealMin += UnityEngine.Random.Range(5, 11);
			player.hardHealMax += UnityEngine.Random.Range(10, 16);
			player.hardDamageMin -= UnityEngine.Random.Range(10, 16);
			player.hardDamageMax -= UnityEngine.Random.Range(5, 11);
			miniSquare.hardMinDamage -= UnityEngine.Random.Range(10, 16);
			miniSquare.hardMaxDamage -= UnityEngine.Random.Range(5, 11);
			if (player.hardDamageMin < 5)
			{
				player.hardDamageMin = 5;
				player.hardDamageMax = 10;
			}
			if (miniSquare.hardMinDamage < 10)
			{
				miniSquare.hardMinDamage = 10;
				miniSquare.hardMaxDamage = 20;
			}
			if (player.hardHealMin > 30)
			{
				player.hardHealMin = 30;
				player.hardHealMax = 50;
			}
		}
		else if (PlayerPrefs.GetString("diff") == "Unfair")
		{
			player.unfairHealMin += UnityEngine.Random.Range(5, 11);
			player.unfairHealMax += UnityEngine.Random.Range(10, 16);
			player.unfairDamageMin -= UnityEngine.Random.Range(10, 16);
			player.unfairDamageMax -= UnityEngine.Random.Range(5, 11);
			miniSquare.unfairMinDamage -= UnityEngine.Random.Range(10, 16);
			miniSquare.unfairMaxDamage -= UnityEngine.Random.Range(5, 11);
			if (player.unfairDamageMin < 5)
			{
				player.unfairDamageMin = 5;
				player.unfairDamageMax = 10;
			}
			if (miniSquare.unfairMinDamage < 10)
			{
				miniSquare.unfairMinDamage = 10;
				miniSquare.unfairMaxDamage = 20;
			}
			if (player.unfairHealMin > 30)
			{
				player.unfairHealMin = 30;
				player.unfairHealMax = 50;
			}
		}
		if (num != 0)
		{
			manager.scoreGoal = manager.score + (int)MathF.Round(manager.score / num);
		}
		int num2 = UnityEngine.Random.Range(10, 25);
		player.maxHealth += num2;
		Mathf.Clamp(player.maxHealth, 100, 165);
		redHealth.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(redHealth.gameObject.GetComponent<RectTransform>().localPosition.x - (float)(num2 * 3), redHealth.gameObject.GetComponent<RectTransform>().localPosition.y, redHealth.gameObject.GetComponent<RectTransform>().localPosition.z);
		RectTransform component = GameObject.Find("Main Camera/UI/HUD/HealthGreen").GetComponent<RectTransform>();
		component.localPosition = new Vector3(component.localPosition.x - (float)(num2 * 3), component.localPosition.y, component.localPosition.z);
		redHealth.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(player.maxHealth * 3, redHealth.GetComponent<RectTransform>().sizeDelta.y);
		player.health += num2;
		UnityEngine.Object.Instantiate(player.floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("+" + num2, Color.cyan, null, null, 2f, 4f);
		UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("powerup");
		UnityEngine.Object.Instantiate(starPowerup, base.transform.position, Quaternion.identity);
		player.manager.starCollected = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
