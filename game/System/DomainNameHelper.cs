using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000169 RID: 361
	internal class DomainNameHelper
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x0000219B File Offset: 0x0000039B
		private DomainNameHelper()
		{
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002ACD8 File Offset: 0x00028ED8
		internal static string ParseCanonicalName(string str, int start, int end, ref bool loopback)
		{
			string text = null;
			for (int i = end - 1; i >= start; i--)
			{
				if (str[i] >= 'A' && str[i] <= 'Z')
				{
					text = str.Substring(start, end - start).ToLower(CultureInfo.InvariantCulture);
					break;
				}
				if (str[i] == ':')
				{
					end = i;
				}
			}
			if (text == null)
			{
				text = str.Substring(start, end - start);
			}
			if (text == "localhost" || text == "loopback")
			{
				loopback = true;
				return "localhost";
			}
			return text;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002AD64 File Offset: 0x00028F64
		internal unsafe static bool IsValid(char* name, ushort pos, ref int returnedEnd, ref bool notCanonical, bool notImplicitFile)
		{
			char* ptr = name + pos;
			char* ptr2 = ptr;
			char* ptr3 = name + returnedEnd;
			while (ptr2 < ptr3)
			{
				char c = *ptr2;
				if (c > '\u007f')
				{
					return false;
				}
				if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					ptr3 = ptr2;
					break;
				}
				ptr2++;
			}
			if (ptr3 == ptr)
			{
				return false;
			}
			for (;;)
			{
				ptr2 = ptr;
				while (ptr2 < ptr3 && *ptr2 != '.')
				{
					ptr2++;
				}
				if (ptr == ptr2 || (long)(ptr2 - ptr) > 63L || !DomainNameHelper.IsASCIILetterOrDigit(*(ptr++), ref notCanonical))
				{
					break;
				}
				while (ptr < ptr2)
				{
					if (!DomainNameHelper.IsValidDomainLabelCharacter(*(ptr++), ref notCanonical))
					{
						return false;
					}
				}
				ptr++;
				if (ptr >= ptr3)
				{
					goto Block_13;
				}
			}
			return false;
			Block_13:
			returnedEnd = (int)((ushort)((long)(ptr3 - name)));
			return true;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002AE14 File Offset: 0x00029014
		internal unsafe static bool IsValidByIri(char* name, ushort pos, ref int returnedEnd, ref bool notCanonical, bool notImplicitFile)
		{
			char* ptr = name + pos;
			char* ptr2 = ptr;
			char* ptr3 = name + returnedEnd;
			while (ptr2 < ptr3)
			{
				char c = *ptr2;
				if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					ptr3 = ptr2;
					break;
				}
				ptr2++;
			}
			if (ptr3 == ptr)
			{
				return false;
			}
			for (;;)
			{
				ptr2 = ptr;
				int num = 0;
				bool flag = false;
				while (ptr2 < ptr3 && *ptr2 != '.' && *ptr2 != '。' && *ptr2 != '．' && *ptr2 != '｡')
				{
					num++;
					if (*ptr2 > 'ÿ')
					{
						num++;
					}
					if (*ptr2 >= '\u00a0')
					{
						flag = true;
					}
					ptr2++;
				}
				if (ptr == ptr2 || (flag ? (num + 4) : num) > 63 || (*(ptr++) < '\u00a0' && !DomainNameHelper.IsASCIILetterOrDigit(*(ptr - 1), ref notCanonical)))
				{
					break;
				}
				while (ptr < ptr2)
				{
					if (*(ptr++) < '\u00a0' && !DomainNameHelper.IsValidDomainLabelCharacter(*(ptr - 1), ref notCanonical))
					{
						return false;
					}
				}
				ptr++;
				if (ptr >= ptr3)
				{
					goto Block_20;
				}
			}
			return false;
			Block_20:
			returnedEnd = (int)((ushort)((long)(ptr3 - name)));
			return true;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002AF20 File Offset: 0x00029120
		internal unsafe static string IdnEquivalent(string hostname)
		{
			bool flag = true;
			bool flag2 = false;
			char* ptr = hostname;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return DomainNameHelper.IdnEquivalent(ptr, 0, hostname.Length, ref flag, ref flag2);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002AF54 File Offset: 0x00029154
		internal unsafe static string IdnEquivalent(char* hostname, int start, int end, ref bool allAscii, ref bool atLeastOneValidIdn)
		{
			string text = null;
			string text2 = DomainNameHelper.IdnEquivalent(hostname, start, end, ref allAscii, ref text);
			if (text2 != null)
			{
				string text4;
				string text3 = text4 = (allAscii ? text2 : text);
				char* ptr = text4;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int length = text3.Length;
				int i = 0;
				int num = 0;
				bool flag = false;
				do
				{
					bool flag2 = false;
					bool flag3 = false;
					flag = false;
					for (i = num; i < length; i++)
					{
						char c = ptr[i];
						if (!flag3)
						{
							flag3 = true;
							if (i + 3 < length && DomainNameHelper.IsIdnAce(ptr, i))
							{
								i += 4;
								flag2 = true;
								continue;
							}
						}
						if (c == '.' || c == '。' || c == '．' || c == '｡')
						{
							flag = true;
							break;
						}
					}
					if (flag2)
					{
						try
						{
							new IdnMapping().GetUnicode(new string(ptr, num, i - num));
							atLeastOneValidIdn = true;
							break;
						}
						catch (ArgumentException)
						{
						}
					}
					num = i + (flag ? 1 : 0);
				}
				while (num < length);
				text4 = null;
			}
			else
			{
				atLeastOneValidIdn = false;
			}
			return text2;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002B068 File Offset: 0x00029268
		internal unsafe static string IdnEquivalent(char* hostname, int start, int end, ref bool allAscii, ref string bidiStrippedHost)
		{
			string result = null;
			if (end <= start)
			{
				return result;
			}
			int i = start;
			allAscii = true;
			while (i < end)
			{
				if (hostname[i] > '\u007f')
				{
					allAscii = false;
					break;
				}
				i++;
			}
			if (!allAscii)
			{
				IdnMapping idnMapping = new IdnMapping();
				bidiStrippedHost = Uri.StripBidiControlCharacter(hostname, start, end - start);
				string ascii;
				try
				{
					ascii = idnMapping.GetAscii(bidiStrippedHost);
				}
				catch (ArgumentException)
				{
					throw new UriFormatException(SR.GetString("An invalid Unicode character by IDN standards was specified in the host."));
				}
				return ascii;
			}
			string text = new string(hostname, start, end - start);
			if (text == null)
			{
				return null;
			}
			return text.ToLowerInvariant();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002B0FC File Offset: 0x000292FC
		private static bool IsIdnAce(string input, int index)
		{
			return input[index] == 'x' && input[index + 1] == 'n' && input[index + 2] == '-' && input[index + 3] == '-';
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002B133 File Offset: 0x00029333
		private unsafe static bool IsIdnAce(char* input, int index)
		{
			return input[index] == 'x' && input[index + 1] == 'n' && input[index + 2] == '-' && input[index + 3] == '-';
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002B16C File Offset: 0x0002936C
		internal unsafe static string UnicodeEquivalent(string idnHost, char* hostname, int start, int end)
		{
			IdnMapping idnMapping = new IdnMapping();
			try
			{
				return idnMapping.GetUnicode(idnHost);
			}
			catch (ArgumentException)
			{
			}
			bool flag = true;
			return DomainNameHelper.UnicodeEquivalent(hostname, start, end, ref flag, ref flag);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002B1AC File Offset: 0x000293AC
		internal unsafe static string UnicodeEquivalent(char* hostname, int start, int end, ref bool allAscii, ref bool atLeastOneValidIdn)
		{
			IdnMapping idnMapping = new IdnMapping();
			allAscii = true;
			atLeastOneValidIdn = false;
			string result = null;
			if (end <= start)
			{
				return result;
			}
			string text = Uri.StripBidiControlCharacter(hostname, start, end - start);
			string text2 = null;
			int num = 0;
			int i = 0;
			int length = text.Length;
			bool flag = false;
			do
			{
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				flag = false;
				for (i = num; i < length; i++)
				{
					char c = text[i];
					if (!flag4)
					{
						flag4 = true;
						if (i + 3 < length && c == 'x' && DomainNameHelper.IsIdnAce(text, i))
						{
							flag3 = true;
						}
					}
					if (flag2 && c > '\u007f')
					{
						flag2 = false;
						allAscii = false;
					}
					if (c == '.' || c == '。' || c == '．' || c == '｡')
					{
						flag = true;
						break;
					}
				}
				if (!flag2)
				{
					string text3 = text.Substring(num, i - num);
					try
					{
						text3 = idnMapping.GetAscii(text3);
					}
					catch (ArgumentException)
					{
						throw new UriFormatException(SR.GetString("An invalid Unicode character by IDN standards was specified in the host."));
					}
					text2 += idnMapping.GetUnicode(text3);
					if (flag)
					{
						text2 += ".";
					}
				}
				else
				{
					bool flag5 = false;
					if (flag3)
					{
						try
						{
							text2 += idnMapping.GetUnicode(text.Substring(num, i - num));
							if (flag)
							{
								text2 += ".";
							}
							flag5 = true;
							atLeastOneValidIdn = true;
						}
						catch (ArgumentException)
						{
						}
					}
					if (!flag5)
					{
						text2 += text.Substring(num, i - num).ToLowerInvariant();
						if (flag)
						{
							text2 += ".";
						}
					}
				}
				num = i + (flag ? 1 : 0);
			}
			while (num < length);
			return text2;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002B364 File Offset: 0x00029564
		private static bool IsASCIILetterOrDigit(char character, ref bool notCanonical)
		{
			if ((character >= 'a' && character <= 'z') || (character >= '0' && character <= '9'))
			{
				return true;
			}
			if (character >= 'A' && character <= 'Z')
			{
				notCanonical = true;
				return true;
			}
			return false;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002B38C File Offset: 0x0002958C
		private static bool IsValidDomainLabelCharacter(char character, ref bool notCanonical)
		{
			if ((character >= 'a' && character <= 'z') || (character >= '0' && character <= '9') || character == '-' || character == '_')
			{
				return true;
			}
			if (character >= 'A' && character <= 'Z')
			{
				notCanonical = true;
				return true;
			}
			return false;
		}

		// Token: 0x0400067E RID: 1662
		private const char c_DummyChar = '￿';

		// Token: 0x0400067F RID: 1663
		internal const string Localhost = "localhost";

		// Token: 0x04000680 RID: 1664
		internal const string Loopback = "loopback";
	}
}
