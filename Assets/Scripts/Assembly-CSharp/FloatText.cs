using TMPro;
using UnityEngine;

public class FloatText : MonoBehaviour
{
	private Camera camera;

	private float width;

	private float height;

	private void Awake()
	{
		camera = Object.FindFirstObjectByType<Camera>();
		width = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
		height = camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
	}

	public void Spawn(string text, Color color, string up, string right, float minSpeed, float maxSpeed)
	{
		Rigidbody2D component = base.gameObject.GetComponent<Rigidbody2D>();
		TextMeshPro component2 = base.gameObject.GetComponent<TextMeshPro>();
		component2.text = text;
		component2.color = color;
		if (up == "true")
		{
			if (right == "true")
			{
				component.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
			}
			else if (right == "false")
			{
				component.velocity = new Vector2(Random.Range(0f - maxSpeed, 0f - minSpeed), Random.Range(minSpeed, maxSpeed));
			}
			else
			{
				component.velocity = new Vector2(Random.Range(0f - maxSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
			}
		}
		else if (up == "false")
		{
			if (right == "true")
			{
				component.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(0f - maxSpeed, 0f - minSpeed));
			}
			else if (right == "false")
			{
				component.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(0f - maxSpeed, 0f - minSpeed));
			}
			else
			{
				component.velocity = new Vector2(Random.Range(0f - maxSpeed, maxSpeed), Random.Range(0f - maxSpeed, 0f - minSpeed));
			}
		}
		else if (right == "true")
		{
			component.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(0f - maxSpeed, maxSpeed));
		}
		else if (right == "false")
		{
			component.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(0f - maxSpeed, maxSpeed));
		}
		else
		{
			component.velocity = new Vector2(Random.Range(0f - maxSpeed, maxSpeed), Random.Range(0f - maxSpeed, maxSpeed));
		}
	}

	private void Update()
	{
		if (base.transform.position.x > width + base.transform.lossyScale.x)
		{
			Object.Destroy(base.gameObject);
		}
		if (base.transform.position.x < 0f - width - base.transform.lossyScale.x)
		{
			Object.Destroy(base.gameObject);
		}
		if (base.transform.position.y > height + base.transform.lossyScale.y)
		{
			Object.Destroy(base.gameObject);
		}
		if (base.transform.position.y < 0f - height - base.transform.lossyScale.y)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
