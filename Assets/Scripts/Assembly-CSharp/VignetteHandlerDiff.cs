using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteHandlerDiff : MonoBehaviour
{
	public PostProcessVolume vol;

	private bool pressed;

	private Vignette vig;
	private Grain grain;
	public float grainChangeSpeed = 5f;
	public float unfairGrainValue = 10f;
	private bool isGrain;
	public float defaultGrain;
	public float volChangeSpeed;
	public AudioSource staticSound;

	private void Start()
	{
		vol.profile.TryGetSettings<Vignette>(out vig);
		vol.profile.TryGetSettings<Grain>(out grain);
	}

	public void EasyHover()
	{
		vig.color.Override(Color.green);
	}

	public void MediumHover()
	{
		vig.color.Override(Color.yellow);
	}

	public void HardHover()
	{
		vig.color.Override(Color.red);
	}

	public void UnfairHover()
	{
		vig.color.Override(Color.blue);
		isGrain = true;
	}

	public void Exit()
	{
		vig.color.Override(Color.black);
		isGrain = false;
	}

	private void Update()
	{
		if (isGrain)
		{
			grain.intensity.Override(Mathf.Lerp(grain.intensity.value, unfairGrainValue, grainChangeSpeed * Time.deltaTime));
			staticSound.volume = Mathf.Lerp(staticSound.volume, 1, volChangeSpeed * Time.deltaTime);
		}
		else
		{
			grain.intensity.Override(Mathf.Lerp(grain.intensity.value, defaultGrain, grainChangeSpeed * 10 * Time.deltaTime));
			staticSound.volume = Mathf.Lerp(staticSound.volume, 0, volChangeSpeed * 10 * Time.deltaTime);
		}
		if (Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.L))
		{
			pressed = false;
		}
		if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.L) && !pressed)
		{
			Application.OpenURL("https://www.youtube.com/watch?v=nkVzm0x9MSI&t=912s");
			pressed = true;
		}
	}
}
