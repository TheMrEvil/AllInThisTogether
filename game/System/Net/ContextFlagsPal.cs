using System;

namespace System.Net
{
	// Token: 0x02000561 RID: 1377
	[Flags]
	internal enum ContextFlagsPal
	{
		// Token: 0x040017FD RID: 6141
		None = 0,
		// Token: 0x040017FE RID: 6142
		Delegate = 1,
		// Token: 0x040017FF RID: 6143
		MutualAuth = 2,
		// Token: 0x04001800 RID: 6144
		ReplayDetect = 4,
		// Token: 0x04001801 RID: 6145
		SequenceDetect = 8,
		// Token: 0x04001802 RID: 6146
		Confidentiality = 16,
		// Token: 0x04001803 RID: 6147
		UseSessionKey = 32,
		// Token: 0x04001804 RID: 6148
		AllocateMemory = 256,
		// Token: 0x04001805 RID: 6149
		Connection = 2048,
		// Token: 0x04001806 RID: 6150
		InitExtendedError = 16384,
		// Token: 0x04001807 RID: 6151
		AcceptExtendedError = 32768,
		// Token: 0x04001808 RID: 6152
		InitStream = 32768,
		// Token: 0x04001809 RID: 6153
		AcceptStream = 65536,
		// Token: 0x0400180A RID: 6154
		InitIntegrity = 65536,
		// Token: 0x0400180B RID: 6155
		AcceptIntegrity = 131072,
		// Token: 0x0400180C RID: 6156
		InitManualCredValidation = 524288,
		// Token: 0x0400180D RID: 6157
		InitUseSuppliedCreds = 128,
		// Token: 0x0400180E RID: 6158
		InitIdentify = 131072,
		// Token: 0x0400180F RID: 6159
		AcceptIdentify = 524288,
		// Token: 0x04001810 RID: 6160
		ProxyBindings = 67108864,
		// Token: 0x04001811 RID: 6161
		AllowMissingBindings = 268435456,
		// Token: 0x04001812 RID: 6162
		UnverifiedTargetName = 536870912
	}
}
