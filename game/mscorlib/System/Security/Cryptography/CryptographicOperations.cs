using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000470 RID: 1136
	public static class CryptographicOperations
	{
		// Token: 0x06002DFF RID: 11775 RVA: 0x000A5998 File Offset: 0x000A3B98
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public unsafe static bool FixedTimeEquals(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			int length = left.Length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				num |= (int)(*left[i] - *right[i]);
			}
			return num == 0;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000A59E7 File Offset: 0x000A3BE7
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static void ZeroMemory(Span<byte> buffer)
		{
			buffer.Clear();
		}
	}
}
