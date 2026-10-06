using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class ProfileViewerHook : MonoBehaviour
{
    public CSteamID id;

    public void View()
    {
        FindFirstObjectByType<ProfileViewer>().ViewOther(id);
    }
}
