using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;

namespace System.Runtime
{
	// Token: 0x02000038 RID: 56
	internal static class UrlUtility
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00007613 File Offset: 0x00005813
		public static NameValueCollection ParseQueryString(string query)
		{
			return UrlUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007620 File Offset: 0x00005820
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
			{
				throw Fx.Exception.ArgumentNull("query");
			}
			if (encoding == null)
			{
				throw Fx.Exception.ArgumentNull("encoding");
			}
			if (query.Length > 0 && query[0] == '?')
			{
				query = query.Substring(1);
			}
			return new UrlUtility.HttpValueCollection(query, encoding);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007677 File Offset: 0x00005877
		public static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UrlUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000768C File Offset: 0x0000588C
		public static string UrlPathEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			int num = str.IndexOf('?');
			if (num >= 0)
			{
				return UrlUtility.UrlPathEncode(str.Substring(0, num)) + str.Substring(num);
			}
			return UrlUtility.UrlEncodeSpaces(UrlUtility.UrlEncodeNonAscii(str, Encoding.UTF8));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000076D5 File Offset: 0x000058D5
		public static string UrlEncode(string str, Encoding encoding)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(UrlUtility.UrlEncodeToBytes(str, encoding));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000076ED File Offset: 0x000058ED
		public static string UrlEncodeUnicode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return UrlUtility.UrlEncodeUnicodeStringToStringInternal(str, false);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000076FC File Offset: 0x000058FC
		private static string UrlEncodeUnicodeStringToStringInternal(string s, bool ignoreAscii)
		{
			int length = s.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if ((c & 'ﾀ') == '\0')
				{
					if (ignoreAscii || UrlUtility.IsSafe(c))
					{
						stringBuilder.Append(c);
					}
					else if (c == ' ')
					{
						stringBuilder.Append('+');
					}
					else
					{
						stringBuilder.Append('%');
						stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 4 & '\u000f')));
						stringBuilder.Append(UrlUtility.IntToHex((int)(c & '\u000f')));
					}
				}
				else
				{
					stringBuilder.Append("%u");
					stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 12 & '\u000f')));
					stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 8 & '\u000f')));
					stringBuilder.Append(UrlUtility.IntToHex((int)(c >> 4 & '\u000f')));
					stringBuilder.Append(UrlUtility.IntToHex((int)(c & '\u000f')));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000077EC File Offset: 0x000059EC
		private static string UrlEncodeNonAscii(string str, Encoding e)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			byte[] array = e.GetBytes(str);
			array = UrlUtility.UrlEncodeBytesToBytesInternalNonAscii(array, 0, array.Length, false);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000782C File Offset: 0x00005A2C
		private static string UrlEncodeSpaces(string str)
		{
			if (str != null && str.IndexOf(' ') >= 0)
			{
				str = str.Replace(" ", "%20");
			}
			return str;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007850 File Offset: 0x00005A50
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return UrlUtility.UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007875 File Offset: 0x00005A75
		public static string UrlDecode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return UrlUtility.UrlDecodeStringFromStringInternal(str, e);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007884 File Offset: 0x00005A84
		private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!UrlUtility.IsSafe(c))
				{
					num2++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (UrlUtility.IsSafe(c2))
				{
					array[num3++] = b;
				}
				else if (c2 == ' ')
				{
					array[num3++] = 43;
				}
				else
				{
					array[num3++] = 37;
					array[num3++] = (byte)UrlUtility.IntToHex(b >> 4 & 15);
					array[num3++] = (byte)UrlUtility.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000794F File Offset: 0x00005B4F
		private static bool IsNonAsciiByte(byte b)
		{
			return b >= 127 || b < 32;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007960 File Offset: 0x00005B60
		private static byte[] UrlEncodeBytesToBytesInternalNonAscii(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (UrlUtility.IsNonAsciiByte(bytes[offset + i]))
				{
					num++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num * 2];
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				if (UrlUtility.IsNonAsciiByte(b))
				{
					array[num2++] = 37;
					array[num2++] = (byte)UrlUtility.IntToHex(b >> 4 & 15);
					array[num2++] = (byte)UrlUtility.IntToHex((int)(b & 15));
				}
				else
				{
					array[num2++] = b;
				}
			}
			return array;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000079FC File Offset: 0x00005BFC
		private static string UrlDecodeStringFromStringInternal(string s, Encoding e)
		{
			int length = s.Length;
			UrlUtility.UrlDecoder urlDecoder = new UrlUtility.UrlDecoder(length, e);
			int i = 0;
			while (i < length)
			{
				char c = s[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_106;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_106;
				}
				if (s[i + 1] == 'u' && i < length - 5)
				{
					int num = UrlUtility.HexToInt(s[i + 2]);
					int num2 = UrlUtility.HexToInt(s[i + 3]);
					int num3 = UrlUtility.HexToInt(s[i + 4]);
					int num4 = UrlUtility.HexToInt(s[i + 5]);
					if (num < 0 || num2 < 0 || num3 < 0 || num4 < 0)
					{
						goto IL_106;
					}
					c = (char)(num << 12 | num2 << 8 | num3 << 4 | num4);
					i += 5;
					urlDecoder.AddChar(c);
				}
				else
				{
					int num5 = UrlUtility.HexToInt(s[i + 1]);
					int num6 = UrlUtility.HexToInt(s[i + 2]);
					if (num5 < 0 || num6 < 0)
					{
						goto IL_106;
					}
					byte b = (byte)(num5 << 4 | num6);
					i += 2;
					urlDecoder.AddByte(b);
				}
				IL_120:
				i++;
				continue;
				IL_106:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_120;
				}
				urlDecoder.AddChar(c);
				goto IL_120;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007B3A File Offset: 0x00005D3A
		private static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007B70 File Offset: 0x00005D70
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007B88 File Offset: 0x00005D88
		internal static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0200008A RID: 138
		private class UrlDecoder
		{
			// Token: 0x06000401 RID: 1025 RVA: 0x00012C14 File Offset: 0x00010E14
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x00012C62 File Offset: 0x00010E62
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x00012C84 File Offset: 0x00010E84
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x00012CBC File Offset: 0x00010EBC
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x00012CFB File Offset: 0x00010EFB
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._numChars > 0)
				{
					return new string(this._charBuffer, 0, this._numChars);
				}
				return string.Empty;
			}

			// Token: 0x040002D6 RID: 726
			private int _bufferSize;

			// Token: 0x040002D7 RID: 727
			private int _numChars;

			// Token: 0x040002D8 RID: 728
			private char[] _charBuffer;

			// Token: 0x040002D9 RID: 729
			private int _numBytes;

			// Token: 0x040002DA RID: 730
			private byte[] _byteBuffer;

			// Token: 0x040002DB RID: 731
			private Encoding _encoding;
		}

		// Token: 0x0200008B RID: 139
		[Serializable]
		private class HttpValueCollection : NameValueCollection
		{
			// Token: 0x06000406 RID: 1030 RVA: 0x00012D2D File Offset: 0x00010F2D
			internal HttpValueCollection(string str, Encoding encoding) : base(StringComparer.OrdinalIgnoreCase)
			{
				if (!string.IsNullOrEmpty(str))
				{
					this.FillFromString(str, true, encoding);
				}
				base.IsReadOnly = false;
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x00012D52 File Offset: 0x00010F52
			protected HttpValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x00012D5C File Offset: 0x00010F5C
			internal void FillFromString(string s, bool urlencoded, Encoding encoding)
			{
				int num = (s != null) ? s.Length : 0;
				for (int i = 0; i < num; i++)
				{
					int num2 = i;
					int num3 = -1;
					while (i < num)
					{
						char c = s[i];
						if (c == '=')
						{
							if (num3 < 0)
							{
								num3 = i;
							}
						}
						else if (c == '&')
						{
							break;
						}
						i++;
					}
					string text = null;
					string text2;
					if (num3 >= 0)
					{
						text = s.Substring(num2, num3 - num2);
						text2 = s.Substring(num3 + 1, i - num3 - 1);
					}
					else
					{
						text2 = s.Substring(num2, i - num2);
					}
					if (urlencoded)
					{
						base.Add(UrlUtility.UrlDecode(text, encoding), UrlUtility.UrlDecode(text2, encoding));
					}
					else
					{
						base.Add(text, text2);
					}
					if (i == num - 1 && s[i] == '&')
					{
						base.Add(null, string.Empty);
					}
				}
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x00012E29 File Offset: 0x00011029
			public override string ToString()
			{
				return this.ToString(true, null);
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x00012E34 File Offset: 0x00011034
			private string ToString(bool urlencoded, IDictionary excludeKeys)
			{
				int count = this.Count;
				if (count == 0)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < count; i++)
				{
					string text = this.GetKey(i);
					if (excludeKeys == null || text == null || excludeKeys[text] == null)
					{
						if (urlencoded)
						{
							text = UrlUtility.UrlEncodeUnicode(text);
						}
						string value = (!string.IsNullOrEmpty(text)) ? (text + "=") : string.Empty;
						ArrayList arrayList = (ArrayList)base.BaseGet(i);
						int num = (arrayList != null) ? arrayList.Count : 0;
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append('&');
						}
						if (num == 1)
						{
							stringBuilder.Append(value);
							string text2 = (string)arrayList[0];
							if (urlencoded)
							{
								text2 = UrlUtility.UrlEncodeUnicode(text2);
							}
							stringBuilder.Append(text2);
						}
						else if (num == 0)
						{
							stringBuilder.Append(value);
						}
						else
						{
							for (int j = 0; j < num; j++)
							{
								if (j > 0)
								{
									stringBuilder.Append('&');
								}
								stringBuilder.Append(value);
								string text2 = (string)arrayList[j];
								if (urlencoded)
								{
									text2 = UrlUtility.UrlEncodeUnicode(text2);
								}
								stringBuilder.Append(text2);
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
		}
	}
}
