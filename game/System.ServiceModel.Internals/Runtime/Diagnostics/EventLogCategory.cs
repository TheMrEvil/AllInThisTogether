using System;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000046 RID: 70
	internal enum EventLogCategory : ushort
	{
		// Token: 0x04000172 RID: 370
		ServiceAuthorization = 1,
		// Token: 0x04000173 RID: 371
		MessageAuthentication,
		// Token: 0x04000174 RID: 372
		ObjectAccess,
		// Token: 0x04000175 RID: 373
		Tracing,
		// Token: 0x04000176 RID: 374
		WebHost,
		// Token: 0x04000177 RID: 375
		FailFast,
		// Token: 0x04000178 RID: 376
		MessageLogging,
		// Token: 0x04000179 RID: 377
		PerformanceCounter,
		// Token: 0x0400017A RID: 378
		Wmi,
		// Token: 0x0400017B RID: 379
		ComPlus,
		// Token: 0x0400017C RID: 380
		StateMachine,
		// Token: 0x0400017D RID: 381
		Wsat,
		// Token: 0x0400017E RID: 382
		SharingService,
		// Token: 0x0400017F RID: 383
		ListenerAdapter
	}
}
