using System;
using System.IO;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000043 RID: 67
	public struct IdentifierBasedRelativeCacheLocationGenerator : IRelativeCacheLocationGenerator
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B535 File Offset: 0x00009735
		internal static IdentifierBasedRelativeCacheLocationGenerator Fallback
		{
			[CompilerGenerated]
			get
			{
				return IdentifierBasedRelativeCacheLocationGenerator.<Fallback>k__BackingField;
			}
		} = new IdentifierBasedRelativeCacheLocationGenerator
		{
			IsFallback = true
		};

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000B53C File Offset: 0x0000973C
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000B544 File Offset: 0x00009744
		internal bool IsFallback
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<IsFallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsFallback>k__BackingField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B54D File Offset: 0x0000974D
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000B555 File Offset: 0x00009755
		public string Identifier
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Identifier>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Identifier>k__BackingField = value;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B560 File Offset: 0x00009760
		public string GetRelativeCacheFilePath(IServiceHub serviceHub)
		{
			FileInfo relativeFile;
			while ((relativeFile = serviceHub.CacheController.GetRelativeFile(this.GeneratePath())).Exists && this.IsFallback)
			{
			}
			return relativeFile.FullName;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B598 File Offset: 0x00009798
		private string GeneratePath()
		{
			return Path.Combine("Parse", this.IsFallback ? "_fallback" : "_global", (this.IsFallback ? new Random().Next().ToString() : this.Identifier) + ".cachefile");
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B5F0 File Offset: 0x000097F0
		// Note: this type is marked as 'beforefieldinit'.
		static IdentifierBasedRelativeCacheLocationGenerator()
		{
		}

		// Token: 0x04000099 RID: 153
		[CompilerGenerated]
		private static readonly IdentifierBasedRelativeCacheLocationGenerator <Fallback>k__BackingField;

		// Token: 0x0400009A RID: 154
		[CompilerGenerated]
		private bool <IsFallback>k__BackingField;

		// Token: 0x0400009B RID: 155
		[CompilerGenerated]
		private string <Identifier>k__BackingField;
	}
}
