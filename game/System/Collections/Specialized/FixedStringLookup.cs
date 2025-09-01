using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020004A1 RID: 1185
	internal static class FixedStringLookup
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x000856BC File Offset: 0x000838BC
		internal static bool Contains(string[][] lookupTable, string value, bool ignoreCase)
		{
			int length = value.Length;
			if (length <= 0 || length - 1 >= lookupTable.Length)
			{
				return false;
			}
			string[] array = lookupTable[length - 1];
			return array != null && FixedStringLookup.Contains(array, value, ignoreCase);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000856F4 File Offset: 0x000838F4
		private static bool Contains(string[] array, string value, bool ignoreCase)
		{
			int num = 0;
			int num2 = array.Length;
			int i = 0;
			while (i < value.Length)
			{
				char c;
				if (ignoreCase)
				{
					c = char.ToLower(value[i], CultureInfo.InvariantCulture);
				}
				else
				{
					c = value[i];
				}
				if (num2 - num <= 1)
				{
					if (c != array[num][i])
					{
						return false;
					}
					i++;
				}
				else
				{
					if (!FixedStringLookup.FindCharacter(array, c, i, ref num, ref num2))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00085764 File Offset: 0x00083964
		private static bool FindCharacter(string[] array, char value, int pos, ref int min, ref int max)
		{
			int num = min;
			while (min < max)
			{
				num = (min + max) / 2;
				char c = array[num][pos];
				if (value == c)
				{
					int num2 = num;
					while (num2 > min && array[num2 - 1][pos] == value)
					{
						num2--;
					}
					min = num2;
					int num3 = num + 1;
					while (num3 < max && array[num3][pos] == value)
					{
						num3++;
					}
					max = num3;
					return true;
				}
				if (value < c)
				{
					max = num;
				}
				else
				{
					min = num + 1;
				}
			}
			return false;
		}
	}
}
