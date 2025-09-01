using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000244 RID: 580
	internal enum SniContext
	{
		// Token: 0x040012EE RID: 4846
		Undefined,
		// Token: 0x040012EF RID: 4847
		Snix_Connect,
		// Token: 0x040012F0 RID: 4848
		Snix_PreLoginBeforeSuccessfulWrite,
		// Token: 0x040012F1 RID: 4849
		Snix_PreLogin,
		// Token: 0x040012F2 RID: 4850
		Snix_LoginSspi,
		// Token: 0x040012F3 RID: 4851
		Snix_ProcessSspi,
		// Token: 0x040012F4 RID: 4852
		Snix_Login,
		// Token: 0x040012F5 RID: 4853
		Snix_EnableMars,
		// Token: 0x040012F6 RID: 4854
		Snix_AutoEnlist,
		// Token: 0x040012F7 RID: 4855
		Snix_GetMarsSession,
		// Token: 0x040012F8 RID: 4856
		Snix_Execute,
		// Token: 0x040012F9 RID: 4857
		Snix_Read,
		// Token: 0x040012FA RID: 4858
		Snix_Close,
		// Token: 0x040012FB RID: 4859
		Snix_SendRows
	}
}
