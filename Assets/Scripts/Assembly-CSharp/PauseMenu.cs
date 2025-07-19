using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[HideInInspector]
	public bool paused;

	public GameObject canvasObject;

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape))
		{
			return;
		}
		if (!paused)
		{
			Pause();
			Cursor.visible = true;
			return;
		}
		Resume();
		if (PlayerPrefs.GetInt("mouseControls") == 0)
		{
			Cursor.visible = false;
		}
	}

	public void Pause()
	{
		paused = true;
		Time.timeScale = 0f;
		canvasObject.SetActive(value: true);
		Object.FindFirstObjectByType<AudioManager>().Pause();
	}

	public void Resume()
	{
		Object.FindFirstObjectByType<AudioManager>().Play("select");
		paused = false;
		Time.timeScale = 1f;
		canvasObject.SetActive(value: false);
		EventSystem.current.SetSelectedGameObject(null);
		Object.FindFirstObjectByType<AudioManager>().Resume();
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Main Menu");
		Time.timeScale = 1f;
		paused = false;
		Object.Destroy(base.gameObject);
	}

	public void Restart()
	{
		SceneManager.LoadScene("Game");
		Time.timeScale = 1f;
		paused = false;
		Object.Destroy(base.gameObject);
	}

	public static void Quit()
	{
		Application.Quit();
	}
}
