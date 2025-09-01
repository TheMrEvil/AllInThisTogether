using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000091 RID: 145
	public interface IEnvironmentData
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600056E RID: 1390
		string TimeZone { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600056F RID: 1391
		string OSVersion { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000570 RID: 1392
		// (set) Token: 0x06000571 RID: 1393
		string Platform { get; set; }
	}
}
