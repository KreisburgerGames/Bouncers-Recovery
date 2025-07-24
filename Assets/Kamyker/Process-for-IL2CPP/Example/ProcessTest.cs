using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using KS.UnityToolbag;
using UnityEngine.UI;


// replace System.Diagnostics with:
using KS.Diagnostics;

// or with this whole block:
// using System.Diagnostics;
// using Process = KS.Diagnostics.Process;
// using ProcessStartInfo = KS.Diagnostics.ProcessStartInfo;
// using Debug = UnityEngine.Debug;

public class ProcessTest : MonoBehaviour
{
	[SerializeField] Text text;
	[SerializeField] Text text2;

	string LogToFile(string text)
	{
#if !UNITY_EDITOR
		File.AppendAllText("log.txt", text);
#endif
		return text;
	}

	IEnumerator Start()
	{
		var unityProcesses = Process.GetProcessesByName("Unity");
		text2.text += LogToFile("Random Unity process id: " + unityProcesses[0].Id + Environment.NewLine);
		text2.text += LogToFile("Has StartTime: " + unityProcesses[0].StartTime + Environment.NewLine);

		foreach(var process in unityProcesses)
			process.Dispose();

		yield return new WaitForSeconds(0.5f);

		var processes = Process.GetProcesses();
		// var processes = System.Diagnostics.Process.GetProcesses();

		StringBuilder sb = new StringBuilder();

		foreach(var process in processes)
		{
			try
			{
				sb.AppendLine(process.ProcessName + ": " + process.Id);
			}
			catch
			{
				//skip finished/unavailable processes
			}
		}

		foreach(var process in processes)
			process.Dispose();

		text2.text += LogToFile(sb.ToString());

		yield return new WaitForSeconds(1);

		var mainFolder = Application.dataPath;

		// this example works only in editor, for building application .exe could be placed for example in StreamingAssets folder https://docs.unity3d.com/Manual/StreamingAssets.html

		// var mainFolder = Application.streamingAssetsPath;
		// var exePath = Path.Combine(mainFolder,"NativeLibraryConsoleTest.ex");

		
		var txtPath = Path.Combine(mainFolder, "Kamyker",
			"Process-for-IL2CPP", "Example",
			"text file for verbs.txt");

		var procStartInfoTxt = new ProcessStartInfo()
		{
			FileName = txtPath,
			WorkingDirectory = Path.GetDirectoryName(txtPath),
			UseShellExecute = true 
		};

		text.text += LogToFile(Environment.NewLine + $"Available verbs for '{Path.GetFileName(procStartInfoTxt.FileName)}' txt file:");
		foreach(var verb in procStartInfoTxt.Verbs)
			text.text += LogToFile(Environment.NewLine + "- " + verb);

		procStartInfoTxt.Verb = "open";
		var procTxt = new Process()
		{
			StartInfo = procStartInfoTxt,
			EnableRaisingEvents = true,
		};
		// procTxt.Exited += (s, d) => Dispatcher.Invoke(() => procTxt.Dispose());
		procTxt.Start();

		
		
		// other example
		var exePath = Path.Combine(mainFolder, "Kamyker",
			"Process-for-IL2CPP", "Example",
// #if UNITY_STANDALONE_LINUX
// 			"NativeLibraryConsoleTest");
// #else
			"NativeLibraryConsoleTest.ex");
// #endif
		var procStartInfo = new ProcessStartInfo()
		{
			FileName = exePath, //exePath
			Arguments = "",
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,
			CreateNoWindow = true,
			WorkingDirectory = Path.GetDirectoryName(exePath),
		};

		text.text += LogToFile(Environment.NewLine);
		text.text += LogToFile(Environment.NewLine + "Program output:");
		
		procStartInfo.FileName = exePath;
		
		var proc = new Process()
		{
			StartInfo = procStartInfo,

			//enables rising Exited event
			EnableRaisingEvents = true
		};
		proc.OutputDataReceived += (s, d) =>
			// Dispatcher is used to run on unity main thread
			Dispatcher.Invoke(() => text.text += LogToFile(Environment.NewLine + d.Data));

		proc.ErrorDataReceived += (s, d) =>
			// Dispatcher is used to run on unity main thread
			Dispatcher.Invoke(() => text.text += LogToFile(Environment.NewLine + d.Data));

		proc.Exited += (s, d) => Dispatcher.Invoke(() => StartCoroutine(OnExit()));

		// make sure path is correct otherwise app may crash
		if(!File.Exists(exePath))
			throw new Exception("File missing: " + exePath);

		if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			proc.Start();
			proc.BeginOutputReadLine();
			proc.BeginErrorReadLine();
			yield return new WaitForSeconds(0.1f);
			proc.StandardInput.BaseStream.Write(new byte[]{55,43,53}, 0, 3);
			proc.StandardInput.BaseStream.Flush();
		}
		else
		{
			UnityEngine.Debug.LogError("Sample contains exe process only for Windows.");
		}
		
		yield return new WaitForSeconds(3f);
		
		// for some reason this returns 0, same result when using .NET api
		text.text += LogToFile(Environment.NewLine + "TotalProcessorTime: " + proc.TotalProcessorTime);
		
		proc.StandardInput.BaseStream.Close();
		// proc.StandardInput.Close();
		proc.Kill();

		IEnumerator OnExit()
		{
			// wait a bit to make sure all messages were read
			yield return new WaitForSeconds(0.1f);
			text.text += LogToFile(Environment.NewLine + "Exited");
			text.text += LogToFile(Environment.NewLine + "ExitCode: " + proc.ExitCode);
			text.text += LogToFile(Environment.NewLine + "ExitTime: " + proc.ExitTime);
			proc.CancelOutputRead();
			proc.Dispose();
			proc = null;
			yield return new WaitForSeconds(0.1f);
		}
	}
}