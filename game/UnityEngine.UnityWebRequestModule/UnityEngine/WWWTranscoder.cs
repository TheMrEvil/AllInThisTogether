using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[VisibleToOtherModules(new string[]
	{
		"UnityEngine.UnityWebRequestWWWModule"
	})]
	internal class WWWTranscoder
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002B64 File Offset: 0x00000D64
		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte b2 = 0;
			for (int i = offset; i < offset + 2; i++)
			{
				b2 *= 16;
				int num = (int)b[i];
				bool flag = num >= 48 && num <= 57;
				if (flag)
				{
					num -= 48;
				}
				else
				{
					bool flag2 = num >= 65 && num <= 75;
					if (flag2)
					{
						num -= 55;
					}
					else
					{
						bool flag3 = num >= 97 && num <= 102;
						if (flag3)
						{
							num -= 87;
						}
					}
				}
				bool flag4 = num > 15;
				if (flag4)
				{
					return 63;
				}
				b2 += (byte)num;
			}
			return b2;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C09 File Offset: 0x00000E09
		private static void Byte2Hex(byte b, byte[] hexChars, out byte byte0, out byte byte1)
		{
			byte0 = hexChars[b >> 4];
			byte1 = hexChars[(int)(b & 15)];
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static string URLEncode(string toEncode)
		{
			return WWWTranscoder.URLEncode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static string URLEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C7C File Offset: 0x00000E7C
		public static byte[] URLEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public static string DataEncode(string toEncode)
		{
			return WWWTranscoder.DataEncode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public static string DataEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D04 File Offset: 0x00000F04
		public static byte[] DataEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static string QPEncode(string toEncode)
		{
			return WWWTranscoder.QPEncode(toEncode, Encoding.UTF8);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static string QPEncode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static byte[] QPEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public static byte[] Encode(byte[] input, byte escapeChar, byte[] space, byte[] forbidden, bool uppercase)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = input[i] == 32;
					if (flag)
					{
						memoryStream.Write(space, 0, space.Length);
					}
					else
					{
						bool flag2 = input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]);
						if (flag2)
						{
							memoryStream.WriteByte(escapeChar);
							byte value;
							byte value2;
							WWWTranscoder.Byte2Hex(input[i], uppercase ? WWWTranscoder.ucHexChars : WWWTranscoder.lcHexChars, out value, out value2);
							memoryStream.WriteByte(value);
							memoryStream.WriteByte(value2);
						}
						else
						{
							memoryStream.WriteByte(input[i]);
						}
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E94 File Offset: 0x00001094
		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = array[i] == b;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002ED0 File Offset: 0x000010D0
		public static string URLDecode(string toEncode)
		{
			return WWWTranscoder.URLDecode(toEncode, Encoding.UTF8);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static string URLDecode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002F28 File Offset: 0x00001128
		public static byte[] URLDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002F4C File Offset: 0x0000114C
		public static string DataDecode(string toDecode)
		{
			return WWWTranscoder.DataDecode(toDecode, Encoding.UTF8);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F6C File Offset: 0x0000116C
		public static string DataDecode(string toDecode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toDecode), WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002FA4 File Offset: 0x000011A4
		public static byte[] DataDecode(byte[] toDecode)
		{
			return WWWTranscoder.Decode(toDecode, WWWTranscoder.urlEscapeChar, WWWTranscoder.dataSpace);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002FC8 File Offset: 0x000011C8
		public static string QPDecode(string toEncode)
		{
			return WWWTranscoder.QPDecode(toEncode, Encoding.UTF8);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002FE8 File Offset: 0x000011E8
		public static string QPDecode(string toEncode, Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003020 File Offset: 0x00001220
		public static byte[] QPDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003044 File Offset: 0x00001244
		private static bool ByteSubArrayEquals(byte[] array, int index, byte[] comperand)
		{
			bool flag = array.Length - index < comperand.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < comperand.Length; i++)
				{
					bool flag2 = array[index + i] != comperand[i];
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003094 File Offset: 0x00001294
		public static byte[] Decode(byte[] input, byte escapeChar, byte[] space)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input.Length))
			{
				for (int i = 0; i < input.Length; i++)
				{
					bool flag = WWWTranscoder.ByteSubArrayEquals(input, i, space);
					if (flag)
					{
						i += space.Length - 1;
						memoryStream.WriteByte(32);
					}
					else
					{
						bool flag2 = input[i] == escapeChar && i + 2 < input.Length;
						if (flag2)
						{
							i++;
							memoryStream.WriteByte(WWWTranscoder.Hex2Byte(input, i++));
						}
						else
						{
							memoryStream.WriteByte(input[i]);
						}
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003148 File Offset: 0x00001348
		public static bool SevenBitClean(string s)
		{
			return WWWTranscoder.SevenBitClean(s, Encoding.UTF8);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003168 File Offset: 0x00001368
		public unsafe static bool SevenBitClean(string s, Encoding e)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				int num = s.Length * 2;
				byte* ptr = stackalloc byte[(UIntPtr)num];
				int bytes;
				fixed (string text = s)
				{
					char* ptr2 = text;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					bytes = e.GetBytes(ptr2, s.Length, ptr, num);
				}
				result = WWWTranscoder.SevenBitClean(ptr, bytes);
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000031D0 File Offset: 0x000013D0
		public unsafe static bool SevenBitClean(byte* input, int inputLength)
		{
			for (int i = 0; i < inputLength; i++)
			{
				bool flag = input[i] < 32 || input[i] > 126;
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003210 File Offset: 0x00001410
		public WWWTranscoder()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000321C File Offset: 0x0000141C
		// Note: this type is marked as 'beforefieldinit'.
		static WWWTranscoder()
		{
		}

		// Token: 0x04000010 RID: 16
		private static byte[] ucHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x04000011 RID: 17
		private static byte[] lcHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789abcdef");

		// Token: 0x04000012 RID: 18
		private static byte urlEscapeChar = 37;

		// Token: 0x04000013 RID: 19
		private static byte[] urlSpace = new byte[]
		{
			43
		};

		// Token: 0x04000014 RID: 20
		private static byte[] dataSpace = WWWForm.DefaultEncoding.GetBytes("%20");

		// Token: 0x04000015 RID: 21
		private static byte[] urlForbidden = WWWForm.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		// Token: 0x04000016 RID: 22
		private static byte qpEscapeChar = 61;

		// Token: 0x04000017 RID: 23
		private static byte[] qpSpace = new byte[]
		{
			95
		};

		// Token: 0x04000018 RID: 24
		private static byte[] qpForbidden = WWWForm.DefaultEncoding.GetBytes("&;=?\"'%+_");
	}
}
