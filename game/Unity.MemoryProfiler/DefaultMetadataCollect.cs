using System;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;

namespace Unity.MemoryProfiler
{
	// Token: 0x02000004 RID: 4
	internal class DefaultMetadataCollect : MetadataCollect
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002154 File Offset: 0x00000354
		public DefaultMetadataCollect()
		{
			MetadataInjector.DefaultCollectorInjected = 1;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002162 File Offset: 0x00000362
		public override void CollectMetadata(MetaData data)
		{
			data.content = "Project name: " + Application.productName;
			data.platform = string.Empty;
		}
	}
}
