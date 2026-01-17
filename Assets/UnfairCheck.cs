using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class UnfairCheck : MonoBehaviour
{
    public GameObject btn;
    private static string[] achivementsRequired =
    {
        "Juked",
        "BulkedUp",
        "Impressive",
        "FirstSteps",
        "OP",
        "SkillIssue"
    };
    public void whatisthis()
    {
        SteamUserStats.SetAchievement("???");
    }
    void Start()
    {
        SteamAPI.Init();
        bool ready = true;
        foreach (string ach in achivementsRequired)
        {
            SteamUserStats.GetAchievement(ach, out bool done);
            if (!done)
            {
                ready = false;
                break;
            }
        }
        if (ready)
        {
            btn.SetActive(true);
        }
    }
}
