using System;
using System.Diagnostics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000E1 RID: 225
	public static class Develop
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00020679 File Offset: 0x0001E879
		public static void Log(in object mes)
		{
			UnityEngine.Debug.Log(string.Format("[MC2] {0}", mes));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0002068C File Offset: 0x0001E88C
		public static void LogWarning(in object mes)
		{
			UnityEngine.Debug.LogWarning(string.Format("[MC2] {0}", mes));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0002069F File Offset: 0x0001E89F
		public static void LogError(in object mes)
		{
			UnityEngine.Debug.LogError(string.Format("[MC2] {0}", mes));
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000206B2 File Offset: 0x0001E8B2
		[Conditional("MC2_DEBUG")]
		public static void DebugLog(in object mes)
		{
			UnityEngine.Debug.Log(string.Format("[MC2 DEBUG] {0}", mes));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000206C5 File Offset: 0x0001E8C5
		[Conditional("MC2_DEBUG")]
		public static void DebugLogWarning(in object mes)
		{
			UnityEngine.Debug.LogWarning(string.Format("[MC2 DEBUG] {0}", mes));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000206D8 File Offset: 0x0001E8D8
		[Conditional("MC2_DEBUG")]
		public static void DebugLogError(in object mes)
		{
			UnityEngine.Debug.LogError(string.Format("[MC2 DEBUG] {0}", mes));
		}
	}
}
