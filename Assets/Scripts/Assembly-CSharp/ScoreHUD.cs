using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
	public Player player;

	private GameManager manager;

	public Text score;

	private void Start()
	{
		manager = player.gameObject.GetComponent<GameManager>();
	}

	private void Update()
	{
		score.text = "Score: " + manager.score;
	}
}
