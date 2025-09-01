using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001B8 RID: 440
	internal static class FixedBufferExtensions
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x0004D74C File Offset: 0x0004B94C
		internal unsafe static string GetStringFromFixedBuffer(this ReadOnlySpan<char> span)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(span))
			{
				return new string(reference, 0, span.GetFixedBufferStringLength());
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004D770 File Offset: 0x0004B970
		internal static int GetFixedBufferStringLength(this ReadOnlySpan<char> span)
		{
			int num = span.IndexOf('\0');
			if (num >= 0)
			{
				return num;
			}
			return span.Length;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004D794 File Offset: 0x0004B994
		internal unsafe static bool FixedBufferEqualsString(this ReadOnlySpan<char> span, string value)
		{
			if (value == null || value.Length > span.Length)
			{
				return false;
			}
			int i;
			for (i = 0; i < value.Length; i++)
			{
				if (value[i] == '\0' || value[i] != (char)(*span[i]))
				{
					return false;
				}
			}
			return i == span.Length || *span[i] == 0;
		}
	}
}
