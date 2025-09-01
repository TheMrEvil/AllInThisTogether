using System;
using UnityEngine;

namespace Unity.MemoryProfiler
{
	// Token: 0x02000002 RID: 2
	internal static class MetadataInjector
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void PlayerInitMetadata()
		{
			MetadataInjector.InitializeMetadataCollection();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		private static void InitializeMetadataCollection()
		{
			MetadataInjector.DefaultCollector = new DefaultMetadataCollect();
		}

		// Token: 0x04000001 RID: 1
		public static DefaultMetadataCollect DefaultCollector;

		// Token: 0x04000002 RID: 2
		public static long CollectorCount;

		// Token: 0x04000003 RID: 3
		public static byte DefaultCollectorInjected;
	}
}
