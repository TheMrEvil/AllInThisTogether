using System;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x0200004B RID: 75
	public class RelativeCacheLocationMutator : IServiceHubMutator
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000C45F File Offset: 0x0000A65F
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000C467 File Offset: 0x0000A667
		public IRelativeCacheLocationGenerator RelativeCacheLocationGenerator
		{
			[CompilerGenerated]
			get
			{
				return this.<RelativeCacheLocationGenerator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RelativeCacheLocationGenerator>k__BackingField = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000C470 File Offset: 0x0000A670
		public bool Valid
		{
			get
			{
				return this.RelativeCacheLocationGenerator != null;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C47C File Offset: 0x0000A67C
		public void Mutate(ref IMutableServiceHub target, in IServiceHub referenceHub)
		{
			IMutableServiceHub mutableServiceHub = target;
			ICacheController cacheController = target.CacheController;
			ICacheController cacheController2;
			if (cacheController != null)
			{
				IDiskFileCacheController diskFileCacheController = cacheController as IDiskFileCacheController;
				if (diskFileCacheController == null)
				{
					cacheController2 = cacheController;
				}
				else
				{
					cacheController2 = new ValueTuple<IDiskFileCacheController, string>(diskFileCacheController, diskFileCacheController.RelativeCacheFilePath = this.RelativeCacheLocationGenerator.GetRelativeCacheFilePath(referenceHub)).Item1;
				}
			}
			else
			{
				cacheController2 = new CacheController
				{
					RelativeCacheFilePath = this.RelativeCacheLocationGenerator.GetRelativeCacheFilePath(referenceHub)
				};
			}
			mutableServiceHub.CacheController = cacheController2;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
		public RelativeCacheLocationMutator()
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C4F1 File Offset: 0x0000A6F1
		void IServiceHubMutator.Mutate(ref IMutableServiceHub target, in IServiceHub composedHub)
		{
			this.Mutate(ref target, composedHub);
		}

		// Token: 0x040000BD RID: 189
		[CompilerGenerated]
		private IRelativeCacheLocationGenerator <RelativeCacheLocationGenerator>k__BackingField;
	}
}
