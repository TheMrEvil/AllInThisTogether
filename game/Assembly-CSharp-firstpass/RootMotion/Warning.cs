using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000C5 RID: 197
	public static class Warning
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0003A6A0 File Offset: 0x000388A0
		public static void Log(string message, Warning.Logger logger, bool logInEditMode = false)
		{
			if (!logInEditMode && !Application.isPlaying)
			{
				return;
			}
			if (Warning.logged)
			{
				return;
			}
			if (logger != null)
			{
				logger(message);
			}
			Warning.logged = true;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0003A6C5 File Offset: 0x000388C5
		public static void Log(string message, Transform context, bool logInEditMode = false)
		{
			if (!logInEditMode && !Application.isPlaying)
			{
				return;
			}
			if (Warning.logged)
			{
				return;
			}
			Debug.LogWarning(message, context);
			Warning.logged = true;
		}

		// Token: 0x040006D0 RID: 1744
		public static bool logged;

		// Token: 0x020001E6 RID: 486
		// (Invoke) Token: 0x0600100A RID: 4106
		public delegate void Logger(string message);
	}
}
