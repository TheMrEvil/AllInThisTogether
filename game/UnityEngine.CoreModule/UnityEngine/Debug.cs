using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000109 RID: 265
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	public class Debug
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x000087A5 File Offset: 0x000069A5
		public static ILogger unityLogger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000087AC File Offset: 0x000069AC
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool depthTest = true;
			Debug.DrawLine(start, end, color, duration, depthTest);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000087C8 File Offset: 0x000069C8
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.DrawLine(start, end, color, duration, depthTest);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000087EC File Offset: 0x000069EC
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.DrawLine(start, end, white, duration, depthTest);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00008813 File Offset: 0x00006A13
		[FreeFunction("DebugDrawLine", IsThreadSafe = true)]
		public static void DrawLine(Vector3 start, Vector3 end, [UnityEngine.Internal.DefaultValue("Color.white")] Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine_Injected(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00008824 File Offset: 0x00006A24
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool depthTest = true;
			Debug.DrawRay(start, dir, color, duration, depthTest);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00008840 File Offset: 0x00006A40
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.DrawRay(start, dir, color, duration, depthTest);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00008864 File Offset: 0x00006A64
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool depthTest = true;
			float duration = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, duration, depthTest);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000888B File Offset: 0x00006A8B
		public static void DrawRay(Vector3 start, Vector3 dir, [UnityEngine.Internal.DefaultValue("Color.white")] Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x0600063B RID: 1595
		[FreeFunction("PauseEditor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Break();

		// Token: 0x0600063C RID: 1596
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DebugBreak();

		// Token: 0x0600063D RID: 1597
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int ExtractStackTraceNoAlloc(byte* buffer, int bufferMax, string projectFolder);

		// Token: 0x0600063E RID: 1598 RVA: 0x000088A0 File Offset: 0x00006AA0
		public static void Log(object message)
		{
			Debug.unityLogger.Log(LogType.Log, message);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000088B0 File Offset: 0x00006AB0
		public static void Log(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Log, message, context);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000088C1 File Offset: 0x00006AC1
		public static void LogFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, format, args);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000088D2 File Offset: 0x00006AD2
		public static void LogFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, context, format, args);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000088E4 File Offset: 0x00006AE4
		public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler debugLogHandler = Debug.unityLogger.logHandler as DebugLogHandler;
			bool flag = debugLogHandler == null;
			if (flag)
			{
				Debug.unityLogger.LogFormat(logType, context, format, args);
			}
			else
			{
				bool flag2 = Debug.unityLogger.IsLogTypeAllowed(logType);
				if (flag2)
				{
					debugLogHandler.LogFormat(logType, logOptions, context, format, args);
				}
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00008938 File Offset: 0x00006B38
		public static void LogError(object message)
		{
			Debug.unityLogger.Log(LogType.Error, message);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00008948 File Offset: 0x00006B48
		public static void LogError(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Error, message, context);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00008959 File Offset: 0x00006B59
		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, format, args);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000896A File Offset: 0x00006B6A
		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
		}

		// Token: 0x06000647 RID: 1607
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearDeveloperConsole();

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000648 RID: 1608
		// (set) Token: 0x06000649 RID: 1609
		public static extern bool developerConsoleVisible { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600064A RID: 1610 RVA: 0x0000897C File Offset: 0x00006B7C
		public static void LogException(Exception exception)
		{
			Debug.unityLogger.LogException(exception, null);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000898C File Offset: 0x00006B8C
		public static void LogException(Exception exception, Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000899C File Offset: 0x00006B9C
		public static void LogWarning(object message)
		{
			Debug.unityLogger.Log(LogType.Warning, message);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000089AC File Offset: 0x00006BAC
		public static void LogWarning(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Warning, message, context);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000089BD File Offset: 0x00006BBD
		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, format, args);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000089CE File Offset: 0x00006BCE
		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000089E0 File Offset: 0x00006BE0
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00008A08 File Offset: 0x00006C08
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed", context);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00008A30 File Offset: 0x00006C30
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00008A54 File Offset: 0x00006C54
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00008A78 File Offset: 0x00006C78
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00008A9C File Offset: 0x00006C9C
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00008AC0 File Offset: 0x00006CC0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00008AE4 File Offset: 0x00006CE4
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00008B09 File Offset: 0x00006D09
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, message);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00008B19 File Offset: 0x00006D19
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Assert, message, context);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00008B2A File Offset: 0x00006D2A
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, format, args);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00008B3B File Offset: 0x00006D3B
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600065C RID: 1628
		[NativeProperty(TargetType = TargetType.Field)]
		[StaticAccessor("GetBuildSettings()", StaticAccessorType.Dot)]
		public static extern bool isDebugBuild { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600065D RID: 1629
		[FreeFunction("DeveloperConsole_OpenConsoleFile")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OpenConsoleFile();

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600065E RID: 1630
		[NativeThrows]
		internal static extern DiagnosticSwitch[] diagnosticSwitches { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600065F RID: 1631 RVA: 0x00008B50 File Offset: 0x00006D50
		internal static DiagnosticSwitch GetDiagnosticSwitch(string name)
		{
			foreach (DiagnosticSwitch diagnosticSwitch in Debug.diagnosticSwitches)
			{
				bool flag = diagnosticSwitch.name == name;
				if (flag)
				{
					return diagnosticSwitch;
				}
			}
			throw new ArgumentException("Could not find DiagnosticSwitch named " + name);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00008BA4 File Offset: 0x00006DA4
		[RequiredByNativeCode]
		internal static bool CallOverridenDebugHandler(Exception exception, Object obj)
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					Debug.unityLogger.LogException(exception, obj);
				}
				catch (Exception arg)
				{
					Debug.s_DefaultLogger.LogError(string.Format("Invalid exception thrown from custom {0}.LogException(). Message: {1}", Debug.unityLogger.logHandler.GetType(), arg), obj);
					return false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00008C20 File Offset: 0x00006E20
		[RequiredByNativeCode]
		internal static bool IsLoggingEnabled()
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool logEnabled;
			if (flag)
			{
				logEnabled = Debug.unityLogger.logEnabled;
			}
			else
			{
				logEnabled = Debug.s_DefaultLogger.logEnabled;
			}
			return logEnabled;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00008C60 File Offset: 0x00006E60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Assert(bool, string, params object[]) is obsolete. Use AssertFormat(bool, string, params object[]) (UnityUpgradable) -> AssertFormat(*)", true)]
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00008C84 File Offset: 0x00006E84
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Debug.logger is obsolete. Please use Debug.unityLogger instead (UnityUpgradable) -> unityLogger")]
		public static ILogger logger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00002072 File Offset: 0x00000272
		public Debug()
		{
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00008C9B File Offset: 0x00006E9B
		// Note: this type is marked as 'beforefieldinit'.
		static Debug()
		{
		}

		// Token: 0x06000666 RID: 1638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLine_Injected(ref Vector3 start, ref Vector3 end, [UnityEngine.Internal.DefaultValue("Color.white")] ref Color color, [UnityEngine.Internal.DefaultValue("0.0f")] float duration, [UnityEngine.Internal.DefaultValue("true")] bool depthTest);

		// Token: 0x0400037E RID: 894
		internal static readonly ILogger s_DefaultLogger = new Logger(new DebugLogHandler());

		// Token: 0x0400037F RID: 895
		internal static ILogger s_Logger = new Logger(new DebugLogHandler());
	}
}
