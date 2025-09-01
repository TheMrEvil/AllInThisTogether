using System;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x0200003D RID: 61
	public class AbsoluteCacheLocationMutator : IServiceHubMutator
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000AF70 File Offset: 0x00009170
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000AF78 File Offset: 0x00009178
		public string CustomAbsoluteCacheFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<CustomAbsoluteCacheFilePath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CustomAbsoluteCacheFilePath>k__BackingField = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000AF81 File Offset: 0x00009181
		public bool Valid
		{
			get
			{
				return this.CustomAbsoluteCacheFilePath != null;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AF8C File Offset: 0x0000918C
		public void Mutate(ref IMutableServiceHub target, in IServiceHub composedHub)
		{
			IDiskFileCacheController diskFileCacheController = target.CacheController as IDiskFileCacheController;
			if (diskFileCacheController != null)
			{
				diskFileCacheController.AbsoluteCacheFilePath = this.CustomAbsoluteCacheFilePath;
				diskFileCacheController.RefreshPaths();
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000AFBB File Offset: 0x000091BB
		public AbsoluteCacheLocationMutator()
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000AFC3 File Offset: 0x000091C3
		void IServiceHubMutator.Mutate(ref IMutableServiceHub target, in IServiceHub composedHub)
		{
			this.Mutate(ref target, composedHub);
		}

		// Token: 0x04000089 RID: 137
		[CompilerGenerated]
		private string <CustomAbsoluteCacheFilePath>k__BackingField;
	}
}
