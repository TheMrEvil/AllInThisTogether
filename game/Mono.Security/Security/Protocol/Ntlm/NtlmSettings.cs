using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000030 RID: 48
	public static class NtlmSettings
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000E14E File Offset: 0x0000C34E
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000E155 File Offset: 0x0000C355
		public static NtlmAuthLevel DefaultAuthLevel
		{
			get
			{
				return NtlmSettings.defaultAuthLevel;
			}
			set
			{
				NtlmSettings.defaultAuthLevel = value;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E15D File Offset: 0x0000C35D
		// Note: this type is marked as 'beforefieldinit'.
		static NtlmSettings()
		{
		}

		// Token: 0x04000114 RID: 276
		private static NtlmAuthLevel defaultAuthLevel = NtlmAuthLevel.LM_and_NTLM_and_try_NTLMv2_Session;
	}
}
