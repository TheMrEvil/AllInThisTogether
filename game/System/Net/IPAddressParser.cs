using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x0200057E RID: 1406
	internal class IPAddressParser
	{
		// Token: 0x06002D86 RID: 11654 RVA: 0x0009BC88 File Offset: 0x00099E88
		internal unsafe static IPAddress Parse(ReadOnlySpan<char> ipSpan, bool tryParse)
		{
			long newAddress;
			if (ipSpan.Contains(':'))
			{
				ushort* ptr = stackalloc ushort[(UIntPtr)16];
				new Span<ushort>((void*)ptr, 8).Clear();
				uint scopeid;
				if (IPAddressParser.Ipv6StringToAddress(ipSpan, ptr, 8, out scopeid))
				{
					return new IPAddress(ptr, 8, scopeid);
				}
			}
			else if (IPAddressParser.Ipv4StringToAddress(ipSpan, out newAddress))
			{
				return new IPAddress(newAddress);
			}
			if (tryParse)
			{
				return null;
			}
			throw new FormatException("An invalid IP address was specified.", new SocketException(SocketError.InvalidArgument));
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0009BCF4 File Offset: 0x00099EF4
		internal unsafe static string IPv4AddressToString(uint address)
		{
			char* ptr = stackalloc char[(UIntPtr)30];
			int length = IPAddressParser.IPv4AddressToStringHelper(address, ptr);
			return new string(ptr, 0, length);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0009BD18 File Offset: 0x00099F18
		internal unsafe static void IPv4AddressToString(uint address, StringBuilder destination)
		{
			char* ptr = stackalloc char[(UIntPtr)30];
			int valueCount = IPAddressParser.IPv4AddressToStringHelper(address, ptr);
			destination.Append(ptr, valueCount);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0009BD3C File Offset: 0x00099F3C
		internal unsafe static bool IPv4AddressToString(uint address, Span<char> formatted, out int charsWritten)
		{
			if (formatted.Length < 15)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(formatted))
			{
				char* addressString = reference;
				charsWritten = IPAddressParser.IPv4AddressToStringHelper(address, addressString);
			}
			return true;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0009BD70 File Offset: 0x00099F70
		private unsafe static int IPv4AddressToStringHelper(uint address, char* addressString)
		{
			int result = 0;
			IPAddressParser.FormatIPv4AddressNumber((int)(address & 255U), addressString, ref result);
			addressString[result++] = '.';
			IPAddressParser.FormatIPv4AddressNumber((int)(address >> 8 & 255U), addressString, ref result);
			addressString[result++] = '.';
			IPAddressParser.FormatIPv4AddressNumber((int)(address >> 16 & 255U), addressString, ref result);
			addressString[result++] = '.';
			IPAddressParser.FormatIPv4AddressNumber((int)(address >> 24 & 255U), addressString, ref result);
			return result;
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0009BDEB File Offset: 0x00099FEB
		internal static string IPv6AddressToString(ushort[] address, uint scopeId)
		{
			return StringBuilderCache.GetStringAndRelease(IPAddressParser.IPv6AddressToStringHelper(address, scopeId));
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0009BDFC File Offset: 0x00099FFC
		internal static bool IPv6AddressToString(ushort[] address, uint scopeId, Span<char> destination, out int charsWritten)
		{
			StringBuilder stringBuilder = IPAddressParser.IPv6AddressToStringHelper(address, scopeId);
			if (destination.Length < stringBuilder.Length)
			{
				StringBuilderCache.Release(stringBuilder);
				charsWritten = 0;
				return false;
			}
			stringBuilder.CopyTo(0, destination, stringBuilder.Length);
			charsWritten = stringBuilder.Length;
			StringBuilderCache.Release(stringBuilder);
			return true;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0009BE48 File Offset: 0x0009A048
		internal static StringBuilder IPv6AddressToStringHelper(ushort[] address, uint scopeId)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(65);
			if (IPv6AddressHelper.ShouldHaveIpv4Embedded(address))
			{
				IPAddressParser.AppendSections(address, 0, 6, stringBuilder);
				if (stringBuilder[stringBuilder.Length - 1] != ':')
				{
					stringBuilder.Append(':');
				}
				IPAddressParser.IPv4AddressToString(IPAddressParser.ExtractIPv4Address(address), stringBuilder);
			}
			else
			{
				IPAddressParser.AppendSections(address, 0, 8, stringBuilder);
			}
			if (scopeId != 0U)
			{
				stringBuilder.Append('%').Append(scopeId);
			}
			return stringBuilder;
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x0009BEB8 File Offset: 0x0009A0B8
		private unsafe static void FormatIPv4AddressNumber(int number, char* addressString, ref int offset)
		{
			offset += ((number > 99) ? 3 : ((number > 9) ? 2 : 1));
			int num = offset;
			do
			{
				int num2;
				number = Math.DivRem(number, 10, out num2);
				addressString[--num] = (char)(48 + num2);
			}
			while (number != 0);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x0009BF00 File Offset: 0x0009A100
		public unsafe static bool Ipv4StringToAddress(ReadOnlySpan<char> ipSpan, out long address)
		{
			int length = ipSpan.Length;
			long num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(ipSpan))
			{
				num = IPv4AddressHelper.ParseNonCanonical(reference, 0, ref length, true);
			}
			if (num != -1L && length == ipSpan.Length)
			{
				address = (long)(((ulong)-16777216 & (ulong)num) >> 24 | (ulong)((16711680L & num) >> 8) | (ulong)((ulong)(65280L & num) << 8) | (ulong)((ulong)(255L & num) << 24));
				return true;
			}
			address = 0L;
			return false;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0009BF70 File Offset: 0x0009A170
		public unsafe static bool Ipv6StringToAddress(ReadOnlySpan<char> ipSpan, ushort* numbers, int numbersLength, out uint scope)
		{
			int length = ipSpan.Length;
			bool flag;
			fixed (char* reference = MemoryMarshal.GetReference<char>(ipSpan))
			{
				flag = IPv6AddressHelper.IsValidStrict(reference, 0, ref length);
			}
			if (flag || length != ipSpan.Length)
			{
				string text = null;
				IPv6AddressHelper.Parse(ipSpan, numbers, 0, ref text);
				long num = 0L;
				if (!string.IsNullOrEmpty(text))
				{
					if (text.Length < 2)
					{
						scope = 0U;
						return false;
					}
					for (int i = 1; i < text.Length; i++)
					{
						char c = text[i];
						if (c < '0' || c > '9')
						{
							scope = 0U;
							return true;
						}
						num = num * 10L + (long)(c - '0');
						if (num > (long)((ulong)-1))
						{
							scope = 0U;
							return false;
						}
					}
				}
				scope = (uint)num;
				return true;
			}
			scope = 0U;
			return false;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0009C01C File Offset: 0x0009A21C
		private static void AppendSections(ushort[] address, int fromInclusive, int toExclusive, StringBuilder buffer)
		{
			ValueTuple<int, int> valueTuple = IPv6AddressHelper.FindCompressionRange(new ReadOnlySpan<ushort>(address, fromInclusive, toExclusive - fromInclusive));
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			bool flag = false;
			for (int i = fromInclusive; i < item; i++)
			{
				if (flag)
				{
					buffer.Append(':');
				}
				flag = true;
				IPAddressParser.AppendHex(address[i], buffer);
			}
			if (item >= 0)
			{
				buffer.Append("::");
				flag = false;
				fromInclusive = item2;
			}
			for (int j = fromInclusive; j < toExclusive; j++)
			{
				if (flag)
				{
					buffer.Append(':');
				}
				flag = true;
				IPAddressParser.AppendHex(address[j], buffer);
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x0009C0A8 File Offset: 0x0009A2A8
		private unsafe static void AppendHex(ushort value, StringBuilder buffer)
		{
			char* ptr = stackalloc char[(UIntPtr)8];
			int num = 4;
			do
			{
				int num2 = (int)(value % 16);
				value /= 16;
				ptr[(IntPtr)(--num) * 2] = ((num2 < 10) ? ((char)(48 + num2)) : ((char)(97 + (num2 - 10))));
			}
			while (value != 0);
			buffer.Append(ptr + num, 4 - num);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x0009C0FA File Offset: 0x0009A2FA
		private static uint ExtractIPv4Address(ushort[] address)
		{
			return (uint)((int)IPAddressParser.Reverse(address[7]) << 16 | (int)IPAddressParser.Reverse(address[6]));
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x0009C110 File Offset: 0x0009A310
		private static ushort Reverse(ushort number)
		{
			return (ushort)((number >> 8 & 255) | ((int)number << 8 & 65280));
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x0000219B File Offset: 0x0000039B
		public IPAddressParser()
		{
		}

		// Token: 0x040018F1 RID: 6385
		private const int MaxIPv4StringLength = 15;
	}
}
