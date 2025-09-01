using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using System.Text;

namespace System.Net
{
	// Token: 0x020005CF RID: 1487
	internal sealed class HttpListenerRequestUriBuilder
	{
		// Token: 0x06003013 RID: 12307 RVA: 0x000A5CB6 File Offset: 0x000A3EB6
		static HttpListenerRequestUriBuilder()
		{
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000A5CE8 File Offset: 0x000A3EE8
		private HttpListenerRequestUriBuilder(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			this.rawUri = rawUri;
			this.cookedUriScheme = cookedUriScheme;
			this.cookedUriHost = cookedUriHost;
			this.cookedUriPath = HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(cookedUriPath);
			if (cookedUriQuery == null)
			{
				this.cookedUriQuery = string.Empty;
				return;
			}
			this.cookedUriQuery = cookedUriQuery;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000A5D35 File Offset: 0x000A3F35
		public static Uri GetRequestUri(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			return new HttpListenerRequestUriBuilder(rawUri, cookedUriScheme, cookedUriHost, cookedUriPath, cookedUriQuery).Build();
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000A5D48 File Offset: 0x000A3F48
		private Uri Build()
		{
			if (HttpListenerRequestUriBuilder.useCookedRequestUrl)
			{
				this.BuildRequestUriUsingCookedPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingRawPath();
				}
			}
			else
			{
				this.BuildRequestUriUsingRawPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingCookedPath();
				}
			}
			return this.requestUri;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000A5D98 File Offset: 0x000A3F98
		private void BuildRequestUriUsingCookedPath()
		{
			if (!Uri.TryCreate(string.Concat(new string[]
			{
				this.cookedUriScheme,
				Uri.SchemeDelimiter,
				this.cookedUriHost,
				this.cookedUriPath,
				this.cookedUriQuery
			}), UriKind.Absolute, out this.requestUri))
			{
				this.LogWarning("BuildRequestUriUsingCookedPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[]
				{
					this.cookedUriScheme,
					this.cookedUriHost,
					this.cookedUriPath,
					this.cookedUriQuery
				});
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000A5E24 File Offset: 0x000A4024
		private void BuildRequestUriUsingRawPath()
		{
			this.rawPath = HttpListenerRequestUriBuilder.GetPath(this.rawUri);
			bool flag;
			if (this.rawPath == string.Empty)
			{
				string text = this.rawPath;
				if (text == string.Empty)
				{
					text = "/";
				}
				flag = Uri.TryCreate(string.Concat(new string[]
				{
					this.cookedUriScheme,
					Uri.SchemeDelimiter,
					this.cookedUriHost,
					text,
					this.cookedUriQuery
				}), UriKind.Absolute, out this.requestUri);
			}
			else
			{
				HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.BuildRequestUriUsingRawPath(HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Primary));
				if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.EncodingError)
				{
					Encoding encoding = HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Secondary);
					parsingResult = this.BuildRequestUriUsingRawPath(encoding);
				}
				flag = (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success);
			}
			if (!flag)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[]
				{
					this.cookedUriScheme,
					this.cookedUriHost,
					this.rawPath,
					this.cookedUriQuery
				});
			}
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000A5F16 File Offset: 0x000A4116
		private static Encoding GetEncoding(HttpListenerRequestUriBuilder.EncodingType type)
		{
			if (type == HttpListenerRequestUriBuilder.EncodingType.Secondary)
			{
				return HttpListenerRequestUriBuilder.ansiEncoding;
			}
			return HttpListenerRequestUriBuilder.utf8Encoding;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000A5F2C File Offset: 0x000A412C
		private HttpListenerRequestUriBuilder.ParsingResult BuildRequestUriUsingRawPath(Encoding encoding)
		{
			this.rawOctets = new List<byte>();
			this.requestUriString = new StringBuilder();
			this.requestUriString.Append(this.cookedUriScheme);
			this.requestUriString.Append(Uri.SchemeDelimiter);
			this.requestUriString.Append(this.cookedUriHost);
			HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.ParseRawPath(encoding);
			if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.requestUriString.Append(this.cookedUriQuery);
				if (!Uri.TryCreate(this.requestUriString.ToString(), UriKind.Absolute, out this.requestUri))
				{
					parsingResult = HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
				}
			}
			if (parsingResult != HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't convert Uri path '{0}' using encoding '{1}'.", new object[]
				{
					this.rawPath,
					encoding.EncodingName
				});
			}
			return parsingResult;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000A5FE8 File Offset: 0x000A41E8
		private HttpListenerRequestUriBuilder.ParsingResult ParseRawPath(Encoding encoding)
		{
			int i = 0;
			while (i < this.rawPath.Length)
			{
				char c = this.rawPath[i];
				if (c == '%')
				{
					i++;
					c = this.rawPath[i];
					if (c == 'u' || c == 'U')
					{
						if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
						}
						if (!this.AppendUnicodeCodePointValuePercentEncoded(this.rawPath.Substring(i + 1, 4)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 5;
					}
					else
					{
						if (!this.AddPercentEncodedOctetToRawOctetsList(encoding, this.rawPath.Substring(i, 2)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 2;
					}
				}
				else
				{
					if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
					{
						return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
					}
					this.requestUriString.Append(c);
					i++;
				}
			}
			if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
			{
				return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
			}
			return HttpListenerRequestUriBuilder.ParsingResult.Success;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000A60AC File Offset: 0x000A42AC
		private bool AppendUnicodeCodePointValuePercentEncoded(string codePoint)
		{
			int utf;
			if (!int.TryParse(codePoint, NumberStyles.HexNumber, null, out utf))
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[]
				{
					codePoint
				});
				return false;
			}
			string text = null;
			try
			{
				text = char.ConvertFromUtf32(utf);
				HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[]
				{
					codePoint
				});
			}
			catch (EncoderFallbackException ex)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[]
				{
					text,
					ex.Message
				});
			}
			return false;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000A616C File Offset: 0x000A436C
		private bool AddPercentEncodedOctetToRawOctetsList(Encoding encoding, string escapedCharacter)
		{
			byte item;
			if (!byte.TryParse(escapedCharacter, NumberStyles.HexNumber, null, out item))
			{
				this.LogWarning("AddPercentEncodedOctetToRawOctetsList", "Can't convert percent encoded value '{0}'.", new object[]
				{
					escapedCharacter
				});
				return false;
			}
			this.rawOctets.Add(item);
			return true;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000A61B4 File Offset: 0x000A43B4
		private bool EmptyDecodeAndAppendRawOctetsList(Encoding encoding)
		{
			if (this.rawOctets.Count == 0)
			{
				return true;
			}
			string text = null;
			try
			{
				text = encoding.GetString(this.rawOctets.ToArray());
				if (encoding == HttpListenerRequestUriBuilder.utf8Encoding)
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, this.rawOctets.ToArray());
				}
				else
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				}
				this.rawOctets.Clear();
				return true;
			}
			catch (DecoderFallbackException ex)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert bytes '{0}' into UTF-16 characters: {1}", new object[]
				{
					HttpListenerRequestUriBuilder.GetOctetsAsString(this.rawOctets),
					ex.Message
				});
			}
			catch (EncoderFallbackException ex2)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[]
				{
					text,
					ex2.Message
				});
			}
			return false;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000A62A0 File Offset: 0x000A44A0
		private static void AppendOctetsPercentEncoded(StringBuilder target, IEnumerable<byte> octets)
		{
			foreach (byte b in octets)
			{
				target.Append('%');
				target.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000A6304 File Offset: 0x000A4504
		private static string GetOctetsAsString(IEnumerable<byte> octets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (byte b in octets)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000A6380 File Offset: 0x000A4580
		private static string GetPath(string uriString)
		{
			int num = 0;
			if (uriString[0] != '/')
			{
				int num2 = 0;
				if (uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 7;
				}
				else if (uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 8;
				}
				if (num2 > 0)
				{
					num = uriString.IndexOf('/', num2);
					if (num == -1)
					{
						num = uriString.Length;
					}
				}
				else
				{
					uriString = "/" + uriString;
				}
			}
			int num3 = uriString.IndexOf('?');
			if (num3 == -1)
			{
				num3 = uriString.Length;
			}
			return HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(uriString.Substring(num, num3 - num));
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000A6409 File Offset: 0x000A4609
		private static string AddSlashToAsteriskOnlyPath(string path)
		{
			if (path.Length == 1 && path[0] == '*')
			{
				return "/*";
			}
			return path;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000A6426 File Offset: 0x000A4626
		private void LogWarning(string methodName, string message, params object[] args)
		{
			bool on = Logging.On;
		}

		// Token: 0x04001A9D RID: 6813
		private static readonly bool useCookedRequestUrl = SettingsSectionInternal.Section.HttpListenerUnescapeRequestUrl;

		// Token: 0x04001A9E RID: 6814
		private static readonly Encoding utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04001A9F RID: 6815
		private static readonly Encoding ansiEncoding = Encoding.GetEncoding(0, new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x04001AA0 RID: 6816
		private readonly string rawUri;

		// Token: 0x04001AA1 RID: 6817
		private readonly string cookedUriScheme;

		// Token: 0x04001AA2 RID: 6818
		private readonly string cookedUriHost;

		// Token: 0x04001AA3 RID: 6819
		private readonly string cookedUriPath;

		// Token: 0x04001AA4 RID: 6820
		private readonly string cookedUriQuery;

		// Token: 0x04001AA5 RID: 6821
		private StringBuilder requestUriString;

		// Token: 0x04001AA6 RID: 6822
		private List<byte> rawOctets;

		// Token: 0x04001AA7 RID: 6823
		private string rawPath;

		// Token: 0x04001AA8 RID: 6824
		private Uri requestUri;

		// Token: 0x020005D0 RID: 1488
		private enum ParsingResult
		{
			// Token: 0x04001AAA RID: 6826
			Success,
			// Token: 0x04001AAB RID: 6827
			InvalidString,
			// Token: 0x04001AAC RID: 6828
			EncodingError
		}

		// Token: 0x020005D1 RID: 1489
		private enum EncodingType
		{
			// Token: 0x04001AAE RID: 6830
			Primary,
			// Token: 0x04001AAF RID: 6831
			Secondary
		}
	}
}
