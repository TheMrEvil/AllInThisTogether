using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200002E RID: 46
	public enum NtlmAuthLevel
	{
		// Token: 0x04000105 RID: 261
		LM_and_NTLM,
		// Token: 0x04000106 RID: 262
		LM_and_NTLM_and_try_NTLMv2_Session,
		// Token: 0x04000107 RID: 263
		NTLM_only,
		// Token: 0x04000108 RID: 264
		NTLMv2_only
	}
}
