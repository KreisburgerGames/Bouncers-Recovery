using UnityEngine;

public class Sound : MonoBehaviour
{
	public new string name;

	public AudioClip sound;

	public float volume;

	public bool loop;

	[HideInInspector]
	public bool wasPlaying;

	[HideInInspector]
	public AudioSource audioSource;

	private void Awake()
	{
		if (PlayerPrefs.HasKey("volume"))
		{
			volume = PlayerPrefs.GetFloat("volume");
		}
		else
		{
			volume = 1f;
		}
	}

	private void Update()
	{
	}
}
