using System;

namespace UnityEngine.XR
{
	// Token: 0x0200002A RID: 42
	internal static class HashCodeHelper
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00004734 File Offset: 0x00002934
		public static int Combine(int hash1, int hash2)
		{
			return hash1 * 486187739 + hash2;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004750 File Offset: 0x00002950
		public static int Combine(int hash1, int hash2, int hash3)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2), hash3);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000475F File Offset: 0x0000295F
		public static int Combine(int hash1, int hash2, int hash3, int hash4)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3), hash4);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000476F File Offset: 0x0000296F
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4), hash5);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004781 File Offset: 0x00002981
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5), hash6);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004795 File Offset: 0x00002995
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6), hash7);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000047AB File Offset: 0x000029AB
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7, int hash8)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6, hash7), hash8);
		}

		// Token: 0x040000F1 RID: 241
		private const int k_HashCodeMultiplier = 486187739;
	}
}
