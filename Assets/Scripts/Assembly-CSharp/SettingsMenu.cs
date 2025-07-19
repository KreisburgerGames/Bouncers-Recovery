using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public bool mouseControls;
	public Scrollbar volumeSlider;

	public AudioMixer audio;

	public static SettingsMenu instance;

	public TMP_Dropdown graphicsDropdown;

	public GameObject mouseControlToggle;

	public GameObject fullscreenToggle;

	private Resolution[] resolutions;

	public GameObject rainbowToggle;

	public TMP_Dropdown resolutionDropdown;

	public Scrollbar shakeMultiplier;

	public TMP_Text multText;
	private int index = 0;

	private void CheckGraphics()
	{
		print(index);
		print(Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString() + " @ " + Screen.currentResolution.refreshRate.ToString());
		graphicsDropdown.value = graphicsDropdown.options.FindIndex(x => x.text == Screen.currentResolution.width.ToString() + " x " + Screen.currentResolution.height.ToString() + " @ " + Screen.currentResolution.refreshRate.ToString());
	}

	private void Awake()
	{
		if (PlayerPrefs.GetInt("rainbowBouncers") == 1)
		{
			MonoBehaviour.print("rainbow");
			rainbowToggle.GetComponent<Toggle>().isOn = true;
		}
		else
		{
			rainbowToggle.GetComponent<Toggle>().isOn = false;
		}
		if (PlayerPrefs.HasKey("MasterVol"))
		{
			volumeSlider.value = PlayerPrefs.GetFloat("MasterVol");
			MonoBehaviour.print("volume");
		}
		else
		{
			PlayerPrefs.SetFloat("MasterVol", -5f);
			volumeSlider.value = 1;
		}
		shakeMultiplier.value = PlayerPrefs.GetFloat("shakeMultiplier");
		multText.text = (MathF.Round(shakeMultiplier.value, 2) + 0.5f).ToString();
		if (PlayerPrefs.HasKey("mouseControls"))
		{
			if (PlayerPrefs.GetInt("mouseControls") == 1)
			{
				mouseControlToggle.GetComponent<Toggle>().isOn = true;
			}
			else
			{
				mouseControlToggle.GetComponent<Toggle>().isOn = false;
			}
			MonoBehaviour.print("mouseControls");
		}
		else
		{
			PlayerPrefs.SetInt("mouseControls", 1);
			mouseControlToggle.GetComponent<Toggle>().isOn = true;
		}
		if (PlayerPrefs.HasKey("fullscreen"))
		{
			if (PlayerPrefs.GetInt("fullscreen") == 1)
			{
				fullscreenToggle.GetComponent<Toggle>().isOn = true;
			}
			else
			{
				fullscreenToggle.GetComponent<Toggle>().isOn = false;
			}
			MonoBehaviour.print("fullscreen");
		}
		else
		{
			PlayerPrefs.SetInt("fullscreen", 1);
			fullscreenToggle.GetComponent<Toggle>().isOn = true;
		}
	}

	private void Start()
	{
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();
		List<string> list = new List<string>();
		for (int i = 0; i < resolutions.Length; i++)
		{
			string item = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate.ToString();
			list.Add(item);
			index++;
		}
		resolutionDropdown.AddOptions(list);
		resolutionDropdown.RefreshShownValue();
		CheckGraphics();
	}

	public void SetResolution(int resIndex)
	{
		Resolution resolution = resolutions[resIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetMultiplier()
	{
		PlayerPrefs.SetFloat("shakeMultiplier", shakeMultiplier.value);
		multText.text = (MathF.Round(shakeMultiplier.value, 2) + 0.5f).ToString();
	}

	public void SetVolume(float volume)
	{
		float value = Mathf.Lerp(-80f, -5f, volume);
		audio.SetFloat("Master", value);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetFullscreen()
	{
		if (fullscreenToggle.gameObject.GetComponent<Toggle>().isOn)
		{
			Screen.fullScreen = true;
		}
		else
		{
			Screen.fullScreen = false;
		}
	}

	public void Save()
	{
		if (mouseControlToggle.GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("mouseControls", 1);
		}
		else
		{
			PlayerPrefs.SetInt("mouseControls", 0);
		}
		if (fullscreenToggle.GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("fullscreen", 1);
		}
		else
		{
			PlayerPrefs.SetInt("fullscreen", 0);
		}
		if (rainbowToggle.GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("rainbowBouncers", 1);
		}
		else
		{
			PlayerPrefs.SetInt("rainbowBouncers", 0);
		}
		PlayerPrefs.SetInt("res", resolutionDropdown.value);
		PlayerPrefs.SetFloat("MasterVol", volumeSlider.value);
		PlayerPrefs.SetInt("graphics", graphicsDropdown.value);
		PlayerPrefs.SetFloat("shakeMultiplier", shakeMultiplier.value);
		PlayerPrefs.Save();
	}
}
