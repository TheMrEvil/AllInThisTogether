using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[UsedByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadingCache.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal static class AssetBundleLoadingCache
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004C RID: 76
		// (set) Token: 0x0600004D RID: 77
		internal static extern uint maxBlocksPerFile { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		internal static extern uint blockCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000050 RID: 80
		internal static extern uint blockSize { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000027E4 File Offset: 0x000009E4
		internal static uint memoryBudgetKB
		{
			get
			{
				return AssetBundleLoadingCache.blockCount * AssetBundleLoadingCache.blockSize;
			}
			set
			{
				uint num = Math.Max(value / AssetBundleLoadingCache.blockSize, 2U);
				uint num2 = Math.Max(AssetBundleLoadingCache.blockCount / 4U, 2U);
				bool flag = num != AssetBundleLoadingCache.blockCount || num2 != AssetBundleLoadingCache.maxBlocksPerFile;
				if (flag)
				{
					AssetBundleLoadingCache.blockCount = num;
					AssetBundleLoadingCache.maxBlocksPerFile = num2;
				}
			}
		}

		// Token: 0x04000010 RID: 16
		internal const int kMinAllowedBlockCount = 2;

		// Token: 0x04000011 RID: 17
		internal const int kMinAllowedMaxBlocksPerFile = 2;
	}
}
