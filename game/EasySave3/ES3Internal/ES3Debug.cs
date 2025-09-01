using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000CF RID: 207
	internal static class ES3Debug
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		public static void Log(string msg, UnityEngine.Object context = null, int indent = 0)
		{
			if (!ES3Settings.defaultSettingsScriptableObject.logDebugInfo)
			{
				return;
			}
			if (context != null)
			{
				Debug.LogFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable these messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Info'</i>", Array.Empty<object>());
				return;
			}
			Debug.LogFormat(context, ES3Debug.Indent(indent) + msg, Array.Empty<object>());
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001AD30 File Offset: 0x00018F30
		public static void LogWarning(string msg, UnityEngine.Object context = null, int indent = 0)
		{
			if (!ES3Settings.defaultSettingsScriptableObject.logWarnings)
			{
				return;
			}
			if (context != null)
			{
				Debug.LogWarningFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable warnings from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Warnings'</i>", Array.Empty<object>());
				return;
			}
			Debug.LogWarningFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable warnings from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Warnings'</i>", Array.Empty<object>());
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public static void LogError(string msg, UnityEngine.Object context = null, int indent = 0)
		{
			if (!ES3Settings.defaultSettingsScriptableObject.logErrors)
			{
				return;
			}
			if (context != null)
			{
				Debug.LogErrorFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable these error messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Errors'</i>", Array.Empty<object>());
				return;
			}
			Debug.LogErrorFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable these error messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Errors'</i>", Array.Empty<object>());
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		private static string Indent(int size)
		{
			if (size < 0)
			{
				return "";
			}
			return new string('-', size);
		}

		// Token: 0x04000122 RID: 290
		private const string disableInfoMsg = "\n<i>To disable these messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Info'</i>";

		// Token: 0x04000123 RID: 291
		private const string disableWarningMsg = "\n<i>To disable warnings from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Warnings'</i>";

		// Token: 0x04000124 RID: 292
		private const string disableErrorMsg = "\n<i>To disable these error messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Errors'</i>";

		// Token: 0x04000125 RID: 293
		private const char indentChar = '-';
	}
}
