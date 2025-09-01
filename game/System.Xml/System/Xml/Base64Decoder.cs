using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000014 RID: 20
	internal class Base64Decoder : IncrementalReadDecoder
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A76 File Offset: 0x00000C76
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A85 File Offset: 0x00000C85
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A98 File Offset: 0x00000C98
		internal unsafe override int Decode(char[] chars, int startPos, int len)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (chars.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int result;
			int num;
			fixed (char* ptr = &chars[startPos])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &this.buffer[this.curIndex])
				{
					byte* ptr4 = ptr3;
					this.Decode(ptr2, ptr2 + len, ptr4, ptr4 + (this.endIndex - this.curIndex), out result, out num);
				}
			}
			this.curIndex += num;
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B48 File Offset: 0x00000D48
		internal unsafe override int Decode(string str, int startPos, int len)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (str.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int result;
			int num;
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (byte* ptr2 = &this.buffer[this.curIndex])
				{
					byte* ptr3 = ptr2;
					this.Decode(ptr + startPos, ptr + startPos + len, ptr3, ptr3 + (this.endIndex - this.curIndex), out result, out num);
				}
			}
			this.curIndex += num;
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C06 File Offset: 0x00000E06
		internal override void Reset()
		{
			this.bitsFilled = 0;
			this.bits = 0;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C16 File Offset: 0x00000E16
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (byte[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C3C File Offset: 0x00000E3C
		private static byte[] ConstructMapBase64()
		{
			byte[] array = new byte[123];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = byte.MaxValue;
			}
			for (int j = 0; j < Base64Decoder.CharsBase64.Length; j++)
			{
				array[(int)Base64Decoder.CharsBase64[j]] = (byte)j;
			}
			return array;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C8C File Offset: 0x00000E8C
		private unsafe void Decode(char* pChars, char* pCharsEndPos, byte* pBytes, byte* pBytesEndPos, out int charsDecoded, out int bytesDecoded)
		{
			byte* ptr = pBytes;
			char* ptr2 = pChars;
			int num = this.bits;
			int num2 = this.bitsFilled;
			XmlCharType instance = XmlCharType.Instance;
			while (ptr2 < pCharsEndPos && ptr < pBytesEndPos)
			{
				char c = *ptr2;
				if (c == '=')
				{
					break;
				}
				ptr2++;
				if ((instance.charProperties[(int)c] & 1) == 0)
				{
					int num3;
					if (c > 'z' || (num3 = (int)Base64Decoder.MapBase64[(int)c]) == 255)
					{
						throw new XmlException("'{0}' is not a valid Base64 text sequence.", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
					}
					num = (num << 6 | num3);
					num2 += 6;
					if (num2 >= 8)
					{
						*(ptr++) = (byte)(num >> num2 - 8 & 255);
						num2 -= 8;
						if (ptr == pBytesEndPos)
						{
							IL_EE:
							this.bits = num;
							this.bitsFilled = num2;
							bytesDecoded = (int)((long)(ptr - pBytes));
							charsDecoded = (int)((long)(ptr2 - pChars));
							return;
						}
					}
				}
			}
			if (ptr2 >= pCharsEndPos || *ptr2 != '=')
			{
				goto IL_EE;
			}
			num2 = 0;
			do
			{
				ptr2++;
			}
			while (ptr2 < pCharsEndPos && *ptr2 == '=');
			if (ptr2 < pCharsEndPos)
			{
				while ((instance.charProperties[(int)(*(ptr2++))] & 1) != 0)
				{
					if (ptr2 >= pCharsEndPos)
					{
						goto IL_EE;
					}
				}
				throw new XmlException("'{0}' is not a valid Base64 text sequence.", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
			}
			goto IL_EE;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public Base64Decoder()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DB1 File Offset: 0x00000FB1
		// Note: this type is marked as 'beforefieldinit'.
		static Base64Decoder()
		{
		}

		// Token: 0x040004E1 RID: 1249
		private byte[] buffer;

		// Token: 0x040004E2 RID: 1250
		private int startIndex;

		// Token: 0x040004E3 RID: 1251
		private int curIndex;

		// Token: 0x040004E4 RID: 1252
		private int endIndex;

		// Token: 0x040004E5 RID: 1253
		private int bits;

		// Token: 0x040004E6 RID: 1254
		private int bitsFilled;

		// Token: 0x040004E7 RID: 1255
		private static readonly string CharsBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x040004E8 RID: 1256
		private static readonly byte[] MapBase64 = Base64Decoder.ConstructMapBase64();

		// Token: 0x040004E9 RID: 1257
		private const int MaxValidChar = 122;

		// Token: 0x040004EA RID: 1258
		private const byte Invalid = 255;
	}
}
