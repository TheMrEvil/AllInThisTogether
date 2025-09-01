using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000646 RID: 1606
	internal abstract class WebProxyDataBuilder
	{
		// Token: 0x06003276 RID: 12918 RVA: 0x000AEAB2 File Offset: 0x000ACCB2
		public WebProxyData Build()
		{
			this.m_Result = new WebProxyData();
			this.BuildInternal();
			return this.m_Result;
		}

		// Token: 0x06003277 RID: 12919
		protected abstract void BuildInternal();

		// Token: 0x06003278 RID: 12920 RVA: 0x000AEACC File Offset: 0x000ACCCC
		protected void SetProxyAndBypassList(string addressString, string bypassListString)
		{
			if (addressString != null)
			{
				addressString = addressString.Trim();
				if (addressString != string.Empty)
				{
					if (addressString.IndexOf('=') == -1)
					{
						this.m_Result.proxyAddress = WebProxyDataBuilder.ParseProxyUri(addressString);
					}
					else
					{
						this.m_Result.proxyHostAddresses = WebProxyDataBuilder.ParseProtocolProxies(addressString);
					}
					if (bypassListString != null)
					{
						bypassListString = bypassListString.Trim();
						if (bypassListString != string.Empty)
						{
							bool bypassOnLocal = false;
							this.m_Result.bypassList = WebProxyDataBuilder.ParseBypassList(bypassListString, out bypassOnLocal);
							this.m_Result.bypassOnLocal = bypassOnLocal;
						}
					}
				}
			}
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000AEB5C File Offset: 0x000ACD5C
		protected void SetAutoProxyUrl(string autoConfigUrl)
		{
			if (!string.IsNullOrEmpty(autoConfigUrl))
			{
				Uri scriptLocation = null;
				if (Uri.TryCreate(autoConfigUrl, UriKind.Absolute, out scriptLocation))
				{
					this.m_Result.scriptLocation = scriptLocation;
				}
			}
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000AEB8A File Offset: 0x000ACD8A
		protected void SetAutoDetectSettings(bool value)
		{
			this.m_Result.automaticallyDetectSettings = value;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000AEB98 File Offset: 0x000ACD98
		private static Uri ParseProxyUri(string proxyString)
		{
			if (proxyString.IndexOf("://") == -1)
			{
				proxyString = "http://" + proxyString;
			}
			Uri result;
			try
			{
				result = new Uri(proxyString);
			}
			catch (UriFormatException)
			{
				bool on = Logging.On;
				throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyString);
			}
			return result;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000AEBEC File Offset: 0x000ACDEC
		private static Hashtable ParseProtocolProxies(string proxyListString)
		{
			string[] array = proxyListString.Split(';', StringSplitOptions.None);
			Hashtable hashtable = new Hashtable(CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!(text == string.Empty))
				{
					string[] array2 = text.Split('=', StringSplitOptions.None);
					if (array2.Length != 2)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					array2[0] = array2[0].Trim();
					array2[1] = array2[1].Trim();
					if (array2[0] == string.Empty || array2[1] == string.Empty)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					hashtable[array2[0]] = WebProxyDataBuilder.ParseProxyUri(array2[1]);
				}
			}
			return hashtable;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000AECA6 File Offset: 0x000ACEA6
		private static FormatException CreateInvalidProxyStringException(string originalProxyString)
		{
			string @string = SR.GetString("The system proxy settings contain an invalid proxy server setting: '{0}'.", new object[]
			{
				originalProxyString
			});
			bool on = Logging.On;
			return new FormatException(@string);
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000AECC8 File Offset: 0x000ACEC8
		private static string BypassStringEscape(string rawString)
		{
			Match match = new Regex("^(?<scheme>.*://)?(?<host>[^:]*)(?<port>:[0-9]{1,5})?$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Match(rawString);
			string text;
			string text2;
			string text3;
			if (match.Success)
			{
				text = match.Groups["scheme"].Value;
				text2 = match.Groups["host"].Value;
				text3 = match.Groups["port"].Value;
			}
			else
			{
				text = string.Empty;
				text2 = rawString;
				text3 = string.Empty;
			}
			text = WebProxyDataBuilder.ConvertRegexReservedChars(text);
			text2 = WebProxyDataBuilder.ConvertRegexReservedChars(text2);
			text3 = WebProxyDataBuilder.ConvertRegexReservedChars(text3);
			if (text == string.Empty)
			{
				text = "(?:.*://)?";
			}
			if (text3 == string.Empty)
			{
				text3 = "(?::[0-9]{1,5})?";
			}
			return string.Concat(new string[]
			{
				"^",
				text,
				text2,
				text3,
				"$"
			});
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000AEDA8 File Offset: 0x000ACFA8
		private static string ConvertRegexReservedChars(string rawString)
		{
			if (rawString.Length == 0)
			{
				return rawString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in rawString)
			{
				if ("#$()+.?[\\^{|".IndexOf(c) != -1)
				{
					stringBuilder.Append('\\');
				}
				else if (c == '*')
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000AEE18 File Offset: 0x000AD018
		private static ArrayList ParseBypassList(string bypassListString, out bool bypassOnLocal)
		{
			string[] array = bypassListString.Split(';', StringSplitOptions.None);
			bypassOnLocal = false;
			if (array.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = null;
			foreach (string text in array)
			{
				if (text != null)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						if (string.Compare(text2, "<local>", StringComparison.OrdinalIgnoreCase) == 0)
						{
							bypassOnLocal = true;
						}
						else
						{
							text2 = WebProxyDataBuilder.BypassStringEscape(text2);
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x0000219B File Offset: 0x0000039B
		protected WebProxyDataBuilder()
		{
		}

		// Token: 0x04001D85 RID: 7557
		private const char addressListDelimiter = ';';

		// Token: 0x04001D86 RID: 7558
		private const char addressListSchemeValueDelimiter = '=';

		// Token: 0x04001D87 RID: 7559
		private const char bypassListDelimiter = ';';

		// Token: 0x04001D88 RID: 7560
		private WebProxyData m_Result;

		// Token: 0x04001D89 RID: 7561
		private const string regexReserved = "#$()+.?[\\^{|";
	}
}
