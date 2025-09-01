using System;

namespace UnityEngine
{
	// Token: 0x020001B0 RID: 432
	public static class HashUnsafeUtilities
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x0001A277 File Offset: 0x00018477
		public unsafe static void ComputeHash128(void* data, ulong dataSize, ulong* hash1, ulong* hash2)
		{
			SpookyHash.Hash(data, dataSize, hash1, hash2);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0001A284 File Offset: 0x00018484
		public unsafe static void ComputeHash128(void* data, ulong dataSize, Hash128* hash)
		{
			ulong u64_ = hash->u64_0;
			ulong u64_2 = hash->u64_1;
			HashUnsafeUtilities.ComputeHash128(data, dataSize, &u64_, &u64_2);
			*hash = new Hash128(u64_, u64_2);
		}
	}
}
