using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Unity;

namespace System.Web.Util
{
	// Token: 0x020001E5 RID: 485
	public class HttpEncoder
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000336F0 File Offset: 0x000318F0
		private static IDictionary<string, char> Entities
		{
			get
			{
				object obj = HttpEncoder.entitiesLock;
				IDictionary<string, char> result;
				lock (obj)
				{
					if (HttpEncoder.entities == null)
					{
						HttpEncoder.InitEntities();
					}
					result = HttpEncoder.entities;
				}
				return result;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003373C File Offset: 0x0003193C
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x00033759 File Offset: 0x00031959
		public static HttpEncoder Current
		{
			get
			{
				if (HttpEncoder.currentEncoder == null)
				{
					HttpEncoder.currentEncoder = HttpEncoder.currentEncoderLazy.Value;
				}
				return HttpEncoder.currentEncoder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				HttpEncoder.currentEncoder = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0003376F File Offset: 0x0003196F
		public static HttpEncoder Default
		{
			get
			{
				return HttpEncoder.defaultEncoder.Value;
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003377C File Offset: 0x0003197C
		static HttpEncoder()
		{
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0000219B File Offset: 0x0000039B
		public HttpEncoder()
		{
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000337D2 File Offset: 0x000319D2
		protected internal virtual void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
		{
			if (string.IsNullOrEmpty(headerName))
			{
				encodedHeaderName = headerName;
			}
			else
			{
				encodedHeaderName = HttpEncoder.EncodeHeaderString(headerName);
			}
			if (string.IsNullOrEmpty(headerValue))
			{
				encodedHeaderValue = headerValue;
				return;
			}
			encodedHeaderValue = HttpEncoder.EncodeHeaderString(headerValue);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000337FF File Offset: 0x000319FF
		private static void StringBuilderAppend(string s, ref StringBuilder sb)
		{
			if (sb == null)
			{
				sb = new StringBuilder(s);
				return;
			}
			sb.Append(s);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00033818 File Offset: 0x00031A18
		private static string EncodeHeaderString(string input)
		{
			StringBuilder stringBuilder = null;
			foreach (char c in input)
			{
				if ((c < ' ' && c != '\t') || c == '\u007f')
				{
					HttpEncoder.StringBuilderAppend(string.Format("%{0:x2}", (int)c), ref stringBuilder);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return input;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00033871 File Offset: 0x00031A71
		protected internal virtual void HtmlAttributeEncode(string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			output.Write(HttpEncoder.HtmlAttributeEncode(value));
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00033896 File Offset: 0x00031A96
		protected internal virtual void HtmlDecode(string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write(HttpEncoder.HtmlDecode(value));
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000338B2 File Offset: 0x00031AB2
		protected internal virtual void HtmlEncode(string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write(HttpEncoder.HtmlEncode(value));
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000338CE File Offset: 0x00031ACE
		protected internal virtual byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			return HttpEncoder.UrlEncodeToBytes(bytes, offset, count);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0003376F File Offset: 0x0003196F
		private static HttpEncoder GetCustomEncoderFromConfig()
		{
			return HttpEncoder.defaultEncoder.Value;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000338D8 File Offset: 0x00031AD8
		protected internal virtual string UrlPathEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			MemoryStream memoryStream = new MemoryStream();
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				HttpEncoder.UrlPathEncodeChar(value[i], memoryStream);
			}
			return Encoding.ASCII.GetString(memoryStream.ToArray());
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00033928 File Offset: 0x00031B28
		internal static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			if (num == 0)
			{
				return new byte[0];
			}
			if (offset < 0 || offset >= num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > num - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			MemoryStream memoryStream = new MemoryStream(count);
			int num2 = offset + count;
			for (int i = offset; i < num2; i++)
			{
				HttpEncoder.UrlEncodeChar((char)bytes[i], memoryStream, false);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000339A0 File Offset: 0x00031BA0
		internal static string HtmlEncode(string s)
		{
			if (s == null)
			{
				return null;
			}
			if (s.Length == 0)
			{
				return string.Empty;
			}
			bool flag = false;
			foreach (char c in s)
			{
				if (c == '&' || c == '"' || c == '<' || c == '>' || c > '\u009f' || c == '\'')
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = s.Length;
			int j = 0;
			while (j < length)
			{
				char c2 = s[j];
				if (c2 <= '\'')
				{
					if (c2 != '"')
					{
						if (c2 != '&')
						{
							if (c2 != '\'')
							{
								goto IL_12E;
							}
							stringBuilder.Append("&#39;");
						}
						else
						{
							stringBuilder.Append("&amp;");
						}
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c2 <= '>')
				{
					if (c2 != '<')
					{
						if (c2 != '>')
						{
							goto IL_12E;
						}
						stringBuilder.Append("&gt;");
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else if (c2 != '＜')
				{
					if (c2 != '＞')
					{
						goto IL_12E;
					}
					stringBuilder.Append("&#65310;");
				}
				else
				{
					stringBuilder.Append("&#65308;");
				}
				IL_17A:
				j++;
				continue;
				IL_12E:
				if (c2 > '\u009f' && c2 < 'Ā')
				{
					stringBuilder.Append("&#");
					StringBuilder stringBuilder2 = stringBuilder;
					int num = (int)c2;
					stringBuilder2.Append(num.ToString(Helpers.InvariantCulture));
					stringBuilder.Append(";");
					goto IL_17A;
				}
				stringBuilder.Append(c2);
				goto IL_17A;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00033B3C File Offset: 0x00031D3C
		internal static string HtmlAttributeEncode(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			bool flag = false;
			foreach (char c in s)
			{
				if (c == '&' || c == '"' || c == '<' || c == '\'')
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = s.Length;
			int j = 0;
			while (j < length)
			{
				char c2 = s[j];
				if (c2 <= '&')
				{
					if (c2 != '"')
					{
						if (c2 != '&')
						{
							goto IL_C1;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c2 != '\'')
				{
					if (c2 != '<')
					{
						goto IL_C1;
					}
					stringBuilder.Append("&lt;");
				}
				else
				{
					stringBuilder.Append("&#39;");
				}
				IL_CA:
				j++;
				continue;
				IL_C1:
				stringBuilder.Append(c2);
				goto IL_CA;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00033C24 File Offset: 0x00031E24
		internal static string HtmlDecode(string s)
		{
			if (s == null)
			{
				return null;
			}
			if (s.Length == 0)
			{
				return string.Empty;
			}
			if (s.IndexOf('&') == -1)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			int length = s.Length;
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (num == 0)
				{
					if (c == '&')
					{
						stringBuilder2.Append(c);
						stringBuilder.Append(c);
						num = 1;
					}
					else
					{
						stringBuilder3.Append(c);
					}
				}
				else if (c == '&')
				{
					num = 1;
					if (flag2)
					{
						stringBuilder2.Append(num2.ToString(Helpers.InvariantCulture));
						flag2 = false;
					}
					stringBuilder3.Append(stringBuilder2.ToString());
					stringBuilder2.Length = 0;
					stringBuilder2.Append('&');
				}
				else if (num == 1)
				{
					if (c == ';')
					{
						num = 0;
						stringBuilder3.Append(stringBuilder2.ToString());
						stringBuilder3.Append(c);
						stringBuilder2.Length = 0;
					}
					else
					{
						num2 = 0;
						flag = false;
						if (c != '#')
						{
							num = 2;
						}
						else
						{
							num = 3;
						}
						stringBuilder2.Append(c);
						stringBuilder.Append(c);
					}
				}
				else if (num == 2)
				{
					stringBuilder2.Append(c);
					if (c == ';')
					{
						string text = stringBuilder2.ToString();
						if (text.Length > 1 && HttpEncoder.Entities.ContainsKey(text.Substring(1, text.Length - 2)))
						{
							text = HttpEncoder.Entities[text.Substring(1, text.Length - 2)].ToString();
						}
						stringBuilder3.Append(text);
						num = 0;
						stringBuilder2.Length = 0;
						stringBuilder.Length = 0;
					}
				}
				else if (num == 3)
				{
					if (c == ';')
					{
						if (num2 == 0)
						{
							stringBuilder3.Append(stringBuilder.ToString() + ";");
						}
						else if (num2 > 65535)
						{
							stringBuilder3.Append("&#");
							stringBuilder3.Append(num2.ToString(Helpers.InvariantCulture));
							stringBuilder3.Append(";");
						}
						else
						{
							stringBuilder3.Append((char)num2);
						}
						num = 0;
						stringBuilder2.Length = 0;
						stringBuilder.Length = 0;
						flag2 = false;
					}
					else if (flag && Uri.IsHexDigit(c))
					{
						num2 = num2 * 16 + Uri.FromHex(c);
						flag2 = true;
						stringBuilder.Append(c);
					}
					else if (char.IsDigit(c))
					{
						num2 = num2 * 10 + (int)(c - '0');
						flag2 = true;
						stringBuilder.Append(c);
					}
					else if (num2 == 0 && (c == 'x' || c == 'X'))
					{
						flag = true;
						stringBuilder.Append(c);
					}
					else
					{
						num = 2;
						if (flag2)
						{
							stringBuilder2.Append(num2.ToString(Helpers.InvariantCulture));
							flag2 = false;
						}
						stringBuilder2.Append(c);
					}
				}
			}
			if (stringBuilder2.Length > 0)
			{
				stringBuilder3.Append(stringBuilder2.ToString());
			}
			else if (flag2)
			{
				stringBuilder3.Append(num2.ToString(Helpers.InvariantCulture));
			}
			return stringBuilder3.ToString();
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00033F41 File Offset: 0x00032141
		internal static bool NotEncoded(char c)
		{
			return c == '!' || c == '(' || c == ')' || c == '*' || c == '-' || c == '.' || c == '_';
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033F68 File Offset: 0x00032168
		internal static void UrlEncodeChar(char c, Stream result, bool isUnicode)
		{
			if (c > 'ÿ')
			{
				result.WriteByte(37);
				result.WriteByte(117);
				int num = (int)(c >> 12);
				result.WriteByte((byte)HttpEncoder.hexChars[num]);
				num = (int)(c >> 8 & '\u000f');
				result.WriteByte((byte)HttpEncoder.hexChars[num]);
				num = (int)(c >> 4 & '\u000f');
				result.WriteByte((byte)HttpEncoder.hexChars[num]);
				num = (int)(c & '\u000f');
				result.WriteByte((byte)HttpEncoder.hexChars[num]);
				return;
			}
			if (c > ' ' && HttpEncoder.NotEncoded(c))
			{
				result.WriteByte((byte)c);
				return;
			}
			if (c == ' ')
			{
				result.WriteByte(43);
				return;
			}
			if (c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z')
			{
				if (isUnicode && c > '\u007f')
				{
					result.WriteByte(37);
					result.WriteByte(117);
					result.WriteByte(48);
					result.WriteByte(48);
				}
				else
				{
					result.WriteByte(37);
				}
				int num2 = (int)(c >> 4);
				result.WriteByte((byte)HttpEncoder.hexChars[num2]);
				num2 = (int)(c & '\u000f');
				result.WriteByte((byte)HttpEncoder.hexChars[num2]);
				return;
			}
			result.WriteByte((byte)c);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00034080 File Offset: 0x00032280
		internal static void UrlPathEncodeChar(char c, Stream result)
		{
			if (c < '!' || c > '~')
			{
				byte[] bytes = Encoding.UTF8.GetBytes(c.ToString());
				for (int i = 0; i < bytes.Length; i++)
				{
					result.WriteByte(37);
					int num = bytes[i] >> 4;
					result.WriteByte((byte)HttpEncoder.hexChars[num]);
					num = (int)(bytes[i] & 15);
					result.WriteByte((byte)HttpEncoder.hexChars[num]);
				}
				return;
			}
			if (c == ' ')
			{
				result.WriteByte(37);
				result.WriteByte(50);
				result.WriteByte(48);
				return;
			}
			result.WriteByte((byte)c);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00034110 File Offset: 0x00032310
		private static void InitEntities()
		{
			HttpEncoder.entities = new SortedDictionary<string, char>(StringComparer.Ordinal);
			HttpEncoder.entities.Add("nbsp", '\u00a0');
			HttpEncoder.entities.Add("iexcl", '¡');
			HttpEncoder.entities.Add("cent", '¢');
			HttpEncoder.entities.Add("pound", '£');
			HttpEncoder.entities.Add("curren", '¤');
			HttpEncoder.entities.Add("yen", '¥');
			HttpEncoder.entities.Add("brvbar", '¦');
			HttpEncoder.entities.Add("sect", '§');
			HttpEncoder.entities.Add("uml", '¨');
			HttpEncoder.entities.Add("copy", '©');
			HttpEncoder.entities.Add("ordf", 'ª');
			HttpEncoder.entities.Add("laquo", '«');
			HttpEncoder.entities.Add("not", '¬');
			HttpEncoder.entities.Add("shy", '­');
			HttpEncoder.entities.Add("reg", '®');
			HttpEncoder.entities.Add("macr", '¯');
			HttpEncoder.entities.Add("deg", '°');
			HttpEncoder.entities.Add("plusmn", '±');
			HttpEncoder.entities.Add("sup2", '²');
			HttpEncoder.entities.Add("sup3", '³');
			HttpEncoder.entities.Add("acute", '´');
			HttpEncoder.entities.Add("micro", 'µ');
			HttpEncoder.entities.Add("para", '¶');
			HttpEncoder.entities.Add("middot", '·');
			HttpEncoder.entities.Add("cedil", '¸');
			HttpEncoder.entities.Add("sup1", '¹');
			HttpEncoder.entities.Add("ordm", 'º');
			HttpEncoder.entities.Add("raquo", '»');
			HttpEncoder.entities.Add("frac14", '¼');
			HttpEncoder.entities.Add("frac12", '½');
			HttpEncoder.entities.Add("frac34", '¾');
			HttpEncoder.entities.Add("iquest", '¿');
			HttpEncoder.entities.Add("Agrave", 'À');
			HttpEncoder.entities.Add("Aacute", 'Á');
			HttpEncoder.entities.Add("Acirc", 'Â');
			HttpEncoder.entities.Add("Atilde", 'Ã');
			HttpEncoder.entities.Add("Auml", 'Ä');
			HttpEncoder.entities.Add("Aring", 'Å');
			HttpEncoder.entities.Add("AElig", 'Æ');
			HttpEncoder.entities.Add("Ccedil", 'Ç');
			HttpEncoder.entities.Add("Egrave", 'È');
			HttpEncoder.entities.Add("Eacute", 'É');
			HttpEncoder.entities.Add("Ecirc", 'Ê');
			HttpEncoder.entities.Add("Euml", 'Ë');
			HttpEncoder.entities.Add("Igrave", 'Ì');
			HttpEncoder.entities.Add("Iacute", 'Í');
			HttpEncoder.entities.Add("Icirc", 'Î');
			HttpEncoder.entities.Add("Iuml", 'Ï');
			HttpEncoder.entities.Add("ETH", 'Ð');
			HttpEncoder.entities.Add("Ntilde", 'Ñ');
			HttpEncoder.entities.Add("Ograve", 'Ò');
			HttpEncoder.entities.Add("Oacute", 'Ó');
			HttpEncoder.entities.Add("Ocirc", 'Ô');
			HttpEncoder.entities.Add("Otilde", 'Õ');
			HttpEncoder.entities.Add("Ouml", 'Ö');
			HttpEncoder.entities.Add("times", '×');
			HttpEncoder.entities.Add("Oslash", 'Ø');
			HttpEncoder.entities.Add("Ugrave", 'Ù');
			HttpEncoder.entities.Add("Uacute", 'Ú');
			HttpEncoder.entities.Add("Ucirc", 'Û');
			HttpEncoder.entities.Add("Uuml", 'Ü');
			HttpEncoder.entities.Add("Yacute", 'Ý');
			HttpEncoder.entities.Add("THORN", 'Þ');
			HttpEncoder.entities.Add("szlig", 'ß');
			HttpEncoder.entities.Add("agrave", 'à');
			HttpEncoder.entities.Add("aacute", 'á');
			HttpEncoder.entities.Add("acirc", 'â');
			HttpEncoder.entities.Add("atilde", 'ã');
			HttpEncoder.entities.Add("auml", 'ä');
			HttpEncoder.entities.Add("aring", 'å');
			HttpEncoder.entities.Add("aelig", 'æ');
			HttpEncoder.entities.Add("ccedil", 'ç');
			HttpEncoder.entities.Add("egrave", 'è');
			HttpEncoder.entities.Add("eacute", 'é');
			HttpEncoder.entities.Add("ecirc", 'ê');
			HttpEncoder.entities.Add("euml", 'ë');
			HttpEncoder.entities.Add("igrave", 'ì');
			HttpEncoder.entities.Add("iacute", 'í');
			HttpEncoder.entities.Add("icirc", 'î');
			HttpEncoder.entities.Add("iuml", 'ï');
			HttpEncoder.entities.Add("eth", 'ð');
			HttpEncoder.entities.Add("ntilde", 'ñ');
			HttpEncoder.entities.Add("ograve", 'ò');
			HttpEncoder.entities.Add("oacute", 'ó');
			HttpEncoder.entities.Add("ocirc", 'ô');
			HttpEncoder.entities.Add("otilde", 'õ');
			HttpEncoder.entities.Add("ouml", 'ö');
			HttpEncoder.entities.Add("divide", '÷');
			HttpEncoder.entities.Add("oslash", 'ø');
			HttpEncoder.entities.Add("ugrave", 'ù');
			HttpEncoder.entities.Add("uacute", 'ú');
			HttpEncoder.entities.Add("ucirc", 'û');
			HttpEncoder.entities.Add("uuml", 'ü');
			HttpEncoder.entities.Add("yacute", 'ý');
			HttpEncoder.entities.Add("thorn", 'þ');
			HttpEncoder.entities.Add("yuml", 'ÿ');
			HttpEncoder.entities.Add("fnof", 'ƒ');
			HttpEncoder.entities.Add("Alpha", 'Α');
			HttpEncoder.entities.Add("Beta", 'Β');
			HttpEncoder.entities.Add("Gamma", 'Γ');
			HttpEncoder.entities.Add("Delta", 'Δ');
			HttpEncoder.entities.Add("Epsilon", 'Ε');
			HttpEncoder.entities.Add("Zeta", 'Ζ');
			HttpEncoder.entities.Add("Eta", 'Η');
			HttpEncoder.entities.Add("Theta", 'Θ');
			HttpEncoder.entities.Add("Iota", 'Ι');
			HttpEncoder.entities.Add("Kappa", 'Κ');
			HttpEncoder.entities.Add("Lambda", 'Λ');
			HttpEncoder.entities.Add("Mu", 'Μ');
			HttpEncoder.entities.Add("Nu", 'Ν');
			HttpEncoder.entities.Add("Xi", 'Ξ');
			HttpEncoder.entities.Add("Omicron", 'Ο');
			HttpEncoder.entities.Add("Pi", 'Π');
			HttpEncoder.entities.Add("Rho", 'Ρ');
			HttpEncoder.entities.Add("Sigma", 'Σ');
			HttpEncoder.entities.Add("Tau", 'Τ');
			HttpEncoder.entities.Add("Upsilon", 'Υ');
			HttpEncoder.entities.Add("Phi", 'Φ');
			HttpEncoder.entities.Add("Chi", 'Χ');
			HttpEncoder.entities.Add("Psi", 'Ψ');
			HttpEncoder.entities.Add("Omega", 'Ω');
			HttpEncoder.entities.Add("alpha", 'α');
			HttpEncoder.entities.Add("beta", 'β');
			HttpEncoder.entities.Add("gamma", 'γ');
			HttpEncoder.entities.Add("delta", 'δ');
			HttpEncoder.entities.Add("epsilon", 'ε');
			HttpEncoder.entities.Add("zeta", 'ζ');
			HttpEncoder.entities.Add("eta", 'η');
			HttpEncoder.entities.Add("theta", 'θ');
			HttpEncoder.entities.Add("iota", 'ι');
			HttpEncoder.entities.Add("kappa", 'κ');
			HttpEncoder.entities.Add("lambda", 'λ');
			HttpEncoder.entities.Add("mu", 'μ');
			HttpEncoder.entities.Add("nu", 'ν');
			HttpEncoder.entities.Add("xi", 'ξ');
			HttpEncoder.entities.Add("omicron", 'ο');
			HttpEncoder.entities.Add("pi", 'π');
			HttpEncoder.entities.Add("rho", 'ρ');
			HttpEncoder.entities.Add("sigmaf", 'ς');
			HttpEncoder.entities.Add("sigma", 'σ');
			HttpEncoder.entities.Add("tau", 'τ');
			HttpEncoder.entities.Add("upsilon", 'υ');
			HttpEncoder.entities.Add("phi", 'φ');
			HttpEncoder.entities.Add("chi", 'χ');
			HttpEncoder.entities.Add("psi", 'ψ');
			HttpEncoder.entities.Add("omega", 'ω');
			HttpEncoder.entities.Add("thetasym", 'ϑ');
			HttpEncoder.entities.Add("upsih", 'ϒ');
			HttpEncoder.entities.Add("piv", 'ϖ');
			HttpEncoder.entities.Add("bull", '•');
			HttpEncoder.entities.Add("hellip", '…');
			HttpEncoder.entities.Add("prime", '′');
			HttpEncoder.entities.Add("Prime", '″');
			HttpEncoder.entities.Add("oline", '‾');
			HttpEncoder.entities.Add("frasl", '⁄');
			HttpEncoder.entities.Add("weierp", '℘');
			HttpEncoder.entities.Add("image", 'ℑ');
			HttpEncoder.entities.Add("real", 'ℜ');
			HttpEncoder.entities.Add("trade", '™');
			HttpEncoder.entities.Add("alefsym", 'ℵ');
			HttpEncoder.entities.Add("larr", '←');
			HttpEncoder.entities.Add("uarr", '↑');
			HttpEncoder.entities.Add("rarr", '→');
			HttpEncoder.entities.Add("darr", '↓');
			HttpEncoder.entities.Add("harr", '↔');
			HttpEncoder.entities.Add("crarr", '↵');
			HttpEncoder.entities.Add("lArr", '⇐');
			HttpEncoder.entities.Add("uArr", '⇑');
			HttpEncoder.entities.Add("rArr", '⇒');
			HttpEncoder.entities.Add("dArr", '⇓');
			HttpEncoder.entities.Add("hArr", '⇔');
			HttpEncoder.entities.Add("forall", '∀');
			HttpEncoder.entities.Add("part", '∂');
			HttpEncoder.entities.Add("exist", '∃');
			HttpEncoder.entities.Add("empty", '∅');
			HttpEncoder.entities.Add("nabla", '∇');
			HttpEncoder.entities.Add("isin", '∈');
			HttpEncoder.entities.Add("notin", '∉');
			HttpEncoder.entities.Add("ni", '∋');
			HttpEncoder.entities.Add("prod", '∏');
			HttpEncoder.entities.Add("sum", '∑');
			HttpEncoder.entities.Add("minus", '−');
			HttpEncoder.entities.Add("lowast", '∗');
			HttpEncoder.entities.Add("radic", '√');
			HttpEncoder.entities.Add("prop", '∝');
			HttpEncoder.entities.Add("infin", '∞');
			HttpEncoder.entities.Add("ang", '∠');
			HttpEncoder.entities.Add("and", '∧');
			HttpEncoder.entities.Add("or", '∨');
			HttpEncoder.entities.Add("cap", '∩');
			HttpEncoder.entities.Add("cup", '∪');
			HttpEncoder.entities.Add("int", '∫');
			HttpEncoder.entities.Add("there4", '∴');
			HttpEncoder.entities.Add("sim", '∼');
			HttpEncoder.entities.Add("cong", '≅');
			HttpEncoder.entities.Add("asymp", '≈');
			HttpEncoder.entities.Add("ne", '≠');
			HttpEncoder.entities.Add("equiv", '≡');
			HttpEncoder.entities.Add("le", '≤');
			HttpEncoder.entities.Add("ge", '≥');
			HttpEncoder.entities.Add("sub", '⊂');
			HttpEncoder.entities.Add("sup", '⊃');
			HttpEncoder.entities.Add("nsub", '⊄');
			HttpEncoder.entities.Add("sube", '⊆');
			HttpEncoder.entities.Add("supe", '⊇');
			HttpEncoder.entities.Add("oplus", '⊕');
			HttpEncoder.entities.Add("otimes", '⊗');
			HttpEncoder.entities.Add("perp", '⊥');
			HttpEncoder.entities.Add("sdot", '⋅');
			HttpEncoder.entities.Add("lceil", '⌈');
			HttpEncoder.entities.Add("rceil", '⌉');
			HttpEncoder.entities.Add("lfloor", '⌊');
			HttpEncoder.entities.Add("rfloor", '⌋');
			HttpEncoder.entities.Add("lang", '〈');
			HttpEncoder.entities.Add("rang", '〉');
			HttpEncoder.entities.Add("loz", '◊');
			HttpEncoder.entities.Add("spades", '♠');
			HttpEncoder.entities.Add("clubs", '♣');
			HttpEncoder.entities.Add("hearts", '♥');
			HttpEncoder.entities.Add("diams", '♦');
			HttpEncoder.entities.Add("quot", '"');
			HttpEncoder.entities.Add("amp", '&');
			HttpEncoder.entities.Add("lt", '<');
			HttpEncoder.entities.Add("gt", '>');
			HttpEncoder.entities.Add("OElig", 'Œ');
			HttpEncoder.entities.Add("oelig", 'œ');
			HttpEncoder.entities.Add("Scaron", 'Š');
			HttpEncoder.entities.Add("scaron", 'š');
			HttpEncoder.entities.Add("Yuml", 'Ÿ');
			HttpEncoder.entities.Add("circ", 'ˆ');
			HttpEncoder.entities.Add("tilde", '˜');
			HttpEncoder.entities.Add("ensp", '\u2002');
			HttpEncoder.entities.Add("emsp", '\u2003');
			HttpEncoder.entities.Add("thinsp", '\u2009');
			HttpEncoder.entities.Add("zwnj", '‌');
			HttpEncoder.entities.Add("zwj", '‍');
			HttpEncoder.entities.Add("lrm", '‎');
			HttpEncoder.entities.Add("rlm", '‏');
			HttpEncoder.entities.Add("ndash", '–');
			HttpEncoder.entities.Add("mdash", '—');
			HttpEncoder.entities.Add("lsquo", '‘');
			HttpEncoder.entities.Add("rsquo", '’');
			HttpEncoder.entities.Add("sbquo", '‚');
			HttpEncoder.entities.Add("ldquo", '“');
			HttpEncoder.entities.Add("rdquo", '”');
			HttpEncoder.entities.Add("bdquo", '„');
			HttpEncoder.entities.Add("dagger", '†');
			HttpEncoder.entities.Add("Dagger", '‡');
			HttpEncoder.entities.Add("permil", '‰');
			HttpEncoder.entities.Add("lsaquo", '‹');
			HttpEncoder.entities.Add("rsaquo", '›');
			HttpEncoder.entities.Add("euro", '€');
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00032884 File Offset: 0x00030A84
		protected internal virtual string JavaScriptStringEncode(string value)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x040007CB RID: 1995
		private static char[] hexChars = "0123456789abcdef".ToCharArray();

		// Token: 0x040007CC RID: 1996
		private static object entitiesLock = new object();

		// Token: 0x040007CD RID: 1997
		private static SortedDictionary<string, char> entities;

		// Token: 0x040007CE RID: 1998
		private static Lazy<HttpEncoder> defaultEncoder = new Lazy<HttpEncoder>(() => new HttpEncoder());

		// Token: 0x040007CF RID: 1999
		private static Lazy<HttpEncoder> currentEncoderLazy = new Lazy<HttpEncoder>(new Func<HttpEncoder>(HttpEncoder.GetCustomEncoderFromConfig));

		// Token: 0x040007D0 RID: 2000
		private static HttpEncoder currentEncoder;

		// Token: 0x020001E6 RID: 486
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000CB6 RID: 3254 RVA: 0x000354D0 File Offset: 0x000336D0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000CB7 RID: 3255 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x06000CB8 RID: 3256 RVA: 0x000354DC File Offset: 0x000336DC
			internal HttpEncoder <.cctor>b__13_0()
			{
				return new HttpEncoder();
			}

			// Token: 0x040007D1 RID: 2001
			public static readonly HttpEncoder.<>c <>9 = new HttpEncoder.<>c();
		}
	}
}
