using System;
using System.Diagnostics;
using Unity.Burst.LowLevel;
using UnityEngine;

namespace Unity.Burst
{
	// Token: 0x02000018 RID: 24
	internal static class SharedStatic
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00005662 File Offset: 0x00003862
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckSizeOf(uint sizeOf)
		{
			if (sizeOf == 0U)
			{
				throw new ArgumentException("sizeOf must be > 0", "sizeOf");
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005677 File Offset: 0x00003877
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe static void CheckResult(void* result)
		{
			if (result == null)
			{
				throw new InvalidOperationException("Unable to create a SharedStatic for this key. This is most likely due to the size of the struct inside of the SharedStatic having changed or the same key being reused for differently sized values. To fix this the editor needs to be restarted.");
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000568C File Offset: 0x0000388C
		[SharedStatic.PreserveAttribute]
		public unsafe static void* GetOrCreateSharedStaticInternal(long getHashCode64, long getSubHashCode64, uint sizeOf, uint alignment)
		{
			Hash128 hash = new Hash128((ulong)getHashCode64, (ulong)getSubHashCode64);
			return BurstCompilerService.GetOrCreateSharedMemory(ref hash, sizeOf, alignment);
		}

		// Token: 0x0200003C RID: 60
		internal class PreserveAttribute : Attribute
		{
			// Token: 0x06000164 RID: 356 RVA: 0x00007DAC File Offset: 0x00005FAC
			public PreserveAttribute()
			{
			}
		}
	}
}
