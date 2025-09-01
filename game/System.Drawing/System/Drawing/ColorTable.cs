using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x0200000F RID: 15
	internal static class ColorTable
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002469 File Offset: 0x00000669
		private static Dictionary<string, Color> GetColors()
		{
			Dictionary<string, Color> dictionary = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
			ColorTable.FillConstants(dictionary, typeof(Color));
			ColorTable.FillConstants(dictionary, typeof(SystemColors));
			return dictionary;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002495 File Offset: 0x00000695
		internal static Dictionary<string, Color> Colors
		{
			get
			{
				return ColorTable.s_colorConstants.Value;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024A4 File Offset: 0x000006A4
		private static void FillConstants(Dictionary<string, Color> colors, Type enumType)
		{
			foreach (PropertyInfo propertyInfo in enumType.GetProperties())
			{
				if (propertyInfo.PropertyType == typeof(Color))
				{
					colors[propertyInfo.Name] = (Color)propertyInfo.GetValue(null, null);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024FA File Offset: 0x000006FA
		internal static bool TryGetNamedColor(string name, out Color result)
		{
			return ColorTable.Colors.TryGetValue(name, out result);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002508 File Offset: 0x00000708
		internal static bool IsKnownNamedColor(string name)
		{
			Color color;
			return ColorTable.Colors.TryGetValue(name, out color);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002522 File Offset: 0x00000722
		// Note: this type is marked as 'beforefieldinit'.
		static ColorTable()
		{
		}

		// Token: 0x04000092 RID: 146
		private static readonly Lazy<Dictionary<string, Color>> s_colorConstants = new Lazy<Dictionary<string, Color>>(new Func<Dictionary<string, Color>>(ColorTable.GetColors));
	}
}
