using System;
using UnityEngine.Profiling.Memory.Experimental;

namespace Unity.MemoryProfiler
{
	// Token: 0x02000003 RID: 3
	public abstract class MetadataCollect : IDisposable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		protected MetadataCollect()
		{
			if (MetadataInjector.DefaultCollector != null && MetadataInjector.DefaultCollector != this && MetadataInjector.DefaultCollectorInjected != 0)
			{
				MemoryProfiler.createMetaData -= MetadataInjector.DefaultCollector.CollectMetadata;
				MetadataInjector.CollectorCount -= 1L;
				MetadataInjector.DefaultCollectorInjected = 0;
			}
			MemoryProfiler.createMetaData += this.CollectMetadata;
			MetadataInjector.CollectorCount += 1L;
		}

		// Token: 0x06000004 RID: 4
		public abstract void CollectMetadata(MetaData data);

		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				MemoryProfiler.createMetaData -= this.CollectMetadata;
				MetadataInjector.CollectorCount -= 1L;
				if (MetadataInjector.DefaultCollector != null && MetadataInjector.CollectorCount < 1L && MetadataInjector.DefaultCollector != this)
				{
					MetadataInjector.DefaultCollectorInjected = 1;
					MemoryProfiler.createMetaData += MetadataInjector.DefaultCollector.CollectMetadata;
					MetadataInjector.CollectorCount += 1L;
				}
			}
		}

		// Token: 0x04000004 RID: 4
		private bool disposed;
	}
}
