using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000005 RID: 5
	public class ColorParser : BasicCachedQcParser<Color>
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022D0 File Offset: 0x000004D0
		public ColorParser()
		{
			this._colorLookup = new Dictionary<string, Color>();
			foreach (PropertyInfo propertyInfo in typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
			{
				if (propertyInfo.CanRead && !propertyInfo.CanWrite)
				{
					MethodInfo getMethod = propertyInfo.GetMethod;
					if (getMethod.ReturnType == typeof(Color))
					{
						this._colorLookup.Add(propertyInfo.Name, (Color)getMethod.Invoke(null, Array.Empty<object>()));
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002364 File Offset: 0x00000564
		public override Color Parse(string value)
		{
			if (this._colorLookup.ContainsKey(value.ToLower()))
			{
				return this._colorLookup[value.ToLower()];
			}
			Color result;
			try
			{
				if (value.StartsWith("0x"))
				{
					result = this.ParseHexColor(value);
				}
				else
				{
					result = this.ParseRGBAColor(value);
				}
			}
			catch (FormatException ex)
			{
				throw new ParserInputException(ex.Message + "\nThe format must be either of:\n   - R,G,B\n   - R,G,B,A\n   - 0xRRGGBB\n   - 0xRRGGBBAA\n   - A preset color such as 'red'", ex);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E4 File Offset: 0x000005E4
		private Color ParseRGBAColor(string value)
		{
			string[] array = value.Split(',', StringSplitOptions.None);
			Color white = Color.white;
			int i = 0;
			if (array.Length < 3 || array.Length > 4)
			{
				throw new FormatException("Cannot parse '" + value + "' as a Color.");
			}
			Color result;
			try
			{
				while (i < array.Length)
				{
					white[i] = ColorParser.<ParseRGBAColor>g__ParsePart|3_0(array[i]);
					i++;
				}
				result = white;
			}
			catch (FormatException)
			{
				throw new FormatException("Cannot parse '" + array[i] + "' as part of a Color, it must be numerical and in the valid range [0,1].");
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002470 File Offset: 0x00000670
		private Color ParseHexColor(string value)
		{
			int num = value.Length - 2;
			if (num != 6 && num != 8)
			{
				throw new FormatException("Hex colors must contain either 6 or 8 hex digits.");
			}
			Color white = Color.white;
			int num2 = num / 2;
			int i = 0;
			Color result;
			try
			{
				while (i < num2)
				{
					white[i] = (float)int.Parse(value.Substring(2 * (1 + i), 2), NumberStyles.HexNumber) / 255f;
					i++;
				}
				result = white;
			}
			catch (FormatException)
			{
				throw new FormatException("Cannot parse '" + value.Substring(2 * (1 + i), 2) + "' as part of a Color as it was invalid hex.");
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002510 File Offset: 0x00000710
		[CompilerGenerated]
		internal static float <ParseRGBAColor>g__ParsePart|3_0(string part)
		{
			float num = float.Parse(part);
			if (num < 0f || num > 1f)
			{
				throw new FormatException(string.Format("{0} falls outside of the valid [0,1] range for a component of a Color.", num));
			}
			return num;
		}

		// Token: 0x04000002 RID: 2
		private readonly Dictionary<string, Color> _colorLookup;
	}
}
