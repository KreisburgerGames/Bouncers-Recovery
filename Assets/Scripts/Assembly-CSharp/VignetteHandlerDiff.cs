using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteHandlerDiff : MonoBehaviour
{
	public PostProcessVolume vol;

	private bool pressed;

	private Vignette vig;

	private void Start()
	{
		vol.profile.TryGetSettings<Vignette>(out vig);
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

	public void Exit()
	{
		vig.color.Override(Color.black);
	}

	private void Update()
	{
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
