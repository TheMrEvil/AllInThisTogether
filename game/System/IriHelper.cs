using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200014D RID: 333
	internal static class IriHelper
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00020BEC File Offset: 0x0001EDEC
		internal static bool CheckIriUnicodeRange(char unicode, bool isQuery)
		{
			return (unicode >= '\u00a0' && unicode <= '퟿') || (unicode >= '豈' && unicode <= '﷏') || (unicode >= 'ﷰ' && unicode <= '￯') || (isQuery && unicode >= '' && unicode <= '');
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00020C44 File Offset: 0x0001EE44
		internal static bool CheckIriUnicodeRange(char highSurr, char lowSurr, ref bool surrogatePair, bool isQuery)
		{
			bool result = false;
			surrogatePair = false;
			if (char.IsSurrogatePair(highSurr, lowSurr))
			{
				surrogatePair = true;
				string strA = new string(new char[]
				{
					highSurr,
					lowSurr
				});
				if ((string.CompareOrdinal(strA, "\ud800\udc00") >= 0 && string.CompareOrdinal(strA, "\ud83f\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud840\udc00") >= 0 && string.CompareOrdinal(strA, "\ud87f\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud880\udc00") >= 0 && string.CompareOrdinal(strA, "\ud8bf\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud8c0\udc00") >= 0 && string.CompareOrdinal(strA, "\ud8ff\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud900\udc00") >= 0 && string.CompareOrdinal(strA, "\ud93f\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud940\udc00") >= 0 && string.CompareOrdinal(strA, "\ud97f\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud980\udc00") >= 0 && string.CompareOrdinal(strA, "\ud9bf\udffd") <= 0) || (string.CompareOrdinal(strA, "\ud9c0\udc00") >= 0 && string.CompareOrdinal(strA, "\ud9ff\udffd") <= 0) || (string.CompareOrdinal(strA, "\uda00\udc00") >= 0 && string.CompareOrdinal(strA, "\uda3f\udffd") <= 0) || (string.CompareOrdinal(strA, "\uda40\udc00") >= 0 && string.CompareOrdinal(strA, "\uda7f\udffd") <= 0) || (string.CompareOrdinal(strA, "\uda80\udc00") >= 0 && string.CompareOrdinal(strA, "\udabf\udffd") <= 0) || (string.CompareOrdinal(strA, "\udac0\udc00") >= 0 && string.CompareOrdinal(strA, "\udaff\udffd") <= 0) || (string.CompareOrdinal(strA, "\udb00\udc00") >= 0 && string.CompareOrdinal(strA, "\udb3f\udffd") <= 0) || (string.CompareOrdinal(strA, "\udb44\udc00") >= 0 && string.CompareOrdinal(strA, "\udb7f\udffd") <= 0) || (isQuery && ((string.CompareOrdinal(strA, "\udb80\udc00") >= 0 && string.CompareOrdinal(strA, "\udbbf\udffd") <= 0) || (string.CompareOrdinal(strA, "\udbc0\udc00") >= 0 && string.CompareOrdinal(strA, "\udbff\udffd") <= 0))))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00020E60 File Offset: 0x0001F060
		internal static bool CheckIsReserved(char ch, UriComponents component)
		{
			if (component != UriComponents.Scheme && component != UriComponents.UserInfo && component != UriComponents.Host && component != UriComponents.Port && component != UriComponents.Path && component != UriComponents.Query && component != UriComponents.Fragment)
			{
				return component == (UriComponents)0 && Uri.IsGenDelim(ch);
			}
			if (component <= UriComponents.Host)
			{
				if (component != UriComponents.UserInfo)
				{
					if (component == UriComponents.Host)
					{
						if (ch == ':' || ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']' || ch == '@')
						{
							return true;
						}
					}
				}
				else if (ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']' || ch == '@')
				{
					return true;
				}
			}
			else if (component != UriComponents.Path)
			{
				if (component != UriComponents.Query)
				{
					if (component == UriComponents.Fragment)
					{
						if (ch == '#' || ch == '[' || ch == ']')
						{
							return true;
						}
					}
				}
				else if (ch == '#' || ch == '[' || ch == ']')
				{
					return true;
				}
			}
			else if (ch == '/' || ch == '?' || ch == '#' || ch == '[' || ch == ']')
			{
				return true;
			}
			return false;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00020F40 File Offset: 0x0001F140
		internal unsafe static string EscapeUnescapeIri(char* pInput, int start, int end, UriComponents component)
		{
			char[] array = new char[end - start];
			byte[] array2 = null;
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			char* ptr = (char*)((void*)gchandle.AddrOfPinnedObject());
			int num = 0;
			int i = start;
			int num2 = 0;
			bool flag = false;
			while (i < end)
			{
				bool flag2 = false;
				flag = false;
				char c;
				if ((c = pInput[i]) == '%')
				{
					if (i + 2 >= end)
					{
						ptr[(IntPtr)(num2++) * 2] = pInput[i];
						goto IL_2E6;
					}
					c = UriHelper.EscapedAscii(pInput[i + 1], pInput[i + 2]);
					if (c == '￿' || c == '%' || IriHelper.CheckIsReserved(c, component) || UriHelper.IsNotSafeForUnescape(c))
					{
						ptr[(IntPtr)(num2++) * 2] = pInput[i++];
						ptr[(IntPtr)(num2++) * 2] = pInput[i++];
						ptr[(IntPtr)(num2++) * 2] = pInput[i];
					}
					else if (c <= '\u007f')
					{
						ptr[(IntPtr)(num2++) * 2] = c;
						i += 2;
					}
					else
					{
						int num3 = i;
						int byteCount = 1;
						if (array2 == null)
						{
							array2 = new byte[end - i];
						}
						array2[0] = (byte)c;
						i += 3;
						while (i < end && pInput[i] == '%' && i + 2 < end)
						{
							c = UriHelper.EscapedAscii(pInput[i + 1], pInput[i + 2]);
							if (c == '￿' || c < '\u0080')
							{
								break;
							}
							array2[byteCount++] = (byte)c;
							i += 3;
						}
						i--;
						Encoding encoding = (Encoding)Encoding.UTF8.Clone();
						encoding.EncoderFallback = new EncoderReplacementFallback("");
						encoding.DecoderFallback = new DecoderReplacementFallback("");
						char[] array3 = new char[array2.Length];
						int chars = encoding.GetChars(array2, 0, byteCount, array3, 0);
						if (chars != 0)
						{
							UriHelper.MatchUTF8Sequence(ptr, array, ref num2, array3, chars, array2, byteCount, component == UriComponents.Query, true);
							goto IL_2E6;
						}
						for (int j = num3; j <= i; j++)
						{
							ptr[(IntPtr)(num2++) * 2] = pInput[j];
						}
						goto IL_2E6;
					}
				}
				else
				{
					if (c <= '\u007f')
					{
						ptr[(IntPtr)(num2++) * 2] = pInput[i];
						goto IL_2E6;
					}
					if (char.IsHighSurrogate(c) && i + 1 < end)
					{
						char lowSurr = pInput[i + 1];
						flag2 = !IriHelper.CheckIriUnicodeRange(c, lowSurr, ref flag, component == UriComponents.Query);
						if (!flag2)
						{
							ptr[(IntPtr)(num2++) * 2] = pInput[i++];
							ptr[(IntPtr)(num2++) * 2] = pInput[i];
							goto IL_2E6;
						}
						goto IL_2E6;
					}
					else
					{
						if (!IriHelper.CheckIriUnicodeRange(c, component == UriComponents.Query))
						{
							flag2 = true;
							goto IL_2E6;
						}
						if (!Uri.IsBidiControlCharacter(c))
						{
							ptr[(IntPtr)(num2++) * 2] = pInput[i];
							goto IL_2E6;
						}
						goto IL_2E6;
					}
				}
				IL_3C6:
				i++;
				continue;
				IL_2E6:
				if (flag2)
				{
					if (num < 12)
					{
						char[] array5;
						char[] array4;
						char* dest;
						checked
						{
							int num4 = array.Length + 90;
							num += 90;
							array4 = (array5 = new char[num4]);
							if (array4 == null || array5.Length == 0)
							{
								dest = null;
							}
							else
							{
								dest = &array5[0];
							}
						}
						Buffer.Memcpy((byte*)dest, (byte*)ptr, num2 * 2, true);
						array5 = null;
						if (gchandle.IsAllocated)
						{
							gchandle.Free();
						}
						array = array4;
						gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
						ptr = (char*)((void*)gchandle.AddrOfPinnedObject());
					}
					byte[] array6 = new byte[4];
					byte[] array7;
					byte* bytes;
					if ((array7 = array6) == null || array7.Length == 0)
					{
						bytes = null;
					}
					else
					{
						bytes = &array7[0];
					}
					int bytes2 = Encoding.UTF8.GetBytes(pInput + i, flag ? 2 : 1, bytes, 4);
					num -= bytes2 * 3;
					for (int k = 0; k < bytes2; k++)
					{
						UriHelper.EscapeAsciiChar((char)array6[k], array, ref num2);
					}
					array7 = null;
					goto IL_3C6;
				}
				goto IL_3C6;
			}
			if (gchandle.IsAllocated)
			{
				gchandle.Free();
			}
			return new string(array, 0, num2);
		}
	}
}
