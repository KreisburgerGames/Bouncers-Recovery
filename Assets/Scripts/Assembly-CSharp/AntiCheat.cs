using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;
using Debug = UnityEngine.Debug;

public class AntiCheat : MonoBehaviour
{
    public static AntiCheat ac;
    private static readonly string[] BlacklistedProcesses = {
        "cheatengine", "ollydbg", "x64dbg", "ida", "dnspy", "ghidra",
        "megadumper", "processhacker", "ilspy", "resharp"
    };

    private static readonly string[] SuspiciousDlls = {
        "cheat", "inject", "mod", "hack", "bypass"
    };

    private static readonly string[] SuspiciousRootFilesOrFolders = {
        "dnSpy", "CheatEngine", "ModMenu.dll", "Injector.exe", "hacks", "bypass.txt",
        "exploit", "dump.cs", "dll_injector", "Trainer"
    };

    private static readonly string[] DecoyTriggerPaths = {
        "C:/temp/backdoor.dll", "C:/Windows/Temp/exploit.dll"
    };

    [DllImport("kernel32.dll")]
    private static extern bool IsDebuggerPresent();

    void Start()
    {
        if (ac != null)
        {
            Destroy(gameObject);
        }else
            ac = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Watchdog());
        HookAssemblyLoad();
        CheckTimeBomb();
        CheckHardwareId();
        CheckDllFolder();
        CheckRootFolder();
        CheckProcessList();
        CheckForDebugger();
        CheckIL();
        SetupDecoyTrap();
    }

    private IEnumerator Watchdog()
    {
        while (true)
        {
            CheckProcessList();
            CheckForDebugger();
            CheckDllFolder();
            CheckRootFolder();
            yield return new WaitForSeconds(5f);
        }
    }

    void Crash(string reason)
    {
        Debug.LogError("Anti-Cheat Triggered: " + reason);
        unsafe
        {
            byte* ptr = null;
            *ptr = 0;
        }
    }

    void CheckProcessList()
    {
        foreach (var proc in Process.GetProcesses())
        {
            string name = proc.ProcessName.ToLower();
            if (BlacklistedProcesses.Any(b => name.Contains(b)))
                Crash("Blacklisted process: " + name);
        }
    }

    void CheckDllFolder()
    {
        string basePath = Application.dataPath;
        var files = Directory.GetFiles(basePath, "*.dll", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            string name = Path.GetFileName(file).ToLower();
            if (SuspiciousDlls.Any(s => name.Contains(s)))
                Crash("Suspicious DLL: " + name);
        }
    }

    void CheckRootFolder()
    {
        string root = Directory.GetParent(Application.dataPath).FullName;
        var filesAndFolders = Directory.GetFileSystemEntries(root);
        foreach (var entry in filesAndFolders)
        {
            string name = Path.GetFileName(entry).ToLower();
            if (SuspiciousRootFilesOrFolders.Any(s => name.Contains(s.ToLower())))
                Crash("Suspicious file/folder in root: " + name);
        }
    }

    void CheckForDebugger()
    {
        if (Debugger.IsAttached || IsDebuggerPresent())
            Crash("Debugger detected");
    }

    void CheckIL()
    {
        var method = typeof(AntiCheat).GetMethod("CheckIL", BindingFlags.NonPublic | BindingFlags.Instance);
        var il = method.GetMethodBody().GetILAsByteArray();
        if (il.Length < 15)
            Crash("IL tampering detected");
    }

    void HookAssemblyLoad()
    {
        AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>
        {
            var name = args.LoadedAssembly.FullName.ToLower();
            if (SuspiciousDlls.Any(s => name.Contains(s)))
                Crash("DLL Injection: " + name);
        };
    }

    void CheckTimeBomb()
    {
        DateTime expiry = new DateTime(2026, 1, 1);
        if (DateTime.Now > expiry)
            Crash("Expired client");
    }

    void CheckHardwareId()
    {
        string hwid = SystemInfo.deviceUniqueIdentifier;
        if (hwid == "blacklisted_hwid_1" || hwid == "blacklisted_hwid_2")
            Crash("Blacklisted HWID");
    }

    void SetupDecoyTrap()
    {
        foreach (var path in DecoyTriggerPaths)
        {
            if (File.Exists(path))
                Crash("Decoy file triggered: " + path);
        }
    }
}
