using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F8 RID: 248
	internal static class StyleCache
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0001C3F0 File Offset: 0x0001A5F0
		public static bool TryGetValue(long hash, out ComputedStyle data)
		{
			return StyleCache.s_ComputedStyleCache.TryGetValue(hash, out data);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001C40E File Offset: 0x0001A60E
		public static void SetValue(long hash, ref ComputedStyle data)
		{
			StyleCache.s_ComputedStyleCache[hash] = data;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001C424 File Offset: 0x0001A624
		public static bool TryGetValue(int hash, out StyleVariableContext data)
		{
			return StyleCache.s_StyleVariableContextCache.TryGetValue(hash, out data);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001C442 File Offset: 0x0001A642
		public static void SetValue(int hash, StyleVariableContext data)
		{
			StyleCache.s_StyleVariableContextCache[hash] = data;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001C454 File Offset: 0x0001A654
		public static bool TryGetValue(int hash, out ComputedTransitionProperty[] data)
		{
			return StyleCache.s_ComputedTransitionsCache.TryGetValue(hash, out data);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001C472 File Offset: 0x0001A672
		public static void SetValue(int hash, ComputedTransitionProperty[] data)
		{
			StyleCache.s_ComputedTransitionsCache[hash] = data;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001C484 File Offset: 0x0001A684
		public static void ClearStyleCache()
		{
			foreach (KeyValuePair<long, ComputedStyle> keyValuePair in StyleCache.s_ComputedStyleCache)
			{
				keyValuePair.Value.Release();
			}
			StyleCache.s_ComputedStyleCache.Clear();
			StyleCache.s_StyleVariableContextCache.Clear();
			StyleCache.s_ComputedTransitionsCache.Clear();
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001C504 File Offset: 0x0001A704
		// Note: this type is marked as 'beforefieldinit'.
		static StyleCache()
		{
		}

		// Token: 0x04000321 RID: 801
		private static Dictionary<long, ComputedStyle> s_ComputedStyleCache = new Dictionary<long, ComputedStyle>();

		// Token: 0x04000322 RID: 802
		private static Dictionary<int, StyleVariableContext> s_StyleVariableContextCache = new Dictionary<int, StyleVariableContext>();

		// Token: 0x04000323 RID: 803
		private static Dictionary<int, ComputedTransitionProperty[]> s_ComputedTransitionsCache = new Dictionary<int, ComputedTransitionProperty[]>();
	}
}
