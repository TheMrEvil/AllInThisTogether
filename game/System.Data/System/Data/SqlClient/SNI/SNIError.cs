using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200028F RID: 655
	internal class SNIError
	{
		// Token: 0x06001E3C RID: 7740 RVA: 0x0008F394 File Offset: 0x0008D594
		public SNIError(SNIProviders provider, uint nativeError, uint sniErrorCode, string errorMessage)
		{
			this.lineNumber = 0U;
			this.function = string.Empty;
			this.provider = provider;
			this.nativeError = nativeError;
			this.sniError = sniErrorCode;
			this.errorMessage = errorMessage;
			this.exception = null;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0008F3D4 File Offset: 0x0008D5D4
		public SNIError(SNIProviders provider, uint sniErrorCode, Exception sniException)
		{
			this.lineNumber = 0U;
			this.function = string.Empty;
			this.provider = provider;
			this.nativeError = 0U;
			this.sniError = sniErrorCode;
			this.errorMessage = string.Empty;
			this.exception = sniException;
		}

		// Token: 0x040014FB RID: 5371
		public readonly SNIProviders provider;

		// Token: 0x040014FC RID: 5372
		public readonly string errorMessage;

		// Token: 0x040014FD RID: 5373
		public readonly uint nativeError;

		// Token: 0x040014FE RID: 5374
		public readonly uint sniError;

		// Token: 0x040014FF RID: 5375
		public readonly string function;

		// Token: 0x04001500 RID: 5376
		public readonly uint lineNumber;

		// Token: 0x04001501 RID: 5377
		public readonly Exception exception;
	}
}
