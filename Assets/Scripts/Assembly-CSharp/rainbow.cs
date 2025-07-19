using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class rainbow : MonoBehaviour
{
	public float speed;

	public PostProcessVolume vol;

	private Vignette vin;

	private void Awake()
	{
		vol.profile.TryGetSettings<Vignette>(out vin);
	}

	private void Update()
	{
		Color rgbColor = vin.color;
		Color.RGBToHSV(rgbColor, out var H, out var S, out var V);
		H += speed * Time.deltaTime;
		if (H > 360f)
		{
			H = 0f;
		}
		rgbColor = Color.HSVToRGB(H, S, V);
		vin.color.Override(rgbColor);
	}
}
