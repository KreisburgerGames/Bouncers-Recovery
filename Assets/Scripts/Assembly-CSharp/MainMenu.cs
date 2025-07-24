using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Security.Cryptography;
using System.IO;
using System.Text;


public class MainMenu : MonoBehaviour
{
	public GameObject[] keepOnSettingsLoad;
	

	private bool started;
	public AudioMixer mixer;
	private float value;

    public static string Decrypt(string cipherText, string password, string salt)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            aes.Key = key.GetBytes(32);
            aes.IV = key.GetBytes(16);

            byte[] buffer = Convert.FromBase64String(cipherText);

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs)) {
                return sr.ReadToEnd();
            }
        }
    }

	private void Awake()
	{
		if (!PlayerPrefs.HasKey("mouseControls"))
		{
			PlayerPrefs.SetInt("mouseControls", 1);
		}
		started = false;
		if (GameObject.FindGameObjectsWithTag("Audio").Length > 1)
		{
			UnityEngine.Object.Destroy(GameObject.FindGameObjectsWithTag("Audio")[0]);
		}
		if (PlayerPrefs.HasKey("MasterVol"))
		{
			float volume = PlayerPrefs.GetFloat("MasterVol");
			value = Mathf.Lerp(-80f, -5f, volume);
		}
		else
		{
			mixer.SetFloat("Master", -5f);
			PlayerPrefs.SetFloat("MasterVol", 1f);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!PlayerPrefs.HasKey("rainbowBouncers"))
		{
			PlayerPrefs.SetInt("rainbowBouncers", 0);
		}
		DateTime dateTime = new DateTime(2023, 6, 20);
		if (Application.version.Contains("Beta") && DateTime.Today > dateTime)
		{
			Application.Quit();
		}
		if (!PlayerPrefs.HasKey("shakeMultiplier"))
		{
			PlayerPrefs.SetFloat("shakeMultiplier", 1f);
		}
		if (!PlayerPrefs.HasKey("Skin"))
		{
			PlayerPrefs.SetString("Skin", "Default");
		}
	}

	private void Update()
	{
		if (UnityEngine.Object.FindFirstObjectByType<AudioManager>() != null && !started)
		{
			mixer.SetFloat("Master", value);
			UnityEngine.Object.FindFirstObjectByType<AudioManager>().Play("theme");
			started = true;
		}
	}
}
