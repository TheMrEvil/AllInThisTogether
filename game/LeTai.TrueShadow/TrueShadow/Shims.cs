using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x02000011 RID: 17
	public static class Shims
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00004D53 File Offset: 0x00002F53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FindObjectOfType<T>(bool includeInactive = false, bool sorted = true) where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectOfType<T>(includeInactive);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004D5B File Offset: 0x00002F5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] FindObjectsOfType<T>(bool includeInactive = false) where T : UnityEngine.Object
		{
			return UnityEngine.Object.FindObjectsOfType<T>(includeInactive);
		}
	}
}
