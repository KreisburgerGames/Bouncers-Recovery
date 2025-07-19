using UnityEngine;

public class SmallPowerup : MonoBehaviour
{
	public float size = 0.3f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Player component = collision.gameObject.GetComponent<Player>();
			component.gameObject.transform.localScale = new Vector3(size, size, size);
			Object.FindFirstObjectByType<AudioManager>().Play("powerup");
			component.gameObject.GetComponent<TrailRenderer>().startWidth = size;
			component.speed *= 1f + size;
			component.manager.smallCollected = true;
			Object.Destroy(base.gameObject);
		}
	}
}
