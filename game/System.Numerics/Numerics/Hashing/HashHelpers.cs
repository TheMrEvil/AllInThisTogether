using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000017 RID: 23
	internal static class HashHelpers
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00010E71 File Offset: 0x0000F071
		public static int Combine(int h1, int h2)
		{
			return (h1 << 5 | (int)((uint)h1 >> 27)) + h1 ^ h2;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00010E80 File Offset: 0x0000F080
		// Note: this type is marked as 'beforefieldinit'.
		static HashHelpers()
		{
		}

		// Token: 0x04000098 RID: 152
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
