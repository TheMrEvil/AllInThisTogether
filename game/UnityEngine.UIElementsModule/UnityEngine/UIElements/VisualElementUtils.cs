using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F3 RID: 243
	internal static class VisualElementUtils
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x0001BE64 File Offset: 0x0001A064
		public static string GetUniqueName(string nameBase)
		{
			string text = nameBase;
			int num = 2;
			while (VisualElementUtils.s_usedNames.Contains(text))
			{
				text = nameBase + num.ToString();
				num++;
			}
			VisualElementUtils.s_usedNames.Add(text);
			return text;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		internal static int GetFoldoutDepth(this VisualElement element)
		{
			int num = 0;
			bool flag = element.parent != null;
			if (flag)
			{
				for (VisualElement parent = element.parent; parent != null; parent = parent.parent)
				{
					bool flag2 = VisualElementUtils.s_FoldoutType.IsAssignableFrom(parent.GetType());
					if (flag2)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001BF08 File Offset: 0x0001A108
		internal static int GetListAndFoldoutDepth(this VisualElement element)
		{
			int num = 0;
			bool flag = element.hierarchy.parent != null;
			if (flag)
			{
				for (VisualElement parent = element.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
				{
					bool flag2 = parent is Foldout || parent is ListView;
					if (flag2)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001BF82 File Offset: 0x0001A182
		// Note: this type is marked as 'beforefieldinit'.
		static VisualElementUtils()
		{
		}

		// Token: 0x0400030B RID: 779
		private static readonly HashSet<string> s_usedNames = new HashSet<string>();

		// Token: 0x0400030C RID: 780
		private static readonly Type s_FoldoutType = typeof(Foldout);
	}
}
