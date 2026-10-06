using System.Collections;
using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileViewer : MonoBehaviour
{
    public GameObject canvas, unfair;
    public RawImage pfp;
    public TMP_Text username, xp, bouncesSurvived, eHS, mHS, hHS, uHS, level, prestiege;

    void Start()
    {
        SteamAPI.Init();
        SteamUserStats.GetAchievement("???", out bool isUnfair);
        if(isUnfair) unfair.SetActive(true);
        canvas.SetActive(false);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void ViewSelf()
    {
        CSteamID id = SteamUser.GetSteamID();
        SteamUserStats.RequestUserStats(id);
        pfp.texture = DisplayHighscores.GetSteamImageAstexture(SteamFriends.GetLargeFriendAvatar(id));
        SteamUserStats.GetUserStat(id, "xp", out int uXp);
        SteamUserStats.GetUserStat(id, "BouncesSurvived", out int uBs);
        SteamUserStats.GetUserStat(id, "EasyHS", out int uEhs);
        SteamUserStats.GetUserStat(id, "MediumHS", out int uMhs);
        SteamUserStats.GetUserStat(id, "HardHS", out int uHhs);
        SteamUserStats.GetUserStat(id, "UnfairHS", out int uUhs);
        SteamUserStats.GetUserStat(id, "level", out int uL);
        SteamUserStats.GetUserStat(id, "prestiege", out int uP);
        username.text = SteamFriends.GetPersonaName();
        xp.text = $"{uXp}xp";
        bouncesSurvived.text = $"{uBs} bounces survived";
        eHS.text = uEhs.ToString();
        mHS.text = uMhs.ToString();
        hHS.text = uHhs.ToString();
        uHS.text = uUhs.ToString();
        prestiege.text = uP != 0 ? "Prestiege {uP}" : "No prestiege" ;
        level.text = $"Level {uL}";
        canvas.SetActive(true);
    }

    public void ViewOther(CSteamID id)
    {
        SteamUserStats.RequestUserStats(id);
        pfp.texture = DisplayHighscores.GetSteamImageAstexture(SteamFriends.GetLargeFriendAvatar(id));
        SteamUserStats.GetUserStat(id, "xp", out int uXp);
        SteamUserStats.GetUserStat(id, "BouncesSurvived", out int uBs);
        SteamUserStats.GetUserStat(id, "EasyHS", out int uEhs);
        SteamUserStats.GetUserStat(id, "MediumHS", out int uMhs);
        SteamUserStats.GetUserStat(id, "HardHS", out int uHhs);
        SteamUserStats.GetUserStat(id, "UnfairHS", out int uUhs);
        SteamUserStats.GetUserStat(id, "level", out int uL);
        SteamUserStats.GetUserStat(id, "prestiege", out int uP);
        username.text = SteamFriends.GetFriendPersonaName(id);
        xp.text = $"{uXp}xp";
        bouncesSurvived.text = $"{uBs} bounces survived";
        eHS.text = uEhs.ToString();
        mHS.text = uMhs.ToString();
        hHS.text = uHhs.ToString();
        uHS.text = uUhs.ToString();
        prestiege.text = uP != 0 ? "Prestiege {uP}" : "No prestiege" ;
        level.text = $"Level {uL}";
        canvas.SetActive(true);
    }
}
