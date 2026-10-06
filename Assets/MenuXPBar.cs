using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Steamworks;
using TMPro;
using UnityEngine;

public class MenuXPBar : MonoBehaviour
{
    public UnityEngine.UI.Image xpBar;
    public TMP_Text levelText, xpText;

    void Start()
    {
        SteamAPI.Init();
        SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
        SteamUserStats.GetStat("level", out int level);
        SteamUserStats.GetStat("xp", out int xp);
        SteamUserStats.GetStat("nextLevel", out int xpNeeded);
        SteamUserStats.GetStat("startedXp", out int xpStarted);
        xpBar.fillAmount = (float)(xp-xpStarted)/(xpNeeded-xpStarted);
        levelText.text = $"Level {level}";
        xpText.text = $"{xp}/{xpNeeded}xp";
    }
}
