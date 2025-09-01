using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000950 RID: 2384
	internal static class HashHelpers
	{
		// Token: 0x0600540D RID: 21517 RVA: 0x00118C77 File Offset: 0x00116E77
		public static int Combine(int h1, int h2)
		{
			return (h1 << 5 | (int)((uint)h1 >> 27)) + h1 ^ h2;
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00118C85 File Offset: 0x00116E85
		// Note: this type is marked as 'beforefieldinit'.
		static HashHelpers()
		{
		}

		// Token: 0x04003391 RID: 13201
		public static readonly int RandomSeed = new Random().Next(int.MinValue, int.MaxValue);
	}
}
