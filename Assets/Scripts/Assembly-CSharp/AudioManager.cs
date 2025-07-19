using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;

	public AudioMixerGroup mixer;

	private void Awake()
	{
		Sound[] array = sounds;
		foreach (Sound sound in array)
		{
			sound.audioSource = base.gameObject.AddComponent<AudioSource>();
			sound.audioSource.clip = sound.sound;
			sound.audioSource.volume = sound.volume;
			sound.audioSource.outputAudioMixerGroup = mixer;
			if (sound.loop)
			{
				sound.audioSource.loop = true;
			}
			else
			{
				sound.audioSource.loop = false;
			}
		}
	}

	public void Play(string name)
	{
		Array.Find(sounds, (Sound x) => x.name == name).audioSource.Play();
	}

	public void Pause()
	{
		Sound[] array = sounds;
		foreach (Sound sound in array)
		{
			if (sound.audioSource.isPlaying)
			{
				sound.wasPlaying = true;
				sound.audioSource.Pause();
			}
		}
	}

	public void Resume()
	{
		Sound[] array = sounds;
		foreach (Sound sound in array)
		{
			if (sound.wasPlaying)
			{
				sound.wasPlaying = false;
				sound.audioSource.Play();
			}
		}
	}
}
