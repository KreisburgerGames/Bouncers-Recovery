using System;
using UnityEngine;

public class MenuBouncer : MonoBehaviour
{
	private Rigidbody2D rb;

	public float maxVelocity = 13f;

	public float startSpeed = 8f;

	public float paddingMultiplier = 1f;

	public float cornerPaddingMultiplier = 2f;

	public int directionRange = 3;

	private float previousx;

	private float previousy;

	public Camera camera;

	private float width;

	private float height;

	private bool vertical;

	public float left;

	private System.Random r = new System.Random();

	private bool set;

	private string lastBounce = "";

	private void Awake()
	{
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(startSpeed, 0f);
		previousx = base.transform.position.x;
		previousy = base.transform.position.y;
	}

	private void Start()
	{
		previousx = base.transform.position.x;
		previousy = base.transform.position.y;
		rb = base.gameObject.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		float x = base.transform.position.x;
		float y = base.transform.position.y;
		float num = x - previousx;
		float num2 = y - previousy;
		float num3 = num * paddingMultiplier;
		float num4 = num2 * paddingMultiplier;
		float num5 = (num + num2) * cornerPaddingMultiplier;
		bool flag = false;
		_ = Screen.width;
		if (!set)
		{
			width = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0f)).x;
			height = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0f)).y;
			left = camera.ScreenToWorldPoint(new Vector3(0f, Screen.height / 2, 0f)).x;
			set = true;
		}
		previousx = base.transform.position.x;
		previousy = base.transform.position.y;
		if (base.transform.position.x + num3 + base.transform.lossyScale.x >= width && lastBounce != "right")
		{
			if (base.transform.position.y - num5 <= 0f - height && base.transform.position.y - num5 <= 0f && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f + 1f);
				flag = true;
			}
			else if (base.transform.position.y + num5 >= height && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f - 1f);
				flag = true;
			}
			rb.velocity = new Vector2(UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed + 2f), rb.velocity.y);
			if (base.transform.position.y > 0f && !flag && !vertical)
			{
				if (base.transform.position.y < 3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(startSpeed - 2f, maxVelocity));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed));
				}
			}
			else if (!flag && !vertical)
			{
				if (base.transform.position.y > -3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(directionRange * -1, directionRange));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(0, directionRange));
				}
			}
			lastBounce = "right";
			vertical = false;
		}
		else if (base.transform.position.x + num3 - base.transform.lossyScale.x <= 0f - width && lastBounce != "left")
		{
			if (base.transform.position.y - num5 <= 0f - height && base.transform.position.y - num5 <= 0f && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f + 1f);
				flag = true;
			}
			else if (base.transform.position.y + num5 >= height && !vertical)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f - 1f);
				flag = true;
			}
			rb.velocity = new Vector2(UnityEngine.Random.Range(startSpeed - 2f, maxVelocity), rb.velocity.y);
			if (base.transform.position.y > 0f && !flag && !vertical)
			{
				if (base.transform.position.y < 3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(startSpeed - 2f, maxVelocity));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed));
				}
			}
			else if (!flag && !vertical)
			{
				if (base.transform.position.y > -3f)
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(directionRange * -1, directionRange));
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + (float)r.Next(0, directionRange));
				}
			}
			lastBounce = "left";
		}
		else if (base.transform.position.y + num4 - base.transform.lossyScale.y <= 0f - height && lastBounce != "down" && !flag)
		{
			vertical = false;
			rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(startSpeed - 2f, maxVelocity));
			lastBounce = "down";
			if (UnityEngine.Random.Range(1, 5) == 3)
			{
				rb.velocity = new Vector2(rb.velocity.x * -1f + (float)UnityEngine.Random.Range(-2, 2), rb.velocity.y);
			}
			vertical = true;
		}
		else if (base.transform.position.y + num4 + base.transform.lossyScale.y >= height && lastBounce != "up" && !flag)
		{
			vertical = false;
			rb.velocity = new Vector2(rb.velocity.x, UnityEngine.Random.Range(0f - maxVelocity, 0f - startSpeed + 2f));
			lastBounce = "up";
			if (UnityEngine.Random.Range(1, 5) == 3)
			{
				rb.velocity = new Vector2(rb.velocity.x * -1f + (float)UnityEngine.Random.Range(-2, 2), rb.velocity.y);
			}
			vertical = true;
		}
		if (rb.velocity.x < maxVelocity * -1f)
		{
			rb.velocity = new Vector2(maxVelocity * -1f, rb.velocity.y);
		}
		if (rb.velocity.x > maxVelocity)
		{
			rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
		}
		if (rb.velocity.y < maxVelocity * -1f)
		{
			rb.velocity = new Vector2(rb.velocity.x, maxVelocity * -1f);
		}
		if (rb.velocity.y > maxVelocity)
		{
			rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
		}
		flag = false;
	}
}
