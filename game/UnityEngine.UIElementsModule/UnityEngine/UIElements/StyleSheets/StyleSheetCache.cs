using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200036B RID: 875
	internal static class StyleSheetCache
	{
		// Token: 0x06001C4B RID: 7243 RVA: 0x00085825 File Offset: 0x00083A25
		internal static void ClearCaches()
		{
			StyleSheetCache.s_RulePropertyIdsCache.Clear();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00085834 File Offset: 0x00083A34
		internal static StylePropertyId[] GetPropertyIds(StyleSheet sheet, int ruleIndex)
		{
			StyleSheetCache.SheetHandleKey key = new StyleSheetCache.SheetHandleKey(sheet, ruleIndex);
			StylePropertyId[] array;
			bool flag = !StyleSheetCache.s_RulePropertyIdsCache.TryGetValue(key, out array);
			if (flag)
			{
				StyleRule styleRule = sheet.rules[ruleIndex];
				array = new StylePropertyId[styleRule.properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = StyleSheetCache.GetPropertyId(styleRule, i);
				}
				StyleSheetCache.s_RulePropertyIdsCache.Add(key, array);
			}
			return array;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000858B4 File Offset: 0x00083AB4
		internal static StylePropertyId[] GetPropertyIds(StyleRule rule)
		{
			StylePropertyId[] array = new StylePropertyId[rule.properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StyleSheetCache.GetPropertyId(rule, i);
			}
			return array;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000858F4 File Offset: 0x00083AF4
		private static StylePropertyId GetPropertyId(StyleRule rule, int index)
		{
			StyleProperty styleProperty = rule.properties[index];
			string name = styleProperty.name;
			StylePropertyId result;
			bool flag = !StylePropertyUtil.s_NameToId.TryGetValue(name, out result);
			if (flag)
			{
				result = (styleProperty.isCustomProperty ? StylePropertyId.Custom : StylePropertyId.Unknown);
			}
			return result;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0008593C File Offset: 0x00083B3C
		// Note: this type is marked as 'beforefieldinit'.
		static StyleSheetCache()
		{
		}

		// Token: 0x04000E1F RID: 3615
		private static StyleSheetCache.SheetHandleKeyComparer s_Comparer = new StyleSheetCache.SheetHandleKeyComparer();

		// Token: 0x04000E20 RID: 3616
		private static Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]> s_RulePropertyIdsCache = new Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]>(StyleSheetCache.s_Comparer);

		// Token: 0x0200036C RID: 876
		private struct SheetHandleKey
		{
			// Token: 0x06001C50 RID: 7248 RVA: 0x00085957 File Offset: 0x00083B57
			public SheetHandleKey(StyleSheet sheet, int index)
			{
				this.sheetInstanceID = sheet.GetInstanceID();
				this.index = index;
			}

			// Token: 0x04000E21 RID: 3617
			public readonly int sheetInstanceID;

			// Token: 0x04000E22 RID: 3618
			public readonly int index;
		}

		// Token: 0x0200036D RID: 877
		private class SheetHandleKeyComparer : IEqualityComparer<StyleSheetCache.SheetHandleKey>
		{
			// Token: 0x06001C51 RID: 7249 RVA: 0x00085970 File Offset: 0x00083B70
			public bool Equals(StyleSheetCache.SheetHandleKey x, StyleSheetCache.SheetHandleKey y)
			{
				return x.sheetInstanceID == y.sheetInstanceID && x.index == y.index;
			}

			// Token: 0x06001C52 RID: 7250 RVA: 0x000859A4 File Offset: 0x00083BA4
			public int GetHashCode(StyleSheetCache.SheetHandleKey key)
			{
				return key.sheetInstanceID.GetHashCode() ^ key.index.GetHashCode();
			}

			// Token: 0x06001C53 RID: 7251 RVA: 0x000020C2 File Offset: 0x000002C2
			public SheetHandleKeyComparer()
			{
			}
		}
	}
}
