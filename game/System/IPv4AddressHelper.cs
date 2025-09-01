using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000141 RID: 321
	internal static class IPv4AddressHelper
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x0001ED08 File Offset: 0x0001CF08
		internal unsafe static int ParseHostNumber(ReadOnlySpan<char> str, int start, int end)
		{
			byte* ptr = stackalloc byte[(UIntPtr)4];
			IPv4AddressHelper.ParseCanonical(str, ptr, start, end);
			return ((int)(*ptr) << 24) + ((int)ptr[1] << 16) + ((int)ptr[2] << 8) + (int)ptr[3];
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001ED3D File Offset: 0x0001CF3D
		internal unsafe static bool IsValid(char* name, int start, ref int end, bool allowIPv6, bool notImplicitFile, bool unknownScheme)
		{
			if (allowIPv6 || unknownScheme)
			{
				return IPv4AddressHelper.IsValidCanonical(name, start, ref end, allowIPv6, notImplicitFile);
			}
			return IPv4AddressHelper.ParseNonCanonical(name, start, ref end, notImplicitFile) != -1L;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001ED64 File Offset: 0x0001CF64
		private unsafe static bool ParseCanonical(ReadOnlySpan<char> name, byte* numbers, int start, int end)
		{
			for (int i = 0; i < 4; i++)
			{
				byte b = 0;
				char c;
				while (start < end && (c = (char)(*name[start])) != '.' && c != ':')
				{
					b = b * 10 + (byte)(c - '0');
					start++;
				}
				numbers[i] = b;
				start++;
			}
			return *numbers == 127;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		internal unsafe static bool IsValidCanonical(char* name, int start, ref int end, bool allowIPv6, bool notImplicitFile)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			while (start < end)
			{
				char c = name[start];
				if (allowIPv6)
				{
					if (c == ']' || c == '/')
					{
						break;
					}
					if (c == '%')
					{
						break;
					}
				}
				else if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					break;
				}
				if (c <= '9' && c >= '0')
				{
					if (!flag && c == '0')
					{
						if (start + 1 < end && name[start + 1] == '0')
						{
							return false;
						}
						flag2 = true;
					}
					flag = true;
					num2 = num2 * 10 + (int)(name[start] - '0');
					if (num2 > 255)
					{
						return false;
					}
				}
				else
				{
					if (c != '.')
					{
						return false;
					}
					if (!flag || (num2 > 0 && flag2))
					{
						return false;
					}
					num++;
					flag = false;
					num2 = 0;
					flag2 = false;
				}
				start++;
			}
			bool flag3 = num == 3 && flag;
			if (flag3)
			{
				end = start;
			}
			return flag3;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
		internal unsafe static long ParseNonCanonical(char* name, int start, ref int end, bool notImplicitFile)
		{
			long* ptr = stackalloc long[(UIntPtr)32];
			long num = 0L;
			bool flag = false;
			int num2 = 0;
			int i;
			for (i = start; i < end; i++)
			{
				char c = name[i];
				num = 0L;
				int num3 = 10;
				if (c == '0')
				{
					num3 = 8;
					i++;
					flag = true;
					if (i < end)
					{
						c = name[i];
						if (c == 'x' || c == 'X')
						{
							num3 = 16;
							i++;
							flag = false;
						}
					}
				}
				while (i < end)
				{
					c = name[i];
					int num4;
					if ((num3 == 10 || num3 == 16) && '0' <= c && c <= '9')
					{
						num4 = (int)(c - '0');
					}
					else if (num3 == 8 && '0' <= c && c <= '7')
					{
						num4 = (int)(c - '0');
					}
					else if (num3 == 16 && 'a' <= c && c <= 'f')
					{
						num4 = (int)(c + '\n' - 'a');
					}
					else
					{
						if (num3 != 16 || 'A' > c || c > 'F')
						{
							break;
						}
						num4 = (int)(c + '\n' - 'A');
					}
					num = num * (long)num3 + (long)num4;
					if (num > (long)((ulong)-1))
					{
						return -1L;
					}
					flag = true;
					i++;
				}
				if (i >= end || name[i] != '.')
				{
					break;
				}
				if (num2 >= 3 || !flag || num > 255L)
				{
					return -1L;
				}
				ptr[num2] = num;
				num2++;
				flag = false;
			}
			if (!flag)
			{
				return -1L;
			}
			if (i < end)
			{
				char c;
				if ((c = name[i]) != '/' && c != '\\' && (!notImplicitFile || (c != ':' && c != '?' && c != '#')))
				{
					return -1L;
				}
				end = i;
			}
			ptr[num2] = num;
			switch (num2)
			{
			case 0:
				if (*ptr > (long)((ulong)-1))
				{
					return -1L;
				}
				return *ptr;
			case 1:
				if (ptr[1] > 16777215L)
				{
					return -1L;
				}
				return *ptr << 24 | (ptr[1] & 16777215L);
			case 2:
				if (ptr[2] > 65535L)
				{
					return -1L;
				}
				return *ptr << 24 | (ptr[1] & 255L) << 16 | (ptr[2] & 65535L);
			case 3:
				if (ptr[3] > 255L)
				{
					return -1L;
				}
				return *ptr << 24 | (ptr[1] & 255L) << 16 | (ptr[2] & 255L) << 8 | (ptr[3] & 255L);
			default:
				return -1L;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001F110 File Offset: 0x0001D310
		internal unsafe static string ParseCanonicalName(string str, int start, int end, ref bool isLoopback)
		{
			byte* ptr = stackalloc byte[(UIntPtr)4];
			isLoopback = IPv4AddressHelper.Parse(str, ptr, start, end);
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)30], 15);
			int num = 0;
			int num2;
			for (int i = 0; i < 3; i++)
			{
				ptr[i].TryFormat(span.Slice(num), out num2, default(ReadOnlySpan<char>), null);
				int num3 = num + num2;
				*span[num3] = '.';
				num = num3 + 1;
			}
			ptr[3].TryFormat(span.Slice(num), out num2, default(ReadOnlySpan<char>), null);
			return new string(span.Slice(0, num + num2));
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001F1B4 File Offset: 0x0001D3B4
		private unsafe static bool Parse(string name, byte* numbers, int start, int end)
		{
			fixed (string text = name)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = end;
				long num2 = IPv4AddressHelper.ParseNonCanonical(ptr, start, ref num, true);
				*numbers = (byte)(num2 >> 24);
				numbers[1] = (byte)(num2 >> 16);
				numbers[2] = (byte)(num2 >> 8);
				numbers[3] = (byte)num2;
			}
			return *numbers == 127;
		}

		// Token: 0x0400053F RID: 1343
		internal const long Invalid = -1L;

		// Token: 0x04000540 RID: 1344
		private const long MaxIPv4Value = 4294967295L;

		// Token: 0x04000541 RID: 1345
		private const int Octal = 8;

		// Token: 0x04000542 RID: 1346
		private const int Decimal = 10;

		// Token: 0x04000543 RID: 1347
		private const int Hex = 16;

		// Token: 0x04000544 RID: 1348
		private const int NumberOfLabels = 4;
	}
}
