using UnityEngine;

public class HealthHUD : MonoBehaviour
{
	public Player player;

	public RectTransform healthBar;

	public RectTransform healthBarRed;

	private float healthBarWidthMultiplier;

	private float startingMultiplier;

	private void Start()
	{
		healthBarWidthMultiplier = healthBar.rect.width / (float)player.maxHealth;
	}

	private void Update()
	{
		healthBar.sizeDelta = new Vector2((float)player.health * healthBarWidthMultiplier, healthBar.rect.height);
		healthBarRed.sizeDelta = new Vector2((float)player.maxHealth * healthBarWidthMultiplier, healthBarRed.rect.height);
		int num = player.maxHealth - player.health;
		healthBar.localPosition = new Vector3(healthBar.sizeDelta.x * -1f - (float)num * healthBarWidthMultiplier, healthBar.localPosition.y, healthBar.localPosition.z);
		healthBarRed.localPosition = new Vector3(healthBarRed.sizeDelta.x * -1f, healthBarRed.localPosition.y, healthBarRed.localPosition.z);
	}
}
