using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000094 RID: 148
	public interface IMetadataController
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000577 RID: 1399
		IHostManifestData HostManifestData { get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000578 RID: 1400
		IEnvironmentData EnvironmentData { get; }
	}
}
