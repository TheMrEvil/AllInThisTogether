using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;

namespace WebSocketSharp.Net
{
	// Token: 0x02000027 RID: 39
	internal static class HttpUtility
	{
		// Token: 0x060002EE RID: 750 RVA: 0x00011FD3 File Offset: 0x000101D3
		static HttpUtility()
		{
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011FF0 File Offset: 0x000101F0
		private static Dictionary<string, char> getEntities()
		{
			object sync = HttpUtility._sync;
			Dictionary<string, char> entities;
			lock (sync)
			{
				bool flag = HttpUtility._entities == null;
				if (flag)
				{
					HttpUtility.initEntities();
				}
				entities = HttpUtility._entities;
			}
			return entities;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00012040 File Offset: 0x00010240
		private static int getNumber(char c)
		{
			return (c >= '0' && c <= '9') ? ((int)(c - '0')) : ((c >= 'A' && c <= 'F') ? ((int)(c - 'A' + '\n')) : ((c >= 'a' && c <= 'f') ? ((int)(c - 'a' + '\n')) : -1));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001208C File Offset: 0x0001028C
		private static int getNumber(byte[] bytes, int offset, int count)
		{
			int num = 0;
			int num2 = offset + count - 1;
			for (int i = offset; i <= num2; i++)
			{
				int number = HttpUtility.getNumber((char)bytes[i]);
				bool flag = number == -1;
				if (flag)
				{
					return -1;
				}
				num = (num << 4) + number;
			}
			return num;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000120E0 File Offset: 0x000102E0
		private static int getNumber(string s, int offset, int count)
		{
			int num = 0;
			int num2 = offset + count - 1;
			for (int i = offset; i <= num2; i++)
			{
				int number = HttpUtility.getNumber(s[i]);
				bool flag = number == -1;
				if (flag)
				{
					return -1;
				}
				num = (num << 4) + number;
			}
			return num;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00012138 File Offset: 0x00010338
		private static string htmlDecode(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			StringBuilder stringBuilder2 = new StringBuilder();
			int num2 = 0;
			foreach (char c in s)
			{
				bool flag = num == 0;
				if (flag)
				{
					bool flag2 = c == '&';
					if (flag2)
					{
						stringBuilder2.Append('&');
						num = 1;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					bool flag3 = c == '&';
					if (flag3)
					{
						stringBuilder.Append(stringBuilder2.ToString());
						stringBuilder2.Length = 0;
						stringBuilder2.Append('&');
						num = 1;
					}
					else
					{
						stringBuilder2.Append(c);
						bool flag4 = num == 1;
						if (flag4)
						{
							bool flag5 = c == ';';
							if (flag5)
							{
								stringBuilder.Append(stringBuilder2.ToString());
								stringBuilder2.Length = 0;
								num = 0;
							}
							else
							{
								num2 = 0;
								num = ((c == '#') ? 3 : 2);
							}
						}
						else
						{
							bool flag6 = num == 2;
							if (flag6)
							{
								bool flag7 = c == ';';
								if (flag7)
								{
									string text = stringBuilder2.ToString();
									string key = text.Substring(1, text.Length - 2);
									Dictionary<string, char> entities = HttpUtility.getEntities();
									bool flag8 = entities.ContainsKey(key);
									if (flag8)
									{
										stringBuilder.Append(entities[key]);
									}
									else
									{
										stringBuilder.Append(text);
									}
									stringBuilder2.Length = 0;
									num = 0;
								}
							}
							else
							{
								bool flag9 = num == 3;
								if (flag9)
								{
									bool flag10 = c == ';';
									if (flag10)
									{
										bool flag11 = stringBuilder2.Length > 3 && num2 < 65536;
										if (flag11)
										{
											stringBuilder.Append((char)num2);
										}
										else
										{
											stringBuilder.Append(stringBuilder2.ToString());
										}
										stringBuilder2.Length = 0;
										num = 0;
									}
									else
									{
										bool flag12 = c == 'x';
										if (flag12)
										{
											num = ((stringBuilder2.Length == 3) ? 4 : 2);
										}
										else
										{
											bool flag13 = !HttpUtility.isNumeric(c);
											if (flag13)
											{
												num = 2;
											}
											else
											{
												num2 = num2 * 10 + (int)(c - '0');
											}
										}
									}
								}
								else
								{
									bool flag14 = num == 4;
									if (flag14)
									{
										bool flag15 = c == ';';
										if (flag15)
										{
											bool flag16 = stringBuilder2.Length > 4 && num2 < 65536;
											if (flag16)
											{
												stringBuilder.Append((char)num2);
											}
											else
											{
												stringBuilder.Append(stringBuilder2.ToString());
											}
											stringBuilder2.Length = 0;
											num = 0;
										}
										else
										{
											int number = HttpUtility.getNumber(c);
											bool flag17 = number == -1;
											if (flag17)
											{
												num = 2;
											}
											else
											{
												num2 = (num2 << 4) + number;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag18 = stringBuilder2.Length > 0;
			if (flag18)
			{
				stringBuilder.Append(stringBuilder2.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000123F4 File Offset: 0x000105F4
		private static string htmlEncode(string s, bool minimal)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				stringBuilder.Append((c == '"') ? "&quot;" : ((c == '&') ? "&amp;" : ((c == '<') ? "&lt;" : ((c == '>') ? "&gt;" : ((!minimal && c > '\u009f') ? string.Format("&#{0};", (int)c) : c.ToString())))));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012490 File Offset: 0x00010690
		private static void initEntities()
		{
			HttpUtility._entities = new Dictionary<string, char>();
			HttpUtility._entities.Add("nbsp", '\u00a0');
			HttpUtility._entities.Add("iexcl", '¡');
			HttpUtility._entities.Add("cent", '¢');
			HttpUtility._entities.Add("pound", '£');
			HttpUtility._entities.Add("curren", '¤');
			HttpUtility._entities.Add("yen", '¥');
			HttpUtility._entities.Add("brvbar", '¦');
			HttpUtility._entities.Add("sect", '§');
			HttpUtility._entities.Add("uml", '¨');
			HttpUtility._entities.Add("copy", '©');
			HttpUtility._entities.Add("ordf", 'ª');
			HttpUtility._entities.Add("laquo", '«');
			HttpUtility._entities.Add("not", '¬');
			HttpUtility._entities.Add("shy", '­');
			HttpUtility._entities.Add("reg", '®');
			HttpUtility._entities.Add("macr", '¯');
			HttpUtility._entities.Add("deg", '°');
			HttpUtility._entities.Add("plusmn", '±');
			HttpUtility._entities.Add("sup2", '²');
			HttpUtility._entities.Add("sup3", '³');
			HttpUtility._entities.Add("acute", '´');
			HttpUtility._entities.Add("micro", 'µ');
			HttpUtility._entities.Add("para", '¶');
			HttpUtility._entities.Add("middot", '·');
			HttpUtility._entities.Add("cedil", '¸');
			HttpUtility._entities.Add("sup1", '¹');
			HttpUtility._entities.Add("ordm", 'º');
			HttpUtility._entities.Add("raquo", '»');
			HttpUtility._entities.Add("frac14", '¼');
			HttpUtility._entities.Add("frac12", '½');
			HttpUtility._entities.Add("frac34", '¾');
			HttpUtility._entities.Add("iquest", '¿');
			HttpUtility._entities.Add("Agrave", 'À');
			HttpUtility._entities.Add("Aacute", 'Á');
			HttpUtility._entities.Add("Acirc", 'Â');
			HttpUtility._entities.Add("Atilde", 'Ã');
			HttpUtility._entities.Add("Auml", 'Ä');
			HttpUtility._entities.Add("Aring", 'Å');
			HttpUtility._entities.Add("AElig", 'Æ');
			HttpUtility._entities.Add("Ccedil", 'Ç');
			HttpUtility._entities.Add("Egrave", 'È');
			HttpUtility._entities.Add("Eacute", 'É');
			HttpUtility._entities.Add("Ecirc", 'Ê');
			HttpUtility._entities.Add("Euml", 'Ë');
			HttpUtility._entities.Add("Igrave", 'Ì');
			HttpUtility._entities.Add("Iacute", 'Í');
			HttpUtility._entities.Add("Icirc", 'Î');
			HttpUtility._entities.Add("Iuml", 'Ï');
			HttpUtility._entities.Add("ETH", 'Ð');
			HttpUtility._entities.Add("Ntilde", 'Ñ');
			HttpUtility._entities.Add("Ograve", 'Ò');
			HttpUtility._entities.Add("Oacute", 'Ó');
			HttpUtility._entities.Add("Ocirc", 'Ô');
			HttpUtility._entities.Add("Otilde", 'Õ');
			HttpUtility._entities.Add("Ouml", 'Ö');
			HttpUtility._entities.Add("times", '×');
			HttpUtility._entities.Add("Oslash", 'Ø');
			HttpUtility._entities.Add("Ugrave", 'Ù');
			HttpUtility._entities.Add("Uacute", 'Ú');
			HttpUtility._entities.Add("Ucirc", 'Û');
			HttpUtility._entities.Add("Uuml", 'Ü');
			HttpUtility._entities.Add("Yacute", 'Ý');
			HttpUtility._entities.Add("THORN", 'Þ');
			HttpUtility._entities.Add("szlig", 'ß');
			HttpUtility._entities.Add("agrave", 'à');
			HttpUtility._entities.Add("aacute", 'á');
			HttpUtility._entities.Add("acirc", 'â');
			HttpUtility._entities.Add("atilde", 'ã');
			HttpUtility._entities.Add("auml", 'ä');
			HttpUtility._entities.Add("aring", 'å');
			HttpUtility._entities.Add("aelig", 'æ');
			HttpUtility._entities.Add("ccedil", 'ç');
			HttpUtility._entities.Add("egrave", 'è');
			HttpUtility._entities.Add("eacute", 'é');
			HttpUtility._entities.Add("ecirc", 'ê');
			HttpUtility._entities.Add("euml", 'ë');
			HttpUtility._entities.Add("igrave", 'ì');
			HttpUtility._entities.Add("iacute", 'í');
			HttpUtility._entities.Add("icirc", 'î');
			HttpUtility._entities.Add("iuml", 'ï');
			HttpUtility._entities.Add("eth", 'ð');
			HttpUtility._entities.Add("ntilde", 'ñ');
			HttpUtility._entities.Add("ograve", 'ò');
			HttpUtility._entities.Add("oacute", 'ó');
			HttpUtility._entities.Add("ocirc", 'ô');
			HttpUtility._entities.Add("otilde", 'õ');
			HttpUtility._entities.Add("ouml", 'ö');
			HttpUtility._entities.Add("divide", '÷');
			HttpUtility._entities.Add("oslash", 'ø');
			HttpUtility._entities.Add("ugrave", 'ù');
			HttpUtility._entities.Add("uacute", 'ú');
			HttpUtility._entities.Add("ucirc", 'û');
			HttpUtility._entities.Add("uuml", 'ü');
			HttpUtility._entities.Add("yacute", 'ý');
			HttpUtility._entities.Add("thorn", 'þ');
			HttpUtility._entities.Add("yuml", 'ÿ');
			HttpUtility._entities.Add("fnof", 'ƒ');
			HttpUtility._entities.Add("Alpha", 'Α');
			HttpUtility._entities.Add("Beta", 'Β');
			HttpUtility._entities.Add("Gamma", 'Γ');
			HttpUtility._entities.Add("Delta", 'Δ');
			HttpUtility._entities.Add("Epsilon", 'Ε');
			HttpUtility._entities.Add("Zeta", 'Ζ');
			HttpUtility._entities.Add("Eta", 'Η');
			HttpUtility._entities.Add("Theta", 'Θ');
			HttpUtility._entities.Add("Iota", 'Ι');
			HttpUtility._entities.Add("Kappa", 'Κ');
			HttpUtility._entities.Add("Lambda", 'Λ');
			HttpUtility._entities.Add("Mu", 'Μ');
			HttpUtility._entities.Add("Nu", 'Ν');
			HttpUtility._entities.Add("Xi", 'Ξ');
			HttpUtility._entities.Add("Omicron", 'Ο');
			HttpUtility._entities.Add("Pi", 'Π');
			HttpUtility._entities.Add("Rho", 'Ρ');
			HttpUtility._entities.Add("Sigma", 'Σ');
			HttpUtility._entities.Add("Tau", 'Τ');
			HttpUtility._entities.Add("Upsilon", 'Υ');
			HttpUtility._entities.Add("Phi", 'Φ');
			HttpUtility._entities.Add("Chi", 'Χ');
			HttpUtility._entities.Add("Psi", 'Ψ');
			HttpUtility._entities.Add("Omega", 'Ω');
			HttpUtility._entities.Add("alpha", 'α');
			HttpUtility._entities.Add("beta", 'β');
			HttpUtility._entities.Add("gamma", 'γ');
			HttpUtility._entities.Add("delta", 'δ');
			HttpUtility._entities.Add("epsilon", 'ε');
			HttpUtility._entities.Add("zeta", 'ζ');
			HttpUtility._entities.Add("eta", 'η');
			HttpUtility._entities.Add("theta", 'θ');
			HttpUtility._entities.Add("iota", 'ι');
			HttpUtility._entities.Add("kappa", 'κ');
			HttpUtility._entities.Add("lambda", 'λ');
			HttpUtility._entities.Add("mu", 'μ');
			HttpUtility._entities.Add("nu", 'ν');
			HttpUtility._entities.Add("xi", 'ξ');
			HttpUtility._entities.Add("omicron", 'ο');
			HttpUtility._entities.Add("pi", 'π');
			HttpUtility._entities.Add("rho", 'ρ');
			HttpUtility._entities.Add("sigmaf", 'ς');
			HttpUtility._entities.Add("sigma", 'σ');
			HttpUtility._entities.Add("tau", 'τ');
			HttpUtility._entities.Add("upsilon", 'υ');
			HttpUtility._entities.Add("phi", 'φ');
			HttpUtility._entities.Add("chi", 'χ');
			HttpUtility._entities.Add("psi", 'ψ');
			HttpUtility._entities.Add("omega", 'ω');
			HttpUtility._entities.Add("thetasym", 'ϑ');
			HttpUtility._entities.Add("upsih", 'ϒ');
			HttpUtility._entities.Add("piv", 'ϖ');
			HttpUtility._entities.Add("bull", '•');
			HttpUtility._entities.Add("hellip", '…');
			HttpUtility._entities.Add("prime", '′');
			HttpUtility._entities.Add("Prime", '″');
			HttpUtility._entities.Add("oline", '‾');
			HttpUtility._entities.Add("frasl", '⁄');
			HttpUtility._entities.Add("weierp", '℘');
			HttpUtility._entities.Add("image", 'ℑ');
			HttpUtility._entities.Add("real", 'ℜ');
			HttpUtility._entities.Add("trade", '™');
			HttpUtility._entities.Add("alefsym", 'ℵ');
			HttpUtility._entities.Add("larr", '←');
			HttpUtility._entities.Add("uarr", '↑');
			HttpUtility._entities.Add("rarr", '→');
			HttpUtility._entities.Add("darr", '↓');
			HttpUtility._entities.Add("harr", '↔');
			HttpUtility._entities.Add("crarr", '↵');
			HttpUtility._entities.Add("lArr", '⇐');
			HttpUtility._entities.Add("uArr", '⇑');
			HttpUtility._entities.Add("rArr", '⇒');
			HttpUtility._entities.Add("dArr", '⇓');
			HttpUtility._entities.Add("hArr", '⇔');
			HttpUtility._entities.Add("forall", '∀');
			HttpUtility._entities.Add("part", '∂');
			HttpUtility._entities.Add("exist", '∃');
			HttpUtility._entities.Add("empty", '∅');
			HttpUtility._entities.Add("nabla", '∇');
			HttpUtility._entities.Add("isin", '∈');
			HttpUtility._entities.Add("notin", '∉');
			HttpUtility._entities.Add("ni", '∋');
			HttpUtility._entities.Add("prod", '∏');
			HttpUtility._entities.Add("sum", '∑');
			HttpUtility._entities.Add("minus", '−');
			HttpUtility._entities.Add("lowast", '∗');
			HttpUtility._entities.Add("radic", '√');
			HttpUtility._entities.Add("prop", '∝');
			HttpUtility._entities.Add("infin", '∞');
			HttpUtility._entities.Add("ang", '∠');
			HttpUtility._entities.Add("and", '∧');
			HttpUtility._entities.Add("or", '∨');
			HttpUtility._entities.Add("cap", '∩');
			HttpUtility._entities.Add("cup", '∪');
			HttpUtility._entities.Add("int", '∫');
			HttpUtility._entities.Add("there4", '∴');
			HttpUtility._entities.Add("sim", '∼');
			HttpUtility._entities.Add("cong", '≅');
			HttpUtility._entities.Add("asymp", '≈');
			HttpUtility._entities.Add("ne", '≠');
			HttpUtility._entities.Add("equiv", '≡');
			HttpUtility._entities.Add("le", '≤');
			HttpUtility._entities.Add("ge", '≥');
			HttpUtility._entities.Add("sub", '⊂');
			HttpUtility._entities.Add("sup", '⊃');
			HttpUtility._entities.Add("nsub", '⊄');
			HttpUtility._entities.Add("sube", '⊆');
			HttpUtility._entities.Add("supe", '⊇');
			HttpUtility._entities.Add("oplus", '⊕');
			HttpUtility._entities.Add("otimes", '⊗');
			HttpUtility._entities.Add("perp", '⊥');
			HttpUtility._entities.Add("sdot", '⋅');
			HttpUtility._entities.Add("lceil", '⌈');
			HttpUtility._entities.Add("rceil", '⌉');
			HttpUtility._entities.Add("lfloor", '⌊');
			HttpUtility._entities.Add("rfloor", '⌋');
			HttpUtility._entities.Add("lang", '〈');
			HttpUtility._entities.Add("rang", '〉');
			HttpUtility._entities.Add("loz", '◊');
			HttpUtility._entities.Add("spades", '♠');
			HttpUtility._entities.Add("clubs", '♣');
			HttpUtility._entities.Add("hearts", '♥');
			HttpUtility._entities.Add("diams", '♦');
			HttpUtility._entities.Add("quot", '"');
			HttpUtility._entities.Add("amp", '&');
			HttpUtility._entities.Add("lt", '<');
			HttpUtility._entities.Add("gt", '>');
			HttpUtility._entities.Add("OElig", 'Œ');
			HttpUtility._entities.Add("oelig", 'œ');
			HttpUtility._entities.Add("Scaron", 'Š');
			HttpUtility._entities.Add("scaron", 'š');
			HttpUtility._entities.Add("Yuml", 'Ÿ');
			HttpUtility._entities.Add("circ", 'ˆ');
			HttpUtility._entities.Add("tilde", '˜');
			HttpUtility._entities.Add("ensp", '\u2002');
			HttpUtility._entities.Add("emsp", '\u2003');
			HttpUtility._entities.Add("thinsp", '\u2009');
			HttpUtility._entities.Add("zwnj", '‌');
			HttpUtility._entities.Add("zwj", '‍');
			HttpUtility._entities.Add("lrm", '‎');
			HttpUtility._entities.Add("rlm", '‏');
			HttpUtility._entities.Add("ndash", '–');
			HttpUtility._entities.Add("mdash", '—');
			HttpUtility._entities.Add("lsquo", '‘');
			HttpUtility._entities.Add("rsquo", '’');
			HttpUtility._entities.Add("sbquo", '‚');
			HttpUtility._entities.Add("ldquo", '“');
			HttpUtility._entities.Add("rdquo", '”');
			HttpUtility._entities.Add("bdquo", '„');
			HttpUtility._entities.Add("dagger", '†');
			HttpUtility._entities.Add("Dagger", '‡');
			HttpUtility._entities.Add("permil", '‰');
			HttpUtility._entities.Add("lsaquo", '‹');
			HttpUtility._entities.Add("rsaquo", '›');
			HttpUtility._entities.Add("euro", '€');
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00013948 File Offset: 0x00011B48
		private static bool isAlphabet(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00013978 File Offset: 0x00011B78
		private static bool isNumeric(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001399C File Offset: 0x00011B9C
		private static bool isUnreserved(char c)
		{
			return c == '*' || c == '-' || c == '.' || c == '_';
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000139C8 File Offset: 0x00011BC8
		private static bool isUnreservedInRfc2396(char c)
		{
			return c == '!' || c == '\'' || c == '(' || c == ')' || c == '*' || c == '-' || c == '.' || c == '_' || c == '~';
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00013A0C File Offset: 0x00011C0C
		private static bool isUnreservedInRfc3986(char c)
		{
			return c == '-' || c == '.' || c == '_' || c == '~';
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00013A38 File Offset: 0x00011C38
		private static byte[] urlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = offset + count - 1;
				for (int i = offset; i <= num; i++)
				{
					byte b = bytes[i];
					char c = (char)b;
					bool flag = c == '%';
					if (flag)
					{
						bool flag2 = i > num - 2;
						if (flag2)
						{
							break;
						}
						int number = HttpUtility.getNumber(bytes, i + 1, 2);
						bool flag3 = number == -1;
						if (flag3)
						{
							break;
						}
						memoryStream.WriteByte((byte)number);
						i += 2;
					}
					else
					{
						bool flag4 = c == '+';
						if (flag4)
						{
							memoryStream.WriteByte(32);
						}
						else
						{
							memoryStream.WriteByte(b);
						}
					}
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00013B04 File Offset: 0x00011D04
		private static void urlEncode(byte b, Stream output)
		{
			bool flag = b > 31 && b < 127;
			if (flag)
			{
				bool flag2 = b == 32;
				if (flag2)
				{
					output.WriteByte(43);
					return;
				}
				bool flag3 = HttpUtility.isNumeric((char)b);
				if (flag3)
				{
					output.WriteByte(b);
					return;
				}
				bool flag4 = HttpUtility.isAlphabet((char)b);
				if (flag4)
				{
					output.WriteByte(b);
					return;
				}
				bool flag5 = HttpUtility.isUnreserved((char)b);
				if (flag5)
				{
					output.WriteByte(b);
					return;
				}
			}
			byte[] buffer = new byte[]
			{
				37,
				(byte)HttpUtility._hexChars[b >> 4],
				(byte)HttpUtility._hexChars[(int)(b & 15)]
			};
			output.Write(buffer, 0, 3);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00013BB8 File Offset: 0x00011DB8
		private static byte[] urlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = offset + count - 1;
				for (int i = offset; i <= num; i++)
				{
					HttpUtility.urlEncode(bytes[i], memoryStream);
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00013C20 File Offset: 0x00011E20
		internal static Uri CreateRequestUrl(string requestUri, string host, bool websocketRequest, bool secure)
		{
			bool flag = requestUri == null || requestUri.Length == 0;
			Uri result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = host == null || host.Length == 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string text = null;
					string arg = null;
					bool flag3 = requestUri.IndexOf('/') == 0;
					if (flag3)
					{
						arg = requestUri;
					}
					else
					{
						bool flag4 = requestUri.MaybeUri();
						if (flag4)
						{
							Uri uri;
							bool flag5 = !Uri.TryCreate(requestUri, UriKind.Absolute, out uri);
							if (flag5)
							{
								return null;
							}
							text = uri.Scheme;
							bool flag6 = websocketRequest ? (text == "ws" || text == "wss") : (text == "http" || text == "https");
							bool flag7 = !flag6;
							if (flag7)
							{
								return null;
							}
							host = uri.Authority;
							arg = uri.PathAndQuery;
						}
						else
						{
							bool flag8 = requestUri == "*";
							if (!flag8)
							{
								host = requestUri;
							}
						}
					}
					bool flag9 = text == null;
					if (flag9)
					{
						text = (websocketRequest ? (secure ? "wss" : "ws") : (secure ? "https" : "http"));
					}
					bool flag10 = host.IndexOf(':') == -1;
					if (flag10)
					{
						host = string.Format("{0}:{1}", host, secure ? 443 : 80);
					}
					string uriString = string.Format("{0}://{1}{2}", text, host, arg);
					Uri uri2;
					result = (Uri.TryCreate(uriString, UriKind.Absolute, out uri2) ? uri2 : null);
				}
			}
			return result;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013DB8 File Offset: 0x00011FB8
		internal static IPrincipal CreateUser(string response, AuthenticationSchemes scheme, string realm, string method, Func<IIdentity, NetworkCredential> credentialsFinder)
		{
			bool flag = response == null || response.Length == 0;
			IPrincipal result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = scheme == AuthenticationSchemes.Digest;
				if (flag2)
				{
					bool flag3 = realm == null || realm.Length == 0;
					if (flag3)
					{
						return null;
					}
					bool flag4 = method == null || method.Length == 0;
					if (flag4)
					{
						return null;
					}
				}
				else
				{
					bool flag5 = scheme != AuthenticationSchemes.Basic;
					if (flag5)
					{
						return null;
					}
				}
				bool flag6 = credentialsFinder == null;
				if (flag6)
				{
					result = null;
				}
				else
				{
					StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
					bool flag7 = response.IndexOf(scheme.ToString(), comparisonType) != 0;
					if (flag7)
					{
						result = null;
					}
					else
					{
						AuthenticationResponse authenticationResponse = AuthenticationResponse.Parse(response);
						bool flag8 = authenticationResponse == null;
						if (flag8)
						{
							result = null;
						}
						else
						{
							IIdentity identity = authenticationResponse.ToIdentity();
							bool flag9 = identity == null;
							if (flag9)
							{
								result = null;
							}
							else
							{
								NetworkCredential networkCredential = null;
								try
								{
									networkCredential = credentialsFinder(identity);
								}
								catch
								{
								}
								bool flag10 = networkCredential == null;
								if (flag10)
								{
									result = null;
								}
								else
								{
									bool flag11 = scheme == AuthenticationSchemes.Basic;
									if (flag11)
									{
										HttpBasicIdentity httpBasicIdentity = (HttpBasicIdentity)identity;
										result = ((httpBasicIdentity.Password == networkCredential.Password) ? new GenericPrincipal(identity, networkCredential.Roles) : null);
									}
									else
									{
										HttpDigestIdentity httpDigestIdentity = (HttpDigestIdentity)identity;
										result = (httpDigestIdentity.IsValid(networkCredential.Password, realm, method, null) ? new GenericPrincipal(identity, networkCredential.Roles) : null);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00013F4C File Offset: 0x0001214C
		internal static Encoding GetEncoding(string contentType)
		{
			string value = "charset=";
			StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
			foreach (string text in contentType.SplitHeaderValue(new char[]
			{
				';'
			}))
			{
				string text2 = text.Trim();
				bool flag = !text2.StartsWith(value, comparisonType);
				if (!flag)
				{
					string value2 = text2.GetValue('=', true);
					bool flag2 = value2 == null || value2.Length == 0;
					if (flag2)
					{
						return null;
					}
					return Encoding.GetEncoding(value2);
				}
			}
			return null;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00014000 File Offset: 0x00012200
		internal static bool TryGetEncoding(string contentType, out Encoding result)
		{
			result = null;
			try
			{
				result = HttpUtility.GetEncoding(contentType);
			}
			catch
			{
				return false;
			}
			return result != null;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001403C File Offset: 0x0001223C
		public static string HtmlAttributeEncode(string s)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			return (s.Length > 0) ? HttpUtility.htmlEncode(s, true) : s;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00014074 File Offset: 0x00012274
		public static void HtmlAttributeEncode(string s, TextWriter output)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			bool flag2 = output == null;
			if (flag2)
			{
				throw new ArgumentNullException("output");
			}
			bool flag3 = s.Length == 0;
			if (!flag3)
			{
				output.Write(HttpUtility.htmlEncode(s, true));
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000140C8 File Offset: 0x000122C8
		public static string HtmlDecode(string s)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			return (s.Length > 0) ? HttpUtility.htmlDecode(s) : s;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00014100 File Offset: 0x00012300
		public static void HtmlDecode(string s, TextWriter output)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			bool flag2 = output == null;
			if (flag2)
			{
				throw new ArgumentNullException("output");
			}
			bool flag3 = s.Length == 0;
			if (!flag3)
			{
				output.Write(HttpUtility.htmlDecode(s));
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00014150 File Offset: 0x00012350
		public static string HtmlEncode(string s)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			return (s.Length > 0) ? HttpUtility.htmlEncode(s, false) : s;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00014188 File Offset: 0x00012388
		public static void HtmlEncode(string s, TextWriter output)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			bool flag2 = output == null;
			if (flag2)
			{
				throw new ArgumentNullException("output");
			}
			bool flag3 = s.Length == 0;
			if (!flag3)
			{
				output.Write(HttpUtility.htmlEncode(s, false));
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000141DC File Offset: 0x000123DC
		public static string UrlDecode(string s)
		{
			return HttpUtility.UrlDecode(s, Encoding.UTF8);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000141FC File Offset: 0x000123FC
		public static string UrlDecode(byte[] bytes, Encoding encoding)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			return (num > 0) ? (encoding ?? Encoding.UTF8).GetString(HttpUtility.urlDecodeToBytes(bytes, 0, num)) : string.Empty;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00014248 File Offset: 0x00012448
		public static string UrlDecode(string s, Encoding encoding)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			bool flag2 = s.Length == 0;
			string result;
			if (flag2)
			{
				result = s;
			}
			else
			{
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				result = (encoding ?? Encoding.UTF8).GetString(HttpUtility.urlDecodeToBytes(bytes, 0, bytes.Length));
			}
			return result;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000142A4 File Offset: 0x000124A4
		public static string UrlDecode(byte[] bytes, int offset, int count, Encoding encoding)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			bool flag2 = num == 0;
			string result;
			if (flag2)
			{
				bool flag3 = offset != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag4 = count != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = string.Empty;
			}
			else
			{
				bool flag5 = offset < 0 || offset >= num;
				if (flag5)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag6 = count < 0 || count > num - offset;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = ((count > 0) ? (encoding ?? Encoding.UTF8).GetString(HttpUtility.urlDecodeToBytes(bytes, offset, count)) : string.Empty);
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00014368 File Offset: 0x00012568
		public static byte[] UrlDecodeToBytes(byte[] bytes)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			return (num > 0) ? HttpUtility.urlDecodeToBytes(bytes, 0, num) : bytes;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000143A0 File Offset: 0x000125A0
		public static byte[] UrlDecodeToBytes(string s)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			bool flag2 = s.Length == 0;
			byte[] result;
			if (flag2)
			{
				result = new byte[0];
			}
			else
			{
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				result = HttpUtility.urlDecodeToBytes(bytes, 0, bytes.Length);
			}
			return result;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000143F4 File Offset: 0x000125F4
		public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			bool flag2 = num == 0;
			byte[] result;
			if (flag2)
			{
				bool flag3 = offset != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag4 = count != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = bytes;
			}
			else
			{
				bool flag5 = offset < 0 || offset >= num;
				if (flag5)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag6 = count < 0 || count > num - offset;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = ((count > 0) ? HttpUtility.urlDecodeToBytes(bytes, offset, count) : new byte[0]);
			}
			return result;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000144A8 File Offset: 0x000126A8
		public static string UrlEncode(byte[] bytes)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			return (num > 0) ? Encoding.ASCII.GetString(HttpUtility.urlEncodeToBytes(bytes, 0, num)) : string.Empty;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000144F0 File Offset: 0x000126F0
		public static string UrlEncode(string s)
		{
			return HttpUtility.UrlEncode(s, Encoding.UTF8);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00014510 File Offset: 0x00012710
		public static string UrlEncode(string s, Encoding encoding)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			bool flag2 = length == 0;
			string result;
			if (flag2)
			{
				result = s;
			}
			else
			{
				bool flag3 = encoding == null;
				if (flag3)
				{
					encoding = Encoding.UTF8;
				}
				byte[] bytes = new byte[encoding.GetMaxByteCount(length)];
				int bytes2 = encoding.GetBytes(s, 0, length, bytes, 0);
				result = Encoding.ASCII.GetString(HttpUtility.urlEncodeToBytes(bytes, 0, bytes2));
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001458C File Offset: 0x0001278C
		public static string UrlEncode(byte[] bytes, int offset, int count)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			bool flag2 = num == 0;
			string result;
			if (flag2)
			{
				bool flag3 = offset != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag4 = count != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = string.Empty;
			}
			else
			{
				bool flag5 = offset < 0 || offset >= num;
				if (flag5)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag6 = count < 0 || count > num - offset;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = ((count > 0) ? Encoding.ASCII.GetString(HttpUtility.urlEncodeToBytes(bytes, offset, count)) : string.Empty);
			}
			return result;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001464C File Offset: 0x0001284C
		public static byte[] UrlEncodeToBytes(byte[] bytes)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			return (num > 0) ? HttpUtility.urlEncodeToBytes(bytes, 0, num) : bytes;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00014684 File Offset: 0x00012884
		public static byte[] UrlEncodeToBytes(string s)
		{
			return HttpUtility.UrlEncodeToBytes(s, Encoding.UTF8);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000146A4 File Offset: 0x000128A4
		public static byte[] UrlEncodeToBytes(string s, Encoding encoding)
		{
			bool flag = s == null;
			if (flag)
			{
				throw new ArgumentNullException("s");
			}
			bool flag2 = s.Length == 0;
			byte[] result;
			if (flag2)
			{
				result = new byte[0];
			}
			else
			{
				byte[] bytes = (encoding ?? Encoding.UTF8).GetBytes(s);
				result = HttpUtility.urlEncodeToBytes(bytes, 0, bytes.Length);
			}
			return result;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000146FC File Offset: 0x000128FC
		public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			bool flag2 = num == 0;
			byte[] result;
			if (flag2)
			{
				bool flag3 = offset != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag4 = count != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = bytes;
			}
			else
			{
				bool flag5 = offset < 0 || offset >= num;
				if (flag5)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				bool flag6 = count < 0 || count > num - offset;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				result = ((count > 0) ? HttpUtility.urlEncodeToBytes(bytes, offset, count) : new byte[0]);
			}
			return result;
		}

		// Token: 0x04000125 RID: 293
		private static Dictionary<string, char> _entities;

		// Token: 0x04000126 RID: 294
		private static char[] _hexChars = "0123456789ABCDEF".ToCharArray();

		// Token: 0x04000127 RID: 295
		private static object _sync = new object();
	}
}
