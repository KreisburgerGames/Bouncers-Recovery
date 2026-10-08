using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Steamworks;
using TMPro;
using UnityEngine;

public class DeathXPBar : MonoBehaviour
{
    public UnityEngine.UI.Image xpBar, leadBar;
    public TMP_Text levelText, xpText, xpGainText;
    private int xpAdding;
    private int oldLevel, oldXp, oldXpNeeded, oldXpStarted, newLevel, newXp, newXpNeeded, newXpStarted;
    private int currentXP;
    private Coroutine c;
    private int xpGain;

    void Awake()
    {
        StartCoroutine(WaitForDSS());
    }

    public void Init(int oL, int oX, int oXN, int oXS)
    {
        oldLevel = oL;
        oldXp = oX;
        oldXpNeeded = oXN;
        oldXpStarted = oXS;
        currentXP = oldXp;
    }
    private IEnumerator WaitForDSS()
    {
        while(oldXpNeeded == 0) yield return null;
        SteamAPI.Init();
        Debug.Log($"DeathXPBar Start() on {gameObject.name}, instance ID: {GetInstanceID()}");
        SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
        SteamUserStats.GetStat("level", out newLevel);
        SteamUserStats.GetStat("xp", out newXp);
        print(newXp);
        SteamUserStats.GetStat("nextLevel", out newXpNeeded);
        SteamUserStats.GetStat("startedXp", out newXpStarted);
        xpBar.fillAmount = (float)(oldXp-oldXpStarted)/(oldXpNeeded-oldXpStarted);
        levelText.text = $"Level {oldLevel}";
        xpText.text = $"{oldXp}/{oldXpNeeded}xp";
        xpGain = newXp - oldXp;
        xpGainText.text = $"+{xpGain}xp";
        xpAdding = GameManager.instance.totalXp;
        StartCoroutine(AddXP());
    }

    private IEnumerator LevelUp(int xp, int xpGoal, int oldXpGoal)
    {
        print("up");
        leadBar.fillAmount = (float)(xp-oldXpGoal)/(xpGoal-oldXpGoal);
        float time = 0f;
        float startFill = leadBar.fillAmount;
        xpText.text = $"{xp}/{xpGoal}xp";
        while (leadBar.fillAmount != 1)
        {
            time += Time.deltaTime;
            time = Mathf.Clamp(time, 0f, 1f);
            leadBar.fillAmount = Mathf.Lerp(startFill, 1f, time);
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        startFill = xpBar.fillAmount;
        while (1 - xpBar.fillAmount > 0.001f)
        {
            xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, 1, 3f * Time.deltaTime);
            int newXpText = (int)Mathf.Round(Mathf.Lerp(xp, xpGoal, xpBar.fillAmount));
            xpText.text = $"{newXpText}/{xpGoal}xp";
            xpGain = newXp - newXpText;
            xpGainText.text = $"+{xpGain}xp";
            yield return null;
        }
        xpBar.fillAmount = 0f;
        leadBar.fillAmount = 0f;
        oldLevel++;
        levelText.text = $"Level {oldLevel}";
        currentXP = xpGoal;
        c = null;
    }

    private IEnumerator AddXP()
    {
        yield return new WaitForSeconds(1f);
        List<IEnumerator> levelCorutines = new List<IEnumerator>();
        int xpNeeded = oldXpNeeded;
        int rXp = oldXp;
        int rXpGoal = oldXpNeeded;
        int rXpStart = oldXpStarted;
        for(int i = 0; i < newLevel - oldLevel; i++)
        {
            levelCorutines.Add(LevelUp(rXp, rXpGoal, rXpStart));
            rXpStart = rXpGoal;
            rXpGoal = Mathf.RoundToInt(rXpGoal * 2.5f * (1 + (oldLevel/75f)));
            rXp = rXpStart;
        }
        foreach(IEnumerator lvl in levelCorutines)
        {
            c = StartCoroutine(lvl);
            while(c != null) yield return null;
        }
        float t = 0;
        float fillAmount = (float)(newXp-newXpStarted)/(newXpNeeded-newXpStarted);
        xpText.text = $"{currentXP}/{newXpNeeded}xp";
        float startFill = leadBar.fillAmount;
        while(leadBar.fillAmount != fillAmount)
        {
            t += Time.deltaTime;
            t = Mathf.Clamp(t, 0f, 1f);
            leadBar.fillAmount = Mathf.Lerp(startFill, fillAmount, t);
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        while (fillAmount - xpBar.fillAmount > 0.001f)
        {
            xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, fillAmount, 2f * Time.deltaTime);
            int newXpText = Mathf.RoundToInt(Mathf.Lerp(currentXP, newXp, xpBar.fillAmount/fillAmount));
            xpText.text = $"{newXpText}/{newXpNeeded}xp";
            xpGain = newXp - newXpText;
            xpGainText.text = $"+{xpGain}xp";
            yield return null;
        }
        xpBar.fillAmount = fillAmount;
        xpText.text = $"{newXp}/{newXpNeeded}xp";
        xpGainText.text = $"+0xp";
        yield return new WaitForSeconds(1f);
        t = 0f;
        while(xpGainText.color.a > 0.001f)
        {
            t += Time.deltaTime;
            UnityEngine.Color c = xpGainText.color;
            c.a = Mathf.Lerp(c.a, 0f, t * 8f * Time.deltaTime);
            xpGainText.color = c;
            yield return null;
        }
        UnityEngine.Color f = xpGainText.color;
        f.a = 0f;
        xpGainText.color = f;
    }
}
