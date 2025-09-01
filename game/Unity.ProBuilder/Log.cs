using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000025 RID: 37
	internal static class Log
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0001166A File Offset: 0x0000F86A
		public static void PushLogLevel(LogLevel level)
		{
			Log.s_logStack.Push(Log.s_LogLevel);
			Log.s_LogLevel = level;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00011681 File Offset: 0x0000F881
		public static void PopLogLevel()
		{
			Log.s_LogLevel = Log.s_logStack.Pop();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00011692 File Offset: 0x0000F892
		public static void SetLogLevel(LogLevel level)
		{
			Log.s_LogLevel = level;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0001169A File Offset: 0x0000F89A
		public static void SetOutput(LogOutput output)
		{
			Log.s_Output = output;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000116A2 File Offset: 0x0000F8A2
		public static void SetLogFile(string path)
		{
			Log.s_LogFilePath = path;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000116AA File Offset: 0x0000F8AA
		[Conditional("DEBUG")]
		public static void Debug<T>(T value)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000116AC File Offset: 0x0000F8AC
		[Conditional("DEBUG")]
		public static void Debug(string message)
		{
			Log.DoPrint(message, LogType.Log);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000116B5 File Offset: 0x0000F8B5
		[Conditional("DEBUG")]
		public static void Debug(string format, params object[] values)
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000116B7 File Offset: 0x0000F8B7
		public static void Info(string format, params object[] values)
		{
			Log.Info(string.Format(format, values));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000116C5 File Offset: 0x0000F8C5
		public static void Info(string message)
		{
			if ((Log.s_LogLevel & LogLevel.Info) > LogLevel.None)
			{
				Log.DoPrint(message, LogType.Log);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public static void Warning(string format, params object[] values)
		{
			Log.Warning(string.Format(format, values));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000116E6 File Offset: 0x0000F8E6
		public static void Warning(string message)
		{
			if ((Log.s_LogLevel & LogLevel.Warning) > LogLevel.None)
			{
				Log.DoPrint(message, LogType.Warning);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000116F9 File Offset: 0x0000F8F9
		public static void Error(string format, params object[] values)
		{
			Log.Error(string.Format(format, values));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00011707 File Offset: 0x0000F907
		public static void Error(string message)
		{
			if ((Log.s_LogLevel & LogLevel.Error) > LogLevel.None)
			{
				Log.DoPrint(message, LogType.Error);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0001171A File Offset: 0x0000F91A
		[Conditional("CONSOLE_PRO_ENABLED")]
		internal static void Watch<T, K>(T key, K value)
		{
			UnityEngine.Debug.Log(string.Format("{0} : {1}\nCPAPI:{{\"cmd\":\"Watch\" \"name\":\"{0}\"}}", key.ToString(), value.ToString()));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00011745 File Offset: 0x0000F945
		private static void DoPrint(string message, LogType type)
		{
			if ((Log.s_Output & LogOutput.Console) > LogOutput.None)
			{
				Log.PrintToConsole(message, type);
			}
			if ((Log.s_Output & LogOutput.File) > LogOutput.None)
			{
				Log.PrintToFile(message, Log.s_LogFilePath);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00011770 File Offset: 0x0000F970
		private static void PrintToFile(string message, string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			string fullPath = Path.GetFullPath(path);
			if (string.IsNullOrEmpty(fullPath))
			{
				Log.PrintToConsole("m_LogFilePath bad: " + fullPath, LogType.Log);
				return;
			}
			if (!File.Exists(fullPath))
			{
				string directoryName = Path.GetDirectoryName(fullPath);
				if (string.IsNullOrEmpty(directoryName))
				{
					Log.PrintToConsole("m_LogFilePath bad: " + fullPath, LogType.Log);
					return;
				}
				Directory.CreateDirectory(directoryName);
				using (StreamWriter streamWriter = File.CreateText(fullPath))
				{
					streamWriter.WriteLine(message);
					return;
				}
			}
			using (StreamWriter streamWriter2 = File.AppendText(fullPath))
			{
				streamWriter2.WriteLine(message);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00011828 File Offset: 0x0000FA28
		public static void ClearLogFile()
		{
			if (File.Exists(Log.s_LogFilePath))
			{
				File.Delete(Log.s_LogFilePath);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00011840 File Offset: 0x0000FA40
		private static void PrintToConsole(string message, LogType type = LogType.Log)
		{
			if (type == LogType.Log)
			{
				UnityEngine.Debug.Log(message);
				return;
			}
			if (type == LogType.Warning)
			{
				UnityEngine.Debug.LogWarning(message);
				return;
			}
			if (type == LogType.Error)
			{
				UnityEngine.Debug.LogError(message);
				return;
			}
			if (type != LogType.Assert)
			{
				UnityEngine.Debug.Log(message);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0001186C File Offset: 0x0000FA6C
		internal static void NotNull<T>(T obj, string message)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(message);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0001187D File Offset: 0x0000FA7D
		// Note: this type is marked as 'beforefieldinit'.
		static Log()
		{
		}

		// Token: 0x04000076 RID: 118
		public const string k_ProBuilderLogFileName = "ProBuilderLog.txt";

		// Token: 0x04000077 RID: 119
		private static Stack<LogLevel> s_logStack = new Stack<LogLevel>();

		// Token: 0x04000078 RID: 120
		private static LogLevel s_LogLevel = LogLevel.All;

		// Token: 0x04000079 RID: 121
		private static LogOutput s_Output = LogOutput.Console;

		// Token: 0x0400007A RID: 122
		private static string s_LogFilePath = "ProBuilderLog.txt";
	}
}
