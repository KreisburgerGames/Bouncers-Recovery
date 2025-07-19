using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
	public float minSpeed = 1f;

	public float maxSpeed = 3f;

	public ParticleSystem claim;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Player component = collision.gameObject.GetComponent<Player>();
			int num = (int)Random.Range(minSpeed, maxSpeed + 1f);
			component.speed += num;
			component.manager.speedCollected = true;
			Object.Instantiate(claim, base.transform.position, Quaternion.identity);
			Object.Instantiate(component.floatText, base.transform.position, Quaternion.identity).GetComponent<FloatText>().Spawn("+" + num + " Speed!", Color.blue, "true", null, 2f, 4f);
			Object.FindFirstObjectByType<AudioManager>().Play("powerup");
			Object.Destroy(base.gameObject);
		}
	}
}
