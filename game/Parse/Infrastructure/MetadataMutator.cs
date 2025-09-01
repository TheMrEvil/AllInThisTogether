using System;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000047 RID: 71
	public class MetadataMutator : MetadataController, IServiceHubMutator
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000BC88 File Offset: 0x00009E88
		public bool Valid
		{
			get
			{
				if (this != null)
				{
					IEnvironmentData environmentData = this.EnvironmentData;
					if (environmentData != null && environmentData.OSVersion != null && environmentData.Platform != null && environmentData.TimeZone != null)
					{
						IHostManifestData hostManifestData = this.HostManifestData;
						if (hostManifestData != null && hostManifestData.Identifier != null && hostManifestData.Name != null && hostManifestData.ShortVersion != null)
						{
							return hostManifestData.Version != null;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000BCE9 File Offset: 0x00009EE9
		public void Mutate(ref IMutableServiceHub target, in IServiceHub referenceHub)
		{
			target.MetadataController = this;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000BCF3 File Offset: 0x00009EF3
		public MetadataMutator()
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000BCFB File Offset: 0x00009EFB
		void IServiceHubMutator.Mutate(ref IMutableServiceHub target, in IServiceHub composedHub)
		{
			this.Mutate(ref target, composedHub);
		}
	}
}
