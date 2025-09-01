using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000043 RID: 67
	internal static class TextUtilities
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0001C98C File Offset: 0x0001AB8C
		internal static void ResizeArray<T>(ref T[] array)
		{
			int newSize = TextUtilities.NextPowerOfTwo(array.Length);
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001C9AC File Offset: 0x0001ABAC
		internal static void ResizeArray<T>(ref T[] array, int size)
		{
			size = TextUtilities.NextPowerOfTwo(size);
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		internal static int NextPowerOfTwo(int v)
		{
			v |= v >> 16;
			v |= v >> 8;
			v |= v >> 4;
			v |= v >> 2;
			v |= v >> 1;
			return v + 1;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		internal static char ToLowerFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1;
			char result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001CA30 File Offset: 0x0001AC30
		internal static char ToUpperFast(char c)
		{
			bool flag = (int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1;
			char result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001CA64 File Offset: 0x0001AC64
		internal static uint ToUpperASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1));
			uint result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
			}
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		internal static uint ToLowerASCIIFast(uint c)
		{
			bool flag = (ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1));
			uint result;
			if (flag)
			{
				result = c;
			}
			else
			{
				result = (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
			}
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		public static int GetHashCodeCaseSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num ^ (int)s[i]);
			}
			return num;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001CB0C File Offset: 0x0001AD0C
		public static int GetHashCodeCaseInSensitive(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num ^ (int)TextUtilities.ToUpperFast(s[i]));
			}
			return num;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		public static uint GetSimpleHashCodeLowercase(string s)
		{
			uint num = 0U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num ^ (uint)TextUtilities.ToLowerFast(s[i]));
			}
			return num;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		internal static uint ConvertToUTF32(uint highSurrogate, uint lowSurrogate)
		{
			return (highSurrogate - 55296U) * 1024U + (lowSurrogate - 56320U + 65536U);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		internal static uint ReadUTF16(uint[] text, int index)
		{
			uint num = 0U;
			num += TextUtilities.HexToInt((char)text[index]) << 12;
			num += TextUtilities.HexToInt((char)text[index + 1]) << 8;
			num += TextUtilities.HexToInt((char)text[index + 2]) << 4;
			return num + TextUtilities.HexToInt((char)text[index + 3]);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001CC10 File Offset: 0x0001AE10
		internal static uint ReadUTF32(uint[] text, int index)
		{
			uint num = 0U;
			num += TextUtilities.HexToInt((char)text[index]) << 30;
			num += TextUtilities.HexToInt((char)text[index + 1]) << 24;
			num += TextUtilities.HexToInt((char)text[index + 2]) << 20;
			num += TextUtilities.HexToInt((char)text[index + 3]) << 16;
			num += TextUtilities.HexToInt((char)text[index + 4]) << 12;
			num += TextUtilities.HexToInt((char)text[index + 5]) << 8;
			num += TextUtilities.HexToInt((char)text[index + 6]) << 4;
			return num + TextUtilities.HexToInt((char)text[index + 7]);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		private static uint HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0U;
			case '1':
				return 1U;
			case '2':
				return 2U;
			case '3':
				return 3U;
			case '4':
				return 4U;
			case '5':
				return 5U;
			case '6':
				return 6U;
			case '7':
				return 7U;
			case '8':
				return 8U;
			case '9':
				return 9U;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10U;
			case 'B':
				return 11U;
			case 'C':
				return 12U;
			case 'D':
				return 13U;
			case 'E':
				return 14U;
			case 'F':
				return 15U;
			default:
				switch (hex)
				{
				case 'a':
					return 10U;
				case 'b':
					return 11U;
				case 'c':
					return 12U;
				case 'd':
					return 13U;
				case 'e':
					return 14U;
				case 'f':
					return 15U;
				}
				break;
			}
			return 15U;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001CDB0 File Offset: 0x0001AFB0
		public static uint StringHexToInt(string s)
		{
			uint num = 0U;
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				num += TextUtilities.HexToInt(s[i]) * (uint)Mathf.Pow(16f, (float)(length - 1 - i));
			}
			return num;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001CE00 File Offset: 0x0001B000
		internal static string UintToString(this List<uint> unicodes)
		{
			char[] array = new char[unicodes.Count];
			for (int i = 0; i < unicodes.Count; i++)
			{
				array[i] = (char)unicodes[i];
			}
			return new string(array);
		}

		// Token: 0x0400038B RID: 907
		private const string k_LookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		// Token: 0x0400038C RID: 908
		private const string k_LookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";
	}
}
