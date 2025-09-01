using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E0 RID: 480
	internal enum SqlConnectionTimeoutErrorPhase
	{
		// Token: 0x04000F18 RID: 3864
		Undefined,
		// Token: 0x04000F19 RID: 3865
		PreLoginBegin,
		// Token: 0x04000F1A RID: 3866
		InitializeConnection,
		// Token: 0x04000F1B RID: 3867
		SendPreLoginHandshake,
		// Token: 0x04000F1C RID: 3868
		ConsumePreLoginHandshake,
		// Token: 0x04000F1D RID: 3869
		LoginBegin,
		// Token: 0x04000F1E RID: 3870
		ProcessConnectionAuth,
		// Token: 0x04000F1F RID: 3871
		PostLogin,
		// Token: 0x04000F20 RID: 3872
		Complete,
		// Token: 0x04000F21 RID: 3873
		Count
	}
}
