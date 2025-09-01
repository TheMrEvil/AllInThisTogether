using System;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000046 RID: 70
	public class MetadataController : IMetadataController
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000BC5B File Offset: 0x00009E5B
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000BC63 File Offset: 0x00009E63
		public IHostManifestData HostManifestData
		{
			[CompilerGenerated]
			get
			{
				return this.<HostManifestData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HostManifestData>k__BackingField = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000BC6C File Offset: 0x00009E6C
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000BC74 File Offset: 0x00009E74
		public IEnvironmentData EnvironmentData
		{
			[CompilerGenerated]
			get
			{
				return this.<EnvironmentData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnvironmentData>k__BackingField = value;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000BC7D File Offset: 0x00009E7D
		public MetadataController()
		{
		}

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		private IHostManifestData <HostManifestData>k__BackingField;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		private IEnvironmentData <EnvironmentData>k__BackingField;
	}
}
