using UnityEngine;

public class HeartBreakEffect : MonoBehaviour
{
	public Rigidbody2D left;

	public Rigidbody2D right;

	public float force;

	public float upForce;

	private void Awake()
	{
		left.AddForce(new Vector2(0f - force, upForce), ForceMode2D.Impulse);
		left.AddTorque(180f);
		right.AddForce(new Vector2(force, upForce), ForceMode2D.Impulse);
		right.AddTorque(-180f);
	}
}
