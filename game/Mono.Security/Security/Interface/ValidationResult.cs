using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000037 RID: 55
	public class ValidationResult
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000EED6 File Offset: 0x0000D0D6
		public ValidationResult(bool trusted, bool user_denied, int error_code, MonoSslPolicyErrors? policy_errors)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
			this.policy_errors = policy_errors;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000EEFB File Offset: 0x0000D0FB
		internal ValidationResult(bool trusted, bool user_denied, int error_code)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000EF18 File Offset: 0x0000D118
		public bool Trusted
		{
			get
			{
				return this.trusted;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000EF20 File Offset: 0x0000D120
		public bool UserDenied
		{
			get
			{
				return this.user_denied;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000EF28 File Offset: 0x0000D128
		public int ErrorCode
		{
			get
			{
				return this.error_code;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000EF30 File Offset: 0x0000D130
		public MonoSslPolicyErrors? PolicyErrors
		{
			get
			{
				return this.policy_errors;
			}
		}

		// Token: 0x04000143 RID: 323
		private bool trusted;

		// Token: 0x04000144 RID: 324
		private bool user_denied;

		// Token: 0x04000145 RID: 325
		private int error_code;

		// Token: 0x04000146 RID: 326
		private MonoSslPolicyErrors? policy_errors;
	}
}
