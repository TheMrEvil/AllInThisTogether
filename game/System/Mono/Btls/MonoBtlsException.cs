using System;

namespace Mono.Btls
{
	// Token: 0x020000DA RID: 218
	internal class MonoBtlsException : Exception
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public MonoBtlsException()
		{
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		public MonoBtlsException(MonoBtlsSslError error) : base(error.ToString())
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		public MonoBtlsException(string message) : base(message)
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000DC38 File Offset: 0x0000BE38
		public MonoBtlsException(string format, params object[] args) : base(string.Format(format, args))
		{
		}
	}
}
