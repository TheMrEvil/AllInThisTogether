using System;

namespace System.Numerics.Hashing
{
	// Token: 0x0200000E RID: 14
	internal static class HashHelpers
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002434 File Offset: 0x00000634
		public static int Combine(int h1, int h2)
		{
			return (h1 << 5 | (int)((uint)h1 >> 27)) + h1 ^ h2;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002444 File Offset: 0x00000644
		// Note: this type is marked as 'beforefieldinit'.
		static HashHelpers()
		{
		}

		// Token: 0x04000091 RID: 145
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
