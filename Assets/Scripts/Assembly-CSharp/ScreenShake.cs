using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	public bool start;

	public AnimationCurve curve;

	public float duration = 1f;

	private Player player;

	private GameManager manager;

	private void Awake()
	{
		player = Object.FindFirstObjectByType<Player>().GetComponent<Player>();
		manager = player.GetComponent<GameManager>();
	}

	private void Update()
	{
		if (start)
		{
			start = false;
			StartCoroutine(Shaking());
		}
	}

	private IEnumerator Shaking()
	{
		Vector2 startPosition = new Vector2(0f, 0f);
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float num = curve.Evaluate(elapsedTime / duration);
			num *= PlayerPrefs.GetFloat("shakeMultiplier") + 0.5f;
			base.transform.position = startPosition + Random.insideUnitCircle * num;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -10f);
			player.chromatic.intensity.Override(num);
			player.grain.intensity.Override(num);
			yield return null;
		}
		manager.tried = true;
		manager.healing = false;
		manager.canHeal = false;
		base.transform.position = startPosition;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -10f);
		player.chromatic.intensity.Override(0f);
		player.grain.intensity.Override(0f);
	}
}
