using System;

namespace System.Net
{
	// Token: 0x0200056C RID: 1388
	internal readonly struct SecurityStatusPal
	{
		// Token: 0x06002CF2 RID: 11506 RVA: 0x0009A14A File Offset: 0x0009834A
		public SecurityStatusPal(SecurityStatusPalErrorCode errorCode, Exception exception = null)
		{
			this.ErrorCode = errorCode;
			this.Exception = exception;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0009A15C File Offset: 0x0009835C
		public override string ToString()
		{
			if (this.Exception != null)
			{
				return string.Format("{0}={1}, {2}={3}", new object[]
				{
					"ErrorCode",
					this.ErrorCode,
					"Exception",
					this.Exception
				});
			}
			return string.Format("{0}={1}", "ErrorCode", this.ErrorCode);
		}

		// Token: 0x04001842 RID: 6210
		public readonly SecurityStatusPalErrorCode ErrorCode;

		// Token: 0x04001843 RID: 6211
		public readonly Exception Exception;
	}
}
