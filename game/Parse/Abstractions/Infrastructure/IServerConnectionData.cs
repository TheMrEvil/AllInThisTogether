using System;
using System.Collections.Generic;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000097 RID: 151
	public interface IServerConnectionData
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000591 RID: 1425
		// (set) Token: 0x06000592 RID: 1426
		string ApplicationID { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000593 RID: 1427
		// (set) Token: 0x06000594 RID: 1428
		string ServerURI { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000595 RID: 1429
		// (set) Token: 0x06000596 RID: 1430
		string Key { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000597 RID: 1431
		// (set) Token: 0x06000598 RID: 1432
		string MasterKey { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000599 RID: 1433
		// (set) Token: 0x0600059A RID: 1434
		IDictionary<string, string> Headers { get; set; }
	}
}
