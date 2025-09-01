using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000036 RID: 54
	public sealed class ManifestResourceInfo
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00008261 File Offset: 0x00006461
		internal ManifestResourceInfo(ModuleReader module, int index)
		{
			this.module = module;
			this.index = index;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008277 File Offset: 0x00006477
		public ResourceAttributes __ResourceAttributes
		{
			get
			{
				return (ResourceAttributes)this.module.ManifestResource.records[this.index].Flags;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008299 File Offset: 0x00006499
		public int __Offset
		{
			get
			{
				return this.module.ManifestResource.records[this.index].Offset;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000082BC File Offset: 0x000064BC
		public ResourceLocation ResourceLocation
		{
			get
			{
				int implementation = this.module.ManifestResource.records[this.index].Implementation;
				if (implementation >> 24 == 35)
				{
					Assembly referencedAssembly = this.ReferencedAssembly;
					if (referencedAssembly == null || referencedAssembly.__IsMissing)
					{
						return ResourceLocation.ContainedInAnotherAssembly;
					}
					return referencedAssembly.GetManifestResourceInfo(this.module.GetString(this.module.ManifestResource.records[this.index].Name)).ResourceLocation | ResourceLocation.ContainedInAnotherAssembly;
				}
				else
				{
					if (implementation >> 24 != 38)
					{
						throw new BadImageFormatException();
					}
					if ((implementation & 16777215) == 0)
					{
						return ResourceLocation.Embedded | ResourceLocation.ContainedInManifestFile;
					}
					return (ResourceLocation)0;
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000835C File Offset: 0x0000655C
		public Assembly ReferencedAssembly
		{
			get
			{
				int implementation = this.module.ManifestResource.records[this.index].Implementation;
				if (implementation >> 24 == 35)
				{
					return this.module.ResolveAssemblyRef((implementation & 16777215) - 1);
				}
				return null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000083A8 File Offset: 0x000065A8
		public string FileName
		{
			get
			{
				int implementation = this.module.ManifestResource.records[this.index].Implementation;
				if (implementation >> 24 != 38)
				{
					return null;
				}
				if ((implementation & 16777215) == 0)
				{
					return null;
				}
				return this.module.GetString(this.module.File.records[(implementation & 16777215) - 1].Name);
			}
		}

		// Token: 0x0400014E RID: 334
		private readonly ModuleReader module;

		// Token: 0x0400014F RID: 335
		private readonly int index;
	}
}
