using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E7 RID: 231
	internal static class VisualElementDebugExtensions
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		public static string GetDisplayName(this VisualElement ve, bool withHashCode = true)
		{
			bool flag = ve == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text = ve.GetType().Name;
				bool flag2 = !string.IsNullOrEmpty(ve.name);
				if (flag2)
				{
					text = text + "#" + ve.name;
				}
				if (withHashCode)
				{
					text = text + " (" + ve.GetHashCode().ToString("x8") + ")";
				}
				result = text;
			}
			return result;
		}
	}
}
