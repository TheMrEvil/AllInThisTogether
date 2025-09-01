using System;

namespace System.Net
{
	// Token: 0x020005EA RID: 1514
	internal enum IgnoreCertProblem
	{
		// Token: 0x04001B75 RID: 7029
		not_time_valid = 1,
		// Token: 0x04001B76 RID: 7030
		ctl_not_time_valid,
		// Token: 0x04001B77 RID: 7031
		not_time_nested = 4,
		// Token: 0x04001B78 RID: 7032
		invalid_basic_constraints = 8,
		// Token: 0x04001B79 RID: 7033
		all_not_time_valid = 7,
		// Token: 0x04001B7A RID: 7034
		allow_unknown_ca = 16,
		// Token: 0x04001B7B RID: 7035
		wrong_usage = 32,
		// Token: 0x04001B7C RID: 7036
		invalid_name = 64,
		// Token: 0x04001B7D RID: 7037
		invalid_policy = 128,
		// Token: 0x04001B7E RID: 7038
		end_rev_unknown = 256,
		// Token: 0x04001B7F RID: 7039
		ctl_signer_rev_unknown = 512,
		// Token: 0x04001B80 RID: 7040
		ca_rev_unknown = 1024,
		// Token: 0x04001B81 RID: 7041
		root_rev_unknown = 2048,
		// Token: 0x04001B82 RID: 7042
		all_rev_unknown = 3840,
		// Token: 0x04001B83 RID: 7043
		none = 4095
	}
}
