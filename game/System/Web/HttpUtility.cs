using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020001DF RID: 479
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpUtility
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0000219B File Offset: 0x0000039B
		public HttpUtility()
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003288C File Offset: 0x00030A8C
		public static void HtmlAttributeEncode(string s, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			HttpEncoder.Current.HtmlAttributeEncode(s, output);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000328A8 File Offset: 0x00030AA8
		public static string HtmlAttributeEncode(string s)
		{
			if (s == null)
			{
				return null;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				HttpEncoder.Current.HtmlAttributeEncode(s, stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000328F0 File Offset: 0x00030AF0
		public static string UrlDecode(string str)
		{
			return HttpUtility.UrlDecode(str, Encoding.UTF8);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000328FD File Offset: 0x00030AFD
		private static char[] GetChars(MemoryStream b, Encoding e)
		{
			return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00032914 File Offset: 0x00030B14
		private static void WriteCharBytes(IList buf, char ch, Encoding e)
		{
			if (ch > 'ÿ')
			{
				foreach (byte b in e.GetBytes(new char[]
				{
					ch
				}))
				{
					buf.Add(b);
				}
				return;
			}
			buf.Add((byte)ch);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00032968 File Offset: 0x00030B68
		public static string UrlDecode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOf('%') == -1 && str.IndexOf('+') == -1)
			{
				return str;
			}
			if (e == null)
			{
				e = Encoding.UTF8;
			}
			long num = (long)str.Length;
			List<byte> list = new List<byte>();
			int num2 = 0;
			while ((long)num2 < num)
			{
				char c = str[num2];
				if (c == '%' && (long)(num2 + 2) < num && str[num2 + 1] != '%')
				{
					int @char;
					if (str[num2 + 1] == 'u' && (long)(num2 + 5) < num)
					{
						@char = HttpUtility.GetChar(str, num2 + 2, 4);
						if (@char != -1)
						{
							HttpUtility.WriteCharBytes(list, (char)@char, e);
							num2 += 5;
						}
						else
						{
							HttpUtility.WriteCharBytes(list, '%', e);
						}
					}
					else if ((@char = HttpUtility.GetChar(str, num2 + 1, 2)) != -1)
					{
						HttpUtility.WriteCharBytes(list, (char)@char, e);
						num2 += 2;
					}
					else
					{
						HttpUtility.WriteCharBytes(list, '%', e);
					}
				}
				else if (c == '+')
				{
					HttpUtility.WriteCharBytes(list, ' ', e);
				}
				else
				{
					HttpUtility.WriteCharBytes(list, c, e);
				}
				num2++;
			}
			byte[] bytes = list.ToArray();
			return e.GetString(bytes);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00032A80 File Offset: 0x00030C80
		public static string UrlDecode(byte[] bytes, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlDecode(bytes, 0, bytes.Length, e);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00032A94 File Offset: 0x00030C94
		private static int GetInt(byte b)
		{
			if (b >= 48 && b <= 57)
			{
				return (int)(b - 48);
			}
			if (b >= 97 && b <= 102)
			{
				return (int)(b - 97 + 10);
			}
			if (b >= 65 && b <= 70)
			{
				return (int)(b - 65 + 10);
			}
			return -1;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00032AD8 File Offset: 0x00030CD8
		private static int GetChar(byte[] bytes, int offset, int length)
		{
			int num = 0;
			int num2 = length + offset;
			for (int i = offset; i < num2; i++)
			{
				int @int = HttpUtility.GetInt(bytes[i]);
				if (@int == -1)
				{
					return -1;
				}
				num = (num << 4) + @int;
			}
			return num;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00032B10 File Offset: 0x00030D10
		private static int GetChar(string str, int offset, int length)
		{
			int num = 0;
			int num2 = length + offset;
			for (int i = offset; i < num2; i++)
			{
				char c = str[i];
				if (c > '\u007f')
				{
					return -1;
				}
				int @int = HttpUtility.GetInt((byte)c);
				if (@int == -1)
				{
					return -1;
				}
				num = (num << 4) + @int;
			}
			return num;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00032B58 File Offset: 0x00030D58
		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
		{
			if (bytes == null)
			{
				return null;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			StringBuilder stringBuilder = new StringBuilder();
			MemoryStream memoryStream = new MemoryStream();
			int num = count + offset;
			int i = offset;
			while (i < num)
			{
				if (bytes[i] != 37 || i + 2 >= count || bytes[i + 1] == 37)
				{
					goto IL_EE;
				}
				if (bytes[i + 1] == 117 && i + 5 < num)
				{
					if (memoryStream.Length > 0L)
					{
						stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
						memoryStream.SetLength(0L);
					}
					int @char = HttpUtility.GetChar(bytes, i + 2, 4);
					if (@char == -1)
					{
						goto IL_EE;
					}
					stringBuilder.Append((char)@char);
					i += 5;
				}
				else
				{
					int @char;
					if ((@char = HttpUtility.GetChar(bytes, i + 1, 2)) == -1)
					{
						goto IL_EE;
					}
					memoryStream.WriteByte((byte)@char);
					i += 2;
				}
				IL_12C:
				i++;
				continue;
				IL_EE:
				if (memoryStream.Length > 0L)
				{
					stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
					memoryStream.SetLength(0L);
				}
				if (bytes[i] == 43)
				{
					stringBuilder.Append(' ');
					goto IL_12C;
				}
				stringBuilder.Append((char)bytes[i]);
				goto IL_12C;
			}
			if (memoryStream.Length > 0L)
			{
				stringBuilder.Append(HttpUtility.GetChars(memoryStream, e));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00032CBF File Offset: 0x00030EBF
		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpUtility.UrlDecodeToBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00032CD0 File Offset: 0x00030ED0
		public static byte[] UrlDecodeToBytes(string str)
		{
			return HttpUtility.UrlDecodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00032CDD File Offset: 0x00030EDD
		public static byte[] UrlDecodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return HttpUtility.UrlDecodeToBytes(e.GetBytes(str));
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00032D00 File Offset: 0x00030F00
		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			if (count == 0)
			{
				return new byte[0];
			}
			int num = bytes.Length;
			if (offset < 0 || offset >= num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset > num - count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			MemoryStream memoryStream = new MemoryStream();
			int num2 = offset + count;
			for (int i = offset; i < num2; i++)
			{
				char c = (char)bytes[i];
				if (c == '+')
				{
					c = ' ';
				}
				else if (c == '%' && i < num2 - 2)
				{
					int @char = HttpUtility.GetChar(bytes, i + 1, 2);
					if (@char != -1)
					{
						c = (char)@char;
						i += 2;
					}
				}
				memoryStream.WriteByte((byte)c);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00032DA4 File Offset: 0x00030FA4
		public static string UrlEncode(string str)
		{
			return HttpUtility.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00032DB4 File Offset: 0x00030FB4
		public static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (str == string.Empty)
			{
				return string.Empty;
			}
			bool flag = false;
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if ((c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z') && !HttpEncoder.NotEncoded(c))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return str;
			}
			byte[] bytes = new byte[e.GetMaxByteCount(str.Length)];
			int bytes2 = e.GetBytes(str, 0, str.Length, bytes, 0);
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes, 0, bytes2));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00032E67 File Offset: 0x00031067
		public static string UrlEncode(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length));
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00032E8C File Offset: 0x0003108C
		public static string UrlEncode(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(bytes, offset, count));
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00032EAF File Offset: 0x000310AF
		public static byte[] UrlEncodeToBytes(string str)
		{
			return HttpUtility.UrlEncodeToBytes(str, Encoding.UTF8);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00032EBC File Offset: 0x000310BC
		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			if (str.Length == 0)
			{
				return new byte[0];
			}
			byte[] bytes = e.GetBytes(str);
			return HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00032EEF File Offset: 0x000310EF
		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			if (bytes.Length == 0)
			{
				return new byte[0];
			}
			return HttpUtility.UrlEncodeToBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00032F0B File Offset: 0x0003110B
		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				return null;
			}
			return HttpEncoder.Current.UrlEncode(bytes, offset, count);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00032F1F File Offset: 0x0003111F
		public static string UrlEncodeUnicode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(HttpUtility.UrlEncodeUnicodeToBytes(str));
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00032F38 File Offset: 0x00031138
		public static byte[] UrlEncodeUnicodeToBytes(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.Length == 0)
			{
				return new byte[0];
			}
			MemoryStream memoryStream = new MemoryStream(str.Length);
			for (int i = 0; i < str.Length; i++)
			{
				HttpEncoder.UrlEncodeChar(str[i], memoryStream, true);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00032F8C File Offset: 0x0003118C
		public static string HtmlDecode(string s)
		{
			if (s == null)
			{
				return null;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				HttpEncoder.Current.HtmlDecode(s, stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00032FD4 File Offset: 0x000311D4
		public static void HtmlDecode(string s, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (!string.IsNullOrEmpty(s))
			{
				HttpEncoder.Current.HtmlDecode(s, output);
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00032FF8 File Offset: 0x000311F8
		public static string HtmlEncode(string s)
		{
			if (s == null)
			{
				return null;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				HttpEncoder.Current.HtmlEncode(s, stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033040 File Offset: 0x00031240
		public static void HtmlEncode(string s, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (!string.IsNullOrEmpty(s))
			{
				HttpEncoder.Current.HtmlEncode(s, output);
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00033064 File Offset: 0x00031264
		public static string HtmlEncode(object value)
		{
			if (value == null)
			{
				return null;
			}
			return HttpUtility.HtmlEncode(value.ToString());
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00033076 File Offset: 0x00031276
		public static string JavaScriptStringEncode(string value)
		{
			return HttpUtility.JavaScriptStringEncode(value, false);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00033080 File Offset: 0x00031280
		public static string JavaScriptStringEncode(string value, bool addDoubleQuotes)
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!addDoubleQuotes)
				{
					return string.Empty;
				}
				return "\"\"";
			}
			else
			{
				int length = value.Length;
				bool flag = false;
				for (int i = 0; i < length; i++)
				{
					char c = value[i];
					if ((c >= '\0' && c <= '\u001f') || c == '"' || c == '\'' || c == '<' || c == '>' || c == '\\')
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (addDoubleQuotes)
					{
						stringBuilder.Append('"');
					}
					for (int j = 0; j < length; j++)
					{
						char c = value[j];
						if ((c >= '\0' && c <= '\a') || (c == '\v' || (c >= '\u000e' && c <= '\u001f')) || c == '\'' || c == '<' || c == '>')
						{
							stringBuilder.AppendFormat("\\u{0:x4}", (int)c);
						}
						else
						{
							int num = (int)c;
							switch (num)
							{
							case 8:
								stringBuilder.Append("\\b");
								goto IL_174;
							case 9:
								stringBuilder.Append("\\t");
								goto IL_174;
							case 10:
								stringBuilder.Append("\\n");
								goto IL_174;
							case 11:
								break;
							case 12:
								stringBuilder.Append("\\f");
								goto IL_174;
							case 13:
								stringBuilder.Append("\\r");
								goto IL_174;
							default:
								if (num == 34)
								{
									stringBuilder.Append("\\\"");
									goto IL_174;
								}
								if (num == 92)
								{
									stringBuilder.Append("\\\\");
									goto IL_174;
								}
								break;
							}
							stringBuilder.Append(c);
						}
						IL_174:;
					}
					if (addDoubleQuotes)
					{
						stringBuilder.Append('"');
					}
					return stringBuilder.ToString();
				}
				if (!addDoubleQuotes)
				{
					return value;
				}
				return "\"" + value + "\"";
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00033221 File Offset: 0x00031421
		public static string UrlPathEncode(string str)
		{
			return HttpEncoder.Current.UrlPathEncode(str);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003322E File Offset: 0x0003142E
		public static NameValueCollection ParseQueryString(string query)
		{
			return HttpUtility.ParseQueryString(query, Encoding.UTF8);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0003323C File Offset: 0x0003143C
		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
			{
				return new HttpUtility.HttpQSCollection();
			}
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			NameValueCollection result = new HttpUtility.HttpQSCollection();
			HttpUtility.ParseQueryString(query, encoding, result);
			return result;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000332AC File Offset: 0x000314AC
		internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
		{
			if (query.Length == 0)
			{
				return;
			}
			string text = HttpUtility.HtmlDecode(query);
			int length = text.Length;
			int i = 0;
			bool flag = true;
			while (i <= length)
			{
				int num = -1;
				int num2 = -1;
				for (int j = i; j < length; j++)
				{
					if (num == -1 && text[j] == '=')
					{
						num = j + 1;
					}
					else if (text[j] == '&')
					{
						num2 = j;
						break;
					}
				}
				if (flag)
				{
					flag = false;
					if (text[i] == '?')
					{
						i++;
					}
				}
				string name;
				if (num == -1)
				{
					name = null;
					num = i;
				}
				else
				{
					name = HttpUtility.UrlDecode(text.Substring(i, num - i - 1), encoding);
				}
				if (num2 < 0)
				{
					i = -1;
					num2 = text.Length;
				}
				else
				{
					i = num2 + 1;
				}
				string value = HttpUtility.UrlDecode(text.Substring(num, num2 - num), encoding);
				result.Add(name, value);
				if (i == -1)
				{
					break;
				}
			}
		}

		// Token: 0x020001E0 RID: 480
		private sealed class HttpQSCollection : NameValueCollection
		{
			// Token: 0x06000C89 RID: 3209 RVA: 0x00033390 File Offset: 0x00031590
			public override string ToString()
			{
				int count = this.Count;
				if (count == 0)
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				string[] allKeys = this.AllKeys;
				for (int i = 0; i < count; i++)
				{
					stringBuilder.AppendFormat("{0}={1}&", allKeys[i], HttpUtility.UrlEncode(base[allKeys[i]]));
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int length = stringBuilder2.Length;
					stringBuilder2.Length = length - 1;
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000C8A RID: 3210 RVA: 0x0002EDF0 File Offset: 0x0002CFF0
			public HttpQSCollection()
			{
			}
		}
	}
}
