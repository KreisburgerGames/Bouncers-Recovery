using UnityEngine;

public class DiffHandler : MonoBehaviour
{
	public void SetDifficulty(string difficulty)
	{
		PlayerPrefs.SetString("diff", difficulty);
	}
}
