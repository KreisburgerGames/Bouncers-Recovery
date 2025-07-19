using UnityEngine;

public class LivesCount : MonoBehaviour
{
	private Player player;

	[HideInInspector]
	public GameObject oneLife;

	public GameObject twoLives;

	public GameObject threeLives;

	public GameObject breakEffect;

	public GameObject canvas;

	private bool effectDone;

	private void Awake()
	{
		player = Object.FindAnyObjectByType<Player>().GetComponent<Player>();
		oneLife = GameObject.Find("1 Life");
		twoLives = GameObject.Find("2 Lives");
		twoLives.SetActive(value: false);
		threeLives = GameObject.Find("3 Lives");
		threeLives.SetActive(value: false);
	}

	private void Update()
	{
		if (player.lives == 1)
		{
			oneLife.SetActive(value: true);
			twoLives.SetActive(value: false);
			threeLives.SetActive(value: false);
			return;
		}
		if (player.lives == 2)
		{
			oneLife.SetActive(value: true);
			twoLives.SetActive(value: true);
			threeLives.SetActive(value: false);
			return;
		}
		if (player.lives == 3)
		{
			oneLife.SetActive(value: true);
			twoLives.SetActive(value: true);
			threeLives.SetActive(value: true);
			return;
		}
		oneLife.SetActive(value: false);
		twoLives.SetActive(value: false);
		threeLives.SetActive(value: false);
		if (!effectDone)
		{
			GameObject obj = Object.Instantiate(breakEffect, oneLife.transform.position, Quaternion.identity);
			obj.transform.SetParent(canvas.transform, worldPositionStays: false);
			obj.transform.position = new Vector2(oneLife.transform.position.x, oneLife.transform.position.y);
			effectDone = true;
		}
	}
}
