using System;
using System.Collections.Concurrent;
using System.Text;
using QFSW.QC.Pooling;
using UnityEngine;

namespace QFSW.QC.Utilities
{
	// Token: 0x02000053 RID: 83
	public static class ColorExtensions
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public static string ColorText(this string text, Color color)
		{
			StringBuilder stringBuilder = ColorExtensions._stringBuilderPool.GetStringBuilder(text.Length + 10);
			stringBuilder.AppendColoredText(text, color);
			return ColorExtensions._stringBuilderPool.ReleaseAndToString(stringBuilder);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public static void AppendColoredText(this StringBuilder stringBuilder, string text, Color color)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				stringBuilder.Append(text);
			}
			string value = ColorExtensions.Color32ToStringNonAlloc(color);
			stringBuilder.Append("<#");
			stringBuilder.Append(value);
			stringBuilder.Append('>');
			stringBuilder.Append(text);
			stringBuilder.Append("</color>");
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008D44 File Offset: 0x00006F44
		public unsafe static string Color32ToStringNonAlloc(Color32 color)
		{
			int key = (int)color.r << 24 | (int)color.g << 16 | (int)color.b << 8 | (int)color.a;
			if (ColorExtensions._colorLookupTable.ContainsKey(key))
			{
				return ColorExtensions._colorLookupTable[key];
			}
			char* ptr = stackalloc char[(UIntPtr)16];
			ColorExtensions.Color32ToHexNonAlloc(color, ptr);
			int length = (color.a < byte.MaxValue) ? 8 : 6;
			string text = new string(ptr, 0, length);
			ColorExtensions._colorLookupTable[key] = text;
			return text;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008DC4 File Offset: 0x00006FC4
		private unsafe static void Color32ToHexNonAlloc(Color32 color, char* buffer)
		{
			ColorExtensions.ByteToHex(color.r, out *buffer, out buffer[1]);
			ColorExtensions.ByteToHex(color.g, out buffer[2], out buffer[3]);
			ColorExtensions.ByteToHex(color.b, out buffer[4], out buffer[5]);
			ColorExtensions.ByteToHex(color.a, out buffer[6], out buffer[7]);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008E25 File Offset: 0x00007025
		private static void ByteToHex(byte value, out char dig1, out char dig2)
		{
			dig1 = ColorExtensions.NibbleToHex((byte)(value >> 4));
			dig2 = ColorExtensions.NibbleToHex(value & 15);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008E3E File Offset: 0x0000703E
		private static char NibbleToHex(byte nibble)
		{
			if (nibble < 10)
			{
				return (char)(48 + nibble);
			}
			return (char)(65 + nibble - 10);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008E53 File Offset: 0x00007053
		// Note: this type is marked as 'beforefieldinit'.
		static ColorExtensions()
		{
		}

		// Token: 0x04000133 RID: 307
		private static readonly ConcurrentStringBuilderPool _stringBuilderPool = new ConcurrentStringBuilderPool();

		// Token: 0x04000134 RID: 308
		private static readonly ConcurrentDictionary<int, string> _colorLookupTable = new ConcurrentDictionary<int, string>();
	}
}
