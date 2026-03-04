using UnityEngine;

public class SlowDown : MonoBehaviour
{
	public float timeScale = 0.3f;

	public float startTimeScale = 0.7f;

	public float changeSpeed = 1f;

	public float viggnetteSpeed = 1f;

	public SpriteRenderer vin;

	public float vinIntensity = 0.75f;

	[HideInInspector]
	public bool slowDown;

	[HideInInspector]
	public bool speedUp;

	public float cutoffNormal = 22000f;
	public float cutoffSlow = 1500f;
	public float cutoffSpeed = 50f;
	private AudioLowPassFilter lowPass;

    void Start()
    {
        lowPass = Camera.main.GetComponent<AudioLowPassFilter>();
    }

    private void Update()
	{
		if(Time.timeScale < 1f)
		{
			lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, cutoffSlow, Time.deltaTime * cutoffSpeed);
		}
		else
		{
			lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, cutoffNormal, Time.deltaTime * cutoffSpeed);
		}
		if (slowDown && Object.FindFirstObjectByType<GameManager>().countdownTime <= 0f)
		{
			if (!Object.FindFirstObjectByType<PauseMenu>().paused)
			{
				Color color = vin.color;
				color.a += viggnetteSpeed * Time.deltaTime;
				if (color.a >= vinIntensity)
				{
					color.a = vinIntensity;
				}
				vin.color = color;
				Time.timeScale -= changeSpeed * Time.deltaTime;
				Time.fixedDeltaTime = 0.02f * Time.timeScale;
				if (Time.timeScale <= timeScale)
				{
					Time.timeScale = timeScale;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					slowDown = false;
				}
			}
			else
			{
				Time.timeScale = 0f;
				Time.fixedDeltaTime = 0.02f * Time.timeScale;
			}
		}
		if (speedUp)
		{
			Color color2 = vin.color;
			color2.a -= viggnetteSpeed * Time.deltaTime;
			if (color2.a <= 0f)
			{
				color2.a = 0f;
			}
			vin.color = color2;
			Time.timeScale += changeSpeed * Time.deltaTime;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			if (Time.timeScale >= 1f)
			{
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f * Time.timeScale;
				speedUp = false;
			}
		}
		if (Object.FindFirstObjectByType<Player>().gameObject.GetComponent<GameManager>().falling)
		{
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bouncer" || collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Dasher")
		{
			speedUp = false;
			Time.timeScale = startTimeScale;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			slowDown = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bouncer" || collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Dasher")
		{
			slowDown = false;
			speedUp = true;
		}
	}
}
