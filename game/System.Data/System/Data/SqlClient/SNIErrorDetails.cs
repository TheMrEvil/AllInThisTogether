using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000257 RID: 599
	internal struct SNIErrorDetails
	{
		// Token: 0x04001384 RID: 4996
		public string errorMessage;

		// Token: 0x04001385 RID: 4997
		public uint nativeError;

		// Token: 0x04001386 RID: 4998
		public uint sniErrorNumber;

		// Token: 0x04001387 RID: 4999
		public int provider;

		// Token: 0x04001388 RID: 5000
		public uint lineNumber;

		// Token: 0x04001389 RID: 5001
		public string function;

		// Token: 0x0400138A RID: 5002
		public Exception exception;
	}
}
