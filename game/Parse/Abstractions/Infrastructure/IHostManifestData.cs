using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000092 RID: 146
	public interface IHostManifestData
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000572 RID: 1394
		string Version { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000573 RID: 1395
		string ShortVersion { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000574 RID: 1396
		string Identifier { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000575 RID: 1397
		string Name { get; }
	}
}
